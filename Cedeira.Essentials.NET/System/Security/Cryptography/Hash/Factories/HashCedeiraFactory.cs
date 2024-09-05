using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Interface;
using Cedeira.Essentials.NET.System.Security.Cryptography.HashService;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    /// <summary>
    /// Implementacion de la fabrica para instanciar objetos
    /// </summary>
    public class HashCedeiraFactory : IHashCedeiraFactory
    {
        /// <summary>
        /// Define la implementacion de fábrica para instanciar objetos de tipo HashCedeira
        /// </summary>
        /// <param name="hashContext"></param>
        /// <returns></returns>
        public HashCedeira CreateHash(HashContext hashContext) 
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashContext.AlgorithmName.Name);

            return new HashCedeira(hashAlgorithm);      
        }
    }

}
