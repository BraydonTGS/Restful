using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;
using Restful.Global.Exceptions;
using System.IO;

namespace Restful.Core.Database
{
    /// <summary>
    /// Database Manager for the Application - SQL Lite
    /// </summary>
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;

        public DatabaseManager(IDbContextFactory<RestfulDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region InitializeDatabase
        /// <summary>
        /// Initialize the Application Database - Creates the SQLite File's if they are not found.
        /// 
        /// Applies Migrations if Migrations need to be applied. 
        /// 
        /// Checks for Test or Prod Env
        /// 
        /// </summary>
        /// <param name="isProd"></param>
        /// <param name="applyMigrations"></param>
        /// <exception cref="InitializeDatabaseException"></exception>
        public void InitializeDatabase()
        {
            try
            {
                EnsureDatabaseDirectoryExists();
                EnsureDatabaseCreated();

            }
            catch (Exception ex)
            {
                throw new InitializeDatabaseException($"Error Initializing Application Database with Message: {ex.Message}");
            }
        }
        #endregion

        #region EnsureDatabaseDirectoryExists
        /// <summary>
        /// Ensure the Database Directory Exists
        /// </summary>
        private void EnsureDatabaseDirectoryExists()
        {
            var directoryPath = Path.GetDirectoryName(DatabaseInfo.DbDirectory);
            if (!string.IsNullOrEmpty(directoryPath))
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
        }
        #endregion

        #region EnsureDatabaseCreated
        /// <summary>
        /// Ensure the Database is Created Successfully and Apply Migrations if Needed
        /// </summary>
        private void EnsureDatabaseCreated()
        {
            using var context = _contextFactory.CreateDbContext();

            context.Database.Migrate();
        }
        #endregion
    }
}
