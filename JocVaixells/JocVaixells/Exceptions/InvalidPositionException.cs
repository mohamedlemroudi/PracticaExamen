using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocVaixells.Exceptions
{
    public class InvalidPositionException : Exception
    {
        public int Min { get; set; } = 0;
        public int Max { get; set; } = 0;

        public InvalidPositionException(string message) : base(message)
        {
        }
    }
}
