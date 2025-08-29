using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.TDD;

namespace Cedeira.Essentials.NET_unittests.TDD
{
    [TestClass]
    public class PrimitiveSamplesTest
    {

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
            // nullable fails
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
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }


        public static IEnumerable<object[]> BoolNegate_AllCases()
        {
            yield return new object[] {
                TestCase<bool?, bool>.Create(
                    "bool?/bool negate: true -> false",
                    true,
                    new SuccessResult<bool, Type>(false))
            };
            yield return new object[] {
                TestCase<bool?, bool>.Create(
                    "bool?/bool negate: false -> true",
                    false,
                    new SuccessResult<bool, Type>(true))
            };
            yield return new object[] {
                TestCase<bool?, bool>.Create(
                    "bool? negate fails if null",
                    null,
                    new FailureResult<bool, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(BoolNegate_AllCases), DynamicDataSourceType.Method)]
        public void BoolNegate(TestCase<bool?, bool> tc)
        {
            try
            {
                if (!tc.Parameters.HasValue) throw new ArgumentNullException(nameof(tc.Parameters));
                var actual = !tc.Parameters.Value;

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

        public static IEnumerable<object[]> GuidParse_AllCases()
        {
            var valid = Guid.NewGuid().ToString();

            yield return new object[] {
                TestCase<string?, Guid>.Create(
                    "guid parse ok",
                    valid,
                    new SuccessResult<Guid, Type>(Guid.Parse(valid)))
            };
            yield return new object[] {
                TestCase<string?, Guid>.Create(
                    "guid parse fails if null",
                    null,
                    new FailureResult<Guid, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<string?, Guid>.Create(
                    "guid parse fails if invalid",
                    "not-a-guid",
                    new FailureResult<Guid, Type>(typeof(FormatException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(GuidParse_AllCases), DynamicDataSourceType.Method)]
        public void GuidParse(TestCase<string?, Guid> tc)
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
    }

    [TestClass]
    public class AsyncAndVoidSamplesTest
    {
        private static void VoidAction(string? input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));
        }

        public static IEnumerable<object[]> VoidAction_AllCases()
        {
            yield return new object[] {
                TestCase<string?, object>.Create(
                    "void ok (non-null)",
                    "hi",
                    new SuccessResult<object, Type>(new object()))
            };
            yield return new object[] {
                TestCase<string?, object>.Create(
                    "void fails on null",
                    null,
                    new FailureResult<object, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(VoidAction_AllCases), DynamicDataSourceType.Method)]
        public void VoidAction_Test(TestCase<string?, object> tc)
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

        private static async Task<int> AddAsync(int a, int b)
        {
            await Task.Yield();
            checked { return a + b; }
        }

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

        private static async Task<string> ProcessTwoObjectsAsync(object objA, object objB)
        {
            await Task.Yield();
            if (objA == null) throw new ArgumentNullException(nameof(objA));
            if (objB == null) throw new ArgumentNullException(nameof(objB));
            return $"{objA}-{objB}";
        }

        public static IEnumerable<object[]> TaskTwoObjects_AllCases()
        {
            yield return new object[] {
                TestCase<(object A, object B), string>.Create(
                    "Task<string> process two objects ok - string inputs",
                    ("Hello", "World"),
                    new SuccessResult<string, Type>("Hello-World"))
            };
            yield return new object[] {
                TestCase<(object A, object B), string>.Create(
                    "Task<string> process two objects ok - mixed types",
                    (42, "Test"),
                    new SuccessResult<string, Type>("42-Test"))
            };
            yield return new object[] {
                TestCase<(object A, object B), string>.Create(
                    "Task<string> process two objects ok - anonymous objects",
                    (new { Id = 1 }, new { Name = "Test" }),
                    new SuccessResult<string, Type>("{ Id = 1 }-{ Name = Test }"))
            };
            yield return new object[] {
                TestCase<(object A, object B), string>.Create(
                    "Task<string> process two objects fails when A is null",
                    (null, "World"),
                    new FailureResult<string, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<(object A, object B), string>.Create(
                    "Task<string> process two objects fails when B is null",
                    ("Hello", null),
                    new FailureResult<string, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(TaskTwoObjects_AllCases), DynamicDataSourceType.Method)]
        public async Task TaskTwoObjects_Test(TestCase<(object A, object B), string> tc)
        {
            try
            {
                var actual = await ProcessTwoObjectsAsync(tc.Parameters.A, tc.Parameters.B);
                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
                Assert.AreEqual(tc.Result.SuccessValue, actual,
                    tc.FailResponse("Task two objects result mismatch", tc.Result.SuccessValue, actual));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }

        private static async Task<bool> CompareObjectsAsync(object objA, object objB)
        {
            await Task.Yield();
            if (objA == null) throw new ArgumentNullException(nameof(objA));
            if (objB == null) throw new ArgumentNullException(nameof(objB));
            
            // Simulate some comparison logic
            if (objA.GetType() != objB.GetType()) throw new InvalidOperationException("Objects must be same type");
            
            return objA.Equals(objB);
        }

        public static IEnumerable<object[]> TaskObjectComparison_AllCases()
        {
            yield return new object[] {
                TestCase<(object A, object B), bool>.Create(
                    "Task<bool> compare objects ok - equal strings",
                    ("test", "test"),
                    new SuccessResult<bool, Type>(true))
            };
            yield return new object[] {
                TestCase<(object A, object B), bool>.Create(
                    "Task<bool> compare objects ok - different strings",
                    ("test1", "test2"),
                    new SuccessResult<bool, Type>(false))
            };
            yield return new object[] {
                TestCase<(object A, object B), bool>.Create(
                    "Task<bool> compare objects ok - equal numbers",
                    (42, 42),
                    new SuccessResult<bool, Type>(true))
            };
            yield return new object[] {
                TestCase<(object A, object B), bool>.Create(
                    "Task<bool> compare objects fails - different types",
                    ("test", 42),
                    new FailureResult<bool, Type>(typeof(InvalidOperationException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<(object A, object B), bool>.Create(
                    "Task<bool> compare objects fails when A is null",
                    (null, "test"),
                    new FailureResult<bool, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
            yield return new object[] {
                TestCase<(object A, object B), bool>.Create(
                    "Task<bool> compare objects fails when B is null",
                    ("test", null),
                    new FailureResult<bool, Type>(typeof(ArgumentNullException), "Operation failed."))
            };
        }

        [TestMethod]
        [DynamicData(nameof(TaskObjectComparison_AllCases), DynamicDataSourceType.Method)]
        public async Task TaskObjectComparison_Test(TestCase<(object A, object B), bool> tc)
        {
            try
            {
                var actual = await CompareObjectsAsync(tc.Parameters.A, tc.Parameters.B);
                if (!tc.Result.IsSuccess()) Assert.Fail(tc.FailResponse("Expected failure, but got success"));
                Assert.AreEqual(tc.Result.SuccessValue, actual,
                    tc.FailResponse("Task object comparison result mismatch", tc.Result.SuccessValue, actual));
            }
            catch (Exception ex)
            {
                if (!tc.Result.IsFailure()) Assert.Fail(tc.FailResponse("Expected success, but got failure", ex));
                Assert.IsInstanceOfType(ex, (Type)tc.Result.FailureValue,
                    tc.FailResponse("Exception type mismatch", tc.Result.FailureValue!, ex.GetType(), ex));
            }
        }
    }
}
