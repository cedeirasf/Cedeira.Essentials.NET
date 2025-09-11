namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    /// <summary>
    /// Interface for creating instances of ISymmetricEncryptionResultPattern.
    /// </summary>
    /// <remarks>
    /// This factory interface defines a method to create instances of the symmetric encryption result pattern.
    /// Implementations of this interface will provide the necessary context for encryption operations.
    /// </remarks>
    public interface ISymmetricEncryptionResultPatternFactory
    {
        /// <summary>
        /// Creates an instance of ISymmetricEncryptionResultPattern.
        /// </summary>
        /// <returns>An instance of ISymmetricEncryptionResultPattern.</returns>
        public ISymmetricEncryptionResultPattern Create();
    }
}