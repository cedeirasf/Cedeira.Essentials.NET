using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    /// <summary>
    /// Interface for symmetric encryption operations.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for encrypting and decrypting data using symmetric encryption.
    /// Implementations of this interface should support various input types, including <see cref="string"/>,
    /// <see cref="byte[]"/>, <see cref="SecureString"/>, and <see cref="StreamReader"/>.
    /// </remarks>
    public interface ISymmetricEncryption
    {
        /// <summary>
        /// Encrypts the specified string input.
        /// </summary>
        /// <param name="input">The string to encrypt.</param>
        /// <returns>The encrypted string.</returns>
        public string Encrypt(string input);

        /// <summary>
        /// Decrypts the specified string input.
        /// </summary>
        /// <param name="input">The encrypted string to decrypt.</param>
        /// <returns>The decrypted string.</returns>
        public string Decrypt(string input);

        /// <summary>
        /// Encrypts the specified byte array input.
        /// </summary>
        /// <param name="input">The byte array to encrypt.</param>
        /// <returns>The encrypted byte array.</returns>
        public byte[] Encrypt(byte[] input);

        /// <summary>
        /// Decrypts the specified byte array input.
        /// </summary>
        /// <param name="input">The encrypted byte array to decrypt.</param>
        /// <returns>The decrypted byte array.</returns>
        public byte[] Decrypt(byte[] input);

        /// <summary>
        /// Encrypts the specified <see cref="SecureString"/> input.
        /// </summary>
        /// <param name="input">The <see cref="SecureString"/> to encrypt.</param>
        /// <returns>The encrypted <see cref="SecureString"/>.</returns>
        public SecureString Encrypt(SecureString input);

        /// <summary>
        /// Decrypts the specified <see cref="SecureString"/> input.
        /// </summary>
        /// <param name="input">The encrypted <see cref="SecureString"/> to decrypt.</param>
        /// <returns>The decrypted <see cref="SecureString"/>.</returns>
        public SecureString Decrypt(SecureString input);

        /// <summary>
        /// Encrypts the specified <see cref="StreamReader"/> input.
        /// </summary>
        /// <param name="input">The <see cref="StreamReader"/> to encrypt.</param>
        /// <returns>The encrypted <see cref="StreamReader"/>.</returns>
        public StreamReader Encrypt(StreamReader input);

        /// <summary>
        /// Decrypts the specified <see cref="StreamReader"/> input.
        /// </summary>
        /// <param name="input">The encrypted <see cref="StreamReader"/> to decrypt.</param>
        /// <returns>The decrypted <see cref="StreamReader"/>.</returns>
        public StreamReader Decrypt(StreamReader input);

        /// <summary>
        /// Validates if the provided string input matches the decrypted version of the cipherInput.
        /// </summary>
        /// <param name="input">The original plain text input string to compare.</param>
        /// <param name="cipherInput">The encrypted input string to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted text matches the original input; otherwise, throws an exception.</returns>
        bool ValidateEncryption(string input, string cipherInput);

        /// <summary>
        /// Validates if the provided byte array input matches the decrypted version of the cipherInput byte array.
        /// </summary>
        /// <param name="input">The original plain text input as a byte array to compare.</param>
        /// <param name="cipherInput">The encrypted byte array to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted byte array matches the original input; otherwise, throws an exception.</returns>
        bool ValidateEncryption(byte[] input, byte[] cipherInput);

        /// <summary>
        /// Validates if the provided SecureString input matches the decrypted version of the cipherInput SecureString.
        /// </summary>
        /// <param name="input">The original plain text input as a SecureString to compare.</param>
        /// <param name="cipherInput">The encrypted SecureString to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted SecureString matches the original input; otherwise, throws an exception.</returns>
        bool ValidateEncryption(SecureString input, SecureString cipherInput);

        /// <summary>
        /// Validates if the provided StreamReader input matches the decrypted version of the cipherInput StreamReader.
        /// </summary>
        /// <param name="input">The original plain text input as a StreamReader to compare.</param>
        /// <param name="cipherInput">The encrypted StreamReader to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted StreamReader matches the original input; otherwise, throws an exception.</returns>
        bool ValidateEncryption(StreamReader input, StreamReader cipherInput);

        /// <summary>
        /// Validates if the provided string input matches the decrypted version of the cipherInput.
        /// </summary>
        /// <param name="input">The original plain text input string to compare.</param>
        /// <param name="cipherInput">The encrypted input string to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted text matches the original input; otherwise, false.</returns>
        void ThrowIfInvalidEncryption(string input, string cipherIput);

        /// <summary>
        /// Validates if the provided byte array input matches the decrypted version of the cipherInput byte array.
        /// </summary>
        /// <param name="input">The original plain text input as a byte array to compare.</param>
        /// <param name="cipherInput">The encrypted byte array to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted byte array matches the original input; otherwise, false.</returns>
        void ThrowIfInvalidEncryption(byte[] input, byte[] cipherIput);

        /// <summary>
        /// Validates if the provided SecureString input matches the decrypted version of the cipherInput SecureString.
        /// </summary>
        /// <param name="input">The original plain text input as a SecureString to compare.</param>
        /// <param name="cipherInput">The encrypted SecureString to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted SecureString matches the original input; otherwise, false.</returns>
        void ThrowIfInvalidEncryption(SecureString input, SecureString cipherIput);

        /// <summary>
        /// Validates if the provided StreamReader input matches the decrypted version of the cipherInput StreamReader.
        /// </summary>
        /// <param name="input">The original plain text input as a StreamReader to compare.</param>
        /// <param name="cipherInput">The encrypted StreamReader to be decrypted and compared against the input.</param>
        /// <returns>Returns true if the decrypted StreamReader matches the original input; otherwise, false.</returns>
        void ThrowIfInvalidEncryption(StreamReader input, StreamReader cipherIput);

    }

}
