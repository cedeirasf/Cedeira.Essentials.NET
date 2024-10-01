using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
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

        public static SymmetricEncryptionContext Create(SymmetricAlgorithmType symmetricAlgorithmName, CipherMode CipherMode)
        {
            var symetricAlgorithmalgorithm = AlgorithmData.Where(x => x.Key == symmetricAlgorithmName).Select(x => x.Value.CreateAlgorithm).First().Invoke();

            symetricAlgorithmalgorithm.GenerateKey();
            symetricAlgorithmalgorithm.GenerateIV();
            symetricAlgorithmalgorithm.Mode = CipherMode;

            return new SymmetricEncryptionContext(symetricAlgorithmalgorithm);
        }
        public static SymmetricEncryptionContext Create(string key, string iV, SymmetricAlgorithmType symmetricAlgorithmName, CipherMode cipherMode)
        {
            ValidateParameters(symmetricAlgorithmName, key,iV);

            var symetricAlgorithmalgorithm = AlgorithmData.Where(x=>x.Key==symmetricAlgorithmName).Select(x=>x.Value.CreateAlgorithm).First().Invoke();

            symetricAlgorithmalgorithm.Key = Encoding.UTF8.GetBytes(key);
            symetricAlgorithmalgorithm.IV = Encoding.UTF8.GetBytes(iV);
            symetricAlgorithmalgorithm.Mode = cipherMode;

            return new SymmetricEncryptionContext(symetricAlgorithmalgorithm);
        }

        private static readonly Dictionary<SymmetricAlgorithmType, (Func<SymmetricAlgorithm> CreateAlgorithm, int[] KeyLengths, int IVLength)> AlgorithmData =
        new Dictionary<SymmetricAlgorithmType, (Func<SymmetricAlgorithm>, int[], int )>
        {
            { SymmetricAlgorithmType.AES, (Aes.Create, new[] { 16, 24, 32 }, 16) },       // AES: 128, 192, 256 bits
            { SymmetricAlgorithmType.DES, (DES.Create, new[] { 8 }, 8)},                  // DES: 64 bits
            { SymmetricAlgorithmType.TripleDES, (TripleDES.Create, new[] { 16, 24 }, 8) } // TripleDES: 128, 192 bits
        };
        private static void ValidateParameters(SymmetricAlgorithmType symmetricAlgorithmName, string key, string iV)
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
