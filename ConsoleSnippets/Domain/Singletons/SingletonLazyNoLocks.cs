using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Singletons
{
    public class SingletonLazyNoLocks
    {
        private SingletonLazyNoLocks()
        {
        }

        private static readonly Lazy<SingletonLazyNoLocks> _lazyInstance = new Lazy<SingletonLazyNoLocks>(() => new SingletonLazyNoLocks());

        public static SingletonLazyNoLocks Instance { get { return _lazyInstance.Value; } }

        public override string ToString()
        {
            return "Singleton: ThreadSafe - Lazy - NoLocks";
        }
    }
}
