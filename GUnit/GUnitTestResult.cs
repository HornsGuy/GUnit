using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUnit
{
    public class GUnitTestResult
    {
        public GUnitTestResult(string name)
        {
            Name = name;
            Passed = true;
        }

        public GUnitTestResult(string name, GUnitException gUnitException)
        {
            Name = name;
            Passed = false;
            Description = gUnitException.Message + "\n" + gUnitException.StackTrace;
        }

        public GUnitTestResult(string name, Exception exception)
        {
            Name = name;
            Passed = false;
            Description = exception.Message + "\n" + exception.StackTrace;
        }

        public string Name { get; private set; }
        public string Description { get; private set; } = "";
        public bool Passed { get; private set; }
        private string TestStatusString
        {
            get
            {
                return Passed ? "Passed" : "Failed";
            }
        }

        public GUnitTestResult(string name, string description, bool passed)
        {
            Name = name;
            Description = description;
            Passed = passed;
        }

        public string ShortDescription()
        {
            return $"Test '{Name}' {TestStatusString}";
        }

        public string LongDescription()
        {
            return ShortDescription() + $"\n{Description}";
        }
    }
}
