namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions
{
    public interface IResult
    {
        ResultStatus Status { get; }
        string Message { get; }
        bool IsSuccess();
        bool IsFailure();
        bool IsWarning();
    }

    public interface IResult<TSuccess, TFailure> : IResult
    {
        TSuccess SuccessValue { get; }
        TFailure FailureValue { get; }
    }

    public interface IResult<TSuccess> : IResult
    {
        TSuccess SuccessValue { get; }
    }
}
