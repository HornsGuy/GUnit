using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUnit;
namespace NUnitTestProject.TestClasses
{
    public class PassingTest
    {
        [GUnitTest]
        public void Test()
        {
            GUnitAssert.IsTrue(true);
        }
    }
}
