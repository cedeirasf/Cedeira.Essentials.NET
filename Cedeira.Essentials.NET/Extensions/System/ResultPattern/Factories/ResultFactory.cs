using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern.Factories
{
    public class ResultFactory : IResultFactory
    {
        public IResultSimple<TSuccess> Success<TSuccess>(TSuccess value) =>
        new SuccessResultSimple<TSuccess>(value);

        public IResultSimple<TSuccces> Warning<TSuccces>(TSuccces value, string message) =>
            new WarningResultSimple<TSuccces>(value, message);

        public IResultSimple<TSuccess> Failure<TSuccess>(string errorMesssage) =>
            new FailureResultSimple<TSuccess>(errorMesssage);

        public IResultComplex<TSuccess, TFailure> Success<TSuccess,TFailure>(TSuccess value) =>
            new SuccessResultComplex<TSuccess, TFailure>(value);

        public IResultComplex<TSuccess, TFailure> Warning<TSuccess, TFailure>(TSuccess value, string message) =>
            new WarningResultComplex<TSuccess, TFailure>(value, message);

        public IResultComplex<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure error, string errorMessage) =>
            new FailureResultComplex<TSuccess, TFailure>(error, errorMessage);

    }
}
