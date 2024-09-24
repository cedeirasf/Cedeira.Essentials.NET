using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    public class HashHandlerFactory : IHashHandlerFactory
    {
        public IHashHandler CreateHash(IHashContext hashcontext)
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashcontext.HashConfig.AlgorithmName.Value.Name);
            return new HashHandler(hashAlgorithm);
        }

        public IHashHandler CreateHashWithOutputFormat(IHashContext hashcontext)
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashcontext.HashConfig.AlgorithmName.Value.Name);
            return new HashHandler(hashAlgorithm, hashcontext.HashConfig.HashFormatter);
        }
    }
}
