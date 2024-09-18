﻿using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    /// <summary>
    /// Implementacion de la fabrica para instanciar objetos
    /// </summary>
    public class HashHandlerFactory : IHashCedeiraFactory 
    {
        //private readonly IHashHandlerResultPattern<T> _resultPatternPattern;       
        //private readonly IHashContext<>   

        //public IHashHandler<T> CreateHash() 
        //{
        //    HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashContext.AlgorithmName.Name);
        //    return new HashHandler<T>(hashAlgorithm, hashContext.HashFormatter);
        //}

        //public IHashHandlerResultPattern<T> CreateHashResultPattern()
        //{
        //    HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashContext.AlgorithmName.Name);
        //    return new HashHandlerResultPattern<T>(hashAlgorithm, resultfactory, hashContext.HashFormatter);
        //}

        public IHashHandler CreateHash()
        {
            throw new NotImplementedException();
        }

        public IHashHandlerResultPattern CreateHashResultPattern()
        {
            throw new NotImplementedException();
        }
    }

}
