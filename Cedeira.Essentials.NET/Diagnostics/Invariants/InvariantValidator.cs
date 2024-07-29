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

        public InvariantValidator<T> IsEqual(T expected)
        {
            return this.IsEqual(expected, $"Value must be equal to {expected}.");
        }

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
        public InvariantValidator<T> IsNotNull()
        {
            return this.IsNotNull("Value cannot be null.");
        }

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
        public InvariantValidator<T> IsNotNullOrEmpty()
        {
            return this.IsNotNullOrEmpty("Value cannot be null or empty.");
        }
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

        public InvariantValidator<T> MaximumLength(int maxLength)
        {
            return this.MaximumLength(maxLength, $"Value cannot exceed {maxLength} characters.");
        }

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

        public InvariantValidator<T> MatchesRegex(string pattern)
        {
            return this.MatchesRegex(pattern, "Value cannot contain especial characters");
        }

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

