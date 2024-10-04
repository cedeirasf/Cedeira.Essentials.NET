using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    /// <summary>
    /// Represents a handler for computing and validating hashes using a specified hash algorithm.Always returning results wrapped in an IResult structure.
    /// </summary>
    public class HashHandlerResultPattern : IHashHandlerResultPattern
    {
        /// <summary>
        /// The factory used to create result objects.
        /// </summary>
        private readonly IResultFactory _resultFactory;

        /// <summary>
        /// The hash handler used for computing and validating hashes.
        /// </summary>
        private readonly IHashHandler _hashHandler;

        /// <summary>
        /// Initializes a new instance of the HashHandlerResultPattern class with the specified hash algorithm and result factory.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm to use.</param>
        /// <param name="resultFactory">The factory to create result objects.</param>
        public HashHandlerResultPattern(HashAlgorithm hashAlgorithm, IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
            _hashHandler = new HashHandler(hashAlgorithm);
        }

        /// <summary>
        /// Initializes a new instance of the HashHandlerResultPattern class with the specified hash algorithm, result factory, and hash formatter.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm to use.</param>
        /// <param name="hashFormatter">The function to format the hash bytes into a string.</param>
        /// <param name="resultFactory">The factory to create result objects.</param>
        public HashHandlerResultPattern(HashAlgorithm hashAlgorithm, Func<byte[], string> hashFormatter,IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
            _hashHandler = new HashHandler(hashAlgorithm, hashFormatter);
        }

        /// <summary>
        /// Calculates the hash of the specified string input and returns the result.
        /// </summary>
        /// <param name="input">The input string to hash.</param>
        /// <returns>The result containing the hash of the input string.</returns>
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

        /// <summary>
        /// Calculates the hash of the specified byte array input and returns the result.
        /// </summary>
        /// <param name="input">The input byte array to hash.</param>
        /// <returns>The result containing the hash of the input byte array.</returns>
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

        /// <summary>
        /// Calculates the hash of the specified StreamReader input and returns the result.
        /// </summary>
        /// <param name="input">The input StreamReader to hash.</param>
        /// <returns>The result containing the hash of the input StreamReader.</returns>
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

        /// <summary>
        /// Calculates the hash of the specified SecureString input and returns the result.
        /// </summary>
        /// <param name="input">The input SecureString to hash.</param>
        /// <returns>The result containing the hash of the input SecureString.</returns>
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

        /// <summary>
        /// Validates the hash of the specified string input against the provided hash and returns the result.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        /// <returns>The result indicating whether the hash matches.</returns>
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

        /// <summary>
        /// Validates the hash of the specified byte array input against the provided hash and returns the result.
        /// </summary>
        /// <param name="input">The input byte array to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        /// <returns>The result indicating whether the hash matches.</returns>
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

        /// <summary>
        /// Validates the hash of the specified SecureString input against the provided hash and returns the result.
        /// </summary>
        /// <param name="input">The input SecureString to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        /// <returns>The result indicating whether the hash matches.</returns>
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

        /// <summary>
        /// Validates the hash of the specified StreamReader input against the provided hash and returns the result.
        /// </summary>
        /// <param name="input">The input StreamReader to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        /// <returns>The result indicating whether the hash matches.</returns>
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
