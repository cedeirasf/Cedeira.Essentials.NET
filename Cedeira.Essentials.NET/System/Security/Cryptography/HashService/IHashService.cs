using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.HashService
{
    public interface IHashService
    {
        public string CreateHash<T>(object data) where T : HashAlgorithm, new();
    }
}
