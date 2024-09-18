using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions
{
    /// <summary>
    /// Interfaz que define el contexto para la configuración de un algoritmo de hash.
    /// </summary>
    public interface IHashContext<T> where T : IEquatable<T>    
    {
        /// <summary>
        /// Obtiene el nombre del algoritmo de hash que se utilizará.
        /// </summary>
        HashAlgorithmName AlgorithmName { get; }
        Func<byte[], T> HashFormatter { get; }

    }
}
