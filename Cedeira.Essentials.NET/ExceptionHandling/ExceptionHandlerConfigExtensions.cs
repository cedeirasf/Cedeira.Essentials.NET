namespace Cedeira.Essentials.NET.ExceptionHandling
{
    /// <summary>
    /// Proporciona métodos de extensión para configurar el comportamiento de manejo de excepciones en ExceptionHandlerConfig.
    /// </summary>
    public static class ExceptionHandlerConfigExtensions
    {
        /// <summary>
        /// Configura la transformación de la excepción para que incluya un mensaje personalizado.
        /// </summary>
        public static ExceptionHandlerConfig WithMessage(this ExceptionHandlerConfig config, string message)
        {
            config.Transform = ex =>
            {
                var excType = ex.GetType();

                var instance = Activator.CreateInstance(excType, message, ex) as Exception
                            ?? Activator.CreateInstance(excType, message) as Exception;

                return instance ?? ex;
            };

            return config;
        }

        /// <summary>
        /// Configura la transformación de la excepción usando una función personalizada que retorna una excepción del tipo especificado.
        /// </summary>
        public static ExceptionHandlerConfig TransformTo<TException>(this ExceptionHandlerConfig config, Func<Exception, TException> transform)
            where TException : Exception
        {
            config.Transform = transform;
            return config;
        }

        /// <summary>
        /// Asocia una acción personalizada que se ejecutará cuando se maneje la excepción.
        /// </summary>
        public static ExceptionHandlerConfig WithAction(this ExceptionHandlerConfig config, Func<Exception, object> action)
        {
            config.Action = action;
            return config;
        }
    }
}