using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public class WarningResultSimple<TSuccess> : ResultBase, IResultSimple<TSuccess>
    {
        public TSuccess SuccessValue { get; private set; }

        public WarningResultSimple(TSuccess successValue, string message)
        {
            SuccessValue = successValue;
            Status = ResultStatus.Warning;
            Message = message;
        }
    }
}
