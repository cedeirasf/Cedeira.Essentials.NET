using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Hash
{
    [TestClass]
    public class HashContextTest
    {
        private Dictionary<string, (string algorithmName, Func<byte[], string>? hashformatter, bool expectedState)> _TestHashContextAlgorithmName;
        private Dictionary<string, (HashAlgorithm algorithm, Func<byte[], string>? hashformatter, bool expectedState)> _TestHashContextAlgorithm;
            
        private IHashContext _hashContext;
        private string _hashAlgorithmName;
        private Func<byte[], string>? _expectedFormatter;
        private ServiceCollection _service;
        private string _exceptionMessage;
        private string _mullExceptionMessage;

        [TestInitialize]
        public void Setup()
        {
            _hashAlgorithmName = "SHA256";
            _expectedFormatter = bytes => BitConverter.ToString(bytes);
            _hashContext = HashContext.Create(_hashAlgorithmName, _expectedFormatter);
            _exceptionMessage = $"Invalid algorithm name: {HashAlgorithmName.SHA3_512}";
            _mullExceptionMessage = "Value cannot be null. (Parameter 'hashAlgorithm')";
            _service = new ServiceCollection();
        }

        [TestMethod]
        public void HashContext_Create_SetsAlgorithmNameAndFormatter()
        {
            Assert.IsNotNull(_hashContext.HashAlgorithm);
            Assert.IsNotNull(_hashContext.HashFormatter);
            Assert.AreEqual(_expectedFormatter, _hashContext.HashFormatter);
        }

        [TestMethod]
        public void ServiceCollection_Register_HashContext_Correctly()
        {
            _TestHashContextAlgorithmName = new Dictionary<string, (string algorithmName, Func<byte[], string>? hashFormatter, bool expectedState)>
            {
                {"1_hashformatter_null",new ( "SHA256",null,true)},
                {"2_otherAlgorithmName",new ( "SHA1",null,true)},
                {"3_hashformatter_base64",new ( "MD5", bytes => Convert.ToBase64String(bytes),true)},
                {"4_NonHandledAlghorithmName",new ( "SHA3-512", bytes => BitConverter.ToString(bytes),false)},
            };

            foreach (var testCase in _TestHashContextAlgorithmName)
            {
                if (!testCase.Value.expectedState)
                {
                    var excep = Assert.ThrowsException<ArgumentException>(() =>
                    {
                        _service.AddSingleton<IHashContext>(HashContext.Create(testCase.Value.algorithmName, testCase.Value.hashformatter));
                    });

                    Assert.AreEqual(excep.Message, _exceptionMessage);
                }
                else
                {
                    _service.AddSingleton<IHashContext>(HashContext.Create(testCase.Value.algorithmName, testCase.Value.hashformatter));

                    _service.AddSingleton<IOptions<IHashContext>>(sp => new OptionsWrapper<IHashContext>(sp.GetRequiredService<IHashContext>()));

                    var serviceProvider = _service.BuildServiceProvider();

                    var hashContext = serviceProvider.GetService<IHashContext>();

                    var optionsHashContext = serviceProvider.GetService<IOptions<IHashContext>>();

                    Assert.IsNotNull(hashContext);
                    Assert.IsNotNull(optionsHashContext);
                    Assert.AreEqual(hashContext, optionsHashContext.Value);
                }
            }
        }

        [TestMethod]
        public void ServiceCollection_Registers_HashContext_with_HashAlgorithm_Correctly()
        {
            _TestHashContextAlgorithm = new Dictionary<string, (HashAlgorithm algorithm, Func<byte[], string>? hashFormatter, bool expectedState)>
            {
                {"1_hashformatter_null",new ( SHA256.Create(),null,true)},
                {"2_otherAlgorithmName",new ( SHA1.Create(),null,true)},
                {"3_hashformatter_base64",new ( MD5.Create(), bytes => Convert.ToBase64String(bytes),true)},
                {"4_NonHandledAlghorithmName",new ( null, bytes => BitConverter.ToString(bytes),false)},
            };

            foreach (var testCase in _TestHashContextAlgorithm)
            {
                if (!testCase.Value.expectedState)
                {
                    var excep = Assert.ThrowsException<ArgumentNullException>(() =>
                    {
                        _service.AddSingleton<IHashContext>(HashContext.Create(testCase.Value.algorithm, testCase.Value.hashformatter));
                    });

                    Assert.AreEqual(excep.Message, _mullExceptionMessage);
                }
                else
                {
                    _service.AddSingleton<IHashContext>(HashContext.Create(testCase.Value.algorithm, testCase.Value.hashformatter));

                    _service.AddSingleton<IOptions<IHashContext>>(sp => new OptionsWrapper<IHashContext>(sp.GetRequiredService<IHashContext>()));

                    var serviceProvider = _service.BuildServiceProvider();

                    var hashContext = serviceProvider.GetService<IHashContext>();

                    var optionsHashContext = serviceProvider.GetService<IOptions<IHashContext>>();

                    Assert.IsNotNull(hashContext);
                    Assert.IsNotNull(optionsHashContext);
                    Assert.AreEqual(hashContext, optionsHashContext.Value);
                }
            }
        }
    }
}
