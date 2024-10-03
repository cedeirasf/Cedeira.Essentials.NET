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
        public Func<byte[], string> HashFormatter { get; private set; }

        /// <summary>
        /// Initializes a new instance of HashContext with the specified hash algorithm and formatter.
        /// </summary>
        /// <param name="algorithmName">The hash algorithm to use.</param>
        /// <param name="hashFormatter">Optional formatter for converting the hash to a string.</param>
        protected HashContext(HashAlgorithm algorithmName, Func<byte[], string> hashFormatter) 
        {
            HashAlgorithm = algorithmName;
            HashFormatter = hashFormatter;
        }

        /// <summary>
        /// Creates an instance of <see cref="HashContext"/> from the specified algorithm name.
        /// Uses <see cref="Convert.ToHexString"/> as the default formatter.
        /// </summary>
        /// <param name="algorithmName">The name of the hash algorithm to use.</param>
        /// <returns>A new instance of <see cref="HashContext"/> configured with the specified algorithm.</returns>
        /// <exception cref="ArgumentException">Thrown if the algorithm name is invalid or the corresponding algorithm cannot be created.</exception>
        public static HashContext CreateFromAlgorithmName(string algorithmName)
        {
            var hashAlgorithm = CryptoConfig.CreateFromName(algorithmName) as HashAlgorithm;

            if (hashAlgorithm is null)
                throw new ArgumentException($"Invalid algorithm name: {algorithmName}");

            return new HashContext(hashAlgorithm, Convert.ToHexString);
        }

        /// <summary>
        /// Creates an instance of <see cref="HashContext"/> from the specified algorithm name with a custom hash formatter.
        /// </summary>
        /// <param name="algorithmName">The name of the hash algorithm to use.</param>
        /// <param name="hashFormatter">A function that defines how to format the hash output.</param>
        /// <returns>A new instance of <see cref="HashContext"/> configured with the specified algorithm and formatter.</returns>
        /// <exception cref="ArgumentException">Thrown if the algorithm name is invalid or the corresponding algorithm cannot be created.</exception>
        public static HashContext CreatFromAlgorithmNameWithFormmatter(string algorithmName, Func<byte[], string> hashFormatter)
        {
            var hashAlgorithm = CryptoConfig.CreateFromName(algorithmName) as HashAlgorithm;

            if (hashAlgorithm is null)
                throw new ArgumentException($"Invalid algorithm name: {algorithmName}");

            return new HashContext(hashAlgorithm, hashFormatter);
        }

        /// <summary>
        /// Creates an instance of <see cref="HashContext"/> from the specified hash algorithm instance.
        /// Uses <see cref="Convert.ToHexString"/> as the default formatter.
        /// </summary>
        /// <param name="algorithm">An instance of <see cref="HashAlgorithm"/> to use.</param>
        /// <returns>A new instance of <see cref="HashContext"/> configured with the specified algorithm.</returns>
        /// <exception cref="ArgumentException">Thrown if the algorithm instance is null.</exception>
        public static HashContext CreateFromAlgorithm(HashAlgorithm algorithm)
        {
            if (algorithm  is null)
                throw new ArgumentException($"Invalid algorithm.");

            return new HashContext(algorithm, Convert.ToHexString);
        }

        /// <summary>
        /// Creates an instance of <see cref="HashContext"/> from the specified hash algorithm and a custom hash formatter.
        /// </summary>
        /// <param name="hashAlgorithm">An instance of <see cref="HashAlgorithm"/> to use.</param>
        /// <param name="hashFormatter">A function that defines how to format the hash output.</param>
        /// <returns>A new instance of <see cref="HashContext"/> configured with the specified algorithm and formatter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the hash algorithm is null.</exception>
        public static HashContext CreateFromAlgorithmWithFormatter(HashAlgorithm hashAlgorithm, Func<byte[], string> hashFormatter)
        {
            if (hashAlgorithm is null)
                throw new ArgumentNullException("hashAlgorithm");     

            return new HashContext(hashAlgorithm, hashFormatter);
        }
    }
}

