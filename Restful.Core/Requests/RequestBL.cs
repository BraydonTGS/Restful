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

        public async Task<ICollection<Request>> GetAllRequestsByCollectionIdAsync(Guid collectionId)
        {
            var entities = await _requestRepository.GetAllRequestsByCollectionIdAsync(collectionId);

            var requests = _mapper.Map(entities);

            return requests;
        }

        public async Task<ICollection<Request>> GetAllRequestsByUserIdAsync(Guid userId)
        {
            var entities = await _requestRepository.GetAllRequestsByUserIdAsync(userId);

            var requests = _mapper.Map(entities);

            return requests;
        }
    }
}
