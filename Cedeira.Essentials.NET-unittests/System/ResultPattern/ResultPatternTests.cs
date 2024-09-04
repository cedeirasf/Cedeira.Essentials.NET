

using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.System.ResultPattern.Factories;

namespace Cedeira.Essentials.NET_unittests.System.ResultPattern
{
    /// <summary>
    /// Contiene pruebas unitarias para la implementación de patrones de resultado en la clase <vea cref="ResultFactory"/>.
    /// </summary>
    [TestClass]
    public class ResultPatternTests
    {
        private IResultFactory _resultFactory;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <ver cref="ResultPatternTests"/> y configura <ver cref="_resultFactory"/> con una nueva instancia de <ver cref="ResultFactory"/>.
        /// </summary>
        public ResultPatternTests()
        {
            _resultFactory = new ResultFactory();
        }

        /// <summary>
        /// Prueba que un resultado exitoso tenga el estado y valor correspondiente.
        /// </summary>
        [TestMethod]
        public void SuccessResult_ShouldHaveCorrectStatusAndValue()
        {
            var expectedValue = "Operación completada exitosamente";

            var result = _resultFactory.Success(expectedValue);

            Assert.IsTrue(result.IsSuccess());
            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual(expectedValue, ((SuccessResult<string>)result).SuccessValue);
        }

        /// <summary>
        /// Prueba que un resultado de advertencia tenga el estado y mensaje correspondiente.
        /// </summary>
        [TestMethod]
        public void WarningResult_ShouldHaveCorrectStatusAndMessage()
        {
            var expectedValue = "Operación completada con advertencias";
            var expectedMessage = "Esto es una advertencia";

            var result = _resultFactory.Warning(expectedValue, expectedMessage);

            Assert.IsTrue(result.IsWarning());
            Assert.AreEqual(ResultStatus.Warning, result.Status);
            Assert.AreEqual(expectedValue, ((WarningResult<string>)result).SuccessValue);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        /// <summary>
        /// Prueba que un resultado fallido tenga el estado y mensaje correspondiente.
        /// </summary>
        [TestMethod]
        public void FailureResult_ShouldHaveCorrectStatusAndMessage()
        {
            var expectedMessage = "Operación fallida";

            var result = _resultFactory.Failure<string>(expectedMessage);

            Assert.IsTrue(result.IsFailure());
            Assert.AreEqual(ResultStatus.Failure, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        /// <summary>
        /// Prueba que un resultado exitoso con tipo de fallo tenga el estado y valor correspondiente.
        /// </summary>
        [TestMethod]
        public void SuccessResultWithFailureType_ShouldHaveCorrectStatusAndValue()
        {
            var expectedValue = "Operación completada exitosamente";

            var result = _resultFactory.Success<string, string>(expectedValue);

            Assert.IsTrue(result.IsSuccess());
            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual(expectedValue, result.SuccessValue);
        }

        /// <summary>
        /// Prueba que un resultado de advertencia con tipo de fallo tenga el estado y mensaje correspondiente.
        /// </summary>
        [TestMethod]
        public void WarningResultWithFailureType_ShouldHaveCorrectStatusAndMessage()
        {
            var expectedValue = "Operación completada con advertencias";
            var expectedMessage = "Esto es una advertencia";

            var result = _resultFactory.Warning<string, string>(expectedValue, expectedMessage);

            Assert.IsTrue(result.IsWarning());
            Assert.AreEqual(ResultStatus.Warning, result.Status);
            Assert.AreEqual(expectedValue, result.SuccessValue);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        /// <summary>
        /// Prueba que un resultado fallido con tipo de fallo tenga el estado y valores correspondientes.
        /// </summary>
        [TestMethod]
        public void FailureResultWithFailureType_ShouldHaveCorrectStatusAndValues()
        {
            var expectedFailureValue = "Valor de operación fallida";
            var expectedMessage = "Operación fallida";

            var result = _resultFactory.Failure<string, string>(expectedFailureValue, expectedMessage);

            Assert.IsTrue(result.IsFailure());
            Assert.AreEqual(ResultStatus.Failure, result.Status);
            Assert.AreEqual(expectedFailureValue, result.FailureValue);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        /// <summary>
        /// Prueba que se lance <vea cref="InvalidOperationException"/> al acceder a <vea cref="IResult{TSuccess, TFailure}.FailureValue"/> en un resultado exitoso con tipo de fallo.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SuccessResultWithFailureType_ShouldThrowExceptionOnFailureValueAccess()
        {
            var result = _resultFactory.Success<string, string>("Operación completada exitosamente");

            // Intentar acceder a FailureValue debe lanzar una excepción
            var failureValue = result.FailureValue;
        }

        /// <summary>
        /// Prueba que se lance <vea cref="InvalidOperationException"/> al acceder a <vea cref="IResult{TSuccess, TFailure}.FailureValue"/> en un resultado de advertencia con tipo de fallo.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WarningResultWithFailureType_ShouldThrowExceptionOnFailureValueAccess()
        {
            var result = _resultFactory.Warning<string, string>("Operación completada con advertencias", "Esto es una advertencia");

            // Intentar acceder a FailureValue debe lanzar una excepción
            var failureValue = result.FailureValue;
        }

        /// <summary>
        /// Prueba que se lance <vea cref="InvalidOperationException"/> al acceder a <vea cref="IResult{TSuccess}.SuccessValue"/> en un resultado fallido.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailureResult_ShouldThrowExceptionOnSuccessValueAccess()
        {
            var result = _resultFactory.Failure<string>("Operación fallida");

            // Intentar acceder a SuccessValue debe lanzar una excepción
            var successValue = result.SuccessValue;
        }

        /// <summary>
        /// Prueba que se lance <vea cref="InvalidOperationException"/> al acceder a <vea cref="IResult{TSuccess, TFailure}.SuccessValue"/> en un resultado fallido con tipo de fallo.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailureResultWithFailureType_ShouldThrowExceptionOnSuccessValueAccess()
        {
            var result = _resultFactory.Failure<string, string>("Valor de operación fallida", "Operación fallida");

            // Intentar acceder a SuccessValue debe lanzar una excepción
            var successValue = result.SuccessValue;
        }


        /// <summary>
        /// Prueba que un resultado exitoso sin valor asociado tenga el estado correcto.
        /// </summary>
        [TestMethod]
        public void SuccessResultWithoutValue_ShouldHaveCorrectStatus()
        {
            var result = _resultFactory.Success();

            Assert.IsTrue(result.IsSuccess());
            Assert.AreEqual(ResultStatus.Success, result.Status);
        }

        /// <summary>
        /// Prueba que un resultado de advertencia con mensaje tenga el estado y mensaje correctos.
        /// </summary>
        [TestMethod]
        public void WarningResultWithMessage_ShouldHaveCorrectStatusAndMessage()
        {
            var expectedMessage = "Esto es una advertencia";

            var result = _resultFactory.Warning(expectedMessage);

            Assert.IsTrue(result.IsWarning());
            Assert.AreEqual(ResultStatus.Warning, result.Status);
            Assert.AreEqual(expectedMessage, ((WarningResult)result).Message);
        }

        /// <summary>
        /// Prueba que un resultado de fallo con mensaje de error tenga el estado y mensaje correctos.
        /// </summary>
        [TestMethod]
        public void FailureResultWithErrorMessage_ShouldHaveCorrectStatusAndMessage()
        {
            var expectedMessage = "Operación fallida";

            var result = _resultFactory.Failure(expectedMessage);

            Assert.IsTrue(result.IsFailure());
            Assert.AreEqual(ResultStatus.Failure, result.Status);
            Assert.AreEqual(expectedMessage, ((FailureResult)result).Message);
        }
    }
}
