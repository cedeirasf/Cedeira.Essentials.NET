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

        /// <summary>
        /// Determina si la excepción o alguna de sus InnerException es del tipo genérico especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de excepción a buscar</typeparam>
        /// <param name="e">La instancia de la excepción</param>
        /// <returns>True si se encuentra una excepción del tipo especificado; de lo contrario, false</returns>
        public static bool ContainsException<T>(this Exception e) where T : Exception
        {
            if (e == null) return false;
            if (e is T) return true;
            return e.InnerException != null && e.InnerException.ContainsException<T>();
        }

        /// <summary>
        /// Busca la primera excepción anidada que coincide con el tipo genérico especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de excepción a buscar</typeparam>
        /// <param name="e">La instancia de la excepción</param>
        /// <returns>La primera excepción encontrada del tipo especificado, o null si no existe</returns>
        public static T? FindException<T>(this Exception e) where T : Exception
        {
            if (e == null) return null;
            if (e is T match) return match;
            return e.InnerException?.FindException<T>();
        }

        /// <summary>
        /// Determina si la excepción o alguna de sus InnerException es del tipo especificado.
        /// </summary>
        /// <param name="e">La instancia de la excepción</param>
        /// <param name="exceptionType">El tipo de excepción a buscar</param>
        /// <returns>True si se encuentra una excepción del tipo especificado; de lo contrario, false</returns>
        public static bool ContainsException(this Exception e, Type exceptionType)
        {
            if (e == null || exceptionType == null) return false;
            if (exceptionType.IsInstanceOfType(e)) return true;
            return e.InnerException != null && e.InnerException.ContainsException(exceptionType);
        }

        /// <summary>
        /// Busca la primera excepción anidada que coincide con el tipo especificado.
        /// </summary>
        /// <param name="e">La instancia de la excepción</param>
        /// <param name="exceptionType">El tipo de excepción a buscar</param>
        /// <returns>La primera excepción encontrada del tipo especificado, o null si no existe</returns>
        public static Exception? FindException(this Exception e, Type exceptionType)
        {
            if (e == null || exceptionType == null) return null;
            if (exceptionType.IsInstanceOfType(e)) return e;
            return e.InnerException?.FindException(exceptionType);
        }

        /// <summary>
        /// Determina si la excepción o alguna de sus InnerException es del tipo especificado por nombre.
        /// </summary>
        /// <param name="e">La instancia de la excepción</param>
        /// <param name="exceptionTypeName">El nombre del tipo de excepción a buscar</param>
        /// <returns>True si se encuentra una excepción del tipo especificado; de lo contrario, false</returns>
        public static bool ContainsException(this Exception e, string exceptionTypeName)
        {
            if (e == null || string.IsNullOrWhiteSpace(exceptionTypeName)) return false;
            if (e.GetType().Name == exceptionTypeName) return true;
            return e.InnerException != null && e.InnerException.ContainsException(exceptionTypeName);
        }

        /// <summary>
        /// Busca la primera excepción anidada que coincide con el nombre de tipo especificado.
        /// </summary>
        /// <param name="e">La instancia de la excepción</param>
        /// <param name="exceptionTypeName">El nombre del tipo de excepción a buscar</param>
        /// <returns>La primera excepción encontrada del tipo especificado, o null si no existe</returns>
        public static Exception? FindException(this Exception e, string exceptionTypeName)
        {
            if (e == null || string.IsNullOrWhiteSpace(exceptionTypeName)) return null;
            if (e.GetType().Name == exceptionTypeName) return e;
            return e.InnerException?.FindException(exceptionTypeName);
        }
    }
}
