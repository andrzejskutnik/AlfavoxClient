using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AlfavoxClient.tests
{
    [TestClass]
    public class UnitTestService
    {
        [TestMethod]
        [DataRow("1,2")]
        public void TestParseModel(string testData)
        {
            //
            var testString = testData;
            //
            var result = testData.ParseModel();
            //
            Assert.AreEqual(result.Count(), 2);
        }

        [TestMethod]
        [DataRow("1,2")]
        public void TestParseModelValue(string testData)
        {
            //
            var testString = testData;
            //
            var result = testData.ParseModel();
            //
            Assert.AreEqual(result.First(), 1);
        }
    }
}