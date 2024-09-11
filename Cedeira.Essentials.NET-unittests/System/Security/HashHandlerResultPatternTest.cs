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
        private Dictionary<string, (HashAlgorithm hashAlgorithm, IResultFactory resultFactory, Func<byte[], object> hashFormatter)> handlersCreate;

        [TestInitialize]
        public void Setup()
        {
            handlersCreate = new Dictionary<string, (HashAlgorithm, IResultFactory, Func<byte[], object>)>
            {
            { "SHA256", (SHA256.Create(), new ResultFactory(), bytes => BitConverter.ToString(bytes).Replace("-", "").ToLower()) },
            { "MD5", (MD5.Create(), new ResultFactory(), bytes => BitConverter.ToString(bytes).Replace("-", "").ToLower()) },
            { "MD5_StreamReader", (MD5.Create(), new ResultFactory(), bytes => new StreamReader(new MemoryStream(bytes))) },
            { "MD5_SecureString", (MD5.Create(), new ResultFactory(), bytes => ConvertToSecureString(Encoding.UTF8.GetString(bytes)))}
            };
        }
        public SecureString ConvertToSecureString(string input)
        {
            SecureString secureString = new SecureString();
            foreach (char c in input)
            {
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();
            return secureString;
        }


        [TestMethod]
        public void CalculateHash_StringInput_ReturnsExpectedHashWithIResultPattern()
        {
            foreach (var handler in handlersCreate)
            {
                object handlerInstance;
                if (handler.Key.Contains("StreamReader"))
                {
                    handlerInstance = new HashHandlerResultPattern<StreamReader>(
                        handler.Value.hashAlgorithm,
                        handler.Value.resultFactory,
                        (Func<byte[], StreamReader>)handler.Value.hashFormatter);
                }
                else if (handler.Key.Contains("SecureString"))
                {
                    handlerInstance = new HashHandlerResultPattern<SecureString>(
                        handler.Value.hashAlgorithm,
                        handler.Value.resultFactory,
                        (Func<byte[], SecureString>)handler.Value.hashFormatter);
                }
                else
                {
                    handlerInstance = new HashHandlerResultPattern<string>(
                        handler.Value.hashAlgorithm,
                        handler.Value.resultFactory,
                        (Func<byte[], string>)handler.Value.hashFormatter);
                }

                var testCases = new Dictionary<string, (string inputName, bool estadoEsperado, object hashEsperado)>
                {
                    {"ok_1", new ("Testeo123", true, "320dee96d097dda6f108c62983def31f")},
                };

                foreach (var testCase in testCases)
                {
                    var result = ((HashHandlerResultPattern<string>)handlerInstance).CalculateHash(testCase.Value.inputName);

                    Assert.AreEqual(testCase.Value.estadoEsperado, result.IsSuccess());
                    if (result.IsSuccess())
                        Assert.AreEqual(testCase.Value.hashEsperado, result.SuccessValue);
                }
            }
        }

        //[TestMethod]
        //public void CalculateHash_ByteArrayInput_ReturnsExpectedHashWithIResultPattern()
        //{
        //    var handler = new HashHandlerResultPattern<string>(_hashAlgorithm, _resultFactory, _hashFormatter);

        //    var input = Encoding.UTF8.GetBytes("test1234");

        //    var expectedHash = _hashFormatter(_hashAlgorithm.ComputeHash(input));

        //    var result = handler.CalculateHash(input);

        //    Assert.AreEqual(expectedHash, result.SuccessValue);
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
