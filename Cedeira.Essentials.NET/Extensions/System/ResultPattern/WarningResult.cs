using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Abstractions;

namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern
{
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
