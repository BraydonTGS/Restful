namespace Restful.Core.Connection
{
    public class Hidden
    {
        public static string GetConnectionString(bool isProd = true)
        {
            if (isProd) return "Data Source=./Database/RestfulProd.db";

            else return "Data Source=./Database/RestfulTest.db";
        }
    }
}