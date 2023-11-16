namespace Obligatorio1.Exceptions
{
    public class CartProductException : Exception
    {
        public CartProductException() : base() { }
        public CartProductException(string message) : base(message) { }
        public CartProductException(string message, Exception innerException) : base(message, innerException) { }
    }
}