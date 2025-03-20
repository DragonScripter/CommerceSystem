using CommerceDAL.Entities;
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
    public class OrderDAO
    {
        readonly IRepository<Orders> _repo;
        public OrderDAO(IRepository<Orders> repo)
        {
            _repo = repo;
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
