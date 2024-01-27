using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUnit;
namespace NUnitTestProject.TestClasses
{
    public class SetupTesting
    {
        int value = -1;
        [GUnitSetUp]
        public void Setup()
        {
            value = 1;
        }

        [GUnitTest]
        public void Test()
        {
            GUnitAssert.AreEqual(1, value);
        }

        [GUnitTearDown]
        public void TearDown()
        {

        }
    }
}
