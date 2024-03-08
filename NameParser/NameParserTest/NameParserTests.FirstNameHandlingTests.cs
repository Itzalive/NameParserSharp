using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]
    public class FirstNameHandlingTests
    {
        [TestMethod]
        public void TestFirstName()
        {
            var hn = new HumanName("Andrew");
            Assert.AreEqual("Andrew", hn.First);
        }

        [TestMethod]
        public void TestAssumeTitleAndOneOtherNameIsLastName()
        {
            var hn = new HumanName("Rev Andrews");
            Assert.AreEqual("Rev", hn.Title);
            Assert.AreEqual("Andrews", hn.Last);
        }

        // TODO: Seems "Andrews, M.D.", Andrews should be treated as a last name
        // but other suffixes like "George Jr." should be first names. Might be
        // related to https://github.com/derek73/python-nameparser/issues/2
        [Ignore("Expect failure")]
        [TestMethod]
        public void TestAssumeSuffixTitleAndOneOtherNameIsLastName()
        {
            var hn = new HumanName("Andrews, M.D.");
            Assert.AreEqual("M.D.", hn.Suffix);
            Assert.AreEqual("Andrews", hn.Last);
        }

        [TestMethod]
        public void TestSuffixInLastnamePartOfLastnameCommaFormat()
        {
            var hn = new HumanName("Smith Jr., John");
            Assert.AreEqual("Smith", hn.Last);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void TestSirExceptionToFirstNameRule()
        {
            var hn = new HumanName("Sir Gerald");
            Assert.AreEqual("Sir", hn.Title);
            Assert.AreEqual("Gerald", hn.First);
        }

        [TestMethod]
        public void TestKingExceptionToFirstNameRule()
        {
            var hn = new HumanName("King Henry");
            Assert.AreEqual("King", hn.Title);
            Assert.AreEqual("Henry", hn.First);
        }

        [TestMethod]
        public void TestQueenExceptionToFirstNameRule()
        {
            var hn = new HumanName("Queen Elizabeth");
            Assert.AreEqual("Queen", hn.Title);
            Assert.AreEqual("Elizabeth", hn.First);
        }

        [TestMethod]
        public void TestDameExceptionToFirstNameRule()
        {
            var hn = new HumanName("Dame Mary");
            Assert.AreEqual("Dame", hn.Title);
            Assert.AreEqual("Mary", hn.First);
        }

        [TestMethod]
        public void TestFirstNameIsNotPrefixIfOnlyTwoParts()
        {
            // When there are only two parts, don't join prefixes or conjunctions
            var hn = new HumanName("Van Nguyen");
            Assert.AreEqual("Van", hn.First);
            Assert.AreEqual("Nguyen", hn.Last);
        }

        [TestMethod]
        public void TestFirstNameIsNotPrefixIfOnlyTwoPartsComma()
        {
            var hn = new HumanName("Nguyen, Van");
            Assert.AreEqual("Van", hn.First);
            Assert.AreEqual("Nguyen", hn.Last);
        }

[Ignore("Expected failure")]
        [TestMethod]
        public void TestFirstNameIsPrefixIfThreeParts()
        {
            //"""Not sure how to fix this without breaking Mr and Mrs"""
            var hn = new HumanName("Mr. Van Nguyen");
            Assert.AreEqual("Van", hn.First);
            Assert.AreEqual("Nguyen", hn.Last);
        }
    }
}