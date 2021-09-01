using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hoarked.PubSub.Tests
{
    public abstract class TestBase
    {
        protected IEventHub CreateHub()
        {
            var services = new ServiceCollection();
            services.AddPubSubHub();
            var sp = services.BuildServiceProvider();

            return sp.GetService<IEventHub>();
        }
    }
}
