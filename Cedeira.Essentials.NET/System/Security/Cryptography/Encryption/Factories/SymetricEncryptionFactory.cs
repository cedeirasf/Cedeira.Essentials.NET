using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories
{
    public class SymetricEncryptionFactory : ISymetricEncryptationFactory
    {

        private readonly ISymmetricEncryptionContext _symmetricEncryptionContext;

        public SymetricEncryptionFactory(ISymmetricEncryptionContext symmetricEncryptionContext)
        {
            _symmetricEncryptionContext = symmetricEncryptionContext;       
        }

        public ISymmetricEncryption Create() 
        {
            return new SymetricEncryption(_symmetricEncryptionContext.SymmetricAlgorithm);
        }
    }
}
