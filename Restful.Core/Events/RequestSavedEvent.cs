using Prism.Events;
using Restful.Core.Interfaces;
using Restful.Core.Models;

namespace Restful.Core.Events
{
    /// <summary>
    /// Request Saved Events
    /// </summary>
    public class RequestSavedEvent : PubSubEvent<IModel<Guid>> { }
}
