using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.Entities
{
    public class Orders : CommerceEntity
    {
        //user product
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int StockId  { get; set; }
        public string CustomerName { get; set; }
        public string ProductDescription { get; set; }
        public int StockQuantity { get; set; }
        public string OrderStatus { get; set; }
        public DateTime Date { get; set; }
        //payment simulation
        public string PaymentStatus { get; set; }
        public decimal TotalPrices { get; set; }
        public DateTime? OrderCompletion {  get; set; }

        public virtual Users Users { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Stocks Stocks { get; set; } = null!;

    }
}
