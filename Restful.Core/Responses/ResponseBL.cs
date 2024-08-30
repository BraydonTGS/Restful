using Restful.Core.Base;
using Restful.Core.Responses.Models;
using Restful.Entity.Entities;
using Serilog;

namespace Restful.Core.Responses
{
    public class ResponseBL : BaseBL<Response, ResponseEntity, Guid>, IResponseBL
    {
        public ResponseBL(
            IResponseRepository responseRepository,
            IMapper<Response, ResponseEntity> mapper,
            ILogger log) : base(responseRepository, mapper, log) { }
    }
}
