using System.Security.Cryptography;
using System.Text;
using Cedeira.Essentials.NET.System.Security.Cryptography.HashService.Interface;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.HashService
{
    public abstract class HashService<T> : IHashService where T : HashAlgorithm, new()  
    {
        public string CreateHash(string input)
        {
            using T algorithm = new();

            byte[] hashBytes = ComputeHash(algorithm,input);

            return ConvertHashToString(hashBytes);
        }

        public void CreateHash(string input, Stream output)
        {
            using T algorithm = new();

            byte[] hashBytes = ComputeHash(algorithm, input);

            output.Write(hashBytes,0,hashBytes.Length);
        }

        protected string ConvertHashToString(byte[] hashBytes)
        {
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes) sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }

        public bool HashValidate(string input, string hash)
        {
            string computedHash = CreateHash(input);
            return string.Equals(computedHash, hash, StringComparison.OrdinalIgnoreCase);
        }

        protected byte[] ComputeHash(HashAlgorithm algorithm, string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            return algorithm.ComputeHash(inputBytes);
        }


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
