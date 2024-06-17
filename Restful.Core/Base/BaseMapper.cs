using System.Collections.ObjectModel;

namespace Restful.Core.Base
{
    public abstract class BaseMapper<TModel, TEntity> : IMapper<TModel, TEntity>
    {

        public abstract TEntity Map(TModel model);
        public abstract TModel Map(TEntity entity);
        public virtual ICollection<TModel> Map(ICollection<TEntity> entities)
        {
           var models = new ObservableCollection<TModel>();

            foreach (var entity in entities)
            {
                var model = Map(entity);
                models.Add(model);
            }
            return models;
        }

        public ICollection<TEntity> Map(ICollection<TModel> models)
        {
            var entities = new List<TEntity>();

            foreach (var model in models)
            {
                var entity = Map(model);
                entities.Add(entity);
            }
            return entities;
        }

    }
}
