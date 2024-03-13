namespace NameParserSharp.Tests;

using NameParserSharp;

using Xunit;

public partial class NameParserTests {
    public class CapitalizationTests {
        [Fact]
        public void TestCapitalizationExceptionForIii() {
            var hn = new HumanName("juan q. xavier velasquez y garcia iii");
            hn.Normalize();
            Assert.Equal("Juan Q. Xavier Velasquez y Garcia III", hn.ToString());
        }

        // FIXME: this test does not pass due to a known issue
        // http://code.google.com/p/python-nameparser/issues/detail?id=22
        [Fact(Skip = "Expected failure")]
        public void TestCapitalizationExceptionForAlreadyCapitalizedIiiknownfailure() {
            var hn = new HumanName("juan garcia III");
            hn.Normalize();
            Assert.Equal("Juan Garcia III", hn.ToString());
        }

        [Fact]
        public void TestCapitalizeTitle() {
            var hn = new HumanName("lt. gen. john a. kenneth doe iv");
            hn.Normalize();
            Assert.Equal("Lt. Gen. John A. Kenneth Doe IV", hn.ToString());
        }

        [Fact]
        public void TestCapitalizeTitleToLower() {
            var hn = new HumanName("LT. GEN. JOHN A. KENNETH DOE IV");
            hn.Normalize();
            Assert.Equal("Lt. Gen. John A. Kenneth Doe IV", hn.ToString());
        }

        // Capitalization with M(a)c and hyphenated names
        [Fact]
        public void TestCapitalizationWithMacAsHyphenatedNames() {
            var hn = new HumanName("donovan mcnabb-smith");
            hn.Normalize();
            Assert.Equal("Donovan McNabb-Smith", hn.ToString());
        }

        [Fact]
        public void TestCapitizationMiddleInitialIsAlsoAConjunction() {
            var hn = new HumanName("scott e. werner");
            hn.Normalize();
            Assert.Equal("Scott E. Werner", hn.ToString());
        }

        // Leaving already-capitalized names alone
        [Fact]
        public void TestNoChangeToMixedChase() {
            var hn = new HumanName("Shirley Maclaine");
            hn.Normalize();
            Assert.Equal("Shirley Maclaine", hn.ToString());
        }

        [Fact]
        public void TestForceCapitalization() {
            var hn = new HumanName("Shirley Maclaine");
            hn.Normalize(true);
            Assert.Equal("Shirley MacLaine", hn.ToString());
        }

        [Fact]
        public void TestCapitalizeDiacritics() {
            var hn = new HumanName("matthëus schmidt");
            hn.Normalize();
            Assert.Equal("Matthëus Schmidt", hn.ToString());
        }

        // http://code.google.com/p/python-nameparser/issues/detail?id=15
        [Fact]
        public void TestDowncasingMac() {
            var hn = new HumanName("RONALD MACDONALD");
            hn.Normalize();
            Assert.Equal("Ronald MacDonald", hn.ToString());
        }

        // http://code.google.com/p/python-nameparser/issues/detail?id=23
        [Fact]
        public void TestDowncasingMc() {
            var hn = new HumanName("RONALD MCDONALD");
            hn.Normalize();
            Assert.Equal("Ronald McDonald", hn.ToString());
        }

        [Fact]
        public void TestShortNamesWithMac() {
            var hn = new HumanName("mack johnson");
            hn.Normalize();
            Assert.Equal("Mack Johnson", hn.ToString());
        }

        [Fact]
        public void TestPortuguesePrefixes() {
            var hn = new HumanName("joao da silva do amaral de souza");
            hn.Normalize();
            Assert.Equal("Joao da Silva do Amaral de Souza", hn.ToString());
        }

        [Fact]
        public void TestCapitalizePrefixClashOnFirstName() {
            var hn = new HumanName("van nguyen");
            hn.Normalize();
            Assert.Equal("Van Nguyen", hn.ToString());
        }
    }
}