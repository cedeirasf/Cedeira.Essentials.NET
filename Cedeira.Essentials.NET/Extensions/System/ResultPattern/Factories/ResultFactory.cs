using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern.Factories
{
    public class ResultFactory : IResultFactory
    {
        public IResult<TSuccess> Success<TSuccess>(TSuccess value) =>
            new SuccessResult<TSuccess>(value);

        public IResult<TSuccess> Warning<TSuccess>(TSuccess value, string message) =>
            new WarningResult<TSuccess>(value, message);

        public IResult<TSuccess> Failure<TSuccess>(string errorMessage) =>
            new FailureResult<TSuccess>(errorMessage);

        public IResult<TSuccess, TFailure> Success<TSuccess, TFailure>(TSuccess value) =>
            new SuccessResult<TSuccess, TFailure>(value);

        public IResult<TSuccess, TFailure> Warning<TSuccess, TFailure>(TSuccess value, string message) =>
            new WarningResult<TSuccess, TFailure>(value, message);

        public IResult<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure error, string errorMessage) =>
            new FailureResult<TSuccess, TFailure>(error, errorMessage);
    }
}
