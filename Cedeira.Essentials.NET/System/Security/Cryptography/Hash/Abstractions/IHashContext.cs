using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions
{
    public interface IHashContext
    {
        HashAlgorithmName AlgorithmName { get; }
        Func<byte[], string> HashFormatter { get; }

    }
}
