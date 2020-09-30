using G2G_LIB.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2G_LIB.Models
{
   // SELECT HID, ProcessNumber AS[InvNo], FreeNumberField_01 AS[InvAmt], FreeNumberField_02 AS[Balance], FreeDateField_01 AS[InvDt], FreeDateField_02 AS[DueDt] FROM
    public class CollectionCall
    {
        public string HID { get; set; }
        public string InvNo { get; set; }
        public decimal InvAmt { get; set; }
        public decimal Balance { get; set; }
        public DateTime? InvDt { get; set; }
        public DateTime? DueDt { get; set; }

        public CollectionCall Setup(DataRow r)
        {
            CollectionCall collectionCall = new CollectionCall();

            collectionCall.HID = r["HID"].ToString();
            collectionCall.InvNo = r["InvNo"].ToString();
            collectionCall.InvAmt = Convert.ToDecimal(r["InvAmt"]);
            collectionCall.Balance = Convert.ToDecimal(r["Balance"]);
            collectionCall.InvDt = !Convert.IsDBNull(r["InvDt"]) ? Convert.ToDateTime(r["InvDt"]) : (DateTime?)null;
            collectionCall.DueDt = !Convert.IsDBNull(r["DueDt"]) ? Convert.ToDateTime(r["DueDt"]) : (DateTime?)null;

            return collectionCall;
        }

        public List<CollectionCall> GetCollectionCallsByCustomerId(string customerId, int currentHID)
        {
            //Add filter check for yesnofield01

            List<CollectionCall> collectionCalls = new List<CollectionCall>();
            string queryString = "SELECT HID, ProcessNumber AS [InvNo], FreeNumberField_01 AS [InvAmt], FreeNumberField_02 AS [Balance], StartDate AS [InvDt], EndDate AS [DueDt] FROM dbo.Absences WHERE Type = 4004 AND Status != 3 AND CustomerID='" + customerId + "' ORDER BY [ProcessNumber]";
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    foreach (DataRow r in dt.Rows)
                    {
                        collectionCalls.Add(Setup(r));
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
                return collectionCalls;
            }
        }

        public void UpdateCollectionCall(string hid, string notes, DateTime follupDate)
        {
            string queryString = "UPDATE dbo.Absences SET Status = 3, RequestComments = " + notes + ", FreeDateField_03 = " + follupDate + " WHERE HID = " + hid;
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void InsertCollectionCallBatch(string hid, string hidBatch)
        {
            //first check if a row already exists with this hid.  If so, update instead of delete
            string queryString;

            CollectionCallBatch _batchService = new CollectionCallBatch();
            CollectionCallBatch extBatch =_batchService.GetCollectionCallBatchByHID(Convert.ToInt32(hid));

            if(extBatch != null)
            {
                queryString = "UPDATE dbo.msmCollectionCallBulkProcess SET HIDs = " + hidBatch;
            }
            else
            {
                queryString = "INSERT INTO dbo.msmCollectionCallBulkProcess(HID, HIDs) VALUES(" + hid + ", '" + hidBatch + "')";
            }
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
