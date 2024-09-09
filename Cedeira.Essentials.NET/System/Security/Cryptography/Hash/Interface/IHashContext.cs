using Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Hash;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface
{
    /// <summary>
    /// Interfaz que define el contexto para la configuración de un algoritmo de hash.
    /// </summary>
    public interface IHashContext
    {
        /// <summary>
        /// Obtiene el nombre del algoritmo de hash que se utilizará.
        /// </summary>
        HashAlgorithmName AlgorithmName { get; }
    }
}
