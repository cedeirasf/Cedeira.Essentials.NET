using Cedeira.Essentials.NET.System.Abstractions;

namespace Cedeira.Essentials.NET.System.ResultPattern
{
    /// <summary>
    /// Representa un resultado de advertencia de una operación.
    /// </summary>
    public class WarningResult : IResult
    {
        /// <summary>
        /// Obtiene el estado del resultado. Siempre será <see cref="ResultStatus.Warning"/>.
        /// </summary>
        public ResultStatus Status { get; private set; }

        /// <summary>
        /// Obtiene el mensaje asociado con la advertencia.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="WarningResult"/> con un mensaje específico.
        /// </summary>
        /// <param name="message">El mensaje que describe la advertencia.</param>
        public WarningResult(string message)
        {
            Status = ResultStatus.Warning;
            Message = message;
        }

        /// <summary>
        /// Determina si el resultado indica éxito.
        /// </summary>
        /// <returns>Siempre retorna <c>false</c> porque no es un éxito completo.</returns>
        public bool IsSuccess() => Status == ResultStatus.Success;

        /// <summary>
        /// Determina si el resultado indica fallo.
        /// </summary>
        /// <returns>Siempre retorna <c>false</c> porque no es un fallo.</returns>
        public bool IsFailure() => Status == ResultStatus.Failure;

        /// <summary>
        /// Determina si el resultado indica una advertencia.
        /// </summary>
        /// <returns>Siempre retorna <c>true</c> porque es una advertencia.</returns>
        public bool IsWarning() => Status == ResultStatus.Warning;
    }

    /// <summary>
    /// Representa un resultado de advertencia
    /// </summary>
    /// <typeparam name="TSuccess">El tipo del valor de éxito</typeparam>
    public class WarningResult<TSuccess> : ResultBase, IResult<TSuccess>
    {
        /// <summary>
        /// Obtiene el valor de éxito
        /// </summary>
        public TSuccess SuccessValue { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <vea cref="WarningResult{TSuccess}"/> con el valor de éxito y el mensaje de advertencia especificados
        /// </summary>
        /// <param name="successValue">El valor de éxito.</param>
        /// <param name="message">El mensaje de advertencia.</param>
        public WarningResult(TSuccess successValue, string message)
        {
            SuccessValue = successValue;
            Status = ResultStatus.Warning;
            Message = message;
        }
    }

    /// <summary>
    /// Representa un resultado de advertencia con valores tanto de éxito como de fallo.
    /// </summary>
    /// <typeparam name="TSuccess">El tipo del valor de éxito.</typeparam>
    /// <typeparam name="TFailure">El tipo del valor de fallo.</typeparam>
    public class WarningResult<TSuccess, TFailure> : ResultBase, IResult<TSuccess, TFailure>
    {
        /// <summary>
        /// Obtiene el valor de éxito.
        /// </summary>
        public TSuccess SuccessValue { get; private set; }

        /// <summary>
        /// Obtiene el valor de fallo.
        /// </summary>
        /// <exception cref="InvalidOperationException">Se lanza cuando se intenta acceder a FailureValue en un resultado de advertencia.</exception>
        public TFailure FailureValue => throw new InvalidOperationException("No se puede acceder a FailureValue en un resultado de advertencia.");

        /// <summary>
        /// Inicializa una nueva instancia de la clase <vea cref="WarningResult{TSuccess, TFailure}"/> con el valor de éxito y el mensaje de advertencia especificados.
        /// </summary>
        /// <param name="successValue">El valor de éxito.</param>
        /// <param name="message">El mensaje de advertencia.</param>
        public WarningResult(TSuccess successValue, string message)
        {
            SuccessValue = successValue;
            Status = ResultStatus.Warning;
            Message = message;
        }
    }
}
