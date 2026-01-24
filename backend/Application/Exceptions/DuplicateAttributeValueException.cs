namespace Application.Exceptions
{
    public class DuplicateAttributeValueException : Exception
    {
        public DuplicateAttributeValueException(string message) : base(message) { }
    }
}
