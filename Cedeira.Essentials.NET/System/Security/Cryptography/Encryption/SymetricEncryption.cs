using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using System.Reflection.PortableExecutable;
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

        public string Encrypt(byte[] input)
        {
            return Encryption(input);
        }
        public string Encrypt(string input)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(input);

            return Encrypt(plainBytes);
        }
        public string Encrypt(SecureString input)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(input);
                int length = input.Length * 2;
                byte[] plainBytes = new byte[length];

                Marshal.Copy(unmanagedString, plainBytes, 0, length);

                return Encrypt(plainBytes);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        public string Encrypt(StreamReader input)
        {
            byte[] plainBytes;
            var memoryStream = new MemoryStream();
            input.BaseStream.CopyTo(memoryStream);
            plainBytes = memoryStream.ToArray();

            return Encrypt(plainBytes);
        }

        public string Decryptt(byte[] input)
        {
            return Decryption(input);
        }
        public string Decrypt(string input)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(input);

            return Decryption(plainBytes);
        }
        public string Decrypt(SecureString input)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(input);
                int length = input.Length * 2;
                byte[] plainBytes = new byte[length];

                Marshal.Copy(unmanagedString, plainBytes, 0, length);

                return Decryption(plainBytes);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        public string Decrypt(StreamReader input)
        {
            byte[] plainBytes;
            var memoryStream = new MemoryStream();
            input.BaseStream.CopyTo(memoryStream);
            plainBytes = memoryStream.ToArray();

            return Decryption(plainBytes);
        }

        private string Encryption(byte[] input)
        {
            var encryptor = _symetricAlgortihm.CreateEncryptor(_symetricAlgortihm.Key, _symetricAlgortihm.Key);

            var memoryStream = new MemoryStream();

            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(input, 0, input.Length);

            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(memoryStream.ToArray());
        }
        private string Decryption(byte[] input)
        {
            var decryptor = _symetricAlgortihm.CreateDecryptor(_symetricAlgortihm.Key, _symetricAlgortihm.IV);

            var cryptoStream = new CryptoStream(new MemoryStream(input), decryptor, CryptoStreamMode.Read);

            var reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }

    }
}
