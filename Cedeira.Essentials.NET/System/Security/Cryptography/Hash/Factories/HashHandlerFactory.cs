using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Interface;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    /// <summary>
    /// Implementacion de la fabrica para instanciar objetos
    /// </summary>
    public class HashHandlerFactory<T> : IHashCedeiraFactory<T> where T : IEquatable<T>
    {
        ///// <summary>
        ///// Define la implementacion de fábrica para instanciar objetos de tipo HashCedeira
        ///// </summary>
        ///// <param name="hashContext"></param>
        ///// <returns> Devuelve un HashCedeira</returns>
        //public HashHandler CreateHash(HashContext<T> hashContext) 
        //{
        //    HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashContext.AlgorithmName.Name);

        //    return new HashHandler(hashAlgorithm, hashContext.HashFormatter);      
        //}

        //public HashHandlerResult CreateHashResult(HashContext hashContext, IResultFactory resultfactory)
        //{
        //    HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashContext.AlgorithmName.Name);

        //    return new HashHandlerResult(hashAlgorithm, resultfactory, hashContext.HashFormatter);

        //}
        public IHashHandler<T> CreateHash() 
        {
            throw new NotImplementedException();
        }

        public IHashHandlerResultPattern<T> CreateHashResult()
        {
            throw new NotImplementedException();
        }
    }

}
