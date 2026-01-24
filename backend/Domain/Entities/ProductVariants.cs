namespace Domain.Entities
{
    public class ProductVariants
    {
        public int ProductVariantsId { get; set; }
        public decimal Price { get; set; }  //skinuli ?

        public Products Product { get; set; }
        public int ProductId { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<VariantValues> VariantValues { get; set; } = new List<VariantValues>();
        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}
