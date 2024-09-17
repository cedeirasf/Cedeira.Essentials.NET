﻿using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Cedeira.Essentials.NET_unittests.System.Security
{
    [TestClass]
    public class HashHandlerTest
    {
        private Dictionary<string, (string inputName, HashAlgorithm algorithm, bool expectedState, string expectedHash)> _testCasesinputString;
        private Dictionary<string, (byte[] inputByte, HashAlgorithm algorithm, bool expectedState, string expectedHash)> _testCasesInputByte;
        private Dictionary<string, (StreamReader inputStream, HashAlgorithm algorithm, bool expectedState, string expectedHash)> _testCasesinputStreamReader;
        private Dictionary<string, (SecureString inputSecureString, HashAlgorithm algorithm, bool expectedState, string expectedHash)> _testCasesInputSecureString;
        private string _input;
        private string _exceptionMessage;
        private byte[] _inputByte;
        private StreamReader _inputStreamReader;
        private SecureString _inputSecureString;


        [TestInitialize]
        public void Setup()
        {
            _input = "Testeo123";
            _exceptionMessage = "Invalid hash.";
            _inputByte = Encoding.UTF8.GetBytes(_input);
            _inputStreamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(_input)));
            _inputSecureString = new SecureString();

            foreach (char character in _input)
            {
                _inputSecureString.AppendChar(character);
            }
            _inputSecureString.MakeReadOnly();
        }

        [TestMethod]
        public void HashValidate_InputString_ReturnsThrowIfInvalidHash()
        {
            _testCasesinputString = new Dictionary<string, (string inputName, HashAlgorithm algorithm, bool estadoEsperado, string expectedHash)>
            {
                {"MD5_1", new (_input, MD5.Create(), true, "320DEE96D097DDA6F108C62983DEF31F") },
                {"SHA256_1", new (_input, SHA256.Create(), true, "167C675E41E07059088728924744805F06DFC328EEDF5F1939DD8143D6D78226")},
                {"SHA1_1", new (_input, SHA1.Create(), true, "6E17FFC27E415630EAA5E5297DA569573267CD11")},
                {"SHA384_1", new (_input, SHA384.Create(), true, "F4C32EFF1D108679DD2149C2D48BABF350DB0BE0E0ED08CCC80FC5B037DF52F550FD1EB76D3AE3024D1957271AC8D6A1")},
                {"SHA512_1", new (_input, SHA512.Create(), true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
                {"MD5_Empty", new ("", MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"MD5_1_InvalidHash", new (_input, MD5.Create(), false, "invalid Hash") },
            };

            foreach (var testCase in _testCasesinputString)
            {
                try
                {
                    var handlerInstance = new HashHandler(testCase.Value.algorithm);
                    handlerInstance.ThrowIfInvalidHash(testCase.Value.inputName, testCase.Value.expectedHash);
                }
                catch (CryptographicException ex)
                {
                    Assert.IsTrue(ex.Message == _exceptionMessage);
                }

            }
        }

        [TestMethod]
        public void HashValidate_InputByte_ReturnsThrowIfInvalidHash()
        {
            _testCasesInputByte = new Dictionary<string, (byte[] inputByte, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                {"MD5_1", new (_inputByte, MD5.Create(), true, "320DEE96D097DDA6F108C62983DEF31F")},
                {"SHA256_1", new (_inputByte, SHA256.Create(), true, "167C675E41E07059088728924744805F06DFC328EEDF5F1939DD8143D6D78226")},
                {"SHA1_1", new (_inputByte, SHA1.Create(), true, "6E17FFC27E415630EAA5E5297DA569573267CD11")},
                {"SHA384_1", new (_inputByte, SHA384.Create(), true, "F4C32EFF1D108679DD2149C2D48BABF350DB0BE0E0ED08CCC80FC5B037DF52F550FD1EB76D3AE3024D1957271AC8D6A1")},
                {"SHA512_1", new (_inputByte, SHA512.Create(), true, "AEACA907A9BCE24DBF9762049B6AFDDD6AC124B2720D2A91C3317500C8691442A98230F674BC58B5DA4553A510E3ECED7141DADC5EB8226836F524CEE0FEAC66")},
                {"MD5_Empty", new (Array.Empty<byte>(), MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"MD5_1_InvalidHash", new (_inputByte, MD5.Create(), false, "invalid Hash") },
            };

            foreach (var testCase in _testCasesInputByte)
            {
                try
                {
                    var handlerInstance = new HashHandler(testCase.Value.algorithm);
                    handlerInstance.ThrowIfInvalidHash(testCase.Value.inputByte, testCase.Value.expectedHash);
                }
                catch (CryptographicException ex)
                {
                    Assert.IsTrue(ex.Message == _exceptionMessage);
                }

            }
        }

        [TestMethod]
        public void ValidaHash_InputStreamReader_ReturnsThrowIfInvalidHash()
        {
            _testCasesinputStreamReader = new Dictionary<string, (StreamReader inputStream, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                {"MD5_1", new (_inputStreamReader, MD5.Create(), true, "320DEE96D097DDA6F108C62983DEF31F")},
                {"SHA256_1", new (_inputStreamReader, SHA256.Create(), true, "E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855")},
                {"SHA1_1", new (_inputStreamReader, SHA1.Create(), true, "DA39A3EE5E6B4B0D3255BFEF95601890AFD80709")},
                {"SHA384_1", new (_inputStreamReader, SHA384.Create(), true, "38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B")},
                {"SHA512_1", new (_inputStreamReader, SHA512.Create(), true, "CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E")},
                {"MD5_Empty", new (new StreamReader(new MemoryStream()), MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"MD5_1_InvalidHash", new (_inputStreamReader, MD5.Create(), false, "invalid Hash") },

            };

            foreach (var testCase in _testCasesinputStreamReader)
            {
                try
                {
                    var handlerInstance = new HashHandler(testCase.Value.algorithm);
                    handlerInstance.ThrowIfInvalidHash(testCase.Value.inputStream, testCase.Value.expectedHash);
                }
                catch (CryptographicException ex)
                {
                    Assert.IsTrue(ex.Message == _exceptionMessage);
                }

            }
        }

        [TestMethod]
        public void ValidateHash_InputSecureString_ReturnsThrowIfInvalidHash()
        {
            _testCasesInputSecureString = new Dictionary<string, (SecureString inputSecureString, HashAlgorithm algorithm, bool estadoEsperado, string hashEsperado)>
            {
                {"MD5_1", new (_inputSecureString, MD5.Create(), true, "834F517D7AE9BEC10C8C040ED0AF53B9")},
                {"SHA256_1", new (_inputSecureString, SHA256.Create(), true, "3383C881BBA7AB67D75DF88C0CC3532F5A04CA60ADF0F557574F37DC9300F7BC")},
                {"SHA1_1", new (_inputSecureString, SHA1.Create(), true, "709A4B5A58B37D8DBE7AAC83EACCF7E9356A1F29")},
                {"SHA384_1", new (_inputSecureString, SHA384.Create(), true, "AD8C29E781A83761EAF212F42C8636AADF5DEBE8ADF4940B385C5FADCC247994569544F35E08DE366889BEF12A3C95D6")},
                {"SHA512_1", new (_inputSecureString, SHA512.Create(), true, "4E6F429F1B72FAA9F5DD8A48E240B24DC4DF70E0DC68C8E3B4242AB3FFE4095B07DD9099FBF4E39148A00B580370DBC22250D67BA860CFDAAEA0CC3BD0E3E018")},
                {"MD5_Empty", new (new SecureString(), MD5.Create(), true, "D41D8CD98F00B204E9800998ECF8427E")},
                {"MD5_1_InvalidHash", new (_inputSecureString, MD5.Create(), false, "invalid Hash") },

            };

            foreach (var testCase in _testCasesInputSecureString)
            {
                var handlerInstance = new HashHandler(testCase.Value.algorithm);

                var result = handlerInstance.HashValidate(testCase.Value.inputSecureString, testCase.Value.expectedHash);


            }
        }
    }
}
