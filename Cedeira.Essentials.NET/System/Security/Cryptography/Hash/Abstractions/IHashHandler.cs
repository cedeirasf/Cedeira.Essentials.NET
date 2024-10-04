using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions
{
    /// <summary>
    /// Defines an interface for handling the calculation and validation of hashes in various formats.
    /// </summary>
    public interface IHashHandler
    {
        /// <summary>
        /// Calculates the hash of a string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CalculateHash(string input);

        /// <summary>
        /// Calculates the hash of a byte array.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CalculateHash(byte[] input);

        /// <summary>
        /// Calculates the hash of a StreamReader
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CalculateHash(StreamReader input);

        /// <summary>
        /// Calculates the hash of a SecureString
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CalculateHash(SecureString input);

        /// <summary>
        /// Validates whether the hash of a string matches the provided hash.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        bool HashValidate(string input, string hash);

        /// <summary>
        /// Validates whether the hash of a byte array matches the provided hash.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        bool HashValidate(byte[] input, string hash);

        /// <summary>
        /// Validates whether the hash of a SecureString matches the provided hash.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        bool HashValidate(SecureString input, string hash);

        /// <summary>
        /// Validates whether the hash of a StreamReader matches the provided hash.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        bool HashValidate(StreamReader input, string hash);

        /// <summary>
        /// Throws an exception if the hash of a string does not match the provided hash.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        void ThrowIfInvalidHash(string input, string hash);

        /// <summary>
        /// hrows an exception if the hash of a byte array does not match the provided hash
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        void ThrowIfInvalidHash(byte[] input, string hash);

        /// <summary>
        /// Throws an exception if the hash of a SecureString does not match the provided hash.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        void ThrowIfInvalidHash(SecureString input, string hash);

        /// <summary>
        /// Throws an exception if the hash of a StreamReader does not match the provided hash.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        void ThrowIfInvalidHash(StreamReader input, string hash);

    }
}
