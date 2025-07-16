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
        /// If any function throws a FallbackStrategyException, the exception is propagated and evaluation stops.
        /// All other exceptions are ignored and the next provider is evaluated.
        /// </summary>
        /// <typeparam name="T">The type of the value to return.</typeparam>
        /// <param name="valueProviders">A list of functions that provide values.</param>
        /// <returns>The first non-null value, or default(T) if none found.</returns>
        /// <exception cref="FallbackStrategyException">Thrown to force an early escape from the evaluation.</exception>
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
                catch (FallbackStrategyException)
                {
                    throw;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return default!;
        }

        /// <summary>
        /// Returns the first non-null value from a list of value providers (asynchronous version).
        /// If any function throws a FallbackStrategyException, the exception is propagated and evaluation stops.
        /// All other exceptions are ignored and the next provider is evaluated.
        /// </summary>
        /// <typeparam name="T">The type of the value to return.</typeparam>
        /// <param name="valueProviders">A list of asynchronous functions that provide values.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first non-null value, or default(T) if none found.</returns>
        /// <exception cref="FallbackStrategyException">Thrown to force an early escape from the evaluation.</exception>
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
                catch (FallbackStrategyException)
                {
                    throw;
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
