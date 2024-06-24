using System.Collections.ObjectModel;

namespace Restful.Core.Base
{
    /// <summary>
    /// Generic Base Mapper
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseMapper<TModel, TEntity> : IMapper<TModel, TEntity>
    {

        public abstract TEntity Map(TModel model);
        public abstract TModel Map(TEntity entity);

        #region Map to Collection of TModels
        /// <summary>
        /// Creates a Collection of TModels From an ICollection of TEntities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
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
        #endregion

        #region Map to Collection of TEntities
        /// <summary>
        /// Creates a Collection of TEntities from an ICollection of TModels
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
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
        #endregion

    }
}
