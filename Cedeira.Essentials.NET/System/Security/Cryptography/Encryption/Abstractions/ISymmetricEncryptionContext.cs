using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions
{
    public interface ISymmetricEncryptionContext
    {
        SymmetricAlgorithm SymmetricAlgorithm { get; }  
    }
}
