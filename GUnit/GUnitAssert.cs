using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GUnit
{
    public static class GUnitAssert
    {
        
        private static bool IsCastableTo(this Type from, Type to, bool implicitly = false)
        {
            return to.IsAssignableFrom(from) || HasCastDefined(from,to, implicitly);
        }

        static bool HasCastDefined(Type from, Type to, bool implicitly)
        {
            if ((from.IsPrimitive || from.IsEnum) && (to.IsPrimitive || to.IsEnum))
            {
                if (!implicitly)
                    return from==to || (from!=typeof(Boolean) && to!=typeof(Boolean));

                Type[][] typeHierarchy = {
                    new Type[] { typeof(Byte),  typeof(SByte), typeof(Char) },
                    new Type[] { typeof(Int16), typeof(UInt16) },
                    new Type[] { typeof(Int32), typeof(UInt32) },
                    new Type[] { typeof(Int64), typeof(UInt64) },
                    new Type[] { typeof(Single) },
                    new Type[] { typeof(Double) }
                };
                IEnumerable<Type> lowerTypes = Enumerable.Empty<Type>();
                foreach (Type[] types in typeHierarchy)
                {
                    if ( types.Any(t => t == to) )
                        return lowerTypes.Any(t => t == from);
                    lowerTypes = lowerTypes.Concat(types);
                }

                return false;   // IntPtr, UIntPtr, Enum, Boolean
            }
            return IsCastDefined(to, m => m.GetParameters()[0].ParameterType, _ => from, implicitly, false)
                || IsCastDefined(from, _ => to, m => m.ReturnType, implicitly, true);
        }

        static bool IsCastDefined(Type type, Func<MethodInfo, Type> baseType,
                                Func<MethodInfo, Type> derivedType, bool implicitly, bool lookInBase)
        {
            var bindinFlags = BindingFlags.Public | BindingFlags.Static
                            | (lookInBase ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);
            return type.GetMethods(bindinFlags).Any(
                m => (m.Name=="op_Implicit" || (!implicitly && m.Name=="op_Explicit"))
                    && baseType(m).IsAssignableFrom(derivedType(m)));
        }

        private static Tuple<object?,object?> BaseEqual(object? actual, object? expected)
        {
            if (actual == null && expected != null)
            {
                throw new GUnitException("Actual was null and expected was not");
            }

            if (actual != null && expected == null)
            {
                throw new GUnitException("Expected was null and actual was not");
            }

            // Both are not null at this point, so perform type checks
            object? castedExpectedVal = expected;
            object? castedActualVal = actual;
            Type actualType = actual.GetType();
            Type expectedType = expected.GetType();
            if (actual.GetType() != expected.GetType())
            {
                // Type did not match, so check if an object can be casted to perform equals comparison
                if(IsCastableTo(expectedType,actualType))
                {
                    castedExpectedVal = Convert.ChangeType(expected, actualType);
                }
                else if(IsCastableTo(actualType,expectedType))
                {
                    castedActualVal = Convert.ChangeType(actual, expectedType);
                }
                else
                {
                    throw new GUnitException($"Types '{actualType}' and '{expectedType}' are not comparable");
                }
            }

            return new Tuple<object?, object?>(castedActualVal, castedExpectedVal);
        }

        public static void AreEqual(object? actual, object? expected)
        {
            // Two are both null, return
            if(actual == null && expected == null)
            {
                return;
            }

            Tuple<object?,object?> tuple = BaseEqual(actual, expected);
            object? castedActualVal = tuple.Item1;
            object? castedExpectedVal = tuple.Item2;

            if (!castedActualVal.Equals(castedExpectedVal))
            {
                throw new GUnitException($"Actual value {castedActualVal} and expected value {castedExpectedVal} are not equal");
            }
        }
        
        public static void AreNotEqual(object? actual, object? expected)
        {
            // Two are both null, return
            if (actual == null && expected == null)
            {
                return;
            }

            Tuple<object?, object?> tuple = BaseEqual(actual, expected);
            object? castedActualVal = tuple.Item1;
            object? castedExpectedVal = tuple.Item2;

            if (castedActualVal.Equals(castedExpectedVal))
            {
                throw new GUnitException($"Actual value {castedActualVal} and expected value {castedExpectedVal} are not equal");
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
