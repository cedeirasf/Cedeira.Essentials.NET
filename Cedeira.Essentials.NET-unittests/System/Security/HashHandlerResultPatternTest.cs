using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security
{
    [TestClass]
    public class HashHandlerResultPatternTest
    {
        private Dictionary<string, (string inputName, HashAlgorithm algorithm, bool expectedState, string expectedHash)> _testCasesinputString;
        private Dictionary<string, (byte[] inputByte, HashAlgorithm algorithm, bool expectedState, string expectedHash)> _testCasesInputByte;
        private Dictionary<string, (StreamReader inputStream, HashAlgorithm algorithm, bool expectedState, string expectedHash)> _testCasesinputStreamReader;
        private Dictionary<string, (SecureString inputSecureString, HashAlgorithm algorithm, bool expectedState, string expectedHash)> _testCasesInputSecureString;
        private IResultFactory _resultFactory;
        private Func<byte[], string> _hashformatterBase64;
        private string _input;
        private byte[] _inputByte;
        private string _messageValidateNull;
        private string _messageValidate;
        private StreamReader _inputStreamReader;
        private SecureString _inputSecureString;

        [TestInitialize]
        public void Setup()
        {
            _input = "Testeo123";
            _messageValidateNull = "input cannot be null";
            _messageValidate = "Hashes do not match.";
            _inputByte = Encoding.UTF8.GetBytes(_input);
            _inputStreamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(_input)));
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
            _testCasesinputString = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool expectedState, string expectedHash)>
            {
                {"MD5_1", new (_input, MD5.Create(), true, "320DEE96D097DDA6F108C62983DEF31F") },
                {"SHA256_1", new (_input, SHA256.Create(), true, "167C675E41E07059088728924744805F06DFC328EEDF5F1939DD8143D6D78226")},
                {"SHA1_1", new (_input, SHA1.Create(), true, "6E17FFC27E415630EAA5E5297DA569573267CD11")},
                {"SHA384_1", new (_input, SHA384.Create(), true, "F4C32EFF1D108679DD2149C2D48BABF350DB0BE0E0ED08CCC80FC5B037DF52F550FD1EB76D3AE3024D1957271AC8D6A1")},
                {"SHA512_1", new (_input, SHA512.Create(), true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
                {"MD5_Empty", new ("", MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"SHA256_Null", new (null, SHA256.Create(), false, _messageValidateNull)},
            };

            foreach (var testCase in _testCasesinputString)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.expectedHash, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                }
            }
        }

        [TestMethod]
        public void CalculateHash_InputString_ReturnsExpectedHashBase64()
        {
            _testCasesinputString = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new (_input, MD5.Create(), true, "Mg3ultCX3abxCMYpg97zHw==")},
                    {"SHA256_1", new (_input,SHA256.Create(), true, "FnxnXkHgcFkIhyiSR0SAXwbfwyju318ZOd2BQ9bXgiY=")},
                    {"SHA1_1", new (_input,SHA1.Create(), true, "bhf/wn5BVjDqpeUpfaVpVzJnzRE=")},
                    {"SHA384_1", new (_input,SHA384.Create(), true, "9MMu/x0QhnndIUnC1Iur81DbC+Dg7QjMyA/FsDffUvVQ/R63bTrjAk0ZVycayNah")},
                    {"SHA512_1", new (_input,SHA512.Create(), true, "rqypB6m84k2/l2IEm2r93WrBJLJyDSqRwzF1AMhpFEKpgjD2dLxYtdpFU6UQ4+ztcUHa3F64Img29STO4P6sZg==")},
                    {"MD5_Empty", new ("", MD5.Create(), true, "1B2M2Y8AsgTpgAmY7PhCfg==")},
                    {"SHA256_Null", new (null, SHA256.Create(), false, _messageValidateNull)},
            };

            foreach (var testCase in _testCasesinputString)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory, _hashformatterBase64);

                var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.expectedHash, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                }
            }
        }

        [TestMethod]
        public void CalculateHash_InputByte_ReturnsExpectedHash()
        {
            _testCasesInputByte = new Dictionary<string, (byte[] inputByte, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                {"MD5_1", new (_inputByte, MD5.Create(), true, "320DEE96D097DDA6F108C62983DEF31F")},
                {"SHA256_1", new (_inputByte, SHA256.Create(), true, "167C675E41E07059088728924744805F06DFC328EEDF5F1939DD8143D6D78226")},
                {"SHA1_1", new (_inputByte, SHA1.Create(), true, "6E17FFC27E415630EAA5E5297DA569573267CD11")},
                {"SHA384_1", new (_inputByte, SHA384.Create(), true, "F4C32EFF1D108679DD2149C2D48BABF350DB0BE0E0ED08CCC80FC5B037DF52F550FD1EB76D3AE3024D1957271AC8D6A1")},
                {"SHA512_1", new (_inputByte, SHA512.Create(), true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
                {"MD5_Empty", new (Array.Empty<byte>(), MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"SHA256_Null", new (null, SHA256.Create(), false, _messageValidateNull)},
            };

            foreach (var testCase in _testCasesInputByte)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.CalculateHash(testCase.Value.inputByte);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.expectedHash, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                }
            }
        }

        [TestMethod]
        public void CalculateHash_InputByte_ReturnsExpectedHashBase64()
        {
            _testCasesInputByte = new Dictionary<string, (byte[] inputByte, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new (_inputByte, MD5.Create(), true, "Mg3ultCX3abxCMYpg97zHw==")},
                    {"SHA256_1", new (_inputByte,SHA256.Create(), true, "FnxnXkHgcFkIhyiSR0SAXwbfwyju318ZOd2BQ9bXgiY=")},
                    {"SHA1_1", new (_inputByte,SHA1.Create(), true, "bhf/wn5BVjDqpeUpfaVpVzJnzRE=")},
                    {"SHA384_1", new (_inputByte,SHA384.Create(), true, "9MMu/x0QhnndIUnC1Iur81DbC+Dg7QjMyA/FsDffUvVQ/R63bTrjAk0ZVycayNah")},
                    {"SHA512_1", new (_inputByte,SHA512.Create(), true, "rqypB6m84k2/l2IEm2r93WrBJLJyDSqRwzF1AMhpFEKpgjD2dLxYtdpFU6UQ4+ztcUHa3F64Img29STO4P6sZg==")},
                    {"MD5_Empty", new (Array.Empty<byte>(), MD5.Create(), true, "1B2M2Y8AsgTpgAmY7PhCfg==")},
                    {"SHA256_Null", new (null, SHA256.Create(), false,_messageValidateNull)},
            };

            foreach (var testCase in _testCasesInputByte)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory, _hashformatterBase64);

                var result = handlerInstance.CalculateHash(testCase.Value.inputByte);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.expectedHash, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                }

            }
        }


        [TestMethod]
        public void CalculateHash_InputStreamReader_ReturnsExpectedHash()
        {
            _testCasesinputStreamReader = new Dictionary<string, (StreamReader inputStream, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                {"MD5_1", new (_inputStreamReader, MD5.Create(), true, "320DEE96D097DDA6F108C62983DEF31F")},
                {"SHA1_1", new (_inputStreamReader, SHA1.Create(), true, "6E17FFC27E415630EAA5E5297DA569573267CD11")},
                {"SHA256_1", new (_inputStreamReader, SHA256.Create(), true, "167C675E41E07059088728924744805F06DFC328EEDF5F1939DD8143D6D78226")},
                {"SHA384_1", new (_inputStreamReader, SHA384.Create(), true, "F4C32EFF1D108679DD2149C2D48BABF350DB0BE0E0ED08CCC80FC5B037DF52F550FD1EB76D3AE3024D1957271AC8D6A1")},
                {"SHA512_1", new (_inputStreamReader, SHA512.Create(), true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
                {"MD5_Empty", new (new StreamReader(new MemoryStream()), MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"SHA256_Null", new (null, SHA256.Create(), false, _messageValidateNull)},
            };

            foreach (var testCase in _testCasesinputStreamReader)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.CalculateHash(testCase.Value.inputStream);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.expectedHash, result.SuccessValue);
                    testCase.Value.inputStream.BaseStream.Position = 0;
                    testCase.Value.inputStream.DiscardBufferedData();
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                }
            }
        }

        [TestMethod]
        public void CalculateHash_InputStreamReader_ReturnsExpectedHashBase64()
        {
            _testCasesinputStreamReader = new Dictionary<string, (StreamReader inputStream, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new (_inputStreamReader, MD5.Create(), true, "Mg3ultCX3abxCMYpg97zHw==")},
                    {"SHA256_1", new (_inputStreamReader,SHA256.Create(), true, "FnxnXkHgcFkIhyiSR0SAXwbfwyju318ZOd2BQ9bXgiY=")},
                    {"SHA1_1", new (_inputStreamReader,SHA1.Create(), true, "bhf/wn5BVjDqpeUpfaVpVzJnzRE=")},
                    {"SHA384_1", new (_inputStreamReader,SHA384.Create(), true, "9MMu/x0QhnndIUnC1Iur81DbC+Dg7QjMyA/FsDffUvVQ/R63bTrjAk0ZVycayNah")},
                    {"SHA512_1", new (_inputStreamReader,SHA512.Create(), true, "rqypB6m84k2/l2IEm2r93WrBJLJyDSqRwzF1AMhpFEKpgjD2dLxYtdpFU6UQ4+ztcUHa3F64Img29STO4P6sZg==")},
                    {"MD5_Empty", new (new StreamReader(new MemoryStream()), MD5.Create(), true, "1B2M2Y8AsgTpgAmY7PhCfg==")},
                    {"SHA256_Null", new (null, SHA256.Create(), false,_messageValidateNull)},
            };

            foreach (var testCase in _testCasesinputStreamReader)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory, _hashformatterBase64);

                var result = handlerInstance.CalculateHash(testCase.Value.inputStream);


                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.expectedHash, result.SuccessValue);
                    testCase.Value.inputStream.BaseStream.Position = 0;
                    testCase.Value.inputStream.DiscardBufferedData();
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                }
            }
        }

        [TestMethod]
        public void CalculateHash_InputSecureString_ReturnsExpectedHash()
        {
            _testCasesInputSecureString = new Dictionary<string, (SecureString inputSecureString, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                {"MD5_1", new (_inputSecureString, MD5.Create(), true, "834F517D7AE9BEC10C8C040ED0AF53B9")},
                {"SHA256_1", new (_inputSecureString, SHA256.Create(), true, "3383C881BBA7AB67D75DF88C0CC3532F5A04CA60ADF0F557574F37DC9300F7BC")},
                {"SHA1_1", new (_inputSecureString, SHA1.Create(), true, "709A4B5A58B37D8DBE7AAC83EACCF7E9356A1F29")},
                {"SHA384_1", new (_inputSecureString, SHA384.Create(), true, "AD8C29E781A83761EAF212F42C8636AADF5DEBE8ADF4940B385C5FADCC247994569544F35E08DE366889BEF12A3C95D6")},
                {"SHA512_1", new (_inputSecureString, SHA512.Create(), true, "4E6F429F1B72FAA9F5DD8A48E240B24DC4DF70E0DC68C8E3B4242AB3FFE4095B07DD9099FBF4E39148A00B580370DBC22250D67BA860CFDAAEA0CC3BD0E3E018")},
                {"MD5_Empty", new (new SecureString(), MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"SHA256_Null", new (null, SHA256.Create(), false, _messageValidateNull)},
            };

            foreach (var testCase in _testCasesInputSecureString)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.CalculateHash(testCase.Value.inputSecureString);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.expectedHash, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                }
            }
        }

        [TestMethod]
        public void CalculateHash_InputSecureString_ReturnsExpectedHashBase64()
        {
            _testCasesInputSecureString = new Dictionary<string, (SecureString inputSecureString, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new (_inputSecureString, MD5.Create(), true, "g09RfXrpvsEMjAQO0K9TuQ==")},
                    {"SHA256_1", new (_inputSecureString,SHA256.Create(), true, "M4PIgbunq2fXXfiMDMNTL1oEymCt8PVXV0833JMA97w=")},
                    {"SHA1_1", new (_inputSecureString,SHA1.Create(), true, "cJpLWlizfY2+eqyD6sz36TVqHyk=")},
                    {"SHA384_1", new (_inputSecureString,SHA384.Create(), true, "rYwp54GoN2Hq8hL0LIY2qt9d6+it9JQLOFxfrcwkeZRWlUTzXgjeNmiJvvEqPJXW")},
                    {"SHA512_1", new (_inputSecureString,SHA512.Create(), true, "Tm9Cnxty+qn13YpI4kCyTcTfcODcaMjjtCQqs//kCVsH3ZCZ+/TjkUigC1gDcNvCIlDWe6hgz9quoMw70OPgGA==")},
                    {"MD5_Empty", new (new SecureString(), MD5.Create(), true, "1B2M2Y8AsgTpgAmY7PhCfg==")},
                    {"SHA256_Null", new (null, SHA256.Create(), false,_messageValidateNull)},
            };

            foreach (var testCase in _testCasesInputSecureString)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory, _hashformatterBase64);

                var result = handlerInstance.CalculateHash(testCase.Value.inputSecureString);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                    Assert.AreEqual(testCase.Value.expectedHash, result.SuccessValue);
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());
                    StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                }
            }
        }

        [TestMethod]
        public void HashValidate_InputString_ReturnsTrueIfValid()
        {
            _testCasesinputString = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string expectedHash)>
            {
                {"MD5_1", new (_input, MD5.Create(), true, "320DEE96D097DDA6F108C62983DEF31F") },
                {"SHA256_1", new (_input, SHA256.Create(), true, "167C675E41E07059088728924744805F06DFC328EEDF5F1939DD8143D6D78226")},
                {"SHA1_1", new (_input, SHA1.Create(), true, "6E17FFC27E415630EAA5E5297DA569573267CD11")},
                {"SHA384_1", new (_input, SHA384.Create(), true, "F4C32EFF1D108679DD2149C2D48BABF350DB0BE0E0ED08CCC80FC5B037DF52F550FD1EB76D3AE3024D1957271AC8D6A1")},
                {"SHA512_1", new (_input, SHA512.Create(), true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
                {"MD5_Empty", new ("", MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"SHA256_Null", new (null, SHA256.Create(), false, null)},
                {"MD5_1_Null", new (_input, MD5.Create(), false, null) },
                {"MD5_1_InvalidHash", new (_input, MD5.Create(), false, "invalid Hash") },
                {"SHA256_Null_Message", new (null, SHA256.Create(), false, _messageValidateNull)},
            };


            foreach (var testCase in _testCasesinputString)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.HashValidate(testCase.Value.inputName, testCase.Value.expectedHash);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());

                    if (testCase.Key == "MD5_1_InvalidHash")
                    {
                        Assert.AreEqual(_messageValidate, result.Message);
                    }
                    else if (testCase.Key == "SHA256_Null_Message")
                    {
                        StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                    }

                    Assert.IsTrue(result.IsFailure());

                }
            }
        }

        [TestMethod]
        public void HashValidate_InputByte_ReturnsTrueIfValid()
        {
            _testCasesInputByte = new Dictionary<string, (byte[] inputByte, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                {"MD5_1", new (_inputByte, MD5.Create(), true, "320DEE96D097DDA6F108C62983DEF31F")},
                {"SHA256_1", new (_inputByte, SHA256.Create(), true, "167C675E41E07059088728924744805F06DFC328EEDF5F1939DD8143D6D78226")},
                {"SHA1_1", new (_inputByte, SHA1.Create(), true, "6E17FFC27E415630EAA5E5297DA569573267CD11")},
                {"SHA384_1", new (_inputByte, SHA384.Create(), true, "F4C32EFF1D108679DD2149C2D48BABF350DB0BE0E0ED08CCC80FC5B037DF52F550FD1EB76D3AE3024D1957271AC8D6A1")},
                {"SHA512_1", new (_inputByte, SHA512.Create(), true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
                {"MD5_Empty", new (Array.Empty<byte>(), MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"SHA256_Null", new (null, SHA256.Create(), false, null)},
                {"MD5_1_Null", new (_inputByte, MD5.Create(), false, null) },
                {"MD5_1_InvalidHash", new (_inputByte, MD5.Create(), false, "invalid Hash") },
                {"SHA256_Null_Message", new (null, SHA256.Create(), false, _messageValidateNull)},
            };

            foreach (var testCase in _testCasesInputByte)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.HashValidate(testCase.Value.inputByte, testCase.Value.expectedHash);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());

                    if (testCase.Key == "MD5_1_InvalidHash")
                    {
                        Assert.AreEqual(_messageValidate, result.Message);
                    }
                    else if (testCase.Key == "SHA256_Null_Message")
                    {
                        StringAssert.Contains(result.Message,testCase.Value.expectedHash);        
                    }

                    Assert.IsTrue(result.IsFailure());

                }
            }
        }

        [TestMethod]
        public void ValidaHash_InputStreamReader_ReturnsTrueIfValid()
        {
            _testCasesinputStreamReader = new Dictionary<string, (StreamReader inputStream, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                {"MD5_1", new (_inputStreamReader, MD5.Create(), true, "320DEE96D097DDA6F108C62983DEF31F")},
                {"SHA1_1", new (_inputStreamReader, SHA1.Create(), true, "6E17FFC27E415630EAA5E5297DA569573267CD11")},
                {"SHA256_1", new (_inputStreamReader, SHA256.Create(), true, "167C675E41E07059088728924744805F06DFC328EEDF5F1939DD8143D6D78226")},
                {"SHA384_1", new (_inputStreamReader, SHA384.Create(), true, "F4C32EFF1D108679DD2149C2D48BABF350DB0BE0E0ED08CCC80FC5B037DF52F550FD1EB76D3AE3024D1957271AC8D6A1")},
                {"SHA512_1", new (_inputStreamReader, SHA512.Create(), true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
                {"MD5_Empty", new (new StreamReader(new MemoryStream()), MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"SHA256_Null", new (null, SHA256.Create(), false, null)},
                {"MD5_1_Null", new (_inputStreamReader, MD5.Create(), false, null) },
                {"MD5_1_InvalidHash", new (_inputStreamReader, MD5.Create(), false, "invalid Hash") },
                {"SHA256_Null_Message", new (null, SHA256.Create(), false, _messageValidateNull)},
            };

            foreach (var testCase in _testCasesinputStreamReader)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.HashValidate(testCase.Value.inputStream, testCase.Value.expectedHash);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                    testCase.Value.inputStream.BaseStream.Position = 0;
                    testCase.Value.inputStream.DiscardBufferedData();
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());

                    if (testCase.Key == "MD5_1_InvalidHash")
                    {
                        Assert.AreEqual(_messageValidate, result.Message);
                    }
                    else if (testCase.Key == "SHA256_Null_Message")
                    {
                        StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                    }

                    Assert.IsTrue(result.IsFailure());

                }
            }
        }

        [TestMethod]
        public void ValidateHash_InputSecureString_ReturnsTrueIfValid()
        {
            _testCasesInputSecureString = new Dictionary<string, (SecureString inputSecureString, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                {"MD5_1", new (_inputSecureString, MD5.Create(), true, "834F517D7AE9BEC10C8C040ED0AF53B9")},
                {"SHA256_1", new (_inputSecureString, SHA256.Create(), true, "3383C881BBA7AB67D75DF88C0CC3532F5A04CA60ADF0F557574F37DC9300F7BC")},
                {"SHA1_1", new (_inputSecureString, SHA1.Create(), true, "709A4B5A58B37D8DBE7AAC83EACCF7E9356A1F29")},
                {"SHA384_1", new (_inputSecureString, SHA384.Create(), true, "AD8C29E781A83761EAF212F42C8636AADF5DEBE8ADF4940B385C5FADCC247994569544F35E08DE366889BEF12A3C95D6")},
                {"SHA512_1", new (_inputSecureString, SHA512.Create(), true, "4E6F429F1B72FAA9F5DD8A48E240B24DC4DF70E0DC68C8E3B4242AB3FFE4095B07DD9099FBF4E39148A00B580370DBC22250D67BA860CFDAAEA0CC3BD0E3E018")},
                {"MD5_Empty", new (new SecureString(), MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"SHA256_Null", new (null, SHA256.Create(), false, null)},
                {"MD5_1_Null", new (_inputSecureString, MD5.Create(), false, null) },
                {"MD5_1_InvalidHash", new (_inputSecureString, MD5.Create(), false, "invalid Hash") },
                {"SHA256_Null_Message", new (null, SHA256.Create(), false, _messageValidateNull)},
            };

            foreach (var testCase in _testCasesInputSecureString)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.HashValidate(testCase.Value.inputSecureString, testCase.Value.expectedHash);

                if (testCase.Value.expectedState)
                {
                    Assert.IsTrue(result.IsSuccess());
                }
                else
                {
                    Assert.IsTrue(result.IsFailure());

                    if (testCase.Key == "MD5_1_InvalidHash")
                    {
                        Assert.AreEqual(_messageValidate, result.Message);
                    }
                    else if (testCase.Key == "SHA256_Null_Message")
                    {
                        StringAssert.Contains(result.Message, testCase.Value.expectedHash);
                    }
                    Assert.IsTrue(result.IsFailure());
                }
            }
        }
    }
}



