using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Hash
{
    [TestClass]
    public class HashContextTest
    {
        private IHashContext _hashContext;
        private HashAlgorithmName _hashAlgorithmName;
        private Func<byte[], string>? _expectedFormatter;

        [TestInitialize]
        public void Setup()
        {
            _hashAlgorithmName =  HashAlgorithmName.SHA256;
            _expectedFormatter = bytes => BitConverter.ToString(bytes);
            _hashContext = HashContext.Create(_hashAlgorithmName, _expectedFormatter);
        }

        [TestMethod]
        public void HashContext_Create_SetsAlgorithmNameAndFormatter()
        {




            // Assert
            Assert.AreEqual(_hashAlgorithmName, _hashContext.HashConfig.AlgorithmName);
            Assert.IsNotNull(_hashContext.HashConfig.HashFormatter);
            Assert.AreEqual(_expectedFormatter, _hashContext.HashConfig.HashFormatter);
        }

    }
}
