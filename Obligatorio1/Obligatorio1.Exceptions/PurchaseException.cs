namespace Obligatorio1.Exceptions
{
    public class PurchaseException : Exception
    {
        public PurchaseException() : base() { }
        public PurchaseException(string message) : base(message) { }
        public PurchaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}