using System;
using System.Collections.Generic;
using System.Linq;

namespace CostReportMaker.Domain
{
    public class Report
    {
        public string Name { get; set; }
        public TimeSpan ReportSpan { get; set; }
        public DateTime ReportDate { get; set; }
        public ISet<ReportFigure> Figures { get; private set; }
        public IList<ReportLine> Lines { get; private set; }

        public Report()
        {
            Figures = new HashSet<ReportFigure>();
            Lines = new List<ReportLine>();
        }

        public void AddFigure(ReportFigure figure)
        {
            if (!this.Figures.Any(f => f.Title == figure.Title))
            {
                this.Figures.Add(figure);
            }
        }
    }
}