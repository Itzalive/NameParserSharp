namespace NameParserTest;

using NameParser;

using Xunit;

public partial class NameParserTests {
    public class SuffixesTestCase {
        [Fact]
        public void TestSuffix() {
            var hn = new HumanName("Joe Franklin Jr");
            Assert.Equal("Joe", hn.First);
            Assert.Equal("Franklin", hn.Last);
            Assert.Equal("Jr", hn.Suffix);
        }

        [Fact]
        public void TestSuffixWithPeriods() {
            var hn = new HumanName("Joe Dentist D.D.S.");
            Assert.Equal("Joe", hn.First);
            Assert.Equal("Dentist", hn.Last);
            Assert.Equal("D.D.S.", hn.Suffix);
        }

        [Fact]
        public void TestTwoSuffixes() {
            var hn = new HumanName("Kenneth Clarke QC MP");
            Assert.Equal("Kenneth", hn.First);
            Assert.Equal("Clarke", hn.Last);
            // NOTE: this adds a comma when the original format did not have one.
            // not ideal but at least its in the right bucket
            Assert.Equal("QC, MP", hn.Suffix);
        }

        [Fact]
        public void TestTwoSuffixesLastnameCommaFormat() {
            var hn = new HumanName("Washington Jr. MD, Franklin");
            Assert.Equal("Franklin", hn.First);
            Assert.Equal("Washington", hn.Last);
            // NOTE: this adds a comma when the original format did not have one.
            Assert.Equal("Jr., MD", hn.Suffix);
        }

        [Fact]
        public void TestTwoSuffixesSuffixCommaFormat() {
            var hn = new HumanName("Franklin Washington, Jr. MD");
            Assert.Equal("Franklin", hn.First);
            Assert.Equal("Washington", hn.Last);
            Assert.Equal("Jr. MD", hn.Suffix);
        }

        [Fact]
        public void TestSuffixContainingPeriods() {
            var hn = new HumanName("Kenneth Clarke Q.C.");
            Assert.Equal("Kenneth", hn.First);
            Assert.Equal("Clarke", hn.Last);
            Assert.Equal("Q.C.", hn.Suffix);
        }

        [Fact]
        public void TestSuffixContainingPeriodsLastnameCommaFormat() {
            var hn = new HumanName("Clarke, Kenneth, Q.C. M.P.");
            Assert.Equal("Kenneth", hn.First);
            Assert.Equal("Clarke", hn.Last);
            Assert.Equal("Q.C. M.P.", hn.Suffix);
        }

        [Fact]
        public void TestSuffixContainingPeriodsSuffixCommaFormat() {
            var hn = new HumanName("Kenneth Clarke Q.C., M.P.");
            Assert.Equal("Kenneth", hn.First);
            Assert.Equal("Clarke", hn.Last);
            Assert.Equal("Q.C., M.P.", hn.Suffix);
        }

        [Fact]
        public void TestSuffixWithSingleCommaFormat() {
            var hn = new HumanName("John Doe jr., MD");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("jr., MD", hn.Suffix);
        }

        [Fact]
        public void TestSuffixWithDoubleCommaFormat() {
            var hn = new HumanName("Doe, John jr., MD");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("jr., MD", hn.Suffix);
        }

        [Fact]
        public void TestPhdWithErroneousSpace() {
            var hn = new HumanName("John Smith, Ph. D.");
            Assert.Equal("John", hn.First);
            Assert.Equal("Smith", hn.Last);
            Assert.Equal("Ph. D.", hn.Suffix);
        }

        [Fact]
        public void TestPhdConflict() {
            var hn = new HumanName("Adolph D");
            Assert.Equal("Adolph", hn.First);
            Assert.Equal("D", hn.Last);

            // http://en.wikipedia.org/wiki/Ma_(surname);
        }

        [Fact]
        public void TestPotentialSuffixThatIsAlsoLastName() {
            var hn = new HumanName("Jack Ma");
            Assert.Equal("Jack", hn.First);
            Assert.Equal("Ma", hn.Last);
        }

        [Fact]
        public void TestPotentialSuffixThatIsAlsoLastNameComma() {
            var hn = new HumanName("Ma, Jack");
            Assert.Equal("Jack", hn.First);
            Assert.Equal("Ma", hn.Last);
        }

        [Fact]
        public void TestPotentialSuffixThatIsAlsoLastNameWithSuffix() {
            var hn = new HumanName("Jack Ma Jr");
            Assert.Equal("Jack", hn.First);
            Assert.Equal("Ma", hn.Last);
            Assert.Equal("Jr", hn.Suffix);
        }

        [Fact]
        public void TestPotentialSuffixThatIsAlsoLastNameWithSuffixComma() {
            var hn = new HumanName("Ma III, Jack Jr");
            Assert.Equal("Jack", hn.First);
            Assert.Equal("Ma", hn.Last);
            Assert.Equal("III, Jr", hn.Suffix);
        }

        // https://github.com/derek73/python-nameparser/issues/27
        [Fact(Skip = "Expected failure")]
        public void TestKing() {
            var hn = new HumanName("Dr King Jr");
            Assert.Equal("Dr", hn.Title);
            Assert.Equal("King", hn.Last);
            Assert.Equal("Jr", hn.Suffix);
        }

        [Fact]
        public void TestMultipleLetterSuffixWithPeriods() {
            var hn = new HumanName("John Doe Msc.Ed.");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Msc.Ed.", hn.Suffix);
        }

        [Fact]
        public void TestSuffixWithPeriodsWithComma() {
            var hn = new HumanName("John Doe, Msc.Ed.");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Msc.Ed.", hn.Suffix);
        }

        [Fact]
        public void TestSuffixWithPeriodsWithLastnameComma() {
            var hn = new HumanName("Doe, John Msc.Ed.");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Msc.Ed.", hn.Suffix);
        }
    }
}