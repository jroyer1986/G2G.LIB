using G2G_LIB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using G2G_LIB.Global;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using G2G_LIB.UserControls;

namespace G2G_LIB
{
    public static class Helpers
    {
        public static Absence GetAbsenceById(string absenceId)
        {
            Absence myAbsence;

            Absence _absenceService = new Absence();
            myAbsence = _absenceService.GetAbsenceByID(absenceId);
            return myAbsence;
        }
        public static string IsDisbursementOnHold(string absenceId = null)
        {
            Absence myAbsence;
            bool isOnHold = false;

            if (absenceId != null)
            {
                Absence _absenceService = new Absence();
                myAbsence = _absenceService.GetAbsenceByID(absenceId);

                if (myAbsence.ID != null)
                {
                    if (myAbsence.AccountVinOnHold)
                    {
                        isOnHold = true;
                    }
                }
            }
            return isOnHold.ToString();
        }
        public static string GetAccountVinHoldReason(string absenceId = null)
        {
            Absence myAbsence;
            string holdReason = "";

            if (absenceId != null)
            {
                Absence _absenceService = new Absence();
                myAbsence = _absenceService.GetAbsenceByID(absenceId);

                if (myAbsence.ID != null)
                {
                    if (myAbsence.AccountVinOnHold)
                    {
                        holdReason = myAbsence.AccountVinHoldReason;
                    }
                }
            }
            return holdReason;
        }
        public static void LoadAbsenceId(string absenceId = null, HiddenField hdnIsOnHold = null, HiddenField hdnOnHoldReason = null, RejectReasonsControl rejectionReasonCtrl = null, CreateShippingLabelControl shippingLabelCtrl = null, CollectionCallControl collectionCallCtrl = null, Exact.Web.UI.Controls.Button btnShippingLabel = null, Exact.Web.UI.Controls.Button btnCollectionCall = null)
        {
            Absence myAbsence;
            if (absenceId != null)
            {
                Absence _absenceService = new Absence();
                myAbsence = _absenceService.GetAbsenceByID(absenceId);
                btnCollectionCall.Visible = false;

                if (myAbsence.ID != null && (myAbsence.Type == 1000 || myAbsence.Type == 1002 || myAbsence.Type == 1010 || myAbsence.Type == 1012))
                {
                    if (rejectionReasonCtrl != null)
                        rejectionReasonCtrl.AbsenceId = absenceId;
                    if (shippingLabelCtrl != null)
                        shippingLabelCtrl.AbsenceId = absenceId;
                    if (btnShippingLabel != null)
                    {
                        btnShippingLabel.Visible = false;
                        if (myAbsence.Type == 1002 || myAbsence.Type == 1012)
                        {
                            if (myAbsence.Status != 2)
                            {
                                btnShippingLabel.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    btnShippingLabel.Visible = false;
                }

                if(hdnIsOnHold != null && hdnOnHoldReason != null)
                {
                    if(myAbsence.AccountVinOnHold)
                    {
                        hdnIsOnHold.Value = true.ToString();
                        hdnOnHoldReason.Value = myAbsence.AccountVinHoldReason;
                    }
                }

                //if absence is a Collection Call, and the status is not yet processed
                if(myAbsence.ID != null && myAbsence.Type == 4004)
                {
                    if (collectionCallCtrl != null)
                    {
                        collectionCallCtrl.AbsenceId = absenceId;
                        CollectionCall _collectionCallService = new CollectionCall();
                        List<CollectionCall> otherCollectionCalls = _collectionCallService.GetCollectionCallsByCustomerId(myAbsence.CustomerID, myAbsence.HID);
                        if (btnCollectionCall != null)
                        {
                            //show collection call
                            btnCollectionCall.Visible = false;
                            if (myAbsence.Status != 3)
                            {
                                //hide collection call
                                btnCollectionCall.Visible = true;
                            }
                            if (otherCollectionCalls == null || otherCollectionCalls.Count <= 1)
                            {
                                btnCollectionCall.Enabled = false;
                            }
                        }
                    }
                }

                //if(hdnHoldReason != null && myAbsence.HoldReason != null && myAbsence.HoldReason != "")
                //{
                //    hdnHoldReason.Text = myAbsence.HoldReason;
                //}
            }
        }
        public static async Task<string> GetShippingLabelURL(string absenceId, Address_EP myToAddress)
        {
            return await GetShippingLabelFromEasyPostRestApi(absenceId, myToAddress);
        }
        private static async Task<string> GetShippingLabelFromEasyPostRestApi(string absenceId, Address_EP myToAddress)
        {
            string shippingLabelUrl;
            string API_Key = GlobalVars.GetEasyPostApiKey();

            try
            {
                Address_EP toAddress = new Address_EP(myToAddress.Id, myToAddress.Company, myToAddress.Street1, myToAddress.Street2, myToAddress.City, myToAddress.State, myToAddress.Country, myToAddress.Zip, myToAddress.Phone);
                shippingLabelUrl = await EasyPostService.GetShippingLabelURL(absenceId, toAddress);
                return shippingLabelUrl;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static DropDownList GetRejectReasonsDDL()
        {
            DropDownList listToReturn = GetRejectReasonsFromDB();
            return listToReturn;
        }
        public static DropDownList GetRejectReasonsFromDB()
        {
            int descriptionReasonsTypeId = 3150;
            DropDownList ddl = new DropDownList();
            string queryString = "SELECT * FROM dbo.Absences WHERE Type = @Type";
            using (SqlConnection connection = new SqlConnection(GlobalVars.DBConnection["Conn"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Type", descriptionReasonsTypeId);

                try
                {
                    connection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());

                    foreach(DataRow r in dt.Rows)
                    {
                        System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem()
                        {
                            Text = Convert.ToString(r["Description"]),
                            Value = Convert.ToString(r["ID"])
                        };
                        ddl.Items.Add(li);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return ddl;
            }
        }
        public static void UpdateAbsencesWithRejectReason(string strReasonId, string absenceId)
        {
            absenceId = ParseStringToRemoveBrackets(absenceId);
            Guid reasonId;

            try
            {
                Guid guid;
                if (Guid.TryParse(strReasonId, out guid))
                {
                    reasonId = guid;
                    Absence _absenceService = new Absence();
                    _absenceService.UpdateDisbursementRejectionReasonByAbsenceID(reasonId, absenceId);
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ParseStringToRemoveBrackets(string myString)
        {
            if (myString != null && myString != "")
            {
                return myString.Replace("{", "").Replace("}", "").Replace(" ", "");
            }
            else
            {
                Exception ex = new Exception("no string provided");
                return null;
            }
        }
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        public static string GetCustomerIdFromAbsenceId(string absenceId = null)
        {
            string customerId;
            Absence myAbsence;

            if(absenceId != null)
            {
                Absence _absenceService = new Absence();
                myAbsence = _absenceService.GetAbsenceByID(absenceId);

                customerId = myAbsence.CustomerID;
                return customerId;
            }
            else
            {
                return null;
            }
        }
        public static string GetWorkflowType(string absenceId = null)
        {
            Absence myAbsence;
            string workflowType = null;

            if (absenceId != null)
            {
                Absence _absenceService = new Absence();
                myAbsence = _absenceService.GetAbsenceByID(absenceId);
                workflowType = myAbsence.Type.ToString();
            }
            return workflowType;
        }
        
    }
}
