using Cedeira.Essentials.NET.Extensions.System.Security.Cryptography.Encryption;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Enum;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption;
using System.Runtime.Intrinsics.X86;
using System.Security;
using System.Security.Cryptography;
using Cedeira.Essentials.NET.System.Security.Cryptography.Encryption.Abstractions;

namespace Cedeira.Essentials.NET_unittests.Extensions.System.Security.Cryptografy.Encryption
{
    [TestClass]
    public class SecureStringExtensionTest
    {
        SecureString _inputSecureString;
        SymmetricEncryptionContext _symmetricEncryptionContext;

        public const string Key = "a1b2c3d4e5f6g7h8i9j0klmnopqrstuv";
        public const string IV = "1234567890abcdef";

        [TestInitialize]
        public void Initialize()
        {
            _inputSecureString = new SecureString();

            _inputSecureString = _inputSecureString.StringToSecureString("Mejor que el codigo Cesar.");

            _symmetricEncryptionContext = SymmetricEncryptionContext.CreateFromFullAlgorithmConfig(SymmetricAlgorithmTypeEnum.AES, CipherModeTypeEnum.CBC, PaddingMode.PKCS7, Key, IV);
        }

        [TestMethod]
        public void Encrypt_ShouldEncryptSecureString()
        {
            var encryptor = _symmetricEncryptionContext.SymmetricAlgorithm.CreateEncryptor(_symmetricEncryptionContext.SymmetricAlgorithm.Key, _symmetricEncryptionContext.SymmetricAlgorithm.IV);

            var dencryptor = _symmetricEncryptionContext.SymmetricAlgorithm.CreateDecryptor(_symmetricEncryptionContext.SymmetricAlgorithm.Key, _symmetricEncryptionContext.SymmetricAlgorithm.IV);

            var  secureStringEncipted = _inputSecureString.Encrypt(encryptor);

             var result = secureStringEncipted.ValidateEncryption(_inputSecureString.Encrypt(encryptor));

            var resultDecrypted = secureStringEncipted.Decrypt(dencryptor).SecureStringToString();
        }


    }
}
