using Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashContext : IHashContext
    {
        public HashAlgorithmName AlgorithmName { get; private set; }
        public Func<byte[], string> HashFormatter { get; private set; }

        public HashContext(HashAlgorithmName algorithmName, Func<byte[], string> hashFormatter)
        {
            HashAlgorithmNameExtension.SetAlgorithm(algorithmName);

            this.AlgorithmName = algorithmName;
            this.HashFormatter = hashFormatter;
        }
    }

}
