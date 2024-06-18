using Restful.Core.Base;
using Restful.Core.Requests.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Requests
{
    public class RequestMapper : BaseMapper<Request, RequestEntity>
    {
        public RequestMapper() { }
        public override Request Map(RequestEntity entity)
        {
            var model = new Request(true);

            return model;
        }
        public override RequestEntity Map(Request model)
        {
            var entity = new RequestEntity();

            return entity;
        }
    }
}
