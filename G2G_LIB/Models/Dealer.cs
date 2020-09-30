using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using G2G_LIB.Global;

namespace G2G_LIB.Models
{
    public class Dealer
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }

        public Dealer Setup(DataRow r)
        {
            return new Dealer
            {
                Name = Convert.ToString(r["cmp_name"]),
                Address1 = Convert.ToString(r["cmp_fadd1"]),
                Address2 = Convert.ToString(r["cmp_fadd2"]),
                City = Convert.ToString(r["cmp_fcity"]),
                State = Convert.ToString(r["StateCode"]),
                Country = Convert.ToString(r["cmp_fctry"]),
                ZipCode = Convert.ToString(r["cmp_fpc"]),
                Phone = Convert.ToString(r["cmp_tel"])
            };
        }

        public Dealer GetDealerByCompanyCode(string companyCode)
        {
            Dealer dealer = new Dealer();
            string queryString = "SELECT * FROM dbo.cicmpy WHERE cmp_wwn = @customerId";
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@customerId", companyCode);

                try
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    dealer = Setup(dt.Rows[0]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dealer;
            }
        }
    }
}
