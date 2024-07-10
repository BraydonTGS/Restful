using Restful.Core.Base;
using Restful.Core.Parameters.Models;
using Restful.Entity.Entities;
using Serilog;

namespace Restful.Core.Parameters
{
    public class ParameterBL : BaseBL<Parameter, ParameterEntity, Guid>, IParameterBL
    {
        public ParameterBL(
        IParameterRepository parameterRepository,
        IMapper<Parameter, ParameterEntity> mapper,
        ILogger log) : base(parameterRepository, mapper, log) { }
    }
}
