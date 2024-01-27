using System.Reflection;

namespace GUnit
{
    public class GUnitTestRunner<T> where T : new()
    {
        public GUnitTestRunResults RunTests()
        {
            MethodInfo[] methods = typeof(T).GetMethods();

            List<MethodInfo> tests = new List<MethodInfo>();

            MethodInfo? setupMethod = null;
            MethodInfo? tearDownMethod = null;

            foreach (MethodInfo method in methods)
            {
                foreach (var attribute in method.CustomAttributes)
                {
                    if(attribute.AttributeType == typeof(GUnitTest))
                    {
                        tests.Add(method);
                    }
                    else if (attribute.AttributeType == typeof(GUnitSetup))
                    {
                        if(setupMethod != null)
                        {
                            throw new Exception("Multiple Setup methods defined. Only a single Setup method can be defined per test class");
                        }

                        setupMethod = method;
                    }
                    else if (attribute.AttributeType == typeof(GUnitTeardown))
                    {
                        if (tearDownMethod != null)
                        {
                            throw new Exception("Multiple TearDown methods defined. Only a single TearDown method can be defined per test class");
                        }
                        tearDownMethod = method;
                    }
                }
            }

            return ExecuteTests(typeof(T).ToString(), tests, setupMethod, tearDownMethod);
        }

        private GUnitTestRunResults ExecuteTests(string className, List<MethodInfo> tests, MethodInfo? setupMethod, MethodInfo? teardownMethod)
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
                catch (GUnitException ex) 
                {
                    results.Add(new GUnitTestResult(testName, ex));
                }
                catch(Exception ex) 
                {
                    results.Add(new GUnitTestResult(testName, ex));
                }

                teardownMethod?.Invoke(obj, null);
            }

            return new GUnitTestRunResults(results);
        }
        
    }
}
