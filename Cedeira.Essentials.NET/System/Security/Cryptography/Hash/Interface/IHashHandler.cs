using Cedeira.Essentials.NET.System.ResultPattern;
using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface
{
    /// <summary>
    /// Define una interfaz para manejar el cálculo y la conversión de hashes en diferentes formatos.
    /// </summary>
    public interface IHashHandler<T> where T : IEquatable<T>
    {

        T CalculateHash(string input);
        T CalculateHash(byte[] input);
        T CalculateHash(StreamReader input);
        T CalculateHash(SecureString input);

        bool HashValidate(string input, T hash);
        bool HashValidate(byte[] input, T hash);
        bool HashValidate(SecureString input, T hash);
        bool HashValidate(StreamReader input, T hash);

        void ThrowIfInvalidHash(string input, T hash);
        void ThrowIfInvalidHash(byte[] input, T hash);
        void ThrowIfInvalidHash(SecureString input, T hash);
        void ThrowIfInvalidHash(StreamReader input, T hash);
    }
}
