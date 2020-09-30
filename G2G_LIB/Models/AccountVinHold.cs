using G2G_LIB.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace G2G_LIB.Models
{
    class AccountVinHold
    {
        public int HID { get; set; }
        public int Type { get; set; }
        public int RequestedByID { get; set; }
        public DateTime RequestedOn { get; set; }
        public string Description { get; set; }
        public int HoldType { get; set; }
        public string HoldReason { get; set; }
        public string AccountNum { get; set; }
        public string VIN { get; set; }
        public int VRA { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; } //1 = hold   3 = released

        public AccountVinHold Setup(DataRow r)
        {
            AccountVinHold hold = new AccountVinHold();

            hold.HID = Convert.ToInt32(r["HID"]);
            hold.Type = Convert.ToInt32(r["Type"]);
            hold.RequestedByID = Convert.ToInt32(r["EmpID"]);
            hold.RequestedOn = DBNull.Value.Equals(r["StartDate"]) ? Convert.ToDateTime(r["syscreated"]) : Convert.ToDateTime(r["StartDate"]);
            hold.Description = DBNull.Value.Equals(r["Description"]) ? null : r["Description"].ToString();
            hold.HoldType = Convert.ToInt32(r["FreeTextField_02"]);
            hold.HoldReason = r["FreeTextField_03"].ToString();
            hold.AccountNum = r["CustomerID"].ToString();
            hold.VIN = DBNull.Value.Equals(r["FreeTextField_01"]) ? null : r["FreeTextField_01"].ToString();
            hold.VRA = DBNull.Value.Equals(r["FreeNumberField_01"]) ? -1 : Convert.ToInt32(r["FreeNumberField_01"]);
            hold.Notes = DBNull.Value.Equals(r["WorkflowComments"]) ? null : r["WorkflowComments"].ToString();
            hold.Status = Convert.ToInt32(r["Status"]);
            
            return hold;
        }

        public List<AccountVinHold> GetAccountVinHoldsByAccountID(string accountId)
        {
            List<AccountVinHold> holds = new List<AccountVinHold>();
            string queryString = "SELECT * FROM dbo.Absences WHERE Type = 4001 AND CustomerID = '" + accountId + "'";
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    foreach(DataRow r in dt.Rows)
                    {
                        holds.Add(Setup(r));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
                return holds;
            }
        }

        public AccountVinHold GetAccountVinHoldByID(int hid)
        {
            AccountVinHold hold = new AccountVinHold();
            string queryString = "SELECT * FROM dbo.Absences WHERE HID = @hid";
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@hid", hid);

                try
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    hold = Setup(dt.Rows[0]);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
                return hold;
            }
        }
    }
}
