using System.Security.Cryptography;
using System.Text;
using Cedeira.Essentials.NET.System.Security.Cryptography.HashService.Interface;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.HashService
{
    public class HashService : IHashService
    {

        //context
        public string CreateHash<T>(object data) where T : HashAlgorithm, new()
        {
            using T hashAlgorithm = new();
            return ComputeHashInternal(data, hashAlgorithm);
        }

        private string ComputeHashInternal(object input, HashAlgorithm hashAlgorithm)
        {
            byte[] data = ConvertInputToByteArray(input);
            byte[] hashBytes = hashAlgorithm.ComputeHash(data);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
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
