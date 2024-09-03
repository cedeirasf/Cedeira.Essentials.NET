using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.HashContext.Interface
{
    public interface IHashContext
    {
        HashAlgorithmName AlgorithmName { get; set; }
        Encoding TextEncoding { get; set; }
    }
}
