using Cedeira.Essentials.NET.System.ResultPattern;
using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface
{
    public interface IHashHandlerResultPattern<T> where T : IEquatable<T>
    {
        IResult<T> CalculateHash(string input);
        IResult<T> CalculateHash(byte[] input);
        IResult<T> CalculateHash(StreamReader input);
        IResult<T> CalculateHash(SecureString input);

        IResult HashValidate(string input, T hash);
        IResult HashValidate(byte[] input, T hash);
        IResult HashValidate(SecureString input, T hash);
        IResult HashValidate(StreamReader input, T hash);

    }
}
