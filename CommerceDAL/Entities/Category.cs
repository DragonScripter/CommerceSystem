using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.Entities
{
    public class Category : CommerceEntity
    {
        public string Name { get; set; }


        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
