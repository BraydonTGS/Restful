using Microsoft.EntityFrameworkCore;
using Restful.Core.Context;
using Restful.Global.Constant;
using Restful.Global.Exceptions;
using System.IO;

namespace Restful.Core.Database
{
    /// <summary>
    /// Database Manager for the Application - SQL Lite
    /// </summary>
    public class DatabaseManager : IDatabaseManager
    {
        private bool _isProd;
        private bool _applyMigrations;
        private string _databaseName = string.Empty;
        private string _databasePath = string.Empty;
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
        public void InitializeDatabase(bool isProd, bool applyMigrations)
        {
            try
            {
                _applyMigrations = applyMigrations;

                if (isProd) { _databaseName = Constants.ProdDb; } else { _databaseName = Constants.TestDb; }
                _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.DbDirectory, Constants.TestDb);

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
            var directoryPath = Path.GetDirectoryName(_databasePath);
            if (!string.IsNullOrEmpty(directoryPath))
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
        }
        #endregion

        #region EnsureDatabaseCreated
        /// <summary>
        /// Ensure the Database is Created Successfully and Apply Migrations if Needed
        /// </summary>
        private void EnsureDatabaseCreated()
        {

            if (!File.Exists(_databasePath))
            {
                using var context = _contextFactory.CreateDbContext();
                context.Database.EnsureCreated();
            }
            else
            {
                if (_applyMigrations)
                {
                    using var context = _contextFactory.CreateDbContext();
                    context.Database.Migrate();
                }
            }
        }
        #endregion
    }
}
