using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    /// <summary>
    /// Factory class for creating instances of IHashHandlerResultPattern based on a provided IHashContext and IResultFactory.
    /// </summary>
    public class HashHandlerResultPatternFactory : IHashHandlerResultPatternFactory 
    {
        /// <summary>
        /// The factory used to create result objects.
        /// </summary>
        private readonly IResultFactory  _resultFactory;
        /// <summary>
        /// The hashContext dto, used for computing and validating hashes.
        /// </summary>
        private readonly IHashContext _hashContext;

        /// <summary>
        /// Initializes a new instance of the HashHandlerResultPatternFactory with the specified hash context and result factory.
        /// </summary>
        /// <param name="hashContext">The context containing the hash algorithm and formatter configurations.</param>
        /// <param name="resultFactory">The factory responsible for creating result instances.</param>
        public HashHandlerResultPatternFactory(IHashContext hashContext, IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
            _hashContext = hashContext; 
        }

        /// <summary>
        /// Creates an instance of IHashHandlerResultPattern using the hash algorithm from the context.
        /// </summary>
        /// <returns>A new instance of IHashHandlerResultPattern.</returns>
        public IHashHandlerResultPattern CreateHash()
        {
            return new HashHandlerResultPattern(_hashContext.HashAlgorithm, _hashContext.HashFormatter, _resultFactory);
        }

    }
}
