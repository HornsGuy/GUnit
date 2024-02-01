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
            GUnitResults results = GUnitTestRunner.RunTests<PassingTest>();

            string expected = "Test 'NUnitTestProject.TestClasses.PassingTest.Test' Passed";
            Assert.That(results.AllTestsPassed, Is.True);
            Assert.That(results.GenerateReport(), Is.EqualTo(expected));
        }

        [Test]
        public void FailingTest()
        {
            GUnitResults results = GUnitTestRunner.RunTests<FailingTest>();

            string expected = "Test 'NUnitTestProject.TestClasses.FailingTest.Test' Failed\n\n   at NUnit.Framework.Assert.ReportFailure(String message)\r\n   at NUnit.Framework.Assert.Fail(String message)\r\n   at NUnit.Framework.Assert.Fail()\r\n   at NUnitTestProject.TestClasses.FailingTest.Test() in C:\\Users\\tevan\\source\\repos\\GUnit\\NUnitTestProject\\TestClasses\\FailingTest.cs:line 15\r\n--- End of stack trace from previous location ---\r\n   at GUnit.GUnitTestRunner.ExecuteTests[T](String className, List`1 tests, MethodInfo setupMethod, MethodInfo teardownMethod) in C:\\Users\\tevan\\source\\repos\\GUnit\\GUnit\\GUnitTestRunner.cs:line 75";
            string actual = results.GenerateReport();
            Assert.That(results.AllTestsPassed, Is.False);
            Assert.That(actual, Is.EqualTo(expected));
            Assert.Pass(); // Must be called due to running Assert.Fail within our example tests. Must be some kind of static somewhere in NUnit tracking this stuff
        }

        [Test]
        public void MixedTests()
        {
            GUnitResults results = GUnitTestRunner.RunTests<BothFailingAndPassingTest>();

            string expected = "Test 'NUnitTestProject.TestClasses.BothFailingAndPassingTest.FailingTest' Failed\n  Assert.That(false, Is.True)\r\n  Expected: True\r\n  But was:  False\r\n\n   at NUnit.Framework.Assert.ReportFailure(String message)\r\n   at NUnit.Framework.Assert.ReportFailure(ConstraintResult result, String message, String actualExpression, String constraintExpression)\r\n   at NUnit.Framework.Assert.That[TActual](TActual actual, IResolveConstraint expression, NUnitString message, String actualExpression, String constraintExpression)\r\n   at NUnitTestProject.TestClasses.BothFailingAndPassingTest.FailingTest() in C:\\Users\\tevan\\source\\repos\\GUnit\\NUnitTestProject\\TestClasses\\BothFailingAndPassingTest.cs:line 15\r\n--- End of stack trace from previous location ---\r\n   at GUnit.GUnitTestRunner.ExecuteTests[T](String className, List`1 tests, MethodInfo setupMethod, MethodInfo teardownMethod) in C:\\Users\\tevan\\source\\repos\\GUnit\\GUnit\\GUnitTestRunner.cs:line 75\r\nTest 'NUnitTestProject.TestClasses.BothFailingAndPassingTest.PassingTest' Passed";
            string actual = results.GenerateReport();
            Assert.That(results.AllTestsPassed, Is.False);
            Assert.That(actual, Is.EqualTo(expected));
            Assert.Pass(); // Must be called due to running Assert.Fail within our example tests. Must be some kind of static somewhere in NUnit tracking this stuff
        }

        [Test]
        public void SetupTest() 
        {
            GUnitResults results = GUnitTestRunner.RunTests<SetupTesting>();

            Assert.That(results.AllTestsPassed, Is.True);
        }

        [Test]
        public void TeardownTest()
        {
            GUnitResults results = GUnitTestRunner.RunTests<TearDownTest>();

            Assert.That(results.AllTestsPassed, Is.True);
        }

        [Test]
        public void FilterTest()
        {
            GUnitResults results = GUnitTestRunner.RunTests<FilterTest>();
            Assert.That(results.AllTestsPassed, Is.True);
        }
    }
}
