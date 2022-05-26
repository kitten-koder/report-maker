using CostReportMaker.Domain;
using Dapper.FluentMap.Mapping;

namespace CostReportMaker.Database.Mapping
{
    public class UserMap : EntityMap<User>
    {
        public UserMap()
        {
        }
    }
}