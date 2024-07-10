using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public class FailureResultComplex<TSuccess, TFailure> : ResultBase, IResultComplex<TSuccess, TFailure>
    {
        public TSuccess SuccessValue => throw new InvalidOperationException("Cannot access SuccessValue in a failure result.");
        public TFailure FailureValue { get; private set; }

        public FailureResultComplex(TFailure failureValue, string message)
        {
            FailureValue = failureValue;
            Status = ResultStatus.Failure;
            Message = message;
        }
    }
}
