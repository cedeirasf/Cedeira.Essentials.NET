using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Hash
{
    /// <summary>
    /// Clase de extensión para la configuración y validación de nombres de algoritmos de hash.
    /// </summary>
    public static class HashAlgorithmNameExtension
    {
        /// <summary>
        /// Conjunto de algoritmos de hash válidos que pueden ser utilizados.
        /// </summary>
        public static readonly HashSet<HashAlgorithmName>? ValidAlgorithms = new()
        {
                HashAlgorithmName.SHA256,
                HashAlgorithmName.SHA1,
                HashAlgorithmName.MD5,
                HashAlgorithmName.SHA384,
                HashAlgorithmName.SHA512,
        };

        /// <summary>
        /// Establece un algoritmo de hash si es válido, o utiliza un valor por defecto si no se especifica uno.
        /// </summary>
        /// <param name="algorithmName">El nombre del algoritmo de hash a validar.</param>
        /// <returns>Un valor booleano que indica si se estableció un algoritmo válido o si se utilizó el valor por defecto.</returns>
        /// <exception cref="ArgumentException">Se lanza si el algoritmo proporcionado no es válido.</exception>
        public static bool ValidAlgorithm(this HashAlgorithmName algorithmName)
        {
            if (!ValidAlgorithms.Contains(algorithmName))
                throw new ArgumentException($"The algorithm '{algorithmName.Name}' is not recognized.");

            return true; // Indica que se estableció un algoritmo válido.
        }
    }

}
