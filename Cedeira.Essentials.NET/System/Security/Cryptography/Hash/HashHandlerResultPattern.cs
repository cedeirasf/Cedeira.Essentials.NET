using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashHandlerResultPattern<T> : IHashHandlerResultPattern<T> where T : IEquatable<T>    
    {
        private readonly HashAlgorithm _hashAlgorithm;
        private readonly IResultFactory _resultFactory;
        private readonly Func<byte[], T> _hashFormatter;

        public HashHandlerResultPattern(HashAlgorithm hashAlgorithm, IResultFactory resultFactory, Func<byte[], T> hashFormatter)
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
            IResult<T> computedHash = CalculateHash(input);

            bool isValid = computedHash.Equals(hash);

            return isValid
                ? _resultFactory.Success(isValid)
                : _resultFactory.Failure("Hashes do not match.");
        }

        public IResult HashValidate(byte[] input, T hash)
        {
            IResult<T> computedHash = CalculateHash(input);

            bool isValid = computedHash.Equals(hash);

            return isValid
                ? _resultFactory.Success(isValid)
                : _resultFactory.Failure("Hashes do not match.");
        }

        public IResult HashValidate(SecureString input, T hash)
        {
            IResult<T> computedHash = CalculateHash(input);

            bool isValid = computedHash.Equals(hash);

            return isValid
                ? _resultFactory.Success(isValid)
                : _resultFactory.Failure("Hashes do not match.");
        }

        public IResult HashValidate(StreamReader input, T hash)
        {
            IResult<T> computedHash = CalculateHash(input);

            bool isValid = computedHash.Equals(hash);

            return isValid
                ? _resultFactory.Success(isValid)
                : _resultFactory.Failure("Hashes do not match.");
        }

        protected byte[] ComputeHash(byte[] input)
        {
            return _hashAlgorithm.ComputeHash(input);
        }

    }
}
