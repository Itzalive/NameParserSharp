using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameParser;

namespace NameParserTest;

public partial class NameParserTests
{
    [TestClass]
    public class BruteForceTests
    {
        [TestMethod]
        public void Test1()
        {
            var hn = new HumanName("John Doe");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
        }

        [TestMethod]
        public void Test2()
        {
            var hn = new HumanName("John Doe, Jr.");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test3()
        {
            var hn = new HumanName("John Doe III");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test4()
        {
            var hn = new HumanName("Doe, John");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
        }

        [TestMethod]
        public void Test5()
        {
            var hn = new HumanName("Doe, John, Jr.");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test6()
        {
            var hn = new HumanName("Doe, John III");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test7()
        {
            var hn = new HumanName("John A. Doe");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
        }

        [TestMethod]
        public void Test8()
        {
            var hn = new HumanName("John A. Doe, Jr");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("Jr", hn.Suffix);
        }

        [TestMethod]
        public void Test9()
        {
            var hn = new HumanName("John A. Doe III");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test10()
        {
            var hn = new HumanName("Doe, John A.");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
        }

        [TestMethod]
        public void Test11()
        {
            var hn = new HumanName("Doe, John A., Jr.");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test12()
        {
            var hn = new HumanName("Doe, John A., III");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test13()
        {
            var hn = new HumanName("John A. Kenneth Doe");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
        }

        [TestMethod]
        public void Test14()
        {
            var hn = new HumanName("John A. Kenneth Doe, Jr.");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test15()
        {
            var hn = new HumanName("John A. Kenneth Doe III");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test16()
        {
            var hn = new HumanName("Doe, John. A. Kenneth");
            Assert.AreEqual("John.", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
        }

        [TestMethod]
        public void Test17()
        {
            var hn = new HumanName("Doe, John. A. Kenneth, Jr.");
            Assert.AreEqual("John.", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test18()
        {
            var hn = new HumanName("Doe, John. A. Kenneth III");
            Assert.AreEqual("John.", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test19()
        {
            var hn = new HumanName("Dr. John Doe");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
        }

        [TestMethod]
        public void Test19_2()
        {
            var hn = new HumanName("Dr P Forrest");
            Assert.AreEqual("P", hn.First);
            Assert.AreEqual("Forrest", hn.Last);
            Assert.AreEqual("Dr", hn.Title);
        }

        [TestMethod]
        public void Test20()
        {
            var hn = new HumanName("Dr. John Doe, Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test21()
        {
            var hn = new HumanName("Dr. John Doe III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test22()
        {
            var hn = new HumanName("Doe, Dr. John");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
        }

        [TestMethod]
        public void Test23()
        {
            var hn = new HumanName("Doe, Dr. John, Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test24()
        {
            var hn = new HumanName("Doe, Dr. John III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test25()
        {
            var hn = new HumanName("Dr. John A. Doe");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
        }

        [TestMethod]
        public void Test26()
        {
            var hn = new HumanName("Dr. John A. Doe, Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test27()
        {
            var hn = new HumanName("Dr. John A. Doe III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test28()
        {
            var hn = new HumanName("Doe, Dr. John A.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
        }

        [TestMethod]
        public void Test29()
        {
            var hn = new HumanName("Doe, Dr. John A. Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test30()
        {
            var hn = new HumanName("Doe, Dr. John A. III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("A.", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test31()
        {
            var hn = new HumanName("Dr. John A. Kenneth Doe");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
        }

        [TestMethod]
        public void Test32()
        {
            var hn = new HumanName("Dr. John A. Kenneth Doe, Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test33()
        {
            var hn = new HumanName("Al Arnold Gore, Jr.");
            Assert.AreEqual("Arnold", hn.Middle);
            Assert.AreEqual("Al", hn.First);
            Assert.AreEqual("Gore", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test34()
        {
            var hn = new HumanName("Dr. John A. Kenneth Doe III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test35()
        {
            var hn = new HumanName("Doe, Dr. John A. Kenneth");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
        }

        [TestMethod]
        public void Test36()
        {
            var hn = new HumanName("Doe, Dr. John A. Kenneth Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test37()
        {
            var hn = new HumanName("Doe, Dr. John A. Kenneth III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("A. Kenneth", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test38()
        {
            var hn = new HumanName("Juan de la Vega");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test39()
        {
            var hn = new HumanName("Juan de la Vega, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test40()
        {
            var hn = new HumanName("Juan de la Vega III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test41()
        {
            var hn = new HumanName("de la Vega, Juan");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test42()
        {
            var hn = new HumanName("de la Vega, Juan, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test43()
        {
            var hn = new HumanName("de la Vega, Juan III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test44()
        {
            var hn = new HumanName("Juan Velasquez y Garcia");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test45()
        {
            var hn = new HumanName("Juan Velasquez y Garcia, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test46()
        {
            var hn = new HumanName("Juan Velasquez y Garcia III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test47()
        {
            var hn = new HumanName("Velasquez y Garcia, Juan");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test48()
        {
            var hn = new HumanName("Velasquez y Garcia, Juan, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test49()
        {
            var hn = new HumanName("Velasquez y Garcia, Juan III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test50()
        {
            var hn = new HumanName("Dr. Juan de la Vega");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test51()
        {
            var hn = new HumanName("Dr. Juan de la Vega, Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test52()
        {
            var hn = new HumanName("Dr. Juan de la Vega III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test53()
        {
            var hn = new HumanName("de la Vega, Dr. Juan");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test54()
        {
            var hn = new HumanName("de la Vega, Dr. Juan, Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test55()
        {
            var hn = new HumanName("de la Vega, Dr. Juan III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test56()
        {
            var hn = new HumanName("Dr. Juan Velasquez y Garcia");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test57()
        {
            var hn = new HumanName("Dr. Juan Velasquez y Garcia, Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test58()
        {
            var hn = new HumanName("Dr. Juan Velasquez y Garcia III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test59()
        {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test60()
        {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan, Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test61()
        {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan III");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test62()
        {
            var hn = new HumanName("Juan Q. de la Vega");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test63()
        {
            var hn = new HumanName("Juan Q. de la Vega, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test64()
        {
            var hn = new HumanName("Juan Q. de la Vega III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test65()
        {
            var hn = new HumanName("de la Vega, Juan Q.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test66()
        {
            var hn = new HumanName("de la Vega, Juan Q., Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test67()
        {
            var hn = new HumanName("de la Vega, Juan Q. III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test68()
        {
            var hn = new HumanName("Juan Q. Velasquez y Garcia");
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test69()
        {
            var hn = new HumanName("Juan Q. Velasquez y Garcia, Jr.");
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test70()
        {
            var hn = new HumanName("Juan Q. Velasquez y Garcia III");
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test71()
        {
            var hn = new HumanName("Velasquez y Garcia, Juan Q.");
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test72()
        {
            var hn = new HumanName("Velasquez y Garcia, Juan Q., Jr.");
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test73()
        {
            var hn = new HumanName("Velasquez y Garcia, Juan Q. III");
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test74()
        {
            var hn = new HumanName("Dr. Juan Q. de la Vega");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test75()
        {
            var hn = new HumanName("Dr. Juan Q. de la Vega, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test76()
        {
            var hn = new HumanName("Dr. Juan Q. de la Vega III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test77()
        {
            var hn = new HumanName("de la Vega, Dr. Juan Q.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
        }

        [TestMethod]
        public void Test78()
        {
            var hn = new HumanName("de la Vega, Dr. Juan Q., Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
            Assert.AreEqual("Dr.", hn.Title);
        }

        [TestMethod]
        public void Test79()
        {
            var hn = new HumanName("de la Vega, Dr. Juan Q. III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
            Assert.AreEqual("Dr.", hn.Title);
        }

        [TestMethod]
        public void Test80()
        {
            var hn = new HumanName("Dr. Juan Q. Velasquez y Garcia");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test81()
        {
            var hn = new HumanName("Dr. Juan Q. Velasquez y Garcia, Jr.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test82()
        {
            var hn = new HumanName("Dr. Juan Q. Velasquez y Garcia III");
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test83()
        {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q.");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test84()
        {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q., Jr.");
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test85()
        {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q. III");
            Assert.AreEqual("Q.", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test86()
        {
            var hn = new HumanName("Juan Q. Xavier de la Vega");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test87()
        {
            var hn = new HumanName("Juan Q. Xavier de la Vega, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test88()
        {
            var hn = new HumanName("Juan Q. Xavier de la Vega III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test89()
        {
            var hn = new HumanName("de la Vega, Juan Q. Xavier");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test90()
        {
            var hn = new HumanName("de la Vega, Juan Q. Xavier, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test91()
        {
            var hn = new HumanName("de la Vega, Juan Q. Xavier III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test92()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier de la Vega");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test93()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier de la Vega, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test94()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier de la Vega III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test95()
        {
            var hn = new HumanName("de la Vega, Dr. Juan Q. Xavier");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("de la Vega", hn.Last);
        }

        [TestMethod]
        public void Test96()
        {
            var hn = new HumanName("de la Vega, Dr. Juan Q. Xavier, Jr.");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test97()
        {
            var hn = new HumanName("de la Vega, Dr. Juan Q. Xavier III");
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("de la Vega", hn.Last);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test98()
        {
            var hn = new HumanName("Juan Q. Xavier Velasquez y Garcia");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test99()
        {
            var hn = new HumanName("Juan Q. Xavier Velasquez y Garcia, Jr.");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test100()
        {
            var hn = new HumanName("Juan Q. Xavier Velasquez y Garcia III");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test101()
        {
            var hn = new HumanName("Velasquez y Garcia, Juan Q. Xavier");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test102()
        {
            var hn = new HumanName("Velasquez y Garcia, Juan Q. Xavier, Jr.");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test103()
        {
            var hn = new HumanName("Velasquez y Garcia, Juan Q. Xavier III");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test104()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier Velasquez y Garcia");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test105()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier Velasquez y Garcia, Jr.");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test106()
        {
            var hn = new HumanName("Dr. Juan Q. Xavier Velasquez y Garcia III");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test107()
        {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q. Xavier");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
        }

        [TestMethod]
        public void Test108()
        {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q. Xavier, Jr.");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("Jr.", hn.Suffix);
        }

        [TestMethod]
        public void Test109()
        {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q. Xavier III");
            Assert.AreEqual("Q. Xavier", hn.Middle);
            Assert.AreEqual("Juan", hn.First);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("Velasquez y Garcia", hn.Last);
            Assert.AreEqual("III", hn.Suffix);
        }

        [TestMethod]
        public void Test110()
        {
            var hn = new HumanName("John Doe, CLU, CFP, LUTC");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("CLU, CFP, LUTC", hn.Suffix);
        }

        [TestMethod]
        public void Test111()
        {
            var hn = new HumanName("John P. Doe, CLU, CFP, LUTC");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("P.", hn.Middle);
            Assert.AreEqual("Doe", hn.Last);
            Assert.AreEqual("CLU, CFP, LUTC", hn.Suffix);
        }

        [TestMethod]
        public void Test112()
        {
            var hn = new HumanName("Dr. John P. Doe-Ray, CLU, CFP, LUTC");
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("P.", hn.Middle);
            Assert.AreEqual("Doe-Ray", hn.Last);
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("CLU, CFP, LUTC", hn.Suffix);
        }

        [TestMethod]
        public void Test113()
        {
            var hn = new HumanName("Doe-Ray, Dr. John P., CLU, CFP, LUTC");
            Assert.AreEqual("Dr.", hn.Title);
            Assert.AreEqual("P.", hn.Middle);
            Assert.AreEqual("John", hn.First);
            Assert.AreEqual("Doe-Ray", hn.Last);
            Assert.AreEqual("CLU, CFP, LUTC", hn.Suffix);
        }

        [TestMethod]
        public void Test115()
        {
            var hn = new HumanName("Hon. Barrington P. Doe-Ray, Jr.");
            Assert.AreEqual("Hon.", hn.Title);
            Assert.AreEqual("P.", hn.Middle);
            Assert.AreEqual("Barrington", hn.First);
            Assert.AreEqual("Doe-Ray", hn.Last);
        }

        [TestMethod]
        public void Test116()
        {
            var hn = new HumanName("Doe-Ray, Hon. Barrington P. Jr., CFP, LUTC");
            Assert.AreEqual("Hon.", hn.Title);
            Assert.AreEqual("P.", hn.Middle);
            Assert.AreEqual("Barrington", hn.First);
            Assert.AreEqual("Doe-Ray", hn.Last);
            Assert.AreEqual("Jr., CFP, LUTC", hn.Suffix);
        }

        [TestMethod]
        public void Test117()
        {
            var hn = new HumanName("Rt. Hon. Paul E. Mary");
            Assert.AreEqual("Rt. Hon.", hn.Title);
            Assert.AreEqual("Paul", hn.First);
            Assert.AreEqual("E.", hn.Middle);
            Assert.AreEqual("Mary", hn.Last);
        }

        [TestMethod]
        public void Test119()
        {
            var hn = new HumanName("Lord God Almighty");
            Assert.AreEqual("Lord", hn.Title);
            Assert.AreEqual("God", hn.First);
            Assert.AreEqual("Almighty", hn.Last);
        }
    }
}