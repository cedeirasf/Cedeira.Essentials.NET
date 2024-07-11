namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern.Factories
{
    public interface IResultFactory
    {
        IResult<TSuccess> Success<TSuccess>(TSuccess value);
        IResult<TSuccess> Warning<TSuccess>(TSuccess value, string message);
        IResult<TSuccess> Failure<TSuccess>(string errorMessage);
        IResult<TSuccess, TFailure> Success<TSuccess, TFailure>(TSuccess value);
        IResult<TSuccess, TFailure> Warning<TSuccess, TFailure>(TSuccess value, string message);
        IResult<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure error, string errorMessage);
    }
}
