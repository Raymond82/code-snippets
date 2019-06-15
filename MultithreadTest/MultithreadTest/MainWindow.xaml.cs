using MultithreadTest.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultithreadTest
{

    /// <summary>
    /// Just a bit of fun with multithreading, while showing of thread (non) safety and critical sections (matrix cells having independent Threads).
    /// 
    /// First three screens (Top left, Top right, Bottom left) are showing "triple buffering" results of random increments 
    /// being done by 9 separate threads, each on a different cell. Because all the threads are working on independent "cells", 
    /// this should be thread safe.
    /// 
    /// Initial matrix is filled using threads which increment numbers every 0-1 second, to simulate data not arriving all at once.
    /// The buffers are there so data is more "stable".
    /// 
    /// Another thread will copy/move the matrix to the rest of the buffers.
    /// 
    /// This shows ways of how data can be "buffered" while the application still is able to handle live data (numbers are used for ease of use).
    /// E.g. Imagine having to do some time consuming work on a "buffer". Since one thread is used for swapping buffers, 
    /// this thread can be suspended or adapted to situations of e.g. "Don't update the third buffer yet".
    /// 
    /// In total 9 + 1 + 9 = 19 thread will be used in this program.
    /// 
    /// Note: the "buffers" are having a bit of an issue updating the window. Takes a bit of time to reflect changes. 
    /// Third buffer should always be more recent than second though.
    /// </summary>
    public partial class MainWindow : Window
    {
        // needed to stop the threads once the window is closing. That way the program can exit cleanly.
        bool isClosing = false;

        // For updating values in the matrix, I am using a random delay between 0 and 1 seconds.
        Random random = new Random();

        /// <summary>
        /// Constructor. 
        /// Setup the Buffers with default (1) data.
        /// Setup the Feed threads
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // TODO: should be in a separate DataContext;
            var datacontext = new MainWindowDataContext();
            DataContext = datacontext;

            datacontext.FirstBuffer.Add(new System.Collections.ObjectModel.ObservableCollection<int>(new List<int> { 1, 1, 1 }));
            datacontext.FirstBuffer.Add(new System.Collections.ObjectModel.ObservableCollection<int>(new List<int> { 1, 1, 1 }));
            datacontext.FirstBuffer.Add(new System.Collections.ObjectModel.ObservableCollection<int>(new List<int> { 1, 1, 1 }));

            datacontext.SecondBuffer.Add(new System.Collections.ObjectModel.ObservableCollection<int>(new List<int> { 1, 1, 1 }));
            datacontext.SecondBuffer.Add(new System.Collections.ObjectModel.ObservableCollection<int>(new List<int> { 1, 1, 1 }));
            datacontext.SecondBuffer.Add(new System.Collections.ObjectModel.ObservableCollection<int>(new List<int> { 1, 1, 1 }));

            datacontext.ThirdBuffer.Add(new System.Collections.ObjectModel.ObservableCollection<int>(new List<int> { 1, 1, 1 }));
            datacontext.ThirdBuffer.Add(new System.Collections.ObjectModel.ObservableCollection<int>(new List<int> { 1, 1, 1 }));
            datacontext.ThirdBuffer.Add(new System.Collections.ObjectModel.ObservableCollection<int>(new List<int> { 1, 1, 1 }));

            // Wait for the Observable Collection and window to be ready. Threads were started whilst the window was still being setup.
            // Let's not be too eager.
            Loaded += (o, s) =>
            {
                SetupFeedThreads(datacontext);

                SetupSwapThread(datacontext);

                // now for some unsafe fun! 9 threads incrementing a single integer (on random intervals).
                // I will increment a safe integer as well to better show off the differences.
                SetupUnsafeThreads(datacontext);
            };
        }

        /// <summary>
        /// Sets up 9 feed threads. One for each cell.
        /// </summary>
        /// <param name="dataContext"></param>
        private void SetupFeedThreads(MainWindowDataContext dataContext)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Thread thread = SetupFeedThread(dataContext, i, j);
                    thread.Start();
                }
            }

        }

        /// <summary>
        /// sets up a single feed thread for feeding a cell on [i][j]
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private Thread SetupFeedThread(MainWindowDataContext dataContext, int i, int j)
        {
            var thr = new Thread(async () =>
            {
                while (!isClosing)
                {
                    var delayTime = random.Next(1000);

                    await Task.Delay(delayTime);
                    try
                    {
                        dataContext.FirstBuffer[i][j]++;
                    }
                    catch (Exception)
                    {
                        // Occasionally the List2DView.cs errors out on e.g. line 89, sees rows as empty while the Thread assumes a row is available.
                        // In this case, just ignore the update.
                    }

                    // Dispatcher.Invoke(() => gridTopLeft.GetBindingExpression(ItemsControl.ItemsSourceProperty).UpdateTarget());
                }
            });

            return thr;
        }

        /// <summary>
        /// Thread for swapping the buffers.
        /// Idea is to always have the latest version of data (as time of swapping) available in the third buffer, 
        /// where the second buffer will have the second to last version of the data.
        /// </summary>
        /// <param name="datacontext"></param>
        private void SetupSwapThread(MainWindowDataContext dataContext)
        {

            Thread swapThread = new Thread(async () =>
            {
                while (!isClosing)
                {
                    await Task.Delay(5000);

                    // So..
                    // FirstBuffer = "live" data.
                    // SecondBuffer = 2nd most recent snapshot
                    // ThirdBuffer =  most recent snapshot.

                    // So, swap second with third and third with first
                    // e.g situation:
                    // 1st = 4, 2nd = 2, 3rd = 3
                    // after first swap:
                    // 1st = 4, 2nd = 3, 3rd = 2
                    // after 2nd swap:
                    // 1st = 2, 2nd = 3, 3rd = 4
                    // Assuming live updates won't stop, 1st should be updated to the most recent version rather quickly.
                    // possible solution is to copy the 3rd after the swap and then replacing 1st. May cause 1st to lose newer data though.

                    // Hmm, refreshing is a bit wonky, which is unfortunate. It seems like the data takes a bit of time to update.

                    var temp = dataContext.SecondBuffer;
                    dataContext.SecondBuffer = dataContext.ThirdBuffer;
                    dataContext.ThirdBuffer = temp;

                    var temp2 = dataContext.ThirdBuffer;
                    dataContext.ThirdBuffer = dataContext.FirstBuffer;
                    dataContext.FirstBuffer = temp2;

                    dataContext.OnPropertyChanged(nameof(dataContext.FirstBuffer));
                    dataContext.OnPropertyChanged(nameof(dataContext.SecondBuffer));
                    dataContext.OnPropertyChanged(nameof(dataContext.ThirdBuffer));
                }
            });

            swapThread.Start();
        }

        /// <summary>
        /// Setup some unsafe threads, showing of importance of critical sections whenever threads use the same memory address/variable.
        /// </summary>
        /// <param name="dataContext"></param>
        private void SetupUnsafeThreads(MainWindowDataContext dataContext)
        {
            for (int i = 0; i < 9; i++)
            {
                Thread thread = new Thread(() =>
                {
                    // NOTE: this will cause unexpected behavior should UnsafeCounter or SafeCounter try to increment past int.MaxValue.
                    // Since the program does not crash, I am fine with this, since the use of this is to show off Thread unsafety/critical sections.
                    while (!isClosing)
                    {
                        dataContext.UnsafeCounter++;
                        dataContext.IncrementSafeCounter();
                    }
                });

                thread.Start();
            }
        }

        /// <summary>
        /// Make sure to stop the running threads by making sure they leave the while loops they are in.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            isClosing = true;
            base.OnClosing(e);
        }
    }
}