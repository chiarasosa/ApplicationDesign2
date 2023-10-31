namespace Obligatorio1.Exceptions
{
    public class ExceptionPurchase : Exception
    {
        public ExceptionPurchase() : base() { }
        public ExceptionPurchase(string message) : base(message) { }
        public ExceptionPurchase(string message, Exception innerException) : base(message, innerException) { }
    }
}