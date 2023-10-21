using System;

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

