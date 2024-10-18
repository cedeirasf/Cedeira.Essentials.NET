using Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Enum;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Encyption
{
    [TestClass]
    public class SymmetricEncryptionResultPatternTest
    {
        /// <summary>
        /// A dictionary to hold test cases for various string expectedResponse.
        /// </summary>
        private Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, string expectedResponse, bool expectedResult)> TestEncryptionsInputString;

        /// <summary>
        /// A dictionary to hold test cases for various Bytes expectedResponse.
        /// </summary>
        private Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, byte[] expectedResponse, bool expectedResult)> TestEncryptionsInputBytes;

        /// <summary>
        /// A dictionary to hold test cases for various SecureString expectedResponse.
        /// </summary>
        private Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, SecureString expectedResponse, bool expectedResult)> TestEncryptionsInputSecureString;

        /// <summary>
        /// A dictionary to hold test cases for various StreamReader expectedResponse.
        /// </summary>
        private Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, StreamReader expectedResponse, bool expectedResult)> TestEncryptionsInputStreamReader;


        private Dictionary<string, (string? value, string? cipherValue, bool expectedResult)> _TestValidateEncryptionString;

        private Dictionary<string, (byte[]? value, byte[]? cipherValue, bool expectedResult)> _TestValidateEncryptionArrayByte;

        private Dictionary<string, (SecureString? value, SecureString? cipherValue, bool expectedResult)> _TestValidateEncryptionSecureString;

        private Dictionary<string, (StreamReader? value, StreamReader? cipherValue, bool expectedResult)> _TestValidateEncryptionStreamReader;

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
        /// Initialization an input string
        /// </summary>
        private string _input;

        /// <summary>
        /// Initialization an input string
        /// </summary>
        private byte[] _inputByte;

        /// <summary>
        /// Initialization an input Byte
        /// </summary>
        private SecureString _inputSecureString;

        /// <summary>
        /// Initialization an input SecureString
        /// </summary>
        private StreamReader _inputStreamReader;

        /// <summary>
        /// Service collection for dependency injection.
        /// </summary>
        private ServiceCollection _serviceCollection;

        /// <summary>
        /// Initialization  IResultFactory
        /// </summary>
        private IResultFactory _resultFactory;


        /// <summary>
        /// Test initialization method to set up keys, IVs, and decyption a encryption methods
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _input = "Mejor que el codigo Cesar.";
            _inputByte = Encoding.UTF8.GetBytes(_input);
            _inputSecureString = new SecureString();

            foreach (char character in _input)
            {
                _inputSecureString.AppendChar(character);
            }
            _inputSecureString.MakeReadOnly();

            _inputStreamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(_input)));
            _inputStreamReader.BaseStream.Position = 0;
            _key_8_bytes = "12345678";
            _key_16_bytes = "1234567890abcdef";
            _key_24_bytes = "1234567890abcdef12345678";
            _key_32_bytes = "1234567890abcdef1234567890abcdef";
            _iV_16_bytes = "abcdef1234567890";
            _iV_8_bytes = "abcdefgh";
            _serviceCollection = new ServiceCollection();
            _resultFactory = new ResultFactory();

        }

        /// <summary>
        /// Test method to create symmetric encryption context with full configuration and test encrypt and decrypt methos wiht a string input
        /// </summary>
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
                {"TRIPLE_DES_GNC_CBC",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_input,true) },
                {"TRIPLE_DES_GNC_CFB",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CFB,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,_input,true) },
                {"TRIPLE_DES_GNC_ECB",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_input, true) },
                {"Aes_CBC_expected_value_null",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,null,false) },
                {"Aes_CBC_expected_value_empty",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,"",true) },
            };

            foreach (var test in TestEncryptionsInputString)
            {
                _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(test.Value.algorithm, test.Value.cipherMode, test.Value.paddingMode, test.Value.key, test.Value.iV));
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

        /// <summary>
        /// Test method to create symmetric encryption context with full configuration and test encrypt and decrypt methos wiht a byte input
        /// </summary>
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
                {"TRIPLE_DES_GNC_CBC",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"TRIPLE_DES_GNC_CFB",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CFB,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputByte,true) },
                {"TRIPLE_DES_GNC_ECB",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputByte, true) },
                {"Aes_CBC_expected_value_null",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,null,false) },
            };

            foreach (var test in TestEncryptionsInputBytes)
            {
                _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(test.Value.algorithm, test.Value.cipherMode, test.Value.paddingMode, test.Value.key, test.Value.iV));
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

        /// <summary>
        /// Test method to create symmetric encryption context with full configuration and test encrypt and decrypt methos wiht a SecureString input
        /// </summary>
        [TestMethod]
        public void SymmetricEncryptionResulPattern_Input_SecureString_Create()
        {
            TestEncryptionsInputSecureString = new Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, SecureString expectedResponse, bool expectedResult)>
            {
                {"Aes_CBC",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"Aes_CFB",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"Aes_ECB",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.ECB,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"DES_CBC",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.CBC,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"DES_CFB",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.CFB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"DES_ECB",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.ECB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"TRIPLE_DES_CBC",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"TRIPLE_DES_CFB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CFB,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"TRIPLE_DES_ECB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputSecureString, true) },
                {"TRIPLE_DES_GNC_CBC",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"TRIPLE_DES_GNC_CFB",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CFB,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputSecureString,true) },
                {"TRIPLE_DES_GNC_ECB",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputSecureString, true) },
                {"Aes_CBC_expected_value_null",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,null,false) },
            };

            foreach (var test in TestEncryptionsInputSecureString)
            {
                _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(test.Value.algorithm, test.Value.cipherMode, test.Value.paddingMode, test.Value.key, test.Value.iV));
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
                    Assert.AreEqual(decryptedMessage.SuccessValue.ToString(), test.Value.expectedResponse.ToString());
                }
                else
                {
                    Assert.IsTrue(encriptedMessage.IsFailure());

                    var decryptedMessage = symmetricEncryptionResultPattern.Decrypt(test.Value.expectedResponse);

                    Assert.IsTrue(decryptedMessage.IsFailure());
                }
            }
        }

        /// <summary>
        /// Test method to create symmetric encryption context with full configuration and test encrypt and decrypt methos wiht a StreamReader input
        /// </summary>
        [TestMethod]
        public void SymmetricEncryptionResulPattern_Input_StreamReader_Create()
        {
            TestEncryptionsInputStreamReader = new Dictionary<string, (SymmetricAlgorithmTypeEnum algorithm, CipherModeTypeEnum cipherMode, string key, string iV, PaddingMode paddingMode, StreamReader expectedResponse, bool expectedResult)>
            {
                {"Aes_CBC",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"Aes_CFB",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_16_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"Aes_ECB",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.ECB,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"DES_CBC",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.CBC,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"DES_CFB",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.CFB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"DES_ECB",new(SymmetricAlgorithmTypeEnum.DES,CipherModeTypeEnum.ECB,_key_8_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"TRIPLE_DES_CBC",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"TRIPLE_DES_CFB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CFB,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"TRIPLE_DES_ECB",new(SymmetricAlgorithmTypeEnum.TripleDES,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputStreamReader, true) },
                {"TRIPLE_DES_GNC_CBC",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CBC,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"TRIPLE_DES_GNC_CFB",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CFB,_key_24_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputStreamReader,true) },
                {"TRIPLE_DES_GNC_ECB",new(SymmetricAlgorithmTypeEnum.TripleDesGNC,CipherModeTypeEnum.CFB,_key_16_bytes,_iV_8_bytes,PaddingMode.PKCS7,_inputStreamReader, true) },
                {"Aes_CBC_expected_value_null",new(SymmetricAlgorithmTypeEnum.AES,CipherModeTypeEnum.CBC,_key_32_bytes,_iV_16_bytes,PaddingMode.PKCS7,null,false) },
            };

            foreach (var test in TestEncryptionsInputStreamReader)
            {

                _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(test.Value.algorithm, test.Value.cipherMode, test.Value.paddingMode, test.Value.key, test.Value.iV));
                _serviceCollection.AddSingleton(sp => new SymmetricEncryptionResultPatternFactory(sp.GetRequiredService<ISymmetricEncryptionContext>(), _resultFactory).Create());

                var serviceProvider = _serviceCollection.BuildServiceProvider();

                var symmetricEncryptionResultPattern = serviceProvider.GetService<ISymmetricEncryptionResultPattern>();

                var encriptedMessage = symmetricEncryptionResultPattern.Encrypt(test.Value.expectedResponse);

                if (test.Value.expectedResult)
                {
                    test.Value.expectedResponse.BaseStream.Position = 0;
                    test.Value.expectedResponse.DiscardBufferedData();

                    var expectedResult = test.Value.expectedResponse.ReadToEnd();

                    Assert.IsNotNull(encriptedMessage);
                    Assert.IsTrue(encriptedMessage.IsSuccess());

                    var decryptedMessage = symmetricEncryptionResultPattern.Decrypt(encriptedMessage.SuccessValue);

                    Assert.IsNotNull(decryptedMessage);

                    var result = decryptedMessage.SuccessValue.ReadToEnd();

                    Assert.IsTrue(encriptedMessage.IsSuccess());
                    Assert.AreEqual(result, expectedResult);
                }
                else
                {
                    Assert.IsTrue(encriptedMessage.IsFailure());

                    var decryptedMessage = symmetricEncryptionResultPattern.Decrypt(test.Value.expectedResponse);

                    Assert.IsTrue(decryptedMessage.IsFailure());
                }
            }
        }


        /// <summary>
        /// Test method to create symmetric encryption context with full configuration and test encrypt and decrypt methos wiht a StreamReader input
        /// </summary>
        [TestMethod]
        public void Validate_SymmetricEncryptionResulPattern_Input_StreamReder_Create()
        {
            _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.Create());
            _serviceCollection.AddSingleton(sp => new SymmetricEncryptionResultPatternFactory(sp.GetRequiredService<ISymmetricEncryptionContext>(), _resultFactory).Create());

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var symmetricEncryptionResultPattern = serviceProvider.GetService<ISymmetricEncryptionResultPattern>();

            var encriptedMessage = symmetricEncryptionResultPattern.Encrypt(_inputStreamReader);

            _TestValidateEncryptionStreamReader = new Dictionary<string, (StreamReader? value, StreamReader? cipherValue, bool expectedResult)>
            {
                {"Value_ok_cipherValue_ok",new (_inputStreamReader,encriptedMessage.SuccessValue,true)},
                {"Value_null_cipherValue_ok",new (null,encriptedMessage.SuccessValue,false)},
                {"Value_ok_cipherValue_null",new (null,encriptedMessage.SuccessValue,false)},
                {"Value_ok_cipherValue_wrong",new (_inputStreamReader,_inputStreamReader,false)}
            };

            foreach (var test in _TestValidateEncryptionStreamReader)
            {
                if (test.Value.expectedResult)
                {
                    test.Value.value.BaseStream.Position = 0;
                    test.Value.value.DiscardBufferedData();

                    var result = symmetricEncryptionResultPattern.ValidateEncryption(test.Value.value, test.Value.cipherValue);


                    Assert.IsTrue(result.IsSuccess());
                }
                else if(test.Value.value is not null || test.Value.cipherValue is not null)
                {
                    Assert.ThrowsException<ArgumentNullException>(() =>
                    {
                        symmetricEncryptionResultPattern.ValidateEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
                else 
                {
                    Assert.ThrowsException<CryptographicException>(() =>
                    {
                        symmetricEncryptionResultPattern.ValidateEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
            }
        }


    }

}




