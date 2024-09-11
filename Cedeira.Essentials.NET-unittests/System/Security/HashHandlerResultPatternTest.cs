using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security
{
    [TestClass]
    public class HashHandlerResultPatternTest
    {
        private Dictionary<string, (HashAlgorithm hashAlgorithm, IResultFactory resultFactory, Func<byte[], string> hashFormatter)> handlersCreateOutputString;
        private Dictionary<string, (HashAlgorithm hashAlgorithm, IResultFactory resultFactory, Func<byte[], byte[]> hashFormatter)> handlersCreateOutputBytes;

        [TestInitialize]
        public void Setup()
        {
            handlersCreateOutputString = new Dictionary<string, (HashAlgorithm, IResultFactory, Func<byte[], string>)>
            {
            { "SHA256", (SHA256.Create(), new ResultFactory(), bytes => BitConverter.ToString(bytes).Replace("-", "").ToLower()) },
            { "MD5", (MD5.Create(), new ResultFactory(), bytes => BitConverter.ToString(bytes).Replace("-", "").ToLower())},
            };

            handlersCreateOutputBytes = new Dictionary<string, (HashAlgorithm, IResultFactory, Func<byte[], byte[]>)>
            {
            { "SHA256", (SHA256.Create(), new ResultFactory(),bytes => bytes)},
            { "MD5", (MD5.Create(), new ResultFactory(),bytes => bytes )}};



        }

        [TestMethod]
        public void CalculateHash_StringInput_ReturnsExpectedHashWithIResultPattern()
        {
            foreach (var handler in handlersCreateOutputString)
            {
                var handlerInstance = new HashHandlerResultPattern<string>(
                    handler.Value.hashAlgorithm,
                    handler.Value.resultFactory,
                    handler.Value.hashFormatter
                );

                var testCases = new Dictionary<string, (string inputName, bool estadoEsperado, string hashEsperado)>
                {
                    { "ok_1", new ("Testeo123", true, "320dee96d097dda6f108c62983def31f") }
                };

                foreach (var testCase in testCases)
                {
                    var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                    Assert.AreEqual(testCase.Value.estadoEsperado, result.IsSuccess());
                    if (result.IsSuccess())
                        Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
                }
            }
        }
        [TestMethod]
        public void CalculateHash_ByteArrayInput_ReturnsExpectedHashWithIResultPattern()
        {
            foreach (var handler in handlersCreateOutputBytes)
            {
                var handlerInstance = new HashHandlerResultPattern<byte[]>(
                        handler.Value.hashAlgorithm,
                        handler.Value.resultFactory,
                        handler.Value.hashFormatter
                    );

                var testCases = new Dictionary<string, (string inputName, bool estadoEsperado, byte[] hashEsperado)>
                {
                    { "ok_1", new ("Testeo123", true, Encoding.UTF8.GetBytes("320dee96d097dda6f108c62983def31f"))}
                };

                foreach (var testCase in testCases)
                {
                    var result = handlerInstance.CalculateHash(testCase.Value.inputName);

                    Assert.AreEqual(testCase.Value.estadoEsperado, result.IsSuccess());
                    if (result.IsSuccess())
                        Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
                }
            }
        }

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

