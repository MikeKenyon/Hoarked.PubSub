using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub
{
    /// <summary>
    /// Legal delegates have [0..1] parameters and return void, <see cref="Task"/> or <see cref="ValueTask"/>
    /// </summary>
    public class InvalidDelegateException : Exception
    {
        /// <summary>
        /// Creates an exception from a bad method.
        /// </summary>
        /// <param name="method">The bad method.</param>
        internal InvalidDelegateException(MethodInfo method)
        {
            Method = method;
        }

        /// <summary>
        /// The invalid method to invoke.
        /// </summary>
        public MethodInfo Method { get; }
    }
}
