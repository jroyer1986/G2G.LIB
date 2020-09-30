using DisbursementDashboard.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DisbursementDashboard.Models
{
    public class DasbboardObject
    {
        public int HID { get; set; }
        public int Type { get; set; }
        public bool Paid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Processed { get; set; }
        public DateTime ProcessedDate { get; set; }
        public bool Unprocessed { get; set; }
        //public bool AccountBlocked { get; set; }
        //public bool VinBlocked { get; set; }
        public bool Rejected { get; set; }
        public bool IsOnHold { get; set; }
        public bool IsUnworked { get; set; }

        public DasbboardObject Setup(DataRow r)
        {
            DasbboardObject dashboardObject = new DasbboardObject();

            dashboardObject.HID = Convert.ToInt32(r["HID"]);
            dashboardObject.Type = Convert.ToInt32(r["Type"]);
            dashboardObject.Paid = Convert.ToBoolean(r["Paid"]);
            dashboardObject.IsOnHold = Convert.ToBoolean(r["IsOnHold"]);
            dashboardObject.Created = r["Created"] != DBNull.Value ? Convert.ToDateTime(r["Created"]) : new DateTime(0001,01,01);
            dashboardObject.Modified = r["Modified"] != DBNull.Value ? Convert.ToDateTime(r["Modified"]) : new DateTime(0001, 01, 01);
            dashboardObject.Processed = Convert.ToBoolean(r["Processed"]);
            dashboardObject.ProcessedDate = r["ProcessedDate"] != DBNull.Value ? Convert.ToDateTime(r["ProcessedDate"]) : new DateTime(0001, 01, 01);
            dashboardObject.Unprocessed = Convert.ToBoolean(r["Unprocessed"]);
            //dashboardObject.AccountBlocked = Convert.ToBoolean(r["AccountBlocked"]);
            //dashboardObject.VinBlocked = Convert.ToBoolean(r["VinBlocked"]);
            dashboardObject.Rejected = Convert.ToBoolean(r["Rejected"]);
            dashboardObject.IsUnworked = DateTime.Compare(dashboardObject.Created.AddSeconds(2), dashboardObject.Modified) > 0 ? true : false;

            return dashboardObject;
        }

        public List<DasbboardObject> GetDashboardObjects(DateTime? startDate, DateTime? endDate)
        {
            List<DasbboardObject> dashboardObjects = new List<DasbboardObject>();
            string queryString = "SELECT HID, Type, CASE WHEN Status = 3 THEN 1 ELSE 0 END AS Paid, syscreated as Created, sysmodified as Modified, Processed as ProcessedDate, CASE WHEN FreeGuidField_02 IS NOT NULL THEN 1 ELSE 0 END AS IsOnHold, CASE WHEN Realized IS NOT NULL AND Status = 4 OR Status = 3 THEN 1 ELSE 0 END AS Processed, CASE WHEN Realized IS NULL AND Status = 1 THEN 1 ELSE 0 END AS Unprocessed, CASE WHEN Status = 2 THEN 1 ELSE 0 END AS Rejected FROM dbo.Absences WHERE Type IN(1000, 1002, 1010, 1012)";
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    foreach(DataRow row in dt.Rows)
                    {
                        dashboardObjects.Add(Setup(row));
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }

                return dashboardObjects;
            }
        }
    }
}