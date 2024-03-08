using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]
    public class CapitalizationTests
    {
        [TestMethod]
        public void test_capitalization_exception_for_III()
        {
            var hn = new HumanName("juan q. xavier velasquez y garcia iii");
            hn.Normalize();
            Assert.AreEqual("Juan Q. Xavier Velasquez y Garcia III", hn.ToString());
        }

        // FIXME: this test does not pass due to a known issue
        // http://code.google.com/p/python-nameparser/issues/detail?id=22
        [Ignore("Expected failure")]
        [TestMethod]
        public void test_capitalization_exception_for_already_capitalized_III_KNOWN_FAILURE()
        {
            var hn = new HumanName("juan garcia III");
            hn.Normalize();
            Assert.AreEqual("Juan Garcia III", hn.ToString());
        }

        [TestMethod]
        public void test_capitalize_title()
        {
            var hn = new HumanName("lt. gen. john a. kenneth doe iv");
            hn.Normalize();
            Assert.AreEqual("Lt. Gen. John A. Kenneth Doe IV", hn.ToString());
        }

        [TestMethod]
        public void test_capitalize_title_to_lower()
        {
            var hn = new HumanName("LT. GEN. JOHN A. KENNETH DOE IV");
            hn.Normalize();
            Assert.AreEqual("Lt. Gen. John A. Kenneth Doe IV", hn.ToString());
        }

        // Capitalization with M(a)c and hyphenated names
        [TestMethod]
        public void test_capitalization_with_Mac_as_hyphenated_names()
        {
            var hn = new HumanName("donovan mcnabb-smith");
            hn.Normalize();
            Assert.AreEqual("Donovan McNabb-Smith", hn.ToString());
        }

        [TestMethod]
        public void test_capitization_middle_initial_is_also_a_conjunction()
        {
            var hn = new HumanName("scott e. werner");
            hn.Normalize();
            Assert.AreEqual("Scott E. Werner", hn.ToString());
        }

        // Leaving already-capitalized names alone
        [TestMethod]
        public void test_no_change_to_mixed_chase()
        {
            var hn = new HumanName("Shirley Maclaine");
            hn.Normalize();
            Assert.AreEqual("Shirley Maclaine", hn.ToString());
        }

        [TestMethod]
        public void test_force_capitalization()
        {
            var hn = new HumanName("Shirley Maclaine");
            hn.Normalize(true);
            Assert.AreEqual("Shirley MacLaine", hn.ToString());
        }

        [TestMethod]
        public void test_capitalize_diacritics()
        {
            var hn = new HumanName("matthëus schmidt");
            hn.Normalize();
            Assert.AreEqual("Matthëus Schmidt", hn.ToString());
        }

        // http://code.google.com/p/python-nameparser/issues/detail?id=15
        [TestMethod]
        public void test_downcasing_mac()
        {
            var hn = new HumanName("RONALD MACDONALD");
            hn.Normalize();
            Assert.AreEqual("Ronald MacDonald", hn.ToString());
        }

        // http://code.google.com/p/python-nameparser/issues/detail?id=23
        [TestMethod]
        public void test_downcasing_mc()
        {
            var hn = new HumanName("RONALD MCDONALD");
            hn.Normalize();
            Assert.AreEqual("Ronald McDonald", hn.ToString());
        }

        [TestMethod]
        public void test_short_names_with_mac()
        {
            var hn = new HumanName("mack johnson");
            hn.Normalize();
            Assert.AreEqual("Mack Johnson", hn.ToString());
        }

        [TestMethod]
        public void test_portuguese_prefixes()
        {
            var hn = new HumanName("joao da silva do amaral de souza");
            hn.Normalize();
            Assert.AreEqual("Joao da Silva do Amaral de Souza", hn.ToString());
        }

        [TestMethod]
        public void test_capitalize_prefix_clash_on_first_name()
        {
            var hn = new HumanName("van nguyen");
            hn.Normalize();
            Assert.AreEqual("Van Nguyen", hn.ToString());
        }
    }
}