using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    public class HashHandlerResultPatternFactory : IHashHandlerResultPatternFactory 
    {
        private readonly IResultFactory  _resultFactory;
        private readonly IHashContext _hashContext;

        public HashHandlerResultPatternFactory(IHashContext hashContext, IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
            _hashContext = hashContext; 
        }

        public IHashHandlerResultPattern CreateHash()
        {
            return new HashHandlerResultPattern(_hashContext.HashAlgorithm, _resultFactory);
        }

        public IHashHandlerResultPattern CreateHashWithOutputFormat()
        {
            return new HashHandlerResultPattern(_hashContext.HashAlgorithm, _resultFactory,_hashContext.HashFormatter);
        }
    }
}
