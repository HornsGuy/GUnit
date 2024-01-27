using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUnit;
namespace NUnitTestProject.TestClasses
{
    public class TearDownTest
    {
        int value = 0;
        [GUnitSetUp]
        public void Setup()
        {
            value++;
        }

        [GUnitTest]
        public void Test1()
        {
            GUnitAssert.AreEqual(1, value);
        }

        [GUnitTest]
        public void Test2()
        {
            GUnitAssert.AreEqual(1, value);
        }

        [GUnitTest]
        public void Test3()
        {
            GUnitAssert.AreEqual(1, value);
        }

        [GUnitTearDown]
        public void TearDown()
        {
            value--;
        }
    }
}
