using Cedeira.Essentials.NET.System.ResultPattern;

namespace Cedeira.Essentials.NET.System.Abstractions
{
    /// <summary>
    /// Representa la clase base para el patron resultado
    /// </summary>
    public abstract class ResultBase : IResult
    {
        /// <summary>
        /// Obtiene el estado del resultado (enum)
        /// </summary>
        public ResultStatus Status { get; protected set; }

        /// <summary>
        /// Obtiene el mensaje asociado con el resultado
        /// </summary>
        public string Message { get; protected set; } = string.Empty;

        /// <summary>
        /// Determina si el resultado indica éxito
        /// </summary>
        /// <returns>Verdadero si el resultado es exitoso; de lo contrario, falso</returns>
        public bool IsSuccess() => Status == ResultStatus.Success;

        /// <summary>
        /// Determina si el resultado indica fallo
        /// </summary>
        /// <returns>Verdadero si el resultado es un fallo; de lo contrario, falso</returns>
        public bool IsFailure() => Status == ResultStatus.Failure;

        /// <summary>
        /// Determina si el resultado indica una advertencia
        /// </summary>
        /// <returns>Verdadero si el resultado es una advertencia; de lo contrario, falso</returns>
        public bool IsWarning() => Status == ResultStatus.Warning;
    }
}
