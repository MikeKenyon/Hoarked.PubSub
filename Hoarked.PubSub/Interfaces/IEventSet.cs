using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub
{
    /// <summary>
    /// An event set is a group of events.  The group is either a named group or an anonymous group of top level events.
    /// </summary>
    public interface IEventSet : IEnumerable<IEvent>
    {
        /// <summary>
        /// The name of the group, or <see cref="string.Empty"/> for the unnamed group.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the event in this group that has this the name that has no payload.
        /// </summary>
        /// <param name="name">The naem of the event.</param>
        /// <returns>The event that has that name.</returns>
        /// <exception cref="IncorrectPayloadException">The payload has previously been defined as having a payload.</exception>
        IEmptyEvent Event(string name);

        /// <summary>
        /// Returns the event in this group that has this the name that has no payload.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="name">The naem of the event.</param>so
        /// <returns>The event that has that name.</returns>
        /// <exception cref="IncorrectPayloadException">The payload has previously been defined as having a differnet payload.</exception>
        IEvent<TPayload> Event<TPayload>(string name);
    }
}
