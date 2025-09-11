using Cedeira.Essentials.NET.Diagnostics.Invariants;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    /// <summary>
    /// Represents a handler for computing and validating hashes using a specified hash algorithm.
    /// </summary>
    public class HashHandler : IHashHandler
    {
        /// <summary>
        /// The hash algorithm used for computing hashes.
        /// </summary>
        private readonly HashAlgorithm _hashAlgorithm;

        /// <summary>
        /// The function used to format the hash bytes into a string.
        /// </summary>
        private readonly Func<byte[], string> _hashFormatter;

        /// <summary>
        /// Initializes a new instance of the HashHandler class with the specified hash algorithm.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm to use.</param>
        public HashHandler(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
            _hashFormatter = Convert.ToHexString;
        }

        /// <summary>
        /// Initializes a new instance of the HashHandler class with the specified hash algorithm and hash formatter.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm to use.</param>
        /// <param name="hashFormatter">The function to format the hash bytes into a string.</param>
        public HashHandler(HashAlgorithm hashAlgorithm, Func<byte[], string> hashFormatter)
        {
            _hashAlgorithm = hashAlgorithm;
            _hashFormatter = hashFormatter;
        }

        /// <summary>
        /// Calculates the hash of the specified string input.
        /// </summary>
        /// <param name="input">The input string to hash.</param>
        /// <returns>The hash of the input string.</returns>
        public string CalculateHash(string input)
        {
            ValidateNull(input);

            byte[] hashBytes = ComputeHash(Encoding.UTF8.GetBytes(input));

            return _hashFormatter(hashBytes);
        }

        /// <summary>
        /// Calculates the hash of the specified byte array input.
        /// </summary>
        /// <param name="input">The input byte array to hash.</param>
        /// <returns>The hash of the input byte array.</returns>
        public string CalculateHash(byte[] input)
        {
            ValidateNull(input);

            byte[] hashBytes = ComputeHash(input);

            return _hashFormatter(hashBytes);
        }

        /// <summary>
        /// Calculates the hash of the specified StreamReader input.
        /// </summary>
        /// <param name="input">The input StreamReader to hash.</param>
        /// <returns>The hash of the input StreamReader.</returns>
        public string CalculateHash(StreamReader input)
        {
            ValidateNull(input);

            byte[] hashBytes = ComputeHash(input);

            return _hashFormatter(hashBytes);
        }

        /// <summary>
        /// Calculates the hash of the specified SecureString input.
        /// </summary>
        /// <param name="input">The input SecureString to hash.</param>
        /// <returns>The hash of the input SecureString.</returns>
        public string CalculateHash(SecureString input)
        {
            ValidateNull(input);

            var bstr = Marshal.SecureStringToBSTR(input);
            try
            {
                var length = Marshal.ReadInt32(bstr, -4);
                var bytes = new byte[length];

                Marshal.Copy(bstr, bytes, 0, length);

                byte[] hashBytes = ComputeHash(bytes);

                return _hashFormatter(hashBytes);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(bstr);
            }
        }

        /// <summary>
        /// Validates the hash of the specified string input against the provided hash.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        /// <returns>True if the hash matches; otherwise, false.</returns>
        public bool HashValidate(string input, string hash)
        {
            HashValidateNull(input, hash);

            var computedHash = CalculateHash(input);

            return computedHash.Equals(hash);
        }

        /// <summary>
        /// Validates the hash of the specified byte array input against the provided hash.
        /// </summary>
        /// <param name="input">The input byte array to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        /// <returns>True if the hash matches; otherwise, false.</returns>
        public bool HashValidate(byte[] input, string hash)
        {
            HashValidateNull(input, hash);

            var computedHash = CalculateHash(input);

            return computedHash.Equals(hash);
        }

        /// <summary>
        /// Validates the hash of the specified SecureString input against the provided hash.
        /// </summary>
        /// <param name="input">The input SecureString to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        /// <returns>True if the hash matches; otherwise, false.</returns>
        public bool HashValidate(SecureString input, string hash)
        {
            HashValidateNull(input, hash);

            var computedHash = CalculateHash(input);

            return computedHash.Equals(hash);
        }

        /// <summary>
        /// Validates the hash of the specified StreamReader input against the provided hash.
        /// </summary>
        /// <param name="input">The input StreamReader to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        /// <returns>True if the hash matches; otherwise, false.</returns>
        public bool HashValidate(StreamReader input, string hash)
        {
            HashValidateNull(input, hash);

            var computedHash = CalculateHash(input);

            return computedHash.Equals(hash);
        }

        /// <summary>
        /// Throws a CryptographicException if the hash of the specified string input does not match the provided hash.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        public void ThrowIfInvalidHash(string input, string hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new CryptographicException("Invalid hash.");
            }
        }

        /// <summary>
        /// Throws a CryptographicException if the hash of the specified byte array input does not match the provided hash.
        /// </summary>
        /// <param name="input">The input byte array to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        public void ThrowIfInvalidHash(byte[] input, string hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new CryptographicException("Invalid hash.");
            }
        }

        /// <summary>
        /// Throws a CryptographicException if the hash of the specified SecureString input does not match the provided hash.
        /// </summary>
        /// <param name="input">The input SecureString to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        public void ThrowIfInvalidHash(SecureString input, string hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new CryptographicException("Invalid hash.");
            }
        }

        /// <summary>
        /// Throws a CryptographicException if the hash of the specified StreamReader input does not match the provided hash.
        /// </summary>
        /// <param name="input">The input StreamReader to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        public void ThrowIfInvalidHash(StreamReader input, string hash)
        {
            if (!HashValidate(input, hash))
            {
                throw new CryptographicException("Invalid hash.");
            }
        }

        /// <summary>
        /// Computes the hash of the specified byte array input.
        /// </summary>
        /// <param name="input">The input byte array to hash.</param>
        /// <returns>The computed hash bytes.</returns>
        private byte[] ComputeHash(byte[] input)
        {
            return _hashAlgorithm.ComputeHash(input);
        }

        /// <summary>
        /// Computes the hash of the specified StreamReader input.
        /// </summary>
        /// <param name="input">The input StreamReader to hash.</param>
        /// <returns>The computed hash bytes.</returns>
        private byte[] ComputeHash(StreamReader input)
        {
            return _hashAlgorithm.ComputeHash(input.BaseStream);
        }

        /// <summary>
        /// Validates that the input is not null.
        /// </summary>
        /// <typeparam name="T">The type of the input.</typeparam>
        /// <param name="input">The input to validate.</param>
        private void ValidateNull<T>(T input)
        {
            Invariants.For(input).IsNotNull($"{nameof(input)} cannot be null.");
        }

        /// <summary>
        /// Validates that the input and hash are not null.
        /// </summary>
        /// <typeparam name="T">The type of the input.</typeparam>
        /// <param name="input">The input to validate.</param>
        /// <param name="hash">The hash to validate against.</param>
        private void HashValidateNull<T>(T input, string hash)
        {
            Invariants.For(input).IsNotNull($"{nameof(input)} cannot be null.");
            Invariants.For(hash).IsNotNull($"{nameof(hash)} cannot be null.");
        }
    }
}