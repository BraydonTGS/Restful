using Restful.Core.Base;
using Restful.Core.Requests.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Requests
{
    /// <summary>
    /// Request Mapper - Responsible for Mapping/Reverse Mapping Request Model's to Request Entities 
    /// </summary>
    public class RequestMapper : BaseMapper<Request, RequestEntity>
    {
        public RequestMapper() { }

        #region Map to Model
        public override Request Map(RequestEntity entity)
        {
            var model = new Request(true)
            {
                Id = entity.Id,
                CollectionId = entity.CollectionId,
                UserId = entity.UserId,
                Name = entity.Name,
                Description = entity.Description,
                Url = entity.Url,
                HttpMethod = entity.HttpMethod,
                IsDeleted = entity.IsDeleted,
                IsDirty = entity.IsDirty
            };

            if (model.Body is not null)
                model.Body.Text = entity.Body;
            if (model.TempResult is not null)
                model.TempResult.Text = entity.TempResult;

            return model;
        }
        #endregion

        #region Map to Entity
        public override RequestEntity Map(Request model)
        {
            var entity = new RequestEntity
            {
                Id = model.Id,
                CollectionId = model.CollectionId,
                UserId = model.UserId,
                Name = model.Name,
                Description = model.Description,
                Url = model.Url,
                HttpMethod = model.HttpMethod,
                IsDeleted = model.IsDeleted,
                IsDirty = model.IsDirty
            };

            if (model.Body is not null)
                entity.Body = model.Body.Text;

            if (model.TempResult is not null)
                entity.TempResult = model.TempResult.Text;

            return entity;
        }
        #endregion
    }
}
