using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions
{
    public interface IHashHandlerFactory
    {
        IHashHandler CreateHash(IHashContext hashcontext);
        IHashHandler CreateHashWithOutPutFormat(IHashContext hashcontext);
    }
}
