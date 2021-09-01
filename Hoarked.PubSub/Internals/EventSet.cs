using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub.Internals
{
    /// <summary>
    /// Defines a list of events.
    /// </summary>
    internal class EventSet : IEventSet
    {
        /// <summary>
        /// The events in this set.
        /// </summary>
        private List<IEvent> Events { get; } = new List<IEvent>();

        /// <summary>
        /// Creates a set of events.
        /// </summary>
        /// <param name="name">The name of this group of events.</param>
        internal EventSet(string name)
        {
            Name = name?.Trim() ?? string.Empty;
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public IEmptyEvent Event(string name)
        {
            var e = Events.FirstOrDefault(e => e.Name == name);
            if(e == null)
            {
                e = new EmptyEvent(this.Name, name);
                Events.Add(e);
            }
            if(e.PayloadType != typeof(void))
            {
                throw new IncorrectPayloadException(e.Name, typeof(void), e.PayloadType);
            }
            return (IEmptyEvent)e;
        }

        /// <inheritdoc/>
        public IEvent<TPayload> Event<TPayload>(string name)
        {
            var e = Events.FirstOrDefault(e => e.Name == name);
            if (e == null)
            {
                e = new Event<TPayload>(this.Name, name);
                Events.Add(e);
            }
            if (e.PayloadType != typeof(void))
            {
                throw new IncorrectPayloadException(e.Name, typeof(TPayload), e.PayloadType);
            }
            return (IEvent<TPayload>)e;
        }

        /// <inheritdoc/>
        public virtual IEnumerator<IEvent> GetEnumerator()
        {
            return (from e in Events
                    orderby e.Name
                    select e).GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }
}
