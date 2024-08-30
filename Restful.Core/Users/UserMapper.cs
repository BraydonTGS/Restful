using Restful.Core.Base;
using Restful.Core.Users.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Users
{
    public class UserMapper : BaseMapper<User, UserEntity>
    {

        #region Map to Model
        public override User Map(UserEntity entity)
        {
            var model = new User()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Username = entity.UserName,
                TempPassword = string.Empty, 
                IsDeleted = entity.IsDeleted,
                IsDirty = entity.IsDirty,
            };

            return model;
        }
        #endregion

        #region Map to Entity
        public override UserEntity Map(User model)
        {
            var entity = new UserEntity() 
            { 
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                IsDeleted =model.IsDeleted,
                IsDirty =model.IsDirty,
            };

            return entity;
        }
        #endregion
    }
}
