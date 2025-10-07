namespace Cedeira.Essentials.NET.Extensions.Options
{
    /// <summary>
    /// Builder for options. Implements the build pattern.
    /// 
    /// This class is used to build options for the application.
    /// It is a base class for all options builders.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OptionsBuilder<T>
    {
        public abstract T Build();
    }
}
