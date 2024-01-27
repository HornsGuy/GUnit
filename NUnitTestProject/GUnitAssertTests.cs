using GUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject
{
    public class GUnitAssertTests
    {
        [Test]
        public void AssertEqual()
        {
            try
            {
                GUnitAssert.AreEqual(1,1);
            }
            catch(GUnitException)
            {
                Assert.Fail("Equal failed when it shouldn't have");
            }

            // Type diff
            try
            {
                GUnitAssert.AreEqual(1,"");
                Assert.Fail("Equal passed when it shouldn't have");
            }
            catch(GUnitException) 
            {
                
            }

            // Value diff
            try
            {
                GUnitAssert.AreEqual(1.0, 2.0);
                Assert.Fail("Equal passed when it shouldn't have");
            }
            catch (GUnitException)
            {

            }
        }

        [Test]
        public void AssertNotEqual()
        {
            // Value difference
            try
            {
                GUnitAssert.AreNotEqual(1, 2);
            }
            catch (GUnitException)
            {
                Assert.Fail("");
            }

            // Type difference
            try
            {
                GUnitAssert.AreNotEqual(2, 2.0);
            }
            catch (GUnitException)
            {
                Assert.Fail("Not Equal failed when it shouldn't have");
            }

            // Equal
            try
            {
                GUnitAssert.AreNotEqual(1, 1);
                Assert.Fail("Not Equal passed when it shouldn't have");
            }
            catch (GUnitException)
            {

            }
        }

        [Test]
        public void AssertIsTrue()
        {
            try
            {
                GUnitAssert.IsTrue(true);
            }
            catch (GUnitException)
            {
                Assert.Fail("IsTrue failed when it should have passed");
            }

            try
            {
                GUnitAssert.IsTrue(false);
                Assert.Fail("IsTrue passed when it should have failed");
            }
            catch (GUnitException)
            {
                
            }
        }

        [Test]
        public void AssertIsFalse()
        {
            try
            {
                GUnitAssert.IsFalse(false);
            }
            catch (GUnitException)
            {
                Assert.Fail("IsFalse failed when it should have passed");
            }

            try
            {
                GUnitAssert.IsFalse(true);
                Assert.Fail("IsFalse passed when it should have failed");
            }
            catch (GUnitException)
            {

            }
        }

        [Test]
        public void AssertIsNull()
        {
            try
            {
                GUnitAssert.IsNull(null);
            }
            catch (GUnitException)
            {
                Assert.Fail("IsNull failed when it should have passed");
            }

            try
            {
                GUnitAssert.IsNull(1);
                Assert.Fail("IsNull passed when it should have failed");
            }
            catch (GUnitException)
            {

            }
        }

        [Test]
        public void AssertIsNotNull()
        {
            try
            {
                GUnitAssert.IsNotNull(1);
            }
            catch (GUnitException)
            {
                Assert.Fail("IsNotNull failed when it should have passed");
            }

            try
            {
                GUnitAssert.IsNotNull(null);
                Assert.Fail("IsNotNull passed when it should have failed");
            }
            catch (GUnitException)
            {

            }
        }
    }
}
