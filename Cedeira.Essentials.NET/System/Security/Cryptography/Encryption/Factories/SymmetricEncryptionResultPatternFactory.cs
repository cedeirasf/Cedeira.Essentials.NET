using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories
{
    public class SymmetricEncryptionResultPatternFactory : ISymmetricEncryptionResultPatternFactory 
    {
        private readonly ISymmetricEncryptionContext _symmetricEncryptionContext; 
        private readonly IResultFactory _resultFactory;     

        public SymmetricEncryptionResultPatternFactory(ISymmetricEncryptionContext symmetricEncryptionContext, IResultFactory resultFactory)
        {
            _symmetricEncryptionContext = symmetricEncryptionContext;
            _resultFactory = resultFactory;     
        }

        public ISymmetricEncryptionResultPattern Create()
        {
            return new SymmetricEncryptionResultPattern(_symmetricEncryptionContext.SymmetricAlgorithm, _resultFactory);
        }
    }
}
