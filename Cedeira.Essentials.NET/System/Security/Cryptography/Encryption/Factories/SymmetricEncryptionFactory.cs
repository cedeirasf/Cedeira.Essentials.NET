using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories
{
    /// <summary>
    /// A factory class for creating instances of <see cref="ISymmetricEncryption"/>.
    /// </summary>
    /// <remarks>
    /// This class depends on an <see cref="ISymmetricEncryptionContext"/> to obtain the symmetric
    /// algorithm required for encryption operations.
    /// </remarks>
    public class SymmetricEncryptionFactory : ISymmetricEncryptationFactory
    {
        /// <summary>
        /// Gets the ISymmetricContext algorithm used to create a new SymmetricEncryption.
        /// </summary>
        private readonly ISymmetricEncryptionContext _symmetricEncryptionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricEncryptionFactory"/> class.
        /// </summary>
        /// <param name="symmetricEncryptionContext">The context providing the symmetric algorithm for encryption.</param>
        public SymmetricEncryptionFactory(ISymmetricEncryptionContext symmetricEncryptionContext)
        {
            _symmetricEncryptionContext = symmetricEncryptionContext;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ISymmetricEncryption"/> using the symmetric algorithm
        /// provided by the <see cref="ISymmetricEncryptionContext"/>.
        /// </summary>
        /// <returns>An instance of <see cref="ISymmetricEncryption"/> configured with the appropriate algorithm.</returns>
        public ISymmetricEncryption Create()
        {
            return new SymmetricEncryption(_symmetricEncryptionContext.SymmetricAlgorithm);
        }
    }
}