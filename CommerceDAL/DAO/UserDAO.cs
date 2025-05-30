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
    public class UserDAO
    {
        readonly IRepository<Users> _repo;
        public UserDAO(IRepository<Users> repo)
        {
            _repo = repo;
        }
        public async Task<Users> GetById(int id)
        {
            Users user;
            try
            {
                user = await _repo.GetOne(c => c.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return user!;
        }
        public async Task<Users?> GetByEmail(string email)
        {
            try
            {
               return await _repo.GetOne(c => c.Email== email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }
        public async Task<int> Add(Users user)
        {
            try
            {
                await _repo.Add(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return user.Id;
        }
    }
}
