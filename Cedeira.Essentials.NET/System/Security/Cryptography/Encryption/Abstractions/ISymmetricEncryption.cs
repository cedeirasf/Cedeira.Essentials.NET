using System.Security;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    public interface ISymmetricEncryption
    {
        public string Encrypt(string input);
        public string Decrypt(string input);
        public byte[] Encrypt(byte[] input);
        public byte[] Decrypt(byte[] input);
        public SecureString Encrypt(SecureString input);
        public StreamReader Encrypt(StreamReader input);
        public SecureString Decrypt(SecureString input);
        public StreamReader Decrypt(StreamReader input);

    }
}
