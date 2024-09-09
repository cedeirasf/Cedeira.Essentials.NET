using Cedeira.Essentials.NET.System.ResultPattern;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface
{
    /// <summary>
    /// Define una interfaz para manejar el cálculo y la conversión de hashes en diferentes formatos.
    /// </summary>
    public interface IHashHandler
    {
        /// <summary>
        /// Calcula el hash de una cadena de entrada utilizando el algoritmo de hash configurado y devuelve el resultado en un arreglo de bytes.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser hasheada.</param>
        /// <returns>El hash de la cadena de entrada en formato de bytes.</returns>
        byte[] CalculateHashByte(string input);

        /// <summary>
        /// Calcula el hash de una cadena de entrada utilizando el algoritmo de hash configurado y devuelve el resultado como una cadena en formato hexadecimal.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser hasheada.</param>
        /// <returns>El hash de la cadena de entrada en formato hexadecimal.</returns>
        string CalculateHashHexadecimal(string input);

        /// <summary>
        /// Calcula el hash de una cadena de entrada utilizando el algoritmo de hash configurado y devuelve el resultado como un <see cref="Stream"/>.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser hasheada.</param>
        /// <returns>Un <see cref="Stream"/> que contiene el hash de la cadena de entrada.</returns>
        Stream CalculateHashStream(string input);

        /// <summary>
        /// Calcula el hash de una cadena de entrada utilizando el algoritmo de hash configurado y devuelve el resultado en formato Base64.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser hasheada.</param>
        /// <returns>El hash de la cadena de entrada en formato Base64.</returns>
        string CalculateHashBase64(string input);

        /// <summary>
        /// Calcula el hash de una cadena de entrada y lo escribe en un <see cref="Stream"/> de salida.
        /// Se valida la consistencia del <see cref="Stream"/> antes de proceder con la escritura.
        /// </summary>
        /// <param name="input">La cadena de entrada que será hasheada.</param>
        /// <param name="output">El <see cref="Stream"/> de salida donde se escribirá el hash.</param>
        /// <exception cref="ArgumentException">Lanza una excepción si el <see cref="Stream"/> es null o no está en modo escritura.</exception>
        void CalculateHash(string input, Stream output);

        /// <summary>
        /// Valida si una cadena de hash proporcionada coincide con el hash calculado de una cadena de entrada.
        /// </summary>
        /// <param name="input">La cadena de entrada a ser validada.</param>
        /// <param name="hash">El hash con el que se comparará el hash calculado.</param>
        /// <returns>Un valor booleano que indica si el hash calculado coincide con el hash proporcionado.</returns>
        bool HashValidate(string input, string hash);
    }
}
