using Cedeira.Essentials.NET.Diagnostics.Invariants;
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

        public HashHandler(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
            _hashFormatter = Convert.ToHexString;
        }

        public HashHandler(HashAlgorithm hashAlgorithm, Func<byte[], string> hashFormatter)
        {
            _hashAlgorithm = hashAlgorithm;
            _hashFormatter = hashFormatter;
        }

        public string CalculateHash(string input)
        {
            ValidateNull(input);

            byte[] hashBytes = ComputeHash(Encoding.UTF8.GetBytes(input));

            return _hashFormatter(hashBytes);
        }


        public string CalculateHash(byte[] input)
        {
            ValidateNull(input);

            byte[] hashBytes = ComputeHash(input);

            return _hashFormatter(hashBytes);
        }

        public string CalculateHash(StreamReader input)
        {
            ValidateNull(input);

            byte[] hashBytes = ComputeHash(input);

            return _hashFormatter(hashBytes);
        }

        public string CalculateHash(SecureString input)
        {
            ValidateNull(input);
        
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
            HashValidateNull(input, hash);

            var computedHash = CalculateHash(input);

            return computedHash.Equals(hash);
        }
       

        public bool HashValidate(byte[] input, string hash)
        {
            HashValidateNull(input, hash);

            var computedHash = CalculateHash(input);

            return computedHash.Equals(hash);
        }

        public bool HashValidate(SecureString input, string hash)
        {
            HashValidateNull(input, hash);

            var computedHash = CalculateHash(input);

            return computedHash.Equals(hash);
        }

        public bool HashValidate(StreamReader input, string hash)
        {
            HashValidateNull(input, hash);

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
        private byte[] ComputeHash(byte[] input)
        {
            return _hashAlgorithm.ComputeHash(input);
        }

        private byte[] ComputeHash(StreamReader input)
        {
            return _hashAlgorithm.ComputeHash(input.BaseStream);
        }
        private void ValidateNull<T>(T input)
        {
            Invariants.For(input).IsNotNull($"{nameof(input)} cannot be null.");
        }
        private void HashValidateNull<T>(T input, string hash)
        {
            Invariants.For(input).IsNotNull($"{nameof(input)} cannot be null.");
            Invariants.For(hash).IsNotNull($"{nameof(hash)} cannot be null.");
        }
    }
}
