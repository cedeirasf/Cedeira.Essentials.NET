

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
                if (x.Length != 5)
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
    }
}

