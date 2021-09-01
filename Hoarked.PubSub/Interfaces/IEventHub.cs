using System;

namespace Hoarked.PubSub
{
    /// <summary>
    /// This is the entry point to a location that can be used to denote a repository of global events that can be subscribed to.
    /// </summary>
    /// <remarks>
    /// The event hub has a group of global events and has access to specific sets for each of it's groups.
    /// The event hub has a group of global events and has access to specific sets for each of it's groups.
    /// </remarks>
    public interface IEventHub : IEventSet
    {
        /// <summary>
        /// Returns the event set for a particular group.  A group is a group of events that are conceptually related.
        /// </summary>
        /// <param name="group">The categorical group of events this is part of.</param>
        /// <returns>The set of events that pertain to this group.</returns>
        IEventSet Group(string group);
    }
}
