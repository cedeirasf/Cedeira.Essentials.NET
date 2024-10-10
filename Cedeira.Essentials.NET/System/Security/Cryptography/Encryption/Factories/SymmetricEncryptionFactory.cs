using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories
{
    public class SymmetricEncryptionFactory : ISymmetricEncryptationFactory
    {

        private readonly ISymmetricEncryptionContext _symmetricEncryptionContext;

        public SymmetricEncryptionFactory(ISymmetricEncryptionContext symmetricEncryptionContext)
        {
            _symmetricEncryptionContext = symmetricEncryptionContext;       
        }

        public ISymmetricEncryption Create() 
        {
            return new SymmetricEncryption(_symmetricEncryptionContext.SymmetricAlgorithm);
        }
    }
}
