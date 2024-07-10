namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions
{
    public interface IResultSimple<TSuccess> : IResult
    {
        TSuccess SuccessValue { get; }
    }
}
