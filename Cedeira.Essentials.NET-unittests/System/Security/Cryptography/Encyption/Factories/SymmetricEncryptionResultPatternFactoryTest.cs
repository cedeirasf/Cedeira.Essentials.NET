using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Encyption.Factories
{
    [TestClass]
    public class SymmetricEncryptionResultPatternFactoryTest
    {
        private ServiceCollection _serviceCollection;
        private IResultFactory _resultFactory;

        [TestInitialize]
        public void SetUp()
        {
            _serviceCollection = new ServiceCollection();
        }

        [TestMethod]
        public void SymmetricEncryptionResulPattern_Create()
        {
            _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.Create());

            _serviceCollection.AddSingleton(sp => new SymmetricEncryptionResultPatternFactory(sp.GetRequiredService<ISymmetricEncryptionContext>(), _resultFactory).Create());

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var symmetricEncryptionResultPattern = serviceProvider.GetService<ISymmetricEncryptionResultPattern>();

            Assert.IsNotNull(symmetricEncryptionResultPattern);
        }

    }
}
