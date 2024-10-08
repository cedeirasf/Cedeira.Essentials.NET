using Cedeira.Essentials.NET.System.ResultPattern;
using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    public interface ISymetricEncryptionResultPattern
    {
        public IResult<byte[]> Encrypt(byte[] input);
        public IResult<byte[]> Decryptt(byte[] input);
        public IResult<string> Encrypt(string input);
        public IResult<string> Decrypt(string input);
        public IResult<SecureString> Encrypt(SecureString input);
        public IResult<StreamReader> Encrypt(StreamReader input);
        public IResult<SecureString> Decrypt(SecureString input);
        public IResult<StreamReader> Decrypt(StreamReader input);
    }
}
