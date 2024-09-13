using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security
{
    [TestClass]
    public class HashHandlerResultPatternTest
    {
        private Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)> testCasesinputString;
        private Dictionary<string, (byte[] inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)> testCasesInputByte;

        private IResultFactory _resultFactory;
        private Func<byte[], string> _hashformatterBase64;
        private string _input;
        private byte[] _inputByte;
        private string _message;

        private StreamReader _inputstreamReader;
        private SecureString _inputSecureString;

        [TestInitialize]
        public void Setup()
        {
            _input = "Testeo123";
            _message = "Input can not be null";
            _inputByte = Encoding.UTF8.GetBytes(_input);
            _inputstreamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(_input)));
            _inputSecureString = new SecureString();
            _resultFactory = new ResultFactory();

            _hashformatterBase64 = bytes => Convert.ToBase64String(bytes);

            foreach (char character in _input)
            {
                _inputSecureString.AppendChar(character);
            }
            _inputSecureString.MakeReadOnly();
        }

        [TestMethod]
        public void CalculateHash_InputString_ReturnsExpectedHash()
        {
            testCasesinputString = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new (_input,MD5.Create(), true, "320dee96d097dda6f108c62983def31f") },
                    {"SHA256_1", new (_input,SHA256.Create(), true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    {"SHA1_1", new (_input,SHA1.Create(), true, "6e17ffc27e415630eaa5e5297da569573267cd11")},
                    {"SHA384_1", new (_input,SHA384.Create(), true, "f4c32eff1d108679dd2149c2d48babf350db0be0e0ed08ccc80fc5b037df52f550fd1eb76d3ae3024d1957271ac8d6a1")},
                    {"SHA512_1", new (_input,SHA512.Create(), true, "aeaca907a9bce24dbf9762049b6afddd6ac124b2720d2a91c3317500c8691442a98230f674bc58b5da4553a510e3eced7141dadc5eb8226836f524cee0feac66")},
                    {"MD5_Empty", new ("", MD5.Create(), true, "d41d8cd98f00b204e9800998ecf8427e")},
                    {"SHA256_Null", new (null, SHA256.Create(), false, _message)},
            };

            foreach (var testCase in testCasesinputString)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                if (testCase.Value.estadoEsperado)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    Assert.AreEqual(testCase.Value.hashEsperado, result.Message);
                }
            }
        }

        [TestMethod]
        public void CalculateHash_InputString_ReturnsExpectedHashBase64()
        {
            testCasesinputString = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new (_input, MD5.Create(), true, "Mg3ultCX3abxCMYpg97zHw==")},
                    {"SHA256_1", new (_input,SHA256.Create(), true, "FnxnXkHgcFkIhyiSR0SAXwbfwyju318ZOd2BQ9bXgiY=")},
                    {"SHA1_1", new (_input,SHA1.Create(), true, "bhf/wn5BVjDqpeUpfaVpVzJnzRE=")},
                    {"SHA384_1", new (_input,SHA384.Create(), true, "9MMu/x0QhnndIUnC1Iur81DbC+Dg7QjMyA/FsDffUvVQ/R63bTrjAk0ZVycayNah")},
                    {"SHA512_1", new (_input,SHA512.Create(), true, "rqypB6m84k2/l2IEm2r93WrBJLJyDSqRwzF1AMhpFEKpgjD2dLxYtdpFU6UQ4+ztcUHa3F64Img29STO4P6sZg==")},
                    {"MD5_Empty", new ("", MD5.Create(), true, "1B2M2Y8AsgTpgAmY7PhCfg==")},
                    {"SHA256_Null", new (null, SHA256.Create(), false, _message)},
            };

            foreach (var testCase in testCasesinputString)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory, _hashformatterBase64);

                var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                if (testCase.Value.estadoEsperado)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    Assert.AreEqual(testCase.Value.hashEsperado, result.Message);
                }
            }
        }

        [TestMethod]
        public void CalculateHash_InputByte_ReturnsExpectedHash()
        {
            testCasesInputByte = new Dictionary<string, (byte[] inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new (_inputByte, MD5.Create(), true, "320dee96d097dda6f108c62983def31f")},
                    {"SHA256_1", new (_inputByte, SHA256.Create(), true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    {"SHA1_1", new (_inputByte, SHA1.Create(), true, "6e17ffc27e415630eaa5e5297da569573267cd11")},
                    {"SHA384_1", new (_inputByte, SHA384.Create(), true, "f4c32eff1d108679dd2149c2d48babf350db0be0e0ed08ccc80fc5b037df52f550fd1eb76d3ae3024d1957271ac8d6a1")},
                    {"SHA512_1", new (_inputByte, SHA512.Create(), true, "aeaca907a9bce24dbf9762049b6afddd6ac124b2720d2a91c3317500c8691442a98230f674bc58b5da4553a510e3eced7141dadc5eb8226836f524cee0feac66")},
                    {"MD5_Empty", new (Array.Empty<byte>(), MD5.Create(), true, "d41d8cd98f00b204e9800998ecf8427e")},
                    {"SHA256_Null", new (null, SHA256.Create(), false, _message)},
            };

            foreach (var testCase in testCasesInputByte)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                if (testCase.Value.estadoEsperado)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    Assert.AreEqual(testCase.Value.hashEsperado, result.Message);
                }
            }
        }

        [TestMethod]
        public void CalculateHash_InputByte_ReturnsExpectedHashBase64()
        {
            testCasesInputByte = new Dictionary<string, (byte[] inputByte, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new (_inputByte, MD5.Create(), true, "Mg3ultCX3abxCMYpg97zHw==")},
                    {"SHA256_1", new (_inputByte,SHA256.Create(), true, "FnxnXkHgcFkIhyiSR0SAXwbfwyju318ZOd2BQ9bXgiY=")},
                    {"SHA1_1", new (_inputByte,SHA1.Create(), true, "bhf/wn5BVjDqpeUpfaVpVzJnzRE=")},
                    {"SHA384_1", new (_inputByte,SHA384.Create(), true, "9MMu/x0QhnndIUnC1Iur81DbC+Dg7QjMyA/FsDffUvVQ/R63bTrjAk0ZVycayNah")},
                    {"SHA512_1", new (_inputByte,SHA512.Create(), true, "rqypB6m84k2/l2IEm2r93WrBJLJyDSqRwzF1AMhpFEKpgjD2dLxYtdpFU6UQ4+ztcUHa3F64Img29STO4P6sZg==")},
                    {"MD5_Empty", new (Array.Empty<byte>(), MD5.Create(), true, "1B2M2Y8AsgTpgAmY7PhCfg==")},
                    {"SHA256_Null", new (null, SHA256.Create(), false,_message)},
            };

            foreach (var testCase in testCasesInputByte)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory, _hashformatterBase64);

                var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                if (testCase.Value.estadoEsperado)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    Assert.AreEqual(testCase.Value.hashEsperado, result.Message);
                }

            }
        }



    }

}



