using Cedeira.Essentials.NET.System.ResultPattern;
using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface
{
    public interface IHashHandlerResultPattern<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Calcula el hash de una cadena de texto y devuelve el resultado.
        /// </summary>
        IResult<T> CalculateHash(string input);

        /// <summary>
        /// Calcula el hash de un arreglo de bytes y devuelve el resultado.
        /// </summary>
        IResult<T> CalculateHash(byte[] input);

        /// <summary>
        /// Calcula el hash de un StreamReader y devuelve el resultado.
        /// </summary>
        IResult<T> CalculateHash(StreamReader input);

        /// <summary>
        /// Calcula el hash de un SecureString y devuelve el resultado.
        /// </summary>
        IResult<T> CalculateHash(SecureString input);

        /// <summary>
        /// Valida si el hash de una cadena de texto coincide con el hash proporcionado y devuelve el resultado.
        /// </summary>
        IResult HashValidate(string input, T hash);

        /// <summary>
        /// Valida si el hash de un arreglo de bytes coincide con el hash proporcionado y devuelve el resultado.
        /// </summary>
        IResult HashValidate(byte[] input, T hash);

        /// <summary>
        /// Valida si el hash de un SecureString coincide con el hash proporcionado y devuelve el resultado.
        /// </summary>
        IResult HashValidate(SecureString input, T hash);

        /// <summary>
        /// Valida si el hash de un StreamReader coincide con el hash proporcionado y devuelve el resultado.
        /// </summary>
        IResult HashValidate(StreamReader input, T hash);


    }
}
