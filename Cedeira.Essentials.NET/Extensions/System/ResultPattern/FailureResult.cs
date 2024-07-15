using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
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
