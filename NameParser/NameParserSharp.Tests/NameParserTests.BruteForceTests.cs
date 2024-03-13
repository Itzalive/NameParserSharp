namespace NameParserSharp.Tests;

using NameParserSharp;

using Xunit;

public partial class NameParserTests {
    public class BruteForceTests {
        [Fact]
        public void Test1() {
            var hn = new HumanName("John Doe");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
        }

        [Fact]
        public void Test2() {
            var hn = new HumanName("John Doe, Jr.");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test3() {
            var hn = new HumanName("John Doe III");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test4() {
            var hn = new HumanName("Doe, John");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
        }

        [Fact]
        public void Test5() {
            var hn = new HumanName("Doe, John, Jr.");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test6() {
            var hn = new HumanName("Doe, John III");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test7() {
            var hn = new HumanName("John A. Doe");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
        }

        [Fact]
        public void Test8() {
            var hn = new HumanName("John A. Doe, Jr");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("Jr", hn.Suffix);
        }

        [Fact]
        public void Test9() {
            var hn = new HumanName("John A. Doe III");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test10() {
            var hn = new HumanName("Doe, John A.");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
        }

        [Fact]
        public void Test11() {
            var hn = new HumanName("Doe, John A., Jr.");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test12() {
            var hn = new HumanName("Doe, John A., III");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test13() {
            var hn = new HumanName("John A. Kenneth Doe");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
        }

        [Fact]
        public void Test14() {
            var hn = new HumanName("John A. Kenneth Doe, Jr.");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test15() {
            var hn = new HumanName("John A. Kenneth Doe III");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test16() {
            var hn = new HumanName("Doe, John. A. Kenneth");
            Assert.Equal("John.", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
        }

        [Fact]
        public void Test17() {
            var hn = new HumanName("Doe, John. A. Kenneth, Jr.");
            Assert.Equal("John.", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test18() {
            var hn = new HumanName("Doe, John. A. Kenneth III");
            Assert.Equal("John.", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test19() {
            var hn = new HumanName("Dr. John Doe");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Dr.", hn.Title);
        }

        [Fact]
        public void Test192() {
            var hn = new HumanName("Dr P Forrest");
            Assert.Equal("P", hn.First);
            Assert.Equal("Forrest", hn.Last);
            Assert.Equal("Dr", hn.Title);
        }

        [Fact]
        public void Test20() {
            var hn = new HumanName("Dr. John Doe, Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test21() {
            var hn = new HumanName("Dr. John Doe III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test22() {
            var hn = new HumanName("Doe, Dr. John");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
        }

        [Fact]
        public void Test23() {
            var hn = new HumanName("Doe, Dr. John, Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test24() {
            var hn = new HumanName("Doe, Dr. John III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test25() {
            var hn = new HumanName("Dr. John A. Doe");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
        }

        [Fact]
        public void Test26() {
            var hn = new HumanName("Dr. John A. Doe, Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test27() {
            var hn = new HumanName("Dr. John A. Doe III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test28() {
            var hn = new HumanName("Doe, Dr. John A.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
        }

        [Fact]
        public void Test29() {
            var hn = new HumanName("Doe, Dr. John A. Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test30() {
            var hn = new HumanName("Doe, Dr. John A. III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("A.", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test31() {
            var hn = new HumanName("Dr. John A. Kenneth Doe");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
        }

        [Fact]
        public void Test32() {
            var hn = new HumanName("Dr. John A. Kenneth Doe, Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test33() {
            var hn = new HumanName("Al Arnold Gore, Jr.");
            Assert.Equal("Arnold", hn.Middle);
            Assert.Equal("Al", hn.First);
            Assert.Equal("Gore", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test34() {
            var hn = new HumanName("Dr. John A. Kenneth Doe III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test35() {
            var hn = new HumanName("Doe, Dr. John A. Kenneth");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
        }

        [Fact]
        public void Test36() {
            var hn = new HumanName("Doe, Dr. John A. Kenneth Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test37() {
            var hn = new HumanName("Doe, Dr. John A. Kenneth III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("A. Kenneth", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test38() {
            var hn = new HumanName("Juan de la Vega");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test39() {
            var hn = new HumanName("Juan de la Vega, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test40() {
            var hn = new HumanName("Juan de la Vega III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test41() {
            var hn = new HumanName("de la Vega, Juan");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test42() {
            var hn = new HumanName("de la Vega, Juan, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test43() {
            var hn = new HumanName("de la Vega, Juan III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test44() {
            var hn = new HumanName("Juan Velasquez y Garcia");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test45() {
            var hn = new HumanName("Juan Velasquez y Garcia, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test46() {
            var hn = new HumanName("Juan Velasquez y Garcia III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test47() {
            var hn = new HumanName("Velasquez y Garcia, Juan");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test48() {
            var hn = new HumanName("Velasquez y Garcia, Juan, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test49() {
            var hn = new HumanName("Velasquez y Garcia, Juan III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test50() {
            var hn = new HumanName("Dr. Juan de la Vega");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test51() {
            var hn = new HumanName("Dr. Juan de la Vega, Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test52() {
            var hn = new HumanName("Dr. Juan de la Vega III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test53() {
            var hn = new HumanName("de la Vega, Dr. Juan");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test54() {
            var hn = new HumanName("de la Vega, Dr. Juan, Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test55() {
            var hn = new HumanName("de la Vega, Dr. Juan III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test56() {
            var hn = new HumanName("Dr. Juan Velasquez y Garcia");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test57() {
            var hn = new HumanName("Dr. Juan Velasquez y Garcia, Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test58() {
            var hn = new HumanName("Dr. Juan Velasquez y Garcia III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test59() {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test60() {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan, Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test61() {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan III");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test62() {
            var hn = new HumanName("Juan Q. de la Vega");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test63() {
            var hn = new HumanName("Juan Q. de la Vega, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test64() {
            var hn = new HumanName("Juan Q. de la Vega III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test65() {
            var hn = new HumanName("de la Vega, Juan Q.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test66() {
            var hn = new HumanName("de la Vega, Juan Q., Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test67() {
            var hn = new HumanName("de la Vega, Juan Q. III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test68() {
            var hn = new HumanName("Juan Q. Velasquez y Garcia");
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test69() {
            var hn = new HumanName("Juan Q. Velasquez y Garcia, Jr.");
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test70() {
            var hn = new HumanName("Juan Q. Velasquez y Garcia III");
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test71() {
            var hn = new HumanName("Velasquez y Garcia, Juan Q.");
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test72() {
            var hn = new HumanName("Velasquez y Garcia, Juan Q., Jr.");
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test73() {
            var hn = new HumanName("Velasquez y Garcia, Juan Q. III");
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test74() {
            var hn = new HumanName("Dr. Juan Q. de la Vega");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test75() {
            var hn = new HumanName("Dr. Juan Q. de la Vega, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test76() {
            var hn = new HumanName("Dr. Juan Q. de la Vega III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test77() {
            var hn = new HumanName("de la Vega, Dr. Juan Q.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Dr.", hn.Title);
        }

        [Fact]
        public void Test78() {
            var hn = new HumanName("de la Vega, Dr. Juan Q., Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
            Assert.Equal("Dr.", hn.Title);
        }

        [Fact]
        public void Test79() {
            var hn = new HumanName("de la Vega, Dr. Juan Q. III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("III", hn.Suffix);
            Assert.Equal("Dr.", hn.Title);
        }

        [Fact]
        public void Test80() {
            var hn = new HumanName("Dr. Juan Q. Velasquez y Garcia");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test81() {
            var hn = new HumanName("Dr. Juan Q. Velasquez y Garcia, Jr.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test82() {
            var hn = new HumanName("Dr. Juan Q. Velasquez y Garcia III");
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test83() {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q.");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test84() {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q., Jr.");
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test85() {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q. III");
            Assert.Equal("Q.", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test86() {
            var hn = new HumanName("Juan Q. Xavier de la Vega");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test87() {
            var hn = new HumanName("Juan Q. Xavier de la Vega, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test88() {
            var hn = new HumanName("Juan Q. Xavier de la Vega III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test89() {
            var hn = new HumanName("de la Vega, Juan Q. Xavier");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test90() {
            var hn = new HumanName("de la Vega, Juan Q. Xavier, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test91() {
            var hn = new HumanName("de la Vega, Juan Q. Xavier III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test92() {
            var hn = new HumanName("Dr. Juan Q. Xavier de la Vega");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test93() {
            var hn = new HumanName("Dr. Juan Q. Xavier de la Vega, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test94() {
            var hn = new HumanName("Dr. Juan Q. Xavier de la Vega III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test95() {
            var hn = new HumanName("de la Vega, Dr. Juan Q. Xavier");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("de la Vega", hn.Last);
        }

        [Fact]
        public void Test96() {
            var hn = new HumanName("de la Vega, Dr. Juan Q. Xavier, Jr.");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test97() {
            var hn = new HumanName("de la Vega, Dr. Juan Q. Xavier III");
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("de la Vega", hn.Last);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test98() {
            var hn = new HumanName("Juan Q. Xavier Velasquez y Garcia");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test99() {
            var hn = new HumanName("Juan Q. Xavier Velasquez y Garcia, Jr.");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test100() {
            var hn = new HumanName("Juan Q. Xavier Velasquez y Garcia III");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test101() {
            var hn = new HumanName("Velasquez y Garcia, Juan Q. Xavier");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test102() {
            var hn = new HumanName("Velasquez y Garcia, Juan Q. Xavier, Jr.");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test103() {
            var hn = new HumanName("Velasquez y Garcia, Juan Q. Xavier III");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test104() {
            var hn = new HumanName("Dr. Juan Q. Xavier Velasquez y Garcia");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test105() {
            var hn = new HumanName("Dr. Juan Q. Xavier Velasquez y Garcia, Jr.");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test106() {
            var hn = new HumanName("Dr. Juan Q. Xavier Velasquez y Garcia III");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test107() {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q. Xavier");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Velasquez y Garcia", hn.Last);
        }

        [Fact]
        public void Test108() {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q. Xavier, Jr.");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("Jr.", hn.Suffix);
        }

        [Fact]
        public void Test109() {
            var hn = new HumanName("Velasquez y Garcia, Dr. Juan Q. Xavier III");
            Assert.Equal("Q. Xavier", hn.Middle);
            Assert.Equal("Juan", hn.First);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("Velasquez y Garcia", hn.Last);
            Assert.Equal("III", hn.Suffix);
        }

        [Fact]
        public void Test110() {
            var hn = new HumanName("John Doe, CLU, CFP, LUTC");
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("CLU, CFP, LUTC", hn.Suffix);
        }

        [Fact]
        public void Test111() {
            var hn = new HumanName("John P. Doe, CLU, CFP, LUTC");
            Assert.Equal("John", hn.First);
            Assert.Equal("P.", hn.Middle);
            Assert.Equal("Doe", hn.Last);
            Assert.Equal("CLU, CFP, LUTC", hn.Suffix);
        }

        [Fact]
        public void Test112() {
            var hn = new HumanName("Dr. John P. Doe-Ray, CLU, CFP, LUTC");
            Assert.Equal("John", hn.First);
            Assert.Equal("P.", hn.Middle);
            Assert.Equal("Doe-Ray", hn.Last);
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("CLU, CFP, LUTC", hn.Suffix);
        }

        [Fact]
        public void Test113() {
            var hn = new HumanName("Doe-Ray, Dr. John P., CLU, CFP, LUTC");
            Assert.Equal("Dr.", hn.Title);
            Assert.Equal("P.", hn.Middle);
            Assert.Equal("John", hn.First);
            Assert.Equal("Doe-Ray", hn.Last);
            Assert.Equal("CLU, CFP, LUTC", hn.Suffix);
        }

        [Fact]
        public void Test115() {
            var hn = new HumanName("Hon. Barrington P. Doe-Ray, Jr.");
            Assert.Equal("Hon.", hn.Title);
            Assert.Equal("P.", hn.Middle);
            Assert.Equal("Barrington", hn.First);
            Assert.Equal("Doe-Ray", hn.Last);
        }

        [Fact]
        public void Test116() {
            var hn = new HumanName("Doe-Ray, Hon. Barrington P. Jr., CFP, LUTC");
            Assert.Equal("Hon.", hn.Title);
            Assert.Equal("P.", hn.Middle);
            Assert.Equal("Barrington", hn.First);
            Assert.Equal("Doe-Ray", hn.Last);
            Assert.Equal("Jr., CFP, LUTC", hn.Suffix);
        }

        [Fact]
        public void Test117() {
            var hn = new HumanName("Rt. Hon. Paul E. Mary");
            Assert.Equal("Rt. Hon.", hn.Title);
            Assert.Equal("Paul", hn.First);
            Assert.Equal("E.", hn.Middle);
            Assert.Equal("Mary", hn.Last);
        }

        [Fact]
        public void Test119() {
            var hn = new HumanName("Lord God Almighty");
            Assert.Equal("Lord", hn.Title);
            Assert.Equal("God", hn.First);
            Assert.Equal("Almighty", hn.Last);
        }
    }
}