namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    /// <summary>
    /// Factory interface for creating instances of symmetric encryption.
    /// </summary>
    /// <remarks>
    /// This interface defines a method for creating an instance of <see cref="ISymmetricEncryption"/>.
    /// Implementations of this interface are responsible for providing the necessary context
    /// for symmetric encryption operations.
    /// </remarks>
    public interface ISymmetricEncryptationFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="ISymmetricEncryption"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="ISymmetricEncryption"/>.</returns>
        public ISymmetricEncryption Create();
    }
}