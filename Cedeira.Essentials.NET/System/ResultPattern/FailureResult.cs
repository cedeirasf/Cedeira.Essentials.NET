using Cedeira.Essentials.NET.System.Abstractions;

namespace Cedeira.Essentials.NET.System.ResultPattern
{
    /// <summary>
    /// Representa un resultado de fallo de una operación.
    /// </summary>
    public class FailureResult : IResult
    {
        /// <summary>
        /// Obtiene el estado del resultado. Siempre será <see cref="ResultStatus.Failure"/>.
        /// </summary>
        public ResultStatus Status { get; private set; }

        /// <summary>
        /// Obtiene el mensaje asociado con el fallo.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FailureResult"/> con un mensaje específico.
        /// </summary>
        /// <param name="message">El mensaje que describe el fallo.</param>
        public FailureResult(string message)
        {
            Status = ResultStatus.Failure;
            Message = message;
        }

        /// <summary>
        /// Determina si el resultado indica éxito.
        /// </summary>
        /// <returns>Siempre retorna <c>false</c> porque no es un éxito.</returns>
        public bool IsSuccess() => Status == ResultStatus.Success;

        /// <summary>
        /// Determina si el resultado indica fallo.
        /// </summary>
        /// <returns>Siempre retorna <c>true</c> porque es un fallo.</returns>
        public bool IsFailure() => Status == ResultStatus.Failure;

        /// <summary>
        /// Determina si el resultado indica una advertencia.
        /// </summary>
        /// <returns>Siempre retorna <c>false</c> porque no es una advertencia.</returns>
        public bool IsWarning() => Status == ResultStatus.Warning;
    }

    /// <summary>
    /// Representa un resultado de fallo
    /// </summary>
    /// <typeparam name="TSuccess">El tipo del valor de éxito</typeparam>
    public class FailureResult<TSuccess> : ResultBase, IResult<TSuccess>
    {
        /// <summary>
        /// Obtiene el valor de éxito
        /// </summary>
        /// <exception cref="InvalidOperationException">Se lanza cuando se intenta acceder a SuccessValue en un resultado de fallo</exception>
        public TSuccess SuccessValue => throw new InvalidOperationException("No se puede acceder a SuccessValue en un resultado de fallo.");

        /// <summary>
        /// Inicializa una nueva instancia de la clase <vea cref="FailureResult{TSuccess}"/> con el mensaje especificado.
        /// </summary>
        /// <param name="message">El mensaje de fallo</param>
        public FailureResult(string message)
        {
            Status = ResultStatus.Failure;
            Message = message;
        }
    }

    /// <summary>
    /// Representa un resultado de fallo con valores tanto de éxito como de fallo
    /// </summary>
    /// <typeparam name="TSuccess">El tipo del valor de éxito</typeparam>
    /// <typeparam name="TFailure">El tipo del valor de fallo</typeparam>
    public class FailureResult<TSuccess, TFailure> : ResultBase, IResult<TSuccess, TFailure>
    {
        /// <summary>
        /// Obtiene el valor de éxito
        /// </summary>
        /// <exception cref="InvalidOperationException">Se lanza cuando se intenta acceder a SuccessValue en un resultado de fallo</exception>
        public TSuccess SuccessValue => throw new InvalidOperationException("No se puede acceder a SuccessValue en un resultado de fallo.");

        /// <summary>
        /// Obtiene el valor de fallo
        /// </summary>
        public TFailure FailureValue { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <vea cref="FailureResult{TSuccess, TFailure}"/> con el valor de fallo y el mensaje especificados
        /// </summary>
        /// <param name="failureValue">El valor de fallo</param>
        /// <param name="message">El mensaje de fallo</param>
        public FailureResult(TFailure failureValue, string message)
        {
            FailureValue = failureValue;
            Status = ResultStatus.Failure;
            Message = message;
        }
    }
}
