using GUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject.TestClasses
{
    public class FailingTest
    {
        [GUnitTest]
        public void Test()
        {
            GUnitAssert.IsTrue(false);
        }
    }
}
