using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Orders
    {
        public int OrdersId {  get; set; }
        public int EmployeeId { get; set; }  
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Employees Employees { get; set; } 
        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}
