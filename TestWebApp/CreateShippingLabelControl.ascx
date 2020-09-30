<%@ Control Language="C#" AutoEventWireup="true" Inherits="G2G_LIB.UserControls.CreateShippingLabelControl" %>

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
    }
    .ui-button:active {
        border-color: ButtonShadow ButtonHighlight ButtonHighlight ButtonShadow;
    }
    .txt-text {
        padding-bottom: 10px;
    }
    .label {
         font-size: 1rem;
    }
</style>

<!--View-->
<div id="dialog-shippingLabel" title="To Shipping Address">
    <div class="row">
        <div class="col-sm-4">
            <asp:Label runat="server" ID="lblName" Text="Payee Name:" class="label"></asp:Label>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtName" ErrorMessage="*Required*" ForeColor="Red" ValidationGroup="vShippingLabel"/>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtName" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <asp:Label runat="server" ID="lblAddress1" Text="Address 1:" class="label"></asp:Label>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtAddress1" ErrorMessage="*Required*" ForeColor="Red" ValidationGroup="vShippingLabel"/>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtAddress1" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <asp:Label runat="server" ID="lblAddress2" Text="Address 2:" class="label"></asp:Label>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtAddress2" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <asp:Label runat="server" ID="lblAddress3" Text="Address 3:" class="label"></asp:Label>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtAddress3" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <asp:Label runat="server" ID="lblCity" Text="City:" class="label"></asp:Label>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtCity" ErrorMessage="*Required*" ForeColor="Red" ValidationGroup="vShippingLabel"/>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtCity" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <asp:Label runat="server" ID="lblState" Text="State:" class="label"></asp:Label>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtState" ErrorMessage="*Required*" ForeColor="Red" ValidationGroup="vShippingLabel"/>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtState" class="form-control" ToolTip="2-Digit State Code"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <asp:Label runat="server" ID="lblCountry" Text="Country:" class="label"></asp:Label>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtCountry" ErrorMessage="*Required*" ForeColor="Red" ValidationGroup="vShippingLabel"/>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtCountry" class="form-control" ToolTip="2-Digit Country Code"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <asp:Label runat="server" ID="lblZip" Text="Zip Code:" class="label"></asp:Label>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtZip" ErrorMessage="*Required*" ForeColor="Red" ValidationGroup="vShippingLabel"/>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtZip" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <asp:Label runat="server" ID="lblPhone" Text="Phone:" class="label"></asp:Label>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtPhone" ErrorMessage="*Required*" ForeColor="Red" ValidationGroup="vShippingLabel"/>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Improper Format*" ForeColor="Red" ControlToValidate="txtPhone" ValidationExpression="^[0-9()\s -]*$" ValidationGroup="vShippingLabel" ></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtPhone" class="form-control" TextMode="Phone" ToolTip="(XXX) XXX-XXXX or XXX-XXX-XXXX"></asp:TextBox>
        </div>
    </div>
    <hr />
    <asp:LinkButton runat="server" ID="btnSubmitShipping" Text="OK" OnClientClick="js_btnSubmitShipping_Click()" CssClass="btn" ValidationGroup="vShippingLabel" ClientIDMode="Static" style="float:right;"/>
</div>

<!--Scripts-->
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<script>
    $(function () {
        $('#dialog-shippingLabel').dialog({
            autoOpen: false,
            modal: true,
            height: 'auto',
            width: '600',
            closeText: "",
            buttons: {
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    });

    function js_btnSubmitShipping_Click() {
        var Address_EP = {
            Company: $('#ucCreateShippingLabel_txtName').val(),
            Street1: $("#ucCreateShippingLabel_txtAddress1").val(),
            Street2: $("#ucCreateShippingLabel_txtAddress2").val(),
            Street3: $("#ucCreateShippingLabel_txtAddress3").val(),
            City: $('#ucCreateShippingLabel_txtCity').val(),
            State: $('#ucCreateShippingLabel_txtState').val(),
            Country: $('#ucCreateShippingLabel_txtCountry').val(),
            Zip: $('#ucCreateShippingLabel_txtZip').val(),
            Phone: $('#ucCreateShippingLabel_txtPhone').val()
        };
        //FOR TESTING
        //var Address_EP = {
        //    company: $('#txtName').val(),
        //    street1: $("#txtAddress1").val(),
        //    street2: $("#txtAddress2").val(),
        //    street3: $("#txtAddress3").val(),
        //    city: $('#txtCity').val(),
        //    state: $('#txtState').val(),
        //    country: $('#txtCountry').val(),
        //    zip: $('#txtZip').val(),
        //    phone: $('#txtPhone').val()
        //};
        var serializedAddress = JSON.stringify(Address_EP);
        SetToAddressHiddenValue(serializedAddress);
    }

    function SetToAddressHiddenValue(serializedAddress) {
        $('#hdnToAddress').val(serializedAddress);
    }

    function ResumeExistingShippingLabelWorkflowFunctionality() {
        SysSet('BCAction', 6);
        SysSubmit();
        fnSaveForm();
    }
</script>