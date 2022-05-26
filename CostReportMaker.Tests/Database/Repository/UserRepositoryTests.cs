using CostReportMaker.Domain;
using CostReportMaker.Tests;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace CostReportMaker.Database.Repository.Tests
{
    [TestClass()]
    public class UserRepositoryTests : DatabaseTestWithSetup
    {
        [TestCategory("Creating User")]
        [TestMethod()]
        public async Task CreateUserTest()
        {
            var user = new User
            {
                Username = "admin",
                Password = "test",
            };

            using (var unitOfWork = new UnitOfWork(Utils.GetTestDatabaseConfig()))
            {
                var userRepository = new UserRepository(unitOfWork);
                await userRepository.CreateUserAsync(user);
                await unitOfWork.CommitAsync();
            }

            var connection = new SqliteConnection(Utils.GetTestDatabaseConfig().ConnectionString);
            var sql = "SELECT * FROM User WHERE username = 'admin'";
            var result = connection.Query<User>(sql).FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(user.Username, result.Username);
        }

        [TestCategory("Creating User")]
        [TestMethod()]
        public async Task CreateUserTestFailWithoutCommit()
        {
            using (var unitOfWork = new UnitOfWork(Utils.GetTestDatabaseConfig()))
            {
                var userRepository = new UserRepository(unitOfWork);
                var user = new User
                {
                    Username = "admin",
                    Password = "test",
                };
                await userRepository.CreateUserAsync(user);

                // no commit, let it fail
            }

            var connection = new SqliteConnection(Utils.GetTestDatabaseConfig().ConnectionString);
            var sql = "SELECT * FROM User WHERE username = 'admin'";
            var result = connection.Query<User>(sql).FirstOrDefault();

            Assert.IsNull(result);
        }

        [TestCategory("Get User")]
        [TestMethod()]
        public void GetByUsernameAsyncTest()
        {
            var connection = new SqliteConnection(Utils.GetTestDatabaseConfig().ConnectionString);
            var sql = "INSERT INTO User (username, password) VALUES ('admin', 'test')";
            connection.Execute(sql);

            using (var unitOfWork = new UnitOfWork(Utils.GetTestDatabaseConfig()))
            {
                IUserProvider userRepository = new UserRepository(unitOfWork);
                var getTask = userRepository.GetByUsernameAsync("admin");
                var user = getTask.Result;

                Assert.IsNotNull(user);
                Assert.AreEqual("admin", user.Username);
            }
        }

        [TestCategory("Get User")]
        [TestMethod()]
        public void GetAllAsyncTest()
        {
            var connection = new SqliteConnection(Utils.GetTestDatabaseConfig().ConnectionString);
            var sql = "INSERT INTO User (username, password) VALUES ('admin', 'test');" +
                "INSERT INTO User (username, password) VALUES ('admin2', 'test')";

            connection.Execute(sql);

            using (var unitOfWork = new UnitOfWork(Utils.GetTestDatabaseConfig()))
            {
                IUserProvider userRepository = new UserRepository(unitOfWork);
                var getTask = userRepository.GetAllAsync();
                var users = getTask.Result;

                Assert.IsNotNull(users);
                Assert.AreEqual(2, users.Count());
            }
        }
    }
}