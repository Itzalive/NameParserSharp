using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]
    public class PrefixesTests
    {
        [TestMethod]
        public void test_prefix()
        {
            var hn = new HumanName("Juan del Sur");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("del Sur", hn.Last);
        }

        [TestMethod]
        public void test_prefix_with_period()
        {
            var hn = new HumanName("Jill St. John");
            Assert.AreEqual("Jill", hn.First);
            Assert.AreEqual("St. John", hn.Last);
        }

        [TestMethod]
        public void test_prefix_before_two_part_last_name()
        {
            var hn = new HumanName("pennie von bergen wessels");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
        }

        [TestMethod]
        public void test_prefix_is_first_name()
        {
            var hn = new HumanName("Van Johnson");
            Assert.AreEqual("Van", hn.First);
            Assert.AreEqual("Johnson", hn.Last);
        }

        [TestMethod]
        public void test_prefix_is_first_name_with_middle_name()
        {
            var hn = new HumanName("Van Jeremy Johnson");
            Assert.AreEqual("Van", hn.First);
            Assert.AreEqual("Jeremy", hn.Middle);
            Assert.AreEqual("Johnson", hn.Last);
        }

        [TestMethod]
        public void test_prefix_before_two_part_last_name_with_suffix()
        {
            var hn = new HumanName("pennie von bergen wessels III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void test_prefix_before_two_part_last_name_with_acronym_suffix()
        {
            var hn = new HumanName("pennie von bergen wessels M.D.");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("M.D.", hn.Suffix);
        }

        [TestMethod]
        public void test_two_part_last_name_with_suffix_comma()
        {
            var hn = new HumanName("pennie von bergen wessels, III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void test_two_part_last_name_with_suffix()
        {
            var hn = new HumanName("von bergen wessels, pennie III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void test_last_name_two_part_last_name_with_two_suffixes()
        {
            var hn = new HumanName("von bergen wessels MD, pennie III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("MD, III", hn.Suffix);
        }

        [TestMethod]
        public void test_comma_two_part_last_name_with_acronym_suffix()
        {
            var hn = new HumanName("von bergen wessels, pennie MD");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("MD", hn.Suffix);
        }

        [TestMethod]
        public void test_comma_two_part_last_name_with_suffix_in_first_part()
        {
            // I'm kinda surprised this works, not really sure if this is a
            // realistic place for a suffix to be.
            var hn = new HumanName("von bergen wessels MD, pennie");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("MD", hn.Suffix);
        }

        [TestMethod]
        public void test_title_two_part_last_name_with_suffix_in_first_part()
        {
            var hn = new HumanName("pennie von bergen wessels MD, III");
            Assert.AreEqual("pennie", hn.First);
            Assert.AreEqual("von bergen wessels", hn.Last);
            Assert.AreEqual("MD, III", hn.Suffix);
        }

        [TestMethod]
        public void test_portuguese_dos()
        {
            var hn = new HumanName("Rafael Sousa dos Anjos");
            Assert.AreEqual("Rafael", hn.First);
            Assert.AreEqual("Sousa", hn.Middle);
            Assert.AreEqual("dos Anjos", hn.Last);
        }

        [TestMethod]
        public void test_portuguese_prefixes()
        {
            var hn = new HumanName("Joao da Silva do Amaral de Souza");
            Assert.AreEqual("Joao", hn.First);
            Assert.AreEqual("da Silva do Amaral", hn.Middle);
            Assert.AreEqual("de Souza", hn.Last);
        }

        [TestMethod]
        public void test_three_conjunctions()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier de la dos Vega III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la dos Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void test_lastname_three_conjunctions()
        {
            var hn = new HumanName("de la dos Vega, Dr. Juan Q. Xavier III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la dos Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void test_comma_three_conjunctions()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier de la dos Vega, III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la dos Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }
    }
}