using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub.Internals
{
    /// <summary>
    /// An event type designed for events that have a payload.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    internal class Event<TPayload> : EventBase, IEvent<TPayload>
    {
        /// <summary>
        /// Creates an event with a given name.
        /// </summary>
        /// <param name="group">The group it's part of.</param>
        /// <param name="name">The name of the event.</param>
        internal Event(string group, string name) : base(group, name, typeof(TPayload)) { }
        public void Publish(TPayload payload)
        {
            InvokeAsync(payload).Wait();
        }

        /// <inheritdoc/>
        public async Task PublishAsync(TPayload payload)
        {
            await InvokeAsync(payload);
        }

        /// <inheritdoc/>
        public void Subscribe(Func<TPayload, Task> handler)
        {
            SubscribeDelegate(handler);
        }

        /// <inheritdoc/>
        public void Subscribe(Action<TPayload> handler)
        {
            SubscribeDelegate(handler);
        }

        /// <inheritdoc/>
        public void Unsubscribe(Func<TPayload, Task> handler)
        {
            UnsubscribeDelegate(handler);
        }

        /// <inheritdoc/>
        public void Unsubscribe(Action<TPayload> handler)
        {
            UnsubscribeDelegate(handler);
        }
    }
}
