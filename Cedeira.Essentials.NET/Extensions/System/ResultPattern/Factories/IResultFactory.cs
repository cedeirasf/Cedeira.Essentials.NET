using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern.Factories
{
    public interface IResultFactory
    {
        IResultSimple<TSuccess> Success<TSuccess>(TSuccess value);
        IResultSimple<TSuccess> Warning<TSuccess>(TSuccess value, string message);
        IResultSimple<TSuccess> Failure<TSuccess>(string errorMessage);
        IResultComplex<TSuccess, TFailure> Success<TSuccess, TFailure>(TSuccess value);
        IResultComplex<TSuccess, TFailure> Warning<TSuccess, TFailure>(TSuccess value, string message);
        IResultComplex<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure error, string errorMessage);
    }
}
