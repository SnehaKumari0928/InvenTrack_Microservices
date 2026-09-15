using Shared.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Shared.Common.Interface
{
    public interface IGenericInterface<T> where T : class 
    {
        Task<Responses.Respone> AddAsync(T entity);
        Task<Responses.Respone> UpdateAsync(T entity);
        Task<Responses.Respone> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
    }
}
