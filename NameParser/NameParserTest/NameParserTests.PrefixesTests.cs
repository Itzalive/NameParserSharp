using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]
    public class PrefixesTests
    {
        [TestMethod]
        public void TestPrefix()
        {
            var hn = new HumanName("Juan del Sur");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("del Sur", hn.Last);
        }

        [TestMethod]
        public void TestPrefixWithPeriod()
        {
            var hn = new HumanName("Jill St. John");
            Assert.AreEqual("Jill", hn.First);
            Assert.AreEqual("St. John", hn.Last);
        }

        [TestMethod]
        public void TestPrefixBeforeTwoPartLastName()
        {
            var hn = new HumanName("pennie von bergen wessels");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
        }

        [TestMethod]
        public void TestPrefixIsFirstName()
        {
            var hn = new HumanName("Van Johnson");
            Assert.AreEqual("Van", hn.First);
            Assert.AreEqual("Johnson", hn.Last);
        }

        [TestMethod]
        public void TestPrefixIsFirstNameWithMiddleName()
        {
            var hn = new HumanName("Van Jeremy Johnson");
            Assert.AreEqual("Van", hn.First);
            Assert.AreEqual("Jeremy", hn.Middle);
            Assert.AreEqual("Johnson", hn.Last);
        }

        [TestMethod]
        public void TestPrefixBeforeTwoPartLastNameWithSuffix()
        {
            var hn = new HumanName("pennie von bergen wessels III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void TestPrefixBeforeTwoPartLastNameWithAcronymSuffix()
        {
            var hn = new HumanName("pennie von bergen wessels M.D.");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("M.D.", hn.Suffix);
        }

        [TestMethod]
        public void TestTwoPartLastNameWithSuffixComma()
        {
            var hn = new HumanName("pennie von bergen wessels, III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void TestTwoPartLastNameWithSuffix()
        {
            var hn = new HumanName("von bergen wessels, pennie III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void TestLastNameTwoPartLastNameWithTwoSuffixes()
        {
            var hn = new HumanName("von bergen wessels MD, pennie III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("MD, III", hn.Suffix);
        }

        [TestMethod]
        public void TestCommaTwoPartLastNameWithAcronymSuffix()
        {
            var hn = new HumanName("von bergen wessels, pennie MD");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("MD", hn.Suffix);
        }

        [TestMethod]
        public void TestCommaTwoPartLastNameWithSuffixInFirstPart()
        {
            // I'm kinda surprised this works, not really sure if this is a
            // realistic place for a suffix to be.
            var hn = new HumanName("von bergen wessels MD, pennie");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("MD", hn.Suffix);
        }

        [TestMethod]
        public void TestTitleTwoPartLastNameWithSuffixInFirstPart()
        {
            var hn = new HumanName("pennie von bergen wessels MD, III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("MD, III", hn.Suffix);
        }

        [TestMethod]
        public void TestPortugueseDos()
        {
            var hn = new HumanName("Rafael Sousa dos Anjos");
            Assert.AreEqual("Rafael", hn.First);
            Assert.AreEqual("Sousa", hn.Middle);
            Assert.AreEqual("dos Anjos", hn.Last);
        }

        [TestMethod]
        public void TestPortuguesePrefixes()
        {
            var hn = new HumanName("Joao da Silva do Amaral de Souza");
            Assert.AreEqual("Joao", hn.First);
            Assert.AreEqual("da Silva do Amaral", hn.Middle);
            Assert.AreEqual("de Souza", hn.Last);
        }

        [TestMethod]
        public void TestThreeConjunctions()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier de la dos Vega III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la dos Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void TestLastnameThreeConjunctions()
        {
            var hn = new HumanName("de la dos Vega, Dr. Juan Q. Xavier III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la dos Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void TestCommaThreeConjunctions()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier de la dos Vega, III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la dos Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }
    }
}