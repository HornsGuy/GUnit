using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUnit
{
    public class GUnitException : Exception
    {
        public override string Message { get; }
        public GUnitException(string message)
        {
            Message = message;
        }
    }
}
