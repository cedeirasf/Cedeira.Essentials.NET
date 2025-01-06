using System.Reflection.Metadata.Ecma335;

namespace Cedeira.Essentials.NET.System.Resilience.Fallback
{
    /// <summary>
    /// class that provides a fallback strategy for multiple value providers.
    /// </summary>
    public static class FallbackStrategy
    {
        /// <summary>
        /// Returns the first non-null value from a list of value providers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueProviders"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the first non-null value from a list of value providers.Async version.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueProviders"></param>
        /// <returns></returns>
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
