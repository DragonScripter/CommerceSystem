using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.Entities
{
    public class Stocks : CommerceEntity
    {
        public int ProductId { get; set; }
        public int Quanity { get; set; }

        public virtual Product Product { get; set; }
    }
}
