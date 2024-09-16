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
            _hashFormatter = BytesToHex;
        }

        public HashHandler(HashAlgorithm hashAlgorithm, Func<byte[], string> hashFormatter)
        {
            _hashAlgorithm = hashAlgorithm;
            _hashFormatter = hashFormatter;
        }

        public static string BytesToHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public string CalculateHash(string input)
        {
            ValidateNullInput(input);
            byte[] hashBytes = ComputeHash(Encoding.UTF8.GetBytes(input));

            return _hashFormatter(hashBytes);
        }


        public string CalculateHash(byte[] input)
        {
            ValidateNullInput(input);
            byte[] hashBytes = ComputeHash(input);

            return _hashFormatter(hashBytes);
        }

        public string CalculateHash(StreamReader input)
        {
            ValidateNullInput(input);
            byte[] hashBytes = ComputeHash(input);

            return _hashFormatter(hashBytes);
        }

        public string CalculateHash(SecureString input)
        {
            ValidateNullInput(input);

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
        private byte[] ComputeHash(byte[] input)
        {
            return _hashAlgorithm.ComputeHash(input);
        }

        private byte[] ComputeHash(StreamReader input)
        {
            input.BaseStream.Position = 0;
            input.DiscardBufferedData();
            return _hashAlgorithm.ComputeHash(input.BaseStream);
        }
        private void ValidateNullInput<T>(T input)
        {
            if (input is null)
            {
                throw new ArgumentException("Input can not be null");
            }
        }

    }
}
