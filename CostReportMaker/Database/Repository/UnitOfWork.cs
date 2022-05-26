using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Threading.Tasks;

namespace CostReportMaker.Database.Repository
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(DatabaseConfig config)
        {
            if (config.DatabaseType == SupportedDatabases.SQLite)
                _connection = new SqliteConnection(config.ConnectionString);
            else if (config.DatabaseType == SupportedDatabases.SQLServer)
                _connection = new SqlConnection(config.ConnectionString);
            else
                throw new ArgumentException("Database is not support");

            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public IDbTransaction GetTransaction()
        {
            return _transaction;
        }
    }
}