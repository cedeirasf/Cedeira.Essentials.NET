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
    }

}
