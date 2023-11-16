namespace Obligatorio1.Exceptions
{
    public class PurchaseProductException : Exception
    {
        public PurchaseProductException() : base() { }
        public PurchaseProductException(string message) : base(message) { }
        public PurchaseProductException(string message, Exception innerException) : base(message, innerException) { }
    }
}