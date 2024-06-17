using Restful.Core.Base;
using Restful.Core.Requests.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Requests
{
    public class RequestMapper : BaseMapper<Request, RequestEntity>
    {
        public RequestMapper()
        {
            
        }
        public override Request Map(RequestEntity entity)
        {
            var request = new Request(true);

            request.Id = entity.Id;
            request.CollectionId = entity.CollectionId;
            request.Name = entity.Name;
            request.Url = entity.Url;
            request.Description = entity.Description;
            request.IsDirty = entity.IsDirty;
            request.IsDeleted = entity.IsDeleted;

            if (request.Body is not null)
                request.Body.Text = entity.Body;
            if (request.TempResult is not null)
                request.TempResult.Text = entity.TempResult;

            // Params //
            // Headers //
            // Response //
            return request;
        }
        public override RequestEntity Map(Request model)
        {
            throw new NotImplementedException();
        }
    }
}
