using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.Entities
{
    public class Product : CommerceEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Quanity { get; set; }
        public decimal Price { get; set; }
    }
}
