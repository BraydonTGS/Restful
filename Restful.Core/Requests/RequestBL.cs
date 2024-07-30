using Restful.Core.Base;
using Restful.Core.Headers;
using Restful.Core.Parameters;
using Restful.Core.Requests.Models;
using Restful.Entity.Entities;
using Serilog;

namespace Restful.Core.Requests
{
    /// <summary>
    /// Request BL
    /// </summary>
    public class RequestBL : BaseBL<Request, RequestEntity, Guid>, IRequestBL
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IHeaderBL _headerBL;
        private readonly IParameterBL _parameterBL;
        public RequestBL(
            IRequestRepository requestRepository,
            IHeaderBL headerBL,
            IParameterBL parameterBL,
            IMapper<Request, RequestEntity> mapper,
            ILogger log) : base(requestRepository, mapper, log)
        {
            _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(IRequestRepository));
            _headerBL = headerBL ?? throw new ArgumentNullException(nameof(IHeaderBL));
            _parameterBL = parameterBL ?? throw new ArgumentNullException(nameof(IParameterBL));
        }

        #region GetAllRequestsByCollectionIdAsync
        /// <summary>
        /// Query Requests from the RequestRepository for the Specified Collection Id
        /// </summary>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        public async Task<ICollection<Request>> GetAllRequestsByCollectionIdAsync(Guid collectionId)
        {
            _log.Information($"Starting GetAllRequestsByCollectionIdAsync");
            try
            {
                var entities = await _requestRepository.GetAllRequestsByCollectionIdAsync(collectionId);

                if (entities.Count is 0)
                {
                    _log.Warning($"No Requests Found with the Specified Collection Id");
                    return [];
                }

                var requests = _mapper.Map(entities);

                _log.Information($"Completed GetAllRequestsByCollectionIdAsync. Found the Requests for the Specified Collection Id");
                return requests;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetAllRequestsByCollectionIdAsync with Message {ex.Message}");
                throw;
            }
        }
        #endregion

        #region GetAllRequestsByUserIdAsync
        /// <summary>
        /// Query Requests from the RequestRepository for the Specified User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ICollection<Request>> GetAllRequestsByUserIdAsync(Guid userId)
        {
            _log.Information($"Starting GetAllRequestsByUserIdAsync");
            try
            {
                var entities = await _requestRepository.GetAllRequestsByUserIdAsync(userId);

                if (entities.Count is 0)
                {
                    _log.Warning($"No Requests Found with the Specified User Id");
                    return [];
                }

                var requests = _mapper.Map(entities);

                _log.Information($"Completed GetAllRequestsByUserIdAsync. Found the Requests for the Specified User Id");
                return requests;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetAllRequestsByUserIdAsync with Message {ex.Message}");
                throw;
            }
        }
        #endregion


        #region GetAllRequestsByUserIdIncludeHeadersAndParametersAsync
        /// <summary>
        /// Query Requests from the RequestRepository for the Specified User Id
        /// 
        /// Include Headers and Parameters for the Specified Request
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ICollection<Request>> GetAllRequestsByUserIdIncludeHeadersAndParametersAsync(Guid userId)
        {
            _log.Information($"Starting GetAllRequestsByUserIdIncludeHeadersAndParametersAsync");
            try
            {
                var requests = await GetAllRequestsByUserIdAsync(userId);

                if (requests.Count > 0)
                    foreach (var request in requests)
                    {
                        // Headers //
                        var headers = await _headerBL.GetAllHeadersByRequestIdAsync(request.Id);
                        if (headers.Count > 0)
                            foreach (var header in headers)
                                request?.Headers?.Add(header);

                        // Params //
                    }

                return requests;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetAllRequestsByUserIdAsync with Message {ex.Message}");
                throw;
            }
        }
        #endregion

    }
}
