using Microsoft.Extensions.DependencyInjection;

namespace PokemonReviewApp
{
    public static class ServiceDependencies
    {
        static IServiceProvider _serviceProvider = default!;

        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static IServiceScope? CreateScope(this object obj, IServiceProvider serviceProvider = default!)
        {
            ArgumentNullException.ThrowIfNull(obj);
            var provider = serviceProvider ?? _serviceProvider;
            return provider?.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }

        public static T? GetService<T>(this object obj, IServiceProvider serviceProvider = default!)
        {
            return (T?)CreateScope(obj, serviceProvider)?.ServiceProvider.GetService(typeof(T));
        }
    }
}
