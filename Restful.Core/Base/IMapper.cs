namespace Restful.Core.Base
{
    public interface IMapper<TModel, TEntity>
    {
        public TModel Map(TEntity entity);
        public ICollection<TModel> Map(ICollection<TEntity> entities);
        public TEntity Map(TModel model);
        public ICollection<TEntity> Map(ICollection<TModel> models);
    }
}