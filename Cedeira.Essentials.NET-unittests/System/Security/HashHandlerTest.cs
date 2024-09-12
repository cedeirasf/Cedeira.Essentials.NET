using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security
{
    [TestClass]
    public class HashHandlerTest
    {
        private HashAlgorithm _hashAlgorithm;
        private Func<byte[], string> _hashFormatter;

        [TestInitialize]
        public void Setup()
        {
            
            ///Inicializa hashAlgorithm con tipo especifico.
            _hashAlgorithm = SHA256.Create();
            ///convierte el arreglo de bytes a una cadena hexadecimal separada por guiones ,
            ///elimina los guiones de la cadena hexadecimal.convierte la cadena resultante a minúsculas.
            _hashFormatter = bytes => BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        [TestMethod]
        public void CalculateHash_StringInput_ReturnsExpectedHash()
        {
         
            var handler = new HashHandler(_hashAlgorithm);

            var input = "test";

            var hashBytes = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var expectedHash = _hashFormatter(hashBytes);
           
            var result = handler.CalculateHash(input);

            Assert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void CalculateHash_ByteArrayInput_ReturnsExpectedHash()
        {
    
            var handler = new HashHandler(_hashAlgorithm);

            var input = Encoding.UTF8.GetBytes("test");

            var expectedHash = _hashFormatter(_hashAlgorithm.ComputeHash(input));

            var result = handler.CalculateHash(input);

            Assert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void CalculateHash_StreamReaderInput_ReturnsExpectedHash()
        {
        
            var handler = new HashHandler(_hashAlgorithm);
            var inputString = "test";
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString));
            using var input = new StreamReader(inputStream);
            var expectedHash = _hashFormatter(_hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString)));
   
            input.BaseStream.Seek(0, SeekOrigin.Begin); // Reinicia el StreamReader
            var result = handler.CalculateHash(input);
     
            Assert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void CalculateHash_SecureStringInput_ReturnsExpectedHash()
        {
           
            var handler = new HashHandler(_hashAlgorithm);
            var secureString = new SecureString();
            foreach (char c in "test") secureString.AppendChar(c);
            secureString.MakeReadOnly();
            var bstr = Marshal.SecureStringToBSTR(secureString);
            var length = Marshal.ReadInt32(bstr, -4);
            var bytes = new byte[length];
            Marshal.Copy(bstr, bytes, 0, length);
            var expectedHash = _hashFormatter(_hashAlgorithm.ComputeHash(bytes));
            Marshal.ZeroFreeBSTR(bstr);

            var result = handler.CalculateHash(secureString);

            Assert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void HashValidate_StringInput_ReturnsTrueForValidHash()
        {
            var handler = new HashHandler(_hashAlgorithm);

            var input = "test";

            var hash = handler.CalculateHash(input);

            var result = handler.HashValidate(input, hash);

            Assert.IsTrue(result);
        }

    }
}
