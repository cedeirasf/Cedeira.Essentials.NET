namespace Cedeira.Essentials.NET.Extensions.System.Exceptions
{
    public static class ExceptionExtension
    {
        private const string DefaultSeparator = ". ";

        public static string FullMessage(this Exception e)
        {
            return e.FullMessage(DefaultSeparator);
        }

        public static string FullMessage(this Exception e, string separator)
        {
            if (e is null) return string.Empty;
            var message = e.Message;
            if (e.InnerException is not null)
            {
                message += separator + e.InnerException.FullMessage(separator);
            }
            return message;
        }
    }
}
