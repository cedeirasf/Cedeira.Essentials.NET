﻿using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Hash.Factories
{
    [TestClass]
    public class HashHandlerResultPatternFactoryTest 
    {
        private IHashContext _hashContext;
        private IResultFactory _resultFactory;
        private Dictionary<string, (string inputName, string algorithmName, Func<byte[], string>? hashformatter, bool expectedState, string expectedHash)> _TestHashResultPatternFactoryInputString;
        private Dictionary<string, (byte[] inputByte, string algorithmName, Func<byte[], string>? hashformatter, bool expectedState, string expectedHash)> _TestHashResultPatternFactoryInputByte;
        private Func<byte[], string>? _expectedFormatter;
        private ServiceCollection _service;
        private string _input;
        private byte[] _inputByte;

        [TestInitialize]
        public void SetUp()
        {
            _expectedFormatter = bytes => BitConverter.ToString(bytes);
            _input = "Testeo123";
            _inputByte = Encoding.UTF8.GetBytes(_input);
            _resultFactory = new ResultFactory();
            _service = new ServiceCollection();
        }

        [TestMethod]
        public void HasHandler_Create_SetWithIhashContext_InputString()
        {
            _TestHashResultPatternFactoryInputString = new Dictionary<string, (string inputName, string algorithmName, Func<byte[], string>? hashformatter, bool expectedState, string expectedHash)>
            {
                 {"MD5_1", new ( _input, "MD5",_expectedFormatter, true, "320DEE96D097DDA6F108C62983DEF31F") },
                 {"SHA512_2_null_Format", new ( _input, "SHA512",null, true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
            };

            foreach (var test in _TestHashResultPatternFactoryInputString)
            {
                _service.AddSingleton(_hashContext = HashContext.Create(test.Value.algorithmName, test.Value.hashformatter));
                _service.AddSingleton(sp => new HashHandlerResultPatternFactory(sp.GetRequiredService<IHashContext>(), _resultFactory).CreateHash());

                var serviceProvider = _service.BuildServiceProvider();

                var hashHandler = serviceProvider.GetService<IHashHandlerResultPattern>();

                Assert.IsNotNull(hashHandler);

                var hash = hashHandler.CalculateHash(test.Value.inputName);

                Assert.AreEqual(hash.SuccessValue, test.Value.expectedHash);
            }
        }

        [TestMethod]
        public void HasHandler_Create_SetWithIhashContext_InputByte_ReturnBase64String()
        {
            _TestHashResultPatternFactoryInputByte = new Dictionary<string, (byte[] inputByte, string algorithmName, Func<byte[], string>? hashformatter, bool expectedState, string expectedHash)>
            {
                 {"MD5_1", new ( _inputByte, "MD5",bytes =>Convert.ToBase64String(bytes), true, "Mg3ultCX3abxCMYpg97zHw==") },
                 {"SHA512_2_null_Format", new ( _inputByte,"SHA512",bytes =>Convert.ToBase64String(bytes), true, "rqypB6m84k2/l2IEm2r93WrBJLJyDSqRwzF1AMhpFEKpgjD2dLxYtdpFU6UQ4+ztcUHa3F64Img29STO4P6sZg==")},
            };

            foreach (var test in _TestHashResultPatternFactoryInputByte)
            {
                _service.AddSingleton(_hashContext = HashContext.Create(test.Value.algorithmName, test.Value.hashformatter));
                _service.AddSingleton(sp => new HashHandlerResultPatternFactory(sp.GetRequiredService<IHashContext>(), _resultFactory).CreateHashWithOutputFormat());

                var serviceProvider = _service.BuildServiceProvider();

                var hashHandler = serviceProvider.GetService<IHashHandlerResultPattern>();

                Assert.IsNotNull(hashHandler);

                var hash = hashHandler.CalculateHash(test.Value.inputByte);

                Assert.AreEqual(hash.SuccessValue, test.Value.expectedHash);
            }
        }
    }
}
