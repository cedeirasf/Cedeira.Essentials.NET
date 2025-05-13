using System;

namespace Cedeira.Essentials.NET.System.Resilience.Fallback
{
    /// <summary>
    /// Excepción utilizada para forzar un escape anticipado en <see cref="FallbackStrategy.Coalesce"/>.
    /// <para>
    /// Esta excepción permite interrumpir la evaluación de proveedores de valores en el método Coalesce, propagando el error de forma inmediata.
    /// Es útil cuando se requiere abortar el fallback y notificar un fallo crítico o una condición no recuperable.
    /// </para>
    /// <example>
    /// Ejemplo de uso:
    /// <code>
    /// var resultado = FallbackStrategy.Coalesce(
    ///     () => ObtenerValorPrimario(),
    ///     () => ObtenerValorSecundario(),
    ///     () => throw new FallbackStrategyException("No se pudo obtener un valor válido")
    /// );
    /// </code>
    /// En este ejemplo, si ninguna función retorna un valor válido, se lanza la excepción y se detiene la evaluación.
    /// </example>
    /// </summary>
    public class FallbackStrategyException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FallbackStrategyException"/>.
        /// Utilice este constructor cuando no sea necesario especificar un mensaje de error.
        /// </summary>
        public FallbackStrategyException() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FallbackStrategyException"/> con un mensaje de error específico.
        /// Utilice este constructor para proporcionar detalles adicionales sobre la causa del escape anticipado.
        /// </summary>
        /// <param name="message">El mensaje que describe el error.</param>
        public FallbackStrategyException(string message) : base(message) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FallbackStrategyException"/> con un mensaje de error y una excepción interna.
        /// Utilice este constructor cuando desee propagar la excepción original que causó el escape anticipado.
        /// </summary>
        /// <param name="message">El mensaje que describe el error.</param>
        /// <param name="inner">La excepción que es la causa de la excepción actual.</param>
        public FallbackStrategyException(string message, Exception inner) : base(message, inner) { }
    }
} 