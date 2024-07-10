using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public class FailureResult<TSuccess> : ResultBase, IResult<TSuccess>
    {
        public TSuccess SuccessValue => throw new InvalidOperationException("Cannot access SuccessValue in a failure result.");

        public FailureResult(string message)
        {
            Status = ResultStatus.Failure;
            Message = message;
        }
    }

    public class FailureResult<TSuccess, TFailure> : ResultBase, IResult<TSuccess, TFailure>
    {
        public TSuccess SuccessValue => throw new InvalidOperationException("Cannot access SuccessValue in a failure result.");
        public TFailure FailureValue { get; private set; }

        public FailureResult(TFailure failureValue, string message)
        {
            FailureValue = failureValue;
            Status = ResultStatus.Failure;
            Message = message;
        }
    }
}
