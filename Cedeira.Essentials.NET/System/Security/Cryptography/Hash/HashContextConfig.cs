using Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    internal class HashContextConfig : IHashContextConfig
    {
        public HashAlgorithmName? AlgorithmName {  get;  private set; }
        public Func<byte[], string>? HashFormatter { get; private set; }

        protected HashContextConfig()
        {
            AlgorithmName = null;
            HashFormatter = null;
        }

        public static HashContextConfig Create(HashAlgorithmName? algorithmName, Func<byte[], string>? hashformat)
        {
            return new HashContextConfig
            {
                AlgorithmName = algorithmName,
                HashFormatter = hashformat
            };
        }
    }
}
