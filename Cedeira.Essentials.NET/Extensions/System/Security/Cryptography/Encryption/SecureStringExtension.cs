namespace Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Encryption
{
    public static class SecureStringExtension
    {
        /// <summary>
        /// Encrypts the provided SecureString using the specified ICryptoTransform.
        /// This method ensures secure handling of sensitive data by immediately clearing memory after use.
        /// </summary>
        /// <param name="input">The SecureString to encrypt. Must not be null.</param>
        /// <param name="cryptoTransform">The ICryptoTransform used for encryption. Must not be null.</param>
        /// <returns>A new SecureString containing the encrypted data in a Base64 format.</returns>
        /// <exception cref="ArgumentNullException">Thrown when input or cryptoTransform is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when SecureString conversion fails.</exception>
        /// <exception cref="CryptographicException">Thrown when encryption process fails.</exception>
        public static SecureString Encrypt(this SecureString input, ICryptoTransform cryptoTransform)
        {
            if (input == null)
                throw new ArgumentNullException("input", "The parameter SecureString cannot be null");
            if (cryptoTransform == null)
                throw new ArgumentNullException("cryptoTransform", "The cryptoTransform parameter cannot be null");

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);

            IntPtr inputPtr = Marshal.SecureStringToGlobalAllocUnicode(input);
            byte[] inputBytes = null;
            byte[] encryptedBytes = null;
            char[] base64Chars = null;

            try
            {
                string inputPtrString = Marshal.PtrToStringUni(inputPtr);
                Marshal.ZeroFreeGlobalAllocUnicode(inputPtr);
                inputPtr = IntPtr.Zero;

                if (inputPtrString is null)
                    throw new InvalidOperationException("Failed to convert SecureString to Unicode string");

                inputBytes = Encoding.Unicode.GetBytes(inputPtrString);
                cs.Write(inputBytes, 0, inputBytes.Length);
                cs.FlushFinalBlock();

                encryptedBytes = ms.ToArray();

                base64Chars = Convert.ToBase64String(encryptedBytes).ToCharArray();

                var secureEncryptedText = new SecureString();
                for (int i = 0; i < base64Chars.Length; i++)
                {
                    secureEncryptedText.AppendChar(base64Chars[i]);
                }
                secureEncryptedText.MakeReadOnly();

                return secureEncryptedText;
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Encryption failed", ex);
            }
            finally
            {
                if (inputBytes != null)
                {
                    Array.Clear(inputBytes, 0, inputBytes.Length);
                }

                if (encryptedBytes != null)
                {
                    Array.Clear(encryptedBytes, 0, encryptedBytes.Length);
                }

                if (base64Chars != null)
                {
                    Array.Clear(base64Chars, 0, base64Chars.Length);
                }

                if (inputPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(inputPtr);
                }
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


