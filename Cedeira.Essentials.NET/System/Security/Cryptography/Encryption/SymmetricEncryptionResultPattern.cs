using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption
{
    public class SymmetricEncryptionResultPattern : ISymmetricEncryptionResultPattern
    {
        private readonly IResultFactory _resultFactory;
        private readonly ISymmetricEncryption _symmetricEncryption;

        public SymmetricEncryptionResultPattern(SymmetricAlgorithm symetricAlgorithm, IResultFactory resultFactory)
        {
            _symmetricEncryption = new SymmetricEncryption(symetricAlgorithm);
            _resultFactory = resultFactory;
        }

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
        public IResult<byte[]> Decryptt(byte[] input)
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
