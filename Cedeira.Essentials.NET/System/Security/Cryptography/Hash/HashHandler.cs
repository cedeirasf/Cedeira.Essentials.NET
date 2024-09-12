using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashHandler<T> : IHashHandler<T> where T :  IEquatable<T>
    {
        private readonly HashAlgorithm _hashAlgorithm;
        private readonly Func<byte[], T> _hashFormatter;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="HashHandler"/> con el algoritmo de hash especificado.
        /// </summary>
        /// <param name="hashAlgorithm">El algoritmo de hash a utilizar.</param>
        public HashHandler(HashAlgorithm hashAlgorithm, Func<byte[], T> hashFormatter)
        {
            _hashAlgorithm = hashAlgorithm;
            _hashFormatter = hashFormatter;
        }

        public T CalculateHash(string input)
        {
            byte[] hashBytes = ComputeHash(Encoding.UTF8.GetBytes(input));
            return _hashFormatter(hashBytes);
        }

        public T CalculateHash(byte[] input)
        {
            byte[] hashBytes = ComputeHash(input);
            return _hashFormatter(hashBytes);
        }

        public T CalculateHash(StreamReader input)
        {
            byte[] hashBytes = ComputeHash(Encoding.UTF8.GetBytes(input.ReadToEnd()));
            return _hashFormatter(hashBytes);
        }

        public T CalculateHash(SecureString input)
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

        public bool HashValidate(string input, T hash)
        {
            var computedHash = CalculateHash(input);
            return computedHash.Equals(hash);
        }
       
        public bool HashValidate(byte[] input, T hash)
        {
            var computedHash = CalculateHash(input);
            return computedHash.Equals(hash);
        }

        public bool HashValidate(SecureString input, T hash)
        {
            var computedHash = CalculateHash(input);
            return computedHash.Equals(hash);
        }

        public bool HashValidate(StreamReader input, T hash)
        {
            var computedHash = CalculateHash(input);
            return computedHash.Equals(hash);
        }

        public void ThrowIfInvalidHash(string input, T hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new ArgumentException("Invalid hash.");
            }
        }

        public void ThrowIfInvalidHash(byte[] input, T hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new ArgumentException("Invalid hash.");
            }
        }

        public void ThrowIfInvalidHash(SecureString input, T hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new ArgumentException("Invalid hash.");
            }
        }

        public void ThrowIfInvalidHash(StreamReader input, T hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new ArgumentException("Invalid hash.");
            }
        }
        protected byte[] ComputeHash(byte[] input)
        {
            return _hashAlgorithm.ComputeHash(input);
        }
    }
}
