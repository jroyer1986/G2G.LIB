using DisbursementDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace DisbursementDashboard
{
    public partial class Program : System.Web.UI.Page
    {
        protected string ConvertListToArray<T>(List<T> list)
        {
            return string.Format("[{0}]", string.Join(",", list));
        }

        //Chart Data
        #region
        protected List<string> dailyDates = new List<string>();
        protected List<int> dailyNewDisbursements = new List<int>();
        protected List<int> dailyNewDisbursementsG2G = new List<int>();
        protected List<int> dailyNewDisbursementsEx = new List<int>();

        protected string dailyNewDisbursementsJS;
        protected string dailyDatesJS;
        protected string dailyNewDisbursementsG2GJS;
        protected string dailyNewDisbursementsExJS;
        
        protected List<string> weeklyDates = new List<string>();
        protected List<int> weeklyNewDisbursements = new List<int>();
        protected List<int> weeklyNewDisbursementsG2G = new List<int>();
        protected List<int> weeklyNewDisbursementsEx = new List<int>();

        protected string weeklyNewDisbursementsJS;
        protected string weeklyDatesJS;
        protected string weeklyNewDisbursementsG2GJS;
        protected string weeklyNewDisbursementsExJS;

        protected List<string> monthlyDates = new List<string>();
        protected List<int> monthlyNewDisbursements = new List<int>();
        protected List<int> monthlyNewDisbursementsG2G = new List<int>();
        protected List<int> monthlyNewDisbursementsEx = new List<int>();

        protected string monthlyNewDisbursementsJS;
        protected string monthlyDatesJS;
        protected string monthlyNewDisbursementsG2GJS;
        protected string monthlyNewDisbursementsExJS;
        #endregion

        //Button Data
        #region
        //rejection column
        public int disbursementsRejectedNotice;
        public int disbursementsRejectedRequest;
        public int disbursementsRejectedNoticeEx;
        public int disbursementsRejectedRequestEx;
        public int disbursementsRejectedNoticeG2G;
        public int disbursementsRejectedRequestG2G;
        //Requests Created Today column
        public int requestsCreatedTodayTotal;
        public int requestsCreatedTodayEx;
        public int requestsCreatedTodayG2G;
        //Notices created Today column
        public int noticesCreatedTodayTotal;
        public int noticesCreatedTodayEx;
        public int noticesCreatedTodayG2G;
        //Requests Processed Today column
        public int requestsProcessedTodayTotal;
        public int requestsProcessedTodayEx;
        public int requestsProcessedTodayG2G;
        //Notices processed today column
        public int noticesProcessedTodayTotal;
        public int noticesProcessedTodayEx;
        public int noticesProcessedTodayG2G;
        //Requests processed last 7 days
        public int requestsProcessed7DaysTotal;
        public int requestsProcessed7DaysEx;
        public int requestsProcessed7DaysG2G;
        //Requests processed last 30 days
        public int requestsProcessed30DaysTotal;
        public int requestsProcessed30DaysEx;
        public int requestsProcessed30DaysG2G;
        //Notices processed last 7 days
        public int noticesProcessed7DaysTotal;
        public int noticesProcessed7DaysEx;
        public int noticesProcessed7DaysG2G;
        //Notices processed last 30 days
        public int noticesProcessed30DaysTotal;
        public int noticesProcessed30DaysEx;
        public int noticesProcessed30DaysG2G;
        //Oldest Request Unworked/Modified
        public string requestOldestUnworkedTotal;
        public string requestOldestUnworkedEx;
        public string requestOldestUnworkedG2G;
        public string requestOldestModifiedTotal;
        public string requestOldestModifiedEx;
        public string requestOldestModifiedG2G;
        //Oldest Notice Unworked/Modified
        public string noticeOldestUnworkedTotal;
        public string noticeOldestUnworkedEx;
        public string noticeOldestUnworkedG2G;
        public string noticeOldestModifiedTotal;
        public string noticeOldestModifiedEx;
        public string noticeOldestModifiedG2G;
        //Disbursements on Hold
        public int requestsOnHoldTotal;
        public int requestsOnHoldEx;
        public int requestsOnHoldG2G;
        public int noticesOnHoldTotal;
        public int noticesOnHoldEx;
        public int noticesOnHoldG2G;

        #endregion

        public IEnumerable<DateTime> DateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                             .Select(d => fromDate.AddDays(d));
        }

        public static List<DateTime> GetWeeklyDates(int numberOfWeeksNeeded)
        {
            DateTime previousDate = new DateTime();

            List<DateTime> dates = new List<DateTime>();
            dates.Add(DateTime.Today.Date);
            previousDate = DateTime.Today.Date;

            int count = 1;
            while(count < numberOfWeeksNeeded)
            {
                DateTime nextDate = previousDate.AddDays(-7);
                dates.Add(nextDate);
                previousDate = nextDate.Date;

                count++;
            }
            return dates;
        }

        public static List<DateTime> GetDates(int year, int month)
        {
            var dates = new List<DateTime>();

            // Loop from the first day of the month until we hit the next month, moving forward a day at a time
            for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            return dates;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DasbboardObject _dashboard = new DasbboardObject();
            //connect to db just like G2G.lib
            List<DasbboardObject> dashboardObjects = _dashboard.GetDashboardObjects(null, null);
            //create values for each of the button numbers to display on dashboard

            //create the collections needed to pass to the charts
            DateTime fromDateDaily = DateTime.Now.Date.AddDays(-13);
            DateTime toDateDaily = DateTime.Now.Date;
            List<DateTime> dailyDateRange = DateRange(fromDateDaily, toDateDaily).ToList();
            foreach(DateTime date in dailyDateRange)
            {
                string dateString = date.ToString("MM/dd");
                dailyDates.Add(dateString);

                int number = dashboardObjects.Where(x => x.Created.Date == date.Date).Count();
                dailyNewDisbursements.Add(number);

                int numberG2G = dashboardObjects.Where(x => x.Created.Date == date.Date && (x.Type == 1010 || x.Type == 1012)).Count();
                dailyNewDisbursementsG2G.Add(numberG2G);

                int numberEx = dashboardObjects.Where(x => x.Created.Date == date.Date && (x.Type == 1000 || x.Type == 1002)).Count();
                dailyNewDisbursementsEx.Add(numberEx);
            }

            List<DateTime> weeklyDateRange = GetWeeklyDates(12);
            weeklyDateRange.Reverse();
            foreach(DateTime date in weeklyDateRange)
            {
                string dateString = date.ToString("MM/dd");
                weeklyDates.Add(dateString);

                int number = dashboardObjects.Where(x => x.Created.Date <= date.Date && x.Created.Date > date.AddDays(-7)).Count();
                weeklyNewDisbursements.Add(number);

                int numberG2G = dashboardObjects.Where(x => x.Created.Date <= date.Date && x.Created.Date > date.AddDays(-7) && (x.Type == 1010 || x.Type == 1012)).Count();
                weeklyNewDisbursementsG2G.Add(numberG2G);

                int numberEx = dashboardObjects.Where(x => x.Created.Date <= date.Date && x.Created.Date > date.AddDays(-7) && (x.Type == 1000 || x.Type == 1002)).Count();
                weeklyNewDisbursementsEx.Add(numberEx);
            }

            DateTime fromDateMonthly = DateTime.Now.Date.AddMonths(-11);
            DateTime toDateMontly = DateTime.Now.Date;
            List<DateTime> monthlyDateRange = DateRange(fromDateMonthly, toDateMontly).ToList();
            var lastTwelveMonths = Enumerable.Range(0, 12)
                              .Select(i => DateTime.Now.AddMonths(i - 11));
            
            foreach (DateTime date in lastTwelveMonths)
            {
                string dateString = date.ToString("MMMM");
                monthlyDates.Add(dateString);

                int number = dashboardObjects.Where(x => x.Created.Month == date.Month && x.Created.Year == date.Year).Count();
                monthlyNewDisbursements.Add(number);

                int numberG2G = dashboardObjects.Where(x => x.Created.Month == date.Month && x.Created.Year == date.Year && (x.Type == 1010 || x.Type == 1012)).Count();
                monthlyNewDisbursementsG2G.Add(numberG2G);

                int numberEx = dashboardObjects.Where(x => x.Created.Month == date.Month && x.Created.Year == date.Year && (x.Type == 1000 || x.Type == 1002)).Count();
                monthlyNewDisbursementsEx.Add(numberEx);
            }


            //write a view that returns the data shown for the dashboard (just the properties used for display on the buttons)

            //when someone clicks on a button, we'll use a specific query for the data in that column

            //define and set a/the collection/s (set for each chart, and a set for each column of buttons

            //pass the data needed for charts to the chart_load functions

            //for each column, send the data needed to display the numbers on each button

            //pass the data needed for the buttons to set their colors

            dailyNewDisbursementsJS = ConvertListToArray<int>(dailyNewDisbursements);
            dailyNewDisbursementsG2GJS = ConvertListToArray<int>(dailyNewDisbursementsG2G);
            dailyNewDisbursementsExJS = ConvertListToArray<int>(dailyNewDisbursementsEx);

            weeklyNewDisbursementsJS = ConvertListToArray<int>(weeklyNewDisbursements);
            weeklyNewDisbursementsG2GJS = ConvertListToArray<int>(weeklyNewDisbursementsG2G);
            weeklyNewDisbursementsExJS = ConvertListToArray<int>(weeklyNewDisbursementsEx);

            monthlyNewDisbursementsJS = ConvertListToArray<int>(monthlyNewDisbursements);
            monthlyNewDisbursementsG2GJS = ConvertListToArray<int>(monthlyNewDisbursementsG2G);
            monthlyNewDisbursementsExJS = ConvertListToArray<int>(monthlyNewDisbursementsEx);

            var jsonSerialiser = new JavaScriptSerializer();
            dailyDatesJS = jsonSerialiser.Serialize(dailyDates);
            weeklyDatesJS = jsonSerialiser.Serialize(weeklyDates);
            monthlyDatesJS = jsonSerialiser.Serialize(monthlyDates);

            //Rejection Column
            disbursementsRejectedNotice = dashboardObjects.Where(d => d.Rejected == true && (d.Type == 1002 || d.Type == 1012)).Count();
            disbursementsRejectedRequest = dashboardObjects.Where(d => d.Rejected == true && (d.Type == 1000 || d.Type == 1010)).Count();
            disbursementsRejectedNoticeEx = dashboardObjects.Where(d => d.Rejected == true && d.Type == 1002).Count();
            disbursementsRejectedRequestEx = dashboardObjects.Where(d => d.Rejected == true && d.Type == 1000).Count();
            disbursementsRejectedNoticeG2G = dashboardObjects.Where(d => d.Rejected == true && d.Type == 1012).Count();
            disbursementsRejectedRequestG2G = dashboardObjects.Where(d => d.Rejected == true && d.Type == 1010).Count();

            //requests created today column
            requestsCreatedTodayTotal = dashboardObjects.Where(d => d.Created.Date == DateTime.Today && (d.Type == 1000 || d.Type == 1010)).Count();
            requestsCreatedTodayEx = dashboardObjects.Where(d => d.Created.Date == DateTime.Today && d.Type == 1000).Count();
            requestsCreatedTodayG2G = dashboardObjects.Where(d => d.Created.Date == DateTime.Today && d.Type == 1010).Count();

            //notices created today column
            noticesCreatedTodayTotal = dashboardObjects.Where(d => d.Created.Date == DateTime.Today && (d.Type == 1002 || d.Type == 1012)).Count();
            noticesCreatedTodayEx = dashboardObjects.Where(d => d.Created.Date == DateTime.Today && d.Type == 1002).Count();
            noticesCreatedTodayG2G = dashboardObjects.Where(d => d.Created.Date == DateTime.Today && d.Type == 1012).Count();

            //requests processed today column
            requestsProcessedTodayTotal = dashboardObjects.Where(d => d.Processed && d.ProcessedDate.Date == DateTime.Today && (d.Type == 1000 || d.Type == 1010)).Count();
            requestsProcessedTodayEx = dashboardObjects.Where(d => d.Processed && d.ProcessedDate.Date == DateTime.Today && d.Type == 1000).Count();
            requestsProcessedTodayG2G = dashboardObjects.Where(d => d.Processed && d.ProcessedDate.Date == DateTime.Today && d.Type == 1010).Count();

            //notices processed today column
            noticesProcessedTodayTotal = dashboardObjects.Where(d => d.Processed && d.ProcessedDate.Date == DateTime.Today && (d.Type == 1002 || d.Type == 1012)).Count();
            noticesProcessedTodayEx = dashboardObjects.Where(d => d.Processed && d.ProcessedDate.Date == DateTime.Today && d.Type == 1002).Count();
            noticesProcessedTodayG2G = dashboardObjects.Where(d => d.Processed && d.ProcessedDate.Date == DateTime.Today && d.Type == 1012).Count();

            //requests Processed last 7 and 30 days
            requestsProcessed7DaysTotal = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-7)) && (d.Type == 1000 || d.Type == 1010)).Count();
            requestsProcessed7DaysEx = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-7)) && d.Type == 1000).Count();
            requestsProcessed7DaysG2G = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-7)) && d.Type == 1010).Count();
            requestsProcessed30DaysTotal = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-30)) && (d.Type == 1000 || d.Type == 1010)).Count();
            requestsProcessed30DaysEx = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-30)) && d.Type == 1000).Count();
            requestsProcessed30DaysG2G = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-30)) && d.Type == 1010).Count();

            //notices Procesesed last 7 and 30 days
            noticesProcessed7DaysTotal = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-7)) && (d.Type == 1002 || d.Type == 1012)).Count();
            noticesProcessed7DaysEx = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-7)) && d.Type == 1002).Count();
            noticesProcessed7DaysG2G = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-7)) && d.Type == 1012).Count();
            noticesProcessed30DaysTotal = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-30)) && (d.Type == 1002 || d.Type == 1012)).Count();
            noticesProcessed30DaysEx = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-30)) && d.Type == 1002).Count();
            noticesProcessed30DaysG2G = dashboardObjects.Where(d => (d.Processed || d.Paid) && (d.ProcessedDate.Date <= DateTime.Today && d.ProcessedDate >= DateTime.Today.AddDays(-30)) && d.Type == 1012).Count();

            //disbursements on hold column
            requestsOnHoldTotal = dashboardObjects.Where(d => d.IsOnHold && (d.Type == 1000 || d.Type == 1010)).Count();
            requestsOnHoldEx = dashboardObjects.Where(d => d.IsOnHold && d.Type == 1000).Count();
            requestsOnHoldG2G = dashboardObjects.Where(d => d.IsOnHold && d.Type == 1010).Count();
            noticesOnHoldTotal = dashboardObjects.Where(d => d.IsOnHold && (d.Type == 1002 || d.Type == 1012)).Count();
            noticesOnHoldEx = dashboardObjects.Where(d => d.IsOnHold && d.Type == 1002).Count();
            noticesOnHoldG2G = dashboardObjects.Where(d => d.IsOnHold && d.Type == 1012).Count();

            //oldest unworked request column
            requestOldestUnworkedTotal = dashboardObjects.Where(d => d.IsUnworked && (d.Type == 1000 || d.Type == 1010) && d.Created.Date > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => d.IsUnworked && (d.Type == 1000 || d.Type == 1010) && d.Created.Date > new DateTime().Date).Min(di => di.Created).ToShortDateString() : "N/A";
            requestOldestUnworkedEx = dashboardObjects.Where(d => d.IsUnworked && d.Type == 1000 && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => d.IsUnworked && d.Type == 1000 && d.Created > new DateTime().Date).Min(di => di.Created).ToShortDateString() : "N/A";
            requestOldestUnworkedG2G = dashboardObjects.Where(d => d.IsUnworked && d.Type == 1010 && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => d.IsUnworked && d.Type == 1010 && d.Created > new DateTime().Date).Min(di => di.Created).ToShortDateString() : "N/A";
            requestOldestModifiedTotal = dashboardObjects.Where(d => !d.IsUnworked && (d.Type == 1000 || d.Type == 1010) && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => !d.IsUnworked && (d.Type == 1000 || d.Type == 1010) && d.Created > new DateTime().Date).Min(di => di.Modified).ToShortDateString() : "N/A";
            requestOldestModifiedEx = dashboardObjects.Where(d => !d.IsUnworked && d.Type == 1000 && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => !d.IsUnworked && d.Type == 1000 && d.Created > new DateTime().Date).Min(di => di.Modified).ToShortDateString() : "N/A";
            requestOldestModifiedG2G = dashboardObjects.Where(d => !d.IsUnworked && d.Type == 1010 && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => !d.IsUnworked && d.Type == 1010 && d.Created > new DateTime().Date).Min(di => di.Modified).ToShortDateString() : "N/A";
            //oldest unworked notice column
            noticeOldestUnworkedTotal = dashboardObjects.Where(d => d.IsUnworked && (d.Type == 1002 || d.Type == 1012) && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => d.IsUnworked && (d.Type == 1002 || d.Type == 1012) && d.Created > new DateTime().Date).Min(di => di.Created).ToShortDateString() : "N/A";
            noticeOldestUnworkedEx = dashboardObjects.Where(d => d.IsUnworked && d.Type == 1002 && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => d.IsUnworked && d.Type == 1002 && d.Created > new DateTime().Date).Min(di => di.Created).ToShortDateString() : "N/A";
            noticeOldestUnworkedG2G = dashboardObjects.Where(d => d.IsUnworked && d.Type == 1012 && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => d.IsUnworked && d.Type == 1012 && d.Created > new DateTime().Date).Min(di => di.Created).ToShortDateString() : "N/A";
            noticeOldestModifiedTotal = dashboardObjects.Where(d => !d.IsUnworked && (d.Type == 1002 || d.Type == 1012) && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => !d.IsUnworked && (d.Type == 1002 || d.Type == 1012) && d.Created > new DateTime().Date).Min(di => di.Modified).ToShortDateString() : "N/A";
            noticeOldestModifiedEx = dashboardObjects.Where(d => !d.IsUnworked && d.Type == 1002 && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => !d.IsUnworked && d.Type == 1002 && d.Created > new DateTime().Date).Min(di => di.Modified).ToShortDateString() : "N/A";
            noticeOldestModifiedG2G = dashboardObjects.Where(d => !d.IsUnworked && d.Type == 1012 && d.Created > new DateTime().Date).Count() > 0 ? dashboardObjects.Where(d => !d.IsUnworked && d.Type == 1012 && d.Created > new DateTime().Date).Min(di => di.Modified).ToShortDateString() : "N/A";
        }


        protected void Chart1_Load(object sender, EventArgs e)
        {

        }
    }

    
}