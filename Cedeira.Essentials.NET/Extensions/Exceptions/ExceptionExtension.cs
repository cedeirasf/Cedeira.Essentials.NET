namespace Cedeira.Essentials.NET.Extensions.Exceptions
{
    /// <summary>
    /// Provides extension methods for the Exception class to obtain complete messages and advanced exception handling.
    /// </summary>
    public static class ExceptionExtension
    {
        private const string DefaultSeparator = ". ";

        /// <summary>
        /// Retrieves the full message of an exception, including all inner exceptions, separated by the default separator.
        /// </summary>
        /// <param name="e">The exception instance.</param>
        /// <returns>The full message of the exception.</returns>
        public static string FullMessage(this Exception e)
        {
            return e.FullMessage(DefaultSeparator);
        }

        /// <summary>
        /// Retrieves the full message of an exception, including all inner exceptions, using a custom separator.
        /// </summary>
        /// <param name="e">The exception instance.</param>
        /// <param name="separator">The separator string.</param>
        /// <returns>The full message of the exception.</returns>
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
        /// Retrieves the message of the last nested exception.
        /// </summary>
        /// <param name="e">The exception instance.</param>
        /// <returns>The message of the last nested exception.</returns>
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
        /// Determines whether the exception or any of its InnerExceptions is of the specified generic type.
        /// </summary>
        /// <typeparam name="T">The exception type to search for.</typeparam>
        /// <param name="e">The exception instance.</param>
        /// <returns>True if an exception of the specified type is found; otherwise, false.</returns>
        /// <example>
        /// <code>
        /// try {
        ///     // ...
        /// } catch (Exception ex) {
        ///     if (ex.ContainsException<ArgumentNullException>()) {
        ///         // Handle ArgumentNullException somewhere in the chain
        ///     }
        /// }
        /// </code>
        /// </example>
        public static bool ContainsException<T>(this Exception e) where T : Exception
        {
            while (e != null)
            {
                if (e is T) return true;
                e = e.InnerException;
            }
            return false;
        }

        /// <summary>
        /// Finds the first nested exception that matches the specified generic type.
        /// </summary>
        /// <typeparam name="T">The exception type to search for.</typeparam>
        /// <param name="e">The exception instance.</param>
        /// <returns>The first exception found of the specified type, or null if not found.</returns>
        /// <example>
        /// <code>
        /// try {
        ///     // ...
        /// } catch (Exception ex) {
        ///     var argEx = ex.FindException<ArgumentNullException>();
        ///     if (argEx != null) {
        ///         // Use argEx
        ///     }
        /// }
        /// </code>
        /// </example>
        public static T? FindException<T>(this Exception e) where T : Exception
        {
            while (e != null)
            {
                if (e is T match) return match;
                e = e.InnerException;
            }
            return null;
        }

        /// <summary>
        /// Determines whether the exception or any of its InnerExceptions is of the specified type.
        /// </summary>
        /// <param name="e">The exception instance.</param>
        /// <param name="exceptionType">The exception type to search for.</param>
        /// <returns>True if an exception of the specified type is found; otherwise, false.</returns>
        /// <example>
        /// <code>
        /// if (ex.ContainsException(typeof(ArgumentNullException))) {
        ///     // Handle ArgumentNullException somewhere in the chain
        /// }
        /// </code>
        /// </example>
        public static bool ContainsException(this Exception e, Type exceptionType)
        {
            while (e != null && exceptionType != null)
            {
                if (exceptionType.IsInstanceOfType(e)) return true;
                e = e.InnerException;
            }
            return false;
        }

        /// <summary>
        /// Finds the first nested exception that matches the specified type.
        /// </summary>
        /// <param name="e">The exception instance.</param>
        /// <param name="exceptionType">The exception type to search for.</param>
        /// <returns>The first exception found of the specified type, or null if not found.</returns>
        /// <example>
        /// <code>
        /// var argEx = ex.FindException(typeof(ArgumentNullException));
        /// if (argEx != null) {
        ///     // Use argEx
        /// }
        /// </code>
        /// </example>
        public static Exception? FindException(this Exception e, Type exceptionType)
        {
            while (e != null && exceptionType != null)
            {
                if (exceptionType.IsInstanceOfType(e)) return e;
                e = e.InnerException;
            }
            return null;
        }

        /// <summary>
        /// Determines whether the exception or any of its InnerExceptions is of the specified type name.
        /// </summary>
        /// <param name="e">The exception instance.</param>
        /// <param name="exceptionTypeName">The name of the exception type to search for.</param>
        /// <returns>True if an exception of the specified type name is found; otherwise, false.</returns>
        /// <example>
        /// <code>
        /// if (ex.ContainsException("ArgumentNullException")) {
        ///     // Handle ArgumentNullException somewhere in the chain
        /// }
        /// </code>
        /// </example>
        public static bool ContainsException(this Exception e, string exceptionTypeName)
        {
            while (e != null && !string.IsNullOrWhiteSpace(exceptionTypeName))
            {
                if (e.GetType().Name == exceptionTypeName) return true;
                e = e.InnerException;
            }
            return false;
        }

        /// <summary>
        /// Finds the first nested exception that matches the specified type name.
        /// </summary>
        /// <param name="e">The exception instance.</param>
        /// <param name="exceptionTypeName">The name of the exception type to search for.</param>
        /// <returns>The first exception found of the specified type name, or null if not found.</returns>
        /// <example>
        /// <code>
        /// var argEx = ex.FindException("ArgumentNullException");
        /// if (argEx != null) {
        ///     // Use argEx
        /// }
        /// </code>
        /// </example>
        public static Exception? FindException(this Exception e, string exceptionTypeName)
        {
            while (e != null && !string.IsNullOrWhiteSpace(exceptionTypeName))
            {
                if (e.GetType().Name == exceptionTypeName) return e;
                e = e.InnerException;
            }
            return null;
        }
    }
}
