using GUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject.TestClasses
{
    public class FilterTest
    {
        [GUnitTest]
        public void Test1()
        {
            Assert.Fail();
        }

        [GUnitTest]
        public void Test2()
        {
            Assert.Fail();
        }

        [GUnitFilter]
        [GUnitTest]
        public void Test3()
        {
            Assert.Pass();
        }
    }
}
