﻿using System;
using System.Collections;
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
        public decimal Price { get; set; }
        public byte[]? ImageData { get; set; }

        public virtual ICollection<Stocks> Stocks { get; set; } = new List<Stocks>();
        public virtual ICollection<Orders> ProductOrders { get; set; } = new List<Orders>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
