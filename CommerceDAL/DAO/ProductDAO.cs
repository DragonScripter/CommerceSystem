﻿using CommerceDAL.Entities;
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
        public ProductDAO(IRepository<Product> repo)
        {
            _repo = repo; 
        }
        public async Task<Product> GetById(int id) 
        {
            Product product;
            try 
            {
                product = await _repo.GetOne(c => c.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return product!;
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
        public async Task<UpdateStatus> Update(Product product) 
        {
            UpdateStatus productUpdated;
            try 
            {
                productUpdated = await _repo.Update(product); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " + ex.Message +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return productUpdated;
        }
        public async Task<int> Add(Product product) 
        {
            try
            {
                await _repo.Add(product);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return product.Id;
        }
    }
}
