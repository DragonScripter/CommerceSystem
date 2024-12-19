using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL.Repository.Interface
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetSome(Expression<Func<T, bool>> match);
        Task<List<T?>> GetOne(Expression<Func<T, bool>> match);
        Task<T> Add(T entity);
        Task<int> Update(T entity); 
        Task<int> Delete(int i);
    }
}
