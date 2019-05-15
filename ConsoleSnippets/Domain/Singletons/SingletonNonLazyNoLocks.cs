using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Singletons
{
    /// <summary>
    /// Thread-safe singleton (without locks). This implemention does not have a critical section since the <see cref="Instance"/> is already initialized.
    /// </summary>
    public class SingletonNonLazyNoLocks
    {
        /// <summary>
        /// Make sure the .ctor is not callable (outside of reflection)
        /// </summary>
        private SingletonNonLazyNoLocks()
        {
        }

        /// <summary>
        /// Auto-property for returning the instance of this <see cref="SingletonNonLazyNoLocks"/>.
        /// </summary>
        public static SingletonNonLazyNoLocks Instance { get; } = new SingletonNonLazyNoLocks();

        public override string ToString()
        {
            return "Singleton: ThreadSafe - NonLazy - NoLocks";
        }
    }
}
