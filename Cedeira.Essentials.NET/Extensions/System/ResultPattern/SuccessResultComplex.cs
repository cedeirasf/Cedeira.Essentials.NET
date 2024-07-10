using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public class SuccessResultComplex<TSuccess, TFailure> : ResultBase, IResultComplex<TSuccess, TFailure>
    {
        public TSuccess SuccessValue { get; private set; }
        public TFailure FailureValue => throw new InvalidOperationException("Cannot access FailureValue in a success result.");

        public SuccessResultComplex(TSuccess successValue, string message = "")
        {
            SuccessValue = successValue;
            Status = ResultStatus.Success;
            Message = message;
        }
    }
}
