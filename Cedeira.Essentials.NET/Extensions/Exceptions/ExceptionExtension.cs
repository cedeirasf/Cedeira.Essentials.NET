namespace Cedeira.Essentials.NET.Extensions.Exceptions
{
    /// <summary>
    /// Proporciona métodos de extensión para la clase Exception para obtener mensajes completos
    /// </summary>
    public static class ExceptionExtension
    {
        private const string DefaultSeparator = ". ";

        /// <summary>
        /// Recupera el mensaje completo de una excepcion
        /// </summary>
        /// <param name="e">La instancia de la excepcion</param>
        /// <returns>El mensaje completo de la excepcion</returns>
        public static string FullMessage(this Exception e)
        {
            return e.FullMessage(DefaultSeparator);
        }

        /// <summary>
        /// Recupera el mensaje completo de una excepcion con un separador especificado
        /// </summary>
        /// <param name="e">La instancia de la excepcion</param>
        /// <param name="separator">el string del separador</param>
        /// <returns>El mensaje completo de la excepcion</returns>
        public static string FullMessage(this Exception e, string separator)
        {
            if (e is null) return string.Empty;
            var message = e.Message;
            if (e.InnerException is not null)
            {
                message += separator + e.InnerException.FullMessage(separator);
            }
            return message;
        }

        /// <summary>
        /// Recupera el mensaje de la última excepción anidada
        /// </summary>
        /// <param name="e">La instancia de la excepcion</param>
        /// <returns>El mensaje de la última excepción anidada</returns>
        public static string LastExceptionMessage(this Exception e)
        {
            if (e is null) return string.Empty;

            while (e.InnerException is not null)
            {
                e = e.InnerException;
            }
            return e.Message;
        }
    }
}
