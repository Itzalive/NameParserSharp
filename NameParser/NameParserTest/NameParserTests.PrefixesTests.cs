using NameParser;
using Xunit;

namespace NameParserTest;

public partial class NameParserTests
{
    public class PrefixesTests
    {
        [Fact]
        public void TestPrefix()
        {
            var hn = new HumanName("Juan del Sur");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("del Sur", hn.Last);
        }

        [Fact]
        public void TestPrefixWithPeriod()
        {
            var hn = new HumanName("Jill St. John");
            Assert.Equal("Jill", hn.First);
            Assert.Equal("St. John", hn.Last);
        }

        [Fact]
        public void TestPrefixBeforeTwoPartLastName()
        {
            var hn = new HumanName("pennie von bergen wessels");
            Assert.Equal("pennie", hn.First);
            Assert.Equal("von bergen wessels", hn.Last);
        }

        [Fact]
        public void TestPrefixIsFirstName()
        {
            var hn = new HumanName("Van Johnson");
            Assert.Equal("Van", hn.First);
            Assert.Equal("Johnson", hn.Last);
        }

        [Fact]
        public void TestPrefixIsFirstNameWithMiddleName()
        {
            var hn = new HumanName("Van Jeremy Johnson");
            Assert.Equal("Van", hn.First);
            Assert.Equal("Jeremy", hn.Middle);
            Assert.Equal("Johnson", hn.Last);
        }

        [Fact]
        public void TestPrefixBeforeTwoPartLastNameWithSuffix()
        {
            var hn = new HumanName("pennie von bergen wessels III");
            Assert.Equal("pennie", hn.First);
            Assert.Equal("von bergen wessels", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void TestPrefixBeforeTwoPartLastNameWithAcronymSuffix()
        {
            var hn = new HumanName("pennie von bergen wessels M.D.");
            Assert.Equal("pennie", hn.First);
            Assert.Equal("von bergen wessels", hn.Last);
            Assert.Equal("M.D.", hn.Suffix);
        }

        [Fact]
        public void TestTwoPartLastNameWithSuffixComma()
        {
            var hn = new HumanName("pennie von bergen wessels, III");
            Assert.Equal("pennie", hn.First);
            Assert.Equal("von bergen wessels", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void TestTwoPartLastNameWithSuffix()
        {
            var hn = new HumanName("von bergen wessels, pennie III");
            Assert.Equal("pennie", hn.First);
            Assert.Equal("von bergen wessels", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void TestLastNameTwoPartLastNameWithTwoSuffixes()
        {
            var hn = new HumanName("von bergen wessels MD, pennie III");
            Assert.Equal("pennie", hn.First);
            Assert.Equal("von bergen wessels", hn.Last);
            Assert.Equal("MD, III", hn.Suffix);
        }

        [Fact]
        public void TestCommaTwoPartLastNameWithAcronymSuffix()
        {
            var hn = new HumanName("von bergen wessels, pennie MD");
            Assert.Equal("pennie", hn.First);
            Assert.Equal("von bergen wessels", hn.Last);
            Assert.Equal("MD", hn.Suffix);
        }

        [Fact]
        public void TestCommaTwoPartLastNameWithSuffixInFirstPart()
        {
            // I'm kinda surprised this works, not really sure if this is a
            // realistic place for a suffix to be.
            var hn = new HumanName("von bergen wessels MD, pennie");
            Assert.Equal("pennie", hn.First);
            Assert.Equal("von bergen wessels", hn.Last);
            Assert.Equal("MD", hn.Suffix);
        }

        [Fact]
        public void TestTitleTwoPartLastNameWithSuffixInFirstPart()
        {
            var hn = new HumanName("pennie von bergen wessels MD, III");
            Assert.Equal("pennie", hn.First);
            Assert.Equal("von bergen wessels", hn.Last);
            Assert.Equal("MD, III", hn.Suffix);
        }

        [Fact]
        public void TestPortugueseDos()
        {
            var hn = new HumanName("Rafael Sousa dos Anjos");
            Assert.Equal("Rafael", hn.First);
            Assert.Equal("Sousa", hn.Middle);
            Assert.Equal("dos Anjos", hn.Last);
        }

        [Fact]
        public void TestPortuguesePrefixes()
        {
            var hn = new HumanName("Joao da Silva do Amaral de Souza");
            Assert.Equal("Joao", hn.First);
            Assert.Equal("da Silva do Amaral", hn.Middle);
            Assert.Equal("de Souza", hn.Last);
        }

        [Fact]
        public void TestThreeConjunctions()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier de la dos Vega III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la dos Vega", hn.Last);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void TestLastnameThreeConjunctions()
        {
            var hn = new HumanName("de la dos Vega, Dr. Juan Q. Xavier III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la dos Vega", hn.Last);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void TestCommaThreeConjunctions()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier de la dos Vega, III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la dos Vega", hn.Last);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }
    }
}