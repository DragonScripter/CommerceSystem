using CommerceDAL.Entities;
using CommerceDAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.Repository.Implementation
{
    public class CommerceRepository<T> : IRepository<T> where T : CommerceEntity
    {
        readonly private CommerceContext _db;
        public CommerceRepository() 
        {
            _db = new CommerceContext();
        }
        public async Task<List<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T?> GetOne(Expression<Func<T, bool>> match)
        {
            return await _db.Set<T>().FirstOrDefaultAsync(match);
        }

        public async Task<List<T>> GetSome(Expression<Func<T, bool>> match)
        {
            return await _db.Set<T>().Where(match).ToListAsync();
        }
        public async Task<T> Add(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        public async Task<UpdateStatus> Update(T entity)
        {
            UpdateStatus status = UpdateStatus.Failed;
            try
            {
                T? currentEntity = await GetOne(e => e.Id == entity.Id);
                _db.Entry(currentEntity!).OriginalValues["Timer"] = entity.Timer;
                _db.Entry(currentEntity!).CurrentValues.SetValues(entity);
                if (await _db.SaveChangesAsync() == 1) 
                {
                    status = UpdateStatus.Ok;
                }
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                status = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod()!.Name + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod()!.Name + ex.Message);
            }
            return status;

        }

        public async Task<int> Delete(int i)
        {
            T? currentEntity = await GetOne(e => e.Id == i);
            _db.Set<T>().Remove(currentEntity!);
            return  _db.SaveChanges();
        }
    }
}
