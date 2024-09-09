using Cedeira.Essentials.NET.System.ResultPattern.Factories;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Interface
{
    /// <summary>
    /// Define una interfaz de fábrica para instanciar objetos de resultado 
    /// </summary>
    public interface IHashCedeiraFactory
    {
        /// <summary>
        /// Define una interfaz de fábrica para instanciar objetos de tipo HashCedeira
        /// </summary>
        /// <param name="hashContext"></param>
        /// <returns></returns>
        HashHandler CreateHash(HashContext hashContext);
        HashHandlerResult CreateHashResult(HashContext hashContext,IResultFactory resultfactory);
    }
}
