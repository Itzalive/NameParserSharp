using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]
    public class TitleTests
    {
        [TestMethod]
        public void TestLastNameIsAlsoTitle()
        {
            var hn = new HumanName("Amy E Maid");
            Assert.AreEqual("Amy", hn.First);
            Assert.AreEqual("E", hn.Middle);
            Assert.AreEqual("Maid", hn.Last);
        }

        [TestMethod]
        public void TestLastNameIsAlsoTitleNoComma()
        {
            var hn = new HumanName("Dr. Martin Luther King Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Martin", hn.First);
            Assert.AreEqual("Luther", hn.Middle);
            Assert.AreEqual("King", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void TestLastNameIsAlsoTitleWithComma()
        {
            var hn = new HumanName("Dr Martin Luther King, Jr.");
            Assert.AreEqual("Dr", hn.Title);
            Assert.AreEqual("Martin", hn.First);
            Assert.AreEqual("Luther", hn.Middle);
            Assert.AreEqual("King", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void TestLastNameIsAlsoTitle3()
        {
            var hn = new HumanName("John King");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("King", hn.Last);
        }

        [TestMethod]
        public void TestTitleWithConjunction()
        {
            var hn = new HumanName("Secretary of State Hillary Clinton");
            Assert.AreEqual("Secretary of State", hn.Title);
            Assert.AreEqual("Hillary", hn.First);
            Assert.AreEqual("Clinton", hn.Last);
        }

        [TestMethod]
        public void TestCompoundTitleWithConjunction()
        {
            var hn = new HumanName("Cardinal Secretary of State Hillary Clinton");
            Assert.AreEqual("Cardinal Secretary of State", hn.Title);
            Assert.AreEqual("Hillary", hn.First);
            Assert.AreEqual("Clinton", hn.Last);
        }

        [TestMethod]
        public void TestTitleIsTitle()
        {
            var hn = new HumanName("Coach");
            Assert.AreEqual("Coach", hn.Title);
        }

// TODO: fix handling of U.S.
        [Ignore("Expected failure")]
        [TestMethod]
        public void TestChainedTitleFirstNameTitleIsInitials()
        {
            var hn = new HumanName("U.S. District Judge Marc Thomas Treadwell");
            Assert.AreEqual("U.S. District Judge", hn.Title);
            Assert.AreEqual("Marc", hn.First);
            Assert.AreEqual("Thomas", hn.Middle);
            Assert.AreEqual("Treadwell", hn.Last);
        }

        [TestMethod]
        public void TestConflictWithChainedTitleFirstNameInitial()
        {
            var hn = new HumanName("U. S. Grant");
            Assert.AreEqual("U.", hn.First);
            Assert.AreEqual("S.", hn.Middle);
            Assert.AreEqual("Grant", hn.Last);
        }

        [TestMethod]
        public void TestChainedTitleFirstNameInitialWithNoPeriod()
        {
            var hn = new HumanName("US Magistrate Judge T Michael Putnam");
            Assert.AreEqual("US Magistrate Judge", hn.Title);
            Assert.AreEqual("T", hn.First);
            Assert.AreEqual("Michael", hn.Middle);
            Assert.AreEqual("Putnam", hn.Last);
        }

        [TestMethod]
        public void TestChainedHyphenatedTitle()
        {
            var hn = new HumanName("US Magistrate-Judge Elizabeth E Campbell");
            Assert.AreEqual("US Magistrate-Judge", hn.Title);
            Assert.AreEqual("Elizabeth", hn.First);
            Assert.AreEqual("E", hn.Middle);
            Assert.AreEqual("Campbell", hn.Last);
        }

        [TestMethod]
        public void TestChainedHyphenatedTitleWithCommaSuffix()
        {
            var hn = new HumanName("Mag-Judge Harwell G Davis, III");
            Assert.AreEqual("Mag-Judge", hn.Title);
            Assert.AreEqual("Harwell", hn.First);
            Assert.AreEqual("G", hn.Middle);
            Assert.AreEqual("Davis", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

[Ignore("Expected failure")]
        [TestMethod]
        public void TestTitleMultipleTitlesWithApostropheS()
        {
            var hn = new HumanName("The Right Hon. the President of the Queen's Bench Division");
            Assert.AreEqual("The Right Hon. the President of the Queen's Bench Division", hn.Title);
        }

        [TestMethod]
        public void TestTitleStartsWithConjunction()
        {
            var hn = new HumanName("The Rt Hon John Jones");
            Assert.AreEqual("The Rt Hon", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Jones", hn.Last);
        }

        [TestMethod]
        public void TestConjunctionBeforeTitle()
        {
            var hn = new HumanName("The Lord of the Universe");
            Assert.AreEqual("The Lord of the Universe", hn.Title);
        }

        [TestMethod]
        public void TestDoubleConjunctionOnTitle()
        {
            var hn = new HumanName("Lord of the Universe");
            Assert.AreEqual("Lord of the Universe", hn.Title);
        }

        [TestMethod]
        public void TestTripleConjunctionOnTitle()
        {
            var hn = new HumanName("Lord and of the Universe");
            Assert.AreEqual("Lord and of the Universe", hn.Title);
        }

        [TestMethod]
        public void TestMultipleConjunctionsOnMultipleTitles()
        {
            var hn = new HumanName("Lord of the Universe and Associate Supreme Queen of the World Lisa Simpson");
            Assert.AreEqual("Lord of the Universe and Associate Supreme Queen of the World", hn.Title);
            Assert.AreEqual("Lisa", hn.First);
            Assert.AreEqual("Simpson", hn.Last);
        }

        [TestMethod]
        public void TestTitleWithLastInitialIsSuffix()
        {
            var hn = new HumanName("King John V.");
            Assert.AreEqual("King", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("V.", hn.Last);
        }

        [TestMethod]
        public void TestInitialsAlsoSuffix()
        {
            var hn = new HumanName("Smith, J.R.");
            Assert.AreEqual("J.R.", hn.First);
            // Assert.AreEqual("R.", hn.Middle);
            Assert.AreEqual("Smith", hn.Last);
        }

        [TestMethod]
        public void TestTwoTitlePartsSeparatedByPeriods()
        {
            var hn = new HumanName("Lt.Gen. John A. Kenneth Doe IV");
            Assert.AreEqual("Lt.Gen.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("IV", hn.Suffix);
        }

        [TestMethod]
        public void TestTwoPartTitle()
        {
            var hn = new HumanName("Lt. Gen. John A. Kenneth Doe IV");
            Assert.AreEqual("Lt. Gen.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("IV", hn.Suffix);
        }

        [TestMethod]
        public void TestTwoPartTitleWithLastnameComma()
        {
            var hn = new HumanName("Doe, Lt. Gen. John A. Kenneth IV");
            Assert.AreEqual("Lt. Gen.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("IV", hn.Suffix);
        }

        [TestMethod]
        public void TestTwoPartTitleWithSuffixComma()
        {
            var hn = new HumanName("Lt. Gen. John A. Kenneth Doe, Jr.");
            Assert.AreEqual("Lt. Gen.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void TestPossibleConflictWithMiddleInitialThatCouldBeSuffix()
        {
            var hn = new HumanName("Doe, Rev. John V, Jr.");
            Assert.AreEqual("Rev.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("V", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void TestPossibleConflictWithSuffixThatCouldBeInitial()
        {
            var hn = new HumanName("Doe, Rev. John A., V, Jr.");
            Assert.AreEqual("Rev.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("V, Jr.", hn.Suffix);
        }

// 'ben' is removed from PREFIXES in v0.2.5
        // this test could re-enable this test if we decide to support 'ben' as a prefix
        [Ignore("Expected failure")]
        [TestMethod]
        public void TestBenAsConjunction()
        {
            var hn = new HumanName("Ahmad ben Husain");
            Assert.AreEqual("Ahmad", hn.First);
            Assert.AreEqual("ben Husain", hn.Last);
        }

        [TestMethod]
        public void TestBenAsFirstName()
        {
            var hn = new HumanName("Ben Johnson");
            Assert.AreEqual("Ben", hn.First);
            Assert.AreEqual("Johnson", hn.Last);
        }

        [TestMethod]
        public void TestBenAsFirstNameWithMiddleName()
        {
            var hn = new HumanName("Ben Alex Johnson");
            Assert.AreEqual("Ben", hn.First);
            Assert.AreEqual("Alex", hn.Middle);
            Assert.AreEqual("Johnson", hn.Last);
        }

        [TestMethod]
        public void TestBenAsMiddleName()
        {
            var hn = new HumanName("Alex Ben Johnson");
            Assert.AreEqual("Alex", hn.First);
            Assert.AreEqual("Ben", hn.Middle);
            Assert.AreEqual("Johnson", hn.Last);
        }

// http://code.google.com/p/python-nameparser/issues/detail?id=13
        [TestMethod]
        public void TestLastNameAlsoPrefix()
        {
            var hn = new HumanName("Jane Doctor");
            Assert.AreEqual("Jane", hn.First);
            Assert.AreEqual("Doctor", hn.Last);
        }

        [TestMethod]
        public void TestTitleWithPeriods()
        {
            var hn = new HumanName("Lt.Gov. John Doe");
            Assert.AreEqual("Lt.Gov.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
        }

        [TestMethod]
        public void TestTitleWithPeriodsLastnameComma()
        {
            var hn = new HumanName("Doe, Lt.Gov. John");
            Assert.AreEqual("Lt.Gov.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
        }

        [TestMethod]
        public void TestMacWithSpaces()
        {
            var hn = new HumanName("Jane Mac Beth");
            Assert.AreEqual("Jane", hn.First);
            Assert.AreEqual("Mac Beth", hn.Last);
        }

        [TestMethod]
        public void TestMacAsFirstName()
        {
            var hn = new HumanName("Mac Miller");
            Assert.AreEqual("Mac", hn.First);
            Assert.AreEqual("Miller", hn.Last);
        }

        [TestMethod]
        public void TestMultiplePrefixes()
        {
            var hn = new HumanName("Mike van der Velt");
            Assert.AreEqual("Mike", hn.First);
            Assert.AreEqual("van der Velt", hn.Last);
        }

        [TestMethod]
        public void Test2SamePrefixesInTheName()
        {
            var hn = new HumanName("Vincent van Gogh van Beethoven");
            Assert.AreEqual("Vincent", hn.First);
            Assert.AreEqual("van Gogh", hn.Middle);
            Assert.AreEqual("van Beethoven", hn.Last);
        }
    }
}