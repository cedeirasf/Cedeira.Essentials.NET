using System.Diagnostics;

namespace Cedeira.Essentials.NET.ExceptionHandling
{
    /// <summary>
    /// Métodos de extensión para un uso más fluido del manejo de excepciones
    /// </summary>
    public static class ExceptionHandlerExtensions
    {
        /// <summary>
        /// Ejecuta una acción manejando automáticamente cualquier excepción
        /// </summary>
        public static void ExecuteWithHandling(this Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            try
            {
                action();
            }
            catch (Exception ex)
            {
                HandleExceptionSafely(ex);
            }
        }

        /// <summary>
        /// Ejecuta una función manejando automáticamente cualquier excepción
        /// reemplazando la respuesta por el valor por defecto si es necesario
        /// </summary>
        public static T ExecuteWithHandling<T>(this Func<T> func, T defaultValue = default)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return HandleExceptionSafely(ex, defaultValue);
            }
        }

        /// <summary>
        /// Ejecuta una acción asíncrona manejando automáticamente cualquier excepción
        /// </summary>
        public static async Task ExecuteWithHandlingAsync(this Func<Task> asyncAction)
        {
            if (asyncAction == null)
                throw new ArgumentNullException(nameof(asyncAction));

            try
            {
                await asyncAction();
            }
            catch (Exception ex)
            {
                HandleExceptionSafely(ex);
            }
        }

        /// <summary>
        /// Ejecuta una función asíncrona manejando automáticamente cualquier excepción,
        /// reemplazando la respuesta por el valor por defecto si es necesario
        /// </summary>
        public static async Task<T> ExecuteWithHandlingAsync<T>(this Func<Task<T>> asyncFunc, T defaultValue = default)
        {
            if (asyncFunc == null)
                throw new ArgumentNullException(nameof(asyncFunc));

            try
            {
                return await asyncFunc();
            }
            catch (Exception ex)
            {
                return HandleExceptionSafely(ex, defaultValue);
            }
        }

        /// <summary>
        /// Maneja una excepción de forma segura para métodos void
        /// </summary>
        private static void HandleExceptionSafely(Exception ex)
        {
            try
            {
                // Primero encolar la excepción
                ExceptionHandler.EnqueueException(ex);

                // Luego intentar manejar (esto puede lanzar excepciones transformadas)
                var result = ExceptionHandler.Handle(ex);
                Debug.WriteLine($"Exception handled with result: {result}");
            }
            catch (Exception transformedEx)
            {
                // Si se lanzó una excepción transformada, la capturamos y logueamos
                Debug.WriteLine($"Exception transformed and caught: {transformedEx.Message}");
            }
        }

        /// <summary>
        /// Maneja una excepción de forma segura para métodos con retorno, reemplazando la respuesta por el valor por defecto
        /// </summary>
        private static T HandleExceptionSafely<T>(Exception ex, T defaultValue)
        {
            try
            {
                // Encolar la excepción original
                ExceptionHandler.EnqueueException(ex);

                // Usar Run para manejar la excepción
                var result = ExceptionHandler.Run(() => throw ex);

                // Si se proporcionó un defaultValue explícito (no es el valor por defecto del tipo), usarlo
                if (!EqualityComparer<T>.Default.Equals(defaultValue, default(T)))
                {
                    return defaultValue;
                }

                // Si no se proporcionó defaultValue, usar el resultado del handler si es del tipo correcto
                if (result is T typedResult)
                {
                    return typedResult;
                }

                return defaultValue; // Si no coincide el tipo, retornar el defaultValue (que será default(T))
            }
            catch (Exception safeEx)
            {
                // Si Run lanza una excepción (por transformación), intentar manejarla también
                try
                {
                    ExceptionHandler.EnqueueException(safeEx);
                    var finalResult = ExceptionHandler.Handle(safeEx);

                    // Misma lógica para el defaultValue
                    if (!EqualityComparer<T>.Default.Equals(defaultValue, default(T)))
                    {
                        return defaultValue;
                    }

                    if (finalResult is T finalTypedResult)
                    {
                        return finalTypedResult;
                    }

                    return defaultValue;
                }
                catch
                {
                    return defaultValue;
                }
            }
        }
    }
}