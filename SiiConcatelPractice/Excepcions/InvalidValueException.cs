using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiiConcatelPractice.Excepcions
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException()
        {

        }


        public InvalidValueException(string message) : base(message)
        {

        }
    }
}
