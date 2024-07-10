using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{

    public class SuccessResultSimple<TSuccess> : ResultBase, IResultSimple<TSuccess>
    {
        public TSuccess SuccessValue { get; private set; }

        public SuccessResultSimple(TSuccess successValue)
        {
            SuccessValue = successValue;
            Status = ResultStatus.Success;
            Message = string.Empty;
        }
    }

}
