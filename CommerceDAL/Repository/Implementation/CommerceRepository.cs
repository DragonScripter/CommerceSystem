using CommerceDAL.Entities;
using CommerceDAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.Repository.Implementation
{
    public class CommerceRepository<T> : IRepository<T> where T : CommerceEntity
    {

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<T?>> GetOne(Expression<Func<T, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetSome(Expression<Func<T, bool>> match)
        {
            throw new NotImplementedException();
        }
        public Task<T> Add(T entity)
        {
            throw new NotImplementedException();
        }
        public Task<Update> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int i)
        {
            throw new NotImplementedException();
        }
    }
}
