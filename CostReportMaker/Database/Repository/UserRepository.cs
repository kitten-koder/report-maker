using CostReportMaker.Domain;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CostReportMaker.Database.Repository
{
    public class UserRepository : IUserProvider, IUserRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;

        public UserRepository(IUnitOfWork uow)
        {
            _transaction = uow.GetTransaction();
            _connection = _transaction.Connection;
        }

        public async Task CreateUserAsync(User user)
        {
            var sql = "INSERT INTO User (Username, Password) VALUES (@Username, @Password)";
            await _connection.ExecuteAsync(sql, user, _transaction);
        }

        public async Task<User> GetAsync<T>(T id)
        {
            var sql = "SELECT rowid AS Id, Username, Password FROM User WHERE rowid = @Id;";
            return await _connection.QueryFirstAsync<User>(sql, new { Id = id });
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var sql = "SELECT rowid AS Id, Username, Password FROM User WHERE Username = @Username;";
            var result = await _connection.QueryFirstAsync<User>(sql, new { Username = username });
            return result;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var sql = "SELECT rowid AS Id, Username, Password FROM User WHERE Email = @Email;";
            var result = await _connection.QueryFirstAsync<User>(sql, new { Email = email });
            return result;
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var sql = "SELECT rowid AS Id, Username, Password FROM User;";
            var result = await _connection.QueryAsync<User>(sql);
            return result.ToList();
        }
    }
}