using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions
{
    /// <summary>
    /// Provides an interface for defining the context used in hashing operations, 
    /// including the hash algorithm and the formatter to convert the byte array result into a string.
    /// </summary>
    public interface IHashContext
    {
        /// <summary>
        /// Gets the hash algorithm used for computing the hash.
        /// </summary>
        HashAlgorithm HashAlgorithm { get; }

        /// <summary>
        /// Gets the formatter function that converts the resulting hash byte array into a string.
        /// </summary>
        Func<byte[], string> HashFormatter { get; }
    }
}
