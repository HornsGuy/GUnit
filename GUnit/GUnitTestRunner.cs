using System.Reflection;

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

            foreach (MethodInfo method in methods)
            {
                foreach (var attribute in method.CustomAttributes)
                {
                    if(attribute.AttributeType == typeof(GUnitTest))
                    {
                        tests.Add(method);
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

            return new GUnitResults(results);
        }
        
    }
}
