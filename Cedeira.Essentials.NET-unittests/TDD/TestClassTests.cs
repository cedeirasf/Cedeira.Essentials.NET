using Cedeira.Essentials.NET.System.ResultPattern;
using Cedeira.Essentials.NET.TDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cedeira.Essentials.NET_unittests.TDD
{
    public class TestClassTests
    {
        [TestClass]
        public class PrimitiveSamplesTest
        {
            public static IEnumerable<object[]> IntAddCases()
            {
                yield return new object[]
                {
                TestCase<(int A, int B), int>.Create(
                    title: "int add: 2 + 3 = 5",
                    parameters: (2, 3),
                    result: new SuccessResult<int, Type>(5))
                };

                yield return new object[]
                {
                TestCase<(int A, int B), int>.Create(
                    title: "int add overflow",
                    parameters: (int.MaxValue, 1),
                    result: new FailureResult<int, Type>(typeof(OverflowException), "Operation failed."))
                };
            }

            [TestMethod]
            [DynamicData(nameof(IntAddCases), DynamicDataSourceType.Method)]
            public void IntAdd(TestCase<(int A, int B), int> test)
            {
                try
                {
                    int actual;
                    checked { actual = test.Parameters.A + test.Parameters.B; }

                    if (!test.Result.IsSuccess())
                        Assert.Fail(test.FailResponse("Expected failure, but got success"));

                    Assert.AreEqual(test.Result.SuccessValue, actual,
                        test.FailResponse("Sum result mismatch", test.Result.SuccessValue, actual));
                }
                catch (Exception ex)
                {
                    if (!test.Result.IsFailure())
                        Assert.Fail(test.FailResponse("Expected success, but got failure", ex));

                    Assert.IsInstanceOfType(ex, (Type)test.Result.FailureValue,
                        test.FailResponse("Exception type mismatch", test.Result.FailureValue!, ex.GetType(), ex));
                }
            }

            public static IEnumerable<object[]> StringConcatCases()
            {
                yield return new object[]
                {
                TestCase<(string L, string R), string>.Create(
                    title: "string concat: 'hello' + ' world'",
                    parameters: ("hello", " world"),
                    result: new SuccessResult<string, Type>("hello world"))
                };

                yield return new object[]
                {
                TestCase<(string L, string R), string>.Create(
                    title: "string concat fails if left is null",
                    parameters: (null!, "world"),
                    result: new FailureResult<string, Type>(typeof(ArgumentNullException), "Operation failed."))
                };
            }

            [TestMethod]
            [DynamicData(nameof(StringConcatCases), DynamicDataSourceType.Method)]
            public void StringConcat(TestCase<(string L, string R), string> test)
            {
                try
                {
                    if (test.Parameters.L is null) throw new ArgumentNullException(nameof(test.Parameters.L));
                    var actual = string.Concat(test.Parameters.L, test.Parameters.R);

                    if (!test.Result.IsSuccess())
                        Assert.Fail(test.FailResponse("Expected failure, but got success"));

                    Assert.AreEqual(test.Result.SuccessValue, actual,
                        test.FailResponse("Concat result mismatch", test.Result.SuccessValue, actual));
                }
                catch (Exception ex)
                {
                    if (!test.Result.IsFailure())
                        Assert.Fail(test.FailResponse("Expected success, but got failure", ex));

                    Assert.IsInstanceOfType(ex, (Type)test.Result.FailureValue,
                        test.FailResponse("Exception type mismatch", test.Result.FailureValue!, ex.GetType(), ex));
                }
            }

            public static IEnumerable<object[]> BoolNegateCases()
            {
                yield return new object[]
                {
                TestCase<bool, bool>.Create(
                    title: "bool negate: true -> false",
                    parameters: true,
                    result: new SuccessResult<bool, Type>(false))
                };
                yield return new object[]
                {
                TestCase<bool, bool>.Create(
                    title: "bool negate: false -> true",
                    parameters: false,
                    result: new SuccessResult<bool, Type>(true))
                };
            }

            [TestMethod]
            [DynamicData(nameof(BoolNegateCases), DynamicDataSourceType.Method)]
            public void BoolNegate(TestCase<bool, bool> test)
            {
                var actual = !test.Parameters;

                if (!test.Result.IsSuccess())
                    Assert.Fail(test.FailResponse("Expected failure, but got success"));

                Assert.AreEqual(test.Result.SuccessValue, actual,
                    test.FailResponse("Negation mismatch", test.Result.SuccessValue, actual));
            }

            public static IEnumerable<object[]> DecimalDivCases()
            {
                yield return new object[]
                {
                TestCase<(decimal A, decimal B), decimal>.Create(
                    title: "decimal div: 10 / 4 = 2.5",
                    parameters: (10m, 4m),
                    result: new SuccessResult<decimal, Type>(2.5m))
                };

                yield return new object[]
                {
                TestCase<(decimal A, decimal B), decimal>.Create(
                    title: "decimal div by zero -> DivideByZeroException",
                    parameters: (5m, 0m),
                    result: new FailureResult<decimal, Type>(typeof(DivideByZeroException), "Operation failed."))
                };
            }

            [TestMethod]
            [DynamicData(nameof(DecimalDivCases), DynamicDataSourceType.Method)]
            public void DecimalDivide(TestCase<(decimal A, decimal B), decimal> test)
            {
                try
                {
                    var actual = test.Parameters.A / test.Parameters.B;

                    if (!test.Result.IsSuccess())
                        Assert.Fail(test.FailResponse("Expected failure, but got success"));

                    Assert.AreEqual(test.Result.SuccessValue, actual,
                        test.FailResponse("Division result mismatch", test.Result.SuccessValue, actual));
                }
                catch (Exception ex)
                {
                    if (!test.Result.IsFailure())
                        Assert.Fail(test.FailResponse("Expected success, but got failure", ex));

                    Assert.IsInstanceOfType(ex, (Type)test.Result.FailureValue,
                        test.FailResponse("Exception type mismatch", test.Result.FailureValue!, ex.GetType(), ex));
                }
            }

            public static IEnumerable<object[]> GuidParseCases()
            {
                var valid = Guid.NewGuid().ToString();
                yield return new object[]
                {
                TestCase<string, Guid>.Create(
                    title: "guid parse ok",
                    parameters: valid,
                    result: new SuccessResult<Guid, Type>(Guid.Parse(valid)))
                };

                yield return new object[]
                {
                TestCase<string, Guid>.Create(
                    title: "guid parse fails",
                    parameters: "not-a-guid",
                    result: new FailureResult<Guid, Type>(typeof(FormatException), "Operation failed."))
                };
            }

            [TestMethod]
            [DynamicData(nameof(GuidParseCases), DynamicDataSourceType.Method)]
            public void GuidParse(TestCase<string, Guid> test)
            {
                try
                {
                    var actual = Guid.Parse(test.Parameters);

                    if (!test.Result.IsSuccess())
                        Assert.Fail(test.FailResponse("Expected failure, but got success"));

                    Assert.AreEqual(test.Result.SuccessValue, actual,
                        test.FailResponse("Guid parse mismatch", test.Result.SuccessValue, actual));
                }
                catch (Exception ex)
                {
                    if (!test.Result.IsFailure())
                        Assert.Fail(test.FailResponse("Expected success, but got failure", ex));

                    Assert.IsInstanceOfType(ex, (Type)test.Result.FailureValue,
                        test.FailResponse("Exception type mismatch", test.Result.FailureValue!, ex.GetType(), ex));
                }
            }

            public static IEnumerable<object[]> DateTimeAddDaysCases()
            {
                var now = DateTime.UtcNow.Date;

                yield return new object[]
                {
                TestCase<(DateTime? Base, int Days), DateTime>.Create(
                    title: "datetime add days: today + 5",
                    parameters: (now, 5),
                    result: new SuccessResult<DateTime, Type>(now.AddDays(5)))
                };

                yield return new object[]
                {
                TestCase<(DateTime? Base, int Days), DateTime>.Create(
                    title: "datetime add days fails if base is null",
                    parameters: (null, 1),
                    result: new FailureResult<DateTime, Type>(typeof(ArgumentNullException), "Operation failed."))
                };
            }

            [TestMethod]
            [DynamicData(nameof(DateTimeAddDaysCases), DynamicDataSourceType.Method)]
            public void DateTimeAddDays(TestCase<(DateTime? Base, int Days), DateTime> test)
            {
                try
                {
                    if (test.Parameters.Base is null)
                        throw new ArgumentNullException(nameof(test.Parameters.Base));

                    var actual = test.Parameters.Base.Value.AddDays(test.Parameters.Days);

                    if (!test.Result.IsSuccess())
                        Assert.Fail(test.FailResponse("Expected failure, but got success"));

                    Assert.AreEqual(test.Result.SuccessValue, actual,
                        test.FailResponse("DateTime.AddDays mismatch", test.Result.SuccessValue, actual));
                }
                catch (Exception ex)
                {
                    if (!test.Result.IsFailure())
                        Assert.Fail(test.FailResponse("Expected success, but got failure", ex));

                    Assert.IsInstanceOfType(ex, (Type)test.Result.FailureValue,
                        test.FailResponse("Exception type mismatch", test.Result.FailureValue!, ex.GetType(), ex));
                }
            }
        }
    }
}
