using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub.Internals
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    internal class EventBase : IEvent
    {
        private List<WeakListener> Listeners { get; } = new List<WeakListener>();
 
        protected EventBase(string group, string name, Type payloadType)
        {
            Name = name;
            Group = group;
            PayloadType = payloadType;
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        internal string Group { get; }

        /// <inheritdoc/>
        public string FullName => string.IsNullOrWhiteSpace(Group) ? Name : $"{Group}.{Name}";

        /// <inheritdoc/>
        public Type PayloadType { get; }

        /// <summary>
        /// Registers this as a thing to listen to.
        /// </summary>
        /// <param name="action">The action to fire.</param>
        protected void SubscribeDelegate(Delegate action)
        {
            Listeners.Add(new WeakListener(action));
        }
        /// <summary>
        /// Unregisters this as a thing to no longer listen to.
        /// </summary>
        /// <param name="action">The action to no longer fire.</param>
        protected void UnsubscribeDelegate(Delegate action)
        {
            var other = new WeakListener(action);
            Listeners.RemoveAll(m => other.Equals(m));
        }

        /// <summary>
        /// Fires this event.
        /// </summary>
        /// <param name="payload">The payload for the event.</param>
        /// <returns>Asynchronous handle.</returns>
        internal async Task InvokeAsync(object payload)
        {
            // Consider doing this with a Parallel.ForEach()
            foreach (var listener in Listeners)
            {
                await listener.PerformAsync(payload);
            }
            Listeners.RemoveAll(l => l.Invalid);
        }

        private string GetDebuggerDisplay()
        {
            return FullName;
        }
    }
}
