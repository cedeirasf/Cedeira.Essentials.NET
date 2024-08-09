using Cedeira.Essentials.NET.System.Abstractions;

namespace Cedeira.Essentials.NET.System.ResultPattern
{
    /// <summary>
    /// Representa un resultado de éxito
    /// </summary>
    /// <typeparam name="TSuccess">El tipo del valor de éxito</typeparam>
    public class SuccessResult<TSuccess> : ResultBase, IResult<TSuccess>
    {
        /// <summary>
        /// Obtiene el valor de éxito
        /// </summary>
        public TSuccess SuccessValue { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <vea cref="SuccessResult{TSuccess}"/> con el valor de éxito y el mensaje especificados
        /// </summary>
        /// <param name="successValue">El valor de éxito</param>
        /// <param name="message">El mensaje de éxito</param>
        public SuccessResult(TSuccess successValue, string message = "")
        {
            SuccessValue = successValue;
            Status = ResultStatus.Success;
            Message = message;
        }
    }

    /// <summary>
    /// Representa un resultado de éxito con valores tanto de éxito como de fallo
    /// </summary>
    /// <typeparam name="TSuccess">El tipo del valor de éxito</typeparam>
    /// <typeparam name="TFailure">El tipo del valor de fallo</typeparam>
    public class SuccessResult<TSuccess, TFailure> : ResultBase, IResult<TSuccess, TFailure>
    {
        /// <summary>
        /// Obtiene el valor de éxito
        /// </summary>
        public TSuccess SuccessValue { get; private set; }

        /// <summary>
        /// Obtiene el valor de fallo
        /// </summary>
        /// <exception cref="InvalidOperationException">Se lanza cuando se intenta acceder a FailureValue en un resultado de éxito</exception>
        public TFailure FailureValue => throw new InvalidOperationException("No se puede acceder a FailureValue en un resultado de éxito.");

        /// <summary>
        /// Inicializa una nueva instancia de la clase <vea cref="SuccessResult{TSuccess, TFailure}"/> con el valor de éxito y el mensaje especificados
        /// </summary>
        /// <param name="successValue">El valor de éxito</param>
        /// <param name="message">El mensaje de éxito</param>
        public SuccessResult(TSuccess successValue, string message = "")
        {
            SuccessValue = successValue;
            Status = ResultStatus.Success;
            Message = message;
        }
    }
}
