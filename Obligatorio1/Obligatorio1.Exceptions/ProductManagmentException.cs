namespace Obligatorio1.Exceptions
{
    public class ProductManagmentException : Exception
    {
        public ProductManagmentException() { }
        public ProductManagmentException(string message) : base(message) { }
        public ProductManagmentException(string message, Exception innerException) : base(message, innerException) { }
    }
}

