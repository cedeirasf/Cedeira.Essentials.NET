using Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash
{
    /// <summary>
    /// Implementación de la interfaz <see cref="IHashContext"/> que representa el contexto de configuración para un algoritmo de hash.
    /// </summary>
    public class HashContext<THash> : IHashContext<THash> where THash : IEquatable<THash>
    {
        /// <summary>
        /// Obtiene el nombre del algoritmo de hash configurado.
        /// </summary>
        public HashAlgorithmName AlgorithmName { get; private set; }
        public Func<byte[], THash> HashFormatter { get; private set; }

        public HashContext()
        {
                
        }

        public void SetAlgorithm(HashAlgorithmName algorithmName)
        {
        }

        public void SetHashFormatter(Func<byte[], THash> hashFormatter) 
        {
        }


        /// <summary>
        /// Constructor que inicializa una nueva instancia de la clase <see cref="HashContext"/> con el algoritmo de hash especificado.
        /// </summary>
        /// <param name="algorithmName">El nombre del algoritmo de hash que se utilizará.</param>
        public HashContext(HashAlgorithmName algorithmName, Func<byte[], THash> hashFormatter)
        {
            HashAlgorithmNameExtension.SetAlgorithm(algorithmName);
            this.AlgorithmName = algorithmName;
            this.HashFormatter = hashFormatter;
        }
    }

}
