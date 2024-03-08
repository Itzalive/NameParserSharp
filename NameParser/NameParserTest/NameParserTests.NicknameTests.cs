using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]

    public class NicknameTests
    {
        // https://code.google.com/p/python-nameparser/issues/detail?id=33
        [TestMethod]
        public void TestNicknameInParenthesis()
        {
            var hn = new HumanName("Benjamin (Ben) Franklin");
            Assert.AreEqual("Benjamin", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("Franklin", hn.Last);
            Assert.AreEqual("Ben", hn.Nickname);
        }

        [TestMethod]
        public void TestTwoWordNicknameInParenthesis()
        {
            var hn = new HumanName("Benjamin (Big Ben) Franklin");
            Assert.AreEqual("Benjamin", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("Franklin", hn.Last);
            Assert.AreEqual("Big Ben", hn.Nickname);
        }

        [TestMethod]
        public void TestTwoWordsInQuotes()
        {
            var hn = new HumanName("Benjamin \"Big Ben\" Franklin");
            Assert.AreEqual("Benjamin", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("Franklin", hn.Last);
            Assert.AreEqual("Big Ben", hn.Nickname);
        }

        [TestMethod]
        public void TestNicknameInParenthesisWithComma()
        {
            var hn = new HumanName("Franklin, Benjamin (Ben)");
            Assert.AreEqual("Benjamin", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("Franklin", hn.Last);
            Assert.AreEqual("Ben", hn.Nickname);
        }

        [TestMethod]
        public void TestNicknameInParenthesisWithCommaAndSuffix()
        {
            var hn = new HumanName("Franklin, Benjamin (Ben), Jr.");
            Assert.AreEqual("Benjamin", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("Franklin", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
            Assert.AreEqual("Ben", hn.Nickname);
        }

        [TestMethod]
        public void TestNicknameInSingleQuotes()
        {
            var hn = new HumanName("Benjamin 'Ben' Franklin");
            Assert.AreEqual("Benjamin", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("Franklin", hn.Last);
            Assert.AreEqual("Ben", hn.Nickname);
        }

        [TestMethod]
        public void TestNicknameInDoubleQuotes()
        {
            var hn = new HumanName("Benjamin \"Ben\" Franklin");
            Assert.AreEqual("Benjamin", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("Franklin", hn.Last);
            Assert.AreEqual("Ben", hn.Nickname);
        }

        [TestMethod]
        public void TestSingleQuotesOnFirstNameNotTreatedAsNickname()
        {
            var hn = new HumanName("Brian Andrew O'connor");
            Assert.AreEqual("Brian", hn.First);
            Assert.AreEqual("Andrew", hn.Middle);
            Assert.AreEqual("O'connor", hn.Last);
            Assert.AreEqual("", hn.Nickname);
        }

        [TestMethod]
        public void TestSingleQuotesOnBothNameNotTreatedAsNickname()
        {
            var hn = new HumanName("La'tanya O'connor");
            Assert.AreEqual("La'tanya", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("O'connor", hn.Last);
            Assert.AreEqual("", hn.Nickname);
        }

        [TestMethod]
        public void TestSingleQuotesOnEndOfLastNameNotTreatedAsNickname()
        {
            var hn = new HumanName("Mari' Aube'");
            Assert.AreEqual("Mari'", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("Aube'", hn.Last);
            Assert.AreEqual("", hn.Nickname);
        }

        [TestMethod]
        public void TestOkinaInsideNameNotTreatedAsNickname()
        {
            var hn = new HumanName("Harrieta Keōpūolani Nāhiʻenaʻena");
            Assert.AreEqual("Harrieta", hn.First);
            Assert.AreEqual("Keōpūolani", hn.Middle);
            Assert.AreEqual("Nāhiʻenaʻena", hn.Last);
            Assert.AreEqual("", hn.Nickname);
        }

        [TestMethod]
        public void TestSingleQuotesNotTreatedAsNicknameHawaiianExample()
        {
            var hn = new HumanName("Harietta Keopuolani Nahi'ena'ena");
            Assert.AreEqual("Harietta", hn.First);
            Assert.AreEqual("Keopuolani", hn.Middle);
            Assert.AreEqual("Nahi'ena'ena", hn.Last);
            Assert.AreEqual("", hn.Nickname);
        }

        [TestMethod]
        public void TestSingleQuotesNotTreatedAsNicknameKenyanExample()
        {
            var hn = new HumanName("Naomi Wambui Ng'ang'a");
            Assert.AreEqual("Naomi", hn.First);
            Assert.AreEqual("Wambui", hn.Middle);
            Assert.AreEqual("Ng'ang'a", hn.Last);
            Assert.AreEqual("", hn.Nickname);
        }

        [TestMethod]
        public void TestSingleQuotesNotTreatedAsNicknameSamoanExample()
        {
            var hn = new HumanName("Va'apu'u Vitale");
            Assert.AreEqual("Va'apu'u", hn.First);
            Assert.AreEqual("", hn.Middle);
            Assert.AreEqual("Vitale", hn.Last);
            Assert.AreEqual("", hn.Nickname);
        }

        // http://code.google.com/p/python-nameparser/issues/detail?id=17
        [TestMethod]
        public void TestParenthesisAreRemovedFromName()
        {
            var hn = new HumanName("John Jones (Unknown)");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Jones", hn.Last);
            // not testing the nicknames because we don't actually care
            // about Google Docs here
        }

        [TestMethod]
        public void TestDuplicateParenthesisAreRemovedFromName()
        {
            var hn = new HumanName("John Jones (Google Docs), Jr. (Unknown)");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Jones", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void TestNicknameAndLastName()
        {
            var hn = new HumanName("\"Rick\" Edmonds");
            Assert.AreEqual("", hn.First);
            Assert.AreEqual("Edmonds", hn.Last);
            Assert.AreEqual("Rick", hn.Nickname);
        }

        [Ignore("Expected failure")]
        [TestMethod]
        public void TestNicknameAndLastNameWithTitle()
        {
            var hn = new HumanName("Senator \"Rick\" Edmonds");
            Assert.AreEqual("Senator", hn.Title);
            Assert.AreEqual("", hn.First);
            Assert.AreEqual("Edmonds", hn.Last);
            Assert.AreEqual("Rick", hn.Nickname);
        }
    }
}