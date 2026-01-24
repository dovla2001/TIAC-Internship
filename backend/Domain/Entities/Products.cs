namespace Domain.Entities
{
    public class Products
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Decsription { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public string ImageUrl { get; set; } = null!;
        public ICollection<ProductVariants> ProductVariants { get; set; } = new List<ProductVariants>();
    }
}
