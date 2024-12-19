using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.Entities
{
    public class Orders : CommerceEntity
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string CustomerName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public int Stock { get; set; }
        public string OrderStatus { get; set; }
        public DateTime Date { get; set; }

        public virtual Users Users { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
