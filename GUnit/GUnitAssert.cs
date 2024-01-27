using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUnit
{
    public class GUnitAssert
    {
        public static void Equal(object? actual, object? expected)
        {
            if(actual != expected)
            {
                throw new GUnitException($"Actual '{actual}' not equal to Expected '{expected}'");
            }
        }

        public static void IsTrue(bool value)
        {
            if(!value)
            {
                throw new GUnitException($"Expected true, actual was false");
            }
        }

        public static void IsFalse(bool value)
        {
            if (value)
            {
                throw new GUnitException($"Expected false, actual was true");
            }
        }

    }
}
