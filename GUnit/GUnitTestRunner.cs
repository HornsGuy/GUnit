using NUnit.Framework;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace GUnit
{
    public class GUnitTestRunner 
    {
        public static GUnitResults RunTests<T>() where T : new()
        {
            MethodInfo[] methods = typeof(T).GetMethods();

            List<MethodInfo> tests = new List<MethodInfo>();

            MethodInfo? setupMethod = null;
            MethodInfo? tearDownMethod = null;

            bool filterActive = false;
            foreach (MethodInfo method in methods)
            {
                bool addTest = false;
                bool ignoreTest = false;
                foreach (var attribute in method.CustomAttributes)
                {   
                    if(!filterActive && attribute.AttributeType == typeof(GUnitTest))
                    {
                        addTest = true;
                    }
                    else if (attribute.AttributeType == typeof(GUnitSetUp))
                    {
                        if(setupMethod != null)
                        {
                            throw new Exception("Multiple GUnitSetup methods defined. Only a single GUnitSetup method can be defined per test class");
                        }

                        setupMethod = method;
                    }
                    else if (attribute.AttributeType == typeof(GUnitTearDown))
                    {
                        if (tearDownMethod != null)
                        {
                            throw new Exception("Multiple GUnitTeardown methods defined. Only a single GUnitTeardown method can be defined per test class");
                        }
                        tearDownMethod = method;
                    }
                    else if(attribute.AttributeType == typeof(GUnitFilter))
                    {
                        tests.Clear();
                        tests.Add(method);
                        filterActive = true;
                    }
                    else if(attribute.AttributeType == typeof (GUnitIgnore))
                    {
                        ignoreTest = true;
                    }
                }

                // Remove test if being ignored
                if (!ignoreTest && addTest)
                {
                    tests.Add(method);
                }
            }

            return ExecuteTests<T>(typeof(T).ToString(), tests, setupMethod, tearDownMethod);
        }

        private static GUnitResults ExecuteTests<T>(string className, List<MethodInfo> tests, MethodInfo? setupMethod, MethodInfo? teardownMethod) where T : new()
        {
            List<GUnitTestResult> results = new List<GUnitTestResult>();

            T obj = new T();

            foreach (MethodInfo test in tests)
            {
                string testName = $"{className}.{test.Name}";

                setupMethod?.Invoke(obj, null);
                
                try
                {

                    test.Invoke(obj, null);
                    results.Add(new GUnitTestResult(testName));
                }
                catch(TargetInvocationException baseEx) 
                {
                    // Reflection causes exceptions throwin from within an invoked method to wrap the exception
                    // Use the below object to unwrap the exception and throw the actual exception we received
                    try
                    {
                        if(baseEx.InnerException != null)
                        {
                            ExceptionDispatchInfo.Capture(baseEx.InnerException).Throw();
                        }
                        else
                        {
                            ExceptionDispatchInfo.Capture(baseEx).Throw();
                        }
                    }
                    catch (SuccessException)
                    {
                        results.Add(new GUnitTestResult(testName));
                    }
                    catch (AssertionException ex)
                    {
                        results.Add(new GUnitTestResult(testName, ex));
                    }
                    catch (Exception ex)
                    {
                        results.Add(new GUnitTestResult(testName, ex));
                    }
                }

                teardownMethod?.Invoke(obj, null);
            }

            return new GUnitResults(className,results);
        }
        
    }
}
