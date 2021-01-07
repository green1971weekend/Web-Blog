using Blog.Application.Interfaces;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Data
{
    ///<inheritdoc cref="IRepository{T}"/>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        ///<inheritdoc/>
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        ///<inheritdoc/>
        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        ///<inheritdoc/>
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        ///<inheritdoc/>
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        ///<inheritdoc/>
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        ///<inheritdoc/>
        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

        ///<inheritdoc/>
        public List<T> GetAllByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }
    }
}
