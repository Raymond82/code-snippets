using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnippets
{
    /// <summary>
    /// The intent of this program is to show off different ways for implementing code/coding skills.
    /// E.g. Different ways to do things, or to show differences between .Net versions, or, as stated in the repository, code snippets.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            WriteWelcomeMessage();
            var shouldExit = false;

            while(!shouldExit)
            {
                WriteMenu();
                shouldExit = ReadMenuOption();
            }
        }

        private static bool ReadMenuOption()
        {
            var key = Console.ReadKey(true);
            bool shouldExit;

            switch(key.KeyChar)
            {
                case '1':
                    SingletonTest();
                    shouldExit = false;
                    break;
                case 'x':
                    shouldExit = true;
                    break;
                default:
                    shouldExit = false;
                    break;
            }

            return shouldExit;
        }

        private static void SingletonTest()
        {
            Console.WriteLine(new string('-', Console.BufferWidth));
            Console.WriteLine(Domain.Singletons.SingletonThreadSafeNonLazy.Instance);
            Console.WriteLine(Domain.Singletons.SingletonNonLazyNoLocks.Instance);
            Console.WriteLine(Domain.Singletons.SingletonLazyNoLocks.Instance);
            Console.WriteLine(new string('-', Console.BufferWidth));
        }

        private static void WriteMenu()
        {
            Console.WriteLine("Pick one of the options below:");
            Console.WriteLine("1 - singletons.");
            Console.WriteLine("x - exit.");
        }

        private static void WriteWelcomeMessage()
        {
            Console.WriteLine("Welcome to my snippet-program. The idea is to show off some programming snippets.");
        }
    }
}
