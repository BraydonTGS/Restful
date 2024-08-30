using Restful.Entity.Base;
using Serilog;

namespace Restful.Core.Base
{
    /// <summary>
    /// Generic Base Business Logic used for interacting with the <see cref="IBaseRepository{TEntity, TKey}"/>
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseBL<TModel, TEntity, TKey> : IBaseBL<TModel, TEntity, TKey>
        where TModel : ModelBase<TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly IBaseRepository<TEntity, TKey> _repository;
        protected readonly IMapper<TModel, TEntity> _mapper;
        protected readonly ILogger _log;

        public BaseBL(
            IBaseRepository<TEntity, TKey> repository,
            IMapper<TModel, TEntity> mapper,
            ILogger log)
        {
            _repository = repository;
            _mapper = mapper;
            _log = log;
        }

        #region GetAllAsync
        /// <summary>
        /// Retrieves all Models of the specified type asynchronously.
        /// </summary>
        /// <returns>A collection of Models or null if none are found.</returns>
        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            _log.Information($"Starting GetAllAsync for EntityType: {typeof(TEntity).Name}.");
            try
            {
                var entities = await _repository.GetAllAsync();

                if (entities.Count is 0)
                {
                    _log.Warning($"No Entities Found During GetAllAsync for EntityType: {typeof(TEntity).Name}.");
                    return [];
                };
                
                var models = _mapper.Map(entities);

                _log.Information($"Completed GetAllAsync for EntityType: {typeof(TEntity).Name}. {entities.Count} Entities Mapped to Models.");
                return models;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetAllAsync for EntityType: {typeof(TEntity).Name} with Message: {ex.Message}.");
                throw;
            }

        }
        #endregion

        #region GetByIdAsync
        /// <summary>
        /// Retrieves a Model of the specified type by its unique identifier asynchronously.
        /// </summary>
        /// <param name="key">The unique identifier of the Model to retrieve.</param>
        /// <returns>The retrieved model or null if not found.</returns>
        public virtual async Task<TModel?> GetByIdAsync(TKey key)
        {
            _log.Information($"Starting GetByIdAsync for EntityType: {typeof(TEntity).Name}.");
            try
            {
                var entity = await _repository.GetByIdAsync(key);

                if (entity == null)
                {
                    _log.Warning($"No Entity Found During GetByIdAsync for EntityType: {typeof(TEntity).Name}.");
                    return null;
                }

                var model = _mapper.Map(entity);

                _log.Information($"Completed GetByIdAsync. Entity Mapped to Model of Type: {typeof(TModel).Name}.");
                return model;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetByIdAsync for EntityType: {typeof(TEntity).Name} with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

        #region CreateAsync
        /// <summary>
        /// Creates a new Model asynchronously.
        /// </summary>
        /// <param name="model">The Model to be created.</param>
        /// <returns>The created model after it is added to the database.</returns>
        public virtual async Task<TModel?> CreateAsync(TModel model)
        {
            _log.Information($"Starting CreateAsync for Model of Type: {typeof(TModel).Name}.");
            try
            {
                var entity = _mapper.Map(model);
                if (entity == null)
                {
                    _log.Warning($"Unable to Map Model of Type {typeof(TModel).Name} to Entity.");
                    return null;
                }

                var createdEntity = await _repository.CreateAsync(entity);
                if (createdEntity == null)
                {
                    _log.Warning($"Failed to create Entity of Type {typeof(TEntity).Name} in Database.");
                    return null;
                }

                var resultModel = _mapper.Map(createdEntity);

                _log.Information($"Completed CreateAsync for Model of Type: {typeof(TModel).Name}. Entity Creation and Mapping Successful.");
                return resultModel;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in CreateAsync for Model of Type: {typeof(TModel).Name} with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

        #region UpdateAsync
        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <returns>The updated model after changes are saved to the database.</returns>
        public virtual async Task<TModel?> UpdateAsync(TModel model)
        {
            _log.Information($"Starting UpdateAsync for Model of Type: {typeof(TModel).Name}.");
            try
            {
                var entity = _mapper.Map(model);
                if (entity is null)
                {
                    _log.Warning($"Unable to Map Model to Entity. Model Type: {typeof(TModel).Name}");
                    return null;
                }

                var results = await _repository.UpdateAsync(entity);
                if (results == null)
                {
                    _log.Warning($"Update operation failed for Entity Type: {typeof(TEntity).Name}");
                    return null;
                }
                model = _mapper.Map(results);

                _log.Information($"Completed UpdateAsync for Model Type: {typeof(TModel).Name}.");
                return model;
            }
            catch (Exception ex)
            {

                _log.Error($"Exception in UpdateAsync for Model Type: {typeof(TModel).Name} with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

        #region SoftDeleteAsync
        /// <summary>
        /// Soft Deletes an Model by setting an IsDeleted flag.
        /// </summary>
        /// <param name="key">The unique identifier of the entity to be soft-deleted.</param>
        /// <returns>True if the soft delete is successful; otherwise, false.</returns>
        public virtual async Task<bool> SoftDeleteAsync(TKey key)
        {
            _log.Information($"Starting SoftDeleteAsync for Entity Type: {typeof(TEntity).Name}.");
            try
            {
                var result = await _repository.SoftDeleteAsync(key);

                _log.Information($"Completed SoftDeleteAsync for Entity Type: {typeof(TEntity).Name}.");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in SoftDeleteAsync for Entity Type: {typeof(TEntity).Name} with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

        #region HardDeleteAsync
        /// <summary>
        /// Hard Deletes an entity from the database.
        /// </summary>
        /// <param name="key">The unique identifier of the entity to be hard-deleted.</param>
        /// <returns>True if the hard delete is successful; otherwise, false.</returns>
        public virtual async Task<bool> HardDeleteAsync(TKey key)
        {
            _log.Information($"Starting HardDeleteAsync for Entity Type: {typeof(TEntity).Name}.");
            try
            {
                var result = await _repository.HardDeleteAsync(key);

                _log.Information($"Completed HardDeleteAsync for Entity Type: {typeof(TEntity).Name}.");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in HardDeleteAsync for Entity Type: {typeof(TEntity).Name} with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

        #region RestoreAsync
        /// <summary>
        /// Restores a Soft-Deleted Model by setting the IsDeleted flag to false.
        /// </summary>
        /// <param name="key">The unique identifier of the entity to be restored.</param>
        /// <returns>True if the restoration is successful; otherwise, false.</returns>
        public virtual async Task<bool> RestoreAsync(TKey key)
        {
            _log.Information($"Starting RestoreAsync for Entity Type: {typeof(TEntity).Name}.");
            try
            {
                var result = await _repository.RestoreAsync(key);

                _log.Information($"Completed RestoreAsync for Entity Type: {typeof(TEntity).Name}.");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in RestoreAsync for Entity Type: {typeof(TEntity).Name} with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion
    }
}
