using Cedeira.Essentials.NET.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security
{
    [TestClass]
    public class HashHandlerResultPatternTest 
    {
        private HashAlgorithm _hashAlgorithm;
        private Func<byte[], string> _hashFormatter;
        private IResultFactory _resultFactory;  

        [TestInitialize]
        public void Setup()
        {
            ///Inicializa hashAlgorithm con tipo especifico.
            _hashAlgorithm = MD5.Create();
            ///convierte el arreglo de bytes a una cadena hexadecimal separada por guiones ,
            ///elimina los guiones de la cadena hexadecimal.convierte la cadena resultante a minúsculas.
            _hashFormatter = bytes => BitConverter.ToString(bytes).Replace("-", "").ToLower();
            // Iniciaiza un result factory
            _resultFactory = new ResultFactory();
        }

        [TestMethod]
        public void CalculateHash_StringInput_ReturnsExpectedHashWithIResultPattern()
        {
            var handler = new HashHandlerResultPattern<string>(_hashAlgorithm, _resultFactory,_hashFormatter);

            var input = "Testeo123";

            var hashBytes = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var expectedHash = _hashFormatter(hashBytes);

            var result = handler.CalculateHash(input);

            Assert.AreEqual(expectedHash, result.SuccessValue);
        }

        [TestMethod]
        public void CalculateHash_ByteArrayInput_ReturnsExpectedHashWithIResultPattern()
        {

            var handler = new HashHandlerResultPattern<string>(_hashAlgorithm,_resultFactory, _hashFormatter);

            var input = Encoding.UTF8.GetBytes("test1234");

            var expectedHash = _hashFormatter(_hashAlgorithm.ComputeHash(input));

            var result = handler.CalculateHash(input);

            Assert.AreEqual(expectedHash, result.SuccessValue);
        }

        [TestMethod]
        public void CalculateHash_StreamReaderInput_ReturnsExpectedHashWithIResultPattern()
        {

            var handler = new HashHandlerResultPattern<string>(_hashAlgorithm,_resultFactory, _hashFormatter);

            var inputString = "HolaMundo@";

            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString));

            using var input = new StreamReader(inputStream);

            var expectedHash = _hashFormatter(_hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString)));

            input.BaseStream.Seek(0, SeekOrigin.Begin); // Reinicia el StreamReader

            var result = handler.CalculateHash(input);

            Assert.AreEqual(expectedHash, result.SuccessValue);
        }
    }
}
