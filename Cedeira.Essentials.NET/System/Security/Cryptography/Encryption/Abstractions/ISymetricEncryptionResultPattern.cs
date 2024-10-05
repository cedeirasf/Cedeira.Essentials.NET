using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    public interface ISymetricEncryptionResultPattern
    {
        public string Encrypt(string input);
        public byte[] Encrypt(byte[] input);
        public SecureString Encrypt(SecureString input);
        public StreamReader Encrypt(StreamReader input);
        public string Decrypt(string input);
        public byte[] Decryptt(byte[] input);
        public SecureString Decrypt(SecureString input);
        public StreamReader Decrypt(StreamReader input);
    }
}
