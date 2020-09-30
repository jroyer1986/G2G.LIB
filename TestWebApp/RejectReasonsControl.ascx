<%@ Control Language="C#" AutoEventWireup="true" Inherits="G2G_LIB.UserControls.RejectReasonsControl" %>

<!--Styling-->
<style>
    .btn {
        text-decoration: none;
        font: menu;
        display: inline-block;
        padding: 2px 8px;
        background: ButtonFace;
        color: ButtonText;
        border-style: solid;
        border-width: 2px;
        border-color: ButtonHighlight ButtonShadow ButtonShadow ButtonHighlight;
    }
    .btn:active {
        border-color: ButtonShadow ButtonHighlight ButtonHighlight ButtonShadow;
    }
    .ui-button {
        text-decoration: none;
        font: menu;
        display: inline-block;
        padding: 2px 8px;
        background: ButtonFace;
        color: ButtonText;
        border-style: solid;
        border-width: 2px;
        border-color: ButtonHighlight ButtonShadow ButtonShadow ButtonHighlight;
        font-size: small;
        font-weight: lighter;
    }
    .ui-button:active {
        border-color: ButtonShadow ButtonHighlight ButtonHighlight ButtonShadow;
    }
    .txt-text {
        padding-bottom: 10px;
    }
    .txt-label {
        padding-bottom: 5px;
    }
    .label {
         font-size: 1rem;
    }
    .dropdown-content {
        font-size: 1rem;
    }
    .dropdown-content option {
        font-size: 1rem;
    }

</style>

<!--View-->
<div id="dialog-confirm" title="Reject Disbursement">
    <div class="row">
        <div class="col-sm-4 txt-label">
            <asp:Label runat="server" ID="lblReasons" Text="Reason:" CssClass="label"></asp:Label>
        </div>
        <div class="col-sm-7 txt-text">
            <asp:DropDownList runat="server" ID="ddlReasons" OnChange="js_OnReasonChange(event);" CssClass="dropdown-content" style="font-size: 1rem;"></asp:DropDownList>
        </div>
    </div>
    <div class="row" style="float: right;">
        <asp:LinkButton runat="server" ID="btnSubmit" Text="OK" OnClientClick="js_btnSubmit_Click()" CssClass="btn" ClientIDMode="Static" style="float:right"/>
    </div>
</div>

<!--Scripts-->
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<script>
    $(function () {
        $('#dialog-confirm').dialog({
            autoOpen: false,
            modal: true,
            width: '500',
            height: 'auto',
            closeText: "",
            buttons: {
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    });

    function js_btnSubmit_Click() {
        var ddl = document.getElementById("ucRejectionReasons_ddlReasons");
        var reasonId = ddl.options[ddl.selectedIndex].value;
        //var reasonId = $('#ucRejectionReasons_ddlReasons').val();
        SetReasonIdHiddenValue(reasonId);
    }

    function SetReasonIdHiddenValue(reasonId) {
        $('#hdnReasonId').val(reasonId);
    }

    function js_OnReasonChange(event) {
        var reasonId = event.target.value;
        setHiddenRejectReason(reasonId);
    }

    function ResumeExistingWorkflowFunctionality() {
        SysSet('BCAction', 6);
        SysSet('Status', 2);
        SysSubmit();
        fnSaveForm();
    }
</script>