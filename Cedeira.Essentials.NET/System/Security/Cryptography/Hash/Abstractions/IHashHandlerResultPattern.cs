using Cedeira.Essentials.NET.System.ResultPattern;
using System.Security;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions
{
    public interface IHashHandlerResultPattern 
    {
        /// <summary>
        /// Calcula el hash de una cadena de texto y devuelve el resultado.
        /// </summary>
        IResult<string> CalculateHash(string input);

        /// <summary>
        /// Calcula el hash de un arreglo de bytes y devuelve el resultado.
        /// </summary>
        IResult<string> CalculateHash(byte[] input);

        /// <summary>
        /// Calcula el hash de un StreamReader y devuelve el resultado.
        /// </summary>
        IResult<string> CalculateHash(StreamReader input);

        /// <summary>
        /// Calcula el hash de un SecureString y devuelve el resultado.
        /// </summary>
        IResult<string> CalculateHash(SecureString input);

        /// <summary>
        /// Valida si el hash de una cadena de texto coincide con el hash proporcionado y devuelve el resultado.
        /// </summary>
        IResult HashValidate(string input, string hash);

        /// <summary>
        /// Valida si el hash de un arreglo de bytes coincide con el hash proporcionado y devuelve el resultado.
        /// </summary>
        IResult HashValidate(byte[] input, string hash);

        /// <summary>
        /// Valida si el hash de un SecureString coincide con el hash proporcionado y devuelve el resultado.
        /// </summary>
        IResult HashValidate(SecureString input, string hash);

        /// <summary>
        /// Valida si el hash de un StreamReader coincide con el hash proporcionado y devuelve el resultado.
        /// </summary>
        IResult HashValidate(StreamReader input, string hash);


    }
}
