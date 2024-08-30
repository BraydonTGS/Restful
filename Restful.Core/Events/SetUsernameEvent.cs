using Prism.Events;

namespace Restful.Core.Events
{
    /// <summary>
    /// Event that is Fired when the User is Logged in and we need to set the application username
    /// </summary>
    public class SetUsernameEvent : PubSubEvent { }
}
