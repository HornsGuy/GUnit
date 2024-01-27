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
            Assert.That(value, Is.EqualTo(1));
        }

        [GUnitTest]
        public void Test2()
        {
            Assert.That(value, Is.EqualTo(1));
        }

        [GUnitTest]
        public void Test3()
        {
            Assert.That(value, Is.EqualTo(1));
        }

        [GUnitTearDown]
        public void TearDown()
        {
            value--;
        }
    }
}
