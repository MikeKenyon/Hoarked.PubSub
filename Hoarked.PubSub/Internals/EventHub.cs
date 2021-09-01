using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub.Internals
{
    /// <summary>
    /// The overall collection of all system wide events in the application.
    /// </summary>
    internal class EventHub : EventSet, IEventHub
    {
        /// <summary>
        /// Creates a new event hub.
        /// </summary>
        public EventHub() : base(string.Empty)
        {
        }

        private List<IEventSet> Groups { get; } = new List<IEventSet>();

        /// <inheritdoc/>
        public IEventSet Group(string group)
        {
            var existing = (from g in Groups
                            where g.Name == @group
                            select g).FirstOrDefault();
            if(existing == null)
            {
                existing = new EventSet(group);
                Groups.Add(existing);
            }
            return existing;
        }

        /// <inheritdoc/>
        public override IEnumerator<IEvent> GetEnumerator()
        {
            var baseEnum = base.GetEnumerator();
            while(baseEnum.MoveNext())
            {
                yield return baseEnum.Current;
            }
            var sets = from g in Groups
                       orderby g.Name
                       select g;


            foreach(var set in sets)
            {
                foreach(var entry in set)
                {
                    yield return entry;
                }
            }
        }
    }
}
