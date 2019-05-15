using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Singletons
{
    /// <summary>
    /// Thread-safe singleton (without locks), using Lazy instancing.
    /// </summary>
    public class SingletonLazyNoLocks
    {
        /// <summary>
        /// Make sure the .ctor is not callable (outside of reflection)
        /// </summary>
        private SingletonLazyNoLocks()
        {
        }

        // Lazy backing field of this singleton instance. Can be handy in case the singleton is expensive to be created and may not be necessary.
        // E.g. imagine a singleton instance which has to setup (database) connections.
        private static readonly Lazy<SingletonLazyNoLocks> _lazyInstance = new Lazy<SingletonLazyNoLocks>(() => new SingletonLazyNoLocks());

        // verify that value indeed is not created initially.
        private static bool _isCreatedInitially = _lazyInstance.IsValueCreated;

        public static SingletonLazyNoLocks Instance { get { return _lazyInstance.Value; } }

        public override string ToString()
        {
            return "Singleton: ThreadSafe - Lazy - NoLocks => Is value initially created? " + _isCreatedInitially + ", Is value created when called? " + _lazyInstance.IsValueCreated;
        }
    }
}
