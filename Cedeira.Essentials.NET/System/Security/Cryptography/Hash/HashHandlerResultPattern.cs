using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;
using System.Security;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashHandlerResultPattern : IHashHandlerResultPattern
    {
        private readonly IResultFactory _resultFactory;
        private readonly HashHandler _hashHandler;

        public HashHandlerResultPattern(HashAlgorithm hashAlgorithm, IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
            _hashHandler = new HashHandler(hashAlgorithm);
        }

        public HashHandlerResultPattern(HashAlgorithm hashAlgorithm, IResultFactory resultFactory, Func<byte[], string> hashFormatter)
        {
            _resultFactory = resultFactory;
            _hashHandler = new HashHandler(hashAlgorithm, hashFormatter);
        }


        public IResult<string> CalculateHash(string input)
        {
            string calculatedHash = _hashHandler.CalculateHash(input);

            return _resultFactory.Success(calculatedHash);
        }

        public IResult<string> CalculateHash(byte[] input)
        {
            string calculatedHash = _hashHandler.CalculateHash(input);

            return _resultFactory.Success(calculatedHash);
        }

        public IResult<string> CalculateHash(StreamReader input)
        {
            string calculatedHash = _hashHandler.CalculateHash(input);

            return _resultFactory.Success(calculatedHash);
        }

        public IResult<string> CalculateHash(SecureString input)
        {
            var calculatedHash = _hashHandler.CalculateHash(input);

            return _resultFactory.Success(calculatedHash);
        }

        public IResult HashValidate(string input, string hash)
        {
            bool isValid = _hashHandler.HashValidate(input, hash);

            return isValid
                ? _resultFactory.Success(isValid)
                : _resultFactory.Failure("Hashes do not match.");
        }

        public IResult HashValidate(byte[] input, string hash)
        {
           bool isValid = _hashHandler.HashValidate(input, hash);

           return isValid
                ? _resultFactory.Success(isValid)
                : _resultFactory.Failure("Hashes do not match.");
        }

        public IResult HashValidate(SecureString input, string hash)
        {
            bool isValid = _hashHandler.HashValidate(input, hash);

            return isValid
                ? _resultFactory.Success(isValid)
                : _resultFactory.Failure("Hashes do not match.");
        }

        public IResult HashValidate(StreamReader input, string hash)
        {
            bool isValid = _hashHandler.HashValidate(input, hash);

            return isValid
                ? _resultFactory.Success(isValid)
                : _resultFactory.Failure("Hashes do not match.");
        }

    }
}
