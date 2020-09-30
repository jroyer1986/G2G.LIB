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
    public class Absence
    {
        public string ID { get; set; }
        public int HID { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public Guid? RejectionReasonGuid { get; set; }
        public string PayeeName { get; set; }
        public string PayeeAddress1 { get; set; }
        public string PayeeAddress2 { get; set; }
        public string PayeeAddress3 { get; set; }
        public string PayeeCity { get; set; }
        public string PayeeState { get; set; }
        public string PayeeCountry { get; set; }
        public string PayeeZipCode { get; set; }
        public string WorkflowComments { get; set; }
        public int Status { get; set; }
        public string VIN { get; set; }
        public string CustomerID { get; set; }
        public bool AccountVinOnHold { get; set; }
        public string AccountVinHoldReason { get; set; }

        public Absence Setup(DataRow r)
        {
            Absence absence = new Absence();

            Guid rejectReasonGuid;

            if (!DBNull.Value.Equals(r["FreeGuidField_04"]))
            {
                Guid.TryParse(r["FreeGuidField_04"].ToString(), out rejectReasonGuid);
                absence.RejectionReasonGuid = rejectReasonGuid;
            }
            absence.ID = r["ID"].ToString();
            absence.HID = Convert.ToInt32(r["HID"]);
            absence.Type = Convert.ToInt32(r["Type"]);
            absence.Description = r["Description"].ToString();
            absence.Status = Convert.ToInt32(r["Status"]);      
            absence.WorkflowComments = DBNull.Value.Equals(r["WorkflowComments"]) ? null : r["WorkflowComments"].ToString();
            absence.VIN = DBNull.Value.Equals(r["ItemCode"]) ? null : r["ItemCode"].ToString();
            absence.CustomerID = DBNull.Value.Equals(r["CustomerID"]) ? null : r["CustomerID"].ToString();
            absence.AccountVinOnHold = IsAccountVinOnHold(absence);
            absence.AccountVinHoldReason = AccountVinOnHoldReason(absence);

            if ((absence.Type == 1000 || absence.Type == 1002) && absence.CustomerID != null)
            {
                //absence = GetPayeeCompanyInfo(absence);
                absence.PayeeName = DBNull.Value.Equals(r["FreeTextField_07"]) ? null : r["FreeTextField_07"].ToString();
                absence.PayeeAddress1 = DBNull.Value.Equals(r["FreeTextField_08"]) ? null : r["FreeTextField_08"].ToString();
                absence.PayeeAddress2 = DBNull.Value.Equals(r["FreeTextField_09"]) ? null : r["FreeTextField_09"].ToString();
                absence.PayeeAddress3 = DBNull.Value.Equals(r["FreeTextField_10"]) ? null : r["FreeTextField_10"].ToString();
                absence.PayeeCity = DBNull.Value.Equals(r["FreeTextField_11"]) ? null : r["FreeTextField_11"].ToString();
                absence.PayeeState = DBNull.Value.Equals(r["FreeTextField_12"]) ? null : r["FreeTextField_12"].ToString();
                absence.PayeeZipCode = DBNull.Value.Equals(r["FreeTextField_13"]) ? null : r["FreeTextField_13"].ToString();
                absence.PayeeCountry = "US";
            }

            return absence;
        }

        public Absence GetPayeeCompanyInfo(Absence absence)
        {
            Dealer dealer = new Dealer();
            //dealer = dealer.GetDealerByCompanyCode(absence.CustomerID);

            absence.PayeeName = dealer.Name;
            absence.PayeeAddress1 = dealer.Address1;
            absence.PayeeAddress2 = dealer.Address2;
            absence.PayeeCity = dealer.City;
            absence.PayeeState = dealer.State;
            absence.PayeeCountry = dealer.Country;
            absence.PayeeZipCode = dealer.ZipCode;

            return absence;
        }
        public string GetUserByHRESID(int id)
        {
            string name = "Heather Mullins";

            Absence absence = new Absence();
            string queryString = "SELECT * FROM dbo.humres WHERE res_id = " + id;
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    if(dt.Rows.Count > 0) 
                        name = dt.Rows[0]["fullname"].ToString();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }

                return name;
            }
        }

        public string AccountVinOnHoldReason(Absence absence)
        {
            string holdReason = "";

            AccountVinHold _holdsRepo = new AccountVinHold();
            if (absence.CustomerID != null)
            {
                List<AccountVinHold> holds = _holdsRepo.GetAccountVinHoldsByAccountID(absence.CustomerID);

                if (holds != null && holds.Count() > 0 && absence.Type != 4001)
                {
                    return holds[0].Description.ToString() + " [" + GetUserByHRESID(holds[0].RequestedByID) + " " +  holds[0].RequestedOn + "]";
                    foreach (AccountVinHold hold in holds)
                    {
                        //Dealer Account on Hold
                        if (hold.HoldType == 1)
                        {
                            return "Account on hold";
                            if (absence.CustomerID == hold.AccountNum)
                            {
                                holdReason = hold.Notes;
                                break;
                            }
                        }
                        //VIN on hold
                        else if (hold.HoldType == 2)
                        {
                            return "VIN on hold";
                            if (absence.VIN == hold.VIN)
                            {
                                holdReason = hold.Notes;
                                break;
                            }
                        }
                        //VRA on hold
                        else if (hold.HoldType == 3)
                        {
                            return "VRA on hold";
                            //might have to add VRA to absences in future?
                        }
                    }
                }
            }
            return holdReason;
        }
        public bool IsAccountVinOnHold(Absence absence)
        {
            bool isOnHold = false;

            AccountVinHold _holdsRepo = new AccountVinHold();
            if (absence.CustomerID != null)
            {
                List<AccountVinHold> holds = _holdsRepo.GetAccountVinHoldsByAccountID(absence.CustomerID);

                if (holds != null && holds.Count > 0 && absence.Type != 4001)
                {
                    foreach (AccountVinHold hold in holds)
                    {
                        //check to see if status is not 'released'
                        if (hold.Status == 1 && absence.Status != 3/* closed */ && absence.Status != 2 /* rejected */)
                        {
                            //Dealer Account on Hold
                            if (hold.HoldType == 1)
                            {
                                if (absence.CustomerID == hold.AccountNum)
                                {
                                    return true;
                                }
                            }
                            //VIN on hold
                            else if (hold.HoldType == 2)
                            {
                                if (absence.VIN == hold.VIN)
                                {
                                    return true;
                                }
                            }
                            //VRA on hold
                            else if (hold.HoldType == 3)
                            {
                                //might have to add VRA to absences in future?

                                //return true
                            }
                        }
                    }
                }
            }
            return isOnHold;
        }
        public Absence GetAbsenceByID(string id)
        {
            Absence absence = new Absence();
            string queryString = "SELECT * FROM dbo.Absences WHERE ID = '" + id + "'";
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    absence = Setup(dt.Rows[0]);
                }
                catch (Exception ex)
                {
                    
                }
                finally
                {
                    connection.Close();
                }

                return absence;
            }
        }

        public void UpdateDisbursementRejectionReasonByAbsenceID(Guid reasonId, string absenceId)
        {
            string queryString = "UPDATE dbo.Absences SET FreeGuidField_04 = '" + reasonId + "' WHERE ID = '" + absenceId + "'";
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@ReasonId", reasonId);

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
        public void UpdateDisbursementShippingLabel(string shippingLabel, string absenceId)
        {
            Absence absence = GetAbsenceByID(absenceId);
            string workflowComments = absence.WorkflowComments.Trim();
            
            //TODO: look for existing easypost url and append "expired"
            if (workflowComments != null)
            {
                var linkParser = new Regex(@"\b(?:https?://easypost-files\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                int numMatches = linkParser.Matches(workflowComments) != null ? linkParser.Matches(workflowComments).Count : 0;

                //at least 1 easypost link was found
                if (numMatches > 0)
                {
                    Match m = linkParser.Matches(workflowComments)[numMatches - 1];

                    //locate final character in url
                    int index = m.Index + m.Length - 1;
                    //easypost link is the most recent comment
                    if(workflowComments.Length == index + 1)
                    {
                        workflowComments += " **EXPIRED -- DO NOT USE**\r\n\r\n" + shippingLabel;
                    }
                    //there is more to comments after the link
                    else if ((workflowComments.Length > index + 1) && (workflowComments.Length > index + 3 && workflowComments[index + 3] != '*'))
                    {
                        //insert EXPIRED 
                        workflowComments = workflowComments.Insert(index + 1, " **EXPIRED -- DO NOT USE**");
                        workflowComments += shippingLabel;
                    }
                    else
                    {
                        workflowComments += "\r\n\r\n" + shippingLabel;
                    }
                }
                //no previous easypost link could be found
                else
                {
                    workflowComments += "\r\n\r\n" + shippingLabel;
                }
            }
            else
            {
                workflowComments += shippingLabel;
            }
            
            if (absence != null)
            {
                string queryString = "UPDATE dbo.absences SET WorkflowComments = @ShippingLabel WHERE ID = @AbsenceId";
                using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@ShippingLabel", workflowComments);
                    command.Parameters.AddWithValue("@AbsenceId", absenceId);

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
}