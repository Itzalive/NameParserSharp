namespace NameParserSharp.Tests;

using NameParserSharp;

using Xunit;
using Xunit.Abstractions;

public partial class NameParserTests {
    public class ConjunctionTests {
        private readonly ITestOutputHelper output;

        public ConjunctionTests(ITestOutputHelper output) {
            this.output = output;
        }

        // Last name with conjunction
        [Fact]
        public void TestLastNameWithConjunction() {
            var hn = new HumanName("Jose Aznar y Lopez");
            Assert.Equal("Jose", hn.First);
            Assert.Equal("Aznar y Lopez", hn.Last);
        }

        [Fact]
        public void TestMultipleConjunctions() {
            var hn = new HumanName("part1 of The part2 of the part3 and part4");
            Assert.Equal("part1 of The part2 of the part3 and part4", hn.First);
        }

        [Fact]
        public void TestMultipleConjunctions2() {
            var hn = new HumanName("part1 of and The part2 of the part3 And part4");
            Assert.Equal("part1 of and The part2 of the part3 And part4", hn.First);
        }

        [Fact]
        public void TestEndsWithConjunction() {
            var hn = new HumanName("Jon Dough and");
            Assert.Equal("Jon", hn.First);
            Assert.Equal("Dough and", hn.Last);
        }

        [Fact]
        public void TestEndsWithTwoConjunctions() {
            var hn = new HumanName("Jon Dough and of");
            Assert.Equal("Jon", hn.First);
            Assert.Equal("Dough and of", hn.Last);
        }

        [Fact]
        public void TestStartsWithConjunction() {
            var hn = new HumanName("and Jon Dough");
            Assert.Equal("and Jon", hn.First);
            Assert.Equal("Dough", hn.Last);
        }

        [Fact]
        public void TestStartsWithTwoConjunctions() {
            var hn = new HumanName("the and Jon Dough");
            Assert.Equal("the and Jon", hn.First);
            Assert.Equal("Dough", hn.Last);
        }

        // Potential conjunction/prefix treated as initial (because uppercase);
        [Fact]
        public void TestUppercaseMiddleInitialConflictWithConjunction() {
            var hn = new HumanName("John E Smith");
            Assert.Equal("John", hn.First);
            Assert.Equal("E", hn.Middle);
            Assert.Equal("Smith", hn.Last);
        }

        [Fact]
        public void TestLowercaseMiddleInitialWithPeriodConflictWithConjunction() {
            var hn = new HumanName("john e. smith");
            Assert.Equal("john", hn.First);
            Assert.Equal("e.", hn.Middle);
            Assert.Equal("smith", hn.Last);
        }

        // The conjunction "e" can also be an initial
        [Fact]
        public void TestLowercaseFirstInitialConflictWithConjunction() {
            var hn = new HumanName("e j smith");
            Assert.Equal("e", hn.First);
            Assert.Equal("j", hn.Middle);
            Assert.Equal("smith", hn.Last);
        }

        [Fact]
        public void TestLowercaseMiddleInitialConflictWithConjunction() {
            var hn = new HumanName("John e Smith");
            Assert.Equal("John", hn.First);
            Assert.Equal("e", hn.Middle);
            Assert.Equal("Smith", hn.Last);
        }

        [Fact]
        public void TestLowercaseMiddleInitialAndSuffixConflictWithConjunction() {
            var hn = new HumanName("John e Smith, III");
            Assert.Equal("John", hn.First);
            Assert.Equal("e", hn.Middle);
            Assert.Equal("Smith", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void TestLowercaseMiddleInitialAndNoCommaSuffixConflictWithConjunction() {
            var hn = new HumanName("John e Smith III");
            Assert.Equal("John", hn.First);
            Assert.Equal("e", hn.Middle);
            Assert.Equal("Smith", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void TestLowercaseMiddleInitialCommaLastnameAndSuffixConflictWithConjunction() {
            var hn = new HumanName("Smith, John e, III, Jr");
            Assert.Equal("John", hn.First);
            Assert.Equal("e", hn.Middle);
            Assert.Equal("Smith", hn.Last);
            Assert.Equal("III, Jr", hn.Suffix);
        }

        [Fact(Skip = "Expected failure")]
        public void TestTwoInitialsConflictWithConjunction() {
            // Supporting this seems to screw up titles with periods in them like M.B.A.
            var hn = new HumanName("E.T. Smith");
            Assert.Equal("E.", hn.First);
            Assert.Equal("T.", hn.Middle);
            Assert.Equal("Smith", hn.Last);
        }

        [Fact]
        public void TestCouplesNames() {
            var hn = new HumanName("John and Jane Smith");
            Assert.Equal("John and Jane", hn.First);
            Assert.Equal("Smith", hn.Last);
        }

        [Fact]
        public void TestCouplesNamesWithConjunctionLastname() {
            var hn = new HumanName("John and Jane Aznar y Lopez");
            Assert.Equal("John and Jane", hn.First);
            Assert.Equal("Aznar y Lopez", hn.Last);
        }

        [Fact]
        public void TestCoupleTitles() {
            var hn = new HumanName("Mr. and Mrs. John and Jane Smith");
            Assert.Equal("Mr. and Mrs.", hn.Title);
            Assert.Equal("John and Jane", hn.First);
            Assert.Equal("Smith", hn.Last);
        }

        [Fact]
        public void TestTitleWithThreePartNameLastInitialIsSuffixUppercaseNoPeriod() {
            var hn = new HumanName("King John Alexander V");
            Assert.Equal("King", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Alexander", hn.Last);
            Assert.Equal("V", hn.Suffix);
        }

        [Fact]
        public void TestFourNamePartsWithSuffixThatCouldBeInitialLowercaseNoPeriod() {
            var hn = new HumanName("larry james edward johnson v");
            Assert.Equal("larry", hn.First);
            Assert.Equal("james edward", hn.Middle);
            Assert.Equal("johnson", hn.Last);
            Assert.Equal("v", hn.Suffix);
        }

        [Fact]
        public void TestFourNamePartsWithSuffixThatCouldBeInitialUppercaseNoPeriod() {
            var hn = new HumanName("Larry James Johnson I");
            Assert.Equal("Larry", hn.First);
            Assert.Equal("James", hn.Middle);
            Assert.Equal("Johnson", hn.Last);
            Assert.Equal("I", hn.Suffix);
        }

        [Fact]
        public void TestRomanNumeralInitials() {
            var hn = new HumanName("Larry V I");
            Assert.Equal("Larry", hn.First);
            Assert.Equal("V", hn.Middle);
            Assert.Equal("I", hn.Last);
            Assert.Equal("", hn.Suffix);
        }

        // tests for Rev. title (Reverend);
        [Fact]
        public void Test124() {
            var hn = new HumanName("Rev. John A. Kenneth Doe");
            Assert.Equal("Rev.", hn.Title);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
        }

        [Fact]
        public void Test125() {
            var hn = new HumanName("Rev John A. Kenneth Doe");
            Assert.Equal("Rev", hn.Title);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
        }

        [Fact]
        public void Test126() {
            var hn = new HumanName("Doe, Rev. John A. Jr.");
            Assert.Equal("Rev.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test127() {
            var hn = new HumanName("Buca di Beppo");
            Assert.Equal("Buca", hn.First);
            Assert.Equal("di Beppo", hn.Last);
        }

        [Fact]
        public void TestLeAsLastName() {
            var hn = new HumanName("Yin Le");
            Assert.Equal("Yin", hn.First);
            Assert.Equal("Le", hn.Last);
        }

        [Fact]
        public void TestLeAsLastNameWithMiddleInitial() {
            var hn = new HumanName("Yin a Le");
            Assert.Equal("Yin", hn.First);
            Assert.Equal("a", hn.Middle);
            Assert.Equal("Le", hn.Last);
        }

        [Fact]
        public void TestConjunctionInAnAddressWithATitle() {
            var hn = new HumanName("His Excellency Lord Duncan");
            Assert.Equal("His Excellency Lord", hn.Title);
            Assert.Equal("Duncan", hn.Last);
        }

        [Fact(Skip = "Expected failure")]
        public void TestConjunctionInAnAddressWithAFirstNameTitle() {
            var hn = new HumanName("Her Majesty Queen Elizabeth");
            Assert.Equal("Her Majesty Queen", hn.Title);
            // if you want to be technical, Queen is in FIRST_NAME_TITLES
            Assert.Equal("Elizabeth", hn.First);
        }

        [Fact]
        public void TestNameIsConjunctions() {
            var hn = new HumanName("e and e");
            Assert.Equal("e and e", hn.First);
        }
    }
}