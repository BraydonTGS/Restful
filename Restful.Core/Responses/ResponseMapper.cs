using Restful.Core.Base;
using Restful.Core.Responses.Models;
using Restful.Entity.Entities;
using System.Drawing;

namespace Restful.Core.Responses
{
    /// <summary>
    /// Response Mapper - Responsible for Mapping/Reverse Mapping Response Model's to Response Entities 
    /// </summary>
    public class ResponseMapper : BaseMapper<Response, ResponseEntity>
    {
        #region Map to Model 
        public override Response Map(ResponseEntity entity)
        {
            var model = new Response()
            {
                Id = entity.Id,
                Result = entity.Result,
                RequestId = entity.RequestId,
                Description = entity.Description,
                IsDeleted = entity.IsDeleted,
            };

            return model;
        }
        #endregion

        #region Map to Entity
        public override ResponseEntity Map(Response model)
        {
            var entity = new ResponseEntity()
            {
                Id = model.Id,
                Result = model.Result,
                RequestId = model.RequestId,
                Description = model.Description,
                IsDeleted = model.IsDeleted,
            };

            return entity;
        }
        #endregion
    }
}
