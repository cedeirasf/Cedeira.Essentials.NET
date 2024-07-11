namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions
{
    public abstract class ResultBase : IResult
    {
        public ResultStatus Status { get; protected set; }
        public string Message { get; protected set; }  = string.Empty;

        public bool IsSuccess() => Status == ResultStatus.Success;
        public bool IsFailure() => Status == ResultStatus.Failure;
        public bool IsWarning() => Status == ResultStatus.Warning;
    }
}
