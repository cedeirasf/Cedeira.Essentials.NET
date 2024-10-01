using System.Security;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    public interface ISymmetricEncryption
    {
        public string Encrypt(string input);
        public string Encrypt(byte[] input);
        public string Encrypt(SecureString input);
        public string Encrypt(StreamReader input);
        public string Decrypt(string input);
        public string Decryptt(byte[] input);
        public string Decrypt(SecureString input);
        public string Decrypt(StreamReader input);

    }
}
