using Cedeira.Essentials.NET.System.Range;

namespace Cedeira.Essentials.NET_unittests.System.Range
{
    [TestClass]
    public class DateRangeTest
    {
        [TestMethod]
        public void DateRange_Create()
        {
            DateRange dateRange = new DateRange(DateTime.Now, DateTime.Now.AddDays(2));

            Assert.IsNotNull(dateRange);
        }

        [TestMethod]
        public void DateRange_Contains()
        {
            DateRange dateRange = new DateRange(DateTime.Now, DateTime.Now.AddDays(2));

            Assert.IsTrue(dateRange.Contains(DateTime.Now.AddDays(1)));
        }

        [TestMethod]
        public void DateRange_OverlapsWith()
        {
            DateRange dateRange = new DateRange(DateTime.Now, DateTime.Now.AddDays(2));
            DateRange other = new DateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));

            Assert.IsTrue(dateRange.OverlapsWith(other));
        }

        [TestMethod]
        public void DateRange_Intersect()
        {
            DateRange dateRange = new DateRange(DateTime.Now, DateTime.Now.AddDays(2));
            DateRange other = new DateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));

            DateRange? intersection = dateRange.Intersect(other);

            var prueb = DateTime.Now.AddDays(1).ToString("ddMMyyyy");

            Assert.IsNotNull(intersection);
            Assert.AreEqual(DateTime.Now.AddDays(1).ToString("ddMMyyyy"), intersection.Value.Start.ToString("ddMMyyyy"));
            Assert.AreEqual(DateTime.Now.AddDays(2).ToString("ddMMyyyy"), intersection.Value.End.ToString("ddMMyyyy"));
        }

        [TestMethod]
        public void DateRange_GetHashCode()
        {
            DateRange dateRange = new DateRange(DateTime.Now, DateTime.Now.AddDays(2));

            Assert.IsNotNull(dateRange.GetHashCode());
        }

        [TestMethod]
        public void DateRange_Equals()
        {
            DateRange dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(2).Date);
            DateRange other = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(2).Date);

            Assert.IsTrue(dateRange.Equals(other));
        }

        [TestMethod]
        public void DateRange_OperatorEqual()
        {
            DateRange dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(2).Date);
            DateRange other = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(2).Date);

            Assert.IsTrue(dateRange == other);
        }

        [TestMethod]
        public void DateRange_OperatorNotEqual()
        {
            DateRange dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(2).Date);
            DateRange other = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(1).Date);

            Assert.IsTrue(dateRange != other);
        }

        [TestMethod]
        public void DateRange_ToString()
        {
            DateRange dateRange = new DateRange(DateTime.Now, DateTime.Now.AddDays(2));

            Assert.IsNotNull(dateRange.ToString());
        }

        [TestMethod]
        public void DateRange_Create_InvalidRange_ThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => new DateRange(DateTime.Now.AddDays(2), DateTime.Now));
            Assert.AreEqual("The start date must be earlier than or equal to the end date.", exception.Message);
        }
    }
}