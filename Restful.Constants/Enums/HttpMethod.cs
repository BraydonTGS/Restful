using System.ComponentModel;

namespace Restful.Global.Enums
{
    public enum HttpMethod
    {
        [Description("GET")]
        GET,

        [Description("POST")]
        POST,

        [Description("PUT")]
        PUT,

        [Description("DELETE")]
        DELETE,

        [Description("PATCH")]
        PATCH
    }
}
