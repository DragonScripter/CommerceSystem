﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.Entities
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
