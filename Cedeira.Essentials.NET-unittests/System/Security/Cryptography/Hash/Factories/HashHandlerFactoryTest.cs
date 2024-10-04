using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Hash.Factories
{
    /// <summary>
    /// Represents a test class for the HashHandlerFactory functionality.
    /// </summary>
    [TestClass]
    public class HashHandlerFactoryTest
    {
        /// <summary>
        /// A dictionary to store test cases with string inputs, algorithm names, hash formatters, expected states, and expected hashes.
        /// </summary>
        private Dictionary<string, (string inputName, string algorithmName, Func<byte[], string> hashformatter, bool expectedState, string expectedHash)> _TestHashFactoryInputString;

        /// <summary>
        /// A dictionary to store test cases with byte array inputs, algorithm names, hash formatters, expected states, and expected hashes.
        private Dictionary<string, (byte[] inputByte, string algorithmName, Func<byte[], string> hashformatter, bool expectedState, string expectedHash)> _TestHashFactoryInputByte;

        /// <summary>
        /// The expected hash hex formatter function.
        /// </summary>
        private Func<byte[], string> _expectedFormatterHexString;

        /// <summary>
        /// The expected hash base64 formatter function.
        /// </summary>
        private Func<byte[], string> _expectedFormatterBase64;

        /// <summary>
        /// The service collection used for dependency injection.
        /// </summary>
        private ServiceCollection _service;

        /// <summary>
        /// The input string used for testing.
        /// </summary>
        private string _input;

        /// <summary>
        /// The byte array representation of the input string used for testing.
        /// </summary>
        private byte[] _inputByte;

        /// <summary>
        /// Initializes the test setup with default values.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _expectedFormatterHexString = bytes => Convert.ToHexString(bytes);
            _expectedFormatterBase64 = bytes => Convert.ToBase64String(bytes);
            _input = "Testeo123";
            _inputByte = Encoding.UTF8.GetBytes(_input);
            _service = new ServiceCollection();
        }

        /// <summary>
        /// Tests that the HashHandler is created and set with IHashContext for string inputs.
        /// </summary>
        [TestMethod]
        public void HasHandler_Create_SetWithIhashContext_InputString()
        {
            _TestHashFactoryInputString = new Dictionary<string, (string inputName, string algorithmName, Func<byte[], string> hashformatter, bool expectedState, string expectedHash)>
            {
                 {"MD5_1", new ( _input, "MD5",_expectedFormatterHexString, true, "320DEE96D097DDA6F108C62983DEF31F") },
                 {"SHA512_2_null_Format", new ( _input, "SHA512",_expectedFormatterHexString, true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
            };

            foreach (var test in _TestHashFactoryInputString)
            {
                _service.AddSingleton((IHashContext)HashContext.CreatFromAlgorithmNameWithFormmatter(test.Value.algorithmName, test.Value.hashformatter));
                _service.AddSingleton(sp => new HashHandlerFactory(sp.GetRequiredService<IHashContext>()).CreateHash());

                var serviceProvider = _service.BuildServiceProvider();

                var hashHandler = serviceProvider.GetService<IHashHandler>();

                Assert.IsNotNull(hashHandler);

                var hash = hashHandler.CalculateHash(test.Value.inputName);

                Assert.AreEqual(hash, test.Value.expectedHash);
            }
        }

        /// <summary>
        /// Tests that the HashHandler is created and set with IHashContext for byte array inputs, returning a Base64 string.
        /// </summary>
        [TestMethod]
        public void HasHandler_Create_SetWithIhashContext_InputByte_ReturnBase64String()
        {
            _TestHashFactoryInputByte = new Dictionary<string, (byte[] inputByte, string algorithmName, Func<byte[], string> hashformatter, bool expectedState, string expectedHash)>
            {
                 {"MD5_1", new ( _inputByte, "MD5",_expectedFormatterBase64, true, "Mg3ultCX3abxCMYpg97zHw==") },
                 {"SHA512_2_null_Format", new ( _inputByte, "SHA512",_expectedFormatterBase64, true, "rqypB6m84k2/l2IEm2r93WrBJLJyDSqRwzF1AMhpFEKpgjD2dLxYtdpFU6UQ4+ztcUHa3F64Img29STO4P6sZg==")},
            };

            foreach (var test in _TestHashFactoryInputByte)
            {
                _service.AddSingleton((IHashContext)HashContext.CreatFromAlgorithmNameWithFormmatter(test.Value.algorithmName, test.Value.hashformatter));
                _service.AddSingleton(sp => new HashHandlerFactory(sp.GetRequiredService<IHashContext>()).CreateHash());

                var serviceProvider = _service.BuildServiceProvider();

                var hashHandler = serviceProvider.GetService<IHashHandler>();

                Assert.IsNotNull(hashHandler);

                var hash = hashHandler.CalculateHash(test.Value.inputByte);

                Assert.AreEqual(hash, test.Value.expectedHash);
            }
        }
    }
}




