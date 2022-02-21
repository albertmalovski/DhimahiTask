using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralException(string message)
           : base(message)
        {

        }
    }
}
