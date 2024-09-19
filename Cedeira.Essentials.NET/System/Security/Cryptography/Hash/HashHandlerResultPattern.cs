using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashHandlerResultPattern : IHashHandlerResultPattern
    {
        private readonly IResultFactory _resultFactory;
        private readonly IHashHandler _hashHandler;

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
            IResult<string> result;

            try
            {
                string calculatedHash = _hashHandler.CalculateHash(input);
                result = _resultFactory.Success(calculatedHash);
            }
            catch (ArgumentException ex)
            {
                result =_resultFactory.Failure<string>(ex.Message);
            }

            return result;
        }

        public IResult<string> CalculateHash(byte[] input)
        {
            IResult<string> result;

            try
            {
                string calculatedHash = _hashHandler.CalculateHash(input);
                result = _resultFactory.Success(calculatedHash);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<string>(ex.Message);
            }

            return result;
        }

        public IResult<string> CalculateHash(StreamReader input)
        {
            IResult<string> result;

            try
            {
                string calculatedHash = _hashHandler.CalculateHash(input);
                result = _resultFactory.Success(calculatedHash);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<string>(ex.Message);
            }

            return result;
        }

        public IResult<string> CalculateHash(SecureString input)
        {
            IResult<string> result;

            try
            {
                string calculatedHash = _hashHandler.CalculateHash(input);
                result = _resultFactory.Success(calculatedHash);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<string>(ex.Message);
            }

            return result;
        }

        public IResult HashValidate(string input, string hash)
        {
            IResult result;

            try
            {
                bool isValid = _hashHandler.HashValidate(input, hash);

                result = isValid
                    ? _resultFactory.Success(isValid)
                    : _resultFactory.Failure("Hashes do not match.");
            }
            catch (ArgumentException ex) 
            {
                result = _resultFactory.Failure(ex.Message);
            }

            return result; 
        }

        public IResult HashValidate(byte[] input, string hash)
        {
            IResult result;

            try
            {
                bool isValid = _hashHandler.HashValidate(input, hash);

                result = isValid
                    ? _resultFactory.Success(isValid)
                    : _resultFactory.Failure("Hashes do not match.");
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure(ex.Message);
            }

            return result;
        }

        public IResult HashValidate(SecureString input, string hash)
        {
            IResult result;

            try
            {
                bool isValid = _hashHandler.HashValidate(input, hash);

                result = isValid
                    ? _resultFactory.Success(isValid)
                    : _resultFactory.Failure("Hashes do not match.");
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure(ex.Message);
            }

            return result;
        }

        public IResult HashValidate(StreamReader input, string hash)
        {
            IResult result;

            try
            {
                bool isValid = _hashHandler.HashValidate(input, hash);

                result = isValid
                    ? _resultFactory.Success(isValid)
                    : _resultFactory.Failure("Hashes do not match.");
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure(ex.Message);
            }

            return result;
        }

    }
}
