using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class DuplicateProductVariantException : Exception
    {
        public DuplicateProductVariantException(string message) : base(message) { }
    }
}
