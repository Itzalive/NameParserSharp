using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]
    public class SuffixesTestCase
    {
        [TestMethod]
        public void test_suffix()
        {
            var hn = new HumanName("Joe Franklin Jr");
            Assert.AreEqual("Joe", hn.First);
            Assert.AreEqual("Franklin", hn.Last);
            Assert.AreEqual("Jr", hn.Suffix);
        }

        [TestMethod]
        public void test_suffix_with_periods()
        {
            var hn = new HumanName("Joe Dentist D.D.S.");
            Assert.AreEqual("Joe", hn.First);
            Assert.AreEqual("Dentist", hn.Last);
            Assert.AreEqual("D.D.S.", hn.Suffix);
        }

        [TestMethod]
        public void test_two_suffixes()
        {
            var hn = new HumanName("Kenneth Clarke QC MP");
            Assert.AreEqual("Kenneth", hn.First);
            Assert.AreEqual("Clarke", hn.Last);
            // NOTE: this adds a comma when the original format did not have one.
            // not ideal but at least its in the right bucket
            Assert.AreEqual("QC, MP", hn.Suffix);
        }

        [TestMethod]
        public void test_two_suffixes_lastname_comma_format()
        {
            var hn = new HumanName("Washington Jr. MD, Franklin");
            Assert.AreEqual("Franklin", hn.First);
            Assert.AreEqual("Washington", hn.Last);
            // NOTE: this adds a comma when the original format did not have one.
            Assert.AreEqual("Jr., MD", hn.Suffix);
        }

        [TestMethod]
        public void test_two_suffixes_suffix_comma_format()
        {
            var hn = new HumanName("Franklin Washington, Jr. MD");
            Assert.AreEqual("Franklin", hn.First);
            Assert.AreEqual("Washington", hn.Last);
            Assert.AreEqual("Jr. MD", hn.Suffix);
        }

        [TestMethod]
        public void test_suffix_containing_periods()
        {
            var hn = new HumanName("Kenneth Clarke Q.C.");
            Assert.AreEqual("Kenneth", hn.First);
            Assert.AreEqual("Clarke", hn.Last);
            Assert.AreEqual("Q.C.", hn.Suffix);
        }

        [TestMethod]
        public void test_suffix_containing_periods_lastname_comma_format()
        {
            var hn = new HumanName("Clarke, Kenneth, Q.C. M.P.");
            Assert.AreEqual("Kenneth", hn.First);
            Assert.AreEqual("Clarke", hn.Last);
            Assert.AreEqual("Q.C. M.P.", hn.Suffix);
        }

        [TestMethod]
        public void test_suffix_containing_periods_suffix_comma_format()
        {
            var hn = new HumanName("Kenneth Clarke Q.C., M.P.");
            Assert.AreEqual("Kenneth", hn.First);
            Assert.AreEqual("Clarke", hn.Last);
            Assert.AreEqual("Q.C., M.P.", hn.Suffix);
        }

        [TestMethod]
        public void test_suffix_with_single_comma_format()
        {
            var hn = new HumanName("John Doe jr., MD");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("jr., MD", hn.Suffix);
        }

        [TestMethod]
        public void test_suffix_with_double_comma_format()
        {
            var hn = new HumanName("Doe, John jr., MD");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("jr., MD", hn.Suffix);
        }

        [TestMethod]
        public void test_phd_with_erroneous_space()
        {
            var hn = new HumanName("John Smith, Ph. D.");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Smith", hn.Last);
            Assert.AreEqual("Ph. D.", hn.Suffix);
        }

        [TestMethod]
        public void test_phd_conflict()
        {
            var hn = new HumanName("Adolph D");
            Assert.AreEqual("Adolph", hn.First);
            Assert.AreEqual("D", hn.Last);

            // http://en.wikipedia.org/wiki/Ma_(surname);
        }

        [TestMethod]
        public void test_potential_suffix_that_is_also_last_name()
        {
            var hn = new HumanName("Jack Ma");
            Assert.AreEqual("Jack", hn.First);
            Assert.AreEqual("Ma", hn.Last);
        }

        [TestMethod]
        public void test_potential_suffix_that_is_also_last_name_comma()
        {
            var hn = new HumanName("Ma, Jack");
            Assert.AreEqual("Jack", hn.First);
            Assert.AreEqual("Ma", hn.Last);
        }

        [TestMethod]
        public void test_potential_suffix_that_is_also_last_name_with_suffix()
        {
            var hn = new HumanName("Jack Ma Jr");
            Assert.AreEqual("Jack", hn.First);
            Assert.AreEqual("Ma", hn.Last);
            Assert.AreEqual("Jr", hn.Suffix);
        }

        [TestMethod]
        public void test_potential_suffix_that_is_also_last_name_with_suffix_comma()
        {
            var hn = new HumanName("Ma III, Jack Jr");
            Assert.AreEqual("Jack", hn.First);
            Assert.AreEqual("Ma", hn.Last);
            Assert.AreEqual("III, Jr", hn.Suffix);
        }

        // https://github.com/derek73/python-nameparser/issues/27
        [Ignore("Expected failure")]
        [TestMethod]
        public void test_king()
        {
            var hn = new HumanName("Dr King Jr");
            Assert.AreEqual("Dr", hn.Title);
            Assert.AreEqual("King", hn.Last);
            Assert.AreEqual("Jr", hn.Suffix);
        }

        [TestMethod]
        public void test_multiple_letter_suffix_with_periods()
        {
            var hn = new HumanName("John Doe Msc.Ed.");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Msc.Ed.", hn.Suffix);
        }

        [TestMethod]
        public void test_suffix_with_periods_with_comma()
        {
            var hn = new HumanName("John Doe, Msc.Ed.");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Msc.Ed.", hn.Suffix);
        }

        [TestMethod]
        public void test_suffix_with_periods_with_lastname_comma()
        {
            var hn = new HumanName("Doe, John Msc.Ed.");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Msc.Ed.", hn.Suffix);
        }
    }
}