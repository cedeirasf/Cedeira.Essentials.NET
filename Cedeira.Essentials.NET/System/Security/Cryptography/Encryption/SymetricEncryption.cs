using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption
{
    public class SymetricEncryption : ISymmetricEncryption
    {
        private readonly SymmetricAlgorithm _symetricAlgortihm;

        public SymetricEncryption(SymmetricAlgorithm symetricAlgortihm)
        {
            _symetricAlgortihm = symetricAlgortihm;
        }

        public byte[] Encrypt(byte[] input)
        {
            return Encryption(input);
        }
        public string Encrypt(string input)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToHexString(Encrypt(plainBytes));
        }
        public SecureString Encrypt(SecureString input)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(input);
                int length = input.Length * 2;
                byte[] plainBytes = new byte[length];

                Marshal.Copy(unmanagedString, plainBytes, 0, length);

                var encryptedBytes = Encryption(plainBytes);

                var secureEncryptedString = new SecureString();

                foreach (byte b in encryptedBytes)
                    secureEncryptedString.AppendChar((char)b);

                secureEncryptedString.MakeReadOnly();
                return secureEncryptedString;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        public StreamReader Encrypt(StreamReader input)
        {
            var memoryStream = new MemoryStream();
            input.BaseStream.CopyTo(memoryStream);

            return new StreamReader(new MemoryStream(Encrypt(memoryStream.ToArray())));
        }
        public byte[] Decryptt(byte[] input)
        {
            return Decryption(input);
        }
        public string Decrypt(string input)
        {
            byte[] plainBytes = Convert.FromHexString(input); 
            return Encoding.UTF8.GetString(Decryption(plainBytes));
        }
        public SecureString Decrypt(SecureString input)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(input);
                int length = input.Length * 2;
                byte[] plainBytes = new byte[length];

                Marshal.Copy(unmanagedString, plainBytes, 0, length);

                var encryptedBytes = Decryption(plainBytes);
                var secureDecryptedString = new SecureString();

                foreach (byte b in encryptedBytes)
                    secureDecryptedString.AppendChar((char)b);

                secureDecryptedString.MakeReadOnly();
                return secureDecryptedString;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        public StreamReader Decrypt(StreamReader input)
        {
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
    }
}
