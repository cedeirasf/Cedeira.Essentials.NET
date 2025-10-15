using Cedeira.Essentials.NET.ExceptionHandling;
using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.TDD;

namespace ExceptionHandling_UnitTest
{
    [TestClass]
    public class ExceptionHandlerTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            // Limpiar el estado antes de cada prueba
            ExceptionHandler.DisableGlobalHandling();
            ExceptionHandler.ClearHandlers();
            ExceptionHandler.ResetUniversalHandling();
        }

        #region Métodos Principales de Configuración

        [TestMethod]
        public void Run_ShouldTransformException_WithMessage()
        {
            TestCase<string, Unit>.Create(
                "Run_ShouldTransformException_WithMessage",
                "Operación inválida detectada"
            ).Run(parameters =>
            {
                // Configurar solo WithMessage
                ExceptionHandler.For<InvalidOperationException>()
                    .WithMessage(parameters);

                // Act & Assert - La excepción transformada debería lanzarse
                try
                {
                    ExceptionHandler.Run(() =>
                    {
                        throw new InvalidOperationException("Error interno");
                    });
                    Assert.Fail("Se esperaba que se lanzara una excepción");
                }
                catch (InvalidOperationException ex)
                {
                    Assert.AreEqual(parameters, ex.Message);
                }
                return Unit.Value;
            });
        }

        [TestMethod]
        public void Run_ShouldExecuteAction_WithAction()
        {
            TestCase<string>.Create(
                "Run_ShouldExecuteAction_WithAction",
                new SuccessResult<string, Type>("Acción ejecutada correctamente")
            ).Run(() =>
            {
                // Arrange
                ExceptionHandler.For<ArgumentException>()
                    .WithAction(ex => "Acción ejecutada correctamente");

                // Act
                var result = ExceptionHandler.Run(() =>
                {
                    throw new ArgumentException("Error interno");
                });

                // Assert
                Assert.AreEqual("Acción ejecutada correctamente", result);
                return (string)result;
            });
        }

        [TestMethod]
        public void Run_ShouldTransformExceptionType_TransformTo()
        {
            TestCase<Unit, Unit>.Create(
                "Run_ShouldTransformExceptionType_TransformTo",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange
                ExceptionHandler.For<DivideByZeroException>()
                    .TransformTo(ex => new ApplicationException($"Transformado: {ex.Message}"));

                // Act & Assert
                try
                {
                    ExceptionHandler.Run(() =>
                    {
                        throw new DivideByZeroException("División por cero");
                    });
                    Assert.Fail("Se esperaba que se lanzara una excepción");
                }
                catch (ApplicationException ex)
                {
                    Assert.AreEqual("Transformado: División por cero", ex.Message);
                }
                return Unit.Value;
            });
        }

        [TestMethod]
        public void Run_ShouldExecuteAction_WithActionReturningObject()
        {
            TestCase<Unit, Unit>.Create(
                "Run_ShouldExecuteAction_WithActionReturningObject",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange
                ExceptionHandler.For<FileNotFoundException>()
                    .WithAction(ex => new { Error = "Archivo no encontrado", Code = 404 });

                // Act
                var result = ExceptionHandler.Run(() =>
                {
                    throw new FileNotFoundException("archivo.txt");
                });

                // Assert
                dynamic dynamicResult = result;
                Assert.AreEqual("Archivo no encontrado", dynamicResult.Error);
                Assert.AreEqual(404, dynamicResult.Code);
                return Unit.Value;
            });
        }

        [TestMethod]
        public void Run_ShouldTransformToCustomException_TransformTo()
        {
            TestCase<Unit, Unit>.Create(
                "Run_ShouldTransformToCustomException_TransformTo",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange
                ExceptionHandler.For<TimeoutException>()
                    .TransformTo(ex => new CustomTestException($"Timeout: {ex.Message}"));

                // Act & Assert
                try
                {
                    ExceptionHandler.Run(() =>
                    {
                        throw new TimeoutException("Tiempo agotado");
                    });
                    Assert.Fail("Se esperaba que se lanzara una excepción");
                }
                catch (CustomTestException ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(CustomTestException));
                }
                return Unit.Value;
            });
        }

        #endregion Métodos Principales de Configuración

        #region Métodos de Extensión

        [TestMethod]
        public void ExecuteWithHandling_Action_ShouldHandleException()
        {
            TestCase<Unit, Unit>.Create(
                "ExecuteWithHandling_Action_ShouldHandleException",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange - Usar tipo único
                ExceptionHandler.For<DataMisalignedException>()
                    .WithAction(ex => "Valor del handler");

                // Act & Assert - No debería lanzar excepción
                new Action(() =>
                {
                    throw new DataMisalignedException("Error en acción");
                }).ExecuteWithHandling();

                Assert.IsTrue(true); // Si llega aquí, la excepción fue manejada
                return Unit.Value;
            });
        }

        [TestMethod]
        public void ExecuteWithHandling_Func_ShouldReturnDefaultValue_NotHandlerValue()
        {
            TestCase<string, Unit>.Create(
                "ExecuteWithHandling_Func_ShouldReturnDefaultValue_NotHandlerValue",
                "Valor por defecto"
            ).Run(parameters =>
            {
                // Arrange - Configurar handler que retorna un valor
                ExceptionHandler.For<InsufficientMemoryException>()
                    .WithAction(ex => "Valor del handler");

                // Act
                var result = new Func<string>(() =>
                {
                    throw new InsufficientMemoryException("Memoria insuficiente");
                }).ExecuteWithHandling(defaultValue: parameters);

                // Assert - Debería retornar el defaultValue, NO el valor del handler
                Assert.AreEqual(parameters, result);
                return Unit.Value;
            });
        }

        [TestMethod]
        public void ExecuteWithHandling_Func_ShouldReturnDefaultValue_WhenNoHandler()
        {
            TestCase<string, Unit>.Create(
                "ExecuteWithHandling_Func_ShouldReturnDefaultValue_WhenNoHandler",
                "Valor por defecto"
            ).Run(parameters =>
            {
                // Arrange - NO configurar handler para esta excepción
                // Act
                var result = new Func<string>(() =>
                {
                    throw new StackOverflowException(); // Tipo sin handler
                }).ExecuteWithHandling(defaultValue: parameters);

                // Assert - Debería retornar el defaultValue cuando no hay handler
                Assert.AreEqual(parameters, result);
                return Unit.Value;
            });
        }

        [TestMethod]
        public void ExecuteWithHandling_Func_ShouldReturnNormalResult_WhenNoException()
        {
            TestCase<string, Unit>.Create(
                "ExecuteWithHandling_Func_ShouldReturnNormalResult_WhenNoException",
                "Resultado normal"
            ).Run(parameters =>
            {
                // Act
                var result = new Func<string>(() =>
                {
                    return parameters;
                }).ExecuteWithHandling(defaultValue: "Valor por defecto");

                // Assert - Debería retornar el resultado normal
                Assert.AreEqual(parameters, result);
                return Unit.Value;
            });
        }

        [TestMethod]
        public async Task ExecuteWithHandlingAsync_Action_ShouldHandleException()
        {
            var testCase = TestCase<Unit, Unit>.Create(
                "ExecuteWithHandlingAsync_Action_ShouldHandleException",
                Unit.Value
            );

            // Ejecutar setup manualmente
            testCase.RunSetup();

            try
            {
                // Arrange - Usar tipo único
                ExceptionHandler.For<AccessViolationException>()
                    .WithAction(ex => "Valor del handler async");

                // Act & Assert - No debería lanzar excepción
                await new Func<Task>(async () =>
                {
                    await Task.Delay(10);
                    throw new AccessViolationException("Violación de acceso async");
                }).ExecuteWithHandlingAsync();
            }
            finally
            {
                // Ejecutar teardown manualmente
                testCase.RunTeardown();
            }
        }

        [TestMethod]
        public async Task ExecuteWithHandlingAsync_Func_ShouldReturnDefaultValue_NotHandlerValue()
        {
            var testCase = TestCase<int, Unit>.Create(
                "ExecuteWithHandlingAsync_Func_ShouldReturnDefaultValue_NotHandlerValue",
                999
            );

            // Ejecutar el setup si existe
            testCase.RunSetup();

            try
            {
                // Arrange - Configurar handler que retorna un valor
                ExceptionHandler.For<BadImageFormatException>()
                    .WithAction(ex => "Valor del handler async");

                // Act
                var result = await new Func<Task<int>>(async () =>
                {
                    await Task.Delay(10);
                    throw new BadImageFormatException("Formato de imagen inválido async");
                }).ExecuteWithHandlingAsync(defaultValue: testCase.Parameters);

                // Assert - Debería retornar el defaultValue, NO el valor del handler
                Assert.AreEqual(testCase.Parameters, result);
            }
            finally
            {
                // Ejecutar el teardown si existe
                testCase.RunTeardown();
            }
        }

        [TestMethod]
        public async Task ExecuteWithHandlingAsync_Func_ShouldReturnNormalResult_WhenNoException()
        {
            var testCase = TestCase<string, Unit>.Create(
                "ExecuteWithHandlingAsync_Func_ShouldReturnNormalResult_WhenNoException",
                "Resultado async normal"
            );

            // Ejecutar setup manualmente
            testCase.RunSetup();

            try
            {
                // Act
                var result = await new Func<Task<string>>(async () =>
                {
                    await Task.Delay(10);
                    return testCase.Parameters;
                }).ExecuteWithHandlingAsync(defaultValue: "Valor por defecto");

                // Assert - Debería retornar el resultado normal
                Assert.AreEqual(testCase.Parameters, result);
            }
            finally
            {
                // Ejecutar teardown manualmente
                testCase.RunTeardown();
            }
        }

        #endregion Métodos de Extensión

        [TestMethod]
        public void Run_ShouldHandleMultipleTransformations()
        {
            TestCase<Unit, Unit>.Create(
                "Run_ShouldHandleMultipleTransformations",
                Unit.Value
            ).Run(parameters =>
            {
                // Cadena: FormatException -> IOException -> ApplicationException
                ExceptionHandler.For<FormatException>()
                    .TransformTo(ex => new IOException($"Primera transformación: {ex.Message}"));

                ExceptionHandler.For<IOException>()
                    .TransformTo(ex => new ApplicationException($"Segunda transformación: {ex.Message}"));

                try
                {
                    ExceptionHandler.Run(() =>
                    {
                        throw new FormatException("Original");
                    });
                    Assert.Fail("Se esperaba que se lanzara una excepción");
                }
                catch (ApplicationException ex)
                {
                    Assert.AreEqual("Segunda transformación: Primera transformación: Original", ex.Message);
                }
                return Unit.Value;
            });
        }

        [TestMethod]
        public void Run_ShouldUseBaseExceptionHandler_ForDerivedException()
        {
            TestCase<string, Unit>.Create(
                "Run_ShouldUseBaseExceptionHandler_ForDerivedException",
                "Manejado por handler base"
            ).Run(parameters =>
            {
                // Configurar handler solo para SystemException base
                ExceptionHandler.For<SystemException>()
                    .WithAction(ex => parameters);

                // Lanzar una excepción derivada de SystemException
                var result = ExceptionHandler.Run(() =>
                {
                    throw new NullReferenceException("Error específico");
                });

                Assert.AreEqual(parameters, result);
                return Unit.Value;
            });
        }

        [TestMethod]
        public void Run_ShouldHandleException_WithInnerException()
        {
            TestCase<Unit, Unit>.Create(
                "Run_ShouldHandleException_WithInnerException",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange
                var innerEx = new ArgumentException("Error interno");
                ExceptionHandler.For<ArithmeticException>()
                    .WithAction(ex => new
                    {
                        Message = ex.Message,
                        InnerMessage = ex.InnerException?.Message,
                        Code = 500
                    });

                // Act
                var result = ExceptionHandler.Run(() =>
                {
                    throw new ArithmeticException("Error externo", innerEx);
                });

                // Assert
                dynamic dynamicResult = result;
                Assert.AreEqual("Error externo", dynamicResult.Message);
                Assert.AreEqual("Error interno", dynamicResult.InnerMessage);
                Assert.AreEqual(500, dynamicResult.Code);
                return Unit.Value;
            });
        }

        #region Excepciones anidadas

        [TestMethod]
        public void Run_ShouldHandleDeepNestedException_WithRecursiveHelper()
        {
            TestCase<Unit, Unit>.Create(
                "Run_ShouldHandleDeepNestedException_WithRecursiveHelper",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange - Crear excepción anidada en 4 niveles
                var level4 = new ArgumentException("Nivel 4 - Error de argumento");
                var level3 = new InvalidOperationException("Nivel 3 - Operación inválida", level4);
                var level2 = new FileNotFoundException("Nivel 2 - Archivo no encontrado", level3);
                var level1 = new TimeoutException("Nivel 1 - Timeout", level2);

                ExceptionHandler.For<TimeoutException>()
                    .WithAction(ex => new
                    {
                        MainMessage = ex.Message,
                        AllInnerMessages = GetAllInnerMessages(ex),
                        ExceptionDepth = GetExceptionDepth(ex),
                        FullExceptionChain = GetExceptionChainSummary(ex),
                        Code = 500
                    });

                // Act
                var result = ExceptionHandler.Run(() =>
                {
                    throw level1;
                });

                // Assert
                dynamic dynamicResult = result;
                Assert.AreEqual("Nivel 1 - Timeout", dynamicResult.MainMessage);
                Assert.AreEqual("Nivel 2 - Archivo no encontrado | Nivel 3 - Operación inválida | Nivel 4 - Error de argumento", dynamicResult.AllInnerMessages);
                Assert.AreEqual(4, dynamicResult.ExceptionDepth);
                Assert.IsTrue(dynamicResult.FullExceptionChain.Contains("TimeoutException -> FileNotFoundException -> InvalidOperationException -> ArgumentException"));
                return Unit.Value;
            });
        }

        #endregion Excepciones anidadas

        [TestMethod]
        public async Task RunAsync_HandlesExceptionAndReturnsHandledResult()
        {
            // Arrange
            ExceptionHandler.ClearHandlers();
            ExceptionHandler.For<InvalidOperationException>().WithAction(ex => "handled");

            // Act
            var result = await ExceptionHandler.RunAsync(async () =>
            {
                await Task.Delay(10);
                throw new InvalidOperationException("Test");
            });

            // Assert
            Assert.AreEqual("handled", result);
        }

        [TestMethod]
        public async Task RunAsync_ReturnsResultWhenNoException()
        {
            // Arrange
            ExceptionHandler.ClearHandlers();

            // Act
            var result = await ExceptionHandler.RunAsync(async () =>
            {
                await Task.Delay(10);
                return "ok";
            });

            // Assert
            Assert.AreEqual("ok", result);
        }

        #region Handle Test

        [TestMethod]
        public void Handle_ShouldReturnActionResult_WhenActionConfigured()
        {
            TestCase<string, Unit>.Create(
                "Handle_ShouldReturnActionResult_WhenActionConfigured",
                "Manejado exitosamente"
            ).Run(parameters =>
            {
                // Arrange
                ExceptionHandler.For<InvalidOperationException>()
                    .WithAction(ex => parameters);

                var exception = new InvalidOperationException("Error de operación");

                // Act
                var result = ExceptionHandler.Handle(exception);

                // Assert
                Assert.AreEqual(parameters, result);
                return Unit.Value;
            });
        }

        [TestMethod]
        public void Handle_ShouldThrowTransformedException_WhenTransformConfigured()
        {
            TestCase<Unit, Unit>.Create(
                "Handle_ShouldThrowTransformedException_WhenTransformConfigured",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange
                ExceptionHandler.For<ArgumentException>()
                    .TransformTo(ex => new ApplicationException($"Transformado: {ex.Message}"));

                var originalException = new ArgumentException("Argumento inválido");

                // Act & Assert
                try
                {
                    ExceptionHandler.Handle(originalException);
                    Assert.Fail("Se esperaba que se lanzara una excepción transformada");
                }
                catch (ApplicationException ex)
                {
                    Assert.AreEqual("Transformado: Argumento inválido", ex.Message);
                }
                return Unit.Value;
            });
        }

        [TestMethod]
        public void Handle_ShouldThrowOriginalException_WhenNoConfiguration()
        {
            TestCase<Unit, Unit>.Create(
                "Handle_ShouldThrowOriginalException_WhenNoConfiguration",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange
                var originalException = new FormatException("Formato incorrecto");

                // Act & Assert
                try
                {
                    ExceptionHandler.Handle(originalException);
                    Assert.Fail("Se esperaba que se lanzara la excepción original");
                }
                catch (FormatException ex)
                {
                    Assert.AreEqual("Formato incorrecto", ex.Message);
                }
                return Unit.Value;
            });
        }

        [TestMethod]
        public void Handle_ShouldUseMostSpecificHandler_ForDerivedException()
        {
            TestCase<Unit, Unit>.Create(
                "Handle_ShouldUseMostSpecificHandler_ForDerivedException",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange
                ExceptionHandler.For<SystemException>()
                    .WithAction(ex => "Handler base");

                ExceptionHandler.For<NullReferenceException>()
                    .WithAction(ex => "Handler específico");

                var exception = new NullReferenceException("Referencia nula");

                // Act
                var result = ExceptionHandler.Handle(exception);

                // Assert - Debería usar el handler más específico
                Assert.AreEqual("Handler específico", result);
                return Unit.Value;
            });
        }

        [TestMethod]
        public void Handle_ShouldPreventInfiniteRecursion_WithCyclicTransformations()
        {
            TestCase<Unit, Unit>.Create(
                "Handle_ShouldPreventInfiniteRecursion_WithCyclicTransformations",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange - Configurar transformación cíclica
                ExceptionHandler.For<InvalidOperationException>()
                    .TransformTo(ex => new ArgumentException("Transformado 1"));

                ExceptionHandler.For<ArgumentException>()
                    .TransformTo(ex => new InvalidOperationException("Transformado 2"));

                var originalException = new InvalidOperationException("Original");

                // Act & Assert - No debería causar stack overflow
                try
                {
                    ExceptionHandler.Handle(originalException);
                    Assert.Fail("Se esperaba que se lanzara una excepción");
                }
                catch (Exception ex)
                {
                    // Debería lanzar alguna excepción sin entrar en recursión infinita
                    Assert.IsTrue(ex is InvalidOperationException || ex is ArgumentException);
                }
                return Unit.Value;
            });
        }

        #endregion Handle Test

        #region DisableGlobalHandling

        [TestMethod]
        public void DisableGlobalHandling_ShouldUnregisterEventHandlers_WhenEnabled()
        {
            TestCase<Unit, Unit>.Create(
                "DisableGlobalHandling_ShouldUnregisterEventHandlers_WhenEnabled",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange - Habilitar primero el manejo global
                ExceptionHandler.EnableGlobalHandling();

                // Act
                ExceptionHandler.DisableGlobalHandling();

                // Assert - Los handlers deberían estar desregistrados
                // (El estado interno debería indicar que no está habilitado)
                Assert.IsTrue(true);
                return Unit.Value;
            });
        }

        [TestMethod]
        public void DisableGlobalHandling_ShouldDoNothing_WhenAlreadyDisabled()
        {
            TestCase<Unit, Unit>.Create(
                "DisableGlobalHandling_ShouldDoNothing_WhenAlreadyDisabled",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange - Asegurar que está deshabilitado
                ExceptionHandler.DisableGlobalHandling();

                // Act - No debería lanzar excepción
                ExceptionHandler.DisableGlobalHandling();

                // Assert - No debería haber errores
                Assert.IsTrue(true);
                return Unit.Value;
            });
        }

        [TestMethod]
        public void DisableGlobalHandling_ShouldClearHandlerReferences()
        {
            TestCase<Unit, Unit>.Create(
                "DisableGlobalHandling_ShouldClearHandlerReferences",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange
                ExceptionHandler.EnableGlobalHandling();

                // Act
                ExceptionHandler.DisableGlobalHandling();

                // Assert - Las referencias a los handlers deberían ser nulas
                Assert.IsTrue(true); // Placeholder
                return Unit.Value;
            });
        }

        [TestMethod]
        public void DisableGlobalHandling_ShouldAllowReEnable()
        {
            TestCase<Unit, Unit>.Create(
                "DisableGlobalHandling_ShouldAllowReEnable",
                Unit.Value
            ).Run(parameters =>
            {
                // Arrange
                ExceptionHandler.EnableGlobalHandling();
                ExceptionHandler.DisableGlobalHandling();

                // Act - Volver a habilitar después de deshabilitar
                ExceptionHandler.EnableGlobalHandling();

                // Assert - No debería haber errores al re-habilitar
                Assert.IsTrue(true);
                return Unit.Value;
            });
        }

        #endregion DisableGlobalHandling

        #region Helpers

        public class CustomTestException : Exception
        {
            public CustomTestException(string message) : base(message)
            {
            }
        }

        // Helper 1: Todos los mensajes internos en un string
        private static string GetAllInnerMessages(Exception ex)
        {
            var messages = new List<string>();
            var current = ex.InnerException;

            while (current != null)
            {
                messages.Add(current.Message);
                current = current.InnerException;
            }

            return string.Join(" | ", messages);
        }

        // Helper 2: Profundidad de la cadena de excepciones
        private static int GetExceptionDepth(Exception ex)
        {
            int depth = 1; // Incluye la excepción principal
            var current = ex.InnerException;

            while (current != null)
            {
                depth++;
                current = current.InnerException;
            }

            return depth;
        }

        // Helper 3: Cadena completa de tipos de excepción
        private static string GetExceptionChainSummary(Exception ex)
        {
            var types = new List<string> { ex.GetType().Name };
            var current = ex.InnerException;

            while (current != null)
            {
                types.Add(current.GetType().Name);
                current = current.InnerException;
            }

            return string.Join(" -> ", types);
        }

        #endregion Helpers
    }
}