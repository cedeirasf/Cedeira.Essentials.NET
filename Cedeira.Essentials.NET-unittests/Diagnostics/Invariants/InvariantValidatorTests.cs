

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
        [ExpectedException(typeof(ArgumentException))]
        public void GreaterThanShouldPass_WhenValueStringFail()
        {
            Invariants.For("A").GreaterThan("B");
        }

        [TestMethod]
        public void GreaterThanShouldPass_WhenValueString()
        {
            Invariants.For("B").GreaterThan("A");
        }
    }
}

