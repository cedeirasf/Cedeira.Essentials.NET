using Cedeira.Essentials.NET.Diagnostics.Invariants;
using Cedeira.Essentials.NET.Extensions.System.Security.Cryptografy.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption
{
    public class SymetricEncryption : ISymetricEncryption
    {
        private readonly SymmetricAlgorithm _symetricAlgortihm;

        public SymetricEncryption(SymmetricAlgorithm symetricAlgortihm)
        {
            _symetricAlgortihm = symetricAlgortihm;
        }

        public byte[] Encrypt(byte[] input)
        {
            ValidateNull(input);

            return Encryption(input);
        }
        public byte[] Decrypt(byte[] input)
        {
            ValidateNull(input);

            return Decryption(input);
        }
        public string Encrypt(string input)
        {
            ValidateNull(input);

            byte[] plainBytes = Encoding.UTF8.GetBytes(input);

            return Convert.ToHexString(Encrypt(plainBytes));
        }
        public string Decrypt(string input)
        {
            ValidateNull(input);

            byte[] plainBytes = Convert.FromHexString(input);

            return Encoding.UTF8.GetString(Decryption(plainBytes));
        }
        public SecureString Encrypt(SecureString input)
        {
            ValidateNull(input);

            byte[] plainBytes = input.SecureStringToBytes();

            byte[] encryptedBytes = Encryption(plainBytes);

            return encryptedBytes.BytesToSecureString();
        }
        public SecureString Decrypt(SecureString input)
        {
            ValidateNull(input);

            byte[] plainBytes = input.SecureStringToBytes();

            byte[] encryptedBytes = Decryption(plainBytes);

            return encryptedBytes.BytesToSecureString();
        }
        public StreamReader Encrypt(StreamReader input)
        {
            ValidateNull(input);

            var memoryStream = new MemoryStream();

            input.BaseStream.CopyTo(memoryStream);

            return new StreamReader(new MemoryStream(Encrypt(memoryStream.ToArray())));
        }
        public StreamReader Decrypt(StreamReader input)
        {
            ValidateNull(input);

            var memoryStream = new MemoryStream();

            input.BaseStream.CopyTo(memoryStream);

            return new StreamReader(new MemoryStream(Decryption(memoryStream.ToArray())));
        }
        private byte[] Encryption(byte[] input)
        {
            var encryptor = _symetricAlgortihm.CreateEncryptor(_symetricAlgortihm.Key, _symetricAlgortihm.IV);

            var memoryStream = new MemoryStream();

            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(input, 0, input.Length);

            cryptoStream.FlushFinalBlock();

            return memoryStream.ToArray();
        }
        private byte[] Decryption(byte[] input)
        {
            var decryptor = _symetricAlgortihm.CreateDecryptor(_symetricAlgortihm.Key, _symetricAlgortihm.IV);

            var cryptoStream = new CryptoStream(new MemoryStream(input), decryptor, CryptoStreamMode.Read);

            var outputMemoryStream = new MemoryStream();

            cryptoStream.CopyTo(outputMemoryStream);

            return outputMemoryStream.ToArray();
        }
        private void ValidateNull<T>(T input)
        {
            Invariants.For(input).IsNotNull($"{nameof(input)} cannot be null.");
        }

    }
}
