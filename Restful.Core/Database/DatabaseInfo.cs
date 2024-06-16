using System.IO;

namespace Restful.Core.Database
{
    internal static class DatabaseInfo
    {

        public static readonly string ProdDbDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Restful\\";

        public static readonly string TestDbDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RestfulTest\\";

        public static readonly string ProdDb = $"{ProdDbDirectory}RestfulProd.db";

        public static readonly string TestDb = $"{TestDbDirectory}RestfulTest.db";

        public static readonly string ProdDbConnection = $"Data Source={DatabaseInfo.ProdDb}";

        public static readonly string TestDbConnection = $"Data Source={DatabaseInfo.TestDb}";
    }
}
