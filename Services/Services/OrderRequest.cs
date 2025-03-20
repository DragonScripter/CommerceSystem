using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public List<ProductRequest> Products { get; set; }
    }

    public class ProductRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
