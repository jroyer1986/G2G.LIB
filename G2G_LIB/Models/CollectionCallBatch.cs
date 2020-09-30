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
    public class CollectionCallBatch
    {
        public string ID { get; set; }
        public string HIDs { get; set; }

        public CollectionCallBatch Setup(DataRow r)
        {
            CollectionCallBatch batch = new CollectionCallBatch();
            batch.ID = r["HID"].ToString();
            batch.HIDs = r["HIDs"].ToString();

            return batch;
        }

        public CollectionCallBatch GetCollectionCallBatchByHID(int hid)
        {
            CollectionCallBatch batch = new CollectionCallBatch();
            string queryString = "SELECT * FROM dbo.msmCollectionCallBulkProcess WHERE HID = @hid";
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@hid", hid);

                try
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    if (dt.Rows.Count > 0) {
                        batch = Setup(dt.Rows[0]);
                    }
                    else
                    {
                        batch = null;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
                return batch;
            }
        }
    }
}
