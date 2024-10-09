﻿using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Enum;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption
{
    public class SymmetricEncryptionContext : ISymmetricEncryptionContext
    {
        public SymmetricAlgorithm SymmetricAlgorithm { get; private set; }

        protected SymmetricEncryptionContext(SymmetricAlgorithm symmetricAlgorithm)
        {
            SymmetricAlgorithm = symmetricAlgorithm;
        }

        public static SymmetricEncryptionContext Create()
        {
            var symetricAlgorithmalgorithm = AlgorithmData.Where(x => x.Key == SymmetricAlgorithmTypeEnum.AES).Select(x => x.Value.CreateAlgorithm).First().Invoke();

            return new SymmetricEncryptionContext(symetricAlgorithmalgorithm);
        }

        public static SymmetricEncryptionContext CreateFromAlgorithmConfig(SymmetricAlgorithmTypeEnum symmetricAlgorithmName, CipherModeTypeEnum CipherMode, PaddingMode padingMode)
        {
            var symetricAlgorithmalgorithm = AlgorithmData.Where(x => x.Key == symmetricAlgorithmName).Select(x => x.Value.CreateAlgorithm).First().Invoke();

            symetricAlgorithmalgorithm.GenerateKey();
            symetricAlgorithmalgorithm.GenerateIV();
            symetricAlgorithmalgorithm.Padding = padingMode;
            symetricAlgorithmalgorithm.Mode = (CipherMode)CipherMode;

            return new SymmetricEncryptionContext(symetricAlgorithmalgorithm);
        }

        public static SymmetricEncryptionContext CreateFromFullAlgorithmConfig(string key, string iV, SymmetricAlgorithmTypeEnum symmetricAlgorithmName, CipherModeTypeEnum CipherMode, PaddingMode padingMode)
        {
            ValidateParameters(symmetricAlgorithmName, key,iV);

            var symetricAlgorithmalgorithm = AlgorithmData.Where(x=>x.Key==symmetricAlgorithmName).Select(x=>x.Value.CreateAlgorithm).First().Invoke();

            symetricAlgorithmalgorithm.Key = Encoding.UTF8.GetBytes(key);
            symetricAlgorithmalgorithm.IV = Encoding.UTF8.GetBytes(iV);
            symetricAlgorithmalgorithm.Padding = padingMode;
            symetricAlgorithmalgorithm.Mode = (CipherMode)CipherMode;

            return new SymmetricEncryptionContext(symetricAlgorithmalgorithm);
        }

        private static readonly Dictionary<SymmetricAlgorithmTypeEnum, (Func<SymmetricAlgorithm> CreateAlgorithm, int[] KeyLengths, int IVLength)> AlgorithmData =
        new Dictionary<SymmetricAlgorithmTypeEnum, (Func<SymmetricAlgorithm>, int[], int )>
        {
            { SymmetricAlgorithmTypeEnum.AES, (Aes.Create, new[] { 16, 24, 32 }, 16) },       
            { SymmetricAlgorithmTypeEnum.DES, (DES.Create, new[] { 8 }, 8)},                  
            { SymmetricAlgorithmTypeEnum.TripleDES, (TripleDES.Create, new[] { 16, 24 }, 8) } 
        };

        private static void ValidateParameters(SymmetricAlgorithmTypeEnum symmetricAlgorithmName, string key, string iV)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));
            ArgumentException.ThrowIfNullOrEmpty(iV, nameof(iV));

            if (AlgorithmData.TryGetValue(symmetricAlgorithmName, out var algorithmInfo))
            {
                if (!algorithmInfo.KeyLengths.Contains(key.Length))
                    throw new ArgumentException($"The key for {symmetricAlgorithmName} must be one of the following lengths: {string.Join(", ", algorithmInfo.KeyLengths)} bytes.");

                if (algorithmInfo.IVLength != iV.Length)
                    throw new ArgumentException($"The IV for {symmetricAlgorithmName} must be {algorithmInfo.IVLength} bytes long.");
            }
            else
            {
                throw new ArgumentException($"The algorithm {symmetricAlgorithmName} is not valid or not supported.");
            }
        }
    }
}
