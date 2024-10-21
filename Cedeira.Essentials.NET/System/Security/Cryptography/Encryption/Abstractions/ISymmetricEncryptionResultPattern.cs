using Cedeira.Essentials.NET.System.ResultPattern;
using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    /// <summary>
    /// Interface that defines the result pattern for symmetric encryption operations.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for encrypting and decrypting various types of input,
    /// including byte arrays, strings, SecureString, and StreamReader.
    /// Each method returns a result indicating success or failure, encapsulated in an <see cref="IResult{T}"/> instance.
    /// </remarks>
    public interface ISymmetricEncryptionResultPattern
    {
        /// <summary>
        /// Encrypts the specified byte array input.
        /// </summary>
        /// <param name="input">The byte array to encrypt.</param>
        /// <returns>A result containing the encrypted byte array.</returns>
        IResult<byte[]> Encrypt(byte[] input);

        /// <summary>
        /// Decrypts the specified byte array input.
        /// </summary>
        /// <param name="input">The byte array to decrypt.</param>
        /// <returns>A result containing the decrypted byte array.</returns>
        IResult<byte[]> Decrypt(byte[] input);

        /// <summary>
        /// Encrypts the specified string input.
        /// </summary>
        /// <param name="input">The string to encrypt.</param>
        /// <returns>A result containing the encrypted string.</returns>
        IResult<string> Encrypt(string input);

        /// <summary>
        /// Decrypts the specified string input.
        /// </summary>
        /// <param name="input">The string to decrypt.</param>
        /// <returns>A result containing the decrypted string.</returns>
        IResult<string> Decrypt(string input);

        /// <summary>
        /// Encrypts the specified SecureString input.
        /// </summary>
        /// <param name="input">The SecureString to encrypt.</param>
        /// <returns>A result containing the encrypted SecureString.</returns>
        IResult<SecureString> Encrypt(SecureString input);

        /// <summary>
        /// Decrypts the specified SecureString input.
        /// </summary>
        /// <param name="input">The SecureString to decrypt.</param>
        /// <returns>A result containing the decryp
        IResult<SecureString> Decrypt(SecureString input);

        /// <summary>
        /// Encrypts the specified StreamReader input.
        /// </summary>
        /// <param name="input">The StreamReader to encrypt.</param>
        /// <returns>A result containing the encrypted StreamReader.</returns>
        IResult<StreamReader> Encrypt(StreamReader input);

        /// <summary>
        /// Decrypts the specified StreamReader input.
        /// </summary>
        /// <param name="input">The StreamReader to decrypt.</param>
        /// <returns>A result containing the decryp
        IResult<StreamReader> Decrypt(StreamReader input);
    }
}
