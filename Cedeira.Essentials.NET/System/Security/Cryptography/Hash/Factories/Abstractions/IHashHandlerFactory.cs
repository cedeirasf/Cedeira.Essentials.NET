using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions
{
    /// <summary>
    /// Define una interfaz de fábrica para instanciar objetos de resultado 
    /// </summary>
    public interface IHashHandlerFactory
    {
        /// <summary>
        /// Define una interfaz de fábrica para instanciar objetos de tipo HashCedeira
        /// </summary>
        /// <param name="hashContext"></param>
        /// <returns></returns>
        IHashHandler CreateHash(IHashContext hashcontext);
        IHashHandler CreateHashWithFormat(IHashContext hashcontext);
        IHashHandlerResultPattern CreateHashResultPattern(IHashContext hashcontext);
        IHashHandlerResultPattern CreateHashResultPatternWithFormat(IHashContext hashcontext);
    }
}
