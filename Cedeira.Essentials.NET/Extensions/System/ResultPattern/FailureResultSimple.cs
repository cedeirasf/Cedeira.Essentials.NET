using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public class FailureResultSimple<TSuccess> : ResultBase, IResultSimple<TSuccess>
    {
        public TSuccess SuccessValue { get; private set; }

        public FailureResultSimple(string message)
        {
            SuccessValue = default;
            Status = ResultStatus.Failure;
            Message = message;
        }
    }
}
