using Microsoft.EntityFrameworkCore;
using RIArchitecture.Core.RIArchitectureCoreBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Infrastructure.RIArchitectureInfrastructureBase
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RIArchitectureDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(RIArchitectureDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();

        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query;
        }

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual T Insert(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual T Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task SaveChangesAsyc()
        {
            await _context.SaveChangesAsync();
        }
    }
}
