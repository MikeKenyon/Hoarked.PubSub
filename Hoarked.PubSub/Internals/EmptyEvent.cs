using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub.Internals
{
    internal class EmptyEvent : EventBase, IEmptyEvent
    {
        /// <summary>
        /// Createa an event without a payload.
        /// </summary>
        /// <param name="group">The group name for the event.</param>
        /// <param name="name">The name of the event.</param>
        internal EmptyEvent(string group, string name) : base(group, name, typeof(void)) { }

        /// <inheritdoc/>
        public void Publish()
        {
            InvokeAsync(null).Wait();
        }

        /// <inheritdoc/>
        public async Task PublishAsync()
        {
            await InvokeAsync(null);
        }

        /// <inheritdoc/>
        public void Subscribe(Func<Task> handler)
        {
            SubscribeDelegate(handler);
        }

        /// <inheritdoc/>
        public void Subscribe(Action handler)
        {
            SubscribeDelegate(handler);
        }

        /// <inheritdoc/>
        public void Unsubscribe(Func<Task> handler)
        {
            UnsubscribeDelegate(handler);
        }

        /// <inheritdoc/>
        public void Unsubscribe(Action handler)
        {
            UnsubscribeDelegate(handler);
        }
    }
}
