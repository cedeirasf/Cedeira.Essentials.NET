using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashContext : IHashContext
    {
        public HashAlgorithm HashAlgorithm { get; private set; }
        public Func<byte[], string>? HashFormatter { get; private set; }

        protected HashContext(HashAlgorithm algorithmName, Func<byte[], string>? hashFormatter) 
        {
            HashAlgorithm = algorithmName;
            HashFormatter = hashFormatter;
        }

        public static HashContext Create(string algorithmName, Func<byte[], string>? hashFormatter)
        {
            var hashAlgorithm = CryptoConfig.CreateFromName(algorithmName) as HashAlgorithm;

            if (hashAlgorithm is null)
                throw new ArgumentException($"Invalid algorithm name: {algorithmName}");

            return new HashContext(hashAlgorithm, hashFormatter);
        }

        public static HashContext Create(HashAlgorithm hashAlgorithm, Func<byte[], string>? hashFormatter)
        {
            if (hashAlgorithm is null)
                throw new ArgumentNullException("hashAlgorithm");     

            return new HashContext(hashAlgorithm, hashFormatter);
        }
    }
}

