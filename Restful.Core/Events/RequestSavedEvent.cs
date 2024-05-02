using Prism.Events;
using Restful.Core.Models;

namespace Restful.Core.Events
{
    public class RequestSavedEvent : PubSubEvent<IModel<Guid>> { }
}
