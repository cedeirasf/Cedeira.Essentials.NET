using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.Extensions.System.Security.Cryptografhy.Encryption
{
    public static class SecureStringExtension
    {
        /// <summary>
        /// Encrypts the provided SecureString using the specified ICryptoTransform.
        /// </summary>
        /// <param name="input">The SecureString to encrypt.</param>
        /// <param name="cryptoTransform">The ICryptoTransform used for encryption.</param>
        /// <returns>A new SecureString containing the encrypted data.</returns>
        public static SecureString Encrypt(this SecureString input, ICryptoTransform cryptoTransform)
        {
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                IntPtr inputPtr = Marshal.SecureStringToGlobalAllocUnicode(input);

                try
                {
                    string inputPtrString = Marshal.PtrToStringUni(inputPtr);
                    byte[] inputBytes = Encoding.UTF8.GetBytes(inputPtrString);

                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(inputPtr);
                }

                byte[] encryptedBytes = ms.ToArray();

                SecureString secureEncryptedText = new SecureString();

                foreach (char c in Convert.ToBase64String(encryptedBytes))
                    secureEncryptedText.AppendChar(c);

                secureEncryptedText.MakeReadOnly();
                Array.Clear(encryptedBytes, 0, encryptedBytes.Length);

                return secureEncryptedText;
            }
        }

        /// <summary>
        /// Decrypts the provided SecureString using the specified ICryptoTransform.
        /// </summary>
        /// <param name="input">The SecureString to decrypt.</param>
        /// <param name="cryptoTransform">The ICryptoTransform used for decryption.</param>
        /// <returns>A new SecureString containing the decrypted data.</returns>
        public static SecureString Decrypt(this SecureString input, ICryptoTransform cryptoTransform)
        {
            IntPtr cipherPtr = Marshal.SecureStringToGlobalAllocUnicode(input);

            try
            {
                byte[] cipherBytes = Convert.FromBase64String(Marshal.PtrToStringUni(cipherPtr));

                using (MemoryStream ms = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Read))
                    {
                        byte[] decryptedBytes = new byte[cipherBytes.Length];

                        int decryptedByteCount = cs.Read(decryptedBytes, 0, decryptedBytes.Length);

                        char[] decryptedChars = Encoding.UTF8.GetChars(decryptedBytes, 0, decryptedByteCount);

                        SecureString decryptedSecureString = new SecureString();

                        foreach (char c in decryptedChars)
                            decryptedSecureString.AppendChar(c);

                        decryptedSecureString.MakeReadOnly();

                        Array.Clear(decryptedBytes, 0, decryptedBytes.Length);

                        return decryptedSecureString;
                    }
                }
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(cipherPtr);
            }
        }
    }
}


