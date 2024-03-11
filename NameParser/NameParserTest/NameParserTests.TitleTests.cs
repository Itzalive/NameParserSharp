namespace NameParserTest;

using NameParser;

using Xunit;

public partial class NameParserTests {
    public class TitleTests {
        [Fact]
        public void TestLastNameIsAlsoTitle() {
            var hn = new HumanName("Amy E Maid");
            Assert.Equal("Amy", hn.First);
            Assert.Equal("E", hn.Middle);
            Assert.Equal("Maid", hn.Last);
        }

        [Fact]
        public void TestLastNameIsAlsoTitleNoComma() {
            var hn = new HumanName("Dr. Martin Luther King Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Martin", hn.First);
            Assert.Equal("Luther", hn.Middle);
            Assert.Equal("King", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void TestLastNameIsAlsoTitleWithComma() {
            var hn = new HumanName("Dr Martin Luther King, Jr.");
            Assert.Equal("Dr", hn.Title);
            Assert.Equal("Martin", hn.First);
            Assert.Equal("Luther", hn.Middle);
            Assert.Equal("King", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void TestLastNameIsAlsoTitle3() {
            var hn = new HumanName("John King");
            Assert.Equal("John", hn.First);
            Assert.Equal("King", hn.Last);
        }

        [Fact]
        public void TestTitleWithConjunction() {
            var hn = new HumanName("Secretary of State Hillary Clinton");
            Assert.Equal("Secretary of State", hn.Title);
            Assert.Equal("Hillary", hn.First);
            Assert.Equal("Clinton", hn.Last);
        }

        [Fact]
        public void TestCompoundTitleWithConjunction() {
            var hn = new HumanName("Cardinal Secretary of State Hillary Clinton");
            Assert.Equal("Cardinal Secretary of State", hn.Title);
            Assert.Equal("Hillary", hn.First);
            Assert.Equal("Clinton", hn.Last);
        }

        [Fact]
        public void TestTitleIsTitle() {
            var hn = new HumanName("Coach");
            Assert.Equal("Coach", hn.Title);
        }

        // TODO: fix handling of U.S.
        [Fact(Skip = "Expected failure")]
        public void TestChainedTitleFirstNameTitleIsInitials() {
            var hn = new HumanName("U.S. District Judge Marc Thomas Treadwell");
            Assert.Equal("U.S. District Judge", hn.Title);
            Assert.Equal("Marc", hn.First);
            Assert.Equal("Thomas", hn.Middle);
            Assert.Equal("Treadwell", hn.Last);
        }

        [Fact]
        public void TestConflictWithChainedTitleFirstNameInitial() {
            var hn = new HumanName("U. S. Grant");
            Assert.Equal("U.", hn.First);
            Assert.Equal("S.", hn.Middle);
            Assert.Equal("Grant", hn.Last);
        }

        [Fact]
        public void TestChainedTitleFirstNameInitialWithNoPeriod() {
            var hn = new HumanName("US Magistrate Judge T Michael Putnam");
            Assert.Equal("US Magistrate Judge", hn.Title);
            Assert.Equal("T", hn.First);
            Assert.Equal("Michael", hn.Middle);
            Assert.Equal("Putnam", hn.Last);
        }

        [Fact]
        public void TestChainedHyphenatedTitle() {
            var hn = new HumanName("US Magistrate-Judge Elizabeth E Campbell");
            Assert.Equal("US Magistrate-Judge", hn.Title);
            Assert.Equal("Elizabeth", hn.First);
            Assert.Equal("E", hn.Middle);
            Assert.Equal("Campbell", hn.Last);
        }

        [Fact]
        public void TestChainedHyphenatedTitleWithCommaSuffix() {
            var hn = new HumanName("Mag-Judge Harwell G Davis, III");
            Assert.Equal("Mag-Judge", hn.Title);
            Assert.Equal("Harwell", hn.First);
            Assert.Equal("G", hn.Middle);
            Assert.Equal("Davis", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact(Skip = "Expected failure")]
        public void TestTitleMultipleTitlesWithApostropheS() {
            var hn = new HumanName("The Right Hon. the President of the Queen's Bench Division");
            Assert.Equal("The Right Hon. the President of the Queen's Bench Division", hn.Title);
        }

        [Fact]
        public void TestTitleStartsWithConjunction() {
            var hn = new HumanName("The Rt Hon John Jones");
            Assert.Equal("The Rt Hon", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Jones", hn.Last);
        }

        [Fact]
        public void TestConjunctionBeforeTitle() {
            var hn = new HumanName("The Lord of the Universe");
            Assert.Equal("The Lord of the Universe", hn.Title);
        }

        [Fact]
        public void TestDoubleConjunctionOnTitle() {
            var hn = new HumanName("Lord of the Universe");
            Assert.Equal("Lord of the Universe", hn.Title);
        }

        [Fact]
        public void TestTripleConjunctionOnTitle() {
            var hn = new HumanName("Lord and of the Universe");
            Assert.Equal("Lord and of the Universe", hn.Title);
        }

        [Fact]
        public void TestMultipleConjunctionsOnMultipleTitles() {
            var hn = new HumanName("Lord of the Universe and Associate Supreme Queen of the World Lisa Simpson");
            Assert.Equal("Lord of the Universe and Associate Supreme Queen of the World", hn.Title);
            Assert.Equal("Lisa", hn.First);
            Assert.Equal("Simpson", hn.Last);
        }

        [Fact]
        public void TestTitleWithLastInitialIsSuffix() {
            var hn = new HumanName("King John V.");
            Assert.Equal("King", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("V.", hn.Last);
        }

        [Fact]
        public void TestInitialsAlsoSuffix() {
            var hn = new HumanName("Smith, J.R.");
            Assert.Equal("J.R.", hn.First);
            // Assert.Equal("R.", hn.Middle);
            Assert.Equal("Smith", hn.Last);
        }

        [Fact]
        public void TestTwoTitlePartsSeparatedByPeriods() {
            var hn = new HumanName("Lt.Gen. John A. Kenneth Doe IV");
            Assert.Equal("Lt.Gen.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("IV", hn.Suffix);
        }

        [Fact]
        public void TestTwoPartTitle() {
            var hn = new HumanName("Lt. Gen. John A. Kenneth Doe IV");
            Assert.Equal("Lt. Gen.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("IV", hn.Suffix);
        }

        [Fact]
        public void TestTwoPartTitleWithLastnameComma() {
            var hn = new HumanName("Doe, Lt. Gen. John A. Kenneth IV");
            Assert.Equal("Lt. Gen.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("IV", hn.Suffix);
        }

        [Fact]
        public void TestTwoPartTitleWithSuffixComma() {
            var hn = new HumanName("Lt. Gen. John A. Kenneth Doe, Jr.");
            Assert.Equal("Lt. Gen.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void TestPossibleConflictWithMiddleInitialThatCouldBeSuffix() {
            var hn = new HumanName("Doe, Rev. John V, Jr.");
            Assert.Equal("Rev.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("V", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void TestPossibleConflictWithSuffixThatCouldBeInitial() {
            var hn = new HumanName("Doe, Rev. John A., V, Jr.");
            Assert.Equal("Rev.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("V, Jr.", hn.Suffix);
        }

        // 'ben' is removed from PREFIXES in v0.2.5
        // this test could re-enable this test if we decide to support 'ben' as a prefix
        [Fact(Skip = "Expected failure")]
        public void TestBenAsConjunction() {
            var hn = new HumanName("Ahmad ben Husain");
            Assert.Equal("Ahmad", hn.First);
            Assert.Equal("ben Husain", hn.Last);
        }

        [Fact]
        public void TestBenAsFirstName() {
            var hn = new HumanName("Ben Johnson");
            Assert.Equal("Ben", hn.First);
            Assert.Equal("Johnson", hn.Last);
        }

        [Fact]
        public void TestBenAsFirstNameWithMiddleName() {
            var hn = new HumanName("Ben Alex Johnson");
            Assert.Equal("Ben", hn.First);
            Assert.Equal("Alex", hn.Middle);
            Assert.Equal("Johnson", hn.Last);
        }

        [Fact]
        public void TestBenAsMiddleName() {
            var hn = new HumanName("Alex Ben Johnson");
            Assert.Equal("Alex", hn.First);
            Assert.Equal("Ben", hn.Middle);
            Assert.Equal("Johnson", hn.Last);
        }

        // http://code.google.com/p/python-nameparser/issues/detail?id=13
        [Fact]
        public void TestLastNameAlsoPrefix() {
            var hn = new HumanName("Jane Doctor");
            Assert.Equal("Jane", hn.First);
            Assert.Equal("Doctor", hn.Last);
        }

        [Fact]
        public void TestTitleWithPeriods() {
            var hn = new HumanName("Lt.Gov. John Doe");
            Assert.Equal("Lt.Gov.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
        }

        [Fact]
        public void TestTitleWithPeriodsLastnameComma() {
            var hn = new HumanName("Doe, Lt.Gov. John");
            Assert.Equal("Lt.Gov.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
        }

        [Fact]
        public void TestMacWithSpaces() {
            var hn = new HumanName("Jane Mac Beth");
            Assert.Equal("Jane", hn.First);
            Assert.Equal("Mac Beth", hn.Last);
        }

        [Fact]
        public void TestMacAsFirstName() {
            var hn = new HumanName("Mac Miller");
            Assert.Equal("Mac", hn.First);
            Assert.Equal("Miller", hn.Last);
        }

        [Fact]
        public void TestMultiplePrefixes() {
            var hn = new HumanName("Mike van der Velt");
            Assert.Equal("Mike", hn.First);
            Assert.Equal("van der Velt", hn.Last);
        }

        [Fact]
        public void Test2SamePrefixesInTheName() {
            var hn = new HumanName("Vincent van Gogh van Beethoven");
            Assert.Equal("Vincent", hn.First);
            Assert.Equal("van Gogh", hn.Middle);
            Assert.Equal("van Beethoven", hn.Last);
        }
    }
}