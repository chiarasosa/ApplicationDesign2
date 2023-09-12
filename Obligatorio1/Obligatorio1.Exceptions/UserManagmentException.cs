using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Exceptions
{
    public class UserManagmentException: Exception
    {
        public UserManagmentException() { }

        public UserManagmentException(string message) : base(message) { }

        public UserManagmentException(string message, Exception innerException) : base(message, innerException) { }
    }
}