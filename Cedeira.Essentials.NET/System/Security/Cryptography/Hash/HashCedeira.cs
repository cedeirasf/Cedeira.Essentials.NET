using System.Security.Cryptography;
using System.Text;
using Cedeira.Essentials.NET.System.Security.Cryptography.HashService.Interface;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.HashService
{
    public class HashCedeira : IHashCedeira 
    {
        private readonly HashAlgorithm _hashAlgorithm;       

        public HashCedeira(HashAlgorithm hashAlgorithm) 
        {
            _hashAlgorithm = hashAlgorithm;       
        }


        /// <summary>
        /// pendiente
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string CaculateHash(string input)
        {
            byte[] hashBytes = ComputeHash(_hashAlgorithm, input);

            return ConvertHashToString(hashBytes);
        }

        /// <summary>
        ///  pendiente
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public void CalculateHash(string input, Stream output)
        {
            byte[] hashBytes = ComputeHash(_hashAlgorithm, input);

            output.Write(hashBytes,0,hashBytes.Length);
        }

        /// <summary>
        ///  pendiente
        /// </summary>
        /// <param name="hashBytes"></param>
        /// <returns></returns>
        protected string ConvertHashToString(byte[] hashBytes)
        {
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes) sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool HashValidate(string input, string hash)
        {
            string computedHash = CaculateHash(input);

            return string.Equals(computedHash, hash, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///  pendiente
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected byte[] ComputeHash(HashAlgorithm algorithm, string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            return algorithm.ComputeHash(inputBytes);
        }

    }
}
