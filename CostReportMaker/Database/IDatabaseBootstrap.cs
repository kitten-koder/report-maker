namespace CostReportMaker.Database
{
    public interface IDatabaseBootstrap
    {
        IBootstrapper Bootstrapper { get; }

        void Setup();
    }
}