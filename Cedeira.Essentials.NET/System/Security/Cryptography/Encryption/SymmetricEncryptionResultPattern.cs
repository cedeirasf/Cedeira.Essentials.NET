using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using System.Security;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption
{
    /// <summary>
    /// Provides methods to encrypt and decrypt data using symmetric encryption,
    /// while encapsulating the result in a standardized result pattern.
    /// </summary>
    /// <remarks>
    /// This class uses an instance of <see cref="ISymmetricEncryption"/> to perform the encryption
    /// and decryption operations and <see cref="IResultFactory"/> to generate the result objects.
    /// </remarks>
    public class SymmetricEncryptionResultPattern : ISymmetricEncryptionResultPattern
    {
        private readonly IResultFactory _resultFactory;
        private readonly ISymmetricEncryption _symmetricEncryption;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricEncryptionResultPattern"/> class.
        /// </summary>
        /// <param name="symetricAlgorithm">The symmetric algorithm to be used for encryption and decryption.</param>
        /// <param name="resultFactory">The factory used to create result objects.</param>
        public SymmetricEncryptionResultPattern(SymmetricAlgorithm symetricAlgorithm, IResultFactory resultFactory)
        {
            _symmetricEncryption = new SymmetricEncryption(symetricAlgorithm);
            _resultFactory = resultFactory;
        }

        /// <summary>
        /// Encrypts the specified byte array and returns the result in an <see cref="IResult{T}"/> object.
        /// </summary>
        /// <param name="input">The byte array to encrypt.</param>
        /// <returns>An <see cref="IResult{byte[]}"/> containing the encrypted data or an error message.</returns>
        public IResult<byte[]> Encrypt(byte[] input)
        {
            IResult<byte[]> result;

            try
            {
                byte[] response = _symmetricEncryption.Encrypt(input);

                result = _resultFactory.Success(response);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<byte[]>(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Decrypts the specified byte array and returns the result in an <see cref="IResult{T}"/> object.
        /// </summary>
        /// <param name="input">The byte array to decrypt.</param>
        /// <returns>An <see cref="IResult{byte[]}"/> containing the decrypted data or an error message.</returns>
        public IResult<byte[]> Decrypt(byte[] input)
        {
            IResult<byte[]> result;

            try
            {
                byte[] response = _symmetricEncryption.Decrypt(input);

                result = _resultFactory.Success(response);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<byte[]>(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Encrypts the specified string and returns the result in an <see cref="IResult{T}"/> object.
        /// </summary>
        /// <param name="input">The string to encrypt.</param>
        /// <returns>An <see cref="IResult{string}"/> containing the encrypted data or an error message.</returns>
        public IResult<string> Encrypt(string input)
        {
            IResult<string> result;

            try
            {
                string response = _symmetricEncryption.Encrypt(input);

                result = _resultFactory.Success(response);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<string>(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Decrypts the specified string and returns the result in an <see cref="IResult{T}"/> object.
        /// </summary>
        /// <param name="input">The string to decrypt.</param>
        /// <returns>An <see cref="IResult{string}"/> containing the decrypted data or an error message.</returns>
        public IResult<string> Decrypt(string input)
        {
            IResult<string> result;

            try
            {
               string response = _symmetricEncryption.Decrypt(input);

               result = _resultFactory.Success(response);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<string>(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Encrypts the specified <see cref="SecureString"/> and returns the result in an <see cref="IResult{T}"/> object.
        /// </summary>
        /// <param name="input">The <see cref="SecureString"/> to encrypt.</param>
        /// <returns>An <see cref="IResult{SecureString}"/> containing the encrypted data or an error message.</returns>
        public IResult<SecureString> Encrypt(SecureString input)
        {
            IResult<SecureString> result;

            try
            {
                SecureString response = _symmetricEncryption.Encrypt(input);

                result = _resultFactory.Success(response);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<SecureString>(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Decrypts the specified <see cref="SecureString"/> and returns the result in an <see cref="IResult{T}"/> object.
        /// </summary>
        /// <param name="input">The <see cref="SecureString"/> to decrypt.</param>
        /// <returns>An <see cref="IResult{SecureString}"/> containing the decrypted data or an error message.</returns>
        public IResult<SecureString> Decrypt(SecureString input)
        {
            IResult<SecureString> result;

            try
            {
                SecureString response = _symmetricEncryption.Decrypt(input);

                result = _resultFactory.Success(response);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<SecureString>(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Encrypts the specified <see cref="StreamReader"/> and returns the result in an <see cref="IResult{T}"/> object.
        /// </summary>
        /// <param name="input">The <see cref="StreamReader"/> to encrypt.</param>
        /// <returns>An <see cref="IResult{StreamReader}"/> containing the encrypted data or an error message.</returns>
        public IResult<StreamReader> Encrypt(StreamReader input)
        {
            IResult<StreamReader> result;

            try
            {
                StreamReader response = _symmetricEncryption.Encrypt(input);

                result = _resultFactory.Success(response);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<StreamReader>(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Decrypts the specified <see cref="StreamReader"/> and returns the result in an <see cref="IResult{T}"/> object.
        /// </summary>
        /// <param name="input">The <see cref="StreamReader"/> to decrypt.</param>
        /// <returns>An <see cref="IResult{StreamReader}"/> containing the decrypted data or an error message.</returns>
        public IResult<StreamReader> Decrypt(StreamReader input)
        {
            IResult<StreamReader> result;

            try
            {
                StreamReader response = _symmetricEncryption.Decrypt(input);

                result = _resultFactory.Success(response);
            }
            catch (ArgumentException ex)
            {
                result = _resultFactory.Failure<StreamReader>(ex.Message);
            }

            return result;
        }
    }
}
