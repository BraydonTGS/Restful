﻿using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;
using Restful.Entity.Base;

namespace Restful.Core.Base
{
    /// <summary>
    /// Generic repository providing basic CRUD (Create, Read, Update, Delete) functionality for entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    /// <see cref="IBaseRepository{TEntity, TKey}"/>
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Private
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        #endregion

        #region Constructor
        public BaseRepository(IDbContextFactory<RestfulDbContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }
        #endregion

        #region GetAllAsync
        /// <summary>
        /// Retrieves all entities of the specified type asynchronously.
        /// </summary>
        /// <returns>A collection of entities or null if none are found.</returns>
        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            return await context.Set<TEntity>().Where(x => !x.IsDeleted).ToListAsync();
        }
        #endregion

        #region GetByIdAsync
        /// <summary>
        /// Retrieves an entity of the specified type by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>The retrieved entity or null if not found.</returns>
        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            return await context.Set<TEntity>().FindAsync(id);
        }
        #endregion

        #region CreateAsync
        /// <summary>
        /// Creates a new entity asynchronously and adds it to the database.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>The created entity after it is added to the database.</returns>
        public virtual async Task<TEntity?> CreateAsync(TEntity entity)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var newEntry = await context.Set<TEntity>().AddAsync(entity);

            await context.SaveChangesAsync();

            return newEntry.Entity;
        }
        #endregion

        #region UpdateAsync
        /// <summary>
        /// Updates an existing entity in the database.
        /// Attaches the provided entity to the context, marks it as modified, and saves changes to the database.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <returns>The updated entity after changes are saved to the database.</returns>
        public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            context.Set<TEntity>().Attach(entity);

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return entity;
        }
        #endregion

        #region SoftDeleteAsync
        /// <summary>
        /// Soft deletes an entity by setting an IsDeleted flag.
        /// Instead of directly finding and updating, this method attaches the entity and marks it as modified.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be soft-deleted.</param>
        /// <returns>True if the soft delete is successful; otherwise, false.</returns>
        public virtual async Task<bool> SoftDeleteAsync(TKey id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity is null) return false;

            entity.IsDeleted = true;

            context.Set<TEntity>().Attach(entity);

            context.Entry(entity).State = EntityState.Modified;

            var rowsAffected = await context.SaveChangesAsync();

            return rowsAffected > 0;
        }
        #endregion

        #region HardDeleteAsync
        /// <summary>
        /// Hard deletes an entity from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be hard-deleted.</param>
        /// <returns>True if the hard delete is successful; otherwise, false.</returns>
        public virtual async Task<bool> HardDeleteAsync(TKey id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity is null) return false;

            context.Set<TEntity>().Remove(entity);

            var rowsAffected = await context.SaveChangesAsync();

            return rowsAffected > 0;
        }
        #endregion

        #region RestoreAsync
        /// <summary>
        /// Restores a soft-deleted entity by setting the IsDeleted flag to false.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be restored.</param>
        /// <returns>True if the restoration is successful; otherwise, false.</returns>
        public virtual async Task<bool> RestoreAsync(TKey id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity is null) return false;

            entity.IsDeleted = false;

            var rowsAffected = await context.SaveChangesAsync();

            return rowsAffected > 0;
        }
        #endregion
    }
}
