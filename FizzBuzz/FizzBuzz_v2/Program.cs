using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            DoFizzBuzz();

            Console.WriteLine("- V2 -");

            Console.ReadKey(true);
        }

        private static void DoFizzBuzz()
        {
            for (int i = 1; i <= 100; i++)
            {
                // DetermineLine is a bit poor when deciding to move this to a Domain, I will change the name once I get there. 
                var line = DetermineLine(i);
                Console.WriteLine(line);
            }
        }

        private static string DetermineLine(int i)
        {
            if (i % 5 == 0 && i % 3 == 0)
            {
                return "FizzBuzz";
            }
            else if (i % 3 == 0)
            {
                return "Fizz";
            }
            else if (i % 5 == 0)
            {
                return "Buzz";
            }
            else
            {
                return i.ToString();
            }
        }
    }
}
