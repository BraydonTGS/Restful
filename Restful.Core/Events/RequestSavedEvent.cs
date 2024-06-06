using Prism.Events;
using Restful.Core.Base;

namespace Restful.Core.Events
{
    /// <summary>
    /// Request Saved Events
    /// </summary>
    public class RequestSavedEvent : PubSubEvent<IModel<Guid>> { }
}
