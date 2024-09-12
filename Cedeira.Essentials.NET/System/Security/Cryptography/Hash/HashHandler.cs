using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashHandler : IHashHandler 
    {
        private readonly HashAlgorithm _hashAlgorithm;
        private readonly Func<byte[], string> _hashFormatter;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="HashHandler"/> con el algoritmo de hash especificado.
        /// </summary>
        /// <param name="hashAlgorithm">El algoritmo de hash a utilizar.</param>
        public HashHandler(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
            _hashFormatter = BytesToHex;
        }

        public static string BytesToHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b); // Formato hexadecimal con dos dígitos, en minúscula
            }
            return hex.ToString();
        }

        public string CalculateHash(string input)
        {
            byte[] hashBytes = ComputeHash(Encoding.UTF8.GetBytes(input));
            return _hashFormatter(hashBytes);
        }

        public string CalculateHash(byte[] input)
        {
            byte[] hashBytes = ComputeHash(input);
            return _hashFormatter(hashBytes);
        }

        public string CalculateHash(StreamReader input)
        {
            byte[] hashBytes = ComputeHash(Encoding.UTF8.GetBytes(input.ReadToEnd()));
            return _hashFormatter(hashBytes);
        }

        public string CalculateHash(SecureString input)
        {
            var bstr = Marshal.SecureStringToBSTR(input);
            try
            {
                var length = Marshal.ReadInt32(bstr, -4);
                var bytes = new byte[length];

                Marshal.Copy(bstr, bytes, 0, length);

                byte[] hashBytes = ComputeHash(bytes);

                return _hashFormatter(hashBytes);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(bstr);
            }
        }

        public bool HashValidate(string input, string hash)
        {
            var computedHash = CalculateHash(input);
            return computedHash.Equals(hash);
        }
       
        public bool HashValidate(byte[] input, string hash)
        {
            var computedHash = CalculateHash(input);
            return computedHash.Equals(hash);
        }

        public bool HashValidate(SecureString input, string hash)
        {
            var computedHash = CalculateHash(input);
            return computedHash.Equals(hash);
        }

        public bool HashValidate(StreamReader input, string hash)
        {
            var computedHash = CalculateHash(input);
            return computedHash.Equals(hash);
        }

        public void ThrowIfInvalidHash(string input, string hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new CryptographicException("Invalid hash.");
            }
        }

        public void ThrowIfInvalidHash(byte[] input, string hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new CryptographicException("Invalid hash.");
            }
        }

        public void ThrowIfInvalidHash(SecureString input, string hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new CryptographicException("Invalid hash.");
            }
        }

        public void ThrowIfInvalidHash(StreamReader input, string hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new CryptographicException("Invalid hash.");
            }
        }
        protected byte[] ComputeHash(byte[] input)
        {
            return _hashAlgorithm.ComputeHash(input);
        }
    }
}
