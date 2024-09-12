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
        private Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)> testCases;
        private Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado, Func<byte[],string> hashformatter)> testCasesWithFunc;
        private IResultFactory _resultFactory;


        [TestInitialize]
        public void Setup()
        {
            _resultFactory = new ResultFactory();

            string input = "Testeo123";

            testCases = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado )>
            {
                    {"MD5_1", new ( input,MD5.Create(), true, "320dee96d097dda6f108c62983def31f") },
                    {"SHA256_1", new (input,SHA256.Create(), true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    {"SHA1_1", new (input,SHA1.Create(), true, "6e17ffc27e415630eaa5e5297da569573267cd11")},
                    {"SHA384_1", new (input,SHA384.Create(), true, "f4c32eff1d108679dd2149c2d48babf350db0be0e0ed08ccc80fc5b037df52f550fd1eb76d3ae3024d1957271ac8d6a1")},
                    {"SHA512_1", new (input,SHA512.Create(), true, "aeaca907a9bce24dbf9762049b6afddd6ac124b2720d2a91c3317500c8691442a98230f674bc58b5da4553a510e3eced7141dadc5eb8226836f524cee0feac66")},
                    {"MD5_Empty", new ("", MD5.Create(), true, "d41d8cd98f00b204e9800998ecf8427e")}, 
                    {"SHA256_Null", new (null, SHA256.Create(), true, "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")},
             };


            testCasesWithFunc = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado, Func<byte[],string> hashformatter)>
            {
                    {"MD5_1", new ("Testeo123", MD5.Create(), true, "VGVzdGVvMTIz", bytes =>Convert.ToBase64String(Encoding.UTF8.GetBytes(input)))},
             };


        }

        [TestMethod]
        public void CalculateHash_StringInput_ReturnsExpectedHashWithIResultPattern()
        {
            foreach (var testCase in testCases)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory);

                var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                Assert.AreEqual(testCase.Value.estadoEsperado, result.IsSuccess());
                if (result.IsSuccess())
                    Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
            }
        }

        [TestMethod]
        public void CalculateHash_StringInput_ReturnsExpectedHashWithFunc()
        {
            foreach (var testCase in testCasesWithFunc)
            {
                var handlerInstance = new HashHandlerResultPattern(testCase.Value.algorithm, _resultFactory, testCase.Value.hashformatter);

                var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                Assert.AreEqual(testCase.Value.estadoEsperado, result.IsSuccess());
                if (result.IsSuccess())
                    Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
            }
        }

    }

}



