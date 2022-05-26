using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CostReportMaker.Domain.Tests
{
    [TestClass()]
    public class ReportBuilderTests
    {
        [TestMethod()]
        public void ReportBuilderTest()
        {
            IReportBuilder builder = new ReportBuilder();
            builder.SetReportName("Report Name");
            builder.AddFigure(new ReportFigure() { Title = "Figure 1", Type = FigureType.PlainNumber });
            builder.AddFigure(new ReportFigure() { Title = "Figure 2", Type = FigureType.PlainNumber });
            builder.AddFigure(new ReportFigure() { Title = "Figure 3", Type = FigureType.Formula });

            var report = builder.Build();

            Assert.IsTrue(report.Name == "Report Name");
            Assert.IsTrue(report.Figures.Count == 3);
            Assert.IsTrue(
                Enumerable.SequenceEqual(report.Figures.Select(f => f.Title).ToArray(),
                new string[] { "Figure 1", "Figure 2", "Figure 3" })
            );
        }

        [TestMethod()]
        public void AddFigureTest()
        {
            IReportBuilder builder = new ReportBuilder();
            builder.SetReportName("Report Name");

            var reportFigure = new ReportFigure() { Title = "Figure 1", Type = FigureType.PlainNumber };
            builder.AddFigure(reportFigure);

            var report = builder.Build();

            Assert.AreEqual(report.Figures.First(), reportFigure);
        }

        [TestMethod()]
        public void BuildTest()
        {
            IReportBuilder builder = new ReportBuilder();
            builder.SetReportName("Report Name");

            var report = builder.Build();

            Assert.AreEqual(report.GetType(), typeof(Report));
            Assert.AreEqual(report.Name, "Report Name");
        }
    }
}