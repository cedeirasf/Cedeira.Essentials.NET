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
        /// <returns>A result containing the decrypted StreamReader.</returns>
        IResult<StreamReader> Decrypt(StreamReader input);

        /// <summary>
        /// Validates if the provided string input matches the decrypted version of the cipherInput.
        /// </summary>
        /// <param name="input">The original plain text input string to compare.</param>
        /// <param name="cipherInput">The encrypted input string to be decrypted and compared against the input.</param>
        /// <returns>A result containing if the decrypted string matches the original input.</returns>
        IResult ValidateEncryption(string input, string cipherInput);

        /// <summary>
        /// Validates if the provided byte array input matches the decrypted version of the cipherInput byte array.
        /// </summary>
        /// <param name="input">The original plain text input as a byte array to compare.</param>
        /// <param name="cipherInput">The encrypted byte array to be decrypted and compared against the input.</param>
        /// <returns>A result containing if the decrypted array bite matches the original input.</returns>
        IResult ValidateEncryption(byte[] input, byte[] cipherInput);

        /// <summary>
        /// Validates if the provided SecureString input matches the decrypted version of the cipherInput SecureString.
        /// </summary>
        /// <param name="input">The original plain text input as a SecureString to compare.</param>
        /// <param name="cipherInput">The encrypted SecureString to be decrypted and compared against the input.</param>
        /// <returns>A result containing if the decrypted secureString matches the original input.</returns>
        IResult ValidateEncryption(SecureString input, SecureString cipherInput);

        /// <summary>
        /// Validates if the provided StreamReader input matches the decrypted version of the cipherInput StreamReader.
        /// </summary>
        /// <param name="input">The original plain text input as a StreamReader to compare.</param>
        /// <param name="cipherInput">The encrypted StreamReader to be decrypted and compared against the input.</param>
        /// <returns>A result containing if the decrypted streamReader matches the original input.</returns>
        IResult ValidateEncryption(StreamReader input, StreamReader cipherInput);

    }
}
