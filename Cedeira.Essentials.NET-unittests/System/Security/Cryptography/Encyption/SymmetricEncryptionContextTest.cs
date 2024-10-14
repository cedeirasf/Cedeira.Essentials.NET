using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Encyption
{
    /// <summary>
    /// Represents a test class for the SymmetricEncryptionContext functionality.
    /// </summary>
    [TestClass]
    public class SymmetricEncryptionContextTest
    {
        /// <summary>
        /// A dictionary to hold test cases for various symmetric encryption configurations.
        /// </summary>
        private Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, bool expectedResult)> TestEncryptions;

        /// <summary>
        /// Key of 8 bytes.
        /// </summary>
        private string _key_8_bytes;

        /// <summary>
        /// Key of 16 bytes.
        /// </summary>
        private string _key_16_bytes;

        /// <summary>
        /// Key of 24 bytes.
        /// </summary>
        private string _key_24_bytes;

        /// <summary>
        /// Key of 32 bytes.
        /// </summary>
        private string _key_32_bytes;

        /// <summary>
        /// Initialization vector of 16 bytes.
        /// </summary>
        private string _iV_16_bytes;

        /// <summary>
        /// Initialization vector of 8 bytes.
        /// </summary>
        private string _iV_8_bytes;

        /// <summary>
        /// Service collection for dependency injection.
        /// </summary>
        private ServiceCollection _serviceCollection;

        /// <summary>
        /// Test initialization method to set up keys, IVs, and test configurations.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _key_8_bytes = "12345678";
            _key_16_bytes = "1234567890abcdef";
            _key_24_bytes = "1234567890abcdef12345678";
            _key_32_bytes = "1234567890abcdef1234567890abcdef";
            _iV_16_bytes = "abcdef1234567890";
            _iV_8_bytes = "abcdefgh";
            _serviceCollection = new ServiceCollection();

            TestEncryptions = new Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, bool expectedResult)>
        {
            {"Aes_CBC", new(SymmetricAlgorithmTypeEnum.AES, CipherModeTypeEnum.CBC, _key_16_bytes, _iV_16_bytes, PaddingMode.PKCS7, true)},
            {"Aes_CFB", new(SymmetricAlgorithmTypeEnum.AES, CipherModeTypeEnum.CFB, _key_16_bytes, _iV_16_bytes, PaddingMode.PKCS7, true)},
            {"Aes_ECB", new(SymmetricAlgorithmTypeEnum.AES, CipherModeTypeEnum.ECB, _key_32_bytes, _iV_16_bytes, PaddingMode.PKCS7, true)},
            {"DES_CBC", new(SymmetricAlgorithmTypeEnum.DES, CipherModeTypeEnum.CBC, _key_8_bytes, _iV_8_bytes, PaddingMode.PKCS7, true)},
            {"DES_CFB", new(SymmetricAlgorithmTypeEnum.DES, CipherModeTypeEnum.CFB, _key_8_bytes, _iV_8_bytes, PaddingMode.PKCS7, true)},
            {"DES_ECB", new(SymmetricAlgorithmTypeEnum.DES, CipherModeTypeEnum.ECB, _key_8_bytes, _iV_8_bytes, PaddingMode.PKCS7, true)},
            {"TRIPLE_DES_CBC", new(SymmetricAlgorithmTypeEnum.TripleDES, CipherModeTypeEnum.CBC, _key_16_bytes, _iV_8_bytes, PaddingMode.PKCS7, true)},
            {"TRIPLE_DES_CFB", new(SymmetricAlgorithmTypeEnum.TripleDES, CipherModeTypeEnum.CFB, _key_24_bytes, _iV_8_bytes, PaddingMode.PKCS7, true)},
            {"TRIPLE_DES_ECB", new(SymmetricAlgorithmTypeEnum.TripleDES, CipherModeTypeEnum.CFB, _key_16_bytes, _iV_8_bytes, PaddingMode.PKCS7, true)},
            {"TRIPLE_DES_GNC_CBC", new(SymmetricAlgorithmTypeEnum.TripleDesGNC, CipherModeTypeEnum.CBC, _key_16_bytes, _iV_8_bytes, PaddingMode.PKCS7, true)},
            {"TRIPLE_DES_GNC_CFB", new(SymmetricAlgorithmTypeEnum.TripleDesGNC, CipherModeTypeEnum.CFB, _key_24_bytes, _iV_8_bytes, PaddingMode.PKCS7, true)},
            {"TRIPLE_DES_GNC_ECB", new(SymmetricAlgorithmTypeEnum.TripleDesGNC, CipherModeTypeEnum.CFB, _key_16_bytes, _iV_8_bytes, PaddingMode.PKCS7, true)},
            {"Aes_CBC_key_null", new(SymmetricAlgorithmTypeEnum.AES, CipherModeTypeEnum.CBC, null, _iV_16_bytes, PaddingMode.PKCS7, false)},
            {"Aes_CBC_Key_IV_null", new(SymmetricAlgorithmTypeEnum.AES, CipherModeTypeEnum.CBC, null, null, PaddingMode.PKCS7, false)},
            {"Aes_CBC_IV_null", new(SymmetricAlgorithmTypeEnum.AES, CipherModeTypeEnum.CBC, _key_16_bytes, null, PaddingMode.PKCS7, false)},
        };
        }

        /// <summary>
        /// Test method to create symmetric encryption context with full configuration.
        /// </summary>
        [TestMethod]
        public void SymmetricEncryptionContex_Create_With_FullConfig()
        {
            foreach (var test in TestEncryptions)
            {
                if (!test.Value.expectedResult)
                {
                    if (test.Value.key is null || test.Value.iV is null)
                    {
                        var excep = Assert.ThrowsException<ArgumentNullException>(() =>
                        {
                            _serviceCollection.AddSingleton<ISymmetricEncryptionContext>(
                                SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(
                                    test.Value.key,
                                    test.Value.iV,
                                    test.Value.algorithm,
                                    test.Value.cipherMode,
                                    test.Value.paddingMode));
                        });
                    }
                    else
                    {
                        var excep = Assert.ThrowsException<ArgumentException>(() =>
                        {
                            _serviceCollection.AddSingleton<ISymmetricEncryptionContext>(
                                SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(
                                    test.Value.key,
                                    test.Value.iV,
                                    test.Value.algorithm,
                                    test.Value.cipherMode,
                                    test.Value.paddingMode));
                        });
                    }
                }
                else
                {
                    
                    _serviceCollection.AddSingleton<ISymmetricEncryptionContext>(
                        SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(
                            test.Value.key,
                            test.Value.iV,
                            test.Value.algorithm,
                            test.Value.cipherMode,
                            test.Value.paddingMode));

                   
                    _serviceCollection.AddSingleton<IOptions<ISymmetricEncryptionContext>>(sp =>
                        new OptionsWrapper<ISymmetricEncryptionContext>(
                            sp.GetRequiredService<ISymmetricEncryptionContext>()));

               
                    var serviceProvider = _serviceCollection.BuildServiceProvider();
                    var symmetricEncryptionContext = serviceProvider.GetService<ISymmetricEncryptionContext>();
                    var optionsSymmetricEncryptionContext = serviceProvider.GetService<IOptions<ISymmetricEncryptionContext>>();

                 
                    Assert.IsNotNull(symmetricEncryptionContext);
                    Assert.IsNotNull(optionsSymmetricEncryptionContext);
                    Assert.AreEqual(symmetricEncryptionContext, optionsSymmetricEncryptionContext.Value);
                }
            }
        }

        /// <summary>
        /// Test method to create symmetric encryption context with default configuration.
        /// </summary>
        [TestMethod]
        public void SymmetricEncryptionContex_Create()
        {
            _serviceCollection.AddSingleton<ISymmetricEncryptionContext>(SymmetricEncryptionContext.Create());
            _serviceCollection.AddSingleton<IOptions<ISymmetricEncryptionContext>>(sp =>
                new OptionsWrapper<ISymmetricEncryptionContext>(
                    sp.GetRequiredService<ISymmetricEncryptionContext>()));
           
            var serviceProvider = _serviceCollection.BuildServiceProvider();
            var symmetricEncryptionContext = serviceProvider.GetService<ISymmetricEncryptionContext>();
            var optionsSymmetricEncryptionContext = serviceProvider.GetService<IOptions<ISymmetricEncryptionContext>>();

            Assert.IsNotNull(symmetricEncryptionContext);
            Assert.IsNotNull(optionsSymmetricEncryptionContext);
            Assert.AreEqual(symmetricEncryptionContext, optionsSymmetricEncryptionContext.Value);
        }

        /// <summary>
        /// Test method to create symmetric encryption context from algorithm configuration.
        /// </summary>
        [TestMethod]
        public void SymmetricEncryptionContex_CreateFromAlgorithmConfig()
        {
            foreach (var test in TestEncryptions.Where(x => x.Value.expectedResult != false).ToDictionary())
            {
                _serviceCollection.AddSingleton<ISymmetricEncryptionContext>(
                    SymmetricEncryptionContext.CreateFromAlgorithmConfig(
                        test.Value.algorithm,
                        test.Value.cipherMode,
                        test.Value.paddingMode));
                
                _serviceCollection.AddSingleton<IOptions<ISymmetricEncryptionContext>>(sp =>
                    new OptionsWrapper<ISymmetricEncryptionContext>(
                        sp.GetRequiredService<ISymmetricEncryptionContext>()));

                var serviceProvider = _serviceCollection.BuildServiceProvider();
                var symmetricEncryptionContext = serviceProvider.GetService<ISymmetricEncryptionContext>();
                var optionsSymmetricEncryptionContext = serviceProvider.GetService<IOptions<ISymmetricEncryptionContext>>();

                Assert.IsNotNull(symmetricEncryptionContext);
                Assert.IsNotNull(optionsSymmetricEncryptionContext);
                Assert.AreEqual(symmetricEncryptionContext, optionsSymmetricEncryptionContext.Value);
            }
        }
    }

}
