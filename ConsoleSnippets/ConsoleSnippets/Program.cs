using Domain.FrameworkVersions;
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
                ReadMenuOption(out shouldExit);
            }
        }


        /// <summary>
        /// Reads the menu options and returns true if exit is pressed.
        /// </summary>
        /// <returns></returns>
        private static void ReadMenuOption(out bool shouldExit)
        {
            var key = Console.ReadKey(true);
            shouldExit = false;

            switch(key.KeyChar)
            {
                case '1':
                    SingletonTest();
                    break;
                case '2':
                    NetFeaturesTest();
                    break;
                case 'x':
                    // Environment.Exit(0) can be used as well here, but I wanted the program to exit "more gracefully" by not using the Environment.Exit(0) call at all;
                    shouldExit = true;
                    break;
                default:
                    break;
            }
        }

        private static void NetFeaturesTest()
        {
            Console.WriteLine(new string('-', Console.BufferWidth));

            Console.WriteLine(FrameworkVersionsFeatureTest.Instance.AutoPropertyInitialized);
            Console.WriteLine(FrameworkVersionsFeatureTest.Instance.PropertyWithBackingField);

            Console.WriteLine(FrameworkVersionsFeatureTest.Instance.LambdaTest());
            Console.WriteLine(FrameworkVersionsFeatureTest.Instance.LinqTest());
            Console.WriteLine(FrameworkVersionsFeatureTest.Instance.InitializerTest());

            Console.WriteLine(new string('-', Console.BufferWidth));
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
            Console.WriteLine("1 - Singletons.");
            Console.WriteLine("2 - Net Versions Features.");
            Console.WriteLine("x - exit.");
        }

        private static void WriteWelcomeMessage()
        {
            Console.WriteLine("Welcome to my snippet-program. The idea is to show off some programming snippets (and the way I program).");
        }
    }
}
