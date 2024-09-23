using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Hash
{
    [TestClass]
    public class HashContextTest
    {
        private Dictionary<string, (HashAlgorithmName algorithmName, Func<byte[], string>? hashformatter, bool expectedState)> _TestHashContext;
        private IHashContext _hashContext;
        private HashAlgorithmName _hashAlgorithmName;
        private Func<byte[], string>? _expectedFormatter;
        private ServiceCollection _service;
        private string _exceptionMessage;


        [TestInitialize]
        public void Setup()
        {
            _hashAlgorithmName = HashAlgorithmName.SHA256;
            _expectedFormatter = bytes => BitConverter.ToString(bytes);
            _hashContext = HashContext.Create(_hashAlgorithmName, _expectedFormatter);

            _exceptionMessage = $"The algorithm '{HashAlgorithmName.SHA3_512.Name}' is not recognized.";
            _service = new ServiceCollection();
        }

        [TestMethod]
        public void HashContext_Create_SetsAlgorithmNameAndFormatter()
        {
            Assert.AreEqual(_hashAlgorithmName, _hashContext.HashConfig.AlgorithmName);
            Assert.IsNotNull(_hashContext.HashConfig.HashFormatter);
            Assert.AreEqual(_expectedFormatter, _hashContext.HashConfig.HashFormatter);
        }

        [TestMethod]
        public void ServiceCollection_RegistersHashContextCorrectly()
        {
            _TestHashContext = new Dictionary<string, (HashAlgorithmName algorithmName, Func<byte[], string>? hashFormatter, bool expectedState)>
            {
                {"1_hashformatter_null",new ( HashAlgorithmName.SHA256,null,true)},
                {"2_hashformatter_base64",new ( HashAlgorithmName.MD5, bytes => Convert.ToBase64String(bytes),true)},
                {"3_otherAlgorithmName",new ( HashAlgorithmName.SHA256,null,true)},
                {"4_NonHandledAlghorithmName",new ( HashAlgorithmName.SHA3_512, bytes => BitConverter.ToString(bytes),false)},
            };

            foreach (var testCase in _TestHashContext)
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
    }
}
