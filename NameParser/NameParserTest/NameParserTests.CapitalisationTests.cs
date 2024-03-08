using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]
    public class CapitalizationTests
    {
        [TestMethod]
        public void TestCapitalizationExceptionForIii()
        {
            var hn = new HumanName("juan q. xavier velasquez y garcia iii");
            hn.Normalize();
            Assert.AreEqual("Juan Q. Xavier Velasquez y Garcia III", hn.ToString());
        }

        // FIXME: this test does not pass due to a known issue
        // http://code.google.com/p/python-nameparser/issues/detail?id=22
        [Ignore("Expected failure")]
        [TestMethod]
        public void TestCapitalizationExceptionForAlreadyCapitalizedIiiknownfailure()
        {
            var hn = new HumanName("juan garcia III");
            hn.Normalize();
            Assert.AreEqual("Juan Garcia III", hn.ToString());
        }

        [TestMethod]
        public void TestCapitalizeTitle()
        {
            var hn = new HumanName("lt. gen. john a. kenneth doe iv");
            hn.Normalize();
            Assert.AreEqual("Lt. Gen. John A. Kenneth Doe IV", hn.ToString());
        }

        [TestMethod]
        public void TestCapitalizeTitleToLower()
        {
            var hn = new HumanName("LT. GEN. JOHN A. KENNETH DOE IV");
            hn.Normalize();
            Assert.AreEqual("Lt. Gen. John A. Kenneth Doe IV", hn.ToString());
        }

        // Capitalization with M(a)c and hyphenated names
        [TestMethod]
        public void TestCapitalizationWithMacAsHyphenatedNames()
        {
            var hn = new HumanName("donovan mcnabb-smith");
            hn.Normalize();
            Assert.AreEqual("Donovan McNabb-Smith", hn.ToString());
        }

        [TestMethod]
        public void TestCapitizationMiddleInitialIsAlsoAConjunction()
        {
            var hn = new HumanName("scott e. werner");
            hn.Normalize();
            Assert.AreEqual("Scott E. Werner", hn.ToString());
        }

        // Leaving already-capitalized names alone
        [TestMethod]
        public void TestNoChangeToMixedChase()
        {
            var hn = new HumanName("Shirley Maclaine");
            hn.Normalize();
            Assert.AreEqual("Shirley Maclaine", hn.ToString());
        }

        [TestMethod]
        public void TestForceCapitalization()
        {
            var hn = new HumanName("Shirley Maclaine");
            hn.Normalize(true);
            Assert.AreEqual("Shirley MacLaine", hn.ToString());
        }

        [TestMethod]
        public void TestCapitalizeDiacritics()
        {
            var hn = new HumanName("matthëus schmidt");
            hn.Normalize();
            Assert.AreEqual("Matthëus Schmidt", hn.ToString());
        }

        // http://code.google.com/p/python-nameparser/issues/detail?id=15
        [TestMethod]
        public void TestDowncasingMac()
        {
            var hn = new HumanName("RONALD MACDONALD");
            hn.Normalize();
            Assert.AreEqual("Ronald MacDonald", hn.ToString());
        }

        // http://code.google.com/p/python-nameparser/issues/detail?id=23
        [TestMethod]
        public void TestDowncasingMc()
        {
            var hn = new HumanName("RONALD MCDONALD");
            hn.Normalize();
            Assert.AreEqual("Ronald McDonald", hn.ToString());
        }

        [TestMethod]
        public void TestShortNamesWithMac()
        {
            var hn = new HumanName("mack johnson");
            hn.Normalize();
            Assert.AreEqual("Mack Johnson", hn.ToString());
        }

        [TestMethod]
        public void TestPortuguesePrefixes()
        {
            var hn = new HumanName("joao da silva do amaral de souza");
            hn.Normalize();
            Assert.AreEqual("Joao da Silva do Amaral de Souza", hn.ToString());
        }

        [TestMethod]
        public void TestCapitalizePrefixClashOnFirstName()
        {
            var hn = new HumanName("van nguyen");
            hn.Normalize();
            Assert.AreEqual("Van Nguyen", hn.ToString());
        }
    }
}