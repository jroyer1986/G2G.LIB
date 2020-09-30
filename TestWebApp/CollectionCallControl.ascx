<%@ Control Language="C#" AutoEventWireup="true" Inherits="G2G_LIB.UserControls.CollectionCallControl" %>

<!--Styling-->
<style>
    .btn-style {
        margin-left: 5px;
        margin-right: 5px;
    }
    .form-control {
        width: auto;
    }
    .ui-button.ui-widget {
        height:25px;
        font-size: 13px;
        font-family: SANS-SERIF;
    }
    .txt-text {
        padding-bottom: 10px;
    }
    .txt-label {
        padding-bottom: 5px;
    }
    .ui-dialog-titlebar {
        display: inline-block;
    }
    .ui-dialog-buttonpane {
        display:none;
    }
    .button-save{
        border: 1px solid #aaa;
        text-decoration: none;
        font-family: Arial;
        color: black;
        padding: 3px 8px;
        font-size: 13px;
        background-color: #C5EFBF; 
        margin-left:2px;
        margin-right:2px;
        border-radius: 2px;
    }
    .button-save:hover {
       background-color: #B9E0B4;
       text-decoration: none;
       color: black;
    }

    .button-selectAll{
        border: 1px solid #aaa;
        text-decoration: none;
        font-family: Arial;
        color: black;
        padding: 3px 8px;
        font-size: 13px;
        background: #BADBEA; 
        border-radius: 2px;
    }
    .button-selectAll:hover {
       background: #B1D1DF;
       text-decoration: none;
       color: black;
    }
    .button-cancel{
        border: 1px solid #aaa;
        text-decoration: none;
        font-family: Arial;
        color: black;
        padding: 3px 8px;
        font-size: 13px;
        background: #ff8585; 
        border-radius: 2px;
    }
    .button-cancel:hover {
       background: #e87676;
       text-decoration: none;
       color: black;
    }

    .center-text {
        text-align: center;
    }
    th {
        padding-left: 20px;
        padding-right: 20px;
    }
</style>

<!--View-->
<div id="dialog-collectionCall" title="Collection Call" style="margin:auto;">
    <div class="row">
        <!--repeater for collection calls that belong to this customer-->
        <div class="col-sm-12">
            <asp:Repeater ID="rptCollectionCalls" runat="server" OnItemDataBound="rptCollectionCalls_ItemDataBound">
                <HeaderTemplate>
                    <table style="width:100%;" id="tblCollectionCalls" class="table table-hover">
                        <tr class="table-secondary">
                            <th class="center-text" scope="col">
                                
                            </th>
                            <th class="center-text" scope="col">
                                Request No.
                            </th>
                            <th class="center-text" scope="col">
                                Inv No.
                            </th>
                            <th class="center-text" scope="col">
                                Inv Amt.
                            </th>
                            <th class="center-text" scope="col">
                                Balance
                            </th>
                            <th class="center-text" scope="col">
                                Inv Date
                            </th>
                            <th class="center-text" scope="col">
                                Due Date
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="table-row">
                        <td class="center-text">
                            <asp:checkbox ID="cbxSelect" runat="server" CssClass="cbxSelect" onclick="handleRowToBatch(this)" ClientIDMode="Static"/>
                        </td>
                        <td class="center-text">
                            <asp:Label ID="lblHid" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.HID") %>' />
                        </td>
                        <td class="center-text">
                            <asp:Label ID="lblInvNo" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.InvNo") %>' />
                        </td>
                        <td class="center-text">
                            $<asp:Label ID="lblInvAmt" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.InvAmt")).ToString("0.00") %>' />
                        </td>
                        <td class="center-text">
                            $<asp:Label ID="lblBalance" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.Balance")).ToString("0.00") %>' />
                        </td>
                        <td class="center-text">
                            <asp:Label ID="lblInvDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.InvDt", "{0:M/d/yyyy}") %>' />
                        </td>
                        <td class="center-text">
                            <asp:Label ID="lblDueDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DueDt", "{0:M/d/yyyy}") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr id="table-row" class="table-primary">
                        <td class="center-text"
                            <asp:checkbox ID="cbxSelect" runat="server" CssClass="cbxSelect" onclick="handleRowToBatch(this)" ClientIDMode="Static"/>
                        </td>
                        <td class="center-text">
                            <asp:Label ID="lblHid" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.HID") %>' />
                        </td>
                        <td class="center-text">
                            <asp:Label ID="lblInvNo" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.InvNo") %>' />
                        </td>
                        <td class="center-text">
                            $<asp:Label ID="lblInvAmt" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.InvAmt")).ToString("0.00") %>' />
                        </td>
                        <td class="center-text">
                            $<asp:Label ID="lblBalance" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.Balance")).ToString("0.00") %>' />
                        </td>
                        <td class="center-text">
                            <asp:Label ID="lblInvDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.InvDt", "{0:M/d/yyyy}") %>' />
                        </td>
                        <td class="center-text">
                            <asp:Label ID="lblDueDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DueDt", "{0:M/d/yyyy}") %>' />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    <tr style="border-top: 1px solid black">
                        <td class="center-text" style="font-weight:bold;">Total</td>
                        <td></td>
                        <td></td>
                        <td class="center-text">$<asp:Label ID="lblInvAmtTotal" runat="server" /></td>
                        <td class="center-text">$<asp:Label ID="lblBalanceTotal" runat="server" /></td>
                        <td></td>
                        <td></td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>



<%--    <div class="row">
        <div class="col-sm-4 txt-label">
            <asp:Label runat="server" ID="lblName" Text="Payee Name:"></asp:Label>
        </div>
        <div class="col-sm-8 txt-text">
            <asp:TextBox runat="server" ID="txtName" style="font-size:small; width:100%;"></asp:TextBox>
        </div>
    </div>--%>

    <div class="row">
        <div class="col-sm-12" style="padding-top:5px;">
            <div>
                <asp:LinkButton runat="server" ID="btnClose" Text="Cancel" OnClientClick="js_btnCancel_Click(); return false;" CssClass="button-cancel" ClientIDMode="Static" style="float:right;"/>
            </div>
            <div>
                <asp:LinkButton runat="server" ID="btnSubmit" Text="Save" OnClientClick="js_btnSubmitCollectionCall_Click();" CssClass="button-save" ClientIDMode="Static" style="float:right;"/>
            </div>
            <div>
                <asp:LinkButton runat="server" ID="btnSelectAll" Text="Select All" OnClientClick="js_btnSelectAll_Click(); return false;" CssClass="button-selectAll" ClientIDMode="Static" style="float:right;"/>
            </div>
        </div>
    </div>
</div>

<!--Scripts-->
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
<link  href="https://code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css" rel="stylesheet" />

<script>
    var batchHIDs = [];

    $(function () {
        $('#dialog-collectionCall').dialog({
            autoOpen: false,
            modal: true,
            height: 'auto',
            width: 'auto',
            buttons: {
                Cancel: function () {
                    js_btnDeselectAll_Click();
                    $(this).dialog("close");
                }
            }
        });
 
    });

    function handleRowToBatch(cbxSelect) {
        //if checked == true add this HID to the public list of HIDs.  If false, remove it
        if (cbxSelect.checked == true) {
            addHidToBatchlist(cbxSelect.id);
        }
        else if (cbxSelect.checked == false) {
            removeHidToBatchlist(cbxSelect.id);
        }
    }

    function addHidToBatchlist(hid) {
        var alreadyThere = batchHIDs.includes(hid);
        if (alreadyThere) {
            
        }
        else {
            batchHIDs.push(hid);
        }
    }

    function removeHidToBatchlist(hid) {
        var alreadyThere = batchHIDs.includes(hid);
        if (alreadyThere) {
            var filteredArray = batchHIDs.filter(function (value, index, arr) {
                return value != hid;
            });
            batchHIDs = filteredArray;
        }
        else {

        }
    }

    function js_btnSelectAll_Click() {
        //select all checkboxes in repeater
        $('input:checkbox').prop('checked', true);

        //save all rows to hdnCollectionCallBatch
        var spans = document.getElementsByClassName("cbxSelect");
        console.log(spans);
        for (let i = 0; i < spans.length; i++) {
            addHidToBatchlist(spans[i].children[0].id);
        }
    }

    function js_btnDeselectAll_Click() {
        //deselect all checkboxes in repeater
        $('input:checkbox').prop('checked', false);
        localStorage.clear();
        $('#hdnCollectionCallBatch').val(null);
    }

    function js_btnSubmitCollectionCall_Click() {
        localStorage.setItem("batchHIDs", JSON.stringify(batchHIDs));
        var batchHIDsString = JSON.stringify(batchHIDs);
        setBatchHIDsHiddenValue(batchHIDsString);
        $('#dialog-collectionCall').dialog("close");
    }

    function js_btnCancel_Click() {
        $('#dialog-collectionCall').dialog("close");
    }

    function setBatchHIDsHiddenValue(serializedBatch) {
        $('#hdnCollectionCallBatch').val(serializedBatch);
    }
</script>