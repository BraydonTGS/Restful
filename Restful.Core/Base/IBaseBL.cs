using Restful.Entity.Base;

namespace Restful.Core.Base
{
    /// <summary>
    /// Generic Interface for defining basic CRUD operations for Models
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseBL<TModel, TEntity, TKey> where TModel : ModelBase<TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TModel>?> GetAllAsync();
        Task<TModel?> GetByIdAsync(TKey key);
        Task<TModel?> CreateAsync(TModel dto);
        Task<TModel?> UpdateAsync(TModel dto);
        Task<bool> SoftDeleteAsync(TKey key);
        Task<bool> HardDeleteAsync(TKey key);
        Task<bool> RestoreAsync(TKey key);
    }
}
