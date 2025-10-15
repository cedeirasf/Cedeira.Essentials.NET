using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Cedeira.Essentials.NET.ExceptionHandling
{
    /// <summary>
    /// Representa la configuración para el manejo de excepciones, permitiendo definir acciones y transformaciones personalizadas.
    /// </summary>
    public class ExceptionHandlerConfig
    {
        public Func<Exception, Exception>? Transform { get; set; }
        public Func<Exception, object>? Action { get; set; }
    }

    /// <summary>
    /// Proporciona mecanismos centralizados y universales para el manejo, transformación y registro de excepciones en la aplicación,
    /// incluyendo soporte para manejo global, canales asíncronos y servicios en segundo plano.
    /// </summary>
    public static class ExceptionHandler
    {
        private static readonly ConcurrentDictionary<Type, ExceptionHandlerConfig> _handlers = new();
        private static bool _globalHandlingEnabled = false;
        private static bool _universalHookInitialized = false;

        private static UnhandledExceptionEventHandler? _domainHandler;
        private static EventHandler<UnobservedTaskExceptionEventArgs>? _taskHandler;

        private static readonly Channel<Exception> _exceptionChannel =
            Channel.CreateUnbounded<Exception>();

        public static bool IsGlobalHandlingEnabled => _globalHandlingEnabled;
        public static bool IsUniversalHookInitialized => _universalHookInitialized;

        /// <summary>
        /// Obtiene o crea la configuración de manejo para el tipo de excepción especificado.
        /// </summary>
        public static ExceptionHandlerConfig For<TException>() where TException : Exception
        {
            var config = new ExceptionHandlerConfig();
            _handlers[typeof(TException)] = config;
            return config;
        }

        /// <summary>
        /// Ejecuta la función proporcionada y maneja cualquier excepción lanzada.
        /// </summary>
        public static object Run(Func<object> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return Handle(ex);
            }
        }

        /// <summary>
        /// Ejecuta de forma asíncrona la función proporcionada y maneja cualquier excepción lanzada.
        /// </summary>
        public static async Task<object> RunAsync(Func<Task<object>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return Handle(ex);
            }
        }

        /// <summary>
        /// Maneja la excepción especificada según la configuración registrada.
        /// </summary>
        public static object Handle(Exception ex)
        {
            return HandleInternal(ex, new HashSet<Type>());
        }

        /// <summary>
        /// Maneja recursivamente la excepción especificada, procesando transformaciones y acciones
        /// en la jerarquía de tipos de excepción, con protección contra recursión infinita.
        /// </summary>
        /// <param name="ex">Excepción a manejar</param>
        /// <param name="processedTypes">Conjunto de tipos ya procesados para prevenir recursión infinita</param>
        /// <returns>Resultado de la acción del handler o lanza la excepción si no se encuentra handler</returns>
        private static object HandleInternal(Exception ex, HashSet<Type> processedTypes)
        {
            var type = ex.GetType();

            // Prevenir recursión infinita
            if (processedTypes.Contains(type))
            {
                throw ex;
            }
            processedTypes.Add(type);

            // Buscar handler exacto o en jerarquía
            var handlerType = type;
            while (handlerType != null && handlerType != typeof(object))
            {
                if (_handlers.TryGetValue(handlerType, out var config))
                {
                    if (config.Transform != null)
                    {
                        var transformedEx = config.Transform(ex);
                        // Procesar recursivamente la excepción transformada
                        return HandleInternal(transformedEx, processedTypes);
                    }

                    if (config.Action != null)
                        return config.Action(ex);
                }

                handlerType = handlerType.BaseType;
            }

            throw ex;
        }

        /// <summary>
        /// Habilita el manejo global de excepciones en el dominio de la aplicación y tareas.
        /// </summary>
        public static void EnableGlobalHandling(bool swallow = false)
        {
            if (_globalHandlingEnabled) return;
            _globalHandlingEnabled = true;

            _domainHandler = (sender, args) =>
            {
                var ex = args.ExceptionObject as Exception;
                if (ex == null) return;

                try
                {
                    var result = Handle(ex);
                    if (!swallow)
                        Console.WriteLine($"[Unhandled Exception]: {result}");
                }
                catch (Exception transformed)
                {
                    if (!swallow)
                        Console.WriteLine($"[Transformed Exception]: {transformed.Message}");
                }
            };

            _taskHandler = (sender, args) =>
            {
                try
                {
                    var result = Handle(args.Exception);
                    if (!swallow)
                        Console.WriteLine($"[Unobserved Task Exception]: {result}");
                }
                catch (Exception transformed)
                {
                    if (!swallow)
                        Console.WriteLine($"[Transformed Exception]: {transformed.Message}");
                }

                args.SetObserved();
            };

            AppDomain.CurrentDomain.UnhandledException += _domainHandler;
            TaskScheduler.UnobservedTaskException += _taskHandler;
        }

        /// <summary>
        /// Deshabilita el manejo global de excepciones.
        /// </summary>
        public static void DisableGlobalHandling()
        {
            if (!_globalHandlingEnabled) return;

            if (_domainHandler != null)
                AppDomain.CurrentDomain.UnhandledException -= _domainHandler;

            if (_taskHandler != null)
                TaskScheduler.UnobservedTaskException -= _taskHandler;

            _domainHandler = null;
            _taskHandler = null;
            _globalHandlingEnabled = false;
        }

        /// <summary>
        /// Inicializa el manejo universal de excepciones en el dominio de la aplicación y tareas.
        /// </summary>
        public static void InitializeUniversalHandling()
        {
            if (_universalHookInitialized) return;

            _universalHookInitialized = true;

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                if (args.ExceptionObject is Exception ex)
                {
                    HandleUniversalException(ex, "AppDomain.UnhandledException");
                }
            };

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                HandleUniversalException(args.Exception, "TaskScheduler.UnobservedTaskException");
                args.SetObserved();
            };

            EnableGlobalHandling();
        }

        /// <summary>
        /// Escribe una excepción en el canal de excepciones de forma asíncrona.
        /// </summary>
        public static async ValueTask WriteExceptionAsync(Exception exception)
        {
            await _exceptionChannel.Writer.WriteAsync(exception);
        }

        /// <summary>
        /// Lee todas las excepciones del canal de forma asíncrona.
        /// </summary>
        public static IAsyncEnumerable<Exception> ReadExceptionsAsync(CancellationToken cancellationToken = default)
        {
            return _exceptionChannel.Reader.ReadAllAsync(cancellationToken);
        }

        /// <summary>
        /// Encola una excepción para su procesamiento asíncrono.
        /// </summary>
        public static void EnqueueException(Exception exception)
        {
            _ = WriteExceptionAsync(exception); // Fire-and-forget
        }

        /// <summary>
        /// Maneja una excepción universal y la registra junto con su origen.
        /// </summary>
        private static void HandleUniversalException(Exception exception, string source)
        {
            try
            {
                var result = Handle(exception);
                Console.WriteLine($"[{source}] Exception handled: {result}");
                EnqueueException(exception);
            }
            catch (Exception handlerEx)
            {
                Console.WriteLine($"[{source}] Error in exception handler: {handlerEx.Message}");
            }
        }

        /// <summary>
        /// Procesa una excepción de forma asíncrona, útil para servicios en segundo plano.
        /// </summary>
        public static async Task HandleExceptionAsync(Exception exception)
        {
            try
            {
                await Task.CompletedTask;
                Console.WriteLine($"[Async Processing] Exception processed: {exception.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Async Processing] Error handling exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Cierra y limpia todos los recursos del ExceptionHandler
        /// </summary>
        public static void Shutdown()
        {
            // 1. Desuscribir event handlers globales
            DisableGlobalHandling();

            // 2. Limpiar todos los handlers registrados
            ClearHandlers();

            // 3. Resetear estado universal
            ResetUniversalHandling();
        }

        /// <summary>
        /// Limpia todos los handlers registrados
        /// </summary>
        public static void ClearHandlers()
        {
            _handlers.Clear();
        }

        /// <summary>
        /// Resetea el estado de universal handling
        /// </summary>
        public static void ResetUniversalHandling()
        {
            _universalHookInitialized = false;
            _globalHandlingEnabled = false;
            _domainHandler = null;
            _taskHandler = null;
        }
    }
}