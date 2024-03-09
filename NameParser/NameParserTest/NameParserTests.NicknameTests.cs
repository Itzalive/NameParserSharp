using NameParser;
using Xunit;

namespace NameParserTest;

public partial class NameParserTests
{

    public class NicknameTests
    {
        // https://code.google.com/p/python-nameparser/issues/detail?id=33
        [Fact]
        public void TestNicknameInParenthesis()
        {
            var hn = new HumanName("Benjamin (Ben) Franklin");
            Assert.Equal("Benjamin", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("Franklin", hn.Last);
            Assert.Equal("Ben", hn.Nickname);
        }

        [Fact]
        public void TestTwoWordNicknameInParenthesis()
        {
            var hn = new HumanName("Benjamin (Big Ben) Franklin");
            Assert.Equal("Benjamin", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("Franklin", hn.Last);
            Assert.Equal("Big Ben", hn.Nickname);
        }

        [Fact]
        public void TestTwoWordsInQuotes()
        {
            var hn = new HumanName("Benjamin \"Big Ben\" Franklin");
            Assert.Equal("Benjamin", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("Franklin", hn.Last);
            Assert.Equal("Big Ben", hn.Nickname);
        }

        [Fact]
        public void TestNicknameInParenthesisWithComma()
        {
            var hn = new HumanName("Franklin, Benjamin (Ben)");
            Assert.Equal("Benjamin", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("Franklin", hn.Last);
            Assert.Equal("Ben", hn.Nickname);
        }

        [Fact]
        public void TestNicknameInParenthesisWithCommaAndSuffix()
        {
            var hn = new HumanName("Franklin, Benjamin (Ben), Jr.");
            Assert.Equal("Benjamin", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("Franklin", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
            Assert.Equal("Ben", hn.Nickname);
        }

        [Fact]
        public void TestNicknameInSingleQuotes()
        {
            var hn = new HumanName("Benjamin 'Ben' Franklin");
            Assert.Equal("Benjamin", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("Franklin", hn.Last);
            Assert.Equal("Ben", hn.Nickname);
        }

        [Fact]
        public void TestNicknameInDoubleQuotes()
        {
            var hn = new HumanName("Benjamin \"Ben\" Franklin");
            Assert.Equal("Benjamin", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("Franklin", hn.Last);
            Assert.Equal("Ben", hn.Nickname);
        }

        [Fact]
        public void TestSingleQuotesOnFirstNameNotTreatedAsNickname()
        {
            var hn = new HumanName("Brian Andrew O'connor");
            Assert.Equal("Brian", hn.First);
            Assert.Equal("Andrew", hn.Middle);
            Assert.Equal("O'connor", hn.Last);
            Assert.Equal("", hn.Nickname);
        }

        [Fact]
        public void TestSingleQuotesOnBothNameNotTreatedAsNickname()
        {
            var hn = new HumanName("La'tanya O'connor");
            Assert.Equal("La'tanya", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("O'connor", hn.Last);
            Assert.Equal("", hn.Nickname);
        }

        [Fact]
        public void TestSingleQuotesOnEndOfLastNameNotTreatedAsNickname()
        {
            var hn = new HumanName("Mari' Aube'");
            Assert.Equal("Mari'", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("Aube'", hn.Last);
            Assert.Equal("", hn.Nickname);
        }

        [Fact]
        public void TestOkinaInsideNameNotTreatedAsNickname()
        {
            var hn = new HumanName("Harrieta Keōpūolani Nāhiʻenaʻena");
            Assert.Equal("Harrieta", hn.First);
            Assert.Equal("Keōpūolani", hn.Middle);
            Assert.Equal("Nāhiʻenaʻena", hn.Last);
            Assert.Equal("", hn.Nickname);
        }

        [Fact]
        public void TestSingleQuotesNotTreatedAsNicknameHawaiianExample()
        {
            var hn = new HumanName("Harietta Keopuolani Nahi'ena'ena");
            Assert.Equal("Harietta", hn.First);
            Assert.Equal("Keopuolani", hn.Middle);
            Assert.Equal("Nahi'ena'ena", hn.Last);
            Assert.Equal("", hn.Nickname);
        }

        [Fact]
        public void TestSingleQuotesNotTreatedAsNicknameKenyanExample()
        {
            var hn = new HumanName("Naomi Wambui Ng'ang'a");
            Assert.Equal("Naomi", hn.First);
            Assert.Equal("Wambui", hn.Middle);
            Assert.Equal("Ng'ang'a", hn.Last);
            Assert.Equal("", hn.Nickname);
        }

        [Fact]
        public void TestSingleQuotesNotTreatedAsNicknameSamoanExample()
        {
            var hn = new HumanName("Va'apu'u Vitale");
            Assert.Equal("Va'apu'u", hn.First);
            Assert.Equal("", hn.Middle);
            Assert.Equal("Vitale", hn.Last);
            Assert.Equal("", hn.Nickname);
        }

        // http://code.google.com/p/python-nameparser/issues/detail?id=17
        [Fact]
        public void TestParenthesisAreRemovedFromName()
        {
            var hn = new HumanName("John Jones (Unknown)");
            Assert.Equal("John", hn.First);
            Assert.Equal("Jones", hn.Last);
            // not testing the nicknames because we don't actually care
            // about Google Docs here
        }

        [Fact]
        public void TestDuplicateParenthesisAreRemovedFromName()
        {
            var hn = new HumanName("John Jones (Google Docs), Jr. (Unknown)");
            Assert.Equal("John", hn.First);
            Assert.Equal("Jones", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void TestNicknameAndLastName()
        {
            var hn = new HumanName("\"Rick\" Edmonds");
            Assert.Equal("", hn.First);
            Assert.Equal("Edmonds", hn.Last);
            Assert.Equal("Rick", hn.Nickname);
        }

        [Fact(Skip = "Expected failure")]
        public void TestNicknameAndLastNameWithTitle()
        {
            var hn = new HumanName("Senator \"Rick\" Edmonds");
            Assert.Equal("Senator", hn.Title);
            Assert.Equal("", hn.First);
            Assert.Equal("Edmonds", hn.Last);
            Assert.Equal("Rick", hn.Nickname);
        }
    }
}