using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Encryption
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
                    byte[] inputBytes = Encoding.Unicode.GetBytes(inputPtrString);  // Usamos Unicode

                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();  // Asegura que todos los datos se escriban
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
                using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Read))
                {
                    using (MemoryStream decryptedStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead;

                        while ((bytesRead = cs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            decryptedStream.Write(buffer, 0, bytesRead);
                        }

                        byte[] decryptedBytes = decryptedStream.ToArray();
                        char[] decryptedChars = Encoding.Unicode.GetChars(decryptedBytes);

                        SecureString decryptedSecureString = new SecureString();

                        foreach (char c in decryptedChars)
                        {
                            decryptedSecureString.AppendChar(c);
                        }

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

        // <summary>
        /// Validates whether the provided <see cref="SecureString"/> input matches the decrypted value.
        /// This method compares the content of two <see cref="SecureString"/> instances after converting them to regular strings.
        /// </summary>
        /// <param name="input">The <see cref="SecureString"/> input to validate.</param>
        /// <param name="decryptedValue">The <see cref="SecureString"/> representing the expected decrypted value.</param>
        /// <returns><c>true</c> if the input matches the decrypted value; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="input"/> or <paramref name="decryptedValue"/> is null.</exception>
        public static bool ValidateEncryption(this SecureString input, SecureString decryptedValue)
        {
            bool result = true;
            nint inputPtr = IntPtr.Zero;
            nint decryptedTextPtr = IntPtr.Zero;

            try
            {
                inputPtr = Marshal.SecureStringToGlobalAllocUnicode(input);
                decryptedTextPtr = Marshal.SecureStringToGlobalAllocUnicode(decryptedValue);

                string inputString = Marshal.PtrToStringUni(inputPtr);
                string decryptedTextString = Marshal.PtrToStringUni(decryptedTextPtr);

                if (inputString != decryptedTextString)
                    result = false;
            }
            finally
            {
                if (inputPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(inputPtr);  
                }

                if (decryptedTextPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(decryptedTextPtr);  
                }
            }

            return result;
        }

        /// <summary>
        /// Converts a <see cref="SecureString"/> to a regular <see cref="string"/>.
        /// This method safely allocates and frees memory for the conversion.
        /// </summary>
        /// <param name="secureString">The <see cref="SecureString"/> to convert.</param>
        /// <returns>A <see cref="string"/> representation of the <see cref="SecureString"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="secureString"/> is null.</exception>
        public static string SecureStringToString(this SecureString secureString)
        {
            if (secureString == null)
                throw new ArgumentNullException(nameof(secureString));

            IntPtr ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            try
            {
                return Marshal.PtrToStringUni(ptr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }

    }
}


