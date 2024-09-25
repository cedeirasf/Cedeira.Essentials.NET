using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    public class HashHandlerFactory : IHashHandlerFactory
    {
        private readonly IHashContext _hashContext;

        public HashHandlerFactory(IHashContext hashContext)
        {
            _hashContext = hashContext; 
        }

        public IHashHandler CreateHash()
        {
            return new HashHandler(_hashContext.HashAlgorithm);
        }

        public IHashHandler CreateHashWithOutputFormat()
        {
            return new HashHandler(_hashContext.HashAlgorithm, _hashContext.HashFormatter);
        }

    }
}
