using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public class SuccessResult<TSuccess> : ResultBase, IResult<TSuccess>
    {
        public TSuccess SuccessValue { get; private set; }

        public SuccessResult(TSuccess successValue, string message = "")
        {
            SuccessValue = successValue;
            Status = ResultStatus.Success;
            Message = message;
        }
    }

    public class SuccessResult<TSuccess, TFailure> : ResultBase, IResult<TSuccess, TFailure>
    {
        public TSuccess SuccessValue { get; private set; }
        public TFailure FailureValue => throw new InvalidOperationException("Cannot access FailureValue in a success result.");

        public SuccessResult(TSuccess successValue, string message = "")
        {
            SuccessValue = successValue;
            Status = ResultStatus.Success;
            Message = message;
        }
    }
}
