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

            Assert.That(results.AllTestsPassed, Is.False);
            Assert.Pass(); // Must be called due to running Assert.Fail within our example tests. Must be some kind of static somewhere in NUnit tracking this stuff
        }

        [Test]
        public void MixedTests()
        {
            GUnitResults results = GUnitTestRunner.RunTests<BothFailingAndPassingTest>();

            Assert.That(results.AllTestsPassed, Is.False);
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
        
        [Test]
        public void IgnoreTest()
        {
            GUnitResults results = GUnitTestRunner.RunTests<IgnoreTest>();
            Assert.That(results.AllTestsPassed, Is.True);
        }

        [Test]
        public void IgnoreAndFilterInSameClass()
        {
            GUnitResults results = GUnitTestRunner.RunTests<IgnoreAndFilter>();
            Assert.That(results.PassingTestCount, Is.EqualTo(1));
        }
    }
}
