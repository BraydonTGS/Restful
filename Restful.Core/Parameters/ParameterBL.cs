using Restful.Core.Base;
using Restful.Core.Parameters.Models;
using Restful.Entity.Entities;
using Serilog;

namespace Restful.Core.Parameters
{
    public class ParameterBL : BaseBL<Parameter, ParameterEntity, Guid>, IParameterBL
    {
        public readonly IParameterRepository _parameterRepository;

        public ParameterBL(
        IParameterRepository parameterRepository,
        IMapper<Parameter, ParameterEntity> mapper,
        ILogger log) : base(parameterRepository, mapper, log)
        {
            _parameterRepository = parameterRepository ?? throw new ArgumentNullException(nameof(IParameterRepository));
        }


        #region GetAllParametersByRequestIdAsync
        /// <summary>
        /// Query Parameters from the ParameterRepository for the Specified Request Id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<ICollection<Parameter>> GetAllParametersByRequestIdAsync(Guid requestId)
        {
            _log.Information($"Starting GetAllParametersByRequestIdAsync");
            try
            {
                var entities = await _parameterRepository.GetAllParametersByRequestIdAsync(requestId);

                if (entities.Count is 0)
                {
                    _log.Warning($"No Parameters Found with the Specified Request Id");
                    return [];
                }

                var requests = _mapper.Map(entities);

                _log.Information($"Completed GetAllParametersByRequestIdAsync. Found the Parameters for the Specified Request Id");
                return requests;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetAllParametersByRequestIdAsync with Message {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
