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
        public string TestClassName { get; private set; }
        public bool AllTestsPassed { get; private set; } = true;
        public int TotalTestCount {  get; private set; }
        public int FailingTestCount { get; private set; }
        public int PassingTestCount { get; private set; }
        public GUnitResults(string testClassName, List<GUnitTestResult> results) 
        {
            TestClassName = testClassName;
            Results = results;
            CheckForFailingTests();
        }

        private void CheckForFailingTests()
        {
            foreach (var result in Results)
            {
                if(!result.Passed)
                {
                    FailingTestCount++;
                    AllTestsPassed = false;
                }
            }
            TotalTestCount = Results.Count;
            PassingTestCount = Results.Count - FailingTestCount;
        }

        public string GenerateReport()
        {
            string toReturn = "";
            foreach (GUnitTestResult result in Results) 
            {
                if(result.Passed)
                {
                    toReturn += result.ShortDescription();
                }
                else
                {
                    toReturn += result.LongDescription();
                }
                toReturn += "\r\n\r\n";
            }

            toReturn = toReturn.Trim();

            return toReturn;
        }


    }
}
