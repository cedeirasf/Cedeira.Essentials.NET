using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Interface;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Interface
{
    /// <summary>
    /// Define una interfaz de fábrica para instanciar objetos de resultado 
    /// </summary>
    public interface IHashCedeiraFactory<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Define una interfaz de fábrica para instanciar objetos de tipo HashCedeira
        /// </summary>
        /// <param name="hashContext"></param>
        /// <returns></returns>
        IHashHandler<T> CreateHash();
        IHashHandlerResultPattern<T> CreateHashResultPattern();
    }
}
