using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attributes
    {
        public int AttributesId {  get; set; }
        public string Name { get; set; } = null!;
        public ICollection<AttributesValues> AttributeValues { get; set; } = new List<AttributesValues>(); 
    }
}  
