using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUnit
{
    public class GUnitResults
    {
        List<GUnitTestResult> Results;
        public bool AllTestsPassed { get; private set; } = false;
        public GUnitResults(List<GUnitTestResult> results) 
        {
            Results = results;
            CheckForFailingTests();
        }

        private void CheckForFailingTests()
        {
            foreach (var result in Results)
            {
                if(!result.Passed)
                {
                    AllTestsPassed = false;
                    return;
                }
            }
            AllTestsPassed = true;
        }

        public string GenerateReport()
        {
            string toReturn = "";
            foreach (GUnitTestResult result in Results) 
            {
                if(result.Passed)
                {
                    toReturn += result.ShortDescription() + "\r\n";
                }
                else
                {
                    toReturn += result.LongDescription() + "\r\n";
                }
            }

            toReturn = toReturn.Trim();

            return toReturn;
        }


    }
}
