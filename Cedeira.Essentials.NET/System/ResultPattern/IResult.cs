namespace Cedeira.Essentials.NET.System.ResultPattern
{
    /// <summary>
    /// Define la interfaz de resultado.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Obtiene el estado del resultado.
        /// </summary>
        ResultStatus Status { get; }

        /// <summary>
        /// Obtiene el mensaje asociado con el resultado.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Determina si el resultado indica éxito.
        /// </summary>
        /// <returns>Verdadero si el resultado es exitoso; de lo contrario, falso.</returns>
        bool IsSuccess();

        /// <summary>
        /// Determina si el resultado indica fallo.
        /// </summary>
        /// <returns>Verdadero si el resultado es un fallo; de lo contrario, falso.</returns>
        bool IsFailure();

        /// <summary>
        /// Determina si el resultado indica una advertencia.
        /// </summary>
        /// <returns>Verdadero si el resultado es una advertencia; de lo contrario, falso.</returns>
        bool IsWarning();
    }

    /// <summary>
    /// Define la interfaz de resultado con valores de éxito y fallo.
    /// </summary>
    /// <typeparam name="TSuccess">El tipo del valor de éxito.</typeparam>
    /// <typeparam name="TFailure">El tipo del valor de fallo.</typeparam>
    public interface IResult<TSuccess, TFailure> : IResult
    {
        /// <summary>
        /// Obtiene el valor de éxito.
        /// </summary>
        TSuccess SuccessValue { get; }

        /// <summary>
        /// Obtiene el valor de fallo.
        /// </summary>
        TFailure FailureValue { get; }
    }

    /// <summary>
    /// Define la interfaz de resultado con un valor de éxito.
    /// </summary>
    /// <typeparam name="TSuccess">El tipo del valor de éxito.</typeparam>
    public interface IResult<TSuccess> : IResult
    {
        /// <summary>
        /// Obtiene el valor de éxito.
        /// </summary>
        TSuccess SuccessValue { get; }
    }
}