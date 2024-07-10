using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
    public interface IResultSimple<TSuccess> : IResult
    {
        TSuccess SuccessValue { get; }
    }
}
