using System;

namespace Cedeira.Essentials.NET.System.Resilience.Fallback
{
    /// <summary>
    /// Excepción utilizada para forzar un escape anticipado en FallbackStrategy.Coalesce.
    /// </summary>
    public class FallbackStrategyException : Exception
    {
        public FallbackStrategyException() { }
        public FallbackStrategyException(string message) : base(message) { }
        public FallbackStrategyException(string message, Exception inner) : base(message, inner) { }
    }
} 