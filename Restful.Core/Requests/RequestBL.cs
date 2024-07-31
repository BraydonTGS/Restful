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
                                request.Headers?.Add(header);

                        // Params //
                        var parameters = await _parameterBL.GetAllParametersByRequestIdAsync(request.Id);
                        if (parameters.Count > 0)
                            foreach (var parameter in parameters)
                                request.Parameters?.Add(parameter);

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

        #region CreateAsync - Override
        public override async Task<Request?> CreateAsync(Request model)
        {
            _log.Information($"Starting CreateAsync for the Specified Request.");
            try
            {
                var entity = _mapper.Map(model);
                if (entity == null)
                {
                    _log.Warning($"Unable to Map Request to RequestEntity.");
                    return null;
                }

                var createdEntity = await _requestRepository.CreateAsync(entity);
                if (createdEntity == null)
                {
                    _log.Warning($"Failed to create RequestEntity in Database.");
                    return null;
                }

                var resultModel = _mapper.Map(createdEntity);
         
                _log.Information($"Check for Request Headers & Parameters and add to the Database.");
                if (resultModel.Headers != null)
                    foreach (var header in resultModel.Headers.Where(x => !x.IsDefault))
                    {
                        header.RequestId = resultModel.Id;
                        await _headerBL.CreateAsync(header);
                    }

                if (resultModel.Parameters != null)
                    foreach (var parameter in resultModel.Parameters)
                    {
                        parameter.RequestId = resultModel.Id;
                        await _parameterBL.CreateAsync(parameter);
                    }

                _log.Information($"Completed CreateAsync for the Specified Request. RequestEntity Creation and Mapping Successful.");
                return resultModel;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in CreateAsync for the Specified Request with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion
    }
}
