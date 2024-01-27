﻿using GUnit;
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
            Assert.That(false, Is.True);
        }

        [GUnitTest]
        public void PassingTest()
        {
            Assert.That(true, Is.True);
        }
    }
}
