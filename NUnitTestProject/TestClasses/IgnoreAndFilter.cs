using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUnit;
namespace NUnitTestProject.TestClasses
{
    public class IgnoreAndFilter
    {
        [GUnitFilter]
        [GUnitTest]
        public void Test0()
        {
            
        }

        [GUnitIgnore]
        [GUnitTest]
        public void Test1()
        {

        }
    }
}
