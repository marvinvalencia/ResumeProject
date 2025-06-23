// <copyright file="EntityBaseRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Repositories
{
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using ResumeProject.Application.Interfaces;
    using ResumeProject.Domain.Interfaces;
    using ResumeProject.Infrastructure.Data;

    /// <summary>
    /// The EntityBaseRepository class provides a base implementation for repository operations on entities that implement IEntityBase.
    /// </summary>
    /// <typeparam name="T">The class.</typeparam>
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
            where T : class, IEntityBase, new()
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EntityBaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// This method retrieves all entities of type T from the database.
        /// </summary>
        /// <returns>The entity.</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return this.context.Set<T>().AsEnumerable();
        }

        /// <summary>
        /// The Count method returns the total number of entities of type T in the database.
        /// </summary>
        /// <returns>The count.</returns>
        public virtual int Count()
        {
            return this.context.Set<T>().Count();
        }

        /// <summary>
        /// The AllIncluding method retrieves all entities of type T from the database, including specified related entities.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>The entities.</returns>
        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.AsEnumerable();
        }

        /// <summary>
        /// The GetSingle method retrieves a single entity of type T from the database by its ID.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The entity.</returns>
        /// <exception cref="InvalidOperationException">The exception.</exception>
        public T GetSingle(Guid id)
        {
            var entity = this.context.Set<T>().FirstOrDefault(x => x.Id == id);
            return entity ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }

        /// <summary>
        /// The GetSingle method retrieves a single entity of type T from the database that matches the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entity.</returns>
        /// <exception cref="InvalidOperationException">The exception.</exception>
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            var entity = this.context.Set<T>().FirstOrDefault(predicate);
            return entity ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} matched the specified condition.");
        }

        /// <summary>
        /// The GetSingle method retrieves a single entity of type T from the database that matches the specified predicate, including specified related entities.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>The entity.</returns>
        /// <exception cref="InvalidOperationException">The exception.</exception>
        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            var entity = query.Where(predicate).FirstOrDefault();
            return entity ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} matched the specified condition.");
        }

        /// <summary>
        /// The FindBy method retrieves all entities of type T from the database that match the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entities.</returns>
        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return this.context.Set<T>().Where(predicate);
        }

        /// <summary>
        /// The Add method adds a new entity of type T to the database.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = this.context.Entry<T>(entity);
            this.context.Set<T>().Add(entity);
        }

        /// <summary>
        /// The Update method updates an existing entity of type T in the database.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = this.context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// The Delete method marks an existing entity of type T for deletion in the database.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = this.context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        /// <summary>
        /// The DeleteWhere method marks all entities of type T that match the specified predicate for deletion in the database.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = this.context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                this.context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// The Commit method saves all changes made in the context to the database.
        /// </summary>
        public virtual void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
