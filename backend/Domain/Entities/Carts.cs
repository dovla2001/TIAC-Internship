using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Carts
    {
        public int CartId {  get; set; }
        public int EmployeesId { get; set; } 
        public Employees Employees { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCartActive { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); 
    }
}
