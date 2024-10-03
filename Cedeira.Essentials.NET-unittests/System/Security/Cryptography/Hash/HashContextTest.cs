using Cedeira.Essentials.NET.System.Security.Cryptography.Hash;
using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Cedeira.Essentials.NET_unittests.System.Security.Cryptography.Hash
{
    /// <summary>
    /// Represents a test class for the HashContext functionality.
    /// </summary>
    [TestClass]
    public class HashContextTest
    {
        /// <summary>
        /// A dictionary to store test cases with algorithm names, hash formatters, and expected states.
        /// </summary>
        private Dictionary<string, (string algorithmName, Func<byte[], string>? hashformatter, bool expectedState)> _TestHashContextAlgorithmNameWithFormat;

        /// <summary>
        /// A dictionary to store test cases with hash algorithms, hash formatters, and expected states.
        /// </summary>
        private Dictionary<string, (HashAlgorithm algorithm, Func<byte[], string>? hashformatter, bool expectedState)> _TestHashContextAlgorithmWithFormat;

        /// <summary>
        /// A dictionary to store test cases with algorithm names and expected states.
        /// </summary>
        private Dictionary<string, (string algorithmName, bool expectedState)> _TestHashContextAlgorithmName;

        /// <summary>
        /// A dictionary to store test cases with algorithms and expected states.
        /// </summary>
        private Dictionary<string, (HashAlgorithm algorithm, bool expectedState)> _TestHashContextAlgorithm;

        /// <summary>
        /// A dictionary to store test cases with differents create modes.
        /// </summary>
        private Dictionary<string, HashContext> _TestHashContextCreate;


        /// <summary>
        /// The expected hash formatter function.
        /// </summary>
        private Func<byte[], string>? _expectedFormatter;

        /// <summary>
        /// The service collection used for dependency injection.
        /// </summary>
        private ServiceCollection _service;

        /// <summary>
        /// The exception message for invalid algorithm names.
        /// </summary>
        private string _exceptionMessage;

        /// <summary>
        /// The exception message for null values.
        /// </summary>
        private string _mullExceptionMessage;

        /// <summary>
        /// Initializes the test setup with default values.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _exceptionMessage = $"Invalid algorithm name: {HashAlgorithmName.SHA3_512}";
            _mullExceptionMessage = "Value cannot be null. (Parameter 'hashAlgorithm')";
            _service = new ServiceCollection();
        }

        /// <summary>
        /// Tests that the HashContext is correctly registered in the service collection with a hash algorithm.
        /// </summary>
        [TestMethod]
        public void HashContext_Create_SetsAlgorithmNameAndFormatter()
        {
            _TestHashContextCreate = new Dictionary<string, HashContext>
            {
                  {"1_CreateFromAlgorithm",HashContext.CreateFromAlgorithm(SHA256.Create())},
                  {"2_CreateFromAlgorithmName",HashContext.CreateFromAlgorithmName("MD5")},
                  {"3_CreateFromAlgorithmWithFormatter",HashContext.CreateFromAlgorithmWithFormatter(SHA1.Create(),bytes => Convert.ToHexString(bytes))},
                  {"4_CreatFromAlgorithmNameWithFormmatter",HashContext.CreatFromAlgorithmNameWithFormmatter("SHA1",bytes => Convert.ToBase64String(bytes))},
            };

            foreach (var test in _TestHashContextCreate)
            {
                Assert.IsNotNull(test.Value.HashAlgorithm);
                Assert.IsNotNull(test.Value.HashFormatter);
            }
        }

        /// <summary>
        /// Tests that the HashContext is correctly registered in the service collection and test some algotihms with a string algorithm name.
        /// </summary>
        [TestMethod]
        public void ServiceCollection_Register_HashContext_Correctly_With_AlgortihmName_And_Ouput_Formatter()
        {
            _TestHashContextAlgorithmNameWithFormat = new Dictionary<string, (string algorithmName, Func<byte[], string>? hashFormatter, bool expectedState)>
            {
                {"1_hashformatter_null",new ( "SHA256",null,true)},
                {"2_otherAlgorithmName",new ( "SHA1",null,true)},
                {"3_hashformatter_base64",new ( "MD5", bytes => Convert.ToBase64String(bytes),true)},
                {"4_NonHandledAlghorithmName",new ( "SHA3-512", bytes => BitConverter.ToString(bytes),false)},
            };

            foreach (var testCase in _TestHashContextAlgorithmNameWithFormat)
            {
                if (!testCase.Value.expectedState)
                {
                    var excep = Assert.ThrowsException<ArgumentException>(() =>
                    {
                        _service.AddSingleton<IHashContext>(HashContext.CreatFromAlgorithmNameWithFormmatter(testCase.Value.algorithmName, testCase.Value.hashformatter));
                    });

                    Assert.AreEqual(excep.Message, _exceptionMessage);
                }
                else
                {
                    _service.AddSingleton<IHashContext>(HashContext.CreatFromAlgorithmNameWithFormmatter(testCase.Value.algorithmName, testCase.Value.hashformatter));

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

        /// <summary>
        /// Tests that the HashContext is correctly registered in the service collection and test some Algotihms with a algorithm.
        /// </summary>
        [TestMethod]
        public void ServiceCollection_Registers_HashContext_Correctly_With_Algortihm_And_Ouput_Formatter()
        {
            _TestHashContextAlgorithmWithFormat = new Dictionary<string, (HashAlgorithm algorithm, Func<byte[], string>? hashFormatter, bool expectedState)>
            {
                {"1_hashformatter_null",new ( SHA256.Create(),null,true)},
                {"2_otherAlgorithmName",new ( SHA1.Create(),null,true)},
                {"3_hashformatter_base64",new ( MD5.Create(), bytes => Convert.ToBase64String(bytes),true)},
                {"4_NonHandledAlghorithmName",new ( null, bytes => BitConverter.ToString(bytes),false)},
            };

            foreach (var testCase in _TestHashContextAlgorithmWithFormat)
            {
                if (!testCase.Value.expectedState)
                {
                    var excep = Assert.ThrowsException<ArgumentNullException>(() =>
                    {
                        _service.AddSingleton<IHashContext>(HashContext.CreateFromAlgorithmWithFormatter(testCase.Value.algorithm, testCase.Value.hashformatter));
                    });

                    Assert.AreEqual(excep.Message, _mullExceptionMessage);
                }
                else
                {
                    _service.AddSingleton<IHashContext>(HashContext.CreateFromAlgorithmWithFormatter(testCase.Value.algorithm, testCase.Value.hashformatter));
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

        /// <summary>
        /// Tests that the HashContext is correctly registered in the service collection and test some algotihms with a string algorithm name.
        /// </summary>
        [TestMethod]
        public void ServiceCollection_Register_HashContext_Correctly_With_AlgortihmName()
        {
            _TestHashContextAlgorithmName = new Dictionary<string, (string algorithmName, bool expectedState)>
            {
                {"1_hashformatter_null",new ( "SHA256",true)},
                {"2_otherAlgorithmName",new ( "SHA1",true)},
                {"4_NonHandledAlghorithmName",new ( null,false)},
            };

            foreach (var testCase in _TestHashContextAlgorithmNameWithFormat)
            {
                if (!testCase.Value.expectedState)
                {
                    var excep = Assert.ThrowsException<ArgumentException>(() =>
                    {
                        _service.AddSingleton<IHashContext>(HashContext.CreateFromAlgorithmName(testCase.Value.algorithmName));
                    });

                    Assert.AreEqual(excep.Message, _exceptionMessage);
                }
                else
                {
                    _service.AddSingleton<IHashContext>(HashContext.CreateFromAlgorithmName(testCase.Value.algorithmName));

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
        /// <summary>
        /// Tests that the HashContext is correctly registered in the service collection and test some Algotihms with a algorithm.
        /// </summary>
        [TestMethod]
        public void ServiceCollection_Registers_HashContext_Correctly_With_Algortihm()
        {
            _TestHashContextAlgorithm = new Dictionary<string, (HashAlgorithm algorithm, bool expectedState)>
            {
                {"1_hashformatter_null",new ( SHA256.Create(),true)},
                {"2_otherAlgorithmName",new ( SHA1.Create(),true)},
                {"4_NonHandledAlghorithmName",new (null,false)},
            };

            foreach (var testCase in _TestHashContextAlgorithmWithFormat)
            {
                if (!testCase.Value.expectedState)
                {
                    var excep = Assert.ThrowsException<ArgumentNullException>(() =>
                    {
                        _service.AddSingleton<IHashContext>(HashContext.CreateFromAlgorithm(testCase.Value.algorithm));
                    });

                    Assert.AreEqual(excep.Message, _mullExceptionMessage);
                }
                else
                {
                    _service.AddSingleton<IHashContext>(HashContext.CreateFromAlgorithm(testCase.Value.algorithm));
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
