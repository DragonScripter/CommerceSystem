using CommerceDAL;
using CommerceDAL.DAO;
using CommerceDAL.Entities;
using CommerceDAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CommerceViewModels
{
    public class OrderViewModel 
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime Date { get; set; }
        public DateTime? OrderCompletion { get; set; }
    }
}