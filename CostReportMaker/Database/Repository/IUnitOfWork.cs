using System.Data;
using System.Threading.Tasks;

namespace CostReportMaker.Database.Repository
{
    public interface IUnitOfWork
    {
        Task CommitAsync();

        IDbTransaction GetTransaction();
    }
}