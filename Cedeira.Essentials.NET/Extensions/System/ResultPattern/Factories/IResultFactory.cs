namespace Cedeira.Essentials.NET.Extensions.System.ResultPattern.Factories
{
    /// <summary>
    /// Define una interfaz de fábrica para instanciar objetos de resultado 
    /// </summary>
    public interface IResultFactory
    {
        /// <summary>
        /// Crea un resultado de éxito con el valor especificado
        /// </summary>
        /// <typeparam name="TSuccess">El tipo del valor de éxito (enum)</typeparam>
        /// <param name="value">El valor de éxito</param>
        /// <returns>Un resultado de éxito</returns>
        IResult<TSuccess> Success<TSuccess>(TSuccess value);

        /// <summary>
        /// Crea un resultado de advertencia con el valor y el mensaje especificados
        /// </summary>
        /// <typeparam name="TSuccess">El tipo del valor de éxito (enum)</typeparam>
        /// <param name="value">El valor de éxito</param>
        /// <param name="message">El mensaje de advertencia</param>
        /// <returns>Un resultado de advertencia</returns>
        IResult<TSuccess> Warning<TSuccess>(TSuccess value, string message);

        /// <summary>
        /// Crea un resultado de fallo con el mensaje de error especificado
        /// </summary>
        /// <typeparam name="TSuccess">El tipo del valor de éxito (enum)</typeparam>
        /// <param name="errorMessage">El mensaje de error</param>
        /// <returns>Un resultado de fallo</returns>
        IResult<TSuccess> Failure<TSuccess>(string errorMessage);

        /// <summary>
        /// Crea un resultado de éxito con los tipos de éxito y fallo especificados
        /// </summary>
        /// <typeparam name="TSuccess">El tipo del valor de éxito (enum)</typeparam>
        /// <typeparam name="TFailure">El tipo del valor de fallo</typeparam>
        /// <param name="value">El valor de éxito</param>
        /// <returns>Un resultado de éxito</returns>
        IResult<TSuccess, TFailure> Success<TSuccess, TFailure>(TSuccess value);

        /// <summary>
        /// Crea un resultado de advertencia con los tipos de éxito y fallo especificados, el valor y el mensaje
        /// </summary>
        /// <typeparam name="TSuccess">El tipo del valor de éxito (enum)</typeparam>
        /// <typeparam name="TFailure">El tipo del valor de fallo</typeparam>
        /// <param name="value">El valor de éxito</param>
        /// <param name="message">El mensaje de advertencia</param>
        /// <returns>Un resultado de advertencia</returns>
        IResult<TSuccess, TFailure> Warning<TSuccess, TFailure>(TSuccess value, string message);

        /// <summary>
        /// Crea un resultado de fallo con los tipos de éxito y fallo especificados, el valor de fallo y el mensaje
        /// </summary>
        /// <typeparam name="TSuccess">El tipo del valor de éxito (enum)</typeparam>
        /// <typeparam name="TFailure">El tipo del valor de fallo</typeparam>
        /// <param name="error">El valor de fallo</param>
        /// <param name="errorMessage">El mensaje de error</param>
        /// <returns>Un resultado de fallo</returns>
        IResult<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure error, string errorMessage);
    }

}
