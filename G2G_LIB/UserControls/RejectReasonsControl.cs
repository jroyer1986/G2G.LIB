using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G2G_LIB.UserControls
{
    public partial class RejectReasonsControl : System.Web.UI.UserControl
    {
        private string _absenceId;

        public string AbsenceId
        {
            get { return _absenceId; }
            set { _absenceId = value.Replace("{", "").Replace("}", ""); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            LinkButton lbSubmit = FindControl("btnSubmit") as LinkButton;
            lbSubmit.Click += btnSubmit_Click;

            if (!IsPostBack)
            {
                DropDownList ddl = FindControl("ddlReasons") as DropDownList;
                ddl.DataSource = G2G_LIB.Helpers.GetRejectReasonsDDL().Items;
                ddl.DataTextField = "Text";
                ddl.DataValueField = "Value";
                ddl.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            HiddenField hdn = Page.FindControl("hdnReasonId") as HiddenField;
            string reasonId = hdn.Value;

            try
            {
                if (reasonId != null)
                {
                    //save reasonId to absences table
                    Helpers.UpdateAbsencesWithRejectReason(reasonId, AbsenceId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //call existing functions to resume workflow
                //Response.Redirect(Request.RawUrl);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ResumeExistingWorkflowFunctionality()", true);
            }
        }

        protected void RejectReason_Changed(object sender, EventArgs e)
        {

        }
    }
}
