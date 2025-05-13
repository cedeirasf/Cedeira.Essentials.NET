using Cedeira.Essentials.NET.Extensions.Exceptions;

namespace Cedeira.Essentials.NET_unittests.Extensions.Exceptions
{
    /// <summary>
    /// Contiene pruebas unitarias para la clase <vea cref="ExceptionExtension"/>.
    /// </summary>
    [TestClass]
    public class ExceptionExtensionTests
    {
        /// <summary>
        /// Verifica que el m�todo <vea cref="ExceptionExtension.FullMessage(Exception)"/> 
        /// devuelve el mensaje correcto cuando no hay excepci�n interna.
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
        /// Verifica que el m�todo <vea cref="ExceptionExtension.FullMessage(Exception)"/> 
        /// devuelve el mensaje combinado cuando hay una excepci�n interna.
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
        /// Verifica que el m�todo <vea cref="ExceptionExtension.FullMessage(Exception)"/> 
        /// devuelve todos los mensajes combinados cuando hay m�ltiples excepciones internas.
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
        /// Verifica que el m�todo <vea cref="ExceptionExtension.FullMessage(Exception, string)"/> 
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
        /// Verifica que el m�todo <vea cref="ExceptionExtension.LastExceptionMessage(Exception)"/> 
        /// devuelve el mensaje correcto cuando no hay excepci�n interna.
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
        /// Verifica que el m�todo <vea cref="ExceptionExtension.LastExceptionMessage(Exception)"/> 
        /// devuelve el mensaje de la �ltima excepci�n anidada.
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
        /// Verifica que el m�todo <vea cref="ExceptionExtension.LastExceptionMessage(Exception)"/> 
        /// devuelve el mensaje de la �ltima excepci�n anidada cuando hay m�ltiples excepciones internas.
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
        /// Verifica que ContainsException(Type) retorna true si la excepción es del tipo buscado.
        /// </summary>
        [TestMethod]
        public void ContainsException_Type_SimpleMatch_ReturnsTrue()
        {
            var ex = new ArgumentNullException();
            Assert.IsTrue(ExceptionExtension.ContainsException(ex, typeof(ArgumentNullException)));
        }

        /// <summary>
        /// Verifica que ContainsException(Type) retorna true si una InnerException es del tipo buscado.
        /// </summary>
        [TestMethod]
        public void ContainsException_Type_InnerMatch_ReturnsTrue()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            Assert.IsTrue(ExceptionExtension.ContainsException(ex, typeof(ArgumentOutOfRangeException)));
        }

        /// <summary>
        /// Verifica que ContainsException(Type) retorna false si no hay coincidencia.
        /// </summary>
        [TestMethod]
        public void ContainsException_Type_NoMatch_ReturnsFalse()
        {
            var ex = new Exception();
            Assert.IsFalse(ExceptionExtension.ContainsException(ex, typeof(ArgumentException)));
        }

        /// <summary>
        /// Verifica que ContainsException(Type) maneja excepciones nulas y tipo nulo.
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
        /// Verifica que ContainsException(string) retorna true si la excepción es del nombre buscado.
        /// </summary>
        [TestMethod]
        public void ContainsException_String_SimpleMatch_ReturnsTrue()
        {
            var ex = new ArgumentNullException();
            Assert.IsTrue(ex.ContainsException("ArgumentNullException"));
        }

        /// <summary>
        /// Verifica que ContainsException(string) retorna true si una InnerException es del nombre buscado.
        /// </summary>
        [TestMethod]
        public void ContainsException_String_InnerMatch_ReturnsTrue()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            Assert.IsTrue(ex.ContainsException("ArgumentOutOfRangeException"));
        }

        /// <summary>
        /// Verifica que ContainsException(string) retorna false si no hay coincidencia.
        /// </summary>
        [TestMethod]
        public void ContainsException_String_NoMatch_ReturnsFalse()
        {
            var ex = new Exception();
            Assert.IsFalse(ex.ContainsException("ArgumentException"));
        }

        /// <summary>
        /// Verifica que ContainsException(string) maneja excepciones nulas y nombre nulo/vacío.
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
        /// Verifica que ContainsException<T>() retorna true si la excepción es del tipo genérico buscado.
        /// </summary>
        [TestMethod]
        public void ContainsException_Generic_SimpleMatch_ReturnsTrue()
        {
            var ex = new ArgumentNullException();
            Assert.IsTrue(ex.ContainsException<ArgumentNullException>());
        }

        /// <summary>
        /// Verifica que ContainsException<T>() retorna true si una InnerException es del tipo genérico buscado.
        /// </summary>
        [TestMethod]
        public void ContainsException_Generic_InnerMatch_ReturnsTrue()
        {
            var inner = new ArgumentOutOfRangeException();
            var ex = new Exception("outer", inner);
            Assert.IsTrue(ex.ContainsException<ArgumentOutOfRangeException>());
        }

        /// <summary>
        /// Verifica que ContainsException<T>() retorna false si no hay coincidencia.
        /// </summary>
        [TestMethod]
        public void ContainsException_Generic_NoMatch_ReturnsFalse()
        {
            var ex = new Exception();
            Assert.IsFalse(ex.ContainsException<ArgumentException>());
        }

        /// <summary>
        /// Verifica que ContainsException<T>() maneja excepciones nulas.
        /// </summary>
        [TestMethod]
        public void ContainsException_Generic_NullInput_ReturnsFalse()
        {
            Exception? ex = null;
            Assert.IsFalse(ex.ContainsException<Exception>());
        }

        /// <summary>
        /// Verifica que ContainsException funciona con jerarquía de herencia.
        /// </summary>
        [TestMethod]
        public void ContainsException_InheritanceHierarchy_ReturnsTrueForBaseAndDerived()
        {
            var ex = new ArgumentOutOfRangeException();
            Assert.IsTrue(ExceptionExtension.ContainsException(ex, typeof(ArgumentException)));
            Assert.IsTrue(ex.ContainsException<ArgumentException>());
            Assert.IsTrue(ex.ContainsException("ArgumentOutOfRangeException"));
        }

        #endregion
    }
}