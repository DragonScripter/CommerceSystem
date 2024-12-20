using CommerceDAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceViewModels
{
    public class ProductViewModel
    {
        readonly private ProductDAO _pDAO;
        //readonly private StockDAO _sDAO;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string ImageUrl { get; set; }


    }
}
