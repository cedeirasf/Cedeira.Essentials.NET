using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories
{
    public class SymetricEncryptionResultPatternFactory : ISymetricEncryptionResultPatternFactory 
    {
        private readonly ISymmetricEncryptionContext _symetricEncryptionContext; 
        private readonly IResultFactory _resultFactory;     

        public SymetricEncryptionResultPatternFactory(ISymmetricEncryptionContext symmetricEncryptionContext, IResultFactory resultFactory)
        {
            _symetricEncryptionContext = symmetricEncryptionContext;
            _resultFactory = resultFactory;     
        }

        public ISymetricEncryptionResultPattern Create()
        {
            return new SymetricEncryptionResultPttern
        }
    }
}
