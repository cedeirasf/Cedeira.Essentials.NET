﻿using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories
{
    /// <summary>
    /// Implementacion de la fabrica para instanciar objetos
    /// </summary>
    public class HashHandlerFactory : IHashHandlerFactory 
    {
        private readonly IResultFactory  _resultFactory;
        public HashHandlerFactory()
        {
            
        }
        public HashHandlerFactory(IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
        }

        public IHashHandler CreateHash(IHashContext hashcontext)
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashcontext.HashConfig.AlgorithmName.Value.Name);
            return new HashHandler(hashAlgorithm);
        }

        public IHashHandler CreateHashWithFormat(IHashContext hashcontext)
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashcontext.HashConfig.AlgorithmName.Value.Name);
            return new HashHandler(hashAlgorithm, hashcontext.HashConfig.HashFormatter);
        }


        public IHashHandlerResultPattern CreateHashResultPattern(IHashContext hashcontext)
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashcontext.HashConfig.AlgorithmName.Value.Name);
            return new HashHandlerResultPattern(hashAlgorithm, _resultFactory);
        }

        public IHashHandlerResultPattern CreateHashResultPatternWithFormat(IHashContext hashcontext)
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashcontext.HashConfig.AlgorithmName.Value.Name);
            return new HashHandlerResultPattern(hashAlgorithm, _resultFactory, hashcontext.HashConfig.HashFormatter);
        }
    }

}
