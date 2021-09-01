using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub
{
    /// <summary>
    /// Defines an event that has no accompanying payload.
    /// </summary>
    public interface IEmptyEvent : IEvent
    {
        /// <summary>
        /// Publishs an event, waiting for all handlers to be processed.
        /// </summary>
        void Publish();
        /// <summary>
        /// Publishes an event asyncrhonously.
        /// </summary>
        /// <returns>Asynchronous handle.</returns>
        Task PublishAsync();
        /// <summary>
        /// Registers a subscription for this handler.
        /// </summary>
        /// <param name="handler">THe handler to register.</param>
        void Subscribe(Func<Task> handler);
        /// <summary>
        /// Registers a subcription for this handler.
        /// </summary>
        /// <param name="handler">The handler to register.</param>
        void Subscribe(Action handler);
        /// <summary>
        /// Unsubscribes so that this event won't notify this handler anymore.
        /// </summary>
        /// <param name="handler">The handler to unregister.</param>
        void Unsubscribe(Func<Task> handler);
        /// <summary>
        /// Unsubscribes so that this event won't notify thi shandler anymore.
        /// </summary>
        /// <param name="handler">The handler to unregister.</param>
        void Unsubscribe(Action handler);
    }
}
