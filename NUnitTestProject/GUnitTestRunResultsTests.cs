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
            GUnitTestRunResults testRunResults = new GUnitTestRunResults(results);
            Assert.IsTrue(testRunResults.AllTestsPassed);
        }

        [Test]
        public void OneTestFailedByException()
        {
            List<GUnitTestResult> results = new List<GUnitTestResult>();
            results.Add(new GUnitTestResult("TestName"));
            results.Add(new GUnitTestResult("TestFailed",new Exception("Message")));
            GUnitTestRunResults testRunResults = new GUnitTestRunResults(results);
            Assert.IsFalse(testRunResults.AllTestsPassed);
        }

        [Test]
        public void OneTestFailed()
        {
            List<GUnitTestResult> results = new List<GUnitTestResult>();
            results.Add(new GUnitTestResult("TestName"));
            results.Add(new GUnitTestResult("TestFailed", new GUnitException("Message")));
            GUnitTestRunResults testRunResults = new GUnitTestRunResults(results);
            Assert.IsFalse(testRunResults.AllTestsPassed);
        }

        [Test]
        public void AllThreePossibilities()
        {
            List<GUnitTestResult> results = new List<GUnitTestResult>();
            results.Add(new GUnitTestResult("TestName"));
            results.Add(new GUnitTestResult("TestFailed1", new Exception("Message")));
            results.Add(new GUnitTestResult("TestFailed2", new GUnitException("Message")));
            GUnitTestRunResults testRunResults = new GUnitTestRunResults(results);
            Assert.IsFalse(testRunResults.AllTestsPassed);
        }
    }
}
