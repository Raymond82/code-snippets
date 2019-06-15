using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadTest.DataContext
{
    class MainWindowDataContext : INotifyPropertyChanged
    {
        // The "first" buffer where live data is being inserted into
        public ObservableCollection<ObservableCollection<int>> FirstBuffer { get; set; } = new ObservableCollection<ObservableCollection<int>>();

        // Double buffer (the way this is set is a bit abusive but generally one wants to refresh the entire buffer anyways)
        public ObservableCollection<ObservableCollection<int>> SecondBuffer { get; set; } = new ObservableCollection<ObservableCollection<int>>();

        // Triple buffer (the way this is set is a bit abusive but generally one wants to refresh the entire buffer anyways)
        public ObservableCollection<ObservableCollection<int>> ThirdBuffer { get; set; } = new ObservableCollection<ObservableCollection<int>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        int _unsafeCounter = 0;

        /// <summary>
        /// Thread unsafe counter, can be changed directly from the MainWindow
        /// </summary>
        public int UnsafeCounter
        {
            get { return _unsafeCounter; }
            set
            {
                _unsafeCounter = value;
                OnPropertyChanged(nameof(UnsafeCounter));
            }
        }

        int _safeCounter = 0; // int.MaxValue; // test behavior of safe counter on max value (does it overflow or crash? Overflow.)

        /// <summary>
        /// Thread safe counter 
        /// </summary>
        public int SafeCounter
        {
            get { return _safeCounter; }
        }

        /// <summary>
        /// Difference between safe and unsafe to show how many increments failed to do.
        /// </summary>
        public int Difference
        {
            get
            {
                return SafeCounter - UnsafeCounter;
            }
        }


        /// <summary>
        /// Callable from the threads in order to safely increase the Thread safe counter.
        /// </summary>
        internal void IncrementSafeCounter()
        {
            Interlocked.Increment(ref _safeCounter);

            OnPropertyChanged(nameof(SafeCounter));
            OnPropertyChanged(nameof(Difference));

        }
    }
}
