namespace Restful.Core.Database
{
    public interface IDatabaseManager
    {
        void InitializeDatabase(bool isProd, bool addMigration);
    }
}