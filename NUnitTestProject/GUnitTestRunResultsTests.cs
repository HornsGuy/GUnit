using GUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject
{
    public class GUnitTestRunResultsTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AllTestsPassed()
        {
            List<GUnitTestResult> results = new List<GUnitTestResult>();
            results.Add(new GUnitTestResult("TestName"));
            GUnitResults testRunResults = new GUnitResults("className",results);
            Assert.That(testRunResults.AllTestsPassed, Is.True);
        }

        [Test]
        public void OneTestFailedByException()
        {
            List<GUnitTestResult> results = new List<GUnitTestResult>();
            results.Add(new GUnitTestResult("TestName"));
            results.Add(new GUnitTestResult("TestFailed",new Exception("Message")));
            GUnitResults testRunResults = new GUnitResults("className",results);
            Assert.That(testRunResults.AllTestsPassed, Is.False);
        }

        [Test]
        public void OneTestFailed()
        {
            List<GUnitTestResult> results = new List<GUnitTestResult>();
            results.Add(new GUnitTestResult("TestName"));
            results.Add(new GUnitTestResult("TestFailed", new AssertionException("Message")));
            GUnitResults testRunResults = new GUnitResults("className",results);
            Assert.That(testRunResults.AllTestsPassed, Is.False);
        }

        [Test]
        public void AllThreePossibilities()
        {
            List<GUnitTestResult> results = new List<GUnitTestResult>();
            results.Add(new GUnitTestResult("TestName"));
            results.Add(new GUnitTestResult("TestFailed1", new Exception("Message")));
            results.Add(new GUnitTestResult("TestFailed2", new AssertionException("Message")));
            GUnitResults testRunResults = new GUnitResults("className",results);
            Assert.That(testRunResults.AllTestsPassed, Is.False);
        }
    }
}
