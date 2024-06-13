namespace Restful.Core.Connection
{
    public class Hidden
    {
        public static string GetConnectionString(bool isProd = true)
        {
            if (isProd) return "Data Source=Restful.db";

            else return "Data Source=RestfulTest.db";
        }
    }
}