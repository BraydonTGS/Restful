using Restful.Core.Base;
using Restful.Core.Passwords.Model;
using Restful.Entity.Entities;


namespace Restful.Core.Passwords
{
    /// <summary>
    /// Password Mapper - Responsible for Mapping/Reverse Mapping Password Model's to Password Entities 
    /// </summary>
    public class PasswordMapper : BaseMapper<Password, PasswordEntity>
    {

        #region Map to Model
        public override Password Map(PasswordEntity entity)
        {
            var model = new Password()
            {
                Id = entity.Id,
                Hash = entity.Hash,
                Salt = entity.Salt,
                UserId = entity.UserId,
            };

            return model;
        }
        #endregion

        #region Map to Entity

        public override PasswordEntity Map(Password model)
        {
            var entity = new PasswordEntity()
            { 
                Id = model.Id,
                Hash = model.Hash,
                Salt = model.Salt,
                UserId = model.UserId,
            };

            return entity;

        }
        #endregion
    }
}
