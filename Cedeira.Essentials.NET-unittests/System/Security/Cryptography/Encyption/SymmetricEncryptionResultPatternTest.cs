using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Enum;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Encyption
{
    [TestClass]
    public class SymmetricEncryptionResultPatternTest
    {

        private Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, string expectedResponse, bool expectedResult)> TestEncryptionsInputString;
        private Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, byte[] expectedResponse, bool expectedResult)> TestEncryptionsInputBytes;


        private string _key_8_bytes;
        private string _key_16_bytes;
        private string _key_24_bytes;
        private string _key_32_bytes;
        private string _iV_16_bytes;
        private string _iV_8_bytes;
        private string _input;
        private byte[] _inputByte;
        private ServiceCollection _serviceCollection;
        private IResultFactory _resultFactory;


        [TestInitialize]
        public void SetUp()
        {
            _input = "Mejor que el codigo Cesar.";
            _inputByte = Encoding.UTF8.GetBytes(_input);
            _key_8_bytes = "12345678";
            _key_16_bytes = "1234567890abcdef";
            _key_24_bytes = "1234567890abcdef12345678";
            _key_32_bytes = "1234567890abcdef1234567890abcdef";
            _iV_16_bytes = "abcdef1234567890";
            _iV_8_bytes = "abcdefgh";
            _serviceCollection = new ServiceCollection();
            _resultFactory = new ResultFactory();
        }

        [TestMethod]
        public void SymmetricEncryptionResulPattern_Input_String_Create()
        {
            TestEncryptionsInputString = new Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, string expectedResponse, bool expectedResult)>
            {
                {"Aes_CBC",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,_input,true) },
                {"Aes_CFB",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,_input,true) },
                {"Aes_ECB",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.ECB,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,_input,true) },
                {"DES_CBC",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.CBC,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_input,true) },
                {"DES_CFB",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.CFB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_input,true) },
                {"DES_ECB",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.ECB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_input,true) },
                {"TRIPLE_DES_CBC",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_input,true) },
                {"TRIPLE_DES_CFB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CFB,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,_input,true) },
                {"TRIPLE_DES_ECB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_input, true) },
                {"Aes_CBC_expected_value_null",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,null,false) },
                {"Aes_CBC_expected_value_empty",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,"",true) },
            };

            foreach (var test in TestEncryptionsInputString)
            {
                _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(test.Value.key, test.Value.iV, test.Value.algorithm, test.Value.cipherMode, test.Value.paddingMode));
                _serviceCollection.AddSingleton(sp => new SymmetricEncryptionResultPatternFactory(sp.GetRequiredService<ISymmetricEncryptionContext>(), _resultFactory).Create());

                var serviceProvider = _serviceCollection.BuildServiceProvider();

                var symmetricEncryptionResultPattern = serviceProvider.GetService<ISymmetricEncryptionResultPattern>();

                var encriptedMessage = symmetricEncryptionResultPattern.Encrypt(test.Value.expectedResponse);

                if (test.Value.expectedResult)
                {
                    Assert.IsNotNull(encriptedMessage);

                    Assert.IsTrue(encriptedMessage.IsSuccess());
                    var decryptedMessage = symmetricEncryptionResultPattern.Decrypt(encriptedMessage.SuccessValue);

                    Assert.IsNotNull(decryptedMessage);

                    Assert.IsTrue(encriptedMessage.IsSuccess());

                    Assert.AreEqual(decryptedMessage.SuccessValue, test.Value.expectedResponse);
                }
                else 
                {
                    Assert.IsTrue(encriptedMessage.IsFailure());

                    var decryptedMessage = symmetricEncryptionResultPattern.Decrypt(test.Value.expectedResponse);

                    Assert.IsTrue(decryptedMessage.IsFailure());
                }
            }
        }

        [TestMethod]
        public void SymmetricEncryptionResulPattern_Input_Byte_Create()
        {
            TestEncryptionsInputBytes = new Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, byte[] expectedResponse, bool expectedResult)>
            {
                {"Aes_CBC",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"Aes_CFB",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"Aes_ECB",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.ECB,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"DES_CBC",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.CBC,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"DES_CFB",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.CFB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"DES_ECB",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.ECB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"TRIPLE_DES_CBC",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"TRIPLE_DES_CFB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CFB,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"TRIPLE_DES_ECB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputByte, true) },
                {"Aes_CBC_expected_value_null",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,null,false) },
            };

            foreach (var test in TestEncryptionsInputBytes)
            {
                _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(test.Value.key, test.Value.iV, test.Value.algorithm, test.Value.cipherMode, test.Value.paddingMode));
                _serviceCollection.AddSingleton(sp => new SymmetricEncryptionResultPatternFactory(sp.GetRequiredService<ISymmetricEncryptionContext>(), _resultFactory).Create());

                var serviceProvider = _serviceCollection.BuildServiceProvider();

                var symmetricEncryptionResultPattern = serviceProvider.GetService<ISymmetricEncryptionResultPattern>();

                var encriptedMessage = symmetricEncryptionResultPattern.Encrypt(test.Value.expectedResponse);

                if (test.Value.expectedResult)
                {
                    Assert.IsNotNull(encriptedMessage);
                    Assert.IsTrue(encriptedMessage.IsSuccess());

                    var decryptedMessage = symmetricEncryptionResultPattern.Decrypt(encriptedMessage.SuccessValue);

                    Assert.IsNotNull(decryptedMessage);
                    Assert.IsTrue(encriptedMessage.IsSuccess());
                    Assert.AreEqual(Convert.ToHexString(decryptedMessage.SuccessValue), Convert.ToHexString(test.Value.expectedResponse));
                }
                else
                {
                    Assert.IsTrue(encriptedMessage.IsFailure());

                    var decryptedMessage = symmetricEncryptionResultPattern.Decrypt(test.Value.expectedResponse);

                    Assert.IsTrue(decryptedMessage.IsFailure());
                }
            }
        }
    }
}
