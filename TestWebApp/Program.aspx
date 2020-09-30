<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Program.aspx.cs" Inherits="TestWebApp.Program" Async="true"%>
<!--JPR this will be added to the top of the form where all other registers are made-->
<%@ Register src="~/RejectReasonsControl.ascx" tagname="RejectReasonsControl" tagprefix="uc1" %>
<%@ Register Src="~/CreateShippingLabelControl.ascx" TagName="CreateShippingLabelControl" TagPrefix="uc2" %>
<%@ Register Src="~/CollectionCallControl.ascx" TagName="CollectionCallControl" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <link  href="https://code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css" rel="stylesheet" />

    <style>
        .ui-dialog {
            
            
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <!--this is aspx markup that will need to be added to form-->
        <asp:UpdatePanel runat="server" ID="upModals">
            <ContentTemplate>
                <div>
                    <uc1:RejectReasonsControl runat="server" ID="ucRejectionReasons" />
                    <asp:HiddenField runat="server" ID="hdnReasonId" ClientIDMode="Static" />
                </div>
                <div>
                    <uc2:CreateShippingLabelControl runat="server" ID="ucCreateShippingLabel" ClientIDMode="Static" />
                    <asp:HiddenField runat="server" ID="hdnToAddress" ClientIDMode="Static" />
                </div>
                <div>
                    <uc3:CollectionCallControl runat="server" ID="ucCollectionCall" ClientIDMode="Static" />
                    <asp:HiddenField runat="server" ID="hdnCollectionCallBatch" ClientIDMode="Static" />
                </div>
                <!--this replicates the REJECT button.  Dont copy-->
                <asp:Button runat="server" ID="btnOpen" Text="Open" OnClientClick="return false;"/>
                <asp:Button runat="server" ID="btnOpen2" Text="OpenLabel" OnClientClick="return false;" />
                <asp:Button runat="server" ID="btnOpen3" Text="Collection Call" OnClientClick="return false;" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

<!--this will be added where all other existing JS calls are on the form-->
<script>
    $(document).ready(function () {
        $("#dialog-collectionCall").dialog({
            modal: true,
            buttons: [

            ]
        });
    })
    $("#btnOpen").on("click", function () {
        $("#dialog-confirm").dialog("open");
    });
    $("#btnOpen2").on("click", function () {
        $("#dialog-shippingLabel").dialog("open");
    });
    $("#btnOpen3").on("click", function () {
        $("#dialog-collectionCall").dialog("open");
    });

    function btnProcess_Click() {
        var btn = $('#btnHidden');
        btn.trigger('click');
        console.log(btn);
    }

</script>
<script type="C#" runat="server">
    void Page_Load(object sender, EventArgs e)
    {
        ucRejectionReasons.AbsenceId = "6F16AB31-CA2E-47EE-BED4-0C61A8B75884";
        ucCreateShippingLabel.AbsenceId = "6F16AB31-CA2E-47EE-BED4-0C61A8B75884";
        ucCollectionCall.AbsenceId = "6F16AB31-CA2E-47EE-BED4-0C61A8B75884";
    }
</script>
