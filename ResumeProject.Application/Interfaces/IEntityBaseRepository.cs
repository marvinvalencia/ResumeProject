// <copyright file="IEntityBaseRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ResumeProject.Application.Interfaces
{
    using System.Linq.Expressions;
    using ResumeProject.Domain.Interfaces;

    /// <summary>
    /// The IEntityBaseRepository interface defines the contract for a repository that manages entities implementing IEntityBase.
    /// </summary>
    /// <typeparam name="T">The class.</typeparam>
    public interface IEntityBaseRepository<T>
        where T : class, IEntityBase, new()
    {
        /// <summary>
        /// This method retrieves all entities of type T from the database.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>The entities.</returns>
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// The GetAll method retrieves all entities of type T from the database.
        /// </summary>
        /// <returns>The entities.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// The Count method returns the total number of entities of type T in the database.
        /// </summary>
        /// <returns>The count.</returns>
        int Count();

        /// <summary>
        /// The GetSingle method retrieves a single entity of type T by its unique identifier.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The entity.</returns>
        T GetSingle(Guid id);

        /// <summary>
        /// The GetSingle method retrieves a single entity of type T from the database based on a predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entity.</returns>
        T GetSingle(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// The GetSingle method retrieves a single entity of type T from the database based on a predicate and includes specified related entities.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>The entity.</returns>
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// The FindBy method retrieves a collection of entities of type T from the database that match the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entities.</returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// The Add method adds a new entity of type T to the database.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);

        /// <summary>
        /// The Update method updates an existing entity of type T in the database.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// The Delete method removes an entity of type T from the database.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// The DeleteWhere method removes entities of type T from the database that match the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        void DeleteWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// The Commit method saves all changes made in the context to the database.
        /// </summary>
        void Commit();
    }
}
