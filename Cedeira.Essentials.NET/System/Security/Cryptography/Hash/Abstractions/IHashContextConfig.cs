using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions
{
    public interface IHashContextConfig
    {
        HashAlgorithmName? AlgorithmName { get; }
        Func<byte[], string> HashFormatter { get; }
    }
}
