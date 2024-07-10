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

        public InvariantValidator<T> IsEqual(T expected, string errorMessage = null)
        {
            if (!Equals(_value, expected))
            {
                throw new ArgumentException(errorMessage ?? $"El valor debe ser igual a {expected}.");
            }
            return this;
        }
        public InvariantValidator<T> IsNotNull(string errorMessage = null)
        {
            if (_value == null)
            {
                throw new ArgumentNullException(errorMessage ?? "El valor no puede ser nulo");
            }
            return this;
        }

        public InvariantValidator<T> IsNotNullOrEmpty(string errorMessage = null)
        {
            if (_value is string x && string.IsNullOrEmpty(x))
            {
                throw new ArgumentException(errorMessage ?? "El valor no puede ser nulo o estar vacío.");
            }
            return this;
        }

        public InvariantValidator<T> MaximumLength(int maxLength, string errorMessage = null)
        {
            if (_value is string x && x.Length > maxLength)
            {
                throw new ArgumentException(errorMessage ?? $"El valor no puede excedecerse de {maxLength} caracteres.");
            }
            return this;
        }

        public InvariantValidator<T> MatchesRegex(string pattern, string errorMessage = null)
        {
            if (_value is string x && !Regex.IsMatch(x, pattern))
            {
                throw new FormatException(errorMessage ?? $"El formato del valor debe seguir el patrón {pattern}.");
            }
            return this;
        }

        //public InvariantValidator<T> Custom(Action<T> validationAction);
    }
}

