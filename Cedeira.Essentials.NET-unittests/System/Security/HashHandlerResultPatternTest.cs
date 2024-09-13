using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security
{
    [TestClass]
    public class HashHandlerResultPatternTest
    {
        private Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)> testCases;
        private Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado, Func<byte[], string> hashformatter)> testCasesWithFunc;

        private Dictionary<string, (byte[] inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)> testCasesInputByte;
        private Dictionary<string, (byte[] inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado, Func<byte[], string> hashformatter)> testCasesInputByteWithFunc;

        private IResultFactory _resultFactory;


        [TestInitialize]
        public void Setup()
        {
            _resultFactory = new ResultFactory();

            string input = "Testeo123";
            byte[] inputByte = Encoding.UTF8.GetBytes("Testeo123");
            string messageException = "Input can not be null";


            testCases = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new ( input,MD5.Create(), true, "320dee96d097dda6f108c62983def31f") },
                    {"SHA256_1", new (input,SHA256.Create(), true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    {"SHA1_1", new (input,SHA1.Create(), true, "6e17ffc27e415630eaa5e5297da569573267cd11")},
                    {"SHA384_1", new (input,SHA384.Create(), true, "f4c32eff1d108679dd2149c2d48babf350db0be0e0ed08ccc80fc5b037df52f550fd1eb76d3ae3024d1957271ac8d6a1")},
                    {"SHA512_1", new (input,SHA512.Create(), true, "aeaca907a9bce24dbf9762049b6afddd6ac124b2720d2a91c3317500c8691442a98230f674bc58b5da4553a510e3eced7141dadc5eb8226836f524cee0feac66")},
                    {"MD5_Empty", new ("", MD5.Create(), true, "d41d8cd98f00b204e9800998ecf8427e")},
                    {"SHA256_Null", new (null, SHA256.Create(), false, messageException)},
            };

            testCasesWithFunc = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado, Func<byte[], string> hashformatter)>
            {
                    {"MD5_1", new (input, MD5.Create(), true, "Mg3ultCX3abxCMYpg97zHw==", bytes => Convert.ToBase64String(bytes))},
                    {"SHA256_1", new (input,SHA256.Create(), true, "FnxnXkHgcFkIhyiSR0SAXwbfwyju318ZOd2BQ9bXgiY=", bytes => Convert.ToBase64String(bytes))},
                    {"SHA1_1", new (input,SHA1.Create(), true, "bhf/wn5BVjDqpeUpfaVpVzJnzRE=", bytes => Convert.ToBase64String(bytes))},
                    {"SHA384_1", new (input,SHA384.Create(), true, "9MMu/x0QhnndIUnC1Iur81DbC+Dg7QjMyA/FsDffUvVQ/R63bTrjAk0ZVycayNah",bytes => Convert.ToBase64String(bytes))},
                    {"SHA512_1", new (input,SHA512.Create(), true, "rqypB6m84k2/l2IEm2r93WrBJLJyDSqRwzF1AMhpFEKpgjD2dLxYtdpFU6UQ4+ztcUHa3F64Img29STO4P6sZg==",bytes => Convert.ToBase64String(bytes))},
                    {"MD5_Empty", new ("", MD5.Create(), true, "1B2M2Y8AsgTpgAmY7PhCfg==",bytes => Convert.ToBase64String(bytes))},
                    {"SHA256_Null", new (null, SHA256.Create(), false, messageException,bytes => Convert.ToBase64String(bytes))},
            };

            testCasesInputByte = new Dictionary<string, (byte[] inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new (inputByte, MD5.Create(), true, "320dee96d097dda6f108c62983def31f")},
                    {"SHA256_1", new (inputByte, SHA256.Create(), true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    {"SHA1_1", new (inputByte, SHA1.Create(), true, "6e17ffc27e415630eaa5e5297da569573267cd11")},
                    {"SHA384_1", new (inputByte, SHA384.Create(), true, "f4c32eff1d108679dd2149c2d48babf350db0be0e0ed08ccc80fc5b037df52f550fd1eb76d3ae3024d1957271ac8d6a1")},
                    {"SHA512_1", new (inputByte, SHA512.Create(), true, "aeaca907a9bce24dbf9762049b6afddd6ac124b2720d2a91c3317500c8691442a98230f674bc58b5da4553a510e3eced7141dadc5eb8226836f524cee0feac66")},
                    {"MD5_Empty", new (Array.Empty<byte>(), MD5.Create(), true, "d41d8cd98f00b204e9800998ecf8427e")},
                    {"SHA256_Null", new (null, SHA256.Create(), false, messageException)},
            };

            testCasesInputByteWithFunc = new Dictionary<string, (byte[] inputByte, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado, Func<byte[], string> hashformatter)>
            {
                    {"MD5_1", new (inputByte, MD5.Create(), true, "Mg3ultCX3abxCMYpg97zHw==", bytes => Convert.ToBase64String(bytes))},
                    {"SHA256_1", new (inputByte,SHA256.Create(), true, "FnxnXkHgcFkIhyiSR0SAXwbfwyju318ZOd2BQ9bXgiY=", bytes => Convert.ToBase64String(bytes))},
                    {"SHA1_1", new (inputByte,SHA1.Create(), true, "bhf/wn5BVjDqpeUpfaVpVzJnzRE=", bytes => Convert.ToBase64String(bytes))},
                    {"SHA384_1", new (inputByte,SHA384.Create(), true, "9MMu/x0QhnndIUnC1Iur81DbC+Dg7QjMyA/FsDffUvVQ/R63bTrjAk0ZVycayNah",bytes => Convert.ToBase64String(bytes))},
                    {"SHA512_1", new (inputByte,SHA512.Create(), true, "rqypB6m84k2/l2IEm2r93WrBJLJyDSqRwzF1AMhpFEKpgjD2dLxYtdpFU6UQ4+ztcUHa3F64Img29STO4P6sZg==",bytes => Convert.ToBase64String(bytes))},
                    {"MD5_Empty", new (Array.Empty<byte>(), MD5.Create(), true, "1B2M2Y8AsgTpgAmY7PhCfg==",bytes => Convert.ToBase64String(bytes))},
                    {"SHA256_Null", new (null, SHA256.Create(), false,messageException,bytes => Convert.ToBase64String(bytes))},
            };

        }

        [TestMethod]
        public void CalculateHash_InputString_ReturnsExpectedHash()
        {
            foreach (var testCase in testCases)
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
        public void CalculateHash_InputString_ReturnsExpectedHashWithFunc()
        {
            foreach (var testCase in testCasesWithFunc)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory, testCase.Value.hashformatter);

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
        public void CalculateHash_InputByte_ReturnsExpectedHashWithFunc()
        {
            foreach (var testCase in testCasesInputByteWithFunc)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory, testCase.Value.hashformatter);

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



