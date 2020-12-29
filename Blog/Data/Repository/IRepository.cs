using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    /// <summary>
    /// Serves as wrapper on DbContext functionality. Repository handels read and write database and abstracts calls to it.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Create a track of addition changes in EF Core.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Add(T entity);

        /// <summary>
        /// Get specific entity by id.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// Create a track of deletion changes in EF Core.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns></returns>
        void Remove(T entity);

        /// <summary>
        /// Create a track of updation changes in EF Core.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns></returns>
        void Update(T entity);

        /// <summary>
        /// Finally save all tracked changes to database asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Get specific entities sorted by the condition.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        List<T> GetAllByCondition(Expression<Func<T, bool>> expression);
    }
}
