using Cedeira.Essentials.NET.Extensions.Exceptions;

namespace Cedeira.Essentials.NET_unittests.Extensions.Exceptions
{
    /// <summary>
    /// Contains unit tests for the <see cref="ExceptionExtension"/> class.
    /// </summary>
    [TestClass]
    public class ExceptionExtensionTests
    {
        /// <summary>
        /// Verifies that the <see cref="ExceptionExtension.FullMessage(Exception)"/> method returns the correct message when there is no inner exception.
        /// </summary>
        [TestMethod]
        public void FullMessage_WithoutInnerException_ReturnsCorrectMessage()
        {
            var exceptionMessage = "This is a test exception";
            var exception = new Exception(exceptionMessage);

            var result = exception.FullMessage();

            Assert.AreEqual(exceptionMessage, result);
        }

        /// <summary>
        /// Verifies that the <see cref="ExceptionExtension.FullMessage(Exception)"/> method returns the combined message when there is an inner exception.
        /// </summary>
        [TestMethod]
        public void FullMessage_WithOneInnerException_ReturnsCombinedMessage()
        {
            var innerExceptionMessage = "This is an inner exception";
            var outerExceptionMessage = "This is an outer exception";
            var innerException = new Exception(innerExceptionMessage);
            var outerException = new Exception(outerExceptionMessage, innerException);
            var expectedMessage = $"{outerExceptionMessage}. {innerExceptionMessage}";

            var result = outerException.FullMessage();

            Assert.AreEqual(expectedMessage, result);
        }

        /// <summary>
        /// Verifies that the <see cref="ExceptionExtension.FullMessage(Exception)"/> method returns all combined messages when there are multiple inner exceptions.
        /// </summary>
        [TestMethod]
        public void FullMessage_WithMultipleInnerExceptions_ReturnsAllCombinedMessages()
        {
            Random rnd = new Random();
            int numberOfInnerExceptions = rnd.Next(2, 10);
            var exceptionMessages = new string[numberOfInnerExceptions];
            Exception? currentException = null;

            for (int i = numberOfInnerExceptions; i >= 1; i--)
            {
                currentException = new Exception($"Exception message {i}", currentException);
                exceptionMessages[i - 1] = currentException.Message;
            }

            var outerException = currentException;
            var expectedMessage = string.Join(". ", exceptionMessages);

            var result = outerException?.FullMessage();

            Assert.AreEqual(expectedMessage, result);
        }

        /// <summary>
        /// Verifies that the <see cref="ExceptionExtension.FullMessage(Exception, string)"/> method returns the combined messages using custom separators.
        /// </summary>
        [TestMethod]
        public void FullMessage_WithCustomSeparators_ReturnsCombinedMessages()
        {
            var testCases = new Dictionary<(List<string> exceptionMessages, string separator), string>
            {
                { (new List<string> { "Outer exception", "Inner exception" }, " | "), "Outer exception | Inner exception" },
                { (new List<string> { "Outer exception", "Middle exception", "Inner exception" }, ""), "Outer exceptionMiddle exceptionInner exception" },
                { (new List<string> { "Outer exception", "Inner exception" }, ","), "Outer exception,Inner exception" }
            };

            foreach (var testCase in testCases)
            {
                var exceptionMessages = testCase.Key.exceptionMessages;
                var separator = testCase.Key.separator;
                var expectedMessage = testCase.Value;

                Exception? currentException = null;
                for (int i = exceptionMessages.Count - 1; i >= 0; i--)
                {
                    currentException = new Exception(exceptionMessages[i], currentException);
                }

                var result = currentException?.FullMessage(separator);

                Assert.AreEqual(expectedMessage, result);
            }
        }

        /// <summary>
        /// Verifies that the <see cref="ExceptionExtension.LastExceptionMessage(Exception)"/> method returns the correct message when there is no inner exception.
        /// </summary>
        [TestMethod]
        public void LastExceptionMessage_WithoutInnerException_ReturnsCorrectMessage()
        {
            var exceptionMessage = "This is a test exception";
            var exception = new Exception(exceptionMessage);

            var result = exception.LastExceptionMessage();

            Assert.AreEqual(exceptionMessage, result);
        }

        /// <summary>
        /// Verifies that the <see cref="ExceptionExtension.LastExceptionMessage(Exception)"/> method returns the message of the last nested exception.
        /// </summary>
        [TestMethod]
        public void LastExceptionMessage_WithOneInnerException_ReturnsInnerExceptionMessage()
        {
            var innerExceptionMessage = "This is an inner exception";
            var outerExceptionMessage = "This is an outer exception";
            var innerException = new Exception(innerExceptionMessage);
            var outerException = new Exception(outerExceptionMessage, innerException);

            var result = outerException.LastExceptionMessage();

            Assert.AreEqual(innerExceptionMessage, result);
        }

        /// <summary>
        /// Verifies that the <see cref="ExceptionExtension.LastExceptionMessage(Exception)"/> method returns the message of the last nested exception when there are multiple inner exceptions.
        /// </summary>
        [TestMethod]
        public void LastExceptionMessage_WithMultipleInnerExceptions_ReturnsLastInnerExceptionMessage()
        {
            var lastInnerExceptionMessage = "This is the last inner exception";
            var innerException = new Exception("Inner exception 2", new Exception(lastInnerExceptionMessage));
            var outerException = new Exception("Outer exception", innerException);

            var result = outerException.LastExceptionMessage();

            Assert.AreEqual(lastInnerExceptionMessage, result);
        }

        #region ContainsException Tests

        /// <summary>
        /// Verifies that ContainsException(Type) returns true if the exception is of the searched type.
        /// </summary>
        [TestMethod]
        public void ContainsException_Type_SimpleMatch_ReturnsTrue()
        {
            var ex = new ArgumentNullException();
            Assert.IsTrue(ExceptionExtension.ContainsException(ex, typeof(ArgumentNullException)));
        }

        /// <summary>
        /// Verifies that ContainsException(Type) returns true if an InnerException is of the searched type.
        /// </summary>
        [TestMethod]
        public void ContainsException_Type_InnerMatch_ReturnsTrue()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            Assert.IsTrue(ExceptionExtension.ContainsException(ex, typeof(ArgumentOutOfRangeException)));
        }

        /// <summary>
        /// Verifies that ContainsException(Type) returns false if there is no match.
        /// </summary>
        [TestMethod]
        public void ContainsException_Type_NoMatch_ReturnsFalse()
        {
            var ex = new Exception();
            Assert.IsFalse(ExceptionExtension.ContainsException(ex, typeof(ArgumentException)));
        }

        /// <summary>
        /// Verifies that ContainsException(Type) handles null exceptions and null type.
        /// </summary>
        [TestMethod]
        public void ContainsException_Type_NullInputs_ReturnsFalse()
        {
            Exception? ex = null;
            Assert.IsFalse(ExceptionExtension.ContainsException(ex, typeof(Exception)));
            var ex2 = new Exception();
            Assert.IsFalse(ExceptionExtension.ContainsException(ex2, (Type)null));
        }

        /// <summary>
        /// Verifies that ContainsException(string) returns true if the exception is of the searched name.
        /// </summary>
        [TestMethod]
        public void ContainsException_String_SimpleMatch_ReturnsTrue()
        {
            var ex = new ArgumentNullException();
            Assert.IsTrue(ex.ContainsException("ArgumentNullException"));
        }

        /// <summary>
        /// Verifies that ContainsException(string) returns true if an InnerException is of the searched name.
        /// </summary>
        [TestMethod]
        public void ContainsException_String_InnerMatch_ReturnsTrue()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            Assert.IsTrue(ex.ContainsException("ArgumentOutOfRangeException"));
        }

        /// <summary>
        /// Verifies that ContainsException(string) returns false if there is no match.
        /// </summary>
        [TestMethod]
        public void ContainsException_String_NoMatch_ReturnsFalse()
        {
            var ex = new Exception();
            Assert.IsFalse(ex.ContainsException("ArgumentException"));
        }

        /// <summary>
        /// Verifies that ContainsException(string) handles null exceptions and null name.
        /// </summary>
        [TestMethod]
        public void ContainsException_String_NullInputs_ReturnsFalse()
        {
            Exception? ex = null;
            Assert.IsFalse(ExceptionExtension.ContainsException(ex, "Exception"));
            var ex2 = new Exception();
            Assert.IsFalse(ExceptionExtension.ContainsException(ex2, (string)null));
            Assert.IsFalse(ExceptionExtension.ContainsException(ex2, ""));
        }

        /// <summary>
        /// Verifies that ContainsException<T>() returns true if the exception is of the searched type.
        /// </summary>
        [TestMethod]
        public void ContainsException_Generic_SimpleMatch_ReturnsTrue()
        {
            var ex = new ArgumentNullException();
            Assert.IsTrue(ex.ContainsException<ArgumentNullException>());
        }

        /// <summary>
        /// Verifies that ContainsException<T>() returns true if an InnerException is of the searched type.
        /// </summary>
        [TestMethod]
        public void ContainsException_Generic_InnerMatch_ReturnsTrue()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            Assert.IsTrue(ex.ContainsException<ArgumentOutOfRangeException>());
        }

        /// <summary>
        /// Verifies that ContainsException<T>() returns false if there is no match.
        /// </summary>
        [TestMethod]
        public void ContainsException_Generic_NoMatch_ReturnsFalse()
        {
            var ex = new Exception();
            Assert.IsFalse(ex.ContainsException<ArgumentException>());
        }

        /// <summary>
        /// Verifies that ContainsException<T>() handles null exceptions.
        /// </summary>
        [TestMethod]
        public void ContainsException_Generic_NullInput_ReturnsFalse()
        {
            Exception? ex = null;
            Assert.IsFalse(ex.ContainsException<Exception>());
        }

        /// <summary>
        /// Verifies that ContainsException works with inheritance hierarchy.
        /// </summary>
        [TestMethod]
        public void ContainsException_InheritanceHierarchy_ReturnsTrueForBaseAndDerived()
        {
            var ex = new ArgumentOutOfRangeException();
            Assert.IsTrue(ExceptionExtension.ContainsException(ex, typeof(ArgumentException)));
            Assert.IsTrue(ex.ContainsException<ArgumentException>());
            Assert.IsTrue(ex.ContainsException("ArgumentOutOfRangeException"));
        }

        #endregion ContainsException Tests

        #region FindException Tests

        /// <summary>
        /// Verifies that FindException(Type) returns the exception if it is of the searched type.
        /// </summary>
        [TestMethod]
        public void FindException_Type_SimpleMatch_ReturnsException()
        {
            var ex = new ArgumentNullException();
            var result = ExceptionExtension.FindException(ex, typeof(ArgumentNullException));
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentNullException));
        }

        /// <summary>
        /// Verifies that FindException(Type) returns the InnerException if it is of the searched type.
        /// </summary>
        [TestMethod]
        public void FindException_Type_InnerMatch_ReturnsException()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            var result = ExceptionExtension.FindException(ex, typeof(ArgumentOutOfRangeException));
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentOutOfRangeException));
        }

        /// <summary>
        /// Verifies that FindException(Type) returns null if there is no match.
        /// </summary>
        [TestMethod]
        public void FindException_Type_NoMatch_ReturnsNull()
        {
            var ex = new Exception();
            var result = ExceptionExtension.FindException(ex, typeof(ArgumentException));
            Assert.IsNull(result);
        }

        /// <summary>
        /// Verifies that FindException(Type) handles null exceptions and null type.
        /// </summary>
        [TestMethod]
        public void FindException_Type_NullInputs_ReturnsNull()
        {
            Exception? ex = null;
            var result1 = ExceptionExtension.FindException(ex, typeof(Exception));
            Assert.IsNull(result1);
            var ex2 = new Exception();
            var result2 = ExceptionExtension.FindException(ex2, (Type)null);
            Assert.IsNull(result2);
        }

        /// <summary>
        /// Verifies that FindException(string) returns the exception if it is of the searched name.
        /// </summary>
        [TestMethod]
        public void FindException_String_SimpleMatch_ReturnsException()
        {
            var ex = new ArgumentNullException();
            var result = ExceptionExtension.FindException(ex, "ArgumentNullException");
            Assert.IsNotNull(result);
            Assert.AreEqual("ArgumentNullException", result.GetType().Name);
        }

        /// <summary>
        /// Verifies that FindException(string) returns the InnerException if it is of the searched name.
        /// </summary>
        [TestMethod]
        public void FindException_String_InnerMatch_ReturnsException()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            var result = ExceptionExtension.FindException(ex, "ArgumentOutOfRangeException");
            Assert.IsNotNull(result);
            Assert.AreEqual("ArgumentOutOfRangeException", result.GetType().Name);
        }

        /// <summary>
        /// Verifies that FindException(string) returns null if there is no match.
        /// </summary>
        [TestMethod]
        public void FindException_String_NoMatch_ReturnsNull()
        {
            var ex = new Exception();
            var result = ExceptionExtension.FindException(ex, "ArgumentException");
            Assert.IsNull(result);
        }

        /// <summary>
        /// Verifies that FindException(string) handles null exceptions and null name.
        /// </summary>
        [TestMethod]
        public void FindException_String_NullInputs_ReturnsNull()
        {
            Exception? ex = null;
            var result1 = ExceptionExtension.FindException(ex, "Exception");
            Assert.IsNull(result1);
            var ex2 = new Exception();
            var result2 = ExceptionExtension.FindException(ex2, (string)null);
            Assert.IsNull(result2);
            var result3 = ExceptionExtension.FindException(ex2, "");
            Assert.IsNull(result3);
        }

        /// <summary>
        /// Verifies that FindException<T>() returns the exception if it is of the searched type.
        /// </summary>
        [TestMethod]
        public void FindException_Generic_SimpleMatch_ReturnsException()
        {
            var ex = new ArgumentNullException();
            var result = ex.FindException<ArgumentNullException>();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentNullException));
        }

        /// <summary>
        /// Verifies that FindException<T>() returns the InnerException if it is of the searched type.
        /// </summary>
        [TestMethod]
        public void FindException_Generic_InnerMatch_ReturnsException()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            var result = ex.FindException<ArgumentOutOfRangeException>();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentOutOfRangeException));
        }

        /// <summary>
        /// Verifies that FindException<T>() returns null if there is no match.
        /// </summary>
        [TestMethod]
        public void FindException_Generic_NoMatch_ReturnsNull()
        {
            var ex = new Exception();
            var result = ex.FindException<ArgumentException>();
            Assert.IsNull(result);
        }

        /// <summary>
        /// Verifies that FindException<T>() handles null exceptions.
        /// </summary>
        [TestMethod]
        public void FindException_Generic_NullInput_ReturnsNull()
        {
            Exception? ex = null;
            var result = ExceptionExtension.FindException<Exception>(ex);
            Assert.IsNull(result);
        }

        /// <summary>
        /// Verifies that FindException works with inheritance hierarchy.
        /// </summary>
        [TestMethod]
        public void FindException_InheritanceHierarchy_ReturnsFirstMatch()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            var resultType = ExceptionExtension.FindException(ex, typeof(ArgumentException));
            Assert.IsNotNull(resultType);
            Assert.IsInstanceOfType(resultType, typeof(ArgumentOutOfRangeException));
            var resultGeneric = ex.FindException<ArgumentException>();
            Assert.IsNotNull(resultGeneric);
            Assert.IsInstanceOfType(resultGeneric, typeof(ArgumentOutOfRangeException));
            var resultString = ExceptionExtension.FindException(ex, "ArgumentOutOfRangeException");
            Assert.IsNotNull(resultString);
            Assert.AreEqual("ArgumentOutOfRangeException", resultString.GetType().Name);
        }

        #endregion FindException Tests
    }
}