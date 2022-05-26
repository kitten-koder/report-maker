using System;
using System.Data;

namespace CostReportMaker.Database
{
    [Obsolete("Use the new Database.Database class instead.")]
    public interface IConnectionProvider
    {
        IDbConnection Provide();
    }
}