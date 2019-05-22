using System;

namespace FizzBuzz_v3.Domain
{
    public class FizzBuzzFactory
    {
        private FizzBuzzFactory()
        {

        }
        public static FizzBuzzFactory Instance { get; } = new FizzBuzzFactory();

        public string DetermineFuzziness(int param)
        {
            if (param % 5 == 0 && param % 3 == 0)
            {
                return "FizzBuzz";
            }
            else if (param % 3 == 0)
            {
                return "Fizz";
            }
            else if (param % 5 == 0)
            {
                return "Buzz";
            }
            else
            {
                return param.ToString();
            }
        }
    }
}
