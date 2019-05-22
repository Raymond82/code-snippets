using System;
using FizzBuzz_v3.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzz_v3.UnitTest
{
    [TestClass]
    public class UnitTestFizzBuzz
    {
        [TestMethod]
        public void TestFizzBuzzValidValues()
        {
            var fizzBuzzFactory = FizzBuzzFactory.Instance;
            Assert.AreEqual("1", fizzBuzzFactory.DetermineFuzziness(1));
            Assert.AreEqual("2", fizzBuzzFactory.DetermineFuzziness(2));
            Assert.AreEqual("Fizz", fizzBuzzFactory.DetermineFuzziness(3));
            Assert.AreEqual("4", fizzBuzzFactory.DetermineFuzziness(4));
            Assert.AreEqual("Buzz", fizzBuzzFactory.DetermineFuzziness(5));

            // 6 is a multiple of 3, which of course should output Fizz.
            // uncommenting this line will cause unit tests to fail
            // Assert.AreEqual("6", fizzBuzzFactory.DetermineFuzziness(6));

            Assert.AreEqual("Fizz", fizzBuzzFactory.DetermineFuzziness(6));

            // skip uniteresting numbers, can add a test case for this if any bugs occur.
            Assert.AreEqual("14", fizzBuzzFactory.DetermineFuzziness(14));
            Assert.AreEqual("FizzBuzz", fizzBuzzFactory.DetermineFuzziness(15));

            // latest 'fizzbuzz' possible when <= 100
            Assert.AreEqual("FizzBuzz", fizzBuzzFactory.DetermineFuzziness(90));

            // latest 'numeric value' possible when <= 100
            Assert.AreEqual("98", fizzBuzzFactory.DetermineFuzziness(98));

            // latest 'fizz' possible
            Assert.AreEqual("Fizz", fizzBuzzFactory.DetermineFuzziness(99));

            // latest 'buzz' possible when <= 100
            Assert.AreEqual("Buzz", fizzBuzzFactory.DetermineFuzziness(100));
        }

        [TestMethod]
        [Description("Make sure the FizzBuzzFactory can handle unexpected values (gracefully) as well.")]
        public void TestFizzBuzzUnexpectedValues()
        {
            var fizzBuzzFactory = FizzBuzzFactory.Instance;
            Assert.AreEqual("FizzBuzz", fizzBuzzFactory.DetermineFuzziness(0));
            Assert.AreEqual("101", fizzBuzzFactory.DetermineFuzziness(101));

            Assert.AreEqual("Fizz", fizzBuzzFactory.DetermineFuzziness(999));
            Assert.AreEqual("Buzz", fizzBuzzFactory.DetermineFuzziness(-10000));


            Assert.AreEqual(int.MaxValue.ToString(), fizzBuzzFactory.DetermineFuzziness(int.MaxValue));
            Assert.AreEqual(int.MinValue.ToString(), fizzBuzzFactory.DetermineFuzziness(int.MinValue));
        }
    }
}
