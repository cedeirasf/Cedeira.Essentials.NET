using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashHandlerResult<T> : IHashHandlerResultPattern<T> where T : IEquatable<T>    
    {
        private readonly HashAlgorithm _hashAlgorithm;
        private readonly IResultFactory _resultFactory;
        private readonly Func<byte[], T> _hashFormatter;

        public HashHandlerResult(HashAlgorithm hashAlgorithm, IResultFactory resultFactory, Func<byte[], T> hashFormatter)
        {
            _hashAlgorithm = hashAlgorithm;
            _resultFactory = resultFactory;
            _hashFormatter = hashFormatter;
        }

        public IResult<T> CalculateHash(string input)
        {
            byte[] hashBytes = ComputeHash(Encoding.UTF8.GetBytes(input));

            return _resultFactory.Success(_hashFormatter(hashBytes));
        }

        public IResult<T> CalculateHash(byte[] input)
        {
            byte[] hashBytes = ComputeHash(input);

            return _resultFactory.Success(_hashFormatter(hashBytes));
        }

        public IResult<T> CalculateHash(StreamReader input)
        {
            byte[] hashBytes = ComputeHash(Encoding.UTF8.GetBytes(input.ReadToEnd()));

            return _resultFactory.Success(_hashFormatter(hashBytes));
        }

        public IResult<T> CalculateHash(SecureString input)
        {
            var bstr = Marshal.SecureStringToBSTR(input);
            try
            {
                var length = Marshal.ReadInt32(bstr, -4);
                var bytes = new byte[length];
                Marshal.Copy(bstr, bytes, 0, length);
                byte[] hashBytes = ComputeHash(bytes);
                return _resultFactory.Success(_hashFormatter(hashBytes));
            }
            finally
            {
                Marshal.ZeroFreeBSTR(bstr);
            }
        }

        public IResult HashValidate(string input, T hash)
        {
            throw new NotImplementedException();
        }

        public IResult HashValidate(byte[] input, T hash)
        {
            throw new NotImplementedException();
        }

        public IResult HashValidate(SecureString input, T hash)
        {
            throw new NotImplementedException();
        }

        public IResult HashValidate(StreamReader input, T hash)
        {
            throw new NotImplementedException();
        }

        protected byte[] ComputeHash(byte[] input)
        {
            return _hashAlgorithm.ComputeHash(input);
        }

        ///// <summary>
        ///// Calcula el hash de una cadena de entrada utilizando el algoritmo de hash configurado.
        ///// </summary>
        ///// <param name="input">La cadena de entrada a ser hasheada.</param>
        ///// <returns>El hash de la cadena de entrada en formato hexadecimal.</returns>
        //public string CalculateHash(string input)
        //{
        //    byte[] hashBytes = ComputeHash(input);

        //    return ConvertHashToString(hashBytes);
        //}

        ///// <summary>
        ///// Calcula el hash de una cadena de entrada y lo escribe en un `Stream` de salida. 
        ///// Se valida la consistencia del Stream antes de proceder con la escritura.
        ///// </summary>
        ///// <param name="input">La cadena de entrada que será hasheada.</param>
        ///// <param name="output">El `Stream` de salida donde se escribirá el hash.</param>
        ///// <returns>Un objeto `IResult` indicando el éxito o el fallo de la operación.</returns>
        //public IResult CalculateHash(string input, Stream output)
        //{
        //    if (output == null)
        //        return _resultFactory.Failure("El stream de salida no puede ser null.");

        //    if (!output.CanWrite)
        //        return _resultFactory.Failure("El stream de salida no está en modo escritura.");

        //    byte[] hashBytes = ComputeHash(input);

        //    output.Write(hashBytes, 0, hashBytes.Length);

        //    return _resultFactory.Success(true);
        //}


        ///// <summary>
        ///// Valida si una cadena de hash proporcionada coincide con el hash calculado de una cadena de entrada.
        ///// </summary>
        ///// <param name="input">La cadena de entrada a ser validada</param>
        ///// <param name="hash">El hash con el que se comparará el hash calculado.</param>
        ///// <returns>Un objeto `IResult` que indica si el hash calculado coincide con el proporcionado</returns>
        //public IResult HashValidate(string input, string hash)
        //{
        //    string computedHash = CalculateHash(input);

        //    bool respuesta = string.Equals(computedHash, hash, StringComparison.OrdinalIgnoreCase);

        //    return _resultFactory.Success(respuesta);
        //}

        ///// <summary>
        /////  Calcula el hash de una cadena de entrada y devuelve el resultado como un arreglo de bytes.
        ///// </summary>
        ///// <param name="input">El arreglo de bytes que representa el hash calculado.</param>
        ///// <returns>El arreglo de bytes que representa el hash calculado.</returns>
        //protected byte[] ComputeHash(string input)
        //{
        //    byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        //    return _hashAlgorithm.ComputeHash(inputBytes);
        //}

        ///// <summary>
        ///// Convierte un arreglo de bytes de hash en su representación hexadecimal como una cadena.
        ///// </summary>
        ///// <param name="hashBytes">El arreglo de bytes del hash a convertir.</param>
        ///// <returns>Una cadena que representa el hash en formato hexadecimal.</returns>
        //protected string ConvertHashToString(byte[] hashBytes)
        //{
        //    StringBuilder sb = new StringBuilder(hashBytes.Length * 2);

        //    foreach (byte b in hashBytes) sb.AppendFormat("{0:x2}", b);

        //    return sb.ToString();
        //}
    }
}
