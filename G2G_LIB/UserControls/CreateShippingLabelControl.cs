using G2G_LIB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G2G_LIB.UserControls
{
    public partial class CreateShippingLabelControl : System.Web.UI.UserControl
    {
        private string _absenceId;
        private Absence _absence;

        public string AbsenceId
        {
            get { return _absenceId; }
            set { _absenceId = value.Replace("{", "").Replace("}", ""); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            LinkButton lbSubmit = FindControl("btnSubmitShipping") as LinkButton;
            lbSubmit.Click += btnSubmitShipping_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            Absence _absenceService = new Absence();
            if(AbsenceId != null && AbsenceId != "")
            {
                _absence = _absenceService.GetAbsenceByID(AbsenceId);
            }
            if (_absence != null && (_absence.Type == 1000 || _absence.Type == 1002))
            {
                TextBox txtBox = FindControl("txtName") as TextBox;
                txtBox.Text = _absence.PayeeName;

                txtBox = FindControl("txtAddress1") as TextBox;
                txtBox.Text = _absence.PayeeAddress1;

                txtBox = FindControl("txtAddress2") as TextBox;
                txtBox.Text = _absence.PayeeAddress2;

                txtBox = FindControl("txtAddress3") as TextBox;
                txtBox.Text = _absence.PayeeAddress3;

                txtBox = FindControl("txtCity") as TextBox;
                txtBox.Text = _absence.PayeeCity;

                txtBox = FindControl("txtState") as TextBox;
                txtBox.Text = _absence.PayeeState;

                txtBox = FindControl("txtCountry") as TextBox;
                txtBox.Text = _absence.PayeeCountry;

                txtBox = FindControl("txtZip") as TextBox;
                txtBox.Text = _absence.PayeeZipCode;
            }
        }

        protected async void btnSubmitShipping_Click(object sender, EventArgs e)
        {
            HiddenField hdn = Page.FindControl("hdnToAddress") as HiddenField;
            string serializedAddress = hdn.Value;

            var serializer = new JavaScriptSerializer();
            Address_EP toAddress = serializer.Deserialize<Address_EP>(serializedAddress);

            var task = Helpers.GetShippingLabelURL(AbsenceId, toAddress);
            string label = await task;

            Response.Redirect(Request.RawUrl);
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ResumeExistingShippingLabelWorkflowFunctionality()", true);
        }
    }
}

