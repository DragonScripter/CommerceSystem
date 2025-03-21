using Castle.Core.Resource;
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

namespace CommerceDAL.DAO
{
    public class OrderDAO
    {
        readonly IRepository<Orders> _repo;
        private readonly CommerceContext _context;
        public OrderDAO(IRepository<Orders> repo, CommerceContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<List<Orders>> GetById(int id)
        {
            return await _context.Orders
                        .Where(o => o.CustomerId == id)
                        .ToListAsync();
        }
        public async Task<List<Orders>> GetAll()
        {
            List<Orders> allOrders = new();
            try
            {
                allOrders = await _repo.GetAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return allOrders;
        }
        public async Task<int> Add(Orders order)
        {
            try
            {
                await _repo.Add(order);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return order.Id;
        }

    }
}
