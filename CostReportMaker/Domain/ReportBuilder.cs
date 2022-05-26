namespace CostReportMaker.Domain
{
    public interface IReportBuilder
    {
        void AddFigure(ReportFigure figure);

        Report Build();

        void SetReportName(string reportName);
    }

    public class ReportBuilder : IReportBuilder
    {
        private Report _report = new Report();

        public ReportBuilder()
        {
            this.Reset();
        }

        private void Reset()
        {
            this._report = new Report();
        }

        public void SetReportName(string reportName)
        {
            _report.Name = reportName;
        }

        public void AddFigure(ReportFigure figure)
        {
            this._report.AddFigure(figure);
        }

        public Report Build()
        {
            return this._report;
        }
    }
}