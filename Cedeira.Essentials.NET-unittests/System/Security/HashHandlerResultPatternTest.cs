using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Reflection.Metadata;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security
{
    [TestClass]
    public class HashHandlerResultPatternTest
    {
        private Dictionary<string, (HashAlgorithm hashAlgorithm, IResultFactory resultFactory)> handlersCreateOutputString;

        [TestInitialize]
        public void Setup()
        {
            handlersCreateOutputString = new Dictionary<string, (HashAlgorithm, IResultFactory)>
            {
            { "MD5", (MD5.Create(), new ResultFactory())},
            { "SHA256", (SHA256.Create(), new ResultFactory())},
            { "SHA1", (SHA1.Create(), new ResultFactory())},
            { "SHA384", (SHA384.Create(), new ResultFactory())},
            { "SHA512", (SHA512.Create(), new ResultFactory())},
            { "SHA3_256", (SHA3_256.Create(), new ResultFactory())},
            { "SHA3_384", (SHA3_384.Create(), new ResultFactory())},
            { "SHA3_512", (SHA3_512.Create(), new ResultFactory())},
            };
        }

        [TestMethod]
        public void CalculateHash_StringInput_ReturnsExpectedHashWithIResultPattern()
        {
            foreach (var handler in handlersCreateOutputString)
            {
                var handlerInstance = new HashHandlerResultPattern(
                    handler.Value.hashAlgorithm,
                    handler.Value.resultFactory
                );

                string input = "Testeo123";

                var testCases = new Dictionary<string, (string inputName, bool estadoEsperado, string hashEsperado)>
                {
                    {"MD5_1", new ( input, true, "320dee96d097dda6f108c62983def31f") },
                    {"SHA256_2", new (input, true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")}
                    //{"SHA1_3", new (input, true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    //{"SHA384_4", new (input, true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    //{"SHA512_5", new (input, true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    //{"SHA3_256_6", new (input, true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    //{"SHA3_384_7", new (input, true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    //{"SHA3_512_8", new (input, true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                };

                foreach (var testCase in testCases.Where(x=>x.Key.Contains(handler.Key)).ToDictionary())
                {
                    var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                    Assert.AreEqual(testCase.Value.estadoEsperado, result.IsSuccess());
                    if (result.IsSuccess())
                        Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
                }
            }
        }
        //[TestMethod]
        //public void CalculateHash_ByteArrayInput_ReturnsExpectedHashWithIResultPattern()
        //{
        //    foreach (var handler in handlersCreateOutputString)
        //    {
        //        var handlerInstance = new HashHandlerResultPattern(
        //                handler.Value.hashAlgorithm,
        //                handler.Value.resultFactory
        //            );

        //        var testCases = new Dictionary<string, (string inputName, bool estadoEsperado, byte[] hashEsperado)>
        //        {
        //            { "ok_1", new (Encoding.UTF8.GetBytes()"Testeo123", true, "320dee96d097dda6f108c62983def31f")}
        //        };

        //        foreach (var testCase in testCases)
        //        {
        //            var result = handlerInstance.CalculateHash(testCase.Value.inputName);

        //            Assert.AreEqual(testCase.Value.estadoEsperado, result.IsSuccess());
        //            if (result.IsSuccess())
        //                Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
        //        }
        //    }
        //}

        //[TestMethod]
        //public void CalculateHash_StreamReaderInput_ReturnsExpectedHashWithIResultPattern()
        //{

        //    var handler = new HashHandlerResultPattern<string>(_hashAlgorithm, _resultFactory, _hashFormatter);

        //    var inputString = "HolaMundo@";

        //    var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString));

        //    using var input = new StreamReader(inputStream);

        //    var expectedHash = _hashFormatter(_hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString)));

        //    input.BaseStream.Seek(0, SeekOrigin.Begin); // Reinicia el StreamReader

        //    var result = handler.CalculateHash(input);

        //    Assert.AreEqual(expectedHash, result.SuccessValue);
        //}
    }

}

