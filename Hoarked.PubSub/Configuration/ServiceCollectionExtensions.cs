using Hoarked.PubSub;
using Hoarked.PubSub.Internals;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions to the service collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a publication/subscriptoin hub.
        /// </summary>
        /// <param name="serviceCollection">The collection to add to.</param>
        /// <returns>Fluent continuation.</returns>
        public static IServiceCollection AddPubSubHub(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IEventHub, EventHub>();
            return serviceCollection;
        }
    }
}
