﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Exceptions
{
    [Serializable]
    public class InvalidResourceException : Exception
    {
        public InvalidResourceException(string message) : base(message)
        {
        }
    }
}
