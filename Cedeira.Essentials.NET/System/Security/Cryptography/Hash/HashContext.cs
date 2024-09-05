using System.Security.Cryptography;
using System.Text;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Extension;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    public class HashContext : IHashContext
    {
        public HashAlgorithmName AlgorithmName { get; private set; }
 
        public HashContext(HashAlgorithmName algorithmName)
        {
            HashAlgorithNameExtension.SetAlgorithm(algorithmName);
            this.AlgorithmName = algorithmName;
        }
    }
}
