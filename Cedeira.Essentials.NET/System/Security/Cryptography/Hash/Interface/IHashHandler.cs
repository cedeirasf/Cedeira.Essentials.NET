using Cedeira.Essentials.NET.System.ResultPattern;
using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface
{
    /// <summary>
    /// Define una interfaz para manejar el cálculo y la conversión de hashes en diferentes formatos.
    /// </summary>
    public interface IHashHandler<T> where T : IEquatable<T>
    {

        /// <summary>
        /// Calcula el hash de una cadena de texto.
        /// </summary>
        T CalculateHash(string input);

        /// <summary>
        /// Calcula el hash de un arreglo de bytes.
        /// </summary>
        T CalculateHash(byte[] input);

        /// <summary>
        /// Calcula el hash de un StreamReader.
        /// </summary>
        T CalculateHash(StreamReader input);

        /// <summary>
        /// Calcula el hash de un SecureString.
        /// </summary>
        T CalculateHash(SecureString input);

        /// <summary>
        /// Valida si el hash de una cadena de texto coincide con el hash proporcionado.
        /// </summary>
        bool HashValidate(string input, T hash);

        /// <summary>
        /// Valida si el hash de un arreglo de bytes coincide con el hash proporcionado.
        /// </summary>
        bool HashValidate(byte[] input, T hash);

        /// <summary>
        /// Valida si el hash de un SecureString coincide con el hash proporcionado.
        /// </summary>
        bool HashValidate(SecureString input, T hash);

        /// <summary>
        /// Valida si el hash de un StreamReader coincide con el hash proporcionado.
        /// </summary>
        bool HashValidate(StreamReader input, T hash);

        /// <summary>
        /// Lanza una excepción si el hash de una cadena de texto no coincide con el hash proporcionado.
        /// </summary>
        void ThrowIfInvalidHash(string input, T hash);

        /// <summary>
        /// Lanza una excepción si el hash de un arreglo de bytes no coincide con el hash proporcionado.
        /// </summary>
        void ThrowIfInvalidHash(byte[] input, T hash);

        /// <summary>
        /// Lanza una excepción si el hash de un SecureString no coincide con el hash proporcionado.
        /// </summary>
        void ThrowIfInvalidHash(SecureString input, T hash);

        /// <summary>
        /// Lanza una excepción si el hash de un StreamReader no coincide con el hash proporcionado.
        /// </summary>
        void ThrowIfInvalidHash(StreamReader input, T hash);

    }
}
