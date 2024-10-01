using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using System.Security;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption
{
    public class SymetricEncryption : ISymmetricEncryption
    {
        private readonly SymmetricAlgorithm _symetricAlgortihm;

        public SymetricEncryption(SymmetricAlgorithm symetricAlgortihm)
        {
             _symetricAlgortihm = symetricAlgortihm;
        }

        public string Encrypt(string input)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(byte[] input)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(SecureString input)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(StreamReader input)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string input)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(SecureString input)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(StreamReader input)
        {
            throw new NotImplementedException();
        }

        public string Decryptt(byte[] input)
        {
            throw new NotImplementedException();
        }

    }
}
