using Cedeira.Essentials.NET.System.ResultPattern;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.HashService.Interface
{
    /// <summary>
    /// Interfaz que define los métodos para generar y validar hashes.
    /// </summary>
    public interface IHashCedeira
    {
        /// <summary>
        /// Genera el hash de una cadena de entrada y devuelve el resultado como una cadena en formato hexadecimal.
        /// </summary>
        /// <param name="input">La cadena de entrada que será hasheada.</param>
        /// <returns>El hash de la cadena de entrada en formato hexadecimal.</returns>
        string CalculateHash(string input);

        /// <summary>
        /// Genera el hash de una cadena de entrada y lo escribe en un `Stream` de salida.
        /// </summary>
        /// <param name="input">La cadena de entrada que será hasheada.</param>
        /// <param name="output">El `Stream` de salida donde se escribirá el hash.</param>
        IResult CalculateHash(string input, Stream output);

        /// <summary>
        /// Valida si el hash generado a partir de una cadena de entrada coincide con el hash proporcionado.
        /// </summary>
        /// <param name="input">La cadena de entrada original.</param>
        /// <param name="hash">El hash proporcionado con el que se comparará el hash calculado.</param>
        /// <returns>Un objeto `IResult` que indica si la validación fue exitosa o fallida.</returns>
        IResult HashValidate(string input, string hash);
    }

}
