using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Enum;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption
{
    /// <summary>
    /// Represents a context for symmetric encryption, encapsulating a symmetric algorithm.
    /// </summary>
    public class SymmetricEncryptionContext : ISymmetricEncryptionContext
    {
        /// <summary>
        /// Gets the symmetric algorithm used for encryption and decryption.
        /// </summary>
        public SymmetricAlgorithm SymmetricAlgorithm { get; private set; }

        /// <summary>
        /// Contains the mapping of symmetric algorithm types to their creation functions,
        /// valid key lengths, and IV lengths.
        /// </summary>
        /// <remarks>
        /// This dictionary holds a collection of symmetric algorithms, where each entry consists of:
        /// - The algorithm type as the key.
        /// - A tuple containing a function to create an instance of the algorithm,
        ///   an array of valid key lengths for the algorithm, and the required IV length.
        /// </remarks>
        private static readonly Dictionary<SymmetricAlgorithmTypeEnum, (Func<SymmetricAlgorithm> CreateAlgorithm, int[] KeyLengths, int IVLength)> AlgorithmData =
        new Dictionary<SymmetricAlgorithmTypeEnum, (Func<SymmetricAlgorithm>, int[], int)>
        {
            { SymmetricAlgorithmTypeEnum.AES, (Aes.Create, new[] { 16, 24, 32 }, 16) },
            { SymmetricAlgorithmTypeEnum.DES, (DES.Create, new[] { 8 }, 8)},
            { SymmetricAlgorithmTypeEnum.TripleDES, (TripleDES.Create, new[] { 16, 24 }, 8) },
            { SymmetricAlgorithmTypeEnum.TripleDesGNC, (TripleDES.Create, new[] { 16, 24 }, 8) }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricEncryptionContext"/> class with the specified symmetric algorithm.
        /// </summary>
        /// <param name="symmetricAlgorithm">The symmetric algorithm to use.</param>
        protected SymmetricEncryptionContext(SymmetricAlgorithm symmetricAlgorithm)
        {
            SymmetricAlgorithm = symmetricAlgorithm;
        }

        /// <summary>
        /// Creates a new <see cref="SymmetricEncryptionContext"/> instance using the default symmetric algorithm (AES).
        /// </summary>
        /// <returns>A new instance of <see cref="SymmetricEncryptionContext"/>.</returns>
        public static SymmetricEncryptionContext Create()
        {
            var symetricAlgorithmalgorithm = AlgorithmData.Where(x => x.Key == SymmetricAlgorithmTypeEnum.AES).Select(x => x.Value.CreateAlgorithm).First().Invoke();

            return new SymmetricEncryptionContext(symetricAlgorithmalgorithm);
        }

        /// <summary>
        /// Creates a new <see cref="SymmetricEncryptionContext"/> instance from a specified algorithm configuration.
        /// </summary>
        /// <param name="symmetricAlgorithmName">The name of the symmetric algorithm to use.</param>
        /// <param name="CipherMode">The cipher mode to apply.</param>
        /// <param name="padingMode">The padding mode to apply.</param>
        /// <returns>A new instance of <see cref="SymmetricEncryptionContext"/>.</returns>
        public static SymmetricEncryptionContext CreateFromAlgorithmConfig(SymmetricAlgorithmTypeEnum symmetricAlgorithmName, CipherModeTypeEnum CipherMode, PaddingMode padingMode)
        {
            var symetricAlgorithmalgorithm = AlgorithmData.Where(x => x.Key == symmetricAlgorithmName).Select(x => x.Value.CreateAlgorithm).First().Invoke();

            symetricAlgorithmalgorithm.GenerateKey();
            symetricAlgorithmalgorithm.GenerateIV();
            symetricAlgorithmalgorithm.Padding = padingMode;
            symetricAlgorithmalgorithm.Mode = (CipherMode)CipherMode;

            return new SymmetricEncryptionContext(symetricAlgorithmalgorithm);
        }

        /// <summary>
        /// Creates a new <see cref="SymmetricEncryptionContext"/> instance using a full algorithm configuration.
        /// </summary>
        /// <param name="key">The encryption key.</param>
        /// <param name="iV">The initialization vector (IV).</param>
        /// <param name="symmetricAlgorithmName">The name of the symmetric algorithm to use.</param>
        /// <param name="CipherMode">The cipher mode to apply.</param>
        /// <param name="padingMode">The padding mode to apply.</param>
        /// <returns>A new instance of <see cref="SymmetricEncryptionContext"/>.</returns>
        public static SymmetricEncryptionContext CreateFromFullAlgorithmConfig(SymmetricAlgorithmTypeEnum symmetricAlgorithmName, CipherModeTypeEnum CipherMode, PaddingMode padingMode, string key, string iV)
        {
            ValidateParameters(symmetricAlgorithmName, key, iV);

            var symetricAlgorithmalgorithm = AlgorithmData.Where(x => x.Key == symmetricAlgorithmName).Select(x => x.Value.CreateAlgorithm).First().Invoke();

            symetricAlgorithmalgorithm.Key = Encoding.UTF8.GetBytes(key);
            symetricAlgorithmalgorithm.IV = Encoding.UTF8.GetBytes(iV);
            symetricAlgorithmalgorithm.Padding = padingMode;
            symetricAlgorithmalgorithm.Mode = (CipherMode)CipherMode;

            return new SymmetricEncryptionContext(symetricAlgorithmalgorithm);
        }

        /// <summary>
        /// Validates the parameters for the symmetric algorithm configuration.
        /// </summary>
        /// <param name="symmetricAlgorithmName">The name of the symmetric algorithm to validate against.</param>
        /// <param name="key">The encryption key to validate.</param>
        /// <param name="iV">The initialization vector (IV) to validate.</param>
        /// <exception cref="ArgumentException">Thrown when the parameters are invalid.</exception>
        private static void ValidateParameters(SymmetricAlgorithmTypeEnum symmetricAlgorithmName, string key, string iV)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));
            ArgumentException.ThrowIfNullOrEmpty(iV, nameof(iV));

            if (AlgorithmData.TryGetValue(symmetricAlgorithmName, out var algorithmInfo))
            {
                if (!algorithmInfo.KeyLengths.Contains(key.Length))
                    throw new ArgumentException($"The key for {symmetricAlgorithmName} must be one of the following lengths: {string.Join(", ", algorithmInfo.KeyLengths)} bytes.");

                if (algorithmInfo.IVLength != iV.Length)
                    throw new ArgumentException($"The IV for {symmetricAlgorithmName} must be {algorithmInfo.IVLength} bytes long.");
            }
            else
            {
                throw new ArgumentException($"The algorithm {symmetricAlgorithmName} is not valid or not supported.");
            }
        }
    }
}