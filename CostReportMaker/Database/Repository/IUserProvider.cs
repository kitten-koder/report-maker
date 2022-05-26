using CostReportMaker.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostReportMaker.Database.Repository
{
    public interface IUserProvider
    {
        Task<IReadOnlyList<User>> GetAllAsync();

        Task<User> GetAsync<T>(T id);

        Task<User> GetByUsernameAsync(string username);

        Task<User> GetByEmailAsync(string email);
    }
}