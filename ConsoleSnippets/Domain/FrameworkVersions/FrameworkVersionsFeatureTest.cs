using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Domain.FrameworkVersions
{
    public class FrameworkVersionsFeatureTest
    {
        private FrameworkVersionsFeatureTest()
        {
        }

        public static FrameworkVersionsFeatureTest Instance = new FrameworkVersionsFeatureTest();


        // For me, I started out in .Net 2.0. There used to be a lot of data classes using the below pattern. 
        // I decided to place Backing and auto-properties next to each other to properly show the difference.
        private string _propertyWithBackingField = "Property with backing field";

        public string PropertyWithBackingField
        {
            get
            {
                return _propertyWithBackingField;
            }
        }

        public string AutoPropertyInitialized { get; } = "Auto property, readily initialized";

        public string LambdaTest()
        {
            int[] numbers = { 4, 25, 9, 16 };

            var rootedNumbers = numbers.Select(x => Math.Sqrt(x));

            return "Lambda test for numbers: " + string.Join(" ", numbers) + " -> rooted versions: " + string.Join(" ", rootedNumbers);
        }

        public string LinqTest()
        {
            var result = new StringBuilder("Linq test for numbers: ");

            int[] numbers = { 4, 25, 9, 16 };

            result.AppendLine(string.Join(" ", numbers));


            var linqExpressionResult =
                from number in numbers
                where number > 4
                orderby number ascending
                select number;

            result.AppendLine("\tSorting and showing numbers larger than four: " + string.Join(" ", linqExpressionResult));

            var linqResultAlternate = numbers.Where(n => n > 4).OrderBy(n => n); // OrderByDescending if needed.

            result.AppendLine("\tSorting and showing numbers larger than four (alternate): " + string.Join(" ", linqResultAlternate));

            return result.ToString();
        }


        public string InitializerTest()
        {
            var retval = "InitializerTest:" + Environment.NewLine;
            var indexTest = new IndexTest() { Name = "IndexTest", [0] = 33 };

            retval += indexTest.ToString() + Environment.NewLine + "Add some more numbers.";

            indexTest[9] = -123;
            indexTest[11] = 23;

            retval += indexTest.ToString() + Environment.NewLine;

            return retval;
        }
    }
}
