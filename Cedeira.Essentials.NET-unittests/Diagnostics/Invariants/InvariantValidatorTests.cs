namespace Cedeira.Essentials.NET.Diagnostics.Invariants
{
    [TestClass]
    public class InvariantValidatorTests
    {
        [TestMethod]
        public void IsEqual_ShouldPass_WhenValuesAreEqual()
        {
            var validator = Invariants.For(5);
            validator.IsEqual(5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsEqual_ShouldThrow_WhenValuesAreNotEqual()
        {
            var validator = Invariants.For(5);
            validator.IsEqual(10);
        }

        [TestMethod]
        public void IsNotNull_ShouldPass_WhenValueIsNotNull()
        {
            var validator = Invariants.For("Hello");
            validator.IsNotNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNull_ShouldThrow_WhenValueIsNull()
        {
            var validator = Invariants.For<string>(null);
            validator.IsNotNull();
        }

        [TestMethod]
        public void IsNotNullOrEmpty_ShouldPass_WhenValueIsNotEmpty()
        {
            var validator = Invariants.For("Hello");
            validator.IsNotNullOrEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsNotNullOrEmpty_ShouldThrow_WhenValueIsEmpty()
        {
            var validator = Invariants.For("");
            validator.IsNotNullOrEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrEmpty_ShouldThrow_WhenValueIsNull()
        {
            string? primitiveString = null;

            var validator = Invariants.For(primitiveString);
            validator.IsNotNullOrEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrEmpty_ShouldThrow_WhenValueIsNull_With_null_errorMessage()
        {
            string? primitiveString = null;

            var validator = Invariants.For(primitiveString);
            validator.IsNotNullOrEmpty(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrEmpty_ShouldThrow_WhenValueIsNull_With_empty_errorMessage()
        {
            string? primitiveString = null;

            var validator = Invariants.For(primitiveString);
            validator.IsNotNullOrEmpty("");
        }

        [TestMethod]
        public void MaximumLength_ShouldPass_WhenValueLengthIsLessThanMax()
        {
            var validator = Invariants.For("Hello");
            validator.MaximumLength(10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MaximumLength_ShouldThrow_WhenValueLengthExceedsMax()
        {
            var validator = Invariants.For("Hello, World!");
            validator.MaximumLength(5);
        }

        [TestMethod]
        public void MatchesRegex_ShouldPass_WhenValueMatchesPattern()
        {
            var validator = Invariants.For("Hello123");
            validator.MatchesRegex("^[a-zA-Z0-9]*$");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void MatchesRegex_ShouldThrow_WhenValueDoesNotMatchPattern()
        {
            var validator = Invariants.For("Hello@123");
            validator.MatchesRegex("^[a-zA-Z0-9]*$");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Chaining_ShouldThrow_WhenOneValidationFails()
        {
            var validator = Invariants.For("Hello@123");
            validator
                .IsNotNull()
                .IsNotNullOrEmpty()
                .MaximumLength(10)
                .MatchesRegex("^[a-zA-Z0-9]*$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Chaining_ShouldThrow_MultipleValidationsFail()
        {
            var validator = Invariants.For("Hello@123456");
            validator
                .IsNotNull()
                .IsNotNullOrEmpty()
                .MaximumLength(10)
                .MatchesRegex("^[a-zA-Z0-9]*$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CustomValidation_Should_Throw_Exception()
        {
            var validator = Invariants.For("Hello");
            validator.CustomInvariant(x =>
            {
                if (x.Length == 5)
                {
                    throw new ArgumentException();
                }
            }, "Value must be 5 characters long.");
        }

        [TestMethod]
        public void CustomValidation_ShouldPass_()
        {
            var validator = Invariants.For(1);
            validator.CustomInvariant(value =>
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }
            }, "Value must be positive.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CustomValidation_ShouldNotPass_With_Null_Action()
        {
            var validator = Invariants.For(1);
            validator.CustomInvariant(null
            , "Value must be positive.");
        }

        [TestMethod]
        public void LessThan_ShouldPass_WhenValueIsLessThanMax()
        {
            Invariants.For(-1).LessThan(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LessThan_ShouldPass_WhenValueIsLessThanMaxFail()
        {
            Invariants.For(10).LessThan(5);
        }

        [TestMethod]
        public void GreaterThan_ShouldPass_WhenValueIsGreaterThanMax()
        {
            Invariants.For(10).GreaterThan(5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GreaterThan_ShouldPass_WhenValueIsGreaterThanMaxFail()
        {
            Invariants.For(-1).GreaterThan(0);
        }

        [TestMethod]
        public void LessThanShouldPass_WhenValueDatetime()
        {
            var today = DateTime.Now.Date;
            Invariants.For(today).LessThan(today.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LessThanShouldPass_WhenValueDatetimeFail()
        {
            var today = DateTime.Now.Date;
            Invariants.For(today.AddDays(1)).LessThan(today);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LessThanShouldPass_WhenValueDatetimeNull()
        {
            var today = DateTime.Now.Date;
            Invariants.For<DateTime?>(null).GreaterThan(today.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LessThanShouldPass_WhenEspectDatetimeNull()
        {
            var today = DateTime.Now.Date;
            Invariants.For<DateTime?>(today).LessThan(null);
        }

        [TestMethod]
        public void LessThanShouldPass_WhenValueString()
        {
            Invariants.For("A").LessThan("B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LessThanShouldPass_WhenValueStringFail()
        {
            Invariants.For("B").LessThan("A");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LessThanShouldPass_WhenExpectedStringNull()
        {
            Invariants.For("A").LessThan(null);
        }

        [TestMethod]
        public void GreaterThanShouldPass_WhenValueDatetime()
        {
            var today = DateTime.Now.Date;
            Invariants.For(today.AddDays(1)).GreaterThan(today);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GreaterThanShouldPass_WhenValueDatetimeFail()
        {
            var today = DateTime.Now.Date;
            Invariants.For(today).GreaterThan(today.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GreaterThanShouldPass_WhenValueDatetimeNull()
        {
            var today = DateTime.Now.Date;
            Invariants.For<DateTime?>(null).GreaterThan(today.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GreaterThanShouldPass_WhenEspectDatetimeNull()
        {
            var today = DateTime.Now.Date;
            Invariants.For<DateTime?>(today).GreaterThan(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GreaterThanShouldPass_WhenValueStringFail()
        {
            Invariants.For("A").GreaterThan("B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LessThanShouldPass_WhenValueNull()
        {
            Invariants.For<int?>(null).LessThan(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GreaterThanShouldPass_WhenValueNull()
        {
            Invariants.For<int?>(null).GreaterThan(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LessThanShouldPass_WhenEspectNull()
        {
            Invariants.For<int?>(5).LessThan(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GreaterThanShouldPass_WhenEspectNull()
        {
            Invariants.For<int?>(10).GreaterThan(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GreaterThanShouldPass_WhenExpectedStringNull()
        {
            Invariants.For("A").GreaterThan(null);
        }

        [TestMethod]
        public void GreaterThanShouldPass_WhenValueString()
        {
            Invariants.For("B").GreaterThan("A");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LessThanShouldPass_WhenValueIsObject()
        {
            object Objeto = new object();
            Invariants.For<object?>(Objeto).LessThan(10);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GreaterThanShouldPass_WhenValueIsObject()
        {
            object Objeto = new object();
            Invariants.For(Objeto).LessThan(10);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LessThanShouldPass_WhenEspectIsObject()
        {
            object Objeto = new object();
            Invariants.For<object?>(5).LessThan(Objeto);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GreaterThanShouldPass_WhenEspectIsObject()
        {
            object Objeto = new object();
            Invariants.For<object?>(5).LessThan(Objeto);
        }

        [TestMethod]
        public void IsNotNullOrWhiteSpace_ShouldPass_WhenValueIsValid()
        {
            var validator = Invariants.For("Hola mundo");
            validator.IsNotNullOrWhiteSpace();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrWhiteSpace_ShouldThrow_WhenValueIsNull()
        {
            string? primitiveString = null;
            var validator = Invariants.For(primitiveString);
            validator.IsNotNullOrWhiteSpace();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsNotNullOrWhiteSpace_ShouldThrow_WhenValueIsEmpty()
        {
            var validator = Invariants.For("");
            validator.IsNotNullOrWhiteSpace();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsNotNullOrWhiteSpace_ShouldThrow_WhenValueIsWhiteSpace()
        {
            var validator = Invariants.For("   ");
            validator.IsNotNullOrWhiteSpace();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrWhiteSpace_ShouldThrow_WhenErrorMessageIsNull()
        {
            var validator = Invariants.For("Hola");
            validator.IsNotNullOrWhiteSpace(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullOrWhiteSpace_ShouldThrow_WhenErrorMessageIsEmpty()
        {
            var validator = Invariants.For("Hola");
            validator.IsNotNullOrWhiteSpace("");
        }

        [TestMethod]
        public void InvariantValidator_ValidateErrorMessage_Tests()
        {
            var validator = Invariants.For("test");

            Assert.ThrowsException<ArgumentNullException>(() => validator.IsNotNull(null));
        }

        [TestMethod]
        public void InvariantValidator_MatchesRegex_Tests()
        {
            var validator = Invariants.For("abc123");

            Assert.ThrowsException<FormatException>(() => validator.MatchesRegex(@"^\d+$", "Only numbers allowed"));

            Assert.ThrowsException<ArgumentNullException>(() => validator.MatchesRegex(null, "Valid error message"));

            Assert.ThrowsException<ArgumentNullException>(() => validator.MatchesRegex("", "Valid error message"));

            Assert.ThrowsException<ArgumentNullException>(() => validator.MatchesRegex(@"^\d+$", null));

            Assert.ThrowsException<ArgumentException>(() => validator.MatchesRegex(@"[", "Invalid regex pattern"));
        }

        [TestMethod]
        public void InvariantValidator_MaximumLength_Tests()
        {
            var validator = Invariants.For("hello");

            Assert.ThrowsException<ArgumentException>(() => validator.MaximumLength(3, "String too long"));

            Assert.ThrowsException<ArgumentException>(() => validator.MaximumLength(-1, "Valid error message"));

            Assert.ThrowsException<ArgumentNullException>(() => validator.MaximumLength(10, null));

            Assert.ThrowsException<ArgumentNullException>(() => validator.MaximumLength(10, ""));
        }

        [TestMethod]
        public void InvariantValidator_IsNotNull_Tests()
        {
            var validValidator = Invariants.For("not null");

            var nullValidator = Invariants.For<string>(null);
            Assert.ThrowsException<ArgumentNullException>(() => nullValidator.IsNotNull("Value is null"));

            var validator = Invariants.For("test");
            Assert.ThrowsException<ArgumentNullException>(() => validator.IsNotNull(null));

            Assert.ThrowsException<ArgumentNullException>(() => validator.IsNotNull(""));
        }

        [TestMethod]
        public void InvariantValidator_IsEqual_WithCustomMessage_Tests()
        {
            var validator = Invariants.For(10);

            Assert.ThrowsException<ArgumentException>(() => validator.IsEqual(20, "Values are not equal"));

            Assert.ThrowsException<ArgumentNullException>(() => validator.IsEqual(10, null));

            Assert.ThrowsException<ArgumentNullException>(() => validator.IsEqual(10, ""));

            var stringValidator = Invariants.For("hello");
            Assert.ThrowsException<ArgumentException>(() => stringValidator.IsEqual("world", "Strings don't match"));
        }

        [TestMethod]
        public void InvariantValidator_IsEqual_DefaultMessage_Tests()
        {
            var validator = Invariants.For(42);

            var exception = Assert.ThrowsException<ArgumentException>(() => validator.IsEqual(99));
            Assert.IsTrue(exception.Message.Contains("Value must be equal to 99"),
                "Default error message should contain expected value");

            var nullValidator = Invariants.For<string>(null);
            Assert.ThrowsException<ArgumentException>(() => nullValidator.IsEqual("not null"));
        }
    }
}