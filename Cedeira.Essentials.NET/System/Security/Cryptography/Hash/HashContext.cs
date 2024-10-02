using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    /// <summary>
    /// Represents a context for configuring a hash algorithm and output formatting for hash operations.
    /// </summary>
    public class HashContext : IHashContext
    {
        /// <summary>
        /// Gets the configured hash algorithm.
        /// </summary>
        public HashAlgorithm HashAlgorithm { get; private set; }

        /// <summary>
        /// Gets the optional formatter function for converting the hash byte array to a string.
        /// </summary>
        public Func<byte[], string>? HashFormatter { get; private set; }

        /// <summary>
        /// Initializes a new instance of HashContext with the specified hash algorithm and optional formatter.
        /// </summary>
        /// <param name="algorithmName">The hash algorithm to use.</param>
        /// <param name="hashFormatter">Optional formatter for converting the hash to a string.</param>
        protected HashContext(HashAlgorithm algorithmName, Func<byte[], string> hashFormatter) 
        {
            HashAlgorithm = algorithmName;
            HashFormatter = hashFormatter;
        }

        /// <summary>
        /// Initializes a new instance of HashContext with the specified hash algorithm with a hexadecimal string format.
        /// </summary>
        /// <param name="algorithmName">The hash algorithm to use.</param>
        /// <param name="hashFormatter">Optional formatter for converting the hash to a string.</param>
        protected HashContext(HashAlgorithm algorithmName)
        {
            HashAlgorithm = algorithmName;
            HashFormatter = Convert.ToHexString;
        }

        public static HashContext Create(string algorithmName)
        {
            var hashAlgorithm = CryptoConfig.CreateFromName(algorithmName) as HashAlgorithm;

            if (hashAlgorithm is null)
                throw new ArgumentException($"Invalid algorithm name: {algorithmName}");

            return new HashContext(hashAlgorithm);
        }

        public static HashContext Create(HashAlgorithm algorithm)
        {
            if (algorithm  is null)
                throw new ArgumentException($"Invalid algorithm.");

            return new HashContext(algorithm);
        }

        /// <summary>
        /// Creates a new HashContext instance based on the algorithm name and optional formatter.
        /// </summary>
        /// <param name="algorithmName">The name of the hash algorithm to use.</param>
        /// <param name="hashFormatter">Optional formatter for converting the hash to a string.</param>
        /// <returns>A new instance of HashContext.</returns>
        /// <exception cref="ArgumentException">Thrown if the algorithm name is invalid.</exception>
        public static HashContext CreateWithFormat(string algorithmName, Func<byte[], string> hashFormatter)
        {
            var hashAlgorithm = CryptoConfig.CreateFromName(algorithmName) as HashAlgorithm;

            if (hashAlgorithm is null)
                throw new ArgumentException($"Invalid algorithm name: {algorithmName}");

            return new HashContext(hashAlgorithm, hashFormatter);
        }

        /// <summary>
        /// Creates a new HashContext instance based on the provided hash algorithm and optional formatter.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm to use.</param>
        /// <param name="hashFormatter">Optional formatter for converting the hash to a string.</param>
        /// <returns>A new instance of HashContext.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the hash algorithm is null.</exception>
        public static HashContext CreateWithFormat(HashAlgorithm hashAlgorithm, Func<byte[], string> hashFormatter)
        {
            if (hashAlgorithm is null)
                throw new ArgumentNullException("hashAlgorithm");     

            return new HashContext(hashAlgorithm, hashFormatter);
        }

    }
}

