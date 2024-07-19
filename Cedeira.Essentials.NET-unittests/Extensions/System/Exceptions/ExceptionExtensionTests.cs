using Cedeira.Essentials.NET.Extensions.System.Exceptions;

namespace Cedeira.Essentials.NET_unittests.Extensions.System.Exceptions
{
    /// <summary>
    /// Contiene pruebas unitarias para la clase <vea cref="ExceptionExtension"/>.
    /// </summary>
    [TestClass]
    public class ExceptionExtensionTests
    {
        /// <summary>
        /// Verifica que el método <vea cref="ExceptionExtension.FullMessage(Exception)"/> 
        /// devuelve el mensaje correcto cuando no hay excepción interna.
        /// </summary>
        [TestMethod]
        public void FullMessage_WithoutInnerException_ReturnsCorrectMessage()
        {
            var exceptionMessage = "This is a test exception";
            var exception = new Exception(exceptionMessage);

            var result = ExceptionExtension.FullMessage(exception);

            Assert.AreEqual(exceptionMessage, result);
        }

        /// <summary>
        /// Verifica que el método <vea cref="ExceptionExtension.FullMessage(Exception)"/> 
        /// devuelve el mensaje combinado cuando hay una excepción interna.
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
        /// Verifica que el método <vea cref="ExceptionExtension.FullMessage(Exception)"/> 
        /// devuelve todos los mensajes combinados cuando hay múltiples excepciones internas.
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
        /// Verifica que el método <vea cref="ExceptionExtension.FullMessage(Exception, string)"/> 
        /// devuelve los mensajes combinados utilizando separadores personalizados.
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
        /// Verifica que el método <vea cref="ExceptionExtension.LastExceptionMessage(Exception)"/> 
        /// devuelve el mensaje correcto cuando no hay excepción interna.
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
        /// Verifica que el método <vea cref="ExceptionExtension.LastExceptionMessage(Exception)"/> 
        /// devuelve el mensaje de la última excepción anidada.
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
        /// Verifica que el método <vea cref="ExceptionExtension.LastExceptionMessage(Exception)"/> 
        /// devuelve el mensaje de la última excepción anidada cuando hay múltiples excepciones internas.
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
    }
}