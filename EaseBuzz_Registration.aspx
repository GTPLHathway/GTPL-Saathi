<%@ Page Title="" Language="C#" MasterPageFile="~/MainUpload.master" AutoEventWireup="true" CodeFile="EaseBuzz_Registration.aspx.cs" Inherits="EaseBuzz_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ MasterType VirtualPath="~/MainUpload.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />

    <script type="text/javascript" language="JavaScript">
        function setFocus() {
            document.getElementById("txtserial").focus();
        }
        // Number Validation
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode != 99 && charCode != 118 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        //Disable right click script
        var message = "";
        ///////////////////////////////////
        function clickIE() { if (document.all) { (message); return false; } }
        function clickNS(e) {
            if (document.layers || (document.getElementById && !document.all)) {
                if (e.which == 2 || e.which == 3) { (message); return false; }
            }
        }
        if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
        else { document.onmouseup = clickNS; document.oncontextmenu = clickIE; }
        document.oncontextmenu = new Function("return false")

    </script>
    <style>
        .rcbInput {
            height: 30px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lbl_error" CssClass="lbl" runat="server" ForeColor="Red"> </asp:Label>


    <div class="col-md-12" id="MAIN_PANEL" runat="server" style="margin: 0; padding: 0;">
        <section class="panel" style="max-width: 100%;">

            <header class="panel-heading">
                Easebuzz Registration
            </header>
            <div class="panel-body" style="border: 0px solid #ccc !important;">
                <div class="row">
                    <div class="col-md-4 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">Select Entity
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue=""
                                ControlToValidate="drop_entity_code" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>
                            </span>
                            <telerik:RadComboBox ID="drop_entity_code" CausesValidation="false" DropDownAutoWidth="Enabled" AutoPostBack="true" ValidationGroup="Commission"
                                OnSelectedIndexChanged="drop_entity_code_SelectedIndexChanged" runat="server" Style="height: 32px;" Width="100%" Skin="Silk" MarkFirstMatch="True" EmptyMessage="Select Entity">
                            </telerik:RadComboBox>
                        </div>

                    </div>

                    <div class="col-md-4 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">Mobile No
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="txt_mobile" ErrorMessage="*" ValidationGroup="valid"
                                    ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue=""
                                    ControlToValidate="txt_mobile" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox ID="txt_mobile" runat="server" placeholder="Mobile_no" CssClass="form-control" Enabled="true">
                            </asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-4 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">Email
                               <asp:RegularExpressionValidator ID="regexEmailValid"
                                   ValidationGroup="valid" runat="server" ForeColor="Red"
                                   ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_email" ErrorMessage="*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue=""
                                    ControlToValidate="txt_email" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox ID="txt_email" runat="server" placeholder="Email" CssClass="form-control" Enabled="true">
                            </asp:TextBox>

                        </div>
                    </div>


                </div>

                <div class="row">

                    <div class="col-md-6 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">Network Name</span>
                            <asp:TextBox ID="txt_Ntework_name" runat="server" placeholder="Network Name" CssClass="form-control" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-6 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">Contact person</span>
                            <asp:TextBox ID="txt_Contact_person" runat="server" placeholder="Contact Person" CssClass="form-control" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>


                </div>


                <div class="row">

                    <div class="col-md-12 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">Address
                         
                            </span>
                            <asp:TextBox ID="txt_Address" Enabled="false" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>

                        </div>

                    </div>

                </div>


                <div class="row">
                    <div class="col-md-4 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">Bank Name
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                    ControlToValidate="drp_bankname" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>

                            </span>
                            <telerik:RadComboBox ID="drp_bankname" AutoPostBack="true" runat="server" Visible="true" OnSelectedIndexChanged="drp_bankname_SelectedIndexChanged"
                                Style="height: 32px;" Width="100%" DataSourceID="sqlentity1" DropDownWidth="300px" DataTextField="BANK_DESCR" DataValueField="BANK_CODE" Skin="Silk" MarkFirstMatch="True" EmptyMessage="Bank Name">
                            </telerik:RadComboBox>

                        </div>


                    </div>
                    <asp:SqlDataSource runat="server" ID="sqlentity1" ConnectionString="<%$ ConnectionStrings:ConGTPL %>"
                        ProviderName="System.Data.OracleClient"></asp:SqlDataSource>
                    <div class="col-md-4 form-group">

                        <div class="input-group m-b-10">
                            <span class="input-group-addon">Branch Name

                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue=""
                                     ControlToValidate="drp_brnch_name" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                     ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>

                                <asp:SqlDataSource runat="server" ID="sqlentity2" ConnectionString="<%$ ConnectionStrings:ConGTPL %>"
                                    ProviderName="System.Data.OracleClient"></asp:SqlDataSource>


                            </span>
                            <telerik:RadComboBox ID="drp_brnch_name" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_brnch_name_SelectedIndexChanged"
                                Style="height: 32px;" Width="100%" DataSourceID="sqlentity2" DropDownWidth="300px" DataTextField="BRANCH_NAME" DataValueField="BRANCH_NAME" Skin="Silk" MarkFirstMatch="True" EmptyMessage="Branch Name">
                            </telerik:RadComboBox>
                        </div>

                    </div>


                    <div class="col-md-4 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">IFSC Code
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue=""
                                       ControlToValidate="txt_bank_detail" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                       ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox ID="txt_IFSC" Enabled="false" runat="server" CssClass="form-control" placeholder="IFSC"></asp:TextBox>

                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">Bank Account No
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                        ControlToValidate="txt_bank_detail" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                        ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox ID="txt_bank_detail" runat="server" placeholder="Bank Account No" CssClass="form-control" Enabled="true">
                            </asp:TextBox>
                        </div>
                        <asp:Label ID="Label1" runat="server" Font-Bold="false" Font-Names="Arial" Font-Size="13px"
                            ForeColor="#1261ac" Text="Current Bank Account"></asp:Label>
                    </div>

                    <div class="col-md-3 form-group">

                        <div class="input-group m-b-10" style="height: 34px;width:100%">
                            <span class="input-group-addon" style="height: 34px;width:100%">Cover page of the Cheque book
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue=""
                                   ControlToValidate="file_up_bankdetail" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                   ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>

                            </span>

                            <asp:Label ID="Label4" runat="server" Font-Bold="false" Font-Names="Arial" Font-Size="13px"
                                ForeColor="#1261ac" Text=""></asp:Label>

                        </div>

                    </div>
                    <div class="col-sm-3 form-group" >
                        <asp:FileUpload ID="file_up_bankdetail" runat="server" CssClass="btn btn-primary" Width="100%" accept=".png,.jpg,.jpeg,.gif,.pdf" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 form-group">
                        <div class="input-group m-b-10">
                            <span class="input-group-addon">PAN Number
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                                        ControlToValidate="txt_PAN_no" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                        ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox ID="txt_PAN_no" runat="server" placeholder="PAN Number" CssClass="form-control" Enabled="true">
                            </asp:TextBox>
                        </div>
                        <asp:Label ID="Label2" runat="server" Font-Bold="false" Font-Names="Arial" Font-Size="13px"
                            ForeColor="#1261ac" Text="Company PAN / Proprietor PAN"></asp:Label>
                    </div>

                    <div class="col-md-3 form-group">

                        <div class="input-group m-b-10" style="height: 34px;width:100%">
                            <span class="input-group-addon" style="height: 34px;width:100%">Copy of PAN Card &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=""
                                 ControlToValidate="FileUp_PAN" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                 ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>
                            </span>
                            <asp:Label ID="Label5" runat="server" Font-Bold="false" Font-Names="Arial" Font-Size="13px"
                                ForeColor="#1261ac" Text=""></asp:Label>

                        </div>
                        <asp:Label ID="Label3" runat="server" Font-Bold="false" Font-Names="Arial" Font-Size="13px"
                            ForeColor="#1261ac" Text="In Case of Company, Company  PAN. In Case of Proprietor, Personal PAN  "></asp:Label>
                    </div>
                    <div class="col-sm-3 form-group" >
                        <asp:FileUpload ID="FileUp_PAN" runat="server" CssClass="btn btn-primary" Width="100%" accept=".png,.jpg,.jpeg,.gif,.pdf" />

                    </div>


                </div>
                <div class="row">
                    <div class="col-sm-3 form-group">
                        <div class="input-group m-b-10" style="height: 34px;width:100%">
                            <span class="input-group-addon" style="height: 34px;width:100%">
                                <asp:DropDownList ID="drop_certi" CausesValidation="false" 
                                    runat="server" MarkFirstMatch="True" Height="21px" EmptyMessage="Select Certificate">

                                    <asp:ListItem Value="" Selected="True"> Select Certificate </asp:ListItem>
                                    <asp:ListItem Value="1"> GST Certificate </asp:ListItem>
                                    <asp:ListItem Value="2"> Incorporation Certificate </asp:ListItem>
                                    <asp:ListItem Value="3"> Registration Certificate </asp:ListItem>
                                    <asp:ListItem Value="4"> Shop / Establish Certificate </asp:ListItem>

                                </asp:DropDownList>

                            </span>
                            <asp:Label ID="Label7" runat="server" Font-Bold="false" Font-Names="Arial" Font-Size="13px"
                                ForeColor="#1261ac" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-3 form-group" >
                        <asp:TextBox ID="txt_certi" runat="server" placeholder="GST No / Registration No / Certificate No " CssClass="form-control" Enabled="true">
                        </asp:TextBox>
                    </div>
                    <div class="col-md-3 form-group">

                        <div class="input-group m-b-10" style="height: 34px;width:100%">
                            <span class="input-group-addon" style="height: 34px;width:100%">Copy of Certificate (any one)  &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                                ControlToValidate="FileUp_CERTI" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"
                                ForeColor="Red" ValidationGroup="valid">*</asp:RequiredFieldValidator>
                            </span>

                            <asp:Label ID="Label6" runat="server" Font-Bold="false" Font-Names="Arial" Font-Size="13px"
                                ForeColor="#1261ac" Text=""></asp:Label>

                        </div>
                    </div>
                    <div class="col-sm-3 form-group" >
                        <asp:FileUpload ID="FileUp_CERTI" runat="server" CssClass="btn btn-primary" Width="100%" accept=".png,.jpg,.jpeg,.gif,.pdf" />
                    </div>
                </div>

                <div class="row">

                    <div class="col-md-12" style="text-align: center">

                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary" Text="Submit"
                            Width="100" OnClick="btn_save_Click" ValidationGroup="valid" />

                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-danger"
                            Text="Cancel" Width="100" OnClick="btn_cancel_Click" />


                    </div>
                </div>

            </div>

        </section>
    </div>

</asp:Content>

