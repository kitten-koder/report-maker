using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CostReportMaker.Tests
{
    [TestCategory("Test Helpers")]
    [TestClass()]
    public class HelpersTests
    {
        [TestMethod()]
        public void OneTest()
        {
            var expectedValue = 1;
            var actualValue = Helpers.One();

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}