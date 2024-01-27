using GUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject
{
    public class GUnitTestResultTests
    {
        [Test]
        public void PassingTest()
        {
            GUnitTestResult gUnitTestResult = new GUnitTestResult("");
            Assert.That(gUnitTestResult.Passed,Is.True);
        }

        [Test]
        public void FailingTest()
        {
            GUnitTestResult gUnitTestResult = new GUnitTestResult("",new AssertionException("something"));
            Assert.That(gUnitTestResult.Passed, Is.False);
        }

        [Test]
        public void ExceptionTest()
        {
            GUnitTestResult gUnitTestResult = new GUnitTestResult("", new Exception("Exception"));
            Assert.That(gUnitTestResult.Passed, Is.False);
        }
    }
}
