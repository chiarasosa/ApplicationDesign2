using System;
namespace Obligatorio1.Exceptions
{
    [Serializable]
    public class NotNull : Exception
    {
        public NotNull(string message) : base(message)
        {
        }
    }
}

