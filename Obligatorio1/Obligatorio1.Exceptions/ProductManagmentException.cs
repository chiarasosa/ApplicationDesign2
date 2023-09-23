using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Exceptions
{
    public class ProductManagmentException : Exception
    {
        public ProductManagmentException() { }
        public ProductManagmentException(string message) : base(message) { }
        public ProductManagmentException(string message, Exception innerException) : base(message, innerException) { }
    }
}

