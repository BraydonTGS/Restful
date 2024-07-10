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
        public HeaderBL(
        IHeaderRepository headerRepository,
        IMapper<Header, HeaderEntity> mapper,
        ILogger log) : base(headerRepository, mapper, log) { }
    }
}
