using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Core.Exceptions
{
    public class InvalidVisaException : BusinessException
    {
        public InvalidVisaException(string message) : base(message)
        {
        }
    }
}
