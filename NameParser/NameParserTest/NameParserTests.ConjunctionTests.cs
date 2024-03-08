using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]
    public class ConjunctionTests
    {
        // Last name with conjunction
        [TestMethod]
        public void TestLastNameWithConjunction()
        {
            var hn = new HumanName("Jose Aznar y Lopez");
            Assert.AreEqual("Jose", hn.First);
            Assert.AreEqual("Aznar y Lopez", hn.Last);
        }

        [TestMethod]
        public void TestMultipleConjunctions()
        {
            var hn = new HumanName("part1 of The part2 of the part3 and part4");
            Assert.AreEqual("part1 of The part2 of the part3 and part4", hn.First);
        }

        [TestMethod]
        public void TestMultipleConjunctions2()
        {
            var hn = new HumanName("part1 of and The part2 of the part3 And part4");
            Assert.AreEqual("part1 of and The part2 of the part3 And part4", hn.First);
        }

        [TestMethod]
        public void TestEndsWithConjunction()
        {
            var hn = new HumanName("Jon Dough and");
            Assert.AreEqual("Jon", hn.First);
            Assert.AreEqual("Dough and", hn.Last);
        }

        [TestMethod]
        public void TestEndsWithTwoConjunctions()
        {
            var hn = new HumanName("Jon Dough and of");
            Assert.AreEqual("Jon", hn.First);
            Assert.AreEqual("Dough and of", hn.Last);
        }

        [TestMethod]
        public void TestStartsWithConjunction()
        {
            var hn = new HumanName("and Jon Dough");
            Assert.AreEqual("and Jon", hn.First);
            Assert.AreEqual("Dough", hn.Last);
        }

        [TestMethod]
        public void TestStartsWithTwoConjunctions()
        {
            var hn = new HumanName("the and Jon Dough");
            Assert.AreEqual("the and Jon", hn.First);
            Assert.AreEqual("Dough", hn.Last);
        }

        // Potential conjunction/prefix treated as initial (because uppercase);
        [TestMethod]
        public void TestUppercaseMiddleInitialConflictWithConjunction()
        {
            var hn = new HumanName("John E Smith");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("E", hn.Middle);
            Assert.AreEqual("Smith", hn.Last);
        }

        [TestMethod]
        public void TestLowercaseMiddleInitialWithPeriodConflictWithConjunction()
        {
            var hn = new HumanName("john e. smith");
            Assert.AreEqual("john", hn.First);
            Assert.AreEqual("e.", hn.Middle);
            Assert.AreEqual("smith", hn.Last);
        }

        // The conjunction "e" can also be an initial
        [TestMethod]
        public void TestLowercaseFirstInitialConflictWithConjunction()
        {
            var hn = new HumanName("e j smith");
            Assert.AreEqual("e", hn.First);
            Assert.AreEqual("j", hn.Middle);
            Assert.AreEqual("smith", hn.Last);
        }

        [TestMethod]
        public void TestLowercaseMiddleInitialConflictWithConjunction()
        {
            var hn = new HumanName("John e Smith");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("e", hn.Middle);
            Assert.AreEqual("Smith", hn.Last);
        }

        [TestMethod]
        public void TestLowercaseMiddleInitialAndSuffixConflictWithConjunction()
        {
            var hn = new HumanName("John e Smith, III");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("e", hn.Middle);
            Assert.AreEqual("Smith", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void TestLowercaseMiddleInitialAndNoCommaSuffixConflictWithConjunction()
        {
            var hn = new HumanName("John e Smith III");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("e", hn.Middle);
            Assert.AreEqual("Smith", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void TestLowercaseMiddleInitialCommaLastnameAndSuffixConflictWithConjunction()
        {
            var hn = new HumanName("Smith, John e, III, Jr");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("e", hn.Middle);
            Assert.AreEqual("Smith", hn.Last);
            Assert.AreEqual("III, Jr", hn.Suffix);
        }

            [Ignore("Expected failure")]
        [TestMethod]
        public void TestTwoInitialsConflictWithConjunction()
        {
            // Supporting this seems to screw up titles with periods in them like M.B.A.
            var hn = new HumanName("E.T. Smith");
            Assert.AreEqual("E.", hn.First);
            Assert.AreEqual("T.", hn.Middle);
            Assert.AreEqual("Smith", hn.Last);
        }

        [TestMethod]
        public void TestCouplesNames()
        {
            var hn = new HumanName("John and Jane Smith");
            Assert.AreEqual("John and Jane", hn.First);
            Assert.AreEqual("Smith", hn.Last);
        }

        [TestMethod]
        public void TestCouplesNamesWithConjunctionLastname()
        {
            var hn = new HumanName("John and Jane Aznar y Lopez");
            Assert.AreEqual("John and Jane", hn.First);
            Assert.AreEqual("Aznar y Lopez", hn.Last);
        }

        [TestMethod]
        public void TestCoupleTitles()
        {
            var hn = new HumanName("Mr. and Mrs. John and Jane Smith");
            Assert.AreEqual("Mr. and Mrs.", hn.Title);
            Assert.AreEqual("John and Jane", hn.First);
            Assert.AreEqual("Smith", hn.Last);
        }

        [TestMethod]
        public void TestTitleWithThreePartNameLastInitialIsSuffixUppercaseNoPeriod()
        {
            var hn = new HumanName("King John Alexander V");
            Assert.AreEqual("King", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Alexander", hn.Last);
            Assert.AreEqual("V", hn.Suffix);
        }

        [TestMethod]
        public void TestFourNamePartsWithSuffixThatCouldBeInitialLowercaseNoPeriod()
        {
            var hn = new HumanName("larry james edward johnson v");
            Assert.AreEqual("larry", hn.First);
            Assert.AreEqual("james edward", hn.Middle);
            Assert.AreEqual("johnson", hn.Last);
            Assert.AreEqual("v", hn.Suffix);
        }

        [TestMethod]
        public void TestFourNamePartsWithSuffixThatCouldBeInitialUppercaseNoPeriod()
        {
            var hn = new HumanName("Larry James Johnson I");
            Assert.AreEqual("Larry", hn.First);
            Assert.AreEqual("James", hn.Middle);
            Assert.AreEqual("Johnson", hn.Last);
            Assert.AreEqual("I", hn.Suffix);
        }

        [TestMethod]
        public void TestRomanNumeralInitials()
        {
            var hn = new HumanName("Larry V I");
            Assert.AreEqual("Larry", hn.First);
            Assert.AreEqual("V", hn.Middle);
            Assert.AreEqual("I", hn.Last);
            Assert.AreEqual("", hn.Suffix);
        }

        // tests for Rev. title (Reverend);
        [TestMethod]
        public void Test124()
        {
            var hn = new HumanName("Rev. John A. Kenneth Doe");
            Assert.AreEqual("Rev.", hn.Title);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
        }

        [TestMethod]
        public void Test125()
        {
            var hn = new HumanName("Rev John A. Kenneth Doe");
            Assert.AreEqual("Rev", hn.Title);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
        }

        [TestMethod]
        public void Test126()
        {
            var hn = new HumanName("Doe, Rev. John A. Jr.");
            Assert.AreEqual("Rev.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test127()
        {
            var hn = new HumanName("Buca di Beppo");
            Assert.AreEqual("Buca", hn.First);
            Assert.AreEqual("di Beppo", hn.Last);
        }

        [TestMethod]
        public void TestLeAsLastName()
        {
            var hn = new HumanName("Yin Le");
            Assert.AreEqual("Yin", hn.First);
            Assert.AreEqual("Le", hn.Last);
        }

        [TestMethod]
        public void TestLeAsLastNameWithMiddleInitial()
        {
            var hn = new HumanName("Yin a Le");
            Assert.AreEqual("Yin", hn.First);
            Assert.AreEqual("a", hn.Middle);
            Assert.AreEqual("Le", hn.Last);
        }

        [TestMethod]
        public void TestConjunctionInAnAddressWithATitle()
        {
            var hn = new HumanName("His Excellency Lord Duncan");
            Assert.AreEqual("His Excellency Lord", hn.Title);
            Assert.AreEqual("Duncan", hn.Last);
        }

        [Ignore("Expected failure")]
        [TestMethod]
        public void TestConjunctionInAnAddressWithAFirstNameTitle()
        {
            var hn = new HumanName("Her Majesty Queen Elizabeth");
            Assert.AreEqual("Her Majesty Queen", hn.Title);
            // if you want to be technical, Queen is in FIRST_NAME_TITLES
            Assert.AreEqual("Elizabeth", hn.First);
        }

        [TestMethod]
        public void TestNameIsConjunctions()
        {
            var hn = new HumanName("e and e");
            Assert.AreEqual("e and e", hn.First);
        }
    }
}