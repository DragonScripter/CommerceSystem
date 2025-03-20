using CommerceDAL.Entities;
using CommerceDAL.Repository.Implementation;
using CommerceDAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.DAO
{
    public class StocksDAO
    {
        readonly IRepository<Stocks> _repo;
        private readonly CommerceContext _context;
        public StocksDAO(IRepository<Stocks> repo, CommerceContext commerceContext)
        {
            _repo = repo;
            _context = commerceContext;
        }
        public async Task<Stocks> GetById(int id) 
        {
            Stocks stock;
            try 
            {
                stock = await _repo.GetOne(s => s.ProductId == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return stock!;
        }
        public async Task<UpdateStatus> Update(Stocks stock)
        {
            UpdateStatus stockUpdated;
            try
            {
                stockUpdated = await _repo.Update(stock);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " + ex.Message +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return stockUpdated;
        }
        public async Task<int> Add(Stocks stock)
        {
            try
            {
                await _repo.Add(stock);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return stock.Id;
        }

        public async Task<Stocks> GetByProductId(int productId)
        {
            return await _context.Stocks
                .Where(s => s.ProductId == productId)
                .FirstOrDefaultAsync();
        }
    }
}
