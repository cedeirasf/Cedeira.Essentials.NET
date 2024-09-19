using Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    internal class HashContext : IHashContext
    {
        public IHashContextConfig HashConfig { get; private set; }

        protected HashContext() 
        {
            HashConfig = HashContextConfig.Create(null, null);
        }

        public static HashContext Create(HashAlgorithmName algorithmName, Func<byte[], string>? hashFormatter)
        {
            HashAlgorithmNameExtension.SetAlgorithm(algorithmName);

            return new HashContext
            {
                HashConfig = HashContextConfig.Create(algorithmName, hashFormatter)
            };
        }
    }
}

