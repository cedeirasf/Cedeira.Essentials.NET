using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Extension;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface
{
    public interface IHashContext
    {
        HashAlgorithmName AlgorithmName { get; }
    }
}
