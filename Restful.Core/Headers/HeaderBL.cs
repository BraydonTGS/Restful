using Restful.Core.Base;
using Restful.Core.Headers.Models;
using Restful.Entity.Entities;
using Serilog;


namespace Restful.Core.Headers
{
    /// <summary>
    /// Header BL
    /// </summary>
    public class HeaderBL : BaseBL<Header, HeaderEntity, Guid>, IHeaderBL
    {
        public readonly IHeaderRepository _headerRepository;
        public HeaderBL(
        IHeaderRepository headerRepository,
        IMapper<Header, HeaderEntity> mapper,
        ILogger log) : base(headerRepository, mapper, log)
        {
            _headerRepository = headerRepository ?? throw new ArgumentNullException(nameof(IHeaderRepository));
        }

        #region GetAllHeadersByRequestIdAsync
        /// <summary>
        /// Query Headers from the HeaderRepository for the Specified Request Id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<ICollection<Header>> GetAllHeadersByRequestIdAsync(Guid requestId)
        {
            _log.Information($"Starting GetAllHeadersByRequestIdAsync");
            try
            {
                var entities = await _headerRepository.GetAllHeadersByRequestIdAsync(requestId);

                if (entities.Count is 0)
                {
                    _log.Warning($"No Headers Found with the Specified Request Id");
                    return [];
                }

                var requests = _mapper.Map(entities);

                _log.Information($"Completed GetAllHeadersByRequestIdAsync. Found the Headers for the Specified Request Id");
                return requests;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetAllHeadersByRequestIdAsync with Message {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
