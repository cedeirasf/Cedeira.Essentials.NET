using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface
{
    /// <summary>
    /// Define una interfaz para manejar el cálculo y la conversión de hashes en diferentes formatos.
    /// </summary>
    public interface IHashHandler
    {

        /// <summary>
        /// Calcula el hash de una cadena de texto.
        /// </summary>
        string CalculateHash(string input);

        /// <summary>
        /// Calcula el hash de un arreglo de bytes.
        /// </summary>
        string CalculateHash(byte[] input);

        /// <summary>
        /// Calcula el hash de un StreamReader.
        /// </summary>
        string CalculateHash(StreamReader input);

        /// <summary>
        /// Calcula el hash de un SecureString.
        /// </summary>
        string CalculateHash(SecureString input);

        /// <summary>
        /// Valida si el hash de una cadena de texto coincide con el hash proporcionado.
        /// </summary>
        bool HashValidate(string input, string hash);

        /// <summary>
        /// Valida si el hash de un arreglo de bytes coincide con el hash proporcionado.
        /// </summary>
        bool HashValidate(byte[] input, string hash);

        /// <summary>
        /// Valida si el hash de un SecureString coincide con el hash proporcionado.
        /// </summary>
        bool HashValidate(SecureString input, string hash);

        /// <summary>
        /// Valida si el hash de un StreamReader coincide con el hash proporcionado.
        /// </summary>
        bool HashValidate(StreamReader input, string hash);

        /// <summary>
        /// Lanza una excepción si el hash de una cadena de texto no coincide con el hash proporcionado.
        /// </summary>
        void ThrowIfInvalidHash(string input, string hash);

        /// <summary>
        /// Lanza una excepción si el hash de un arreglo de bytes no coincide con el hash proporcionado.
        /// </summary>
        void ThrowIfInvalidHash(byte[] input, string hash);

        /// <summary>
        /// Lanza una excepción si el hash de un SecureString no coincide con el hash proporcionado.
        /// </summary>
        void ThrowIfInvalidHash(SecureString input, string hash);

        /// <summary>
        /// Lanza una excepción si el hash de un StreamReader no coincide con el hash proporcionado.
        /// </summary>
        void ThrowIfInvalidHash(StreamReader input, string hash);

    }
}
