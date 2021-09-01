using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub
{
    /// <summary>
    /// An error wherein the payload type the event was first registered under is not the payload type later used.
    /// </summary>
    public class IncorrectPayloadException : Exception
    {
        /// <summary>
        /// Creates such an exception.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="expected">The expected type of the event.</param>
        /// <param name="retreived">The type that was actually found for the event.</param>
        internal IncorrectPayloadException(string name, Type expected, Type retreived) : base(ToMessage(name, expected, retreived)) { }

        /// <summary>
        /// Calculates the error message.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="expected">The expected type of the event.</param>
        /// <param name="retreived">The type that was actually found for the event.</param>
        /// <returns>A displayable error.</returns>
        private static string ToMessage(string name, Type expected, Type retreived)
        {
            return $"Event '{name}' was found to have a type of {retreived.FullName}, while earlier we were told to expect {expected.FullName}.";
        }
    }
}
