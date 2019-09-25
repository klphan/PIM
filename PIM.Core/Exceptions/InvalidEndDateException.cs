using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Core.Exceptions
{
    public class InvalidEndDateException : Exception
    {
        public InvalidEndDateException(string message) : base(message)
        {
        }

    }
}
