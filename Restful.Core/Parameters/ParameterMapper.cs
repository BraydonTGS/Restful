﻿using Restful.Core.Base;
using Restful.Core.Parameters.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Parameters
{
    /// <summary>
    /// Parameter Mapper - Responsible for Mapping/Reverse Mapping Parameter Model's to Parameter Entities 
    /// </summary>
    public class ParameterMapper : BaseMapper<Parameter, ParameterEntity>
    {
        #region Map to Model 
        public override Parameter Map(ParameterEntity entity)
        {
            var model = new Parameter()
            {
                Id = entity.Id,
                Key = entity.Key,
                Value = entity.Value,
                Enabled = entity.Enabled,
                RequestId = entity.RequestId,
                Description = entity.Description,
                IsDeleted = entity.IsDeleted,
            };

            return model;
        }
        #endregion

        #region Map to Entity
        public override ParameterEntity Map(Parameter model)
        {
            var entity = new ParameterEntity()
            {
                Id = model.Id,
                Key = model.Key,
                Value = model.Value,
                Enabled = model.Enabled,
                RequestId = model.RequestId,
                Description = model.Description,
                IsDeleted= model.IsDeleted,
            };

            return entity;
        }
        #endregion
    }
}
