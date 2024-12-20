using CommerceDAL.DAO;
using CommerceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommerceViewModels
{
    public class ProductViewModel
    {
        readonly private ProductDAO _pDAO;
        readonly private StocksDAO _sDAO;
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string? ImageUrl { get; set; }
        public string? Timer {  get; set; }


        public async Task<int> GetProductAmount(int id) 
        {
            var amount = await _sDAO.GetById(id);
            return amount.Quanity;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            List<ProductViewModel> allProductsVm = new();
            try 
            {
                List<Product> allProducts = await _pDAO.GetAll();
                foreach (Product product in allProducts) 
                {
                    ProductViewModel vm = new() 
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description!,
                        Price = product.Price,
                        Amount = await GetProductAmount(product.Id),
                        Timer = Convert.ToBase64String(product.Timer!),
                };
                    if (product.ImageData != null)
                    {
                        ImageUrl = Convert.ToBase64String(product.ImageData);
                    }
                    allProductsVm.Add(vm);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return allProductsVm;
        }
    }
}
