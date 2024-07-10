using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public class SuccessResultComplex<TSuccess, TFailure> : ResultBase, IResultComplex<TSuccess, TFailure>
    {
        public TSuccess SuccessValue { get; private set; }
        public TFailure FailureValue => default;

        public SuccessResultComplex(TSuccess successValue)
        {
            SuccessValue = successValue;
            Status = ResultStatus.Success;
            Message = string.Empty;
        }
    }
}
