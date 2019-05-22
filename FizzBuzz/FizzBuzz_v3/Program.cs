using FizzBuzz_v3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz_v3
{
    class Program
    {
        static void Main(string[] args)
        {
            DoFizzBuzz();

            Console.WriteLine("- V3 -");
            Console.ReadKey(true);
        }

        private static void DoFizzBuzz()
        {
            for (int i = 1; i <= 100; i++)
            {
                // DetermineLine is a bit poor when deciding to move this to a Domain, I will change the name once I get there. 
                var line = FizzBuzzFactory.Instance.DetermineFuzziness(i);
                Console.WriteLine(line);
            }
        }
    }
}
