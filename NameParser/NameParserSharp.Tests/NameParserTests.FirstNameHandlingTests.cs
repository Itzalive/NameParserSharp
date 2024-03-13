namespace NameParserSharp.Tests;

using NameParserSharp;

using Xunit;

public partial class NameParserTests {
    public class FirstNameHandlingTests {
        [Fact]
        public void TestFirstName() {
            var hn = new HumanName("Andrew");
            Assert.Equal("Andrew", hn.First);
        }

        [Fact]
        public void TestAssumeTitleAndOneOtherNameIsLastName() {
            var hn = new HumanName("Rev Andrews");
            Assert.Equal("Rev", hn.Title);
            Assert.Equal("Andrews", hn.Last);
        }

        // TODO: Seems "Andrews, M.D.", Andrews should be treated as a last name
        // but other suffixes like "George Jr." should be first names. Might be
        // related to https://github.com/derek73/python-nameparser/issues/2
        [Fact(Skip = "Expected failure")]
        public void TestAssumeSuffixTitleAndOneOtherNameIsLastName() {
            var hn = new HumanName("Andrews, M.D.");
            Assert.Equal("M.D.", hn.Suffix);
            Assert.Equal("Andrews", hn.Last);
        }

        [Fact]
        public void TestSuffixInLastnamePartOfLastnameCommaFormat() {
            var hn = new HumanName("Smith Jr., John");
            Assert.Equal("Smith", hn.Last);
            Assert.Equal("John", hn.First);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void TestSirExceptionToFirstNameRule() {
            var hn = new HumanName("Sir Gerald");
            Assert.Equal("Sir", hn.Title);
            Assert.Equal("Gerald", hn.First);
        }

        [Fact]
        public void TestKingExceptionToFirstNameRule() {
            var hn = new HumanName("King Henry");
            Assert.Equal("King", hn.Title);
            Assert.Equal("Henry", hn.First);
        }

        [Fact]
        public void TestQueenExceptionToFirstNameRule() {
            var hn = new HumanName("Queen Elizabeth");
            Assert.Equal("Queen", hn.Title);
            Assert.Equal("Elizabeth", hn.First);
        }

        [Fact]
        public void TestDameExceptionToFirstNameRule() {
            var hn = new HumanName("Dame Mary");
            Assert.Equal("Dame", hn.Title);
            Assert.Equal("Mary", hn.First);
        }

        [Fact]
        public void TestFirstNameIsNotPrefixIfOnlyTwoParts() {
            // When there are only two parts, don't join prefixes or conjunctions
            var hn = new HumanName("Van Nguyen");
            Assert.Equal("Van", hn.First);
            Assert.Equal("Nguyen", hn.Last);
        }

        [Fact]
        public void TestFirstNameIsNotPrefixIfOnlyTwoPartsComma() {
            var hn = new HumanName("Nguyen, Van");
            Assert.Equal("Van", hn.First);
            Assert.Equal("Nguyen", hn.Last);
        }

        [Fact(Skip = "Expected failure")]
        public void TestFirstNameIsPrefixIfThreeParts() {
            //"""Not sure how to fix this without breaking Mr and Mrs"""
            var hn = new HumanName("Mr. Van Nguyen");
            Assert.Equal("Van", hn.First);
            Assert.Equal("Nguyen", hn.Last);
        }
    }
}