using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Encyption.Factories
{
    /// <summary>
    /// Represents a test class for the SymmetricEncryptionResultPatternFactory functionality.
    /// </summary>
    [TestClass]
    public class SymmetricEncryptionResultPatternFactoryTest
    {
        /// <summary>
        /// Service collection for dependency injection.
        /// </summary>
        private ServiceCollection _serviceCollection;

        /// <summary>
        /// Factory for creating result patterns.
        /// </summary>
        private IResultFactory _resultFactory;

        /// <summary>
        /// Test initialization method to set up the service collection.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _serviceCollection = new ServiceCollection();
        }

        /// <summary>
        /// Test method to create a SymmetricEncryptionResulPattern.
        /// </summary>
        [TestMethod]
        public void SymmetricEncryptionResulPattern_Create()
        {
            _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.Create());

            _serviceCollection.AddSingleton(sp =>
                new SymmetricEncryptionResultPatternFactory(sp.GetRequiredService<ISymmetricEncryptionContext>(), _resultFactory).Create());

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var symmetricEncryptionResultPattern = serviceProvider.GetService<ISymmetricEncryptionResultPattern>();

            Assert.IsNotNull(symmetricEncryptionResultPattern);
        }
    }
}