using Cedeira.Essentials.NET.System.Resilience.Fallback;

namespace Cedeira.Essentials.NET_unittests.System.Resilience
{
    [TestClass]
    public class FallbackStrategyTest
    {
        private UserClassTest _user;

        [TestInitialize]
        public void Setup()
        {
            _user = new UserClassTest { Name = "John" };
        }       

        [TestMethod]
        public void Coalesce_WithStringFunctions_ReturnsFirstNonEmptyValue()
        {
            var stringTest = FallbackStrategy.Coalesce(() => "a", () => "b", () => "c");
            Assert.IsNotNull(stringTest);
        }

        [TestMethod]
        public void Coalesce_WithNullValues_ReturnsFirstNonNullValue()
        {
            var stringTest = FallbackStrategy.Coalesce(() => null, () => null, () => "c");
            Assert.AreEqual(stringTest, "c");
        }
        [TestMethod]
        public void Coalesce_WithEmptyStringAndNull_ReturnsFirstValidString()
        {
            var stringTest = FallbackStrategy.Coalesce(() => "", () => null, () => "c");
            Assert.AreEqual(stringTest, "c");
        }

        [TestMethod]
        public void Coalesce_WithIntegers_ReturnsFirstNonDefaultValue()
        {
            var intTest = FallbackStrategy.Coalesce(() => 0, () => 1, () => 2);
            Assert.AreEqual(intTest, 1);
        }

        [TestMethod]
        public void Coalesce_WithNullableDateTime_ReturnsFirstNonNullValue()
        {
            var dateTimeTest = FallbackStrategy.Coalesce(() => (DateTime?)null, () => DateTime.Now, () => DateTime.UtcNow);
            Assert.IsNotNull(dateTimeTest);
        }
        [TestMethod]
        public void Coalesce_WithDateTime_ReturnsFirstNonNullValue()
        {
            var dateTimeTest = FallbackStrategy.Coalesce(() => (DateTime?)null, () => DateTime.Now, () => DateTime.UtcNow);
            Assert.AreEqual(dateTimeTest.ToString(), DateTime.Now.ToString());
        }

        [TestMethod]
        public void Coalesce_WithStringCollections_ReturnsFirstNonEmptyCollection()
        {
            var collectionTest = FallbackStrategy.Coalesce(
                () => new List<string>(),
                () => (List<string>?)null,
                () => new List<string> { "value1", "value2" });

            Assert.IsNotNull(collectionTest);
            Assert.IsTrue(collectionTest.Count > 0);
            Assert.AreEqual("value1", collectionTest.First());
        }

        [TestMethod]
        public void Coalesce_ReturnsSecondProviderValue_WhenFirstReturnsDefault()
        {
            var result = FallbackStrategy.Coalesce(() => 0, () => 1);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Coalesce_ReturnsFirstNonEmptyString()
        {
            var result = FallbackStrategy.Coalesce(() => string.Empty, () => null, () => "valid");
            Assert.AreEqual("valid", result);
        }

        [TestMethod]
        public void Coalesce_ReturnsDefault_WhenAllProvidersReturnDefault()
        {
            var result = FallbackStrategy.Coalesce(() => 0, () => 0);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Coalesce_ReturnsValue_WhenSingleProviderIsNotDefault()
        {
            var result = FallbackStrategy.Coalesce(() => 42);
            Assert.AreEqual(42, result);
        }

        [TestMethod]
        public void Coalesce_ReturnsValidUserInstance()
        {
            var result = FallbackStrategy.Coalesce(() => null, () => _user);

            Assert.IsNotNull(result);
            Assert.AreEqual(_user.Name, result.Name);
        }

        [TestMethod]
        public void Coalesce_EvaluatesInOrder()
        {
            var evaluated = new List<string>();
            var result = FallbackStrategy.Coalesce(
                () => { evaluated.Add("first"); return null; },
                () => { evaluated.Add("second"); return "valid"; }
            );

            Assert.AreEqual("valid", result);
            Assert.AreEqual(2, evaluated.Count);
            Assert.AreEqual("first", evaluated[0]);
            Assert.AreEqual("second", evaluated[1]);
        }

        [TestMethod]
        public void Coalesce_HandlesExceptionsFromProviders()
        {
            var result = FallbackStrategy.Coalesce(
                () => throw new Exception("Error"),
                () => "valid"
            );

            Assert.AreEqual("valid", result);
        }

        [TestMethod]
        public async Task Coalesce_WithAsyncFunctions_ReturnsFirstNonNullValue()
        {
            var asyncTest = await FallbackStrategy.Coalesce(
                async () => await Task.FromResult(""),
                async () => await Task.FromResult("valid")
            );

            Assert.AreEqual("valid", asyncTest);
        }

        [TestMethod]
        public void Coalesce_ThrowsFallbackStrategyException_WhenProviderThrowsIt()
        {
            Assert.ThrowsException<FallbackStrategyException>(() =>
                FallbackStrategy.Coalesce(
                    () => throw new FallbackStrategyException("Escape anticipado"),
                    () => "no debe llegar aquí"
                )
            );
        }

        [TestMethod]
        public async Task CoalesceAsync_ThrowsFallbackStrategyException_WhenProviderThrowsIt()
        {
            await Assert.ThrowsExceptionAsync<FallbackStrategyException>(async () =>
                await FallbackStrategy.Coalesce(
                    async () => throw new FallbackStrategyException("Escape anticipado async"),
                    async () => await Task.FromResult("no debe llegar aquí")
                )
            );
        }
    }
}
