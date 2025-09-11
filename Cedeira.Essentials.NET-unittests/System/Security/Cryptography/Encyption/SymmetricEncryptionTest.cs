using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Factories;
using Microsoft.Extensions.DependencyInjection;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Encyption
{
    [TestClass]
    public class SymmetricEncryptionTest
    {
        private Dictionary<string, (string? value, string? cipherValue)> _TestValidateEncryptionString;

        private Dictionary<string, (byte[]? value, byte[]? cipherValue)> _TestValidateEncryptionArrayByte;

        private Dictionary<string, (SecureString? value, SecureString? cipherValue)> _TestValidateEncryptionSecureString;

        private Dictionary<string, (StreamReader? value, StreamReader? cipherValue)> _TestValidateEncryptionStreamReader;

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

        private StreamReader _inputStreamReaderFake;

        private string _inputFake;

        private byte[] _inputByteFake;

        private SecureString _inputSecureStringFake;

        /// <summary>
        /// Service collection for dependency injection.
        /// </summary>
        private ServiceCollection _serviceCollection;

        [TestInitialize]
        public void SepUp()
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

            _inputStreamReaderFake = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes("Fake.")));
            _inputStreamReaderFake.BaseStream.Position = 0;
            _inputFake = " Calle falsa 123";
            _inputByteFake = Encoding.UTF8.GetBytes(_inputFake);

            _inputSecureStringFake = new SecureString();

            foreach (char character in _inputFake)
            {
                _inputSecureStringFake.AppendChar(character);
            }
            _inputSecureStringFake.MakeReadOnly();

            _serviceCollection = new ServiceCollection();
        }

        /// <summary>
        /// Test method to create symmetric encryption context with full configuration and test encrypt and decrypt methos wiht a StreamReader input
        /// </summary>
        [TestMethod]
        public void Validate_SymmetricEncryption_Input_StreamReder_Create()
        {
            _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.Create());
            _serviceCollection.AddSingleton(sp => new SymmetricEncryptionFactory(sp.GetRequiredService<ISymmetricEncryptionContext>()).Create());

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var symmetricEncryption = serviceProvider.GetService<ISymmetricEncryption>();

            var encriptedMessage = symmetricEncryption.Encrypt(_inputStreamReader);

            var otherEncriptedMessage = symmetricEncryption.Encrypt(_inputStreamReaderFake);

            _TestValidateEncryptionStreamReader = new Dictionary<string, (StreamReader? value, StreamReader? cipherValue)>
            {
                {"Value_null_cipherValue_ok",new (null,encriptedMessage)},
                {"Value_ok_cipherValue_null",new (null,encriptedMessage)},
                {"Value_ok_cipherValue_wrong",new (_inputStreamReader, otherEncriptedMessage)}
            };

            foreach (var test in _TestValidateEncryptionStreamReader)
            {
                if (test.Value.value != null && test.Value.cipherValue != null)
                {
                    Assert.ThrowsException<CryptographicException>(() =>
                    {
                        symmetricEncryption.ThrowIfInvalidEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
                else
                {
                    Assert.ThrowsException<ArgumentNullException>(() =>
                    {
                        var result = symmetricEncryption.ValidateEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
            }
        }

        /// <summary>
        /// Test method to create symmetric encryption context with full configuration and test encrypt and decrypt methos wiht a string input
        /// </summary>
        [TestMethod]
        public void Validate_SymmetricEncryption_Input_String_Create()
        {
            _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.Create());
            _serviceCollection.AddSingleton(sp => new SymmetricEncryptionFactory(sp.GetRequiredService<ISymmetricEncryptionContext>()).Create());

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var symmetricEncryption = serviceProvider.GetService<ISymmetricEncryption>();

            var encriptedMessage = symmetricEncryption.Encrypt(_input);

            var otherEncriptedMessage = symmetricEncryption.Encrypt(_inputFake);

            _TestValidateEncryptionString = new Dictionary<string, (string? value, string? cipherValue)>
            {
                {"Value_null_cipherValue_ok",new (null,encriptedMessage)},
                {"Value_ok_cipherValue_null",new (null,encriptedMessage)},
                {"Value_ok_cipherValue_wrong",new (_input, otherEncriptedMessage)}
            };

            foreach (var test in _TestValidateEncryptionString)
            {
                if (test.Value.value != null && test.Value.cipherValue != null)
                {
                    Assert.ThrowsException<CryptographicException>(() =>
                    {
                        symmetricEncryption.ThrowIfInvalidEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
                else
                {
                    Assert.ThrowsException<ArgumentNullException>(() =>
                    {
                        var result = symmetricEncryption.ValidateEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
            }
        }

        /// <summary>
        /// Test method to create symmetric encryption context with full configuration and test encrypt and decrypt methos wiht a string input
        /// </summary>
        [TestMethod]
        public void Validate_SymmetricEncryption_Input_Array_Byte_Create()
        {
            _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.Create());
            _serviceCollection.AddSingleton(sp => new SymmetricEncryptionFactory(sp.GetRequiredService<ISymmetricEncryptionContext>()).Create());

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var symmetricEncryption = serviceProvider.GetService<ISymmetricEncryption>();

            var encriptedMessage = symmetricEncryption.Encrypt(_inputByte);

            var otherEncriptedMessage = symmetricEncryption.Encrypt(_inputByteFake);

            _TestValidateEncryptionArrayByte = new Dictionary<string, (byte[]? value, byte[]? cipherValue)>
            {
                {"Value_null_cipherValue_ok",new (null,encriptedMessage)},
                {"Value_ok_cipherValue_null",new (null,encriptedMessage)},
                {"Value_ok_cipherValue_wrong",new (_inputByte, otherEncriptedMessage)}
            };

            foreach (var test in _TestValidateEncryptionArrayByte)
            {
                if (test.Value.value != null && test.Value.cipherValue != null)
                {
                    Assert.ThrowsException<CryptographicException>(() =>
                    {
                        symmetricEncryption.ThrowIfInvalidEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
                else
                {
                    Assert.ThrowsException<ArgumentNullException>(() =>
                    {
                        var result = symmetricEncryption.ValidateEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
            }
        }

        /// <summary>
        /// Test method to create symmetric encryption context with full configuration and test encrypt and decrypt methos wiht a SecureString input
        /// </summary>
        [TestMethod]
        public void Validate_SymmetricEncryption_Input_SecureString_Create()
        {
            _serviceCollection.AddSingleton((ISymmetricEncryptionContext)SymmetricEncryptionContext.Create());
            _serviceCollection.AddSingleton(sp => new SymmetricEncryptionFactory(sp.GetRequiredService<ISymmetricEncryptionContext>()).Create());

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var symmetricEncryption = serviceProvider.GetService<ISymmetricEncryption>();

            var encriptedMessage = symmetricEncryption.Encrypt(_inputSecureString);

            var otherEncriptedMessage = symmetricEncryption.Encrypt(_inputSecureStringFake);

            _TestValidateEncryptionSecureString = new Dictionary<string, (SecureString? value, SecureString? cipherValue)>
            {
                {"Value_null_cipherValue_ok",new (null,encriptedMessage)},
                {"Value_ok_cipherValue_null",new (null,encriptedMessage)},
                {"Value_ok_cipherValue_wrong",new (_inputSecureString, otherEncriptedMessage)}
            };

            foreach (var test in _TestValidateEncryptionSecureString)
            {
                if (test.Value.value != null && test.Value.cipherValue != null)
                {
                    Assert.ThrowsException<CryptographicException>(() =>
                    {
                        symmetricEncryption.ThrowIfInvalidEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
                else
                {
                    Assert.ThrowsException<ArgumentNullException>(() =>
                    {
                        var result = symmetricEncryption.ValidateEncryption(test.Value.value, test.Value.cipherValue);
                    });
                }
            }
        }
    }
}