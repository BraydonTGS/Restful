using Restful.Core.Base;
using Restful.Core.Requests.Models;
using Restful.Entity.Entities;
using Serilog;

namespace Restful.Core.Requests
{
    public class RequestBL : BaseBL<Request, RequestEntity, Guid>, IRequestBL
    {
        private readonly IRequestRepository _requestRepository;
        public RequestBL(
            IRequestRepository requestRepository,
            IMapper<Request, RequestEntity> mapper,
            ILogger log) : base(requestRepository, mapper, log)
        {
            _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
        }
    }
}
