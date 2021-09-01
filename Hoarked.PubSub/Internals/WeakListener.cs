using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub.Internals
{
    internal class WeakListener : IEquatable<WeakListener>
    {
        private MethodInfo TargetMethod { get; }        
        private WeakReference TargetObject { get; }
        private bool IsStatic { get; }
        /// <summary>
        /// Whether this listener is invalid and should be removed.
        /// </summary>
        internal bool Invalid { get; private set; }
        internal WeakListener(Delegate d)
        {
            if(!Valid(d))
            {
                throw new InvalidDelegateException(d.Method);
            }

            IsStatic = d.Method.IsStatic;
            TargetObject = !IsStatic ? new WeakReference(d.Target) : null;
            TargetMethod = d.Method;
        }

        /// <summary>
        /// Whether this is a valid method to use or not.
        /// </summary>
        /// <param name="method">The method to test.</param>
        /// <returns>Whether it's valid.</returns>
        internal static bool Valid(Delegate d)
        {
            var method = d.GetMethodInfo();
            var numParams = method.GetParameters().Length;
            return (numParams == 0 || numParams == 1) && (
                method.ReturnType == typeof(Task) ||
                method.ReturnType == typeof(ValueTask) ||
                method.ReturnType == typeof(void));
        }

        /// <inheritdoc/>
        public bool Equals(WeakListener other)
        {
            return (other is not null
                && (IsStatic && other.IsStatic) || object.ReferenceEquals(TargetObject?.Target, other.TargetObject?.Target)) 
                && TargetMethod.Equals(other.TargetMethod);
        }

        /// <summary>
        /// Invokes this listener.
        /// </summary>
        /// <param name="payload">The payload to pass (if any).</param>
        /// <returns>Asynchronous handle.</returns>
        internal async Task PerformAsync(object payload)
        {
            if(Invalid)
            {
                return;
            }
            if (IsStatic)
            {
                await CallMethod(null, payload);
            }
            else
            {
                var bad = true;
                if (TargetObject != null && TargetObject.IsAlive)
                {
                    var target = TargetObject.Target;
                    if (target != null)
                    {
                        bad = false;
                        await CallMethod(target, payload);
                    }
                }
                if (bad)
                {
                    Invalid = true;
                }
            }
        }

        /// <summary>
        /// Does the work of calling the method and (maybe) waiting for it's response.
        /// </summary>
        /// <param name="target">The target object to call this on.</param>
        /// <param name="payload">The payload to pass to it (if any).</param>
        /// <returns>Asynchronous handle.</returns>
        private async Task CallMethod(object target, object payload)
        {
            var response = TargetMethod.Invoke(target, payload != null ? new[] { payload } : null);
            if (response is Task t)
            {
                await t;
            }
            else if (response is ValueTask vt)
            {
                await vt;
            }
            else if (response != null)
            {
                throw new InvalidDelegateException(TargetMethod);
            }
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as WeakListener);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return TargetMethod.GetHashCode() * (TargetObject?.GetHashCode() ?? 57);
        }
    }
}
