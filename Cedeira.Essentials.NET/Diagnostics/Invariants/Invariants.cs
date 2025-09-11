namespace Cedeira.Essentials.NET.Diagnostics.Invariants
{
    public static class Invariants
    {
        public static InvariantValidator<T> For<T>(T value)
        {
            return new InvariantValidator<T>(value);
        }
    }
}