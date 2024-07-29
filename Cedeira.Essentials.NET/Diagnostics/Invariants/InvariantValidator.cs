using System.Text.RegularExpressions;

namespace Cedeira.Essentials.NET.Diagnostics.Invariants
{
    public class InvariantValidator<T>
    {
        private readonly T _value;

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
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }
            if (!Equals(_value, expected))
            {
                throw new ArgumentException(errorMessage ?? $"Value must be equal to {expected}.");
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
            return this.IsNotNull("Value cannot be null.");
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
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }
            if (_value == null)
            {
                throw new ArgumentNullException(errorMessage ?? "Value cannot be null.");
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
            return this.IsNotNullOrEmpty("Value cannot be null or empty.");
        }

        /// <summary>
        /// Verifica que el valor no sea nulo esté vacío.
        /// Lanza una ArgumentException si el valor es una cadena vacía.
        /// </summary>
        /// <param name="errorMessage">El mensaje de error específico a lanzar si el valor es nulo o está vacío.</param>
        /// <returns>El propio InvariantValidator para permitir chaining.</returns>
        /// <exception cref="ArgumentNullException">Si el mensaje de error es nulo o vacío.</exception>
        /// <exception cref="ArgumentException">Si el valor es una cadena vacía.</exception>
        public InvariantValidator<T> IsNotNullOrEmpty(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }
            if (_value is string x && string.IsNullOrEmpty(x))
            {
                throw new ArgumentException(errorMessage ?? "Value cannot be null or empty.");
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
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }
            if (_value is string x && x.Length > maxLength)
            {
                throw new ArgumentException(errorMessage ?? $"Value cannot exceed {maxLength} characters.");
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
            return this.MatchesRegex(pattern, "Value cannot contain especial characters");
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
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }
            if (_value is string x && !Regex.IsMatch(x, pattern))
            {
                throw new FormatException(errorMessage ?? "Value cannot contain especial characters.");
            }
            return this;
        }


    }
}

