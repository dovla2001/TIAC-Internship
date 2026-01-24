namespace Presentation.Contract.WishListItem
{
    public class WishListItemResponse
    {
        public int WishListItemID { get; set; }
        public int ProductVariantId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new();
        public DateTime DateAdded { get; set; }
    }
}
