using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Encyption.Factories
{
    [TestClass]
    public class SymmetricEncryptionFactoryTest
    {
        private ServiceCollection _serviceCollection;

        [TestInitialize]
        public void SetUp()
        {
            _serviceCollection = new ServiceCollection();
        }

        [TestMethod]
        public void SymmetricEncryption_Create()
        {
            _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.Create());

            _serviceCollection.AddSingleton(sp => new SymmetricEncryptionFactory(sp.GetRequiredService<ISymmetricEncryptionContext>()).Create());

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var symmetricEncryption = serviceProvider.GetService<ISymmetricEncryption>();

            Assert.IsNotNull(symmetricEncryption);
        }
    }
}
