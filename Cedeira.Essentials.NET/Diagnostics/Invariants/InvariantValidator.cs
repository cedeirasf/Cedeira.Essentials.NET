using System;
using System.Text.RegularExpressions;

namespace Cedeira.Essentials.NET.Diagnostics.Invariants
{
    public class InvariantValidator<T>
    {
        private readonly T _value;
        private const string DefaultNullErrorMessage = "Value cannot be null or empty.";
        private const string DefaultSpecialCharactersMessage = "Value cannot contain special characters";
        private const string ErrorMessageParamName = "errorMessage";

        public InvariantValidator(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Verifica que el valor sea igual al valor esperado.
        /// Lanza una ArgumentException si los valores no son iguales.
        /// </summary>
        /// <param name="expected">El valor esperado.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        public InvariantValidator<T> IsEqual(T expected)
        {
            return this.IsEqual(expected, $"Value must be equal to {expected}.");
        }

        /// <summary>
        /// Verifica que el valor sea igual al valor esperado.
        /// Lanza una ArgumentException si los valores no son iguales.
        /// </summary>
        /// <param name="expected">El valor esperado.</param>
        /// <param name="errorMessage">El mensaje de error específico a lanzar si los valores no son iguales.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        /// <exception cref="ArgumentNullException">Si el mensaje de error es nulo o vacío.</exception>
        /// <exception cref="ArgumentException">Si los valores no son iguales.</exception>
        public InvariantValidator<T> IsEqual(T expected, string errorMessage)
        {
            ValidateErrorMessage(errorMessage);

            if (!Equals(_value, expected))
            {
                throw new ArgumentException(errorMessage);
            }
            return this;
        }

        /// <summary>
        /// Verifica que el valor no sea nulo.
        /// Lanza una ArgumentNullException si el valor es nulo.
        /// </summary>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        public InvariantValidator<T> IsNotNull()
        {
            return this.IsNotNull(DefaultNullErrorMessage);
        }

        /// <summary>
        /// Verifica que el valor no sea nulo.
        /// Lanza una ArgumentNullException si el valor es nulo.
        /// </summary>
        /// <param name="errorMessage">El mensaje de error específico a lanzar si el valor es nulo.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        /// <exception cref="ArgumentNullException">Si el mensaje de error es nulo o vacío, o si el valor es nulo.</exception>
        public InvariantValidator<T> IsNotNull(string errorMessage)
        {
            ValidateErrorMessage(errorMessage);

            if (_value == null)
            {
                throw new ArgumentNullException(errorMessage);
            }
            return this;
        }

        /// <summary>
        /// Verifica que el valor no sea nulo o esté vacío.
        /// Lanza una ArgumentException si el valor es una cadena vacía.
        /// </summary>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        public InvariantValidator<T> IsNotNullOrEmpty()
        {
            return this.IsNotNullOrEmpty(DefaultNullErrorMessage);
        }

        /// <summary>
        /// Verifica que el valor no sea nulo o esté vacío.
        /// Lanza una ArgumentException si el valor es nulo o una cadena vacía.
        /// </summary>
        /// <param name="errorMessage">El mensaje de error específico a lanzar si el valor es nulo o está vacío.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        /// <exception cref="ArgumentNullException">Si el mensaje de error es nulo o vacío.</exception>
        /// <exception cref="ArgumentException">Si el valor es nulo o está vacío.</exception>
        public InvariantValidator<T> IsNotNullOrEmpty(string errorMessage)
        {
            ValidateErrorMessage(errorMessage);

            if (_value == null)
            {
                throw new ArgumentNullException(errorMessage);
            }

            if (_value is string stringValue && string.IsNullOrEmpty(stringValue))
            {
                throw new ArgumentException(errorMessage);
            }

            return this;
        }

        /// <summary>
        /// Verifica que la longitud del valor no exceda la longitud máxima especificada.
        /// Lanza una ArgumentException si la longitud del valor excede la longitud máxima.
        /// </summary>
        /// <param name="maxLength">La longitud máxima permitida.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        public InvariantValidator<T> MaximumLength(int maxLength)
        {
            return this.MaximumLength(maxLength, $"Value cannot exceed {maxLength} characters.");
        }

        /// <summary>
        /// Verifica que la longitud del valor no exceda la longitud máxima especificada.
        /// Lanza una ArgumentException si la longitud del valor excede la longitud máxima.
        /// </summary>
        /// <param name="maxLength">La longitud máxima permitida.</param>
        /// <param name="errorMessage">El mensaje de error específico a lanzar si la longitud del valor excede la longitud máxima.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        /// <exception cref="ArgumentNullException">Si el mensaje de error es nulo o vacío.</exception>
        /// <exception cref="ArgumentException">Si la longitud del valor excede la longitud máxima.</exception>
        public InvariantValidator<T> MaximumLength(int maxLength, string errorMessage)
        {
            ValidateErrorMessage(errorMessage);

            if (maxLength < 0)
            {
                throw new ArgumentException("Maximum length cannot be negative.", nameof(maxLength));
            }

            if (_value is string stringValue && stringValue.Length > maxLength)
            {
                throw new ArgumentException(errorMessage);
            }
            return this;
        }

        /// <summary>
        /// Verifica que el valor no contenga caracteres especiales.
        /// Lanza una FormatException si el valor contiene caracteres especiales.
        /// </summary>
        /// <param name="pattern">El patrón de expresión regular con el que se debe comparar el valor.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        public InvariantValidator<T> MatchesRegex(string pattern)
        {
            return this.MatchesRegex(pattern, DefaultSpecialCharactersMessage);
        }

        /// <summary>
        /// Verifica que el valor no contenga caracteres especiales.
        /// Lanza una FormatException si el valor contiene caracteres especiales.
        /// </summary>
        /// <param name="pattern">El patrón de expresión regular con el que se debe comparar el valor.</param>
        /// <param name="errorMessage">El mensaje específico de error a lanzar si el valor contiene caracteres especiales.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        /// <exception cref="ArgumentNullException">Si el mensaje de error es nulo o vacío.</exception>
        /// <exception cref="FormatException">Si el valor contiene caracteres especiales.</exception>
        public InvariantValidator<T> MatchesRegex(string pattern, string errorMessage)
        {
            ValidateErrorMessage(errorMessage);

            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (_value is string stringValue)
            {
                try
                {
                    if (!Regex.IsMatch(stringValue, pattern, RegexOptions.Compiled))
                    {
                        throw new FormatException(errorMessage);
                    }
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException("Invalid regular expression pattern", nameof(pattern), ex);
                }
            }
            return this;
        }

        /// <summary>
        /// Verifica que el valor cumpla con una condición personalizada.
        /// Si es satisfactoria, no hace nada. Si no, lanza una ArgumentException.
        /// </summary>
        /// <param name="action">La acción de validación personalizada.</param>
        /// <param name="errorMessage">Mensaje de error opcional.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        /// <exception cref="ArgumentException">Si la validación falla.</exception>
        public InvariantValidator<T> CustomInvariant(Action<T> action, string? errorMessage = null)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            try
            {
                action(_value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(errorMessage ?? ex.Message, ex);
            }
            return this;
        }
        /// <summary>
        /// Compara _value con expected.
        /// Lanza una FormatException Si _value > expected.
        /// Si no es comparable, lanza excepción de tipo.
        /// Si ambos son comparables y válidos, retorna this para permitir encadenamiento
        /// </summary>
        /// <param name="expected">El patrón de expresión regular con el que se debe comparar el valor.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
 
        public InvariantValidator<T> LessThan(T expected)
        {
            if (_value == null)
                throw new ArgumentNullException(nameof(_value), "Value to compare cannot be null.");

            if (expected == null)
                throw new ArgumentNullException(nameof(expected), "Expected value cannot be null.");

            if (!(_value is IComparable<T> comparable))
                throw new InvalidOperationException($"Type {typeof(T).Name} does not support comparison.");
            if (comparable.CompareTo(expected) > 0)
            {
                throw new ArgumentException("value can not be higher than expected");
            }
            return this;
        }

        /// <summary>
        /// Compara el valor actual (_value) con el valor esperado.
        /// Lanza una ArgumentException si _value es menor que expected.
        /// Si el tipo T no implementa IComparable,lanza InvalidOperationException.
        /// Si ambos valores son válidos y comparables, retorna la instancia actual para permitir encadenamiento.
        /// </summary>
        /// <param name="expected">El valor con el que se debe comparar el valor actual.</param>
        /// <returns>La instancia actual de InvariantValidator para permitir encadenamiento (chaining).</returns>
        public InvariantValidator<T> GreaterThan(T expected)
        {
            if (_value == null)
                throw new ArgumentNullException(nameof(_value), "Value to compare cannot be null.");

            if (expected == null)
                throw new ArgumentNullException(nameof(expected), "Expected value cannot be null.");

            if (!(_value is IComparable<T> comparable))
                throw new InvalidOperationException($"Type {typeof(T).Name} does not support comparison.");
            if (comparable.CompareTo(expected) < 0)
            {
                throw new ArgumentException("value can not be less than expected");
            }
            return this;
        }
        
        /// <summary>
        /// Método Helper Privado para validar mensajes de error
        /// </summary>
        /// <param name="errorMessage">El mensaje de error a validar</param>
        /// <param name="paramName">El nombre del parámetro para la excepción</param>
        /// <exception cref="ArgumentNullException">Si el mensaje de error es nulo o vacío</exception>
        private static void ValidateErrorMessage(string errorMessage, string paramName = ErrorMessageParamName)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentNullException(paramName, "Error message cannot be null or empty.");
            }
        }
    }
}