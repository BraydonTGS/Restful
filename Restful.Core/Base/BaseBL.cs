using Restful.Entity.Base;

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
        private readonly IMapper<TModel, TEntity> _mapper;

        public BaseBL(
            IBaseRepository<TEntity, TKey> repository,
            IMapper<TModel, TEntity> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<TModel?> CreateAsync(TModel dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TModel?> GetByIdAsync(TKey key)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(key);

                if (entity == null) { return null; }

                var model = _mapper.Map(entity);

                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> HardDeleteAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RestoreAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<TModel?> UpdateAsync(TModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
