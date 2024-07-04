using Cedeira.Essentials.NET.Extensions.System.Exceptions;

namespace Cedeira.Essentials.NET_unittests.Extensions.System.Exceptions
{
    [TestClass]
    public class ExceptionExtensionTests
    {

        [TestMethod]
        public void FullMessage_WithoutInnerException_ReturnsCorrectMessage()
        {
            var exceptionMessage = "This is a test exception";
            var exception = new Exception(exceptionMessage);

            var result = ExceptionExtension.FullMessage(exception);

            Assert.AreEqual(exceptionMessage, result);
        }

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
    }
}