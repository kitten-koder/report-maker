using CostReportMaker.Domain;
using System.Threading.Tasks;

namespace CostReportMaker.Database.Repository
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
    }
}