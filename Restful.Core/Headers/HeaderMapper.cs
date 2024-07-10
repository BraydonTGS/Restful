using Restful.Core.Base;
using Restful.Core.Headers.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Headers
{
    /// <summary>
    /// Header Mapper - Responsible for Mapping/Reverse Mapping Header Model's to Header Entities 
    /// </summary>
    public class HeaderMapper : BaseMapper<Header, HeaderEntity>
    {
        #region Map to Model 
        public override Header Map(HeaderEntity entity)
        {
            var model = new Header()
            {
                Id = entity.Id,
                Key = entity.Key,
                Value = entity.Value,
                Enabled = entity.Enabled,
                RequestId = entity.RequestId,
                Description = entity.Description,
            };

            return model;
        }
        #endregion

        #region Map to Entity
        public override HeaderEntity Map(Header model)
        {
            var entity = new HeaderEntity()
            {
                Id = model.Id,
                Key = model.Key,
                Value = model.Value,
                Enabled = model.Enabled,
                RequestId = model.RequestId,
                Description = model.Description,
            };

            return entity;
        }
        #endregion
    }
}
