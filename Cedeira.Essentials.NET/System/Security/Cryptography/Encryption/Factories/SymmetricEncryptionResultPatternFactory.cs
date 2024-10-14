using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories
{
    /// <summary>
    /// A factory class for creating instances of <see cref="ISymmetricEncryptionResultPattern"/>.
    /// </summary>
    /// <remarks>
    /// This class is responsible for providing the necessary context and result factory
    /// to create an instance of <see cref="SymmetricEncryptionResultPattern"/>.
    /// </remarks>
    public class SymmetricEncryptionResultPatternFactory : ISymmetricEncryptionResultPatternFactory 
    {
        /// <summary>
        /// Gets the symmetric algorithm used for encryption and decryption.
        /// </summary>
        private readonly ISymmetricEncryptionContext _symmetricEncryptionContext;

        /// <summary>
        /// Gets the IResultFactory for SymmetricEncryptionResultPattern.
        /// </summary>
        private readonly IResultFactory _resultFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricEncryptionResultPatternFactory"/> class.
        /// </summary>
        /// <param name="symmetricEncryptionContext">The context providing the symmetric algorithm for encryption.</param>
        /// <param name="resultFactory">The factory responsible for creating result objects.</param>
        public SymmetricEncryptionResultPatternFactory(ISymmetricEncryptionContext symmetricEncryptionContext, IResultFactory resultFactory)
        {
            _symmetricEncryptionContext = symmetricEncryptionContext;
            _resultFactory = resultFactory;     
        }

        /// <summary>
        /// Creates a new instance of <see cref="ISymmetricEncryptionResultPattern"/> using the provided context and result factory.
        /// </summary>
        /// <returns>An instance of <see cref="ISymmetricEncryptionResultPattern"/> configured with the appropriate algorithm and result factory.</returns>
        public ISymmetricEncryptionResultPattern Create()
        {
            return new SymmetricEncryptionResultPattern(_symmetricEncryptionContext.SymmetricAlgorithm, _resultFactory);
        }
    }
}
