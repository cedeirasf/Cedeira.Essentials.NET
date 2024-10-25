using Cedeira.Essentials.NET.Diagnostics.Invariants;
using Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption
{
    /// <summary>
    /// Provides methods for symmetric encryption and decryption of data using a specified symmetric algorithm.
    /// </summary>
    public class SymmetricEncryption : ISymmetricEncryption
    {
        /// <summary>
        /// Gets the symmetric algorithm used for encryption and decryption.
        /// </summary>
        private readonly SymmetricAlgorithm _symmetricAlgortihm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricEncryption"/> class with the specified symmetric algorithm.
        /// </summary>
        /// <param name="symetricAlgortihm">The symmetric algorithm used for encryption and decryption.</param>
        public SymmetricEncryption(SymmetricAlgorithm symetricAlgortihm)
        {
            _symmetricAlgortihm = symetricAlgortihm;
        }

        /// <summary>
        /// Encrypts the specified byte array.
        /// </summary>
        /// <param name="input">The byte array to encrypt.</param>
        /// <returns>The encrypted byte array.</returns>
        public byte[] Encrypt(byte[] input)
        {
            ValidateNull(input);

            return Encryption(input);
        }

        /// <summary>
        /// Decrypts the specified byte array.
        /// </summary>
        /// <param name="input">The byte array to decrypt.</param>
        /// <returns>The decrypted byte array.</returns>
        public byte[] Decrypt(byte[] input)
        {
            ValidateNull(input);

            return Decryption(input);
        }

        /// <summary>
        /// Encrypts the specified string.
        /// </summary>
        /// <param name="input">The string to encrypt.</param>
        /// <returns>The encrypted string represented in hexadecimal format.</returns>
        public string Encrypt(string input)
        {
            ValidateNull(input);

            byte[] plainBytes = Encoding.UTF8.GetBytes(input);

            return Convert.ToHexString(Encryption(plainBytes));
        }

        /// <summary>
        /// Decrypts the specified string represented in hexadecimal format.
        /// </summary>
        /// <param name="input">The hexadecimal string to decrypt.</param>
        /// <returns>The decrypted string.</returns>
        public string Decrypt(string input)
        {
            ValidateNull(input);

            byte[] plainBytes = Convert.FromHexString(input);

            return Encoding.UTF8.GetString(Decryption(plainBytes));
        }
        /// <summary>
        /// Encrypts the specified SecureString.
        /// </summary>
        /// <param name="input">The SecureString to encrypt.</param>
        /// <returns>The encrypted SecureString.</returns>
        public SecureString Encrypt(SecureString input)
        {
            ValidateNull(input);

            var encryptor = _symmetricAlgortihm.CreateEncryptor(_symmetricAlgortihm.Key, _symmetricAlgortihm.IV);

            return input.Encrypt(encryptor);

        }

        /// <summary>
        /// Decrypts the specified SecureString.
        /// </summary>
        /// <param name="input">The SecureString to decrypt.</param>
        /// <returns>The decrypted SecureString.</returns>
        public SecureString Decrypt(SecureString input)
        {
            ValidateNull(input);

            var decryptor = _symmetricAlgortihm.CreateDecryptor(_symmetricAlgortihm.Key, _symmetricAlgortihm.IV);

            return input.Decrypt(decryptor);
        }

        /// <summary>
        /// Encrypts the data from the specified StreamReader.
        /// </summary>
        /// <param name="input">The StreamReader containing the data to encrypt.</param>
        /// <returns>A StreamReader containing the encrypted data.</returns>
        public StreamReader Encrypt(StreamReader input)
        {
            ValidateNull(input);

            var encryptor = _symmetricAlgortihm.CreateEncryptor(_symmetricAlgortihm.Key, _symmetricAlgortihm.IV);

            var output = new MemoryStream();

            using (var cryptoStream = new CryptoStream(output, encryptor, CryptoStreamMode.Write, leaveOpen: true))
            {
                using (var writer = new StreamWriter(cryptoStream))
                {
                    input.BaseStream.Position = 0; 

                    writer.Write(input.ReadToEnd());

                    writer.Flush();
                }
            }
            output.Position = 0;

            return new StreamReader(output, leaveOpen: true);
        }

        /// <summary>
        /// Decrypts the data from the specified StreamReader.
        /// </summary>
        /// <param name="input">The StreamReader containing the data to decrypt.</param>
        /// <returns>A StreamReader containing the decrypted data.</returns>
        public StreamReader Decrypt(StreamReader input)
        {
            ValidateNull(input);

            var decryptor = _symmetricAlgortihm.CreateDecryptor(_symmetricAlgortihm.Key, _symmetricAlgortihm.IV);

            var output = new MemoryStream();

            using (var cryptoStream = new CryptoStream(input.BaseStream, decryptor, CryptoStreamMode.Read))
            {
                cryptoStream.CopyTo(output);
            }

            output.Position = 0;

            return new StreamReader(output);

        }

        /// <summary>
        /// Validates if the provided string input matches the decrypted version of the cipherInput.
        /// </summary>
        /// <param name="input">The original plain text input string to compare.</param>
        /// <param name="cipherInput">The encrypted input string to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted text matches the original input; otherwise, false</returns>
        public bool ValidateEncryption(string input, string cipherInput)
        {
            ValidateNull(input);
            ValidateNull(cipherInput);

            var decryptedText = Decrypt(cipherInput);

            return (input == decryptedText) ? true : false;
        }

        /// <summary>
        /// Validates if the provided byte array input matches the decrypted version of the cipherInput byte array.
        /// </summary>
        /// <param name="input">The original plain text input as a byte array to compare.</param>
        /// <param name="cipherInput">The encrypted byte array to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted byte array matches the original input; otherwise, false.</returns>
        public bool ValidateEncryption(byte[] input, byte[] cipherInput)
        {
            ValidateNull(input);
            ValidateNull(cipherInput);

            var decryptedText = Decrypt(cipherInput);

            return (input.SequenceEqual(decryptedText)) ? true : false;
        }

        /// <summary>
        /// Validates if the provided SecureString input matches the decrypted version of the cipherInput SecureString.
        /// </summary>
        /// <param name="input">The original plain text input as a SecureString to compare.</param>
        /// <param name="cipherInput">The encrypted SecureString to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted SecureString matches the original input; otherwise,false.</returns>
        public bool ValidateEncryption(SecureString input, SecureString cipherInput)
        {
            ValidateNull(input);
            ValidateNull(cipherInput);

            var decryptedText = Decrypt(cipherInput);

            return (input.ValidateEncryption(decryptedText)) ? true : false;
        }

        /// <summary>
        /// Validates if the provided StreamReader input matches the decrypted version of the cipherInput StreamReader.
        /// </summary>
        /// <param name="input">The original plain text input as a StreamReader to compare.</param>
        /// <param name="cipherInput">The encrypted StreamReader to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted StreamReader matches the original input; otherwise false.</returns>
        public bool ValidateEncryption(StreamReader input, StreamReader cipherInput)
        {
            ValidateNull(input);
            ValidateNull(cipherInput);

            bool result = true; 

            var decryptedText = Decrypt(cipherInput);

            input.BaseStream.Position = 0;
            decryptedText.BaseStream.Position = 0;

            input.DiscardBufferedData();
            decryptedText.DiscardBufferedData();

            int byteFromInput, byteFromDecrypted;

            while ((byteFromInput = input.BaseStream.ReadByte()) != -1 &&
                       (byteFromDecrypted = decryptedText.BaseStream.ReadByte()) != -1)
            {
                if (byteFromInput != byteFromDecrypted)
                    result = false;
            }

            if (input.BaseStream.ReadByte() != -1 || decryptedText.BaseStream.ReadByte() != -1)
                result = false;

            return result;
        }

        /// <summary>
        /// Validates if the provided string input matches the decrypted version of the cipherInput.
        /// Throws a CryptographicException if the validation fails.
        /// </summary>
        /// <param name="input">The original plain text input string to compare.</param>
        /// <param name="cipherInput">The encrypted input string to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted text matches the original input; otherwise, throws a CryptographicException.</returns>
        public void ThrowIfInvalidEncryption(string input, string cipherIput) 
        {
            if (!ValidateEncryption(input, cipherIput))
                throw new CryptographicException("Encryption validation failed: The decrypted text does not match the original input.");
        }

        /// <summary>
        /// Validates if the provided byte array input matches the decrypted version of the cipherInput byte array.
        /// Throws a CryptographicException if the validation fails.
        /// </summary>
        /// <param name="input">The original plain text input as a byte array to compare.</param>
        /// <param name="cipherInput">The encrypted byte array to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted byte array matches the original input; otherwise, throws a CryptographicException.</returns>
        public void ThrowIfInvalidEncryption(byte[] input, byte[] cipherIput)
        {
            if (!ValidateEncryption(input, cipherIput))
                throw new CryptographicException("Encryption validation failed: The decrypted text does not match the original input.");
        }

        /// <summary>
        /// Validates if the provided SecureString input matches the decrypted version of the cipherInput SecureString.
        /// </summary>
        /// <param name="input">The original plain text input as a SecureString to compare.</param>
        /// <param name="cipherInput">The encrypted SecureString to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted SecureString matches the original input; otherwise throws a CryptographicException.</returns>
        public void ThrowIfInvalidEncryption(SecureString input, SecureString cipherIput)
        {
            if (!ValidateEncryption(input, cipherIput))
                throw new CryptographicException("Encryption validation failed: The decrypted text does not match the original input.");
        }

        /// <summary>
        /// Validates if the provided StreamReader input matches the decrypted version of the cipherInput StreamReader.
        /// Throws a CryptographicException if the validation fails.
        /// </summary>
        /// <param name="input">The original plain text input as a StreamReader to compare.</param>
        /// <param name="cipherInput">The encrypted StreamReader to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted StreamReader matches the original input; otherwise throws a CryptographicException.</returns>
        public void ThrowIfInvalidEncryption(StreamReader input, StreamReader cipherIput)
        {
            if (!ValidateEncryption(input, cipherIput))
                throw new CryptographicException("Encryption validation failed: The decrypted text does not match the original input.");
        }


        /// <summary>
        /// Encrypts the specified byte array using the symmetric algorithm.
        /// </summary>
        /// <param name="input">The byte array to encrypt.</param>
        /// <returns>The encrypted byte array.</returns>
        private byte[] Encryption(byte[] input)
        {
            var decryptor = _symmetricAlgortihm.CreateEncryptor(_symmetricAlgortihm.Key, _symmetricAlgortihm.IV);

            using (var inputMemoryStream = new MemoryStream(input))
            using (var cryptoStream = new CryptoStream(inputMemoryStream, decryptor, CryptoStreamMode.Read))
            using (var outputMemoryStream = new MemoryStream())
            {
                cryptoStream.CopyTo(outputMemoryStream);

                return outputMemoryStream.ToArray();
            }
        }

        /// <summary>
        /// Decrypts the specified byte array using the symmetric algorithm.
        /// </summary>
        /// <param name="input">The byte array to decrypt.</param>
        /// <returns>The decrypted byte array.</returns>
        private byte[] Decryption(byte[] input)
        {
            var encryptor = _symmetricAlgortihm.CreateDecryptor(_symmetricAlgortihm.Key, _symmetricAlgortihm.IV);

            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(input, 0, input.Length);

                cryptoStream.FlushFinalBlock();

                return memoryStream.ToArray();
            }
        }


        /// <summary>
        /// Validates that the specified input is not null.
        /// </summary>
        /// <typeparam name="T">The type of the input.</typeparam>
        /// <param name="input">The input to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when the input is null.</exception>
        private void ValidateNull<T>(T input)
        {
            Invariants.For(input).IsNotNull($"{nameof(input)} cannot be null.");
        }
    }
}

