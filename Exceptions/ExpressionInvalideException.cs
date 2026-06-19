using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class ExpressionInvalideException : Exception
    {
        public ExpressionInvalideException() : base("Expression invalide")
        {
        }
    }
}
