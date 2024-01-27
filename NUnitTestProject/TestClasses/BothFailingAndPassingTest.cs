using GUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject.TestClasses
{
    public class BothFailingAndPassingTest
    {
        [GUnitTest]
        public void FailingTest()
        {
            GUnitAssert.IsTrue(false);
        }

        [GUnitTest]
        public void PassingTest()
        {
            GUnitAssert.IsTrue(true);
        }
    }
}
