namespace Obligatorio1.Exceptions
{
    public class CartException : Exception
    {
        public CartException() : base() { }
        public CartException(string message) : base(message) { }
        public CartException(string message, Exception innerException) : base(message, innerException) { }
    }
}