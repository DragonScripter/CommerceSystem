using CommerceDAL.Entities;
using CommerceDAL.Repository.Implementation;
using CommerceDAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.DAO
{
    public class ProductDAO
    {
        readonly IRepository<Product> _repo;
        public ProductDAO()
        {
            _repo = new CommerceRepository<Product>();
        }

        public async Task<List<Product>> GetAll() 
        {
            List<Product> allProducts = new();
            try
            {
                allProducts = await _repo.GetAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return allProducts;
        }
    }
}
