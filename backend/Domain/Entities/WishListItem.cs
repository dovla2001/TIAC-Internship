namespace Domain.Entities
{
    public class WishListItem
    {
        public int WishListItemId { get; set; }

        public int EmployeeId { get; set; }
        public Employees Employee { get; set; }

        public int ProductVariantId { get; set; }
        public ProductVariants ProductVariant { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
