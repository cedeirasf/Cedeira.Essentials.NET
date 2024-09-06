using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Extension
{
    public static class HashAlgorithmNameExtension
    {
        private static readonly HashSet<HashAlgorithmName> ValidAlgorithms = new()
        {
            HashAlgorithmName.SHA256,
            HashAlgorithmName.SHA1,
            HashAlgorithmName.MD5,
            HashAlgorithmName.SHA384,
            HashAlgorithmName.SHA512,
            HashAlgorithmName.SHA3_256,
            HashAlgorithmName.SHA3_384,
            HashAlgorithmName.SHA3_512
        };

        public static bool SetAlgorithm(this HashAlgorithmName algorithmName)
        {
            if (algorithmName.Name is null)
            {
                algorithmName = HashAlgorithmName.SHA256;
                return false; // Indica que se utilizó el valor por defecto.
            }

            //Incorporar patron result
            if (!ValidAlgorithms.Contains(algorithmName))
                throw new ArgumentException($"The algorithm '{algorithmName.Name}' is not recognized.");

            return true; // Indica que se estableció un algoritmo válido.
        }
    }
}
