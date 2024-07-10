namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions
{
    public interface IResultComplex<TSuccess, TFailure> : IResult
    {
        TSuccess SuccessValue { get; }
        TFailure FailureValue { get; }
    }

}
