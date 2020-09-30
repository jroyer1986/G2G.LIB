using G2G_LIB.Global;
using G2G_LIB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G2G_LIB.UserControls
{
    public partial class CollectionCallControl : System.Web.UI.UserControl
    {
        private string _absenceId;
        private Absence _absence;
        private string _customerId;

        public List<CollectionCall> batchInvoices;

        public string AbsenceId
        {
            get { return _absenceId; }
            set { _absenceId = value.Replace("{", "").Replace("}", ""); }
        }
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value.Replace("{", "").Replace("}", ""); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            LinkButton lbSubmit = FindControl("btnSubmit") as LinkButton;
            if(lbSubmit != null)
                lbSubmit.Click += btnSubmit_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Absence _absenceService = new Absence();
            if (AbsenceId != null && AbsenceId != "")
            {
                _absence = _absenceService.GetAbsenceByID(AbsenceId);
            }
            if (_absence != null && (_absence.Type == 4004))
            {
                //load view with absence information
                TextBox txtBox = FindControl("txtName") as TextBox;
                if (txtBox != null)
                {
                    txtBox.Text = _absence.CustomerID;
                }

                CustomerId = _absence.CustomerID;
                CollectionCall _collectionCallService = new CollectionCall();
                List<CollectionCall> collectionCalls = _collectionCallService.GetCollectionCallsByCustomerId(CustomerId, _absence.HID);
                //feed the repeater with the collection calls
                Repeater rptCollectionCalls = FindControl("rptCollectionCalls") as Repeater;

                rptCollectionCalls.DataSource = collectionCalls;
                rptCollectionCalls.DataBind();
                

            }
        }

        private decimal balanceTotal = 0m;
        private decimal invAmtTotal = 0m;
        protected void rptCollectionCalls_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.
                invAmtTotal += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "InvAmt"));
                balanceTotal += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Balance"));

                var checkbox = e.Item.FindControl("cbxSelect") as CheckBox;
                Label hid = ((Label)e.Item.FindControl("lblHid"));
                checkbox.ID = hid.Text;
                //if row HID = currentHID, autocheck and disable checkbox
                if(hid.Text == _absence.HID.ToString())
                {
                    checkbox.Checked = true;
                    checkbox.Enabled = false;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                Repeater rptCollectionCalls = FindControl("rptCollectionCalls") as Repeater;
                foreach (RepeaterItem i in rptCollectionCalls.Controls)
                {
                    if (i.ItemType != ListItemType.Footer) continue;
                    var lblInvAmtTotal = ((Label)i.FindControl("lblInvAmtTotal"));
                    var lblBalanceTotal = ((Label)i.FindControl("lblBalanceTotal"));
                    lblInvAmtTotal.Text = invAmtTotal.ToString("0.00");
                    lblBalanceTotal.Text = balanceTotal.ToString("0.00");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //add a row to temp table with column HID and other HIDs to update
            HiddenField hids = Page.FindControl("hdnCollectionCallBatch") as HiddenField;
            string serializedHIDs = hids.Value;

            var serializer = new JavaScriptSerializer();
            List<string> batchHIDs = serializer.Deserialize<List<string>>(serializedHIDs);
            string batchHIDsString = string.Join(",", batchHIDs);
            CollectionCallBatch batch = new CollectionCallBatch();
            batch.ID = _absence.HID.ToString();
            batch.HIDs = batchHIDsString;

            CollectionCall _collectionCallService = new CollectionCall();
            _collectionCallService.InsertCollectionCallBatch(batch.ID, batch.HIDs);
        }
    }
}
