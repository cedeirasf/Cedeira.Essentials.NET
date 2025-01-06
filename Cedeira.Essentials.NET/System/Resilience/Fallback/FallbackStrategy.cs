using System.Reflection.Metadata.Ecma335;

namespace Cedeira.Essentials.NET.System.Resilience.Fallback
{
    public static class FallbackStrategy
    {
        public static T Coalesce<T>(params Func<T>[] valueProviders)
        {

            foreach (var valueProvider in valueProviders)
            {
                try
                {
                    var result = valueProvider();

                    if (result is string s)
                    {
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            return result;
                        }
                        else { continue; }
                    }

                    if (result is IEnumerable<object> collection)
                    {
                        if (collection != null && collection.Any())
                        {
                            return result;
                        }
                        else { continue; }
                    }

                    if (!EqualityComparer<T>.Default.Equals(result, default!))
                    {
                        return result;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return default!;
        }

        public static async Task<T> Coalesce<T>(params Func<Task<T>>[] valueProviders)
        {
            foreach (var valueProvider in valueProviders)
            {
                try
                {
                    var result = await valueProvider();

                    if (result is string s)
                    {
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            return result;
                        }
                        else { continue; }
                    }

                    if (result is IEnumerable<object> collection)
                    {
                        if (collection != null && collection.Any())
                        {
                            return result;
                        }
                        else { continue; }
                    }

                    if (!EqualityComparer<T>.Default.Equals(result, default!))
                    {
                        return result;
                    }

                }
                catch (Exception)
                {
                    continue;
                }
            }

            return default!;
        }
    }
}
