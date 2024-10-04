using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    /// <summary>
    /// Factory class for creating instances of IHashHandler based on a provided IHashContext.
    /// </summary>
    public class HashHandlerFactory : IHashHandlerFactory
    {
        private readonly IHashContext _hashContext;

        /// <summary>
        /// Initializes a new instance of the HashHandlerFactory with the specified hash context.
        /// </summary>
        /// <param name="hashContext">The context containing the hash algorithm and formatter configurations.</param>
        public HashHandlerFactory(IHashContext hashContext)
        {
            _hashContext = hashContext; 
        }

        /// <summary>
        /// Creates an instance of IHashHandler using the hash algorithm and output formatter from the context.
        /// </summary>
        /// <returns>A new instance of IHashHandler with the configured output format.</returns>
        public IHashHandler CreateHash()
        {
            return new HashHandler(_hashContext.HashAlgorithm, _hashContext.HashFormatter);
        }
    }
}
