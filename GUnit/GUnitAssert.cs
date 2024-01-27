using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUnit
{
    public class GUnitAssert
    {

        public static void AreEqual(object? actual, object? expected)
        {
            if(actual == null)
            {
                throw new GUnitException($"Actual was null. Unable to perform equality check");
            }

            if(actual.GetType() != expected?.GetType())
            {
                throw new GUnitException($"Actual and Expected have different types");
            }

            if (!actual.Equals(expected))
            {
                throw new GUnitException($"Actual '{actual}' not equal to Expected '{expected}'");
            }
        }
        
        public static void AreNotEqual(object? actual, object? expected)
        {
            if(actual == null)
            {
                throw new GUnitException($"Actual was null. Unable to perform equality check");
            }

            // Type mismatch, so we know they aren't equal
            // Exit early
            if (actual.GetType() != expected?.GetType())
            {
                return;
            }

            if (actual.Equals(expected))
            {
                throw new GUnitException($"Actual '{actual}' equal to Expected '{expected}'");
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

        public static void IsNull(object? value)
        {
            if (value != null)
            {
                throw new GUnitException($"Value was not null");
            }
        }

        public static void IsNotNull(object? value)
        {
            if (value == null)
            {
                throw new GUnitException($"Value was null");
            }
        }
    }
}
