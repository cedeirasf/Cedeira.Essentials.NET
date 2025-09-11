using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    /// <summary>
    /// Interface that provides the context for symmetric encryption operations.
    /// </summary>
    /// <remarks>
    /// This interface exposes the symmetric algorithm that will be used in encryption and decryption operations.
    /// Implementations of this interface should provide the necessary algorithm configuration
    /// to be used by <see cref="ISymmetricEncryption"/> implementations.
    /// </remarks>
    public interface ISymmetricEncryptionContext
    {
        /// <summary>
        /// Gets the <see cref="SymmetricAlgorithm"/> used for encryption and decryption.
        /// </summary>
        /// <value>The symmetric algorithm instance, such as AES, DES, or TripleDES.</value>
        SymmetricAlgorithm SymmetricAlgorithm { get; }
    }
}