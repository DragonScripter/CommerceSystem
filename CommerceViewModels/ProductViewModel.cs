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
    public class ProductViewModel
    {
        readonly private ProductDAO _pDAO;
        readonly private StocksDAO _sDAO;
        readonly private CommerceContext _context;
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string? ImageUrl { get; set; }
        public string? Timer { get; set; }

        public ProductViewModel(ProductDAO pDAO, StocksDAO sDAO)
        {
            _pDAO = pDAO;
            _sDAO = sDAO;
        }
        public ProductViewModel(ProductDAO pDAO, StocksDAO sDAO, CommerceContext context)
        {
            _pDAO = pDAO;
            _sDAO = sDAO;
            _context = context;
        }

        public async Task<int> GetProductAmount(int id)
        {
            var amount = await _sDAO.GetById(id);
            return amount?.Quanity ?? 0;
        }
        public async Task<Stocks> GetStock(int id)
        {
            var amount = await _sDAO.GetById(id);
            return amount;
        }

        public async Task<Product> GetById() 
        {
            Product product = await _pDAO.GetById(Id!);
            return product;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            List<ProductViewModel> allProductsVm = new();
            try
            {
                List<Product> allProducts = await _pDAO.GetAll();
                foreach (Product product in allProducts)
                {
                    ProductViewModel vm = new ProductViewModel(_pDAO, _sDAO)
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
                        
                        vm.ImageUrl = $"/images/{product.Id}.jpg";  

                       
                        // vm.ImageUrl = Convert.ToBase64String(product.ImageData); 
                    }

                    allProductsVm.Add(vm);
                }
            }
            catch (Exception ex)
            {
                // Handle error (optional)
                Console.WriteLine(ex.Message);
            }
            return allProductsVm;
        }

        //going to make it to a transaction
        //public async Task<int> Update() 
        //{
        //    int update;
        //    int update2;
        //    try
        //    {
        //        Product product = new()
        //        {
        //            Id = Id,
        //            Name = Name!,
        //            Description = Description,
        //            Price = Price,
        //            Timer = Timer != null ? Convert.FromBase64String(Timer) : null

        //        };
        //        if (product.ImageData != null)
        //        {
        //            ImageUrl = Convert.ToBase64String(product.ImageData);
        //        }
        //        var stock = await GetStock(Id);
        //        if (stock != null) 
        //        {
        //            stock.Quanity = Amount;
        //        }
        //        // going to add this later on for adding stock if product doesnt have any
        //        //else 
        //        //{
        //        //    stock = new Stocks
        //        //    {
        //        //        ProductId = product.Id,
        //        //        Quantity = Amount
        //        //    };

        //        //    await _sDAO.AddStock(stock); 
        //        //}



        //        update = (int)await _pDAO.Update(product);
        //        update2 = (int)await _sDAO.Update(stock);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Problem in " + GetType().Name + " " +
        //        MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
        //        throw;
        //    }
        //    if (update == 1 && update2 == 1) 
        //    {
        //        return update;
        //    }

        //}
        public async Task<int> Update()
        {
            int productUpdateResult = 0;
            int stockUpdateResult = 0;

            try
            {
               
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    Product product = new()
                    {
                        Id = Id,
                        Name = Name ?? throw new ArgumentNullException(nameof(Name), "Product name cannot be null."),
                        Description = Description,
                        Price = Price,
                        Timer = Timer != null ? Convert.FromBase64String(Timer) : null
                    };
                   
                    var stock = await GetStock(Id);
                    if (stock != null)
                    {
                        
                        stock.Quanity = Amount;
                    }
                    //else
                    //{
                    //  
                    //    stock = new Stocks
                    //    {
                    //        ProductId = product.Id,
                    //        Quantity = Amount
                    //    };
                    //    stockUpdateResult = await _sDAO.AddStock(stock);
                    //}

                    
                    productUpdateResult = (int)await _pDAO.Update(product); 
                    if (stock != null && stockUpdateResult == 0)
                    {
                        stockUpdateResult = (int)await _sDAO.Update(stock);
                    }

          
                    if (productUpdateResult == 1 && stockUpdateResult == 1)
                    {
                        await transaction.CommitAsync();
                        return 1; 
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
               
                Debug.WriteLine($"Error in {GetType().Name}.{MethodBase.GetCurrentMethod()?.Name}: {ex.Message}");
                throw; 
            }
        }
        public async Task Add() 
        {
            Id = -1;
            try
            {
                Product product = new()
                {
                    Name = Name ?? throw new ArgumentNullException(nameof(Name), "Product name cannot be null."),
                    Description = Description,
                    Price = Price,
                    ImageData = ImageUrl != null ? Convert.FromBase64String(ImageUrl!) : null
                };
                Id = await _pDAO.Add(product);
                if (Id != -1)
                {
                    Stocks stock = new()
                    {
                        ProductId = Id, 
                        Quanity = Amount    
                    };

                 
                    await _sDAO.Add(stock); 
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }
    }
}