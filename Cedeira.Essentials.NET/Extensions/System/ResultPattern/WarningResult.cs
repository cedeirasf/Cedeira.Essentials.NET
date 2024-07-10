using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public class WarningResult<TSuccess> : ResultBase, IResult<TSuccess>
    {
        public TSuccess SuccessValue { get; private set; }

        public WarningResult(TSuccess successValue, string message)
        {
            SuccessValue = successValue;
            Status = ResultStatus.Warning;
            Message = message;
        }
    }

    public class WarningResult<TSuccess, TFailure> : ResultBase, IResult<TSuccess, TFailure>
    {
        public TSuccess SuccessValue { get; private set; }
        public TFailure FailureValue => throw new InvalidOperationException("Cannot access FailureValue in a warning result.");

        public WarningResult(TSuccess successValue, string message)
        {
            SuccessValue = successValue;
            Status = ResultStatus.Warning;
            Message = message;
        }
    }
}
