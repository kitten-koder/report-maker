namespace CostReportMaker.Database
{
    public interface IDatabaseTable
    {
        string CreateSQL();

        string GetName();
    }
}