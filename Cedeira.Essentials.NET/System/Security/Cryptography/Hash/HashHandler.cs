using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashHandler : IHashHandler
    {
        private readonly HashAlgorithm _hashAlgorithm;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="HashHandler"/> con el algoritmo de hash especificado.
        /// </summary>
        /// <param name="hashAlgorithm">El algoritmo de hash a utilizar.</param>
        public HashHandler(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        /// <summary>
        /// Calcula el hash de una cadena de entrada utilizando el algoritmo de hash configurado y devuelve el resultado en un arreglo de bytes.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser hasheada.</param>
        /// <returns>El hash de la cadena de entrada en formato de bytes.</returns>
        public byte[] CalculateHashByte(string input)
        {
            return ComputeHash(input);
        }

        /// <summary>
        /// Calcula el hash de una cadena de entrada utilizando el algoritmo de hash configurado y devuelve el resultado como una cadena en formato hexadecimal.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser hasheada.</param>
        /// <returns>El hash de la cadena de entrada en formato hexadecimal.</returns>
        public string CalculateHashHexadecimal(string input)
        {
            byte[] hashBytes = ComputeHash(input);

            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calcula el hash de una cadena de entrada utilizando el algoritmo de hash configurado y devuelve el resultado como un <see cref="Stream"/>.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser hasheada.</param>
        /// <returns>Un <see cref="Stream"/> que contiene el hash de la cadena de entrada.</returns>
        public Stream CalculateHashStream(string input)
        {
            byte[] hashBytes = ComputeHash(input);

            return new MemoryStream(hashBytes);
        }

        /// <summary>
        /// Calcula el hash de una cadena de entrada utilizando el algoritmo de hash configurado y devuelve el resultado en formato Base64.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser hasheada.</param>
        /// <returns>El hash de la cadena de entrada en formato Base64.</returns>
        public string CalculateHashBase64(string input)
        {
            byte[] hashBytes = ComputeHash(input);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Calcula el hash de una cadena de entrada y lo escribe en un <see cref="Stream"/> de salida.
        /// Se valida la consistencia del <see cref="Stream"/> antes de proceder con la escritura.
        /// </summary>
        /// <param name="input">La cadena de entrada que será hasheada.</param>
        /// <param name="output">El <see cref="Stream"/> de salida donde se escribirá el hash.</param>
        /// <exception cref="ArgumentException">Lanza una excepción si el <see cref="Stream"/> es null o no está en modo escritura.</exception>
        public void CalculateHash(string input, Stream output)
        {
            if (output == null)
                throw new ArgumentException("El stream de salida no puede ser null.");

            if (!output.CanWrite)
                throw new ArgumentException("El stream de salida no está en modo escritura.");

            byte[] hashBytes = ComputeHash(input);

            output.Write(hashBytes, 0, hashBytes.Length);
        }

        /// <summary>
        /// Valida si una cadena de hash proporcionada coincide con el hash calculado de una cadena de entrada.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser validada.</param>
        /// <param name="hash">El hash con el que se comparará el hash calculado.</param>
        /// <returns>Un valor booleano que indica si el hash calculado coincide con el hash proporcionado.</returns>
        public bool HashValidate(string input, string hash)
        {
            string computedHash = CalculateHashHexadecimal(input);

            return string.Equals(computedHash, hash, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Calcula el hash de una cadena de entrada y devuelve el resultado como un arreglo de bytes.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser hasheada.</param>
        /// <returns>El arreglo de bytes que representa el hash calculado.</returns>
        protected byte[] ComputeHash(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            return _hashAlgorithm.ComputeHash(inputBytes);
        }
    }
}
