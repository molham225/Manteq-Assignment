using Api.Context;
using Api.Context.Interfaces;
using Api.Model.Entities;
using Api.Persistense.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Persistense.Repositories.EF
{
    public class GenericRepository<T,I> : IGenericRepository<T,I> where T : Entity<I>
    {
        protected readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(I id)
        {
            return _context.Set<T>().Find(id);
        }
        public async Task<T> GetByIdAsync(I id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
