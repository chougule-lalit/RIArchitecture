using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Core.RIArchitectureCoreBase.Interface
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(object id);
        void Delete(T entityToDelete);
        Task<T> GetByIdAsync(object id);
        Task SaveChangesAsyc();

    }
}
