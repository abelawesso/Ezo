using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class DivisionParZeroException : Exception 
    {
        public DivisionParZeroException() : base("Tentative de division par zéro")
        {
        }
    }
}
