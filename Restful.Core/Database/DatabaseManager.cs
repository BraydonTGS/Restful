using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;
using Restful.Global.Exceptions;
using Serilog;
using System.IO;

namespace Restful.Core.Database
{
    /// <summary>
    /// Database Manager for the Application - SQL Lite
    /// </summary>
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        private readonly ILogger _log;

        public DatabaseManager(
            IDbContextFactory<RestfulDbContext> contextFactory,
            ILogger log)
        {
            _contextFactory = contextFactory;
            _log = log;
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
            _log.Information($"Starting EnsureDatabaseDirectoryExists");
            try
            {
                _log.Information($"Get the Directory Name and Ensure It Exists");
                var directoryPath = Path.GetDirectoryName(DatabaseInfo.DbDirectory);
                if (!string.IsNullOrEmpty(directoryPath))
                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                _log.Information("Successfully Ensured the Database Directory Exists");
            }
            catch (Exception ex) 
            {
                _log.Error($"Error Ensuring the Database Directory Exists with Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region EnsureDatabaseCreated
        /// <summary>
        /// Ensure the Database is Created Successfully and Apply Migrations if Needed
        /// </summary>
        private void EnsureDatabaseCreated()
        {
            _log.Information("Starting EnsureDatabaseCreated");
            try
            {
                _log.Information("Ensure the Database is Created - Create the DB Context");
                using var context = _contextFactory.CreateDbContext();

                _log.Information($"Context has been Established, Created Database and Apply Migrations");
                context.Database.Migrate();

                _log.Information($"Ensure Database Created Success and Migrations Applied");
            }
            catch (Exception ex)
            {
                _log.Error($"Error Ensuring the Database Exists and Applying Migrations with Message: {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
