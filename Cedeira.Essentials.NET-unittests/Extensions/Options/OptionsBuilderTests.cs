using Cedeira.Essentials.NET.Extensions.Options;

namespace Cedeira.Essentials.NET_unittests.Extensions.Options
{
    /// <summary>
    /// This class contains unit tests for the <see cref="OptionsBuilder{T}"/> class.
    /// </summary>
    [TestClass]
    public class OptionsBuilderTests
    {
        /// <summary>
        /// Concrete test class for testing OptionsBuilder.
        /// </summary>
        private class TestOptions
        {
            public string Name { get; set; } = string.Empty;
            public int Value { get; set; }
            public bool IsEnabled { get; set; }
        }

        /// <summary>
        /// Concrete implementation of OptionsBuilder for testing.
        /// </summary>
        private class TestOptionsBuilder : OptionsBuilder<TestOptions>
        {
            private readonly TestOptions _options;

            public TestOptionsBuilder()
            {
                _options = new TestOptions();
            }

            public TestOptionsBuilder WithName(string name)
            {
                _options.Name = name;
                return this;
            }

            public TestOptionsBuilder WithValue(int value)
            {
                _options.Value = value;
                return this;
            }

            public TestOptionsBuilder WithIsEnabled(bool isEnabled)
            {
                _options.IsEnabled = isEnabled;
                return this;
            }

            public override TestOptions Build()
            {
                return _options;
            }
        }

        /// <summary>
        /// Test that OptionsBuilder is an abstract class and cannot be instantiated directly.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(MissingMethodException))]
        public void OptionsBuilder_ShouldBeAbstract()
        {
            // Try to instantiate OptionsBuilder directly should fail
            var builder = Activator.CreateInstance(typeof(OptionsBuilder<TestOptions>));
        }

        /// <summary>
        /// Test that a concrete implementation of OptionsBuilder can build options correctly.
        /// </summary>
        [TestMethod]
        public void ConcreteOptionsBuilder_ShouldBuildOptionsCorrectly()
        {
            // Arrange:
            var expectedName = "TestName";
            var expectedValue = 42;
            var expectedIsEnabled = true;

            // Act:
            var options = new TestOptionsBuilder()
                .WithName(expectedName)
                .WithValue(expectedValue)
                .WithIsEnabled(expectedIsEnabled)
                .Build();

            // Assert:
            Assert.IsNotNull(options);
            Assert.AreEqual(expectedName, options.Name);
            Assert.AreEqual(expectedValue, options.Value);
            Assert.AreEqual(expectedIsEnabled, options.IsEnabled);
        }

        /// <summary>
        /// Test that OptionsBuilder can build options with default values.
        /// </summary>
        [TestMethod]
        public void OptionsBuilder_ShouldBuildOptionsWithDefaultValues()
        {
            // Act:
            var options = new TestOptionsBuilder().Build();

            // Assert:
            Assert.IsNotNull(options);
            Assert.AreEqual(string.Empty, options.Name);
            Assert.AreEqual(0, options.Value);
            Assert.AreEqual(false, options.IsEnabled);
        }

        /// <summary>
        /// Test that OptionsBuilder can build options with partial configuration.
        /// </summary>
        [TestMethod]
        public void OptionsBuilder_ShouldBuildOptionsWithPartialConfiguration()
        {
            // Arrange:
            var expectedName = "PartialTest";

            // Act:
            var options = new TestOptionsBuilder()
                .WithName(expectedName)
                .Build();

            // Assert:
            Assert.IsNotNull(options);
            Assert.AreEqual(expectedName, options.Name);
            Assert.AreEqual(0, options.Value); // Default value
            Assert.AreEqual(false, options.IsEnabled); // Default value
        }

        /// <summary>
        /// Test that OptionsBuilder can build multiple independent instances.
        /// </summary>
        [TestMethod]
        public void OptionsBuilder_ShouldBuildMultipleIndependentInstances()
        {
            // Arrange:
            var builder1 = new TestOptionsBuilder()
                .WithName("First")
                .WithValue(1)
                .WithIsEnabled(true);

            var builder2 = new TestOptionsBuilder()
                .WithName("Second")
                .WithValue(2)
                .WithIsEnabled(false);

            // Act:
            var options1 = builder1.Build();
            var options2 = builder2.Build();

            // Assert:
            Assert.IsNotNull(options1);
            Assert.IsNotNull(options2);
            Assert.AreNotSame(options1, options2);
            Assert.AreEqual("First", options1.Name);
            Assert.AreEqual("Second", options2.Name);
            Assert.AreEqual(1, options1.Value);
            Assert.AreEqual(2, options2.Value);
            Assert.AreEqual(true, options1.IsEnabled);
            Assert.AreEqual(false, options2.IsEnabled);
        }

        /// <summary>
        /// Test that OptionsBuilder can build options with fluent configuration (method chaining).
        /// </summary>
        [TestMethod]
        public void OptionsBuilder_ShouldSupportFluentConfiguration()
        {
            // Arrange:
            var expectedName = "FluentTest";
            var expectedValue = 100;
            var expectedIsEnabled = true;

            // Act:
            var options = new TestOptionsBuilder()
                .WithName(expectedName)
                .WithValue(expectedValue)
                .WithIsEnabled(expectedIsEnabled)
                .Build();

            // Assert:
            Assert.IsNotNull(options);
            Assert.AreEqual(expectedName, options.Name);
            Assert.AreEqual(expectedValue, options.Value);
            Assert.AreEqual(expectedIsEnabled, options.IsEnabled);
        }

        /// <summary>
        /// Test that OptionsBuilder can build options with null and default values.
        /// </summary>
        [TestMethod]
        public void OptionsBuilder_ShouldHandleNullAndDefaultValues()
        {
            // Arrange:
            string? nullName = null;
            var expectedValue = 0;
            var expectedIsEnabled = false;

            // Act:
            var options = new TestOptionsBuilder()
                .WithName(nullName ?? string.Empty)
                .WithValue(expectedValue)
                .WithIsEnabled(expectedIsEnabled)
                .Build();

            // Assert:
            Assert.IsNotNull(options);
            Assert.AreEqual(string.Empty, options.Name);
            Assert.AreEqual(expectedValue, options.Value);
            Assert.AreEqual(expectedIsEnabled, options.IsEnabled);
        }

        /// <summary>
        /// Test that OptionsBuilder can build options with extreme values.
        /// </summary>
        [TestMethod]
        public void OptionsBuilder_ShouldHandleExtremeValues()
        {
            // Arrange:
            var expectedName = "ExtremeTest";
            var expectedValue = int.MaxValue;
            var expectedIsEnabled = true;

            // Act:
            var options = new TestOptionsBuilder()
                .WithName(expectedName)
                .WithValue(expectedValue)
                .WithIsEnabled(expectedIsEnabled)
                .Build();

            // Assert:
            Assert.IsNotNull(options);
            Assert.AreEqual(expectedName, options.Name);
            Assert.AreEqual(expectedValue, options.Value);
            Assert.AreEqual(expectedIsEnabled, options.IsEnabled);
        }

        /// <summary>
        /// Test that OptionsBuilder can build options with negative values.
        /// </summary>
        [TestMethod]
        public void OptionsBuilder_ShouldHandleNegativeValues()
        {
            // Arrange:
            var expectedName = "NegativeTest";
            var expectedValue = -42;
            var expectedIsEnabled = false;

            // Act:
            var options = new TestOptionsBuilder()
                .WithName(expectedName)
                .WithValue(expectedValue)
                .WithIsEnabled(expectedIsEnabled)
                .Build();

            // Assert:
            Assert.IsNotNull(options);
            Assert.AreEqual(expectedName, options.Name);
            Assert.AreEqual(expectedValue, options.Value);
            Assert.AreEqual(expectedIsEnabled, options.IsEnabled);
        }

        /// <summary>
        /// Test that OptionsBuilder can build options with empty and whitespace strings.
        /// </summary>
        [TestMethod]
        public void OptionsBuilder_ShouldHandleEmptyAndWhitespaceStrings()
        {
            // Arrange:
            var emptyName = string.Empty;
            var whitespaceName = "   ";
            var expectedValue = 0;
            var expectedIsEnabled = false;

            // Act:
            var options1 = new TestOptionsBuilder()
                .WithName(emptyName)
                .WithValue(expectedValue)
                .WithIsEnabled(expectedIsEnabled)
                .Build();

            var options2 = new TestOptionsBuilder()
                .WithName(whitespaceName)
                .WithValue(expectedValue)
                .WithIsEnabled(expectedIsEnabled)
                .Build();

            // Assert:
            Assert.IsNotNull(options1);
            Assert.IsNotNull(options2);
            Assert.AreEqual(emptyName, options1.Name);
            Assert.AreEqual(whitespaceName, options2.Name);
            Assert.AreEqual(expectedValue, options1.Value);
            Assert.AreEqual(expectedValue, options2.Value);
            Assert.AreEqual(expectedIsEnabled, options1.IsEnabled);
            Assert.AreEqual(expectedIsEnabled, options2.IsEnabled);
        }
    }
}
