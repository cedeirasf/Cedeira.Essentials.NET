using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    public class HashHandlerResultPatternFactory : IHashHandlerResultPatternFactory 
    {
        private readonly IResultFactory  _resultFactory;

        public HashHandlerResultPatternFactory(IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
        }

        public IHashHandlerResultPattern CreateHash(IHashContext hashcontext)
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashcontext.HashConfig.AlgorithmName.Value.Name);
            return new HashHandlerResultPattern(hashAlgorithm, _resultFactory);
        }

        public IHashHandlerResultPattern CreateHashWithOutputFormat(IHashContext hashcontext)
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashcontext.HashConfig.AlgorithmName.Value.Name);
            return new HashHandlerResultPattern(hashAlgorithm, _resultFactory, hashcontext.HashConfig.HashFormatter);
        }
    }
}
