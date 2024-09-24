using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions
{
    public interface IHashHandlerResultPatternFactory
    {
        IHashHandlerResultPattern CreateHash(IHashContext hashcontext);
        IHashHandlerResultPattern CreateHashWithOutPutFormat(IHashContext hashcontext);
    }
}
