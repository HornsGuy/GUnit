using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUnit
{
    public class GUnitTestRunResults
    {
        List<GUnitTestResult> Results;
        public GUnitTestRunResults(List<GUnitTestResult> results) 
        {
            Results = results;
        }

        public string GenerateReport()
        {
            string toReturn = "";
            foreach (GUnitTestResult result in Results) 
            {
                if(result.Passed)
                {
                    toReturn += result.ShortDescription() + "\n";
                }
                else
                {
                    toReturn += result.LongDescription() + "\n";
                }
            }

            return toReturn;
        }


    }
}
