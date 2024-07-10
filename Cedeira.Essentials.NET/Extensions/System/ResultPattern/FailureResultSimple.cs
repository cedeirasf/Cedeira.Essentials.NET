using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public class FailureResultSimple<TSuccess> : ResultBase, IResultSimple<TSuccess>
    {
        public TSuccess SuccessValue => throw new InvalidOperationException("Cannot access SuccessValue in a failure result.");

        public FailureResultSimple(string message)
        {
            Status = ResultStatus.Failure;
            Message = message;
        }
    }
}
