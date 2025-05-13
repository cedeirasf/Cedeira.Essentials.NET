using System;
using System.Runtime.Serialization;

namespace Cedeira.Essentials.NET.System.Resilience.Fallback
{
    /// <summary>
    /// Exception used to force an early escape in <see cref="FallbackStrategy.Coalesce"/>.
    /// <para>
    /// This exception allows to interrupt the evaluation of value providers in the Coalesce method, propagating the error immediately.
    /// It is useful when you need to abort the fallback and notify a critical failure or an unrecoverable condition.
    /// </para>
    /// <example>
    /// Usage example:
    /// <code>
    /// var result = FallbackStrategy.Coalesce(
    ///     () => GetPrimaryValue(),
    ///     () => GetSecondaryValue(),
    ///     () => throw new FallbackStrategyException("No valid value could be obtained")
    /// );
    /// </code>
    /// In this example, if no function returns a valid value, the exception is thrown and the evaluation stops.
    /// </example>
    /// </summary>
    [Serializable]
    public class FallbackStrategyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FallbackStrategyException"/> class.
        /// Use this constructor when no error message is needed.
        /// </summary>
        public FallbackStrategyException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FallbackStrategyException"/> class with a specific error message.
        /// Use this constructor to provide additional details about the cause of the early escape.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FallbackStrategyException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FallbackStrategyException"/> class with a specific error message and an inner exception.
        /// Use this constructor when you want to propagate the original exception that caused the early escape.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public FallbackStrategyException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FallbackStrategyException"/> class with serialized data.
        /// This constructor is used during deserialization to reconstruct the exception object transmitted over a stream.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected FallbackStrategyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
} 