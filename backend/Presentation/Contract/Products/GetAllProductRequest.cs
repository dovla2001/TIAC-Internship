namespace Presentation.Contract.Products
{
    public class GetAllProductRequest
    {
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 5;

        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }

        public string? Name { get; set; }
    }
}
