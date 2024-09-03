using System.Security.Cryptography;
using System.Text;
using Cedeira.Essentials.NET.System.Security.Cryptography.HashService.Interface;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.HashService
{
    public abstract class HashService<T> : IHashService where T : HashAlgorithm, new()  
    {
        public string CreateHash(string input)
        {
            using T algorithm = new T();

            byte[] hashBytes = ComputeHash(algorithm,input);

            return ConvertHashToString(hashBytes);
        }

        protected string ConvertHashToString(byte[] hashBytes)
        {
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes) sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }

        protected byte[] ComputeHash(HashAlgorithm algorithm, string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            return algorithm.ComputeHash(inputBytes);
        }

        //private string ComputeHash(string input, HashAlgorithm hashAlgorithm)
        //{
        //    byte[] data = ConvertInputToByteArray(input);
        //    byte[] hashBytes = hashAlgorithm.ComputeHash(data);
        //    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        //}

        private byte[] ConvertInputToByteArray(object input)
        {
            if (input is string str)
            {
                return Encoding.ASCII.GetBytes(str);
            }
            else if (input is Stream stream)
            {
                using MemoryStream ms = new();
                stream.CopyTo(ms);
                return ms.ToArray();
            }
            else
            {
                throw new ArgumentException("Data type nos supported. We recomend either string or stream ", nameof(input));
            }
        }
    }
}
