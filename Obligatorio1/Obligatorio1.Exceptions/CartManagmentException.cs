namespace Obligatorio1.Exceptions
{
    public class CartManagmentException : Exception
    {
        public CartManagmentException() : base() { }
        public CartManagmentException(string message) : base(message) { }
        public CartManagmentException(string message, Exception innerException) : base(message, innerException) { }
    }
}