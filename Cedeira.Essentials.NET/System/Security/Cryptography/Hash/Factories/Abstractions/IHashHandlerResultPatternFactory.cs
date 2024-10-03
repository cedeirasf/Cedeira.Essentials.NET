using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions
{
    /// <summary>
    /// Defines a factory interface for creating instances of IHashHandlerResultPattern.Any response includes a IResult
    /// </summary>
    public interface IHashHandlerResultPatternFactory
    {
        /// <summary>
        /// Creates an instance of IHashHandlerResultPattern with default settings.
        /// </summary>
        /// <returns>An instance of IHashHandlerResultPattern.</returns>
        IHashHandlerResultPattern CreateHash();
    }
}
