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
            Assert.IsTrue(gUnitTestResult.Passed);
        }

        [Test]
        public void FailingTest()
        {
            GUnitTestResult gUnitTestResult = new GUnitTestResult("",new GUnitException("something"));
            Assert.IsFalse(gUnitTestResult.Passed);
        }

        [Test]
        public void ExceptionTest()
        {
            GUnitTestResult gUnitTestResult = new GUnitTestResult("", new Exception("Exception"));
            Assert.IsFalse(gUnitTestResult.Passed);
        }
    }
}
