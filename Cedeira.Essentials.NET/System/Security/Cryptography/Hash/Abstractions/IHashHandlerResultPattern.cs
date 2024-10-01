using Cedeira.Essentials.NET.System.ResultPattern;
using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions
{
    /// <summary>
    /// Defines an interface for handling the calculation and validation of hashes, 
    /// returning results wrapped in an IResult structure.
    /// </summary>
    public interface IHashHandlerResultPattern 
    {
        /// <summary>
        /// Calculates the hash of a string and returns the result.
        /// </summary>
        /// <param name="input">The input string to be hashed.</param>
        /// <returns>IResult containing the computed hash as a string.</returns>
        IResult<string> CalculateHash(string input);

        /// <summary>
        /// Calculates the hash of a byte array and returns the result.
        /// </summary>
        /// <param name="input">The input byte array to be hashed.</param>
        /// <returns>IResult containing the computed hash as a string.</returns>
        IResult<string> CalculateHash(byte[] input);

        /// <summary>
        /// Calculates the hash of a StreamReader and returns the result.
        /// </summary>
        /// <param name="input">The StreamReader containing the input data to be hashed.</param>
        /// <returns>IResult containing the computed hash as a string.</returns>
        IResult<string> CalculateHash(StreamReader input);

        /// <summary>
        /// Calculates the hash of a SecureString and returns the result.
        /// </summary>
        /// <param name="input">The SecureString to be hashed.</param>
        /// <returns>IResult containing the computed hash as a string.</returns>
        IResult<string> CalculateHash(SecureString input);

        /// <summary>
        /// Validates whether the hash of a string matches the provided hash and returns the result.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <param name="hash">The expected hash value to compare against.</param>
        /// <returns>IResult indicating whether the validation was successful.</returns>
        IResult HashValidate(string input, string hash);

        /// <summary>
        /// Validates whether the hash of a byte array matches the provided hash and returns the result.
        /// </summary>
        /// <param name="input">The input byte array to validate.</param>
        /// <param name="hash">The expected hash value to compare against.</param>
        /// <returns>IResult indicating whether the validation was successful.</returns>
        IResult HashValidate(byte[] input, string hash);

        /// <summary>
        /// Validates whether the hash of a SecureString matches the provided hash and returns the result.
        /// </summary>
        /// <param name="input">The SecureString to validate.</param>
        /// <param name="hash">The expected hash value to compare against.</param>
        /// <returns>IResult indicating whether the validation was successful.</returns>
        IResult HashValidate(SecureString input, string hash);

        /// <summary>
        /// Validates whether the hash of a StreamReader matches the provided hash and returns the result.
        /// </summary>
        /// <param name="input">The StreamReader containing the input data to validate.</param>
        /// <param name="hash">The expected hash value to compare against.</param>
        /// <returns>IResult indicating whether the validation was successful.</returns>
        IResult HashValidate(StreamReader input, string hash);


    }
}
