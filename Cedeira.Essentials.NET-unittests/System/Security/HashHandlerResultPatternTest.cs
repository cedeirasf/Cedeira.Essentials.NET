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
        private IResultFactory _resultFactory;


        //HashAlgorithmName.SHA256,
        //        HashAlgorithmName.SHA1,
        //        HashAlgorithmName.MD5,
        //        HashAlgorithmName.SHA384,
        //        HashAlgorithmName.SHA512,
        //        HashAlgorithmName.SHA3_256,
        //        HashAlgorithmName.SHA3_384,
        //        HashAlgorithmName.SHA3_512



        [TestInitialize]
        public void Setup()11|¿
        {
            _resultFactory = new ResultFactory();

            string input = "Testeo123";

            testCases = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                    {"MD5_1", new ( input,MD5.Create(), true, "320dee96d097dda6f108c62983def31f") },
                    {"SHA256_1", new (input,SHA256.Create(), true, "167c675e41e07059088728924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    {"SHA1_1", new (input,SHA1.Create(), true, "ad267a2368772b0712ba53c8c8a3a313ac4d56f6")},
                    {"SHA384_1", new (input,SHA384.Create(), true, "f4c32eff1d108679dd2149c2d48babf350db0be0e0ed08ccc80fc5b037df52f550fd1eb76d3ae3024d1957271ac8d6a1")},
                    {"SHA512_1", new (input,SHA512.Create(), true, "aeaca907a9bce24dbf9762049b6afddd6ac124b2720d2a91c3317500c8691442a98230f674bc58b5da4553a510e3eced7141dadc5eb8226836f524cee0feac66924744805f06dfc328eedf5f1939dd8143d6d78226")},
                    {"SHA3_256_1", new (input,SHA3_256.Create(), true, "2c17948d0e7c60c93b5e5b411f3ed77041efe397824a8b49091f6f76bafccd69")},
                    {"SHA3_384_1", new (input,SHA3_384.Create(), true, "b4c9d8ea950f2008c8d637b98dd4377f84e2e05fc6ac94891e4e35be214745933ab0563ea5a19dc07b6e149524ee277c")},

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
    }

}



