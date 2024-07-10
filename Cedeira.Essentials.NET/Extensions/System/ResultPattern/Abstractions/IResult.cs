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
}
