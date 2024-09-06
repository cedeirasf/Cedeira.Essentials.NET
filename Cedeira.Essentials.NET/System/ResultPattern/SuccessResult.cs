using Cedeira.Essentials.NET.System.Abstractions;

namespace Cedeira.Essentials.NET.System.ResultPattern
{
    /// <summary>
    /// Representa un resultado exitoso de una operación.
    /// </summary>
    public class SuccessResult : IResult
    {
        /// <summary>
        /// Obtiene el estado del resultado. Siempre será <see cref="ResultStatus.Success"/>.
        /// </summary>
        public ResultStatus Status { get; private set; }

        /// <summary>
        /// Obtiene el mensaje asociado con el resultado. No se puede acceder
        /// </summary>
        public string Message => throw new InvalidOperationException("El mensaje es inaccesible.");

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SuccessResult"/>.
        /// </summary>
        public SuccessResult()
        {
            Status = ResultStatus.Success;
        }

        /// <summary>
        /// Determina si el resultado indica éxito.
        /// </summary>
        /// <returns>Siempre retorna <c>true</c> porque es un resultado exitoso.</returns>
        public bool IsSuccess() => Status == ResultStatus.Success;

        /// <summary>
        /// Determina si el resultado indica fallo.
        /// </summary>
        /// <returns>Siempre retorna <c>false</c> porque no es un fallo.</returns>
        public bool IsFailure() => Status == ResultStatus.Failure;

        /// <summary>
        /// Determina si el resultado indica una advertencia.
        /// </summary>
        /// <returns>Siempre retorna <c>false</c> porque no es una advertencia.</returns>
        public bool IsWarning() => Status == ResultStatus.Warning;
    }


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
        /// Obtiene el mensaje asociado con el resultado. No se puede acceder
        /// </summary>
        public string Message => throw new InvalidOperationException("El mensaje es inaccesible.");


        /// <summary>
        /// Inicializa una nueva instancia de la clase <vea cref="SuccessResult{TSuccess}"/> con el valor de éxito
        /// </summary>
        /// <param name="successValue">El valor de éxito</param>
        public SuccessResult(TSuccess successValue)
        {
            SuccessValue = successValue;
            Status = ResultStatus.Success;
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
        /// Obtiene el mensaje asociado con el resultado. No se puede acceder
        /// </summary>
        public string Message => throw new InvalidOperationException("El mensaje es inaccesible.");

        /// <summary>
        /// Obtiene el valor de fallo
        /// </summary>
        /// <exception cref="InvalidOperationException">Se lanza cuando se intenta acceder a FailureValue en un resultado de éxito</exception>
        public TFailure FailureValue => throw new InvalidOperationException("No se puede acceder a FailureValue en un resultado de éxito.");

        /// <summary>
        /// Inicializa una nueva instancia de la clase <vea cref="SuccessResult{TSuccess, TFailure}"/> con el valor de éxito
        /// </summary>
        /// <param name="successValue">El valor de éxito</param>
        public SuccessResult(TSuccess successValue)
        {
            SuccessValue = successValue;
            Status = ResultStatus.Success;
        }
    }
}
