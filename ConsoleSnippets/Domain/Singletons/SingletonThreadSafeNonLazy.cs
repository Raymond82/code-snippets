using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Singletons
{
    /// <summary>
    /// Thread Safe Singleton (with locks). Initial idea here. This works but having code entering the critical section is not ideal. <see cref="SingletonThreadSafeNonLazy"/> for a better implementation.
    /// </summary>
    public class SingletonThreadSafeNonLazy
    {
        private static object _syncRoot = new object();
        private static SingletonThreadSafeNonLazy _instance;

        /// <summary>
        /// Make sure the .ctor is not callable (outside of reflection)
        /// </summary>
        private SingletonThreadSafeNonLazy()
        {
        }

        /// <summary>
        /// returns the instance of this <see cref="SingletonThreadSafeNonLazy"/>
        /// </summary>
        public static SingletonThreadSafeNonLazy Instance
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonThreadSafeNonLazy();
                    }

                    return _instance;
                }
            }
        }

        public override string ToString()
        {
            return "Singleton: ThreadSafe - NonLazy - Locks";
        }
    }
}
