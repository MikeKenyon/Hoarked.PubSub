using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub
{
    public interface IEvent<TPayload> : IEvent
    {
        /// <summary>
        /// Publishs an event, waiting for all handlers to be processed.
        /// </summary>
        /// <param name="payload">The payload for this event.</param>
        void Publish(TPayload payload);
        /// <summary>
        /// Publishes an event asyncrhonously.
        /// </summary>
        /// <param name="payload">The payload for this event.</param>
        /// <returns>Asynchronous handle.</returns>
        Task PublishAsync(TPayload payload);
        /// <summary>
        /// Registers a subscription for this handler.
        /// </summary>
        /// <param name="handler">THe handler to register.</param>
        void Subscribe(Func<TPayload, Task> handler);
        /// <summary>
        /// Registers a subcription for this handler.
        /// </summary>
        /// <param name="handler">The handler to register.</param>
        void Subscribe(Action<TPayload> handler);
        /// <summary>
        /// Unsubscribes so that this event won't notify this handler anymore.
        /// </summary>
        /// <param name="handler">The handler to unregister.</param>
        void Unsubscribe(Func<TPayload,Task> handler);
        /// <summary>
        /// Unsubscribes so that this event won't notify thi shandler anymore.
        /// </summary>
        /// <param name="handler">The handler to unregister.</param>
        void Unsubscribe(Action<TPayload> handler);
    }
}
