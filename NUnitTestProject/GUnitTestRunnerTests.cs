using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUnit;
using NUnitTestProject.TestClasses;
namespace NUnitTestProject
{
    public class GUnitTestRunnerTests
    {
        [Test]
        public void PassingTest()
        {
            GUnitTestRunResults results = GUnitTestRunner.RunTests<PassingTest>();

            Assert.IsTrue(results.AllTestsPassed);
            Assert.AreEqual(results.GenerateReport(),"Test 'NUnitTestProject.TestClasses.PassingTest.Test' Passed");
        }

        [Test]
        public void FailingTest()
        {
            GUnitTestRunResults results = GUnitTestRunner.RunTests<FailingTest>();

            Assert.IsFalse(results.AllTestsPassed);
            string expected = "Test 'NUnitTestProject.TestClasses.FailingTest.Test' Failed\nException has been thrown by the target of an invocation.\n   at System.RuntimeMethodHandle.InvokeMethod(Object target, Span`1& arguments, Signature sig, Boolean constructor, Boolean wrapExceptions)\r\n   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)\r\n   at System.Reflection.MethodBase.Invoke(Object obj, Object[] parameters)\r\n   at GUnit.GUnitTestRunner.ExecuteTests[T](String className, List`1 tests, MethodInfo setupMethod, MethodInfo teardownMethod) in C:\\Users\\tevan\\source\\repos\\GUnit\\GUnit\\GUnitTestRunner.cs:line 61";
            string actual = results.GenerateReport();
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void MixedTests()
        {
            GUnitTestRunResults results = GUnitTestRunner.RunTests<BothFailingAndPassingTest>();

            Assert.IsFalse(results.AllTestsPassed);
            string expected = "Test 'NUnitTestProject.TestClasses.BothFailingAndPassingTest.FailingTest' Failed\nException has been thrown by the target of an invocation.\n   at System.RuntimeMethodHandle.InvokeMethod(Object target, Span`1& arguments, Signature sig, Boolean constructor, Boolean wrapExceptions)\r\n   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)\r\n   at System.Reflection.MethodBase.Invoke(Object obj, Object[] parameters)\r\n   at GUnit.GUnitTestRunner.ExecuteTests[T](String className, List`1 tests, MethodInfo setupMethod, MethodInfo teardownMethod) in C:\\Users\\tevan\\source\\repos\\GUnit\\GUnit\\GUnitTestRunner.cs:line 61\r\n";
            expected += "Test 'NUnitTestProject.TestClasses.BothFailingAndPassingTest.PassingTest' Passed";
            string actual = results.GenerateReport();
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void SetupTest() 
        {
            GUnitTestRunResults results = GUnitTestRunner.RunTests<SetupTesting>();

            Assert.IsTrue(results.AllTestsPassed);
        }

        [Test]
        public void TeardownTest()
        {
            GUnitTestRunResults results = GUnitTestRunner.RunTests<TearDownTest>();

            Assert.IsTrue(results.AllTestsPassed);
        }
    }
}
