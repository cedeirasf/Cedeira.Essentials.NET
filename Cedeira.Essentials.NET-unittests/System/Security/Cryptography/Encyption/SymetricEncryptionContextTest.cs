using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Encyption
{
    [TestClass]
    public class SymetricEncryptionContextTest
    {
        private Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherMode cipherMode, string key, string iV, PaddingMode paddingMode, bool expectedResult)> TestEncryptions;
        private string _input;
        private string _key_8_bytes;
        private string _key_16_bytes;
        private string _key_24_bytes;
        private string _key_32_bytes;
        private string _iV_16_bytes;
        private string _iV_8_bytes;
        private ServiceCollection _serviceCollection;

        [TestInitialize]
        public void SetUp()
        {
            _input = "The password is really safe.";
            _key_8_bytes = "12345678";
            _key_16_bytes = "1234567890abcdef";
            _key_24_bytes = "1234567890abcdef12345678";
            _key_32_bytes = "1234567890abcdef1234567890abcdef";
            _iV_16_bytes = "abcdef1234567890";
            _iV_8_bytes = "abcdefgh";
            _serviceCollection = new ServiceCollection();   

        }

        [TestMethod]
        public void HashContext_Create_SymetricEncryptionContex_With_FullConfig()
        {
            TestEncryptions = new Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherMode ciphermode, string key, string iV, PaddingMode paddingMode, bool expectedResult)>
            {
                {"Aes_CBC",new(SymmetricAlgorithmTypeEnum.AES,CipherMode.CBC,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,true) },
                {"Aes_CFB",new(SymmetricAlgorithmTypeEnum.AES,CipherMode.CFB,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,true) },
                {"Aes_ECB",new(SymmetricAlgorithmTypeEnum.AES,CipherMode.ECB,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,true) },
                //{"Aes_CTS",new(SymmetricAlgorithmTypeEnum.AES,CipherMode.CTS,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,true) },
                {"DES_CBC",new(SymmetricAlgorithmTypeEnum.DES,CipherMode.CBC,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,true) },
                {"DES_CFB",new(SymmetricAlgorithmTypeEnum.DES,CipherMode.CFB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,true) },
                {"DES_ECB",new(SymmetricAlgorithmTypeEnum.DES,CipherMode.ECB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,true) },
                //{"DES_CTS",new(SymmetricAlgorithmTypeEnum.DES,CipherMode.CTS,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,true) },
                {"TRIPLE_DES_CBC",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherMode.CBC,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,true) },
                {"TRIPLE_DES_CFB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherMode.CFB,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,true) },
                {"TRIPLE_DES_ECB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherMode.ECB,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,true) },
                //{"TRIPLE_DES_DES_CTS",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherMode.CTS,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,true) },
            };


            foreach (var test in TestEncryptions)
            {
                _serviceCollection.AddSingleton<ISymmetricEncryptionContext>(SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(test.Value.key, test.Value.iV,test.Value.algorithm,test.Value.cipherMode, test.Value.paddingMode));

                _serviceCollection.AddSingleton<IOptions<ISymmetricEncryptionContext>>(sp => new OptionsWrapper<ISymmetricEncryptionContext>(sp.GetRequiredService<ISymmetricEncryptionContext>()));

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
