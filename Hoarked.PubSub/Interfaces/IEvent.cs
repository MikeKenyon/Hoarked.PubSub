using System;
using System.Threading.Tasks;

namespace Hoarked.PubSub
{
    /// <summary>
    /// Represents a event that you can subsribe or publish to.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Gets the name this event was registered under.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets a formatted name structured as "SetName.EventName".
        /// </summary>
        string FullName { get; }
        /// <summary>
        /// Gets the type of the payload associated with this event. 
        /// </summary>
        /// <remarks>If there is no payload, this will be <see cref="Void"/>.</remarks>
        Type PayloadType { get; }
    }

}