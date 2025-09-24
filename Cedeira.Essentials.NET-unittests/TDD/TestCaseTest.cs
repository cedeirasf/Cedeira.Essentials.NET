using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.TDD;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Cedeira.Essentials.NET_unittests.TDD
{
    [TestClass]
    public class PrimitiveSamplesTest
    {
        /// <summary>
        /// Pruebas de creación de TestCase
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TestCaseCreation()
        {
            // TestCase sin parámetros
            var testCase = TestCase<string>.Create(
                title: "test a Task as paremeter",
                new SuccessResult<string, Type>("10"));

            Assert.IsNotNull(testCase, "Test case is null");
            Assert.AreEqual("test a Task as paremeter", testCase.Title, "Test case title mismatch");
            Assert.IsInstanceOfType(testCase.Result, typeof(SuccessResult<string, Type>), "Result is not of type SuccessResult<string, Type>");
            Assert.IsNotNull(testCase.Result, "Result is null");
            Assert.AreEqual("10", testCase.Result.SuccessValue.ToString(), "Result value mismatch");

            // TestCase con parámetros
            var TestCaseWithParams = TestCase<(int A, int B), int>.Create(
                title: "Task<int> add ok",
                (10, 5),
                new SuccessResult<int, Type>(15));

            Assert.IsNotNull(TestCaseWithParams, "Test case with parameters is null");
            Assert.AreEqual("Task<int> add ok", TestCaseWithParams.Title, "Test case with parameters title mismatch");
            Assert.IsInstanceOfType(TestCaseWithParams.Result, typeof(SuccessResult<int, Type>), "Result is not of type SuccessResult<int, Type>");
            Assert.IsNotNull(TestCaseWithParams.Result, "Result is null");
            Assert.AreEqual(15, TestCaseWithParams.Result.SuccessValue, "Result value mismatch");

            // TestCase con parámetros null
            try
            {
                var TestCaseWithParamsNull = TestCase<int?, int>.Create(
                        title: "Task<int> add ok",
                        parameters: null,
                        new SuccessResult<int, Type>(15));
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException), "Exception type mismatch when creating TestCase with null parameters");
            }
        }

        /// <summary>
        /// Pruebas de funcionalidad de dependencias en TestCase
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TestCase_WithDependency_SingleParameter()
        {
            var testCase = TestCase<string>.Create(
            "Test with single dependency",
            new SuccessResult<string, Type>("test result"));

            var testCaseWithDep = testCase.WithDependency<ITestService>(result =>
            {
                var mock = new Mock<ITestService>();
                mock.Setup(x => x.GetValue()).Returns(result.IsSuccess() ? "success" : "failure");
                return mock;
            });
            Assert.IsNotNull(testCaseWithDep, "TestCase with dependency should not be null");
            Assert.AreSame(testCase, testCaseWithDep, "WithDependency should return same instance");

            var services = new ServiceCollection();

            testCase.RegisterDependencies(services);

            Assert.AreEqual(1, services.Count, "Should register two services");

            var serviceProvider = services.BuildServiceProvider();
            var testService = serviceProvider.GetService<ITestService>();

            Assert.IsNotNull(testService, "ITestService should be registered and resolvable");
        }

        public interface ITestService
        {
            string GetValue();
        }

        [TestMethod]
        [Priority(3)]
        public void TestCase_WithDependency_TwoParameters()
        {
            var testCase = TestCase<(int A, int B), string>.Create(
            "Test with dependency using parameters",
            (10, 20),
            new SuccessResult<string, Type>("30"));

            var testCaseWithDep = testCase.WithDependency<ICalculatorService>((parameters, result) =>
            {
                var mock = new Mock<ICalculatorService>();
                mock.Setup(x => x.Add(parameters.A, parameters.B))
                    .Returns(result.IsSuccess() ? parameters.A + parameters.B : 0);
                return mock;
            });

            Assert.IsNotNull(testCaseWithDep, "TestCase with dependency should not be null");
            Assert.AreSame(testCase, testCaseWithDep, "WithDependency should return same instance");

            var services = new ServiceCollection();

            testCase.RegisterDependencies(services);

            Assert.AreEqual(1, services.Count, "Should register two services");

            var serviceProvider = services.BuildServiceProvider();
            var testService = serviceProvider.GetService<ICalculatorService>();

            Assert.IsNotNull(testService, "ICalculateService should be registered and resolvable");
        }

        public interface ICalculatorService
        {
            int Add(int a, int b);
        }

        /// <summary>
        /// Pruebas de suma de enteros con manejo de nullables y excepciones
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> IntAdd_AllCases()
        {
            yield return new object[] {
                TestCase<(int? A, int? B), int>.Create(
                    "int?/int add: 2 + 3 = 5",
                    (2, 3),
                    new SuccessResult<int, Type>(5))
            };
            yield return new object[] {
                TestCase<(int? A, int? B), int>.Create(
                    "int?/int add overflow",
                    (int.MaxValue, 1),
                    new FailureResult<int, Type>(typeof(OverflowException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<(int? A, int? B), int>.Create(
                    "int? add fails if A is null",
                    (null, 1),
                    new FailureResult<int, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<(int? A, int? B), int>.Create(
                    "int? add fails if B is null",
                    (1, null),
                    new FailureResult<int, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(IntAdd_AllCases), DynamicDataSourceType.Method)]
        public void IntAdd(TestCase<(int? A, int? B), int> tc)
        {
            try
            {
                if (!tc.Parameters.A.HasValue) throw new ArgumentNullException(nameof(tc.Parameters.A));
                if (!tc.Parameters.B.HasValue) throw new ArgumentNullException(nameof(tc.Parameters.B));

                int actual;
                checked { actual = tc.Parameters.A.Value + tc.Parameters.B.Value; }

                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
                Assert.AreEqual(tc.Result.SuccessValue, actual,
                    tc.FailResponse("Sum result mismatch", tc.Result.SuccessValue, actual));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        /// <summary>
        ///  pruebas de concatenación de strings con manejo de nullables y excepciones
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> StringConcat_AllCases()
        {
            yield return new object[] {
                TestCase<(string? L, string? R), string>.Create(
                    "string?/string concat ok",
                    ("hello", " world"),
                    new SuccessResult<string, Type>("hello world"))
            };
            yield return new object[] {
                TestCase<(string? L, string? R), string>.Create(
                    "string? concat fails if L is null",
                    (null, "world"),
                    new FailureResult<string, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<(string? L, string? R), string>.Create(
                    "string? concat fails if R is null",
                    ("hello", null),
                    new FailureResult<string, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<(string? L, string? R), string>.Create(
                    "string? concat fails if both are null",
                    (null, null),
                    new FailureResult<string, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(StringConcat_AllCases), DynamicDataSourceType.Method)]
        public void StringConcat(TestCase<(string? L, string? R), string> tc)
        {
            try
            {
                if (tc.Parameters.L is null) throw new ArgumentNullException(nameof(tc.Parameters.L));
                if (tc.Parameters.R is null) throw new ArgumentNullException(nameof(tc.Parameters.R));

                var actual = string.Concat(tc.Parameters.L, tc.Parameters.R);

                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
                Assert.AreEqual(tc.Result.SuccessValue, actual,
                    tc.FailResponse("Concat result mismatch", tc.Result.SuccessValue, actual));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex, ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        /// <summary>
        /// Pruebas de negación booleana con manejo de nullables
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> BoolNegate_AllCases()
        {
            yield return new object[] {
                TestCase<bool, bool>.Create(
                    "bool?/bool negate: true -> false",
                    true,
                    new SuccessResult<bool, Type>(false))
            };
            yield return new object[] {
                TestCase<bool, bool>.Create(
                    "bool?/bool negate: false -> true",
                    false,
                    new SuccessResult<bool, Type>(true))
            };
        }

        [TestMethod]
        [DynamicData(nameof(BoolNegate_AllCases), DynamicDataSourceType.Method)]
        public void BoolNegate(TestCase<bool, bool> tc)
        {
            try
            {
                var actual = !tc.Parameters;

                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
                Assert.AreEqual(tc.Result.SuccessValue, actual,
                    tc.FailResponse("Negation mismatch", tc.Result.SuccessValue, actual));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        /// <summary>
        /// Pruebas de división de decimales con manejo de nullables y excepciones
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> DecimalDiv_AllCases()
        {
            yield return new object[] {
                TestCase<(decimal? A, decimal? B), decimal>.Create(
                    "decimal?/decimal div ok: 10 / 4 = 2.5",
                    (10m, 4m),
                    new SuccessResult<decimal, Type>(2.5m))
            };
            yield return new object[] {
                TestCase<(decimal? A, decimal? B), decimal>.Create(
                    "decimal?/decimal div by zero -> DivideByZeroException",
                    (5m, 0m),
                    new FailureResult<decimal, Type>(typeof(DivideByZeroException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<(decimal? A, decimal? B), decimal>.Create(
                    "decimal? div fails if A is null",
                    (null, 2m),
                    new FailureResult<decimal, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<(decimal? A, decimal? B), decimal>.Create(
                    "decimal? div fails if B is null",
                    (2m, null),
                    new FailureResult<decimal, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(DecimalDiv_AllCases), DynamicDataSourceType.Method)]
        public void DecimalDivide(TestCase<(decimal? A, decimal? B), decimal> tc)
        {
            try
            {
                if (!tc.Parameters.A.HasValue) throw new ArgumentNullException(nameof(tc.Parameters.A));
                if (!tc.Parameters.B.HasValue) throw new ArgumentNullException(nameof(tc.Parameters.B));

                var actual = tc.Parameters.A.Value / tc.Parameters.B.Value;

                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
                Assert.AreEqual(tc.Result.SuccessValue, actual,
                    tc.FailResponse("Division result mismatch", tc.Result.SuccessValue, actual));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        /// <summary>
        /// Pruebas de parseo de GUID con manejo de excepciones
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GuidParse_AllCases()
        {
            var valid = Guid.NewGuid().ToString();

            yield return new object[] {
                TestCase<string, Guid>.Create(
                    "guid parse ok",
                    valid,
                    new SuccessResult<Guid, Type>(Guid.Parse(valid)))
            };
            yield return new object[] {
                TestCase<string, Guid>.Create(
                    "guid parse fails if invalid",
                    "not-a-guid",
                    new FailureResult<Guid, Type>(typeof(FormatException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(GuidParse_AllCases), DynamicDataSourceType.Method)]
        public void GuidParse(TestCase<string, Guid> tc)
        {
            try
            {
                if (tc.Parameters is null) throw new ArgumentNullException(nameof(tc.Parameters));
                var actual = Guid.Parse(tc.Parameters);

                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
                Assert.AreEqual(tc.Result.SuccessValue, actual,
                    tc.FailResponse("Guid parse mismatch", tc.Result.SuccessValue, actual));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        /// <summary>
        /// Pruebas de suma de días a DateTime con manejo de nullables y excepciones
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> DateTimeAddDays_AllCases()
        {
            var now = DateTime.UtcNow.Date;

            yield return new object[] {
                TestCase<(DateTime? Base, int Days), DateTime>.Create(
                    "datetime?/datetime add days ok: today + 5",
                    (now, 5),
                    new SuccessResult<DateTime, Type>(now.AddDays(5)))
            };
            yield return new object[] {
                TestCase<(DateTime? Base, int Days), DateTime>.Create(
                    "datetime? add days fails if base is null",
                    (null, 1),
                    new FailureResult<DateTime, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(DateTimeAddDays_AllCases), DynamicDataSourceType.Method)]
        public void DateTimeAddDays(TestCase<(DateTime? Base, int Days), DateTime> tc)
        {
            try
            {
                if (tc.Parameters.Base is null) throw new ArgumentNullException(nameof(tc.Parameters.Base));
                var actual = tc.Parameters.Base.Value.AddDays(tc.Parameters.Days);

                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
                Assert.AreEqual(tc.Result.SuccessValue, actual,
                    tc.FailResponse("DateTime.AddDays mismatch", tc.Result.SuccessValue, actual));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        /// <summary>
        /// Pruebas de método void con manejo de excepciones
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> VoidAction_AllCases()
        {
            yield return new object[] {
                TestCase<string, object>.Create(
                    "void ok (non-null)",
                    "hi",
                    new SuccessResult<object, Type>(new object()))
            };
        }

        private static void VoidAction(string? input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));
        }

        [TestMethod]
        [DynamicData(nameof(VoidAction_AllCases), DynamicDataSourceType.Method)]
        public void VoidAction_Test(TestCase<string, object> tc)
        {
            try
            {
                VoidAction(tc.Parameters);
                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        /// <summary>
        /// Pruebas de suma de enteros en método asíncrono con manejo de nullables y excepciones
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> TaskAdd_AllCases()
        {
            yield return new object[] {
                TestCase<(int A, int B), int>.Create(
                    "Task<int> add ok",
                    (10, 5),
                    new SuccessResult<int, Type>(15))
            };
            yield return new object[] {
                TestCase<(int A, int B), int>.Create(
                    "Task<int> add overflow fails",
                    (int.MaxValue, 1),
                    new FailureResult<int, Type>(typeof(OverflowException), "Operation failed."))
            };
        }

        private static async Task<int?> AddAsync(int a, int b)
        {
            await Task.Yield(); // simulate async work
            checked { return a + b; } // may throw OverflowException
        }

        [TestMethod]
        [DynamicData(nameof(TaskAdd_AllCases), DynamicDataSourceType.Method)]
        public async Task TaskAdd_Test(TestCase<(int A, int B), int> tc)
        {
            try
            {
                var actual = await AddAsync(tc.Parameters.A, tc.Parameters.B);
                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
                Assert.AreEqual(tc.Result.SuccessValue, actual,
                    tc.FailResponse("Task add result mismatch", tc.Result.SuccessValue, actual));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        /// <summary>
        /// Pruebas de paso de Task como parámetro
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> Task_asParameter_AllCases()
        {
            var tarea = new Task(() => Task.Delay(100));

            yield return new object[] {
                TestCase<Task, Task>.Create(
                    title: "test a Task as paremeter",
                    parameters: tarea,
                    new SuccessResult<Task, Type>(tarea))
            };
        }

        [TestMethod]
        [DynamicData(nameof(Task_asParameter_AllCases), DynamicDataSourceType.Method)]
        public void IntAdd(TestCase<Task, Task> tc)
        {
            try
            {
                var task = tc.Parameters;

                task.Start();

                Assert.IsInstanceOfType(task, typeof(Task), "Task parameter is not of type Task");
                Assert.IsNotNull(task, tc.Result.IsFailure() ? "Expected failure, but got success" : "Task parameter is null");
                Assert.AreEqual(tc.Result.SuccessValue, task, tc.FailResponse("Task parameter mismatch", tc.Result.SuccessValue, task));

                if (!tc.Result.IsSuccess())
                    Assert.Fail(tc.FailResponse("Expected failure, but got success"));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex, "fail"));

                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue, tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        /// <summary>
        /// Pruebas de método asíncrono sin parámetros que devuelve un Task con resultado
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> Task_Without_Pararemeters_AllCases()
        {
            string result = "10";

            yield return new object[] {
                TestCase<string>.Create(
                    title: "test a Task as paremeter",
                    new SuccessResult<string, Type>(result))
            };
        }

        [TestMethod]
        [DynamicData(nameof(Task_Without_Pararemeters_AllCases), DynamicDataSourceType.Method)]
        public void WithoutParameters(TestCase<string> tc)
        {
            try
            {
                var result = tc.Result;

                int test = 10;

                var testAnswer = test.ToString();

                Assert.IsInstanceOfType(testAnswer, typeof(string), "Result is not of type string");
                Assert.IsNotNull(testAnswer, tc.Result.IsFailure() ? "Expected failure, but got success" : "Result is null");
                Assert.AreEqual(tc.Result.SuccessValue.ToString(), testAnswer, tc.FailResponse("Result mismatch", tc.Result.SuccessValue, testAnswer));

                if (!tc.Result.IsSuccess())
                    Assert.Fail(tc.FailResponse("Expected failure, but got success"));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex, ex));

                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue, tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        public static IEnumerable<object[]> ProceduralCases()
        {
            yield return new object[] { TestCase<string, Unit>.Create("Case 1", "Parameter 1") };
            yield return new object[] { TestCase<string, Unit>.Create("Case 2", "Parameter 2") };
        }

        [TestMethod]
        [DynamicData(nameof(ProceduralCases), DynamicDataSourceType.Method)]
        public void Create_WithValidParameters_ReturnsTestCaseWithUnitSuccess(TestCase<string, Unit> tc)
        {
            try
            {
                var title = tc.Title;
                var parameters = tc.Parameters;

                var testcaseresult = TestCase<string, Unit>.Create(title, parameters);

                Assert.IsNotNull(tc, "TestCase should not be null");
                Assert.AreEqual(title, tc.Title, "Title mismatch");
                Assert.AreEqual(parameters, tc.Parameters, "Parameters mismatch");
                Assert.IsInstanceOfType(tc.Result, typeof(SuccessResult<Unit, Type>), "Result should be SuccessResult<Unit, Type>");
                Assert.AreEqual(Unit.Value, tc.Result.SuccessValue, "SuccessValue should be Unit.Value");
            }
            catch (Exception ex)
            {
                Assert.Fail(tc.FailResponse("Procedural test failed", ex));
            }
        }

        /// <summary>
        /// Casos de prueba para callbacks sin parámetros
        /// </summary>
        public static IEnumerable<object[]> Callback_NoParameters_AllCases()
        {
            yield return new object[]
            {
                TestCase<int>.Create(
                    "Test callbacks no parameters",
                    new SuccessResult<int, Type>(42))
            };
        }

        [TestMethod]
        [DynamicData(nameof(Callback_NoParameters_AllCases), DynamicDataSourceType.Method)]
        [Priority(4)]
        public void Callback_NoParameters(TestCase<int> tc)
        {
            var executed = new List<string>();

            tc.Setup = t => executed.Add($"setup:{t.Title}");
            tc.Teardown = t => executed.Add($"teardown:{t.Title}");

            var result = tc.Run(() => 42);

            Assert.AreEqual(42, result, "Execution result mismatch");
            CollectionAssert.AreEqual(
                new[] { $"setup:{tc.Title}", $"teardown:{tc.Title}" },
                executed,
                "Callbacks did not execute in expected order");
        }

        /// <summary>
        /// Casos de prueba para callbacks con parámetros
        /// </summary>
        public static IEnumerable<object[]> Callback_WithParameters_AllCases()
        {
            yield return new object[]
            {
                TestCase<(int A, int B), int>.Create(
                    "Test callbacks with parameters",
                    (2, 3),
                    new SuccessResult<int, Type>(5))
            };
        }

        [TestMethod]
        [DynamicData(nameof(Callback_WithParameters_AllCases), DynamicDataSourceType.Method)]
        [Priority(5)]
        public void Callback_WithParameters(TestCase<(int A, int B), int> tc)
        {
            var executed = new List<string>();

            tc.Setup = t => executed.Add($"setup:{t.Title}");
            tc.Teardown = t => executed.Add($"teardown:{t.Title}");

            var result = tc.Run(p => p.A + p.B);

            Assert.AreEqual(5, result, "Execution result mismatch");
            CollectionAssert.AreEqual(
                new[] { $"setup:{tc.Title}", $"teardown:{tc.Title}" },
                executed,
                "Callbacks did not execute in expected order");
        }

        /// <summary>
        /// Casos de prueba para callbacks ejecutados manualmente
        /// </summary>
        public static IEnumerable<object[]> Callback_Manual_AllCases()
        {
            yield return new object[]
            {
                TestCase<string>.Create(
                    "Test manual callbacks",
                    new SuccessResult<string, Type>("ok"))
            };
        }

        [TestMethod]
        [DynamicData(nameof(Callback_Manual_AllCases), DynamicDataSourceType.Method)]
        [Priority(6)]
        public void Callback_Manual(TestCase<string> tc)
        {
            var executed = new List<string>();

            tc.Setup = t => executed.Add($"setup:{t.Title}");
            tc.Teardown = t => executed.Add($"teardown:{t.Title}");

            // Ejecución manual
            tc.RunSetup();
            executed.Add("execute");
            tc.RunTeardown();

            CollectionAssert.AreEqual(
                new[] { $"setup:{tc.Title}", "execute", $"teardown:{tc.Title}" },
                executed,
                "Manual execution order mismatch");
        }

        /// <summary>
        /// Casos de prueba cuando no se configuran callbacks
        /// </summary>
        public static IEnumerable<object[]> Callback_None_AllCases()
        {
            yield return new object[]
            {
                TestCase<int>.Create(
                    "Test callbacks none",
                    new SuccessResult<int, Type>(99))
            };
        }

        [TestMethod]
        [DynamicData(nameof(Callback_None_AllCases), DynamicDataSourceType.Method)]
        [Priority(7)]
        public void Callback_None(TestCase<int> tc)
        {
            // No se configuran Setup ni Teardown
            var result = tc.Run(() => 99);

            Assert.AreEqual(99, result, "Execution result mismatch when no callbacks defined");
        }

        /// <summary>
        /// Casos de prueba para verificar que el estado no se acumula entre ejecuciones de Run
        /// </summary>
        public static IEnumerable<object[]> Callback_StateIsolation_AllCases()
        {
            yield return new object[]
            {
        TestCase<int>.Create(
            "Test state isolation",
            new SuccessResult<int, Type>(0))
            };
        }

        [TestMethod]
        [DynamicData(nameof(Callback_StateIsolation_AllCases), DynamicDataSourceType.Method)]
        [Priority(8)]
        public void Callback_StateIsolation(TestCase<int> tc)
        {
            var counter = 0;

            tc.Setup = t => counter++;

            var r1 = tc.Run(() => 100);

            var r2 = tc.Run(() => 200);

            Assert.AreEqual(100, r1, "First run result mismatch");
            Assert.AreEqual(200, r2, "Second run result mismatch");

            Assert.AreEqual(2, counter, "Setup should run once per test execution, ensuring state isolation");
        }

        /// <summary>
        /// Casos de prueba para verificar que Teardown limpia recursos (carpeta temporal)
        /// </summary>
        public static IEnumerable<object[]> Callback_CreateFolder_AllCases()
        {
            yield return new object[]
            {
        TestCase<string, string>.Create(
            "Test folder creation and cleanup",
            "tempFolder",
            new SuccessResult<string, Type>("tempFolder"))
            };
        }

        [TestMethod]
        [DynamicData(nameof(Callback_CreateFolder_AllCases), DynamicDataSourceType.Method)]
        [Priority(9)]
        public void Callback_CreateFolderAndCleanup(TestCase<string, string> tc)
        {
            string folderPath = Path.Combine(Path.GetTempPath(), tc.Parameters);

            // Setup: crear carpeta
            tc.Setup = t =>
            {
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
            };

            // Teardown: eliminar carpeta
            tc.Teardown = t =>
            {
                if (Directory.Exists(folderPath))
                    Directory.Delete(folderPath, true);
            };

            tc.Run(p =>
            {
                Assert.IsTrue(Directory.Exists(folderPath), "Folder should exist during test execution");
                return folderPath;
            });

            // Después de Run, Teardown ya se ejecutó: la carpeta debe desaparecer
            Assert.IsFalse(Directory.Exists(folderPath), "Folder should be deleted after test execution");
        }

        /// <summary>
        /// Casos de prueba: RunSetup ejecuta Setup correctamente
        /// </summary>
        public static IEnumerable<object[]> RunSetup_Executes_AllCases()
        {
            yield return new object[]
            {
        TestCase<string, int>.Create(
            "RunSetup executes",
            "param",
            new SuccessResult<int, Type>(1))
            };
        }

        [TestMethod]
        [DynamicData(nameof(RunSetup_Executes_AllCases), DynamicDataSourceType.Method)]
        [Priority(10)]
        public void RunSetup_Executes(TestCase<string, int> tc)
        {
            var executed = new List<string>();
            tc.Setup = t => executed.Add("setup");

            tc.RunSetup();

            CollectionAssert.AreEqual(new[] { "setup" }, executed, "Setup should have been executed");
        }

        /// <summary>
        /// Casos de prueba: RunSetup no hace nada si Setup es null
        /// </summary>
        public static IEnumerable<object[]> RunSetup_Null_AllCases()
        {
            yield return new object[]
            {
        TestCase<string, int>.Create(
            "RunSetup null",
            "param",
            new SuccessResult<int, Type>(1))
            };
        }

        [TestMethod]
        [DynamicData(nameof(RunSetup_Null_AllCases), DynamicDataSourceType.Method)]
        [Priority(11)]
        public void RunSetup_Null(TestCase<string, int> tc)
        {
            tc.Setup = null;
            tc.RunSetup();
            Assert.IsTrue(true, "RunSetup with null should not throw");
        }

        /// <summary>
        /// Casos de prueba: RunSetup envuelve excepciones
        /// </summary>
        public static IEnumerable<object[]> RunSetup_Exception_AllCases()
        {
            yield return new object[]
            {
        TestCase<string, int>.Create(
            "RunSetup throws",
            "param",
            new SuccessResult<int, Type>(1))
            };
        }

        [TestMethod]
        [DynamicData(nameof(RunSetup_Exception_AllCases), DynamicDataSourceType.Method)]
        [Priority(12)]
        public void RunSetup_Exception(TestCase<string, int> tc)
        {
            tc.Setup = t => throw new InvalidOperationException("Boom");

            var ex = Assert.ThrowsException<Exception>(() => tc.RunSetup());
            StringAssert.Contains(ex.Message, "Error in setup of test", "Exception message should contain context info");
            Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException), "Inner exception should be preserved");
        }

        /// <summary>
        /// Casos de prueba: RunTeardown ejecuta Teardown correctamente
        /// </summary>
        public static IEnumerable<object[]> RunTeardown_Executes_AllCases()
        {
            yield return new object[]
            {
        TestCase<string, int>.Create(
            "RunTeardown executes",
            "param",
            new SuccessResult<int, Type>(1))
            };
        }

        [TestMethod]
        [DynamicData(nameof(RunTeardown_Executes_AllCases), DynamicDataSourceType.Method)]
        [Priority(13)]
        public void RunTeardown_Executes(TestCase<string, int> tc)
        {
            var executed = new List<string>();
            tc.Teardown = t => executed.Add("teardown");

            tc.RunTeardown();

            CollectionAssert.AreEqual(new[] { "teardown" }, executed, "Teardown should have been executed");
        }

        /// <summary>
        /// Casos de prueba: RunTeardown no hace nada si Teardown es null
        /// </summary>
        public static IEnumerable<object[]> RunTeardown_Null_AllCases()
        {
            yield return new object[]
            {
        TestCase<string, int>.Create(
            "RunTeardown null",
            "param",
            new SuccessResult<int, Type>(1))
            };
        }

        [TestMethod]
        [DynamicData(nameof(RunTeardown_Null_AllCases), DynamicDataSourceType.Method)]
        [Priority(14)]
        public void RunTeardown_Null(TestCase<string, int> tc)
        {
            tc.Teardown = null;
            tc.RunTeardown();
            Assert.IsTrue(true, "RunTeardown with null should not throw");
        }

        /// <summary>
        /// Casos de prueba: RunTeardown envuelve excepciones
        /// </summary>
        public static IEnumerable<object[]> RunTeardown_Exception_AllCases()
        {
            yield return new object[]
            {
        TestCase<string, int>.Create(
            "RunTeardown throws",
            "param",
            new SuccessResult<int, Type>(1))
            };
        }

        [TestMethod]
        [DynamicData(nameof(RunTeardown_Exception_AllCases), DynamicDataSourceType.Method)]
        [Priority(15)]
        public void RunTeardown_Exception(TestCase<string, int> tc)
        {
            tc.Teardown = t => throw new InvalidOperationException("Boom");

            var ex = Assert.ThrowsException<Exception>(() => tc.RunTeardown());
            StringAssert.Contains(ex.Message, "Error in teardown of test", "Exception message should contain context info");
            Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException), "Inner exception should be preserved");
        }

        /// <summary>
        /// Casos de prueba: RunSetup lanza excepción en TestCase<R>
        /// </summary>
        public static IEnumerable<object[]> RunSetup_Exception_AllCases_R()
        {
            yield return new object[]
            {
        TestCase<int>.Create(
            "RunSetup throws (R)",
            new SuccessResult<int, Type>(1))
            };
        }

        [TestMethod]
        [DynamicData(nameof(RunSetup_Exception_AllCases_R), DynamicDataSourceType.Method)]
        [Priority(20)]
        public void RunSetup_Exception_R(TestCase<int> tc)
        {
            tc.Setup = t => throw new InvalidOperationException("Boom");

            var ex = Assert.ThrowsException<Exception>(() => tc.RunSetup());

            StringAssert.Contains(ex.Message, "Error in setup of test", "Exception message should contain context info");
            Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException), "Inner exception should be preserved");
        }

        /// <summary>
        /// Casos de prueba: RunTeardown lanza excepción en TestCase<R>;
        /// </summary>
        public static IEnumerable<object[]> RunTeardown_Exception_AllCases_R()
        {
            yield return new object[]
            {
        TestCase<int>.Create(
            "RunTeardown throws (R)",
            new SuccessResult<int, Type>(1))
            };
        }

        [TestMethod]
        [DynamicData(nameof(RunTeardown_Exception_AllCases_R), DynamicDataSourceType.Method)]
        [Priority(21)]
        public void RunTeardown_Exception_R(TestCase<int> tc)
        {
            tc.Teardown = t => throw new InvalidOperationException("Boom");

            var ex = Assert.ThrowsException<Exception>(() => tc.RunTeardown());

            StringAssert.Contains(ex.Message, "Error in teardown of test", "Exception message should contain context info");
            Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException), "Inner exception should be preserved");
        }

    }
}