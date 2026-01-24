namespace Domain.Entities
{
    public class Employees
    {
        public int EmployeesId { get; init; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
        public ICollection<Carts> Carts { get; set; } = new List<Carts>();
        public ICollection<WishListItem> WishListItems { get; set; } = new List<WishListItem>(); //dodao za wishlist

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }

    }
}
