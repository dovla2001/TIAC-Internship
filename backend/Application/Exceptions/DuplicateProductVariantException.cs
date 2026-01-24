namespace Application.Exceptions
{
    public class DuplicateProductVariantException : Exception
    {
        public DuplicateProductVariantException(string message) : base(message) { }
    }
}
