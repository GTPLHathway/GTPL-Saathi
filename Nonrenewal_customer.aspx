<%@ Page Title="" Language="C#" MasterPageFile="~/MainUpload.master" AutoEventWireup="true" CodeFile="Nonrenewal_customer.aspx.cs" Inherits="Nonrenewal_customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ MasterType VirtualPath="~/MainUpload.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <style type="text/css">
        #grid_data {
            box-shadow: 0 0 2px rgba(0, 0, 0, 0.4) !important;
            border: 1px solid #4d87a5 !important;
        }

        .RadComboBoxDropDown.rcbAutoWidth {
            min-width: 200px !important;
        }

       

        /*.rwTable {
            height: 550px !important;
            width: 700px !important;
        }*/

        #RadWindowWrapper_rad_add_client, #RadWindowWrapper_rad_edit_client {
            top: 20px !important;
        }

        .rgCommandTable td {
            border: 0;
            padding: 2px 7px;
            text-align: right;
        }


        .RadGrid_Silk .rgHeader a {
            color: #0394ae !important;
            font-size: 11px;
            font-weight: bold;
            text-transform: uppercase;
        }

        .RadGrid .rgFilterBox {
            height: 22px !important;
            width: 100% !important;
        }

        .rwTable {
            height: 600px !important;
            width: 900px !important;
        }

        #RadWindowWrapper_rad_alacarte, #RadWindowWrapper_rad_servicegroup {
            top: 10px !important;
        }

        .RadInput_Silk .riTextBox, html body .RadInputMgr_Silk {
            border-radius: 0.3em;
            height: 33px !important;
        }

        html .RadComboBox .rcbInput {
            height: 30px;
        }

        table, tr, td {
            text-align: center;
        }

        thead tr td {
            color: #333333;
            font-family: Arial;
            font-weight: bold;
        }

        table tr td a {
            color: #333;
            padding: 5px;
        }

        .kishan {
            background-color: #f9ddb2;
            text-align: center;
        }

        #overlay {
            background-color: #000000;
            height: 100%;
            left: 0;
            opacity: 0.6;
            position: fixed;
            top: 0;
            width: 100%;
            z-index: 1500;
        }

        #theprogress {
            background-color: #000000;
            border: 0 solid #CCCCCC;
            height: 30px;
            line-height: 30px;
            opacity: 0.6;
            padding: 10px;
            text-align: center;
            width: 300px;
        }

        #modalprogress {
            position: absolute;
            top: 45%;
            left: 50%;
            margin: -11px 0 0 -150px;
            color: #990000;
            font-weight: bold;
            font-size: 14px;
        }

        .RepeaterLabel {
            color: #2980b9;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lbl_error" CssClass="lbl" runat="server" ForeColor="Red"> </asp:Label>

    
    <div class="col-md-12" id="MAIN_PANEL" runat="server" style="margin: 0; padding: 0;">
        <section class="panel" style="max-width: 100%;">
            <header class="panel-heading">
                Nonrenewal Customer
            </header>

    <div class="panel-body" style="border: 0px solid #ccc !important;">
         <div class="row">
                    <div class="radio radiobuttonlist col-md-12" style="margin: 0 0 0px 15px; padding: 0px; height: 40px">
                        <asp:RadioButtonList ID="rbt_Nonrenewal_cust" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbt_Nonrenewal_cust_SelectedIndexChanged"
                            RepeatLayout="Flow" RepeatDirection="Horizontal" Font-Bold="True">
                            <asp:ListItem class="radio-inline " Value="View"  style="color: #16a085; font-weight: bold;">View Data</asp:ListItem>
                            <asp:ListItem class="radio-inline " Value="Upload" style="color: #16a085; font-weight: bold;">Upload Data</asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                </div>

         <div class="col-md-12" id="View_data" runat="server" visible="false">

              <div class="row">

                  <div class="col-md-12 form-group">
                        <asp:Button ID="btnSearch" Visible="true" runat="server" Text="Show Data" class="btn btn-primary" OnClick="btnSearch_Click" OnClientClick="javascript:showWait();"></asp:Button>
                         <asp:Button ID="btn_Export" runat="server" Text="Export" Width="80px"  class="btn btn-primary" OnClick="btn_Export_Click" Visible="false"></asp:Button>
                    </div>
                  </div>

                   <div class="row">
                        <div class="col-md-12">
                            <telerik:RadGrid ID="grid_data" runat="server" ShowStatusBar="True" AllowPaging="TRUE" visible="false"
                                AllowSorting="True" GroupingEnabled="False" AllowFilteringByColumn="true" AutoGenerateColumns="false"
                                EnableHeaderContextMenu="True" Skin="WebBlue" OnNeedDataSource="grid_data_NeedDataSource"
                                Width="100%" GridLines="Both"  PageSize="100" AlternatingItemStyle-HorizontalAlign="Center"
                                ExportSettings-ExportOnlyData="True" ExportSettings-HideStructureColumns="True">

                                <MasterTableView GridLines="Both" HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" AllowFilteringByColumn="true"
                                    ClientDataKeyNames="ENTITY_ID" ItemStyle-HorizontalAlign="Center" CommandItemDisplay="None" CommandItemSettings-ShowAddNewRecordButton="False" CommandItemSettings-ShowRefreshButton="False">
                                    <Columns>
                                        
                                         <telerik:GridBoundColumn DataField="CUSTOMER_NBR" UniqueName="CUSTOMER_NBR" HeaderText="CUSTOMER NBR" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="CUSTOMER_NAME" UniqueName="CUSTOMER_NAME" HeaderText="CUSTOMER NAME" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MOBILE_PHONE" UniqueName="MOBILE_PHONE" HeaderText="MOBILE " HeaderStyle-Width="100px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn> 
                                        <telerik:GridBoundColumn DataField="SERIAL_NUMBER" UniqueName="SERIAL_NUMBER" HeaderText="SERIAL NUMBER" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ADDRESS" UniqueName="ADDRESS" HeaderText="ADDRESS" HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ENTITY_CODE" UniqueName="ENTITY_CODE" HeaderText="ENTITY CODE" HeaderStyle-Width="70px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="NETWORK_NAME" UniqueName="NETWORK_NAME" HeaderText="ENTITY NAME" HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="INACTIVE_DATE_NEW" UniqueName="INACTIVE_DATE_NEW" HeaderText="INACTIVE DATE" HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                        
                                        
                                       
                      
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings ReorderColumnsOnClient="True" EnableRowHoverStyle="True" EnablePostBackOnRowClick="false"
                                    Selecting-AllowRowSelect="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                                <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" />
                            </telerik:RadGrid>

                        </div>
                    </div>

             </div>

         <div class="col-md-12" id="pnl_upload" runat="server" visible="false">

                    <div class="row">
                        <div class="col-md-3 form-group">
                            <asp:FileUpload ID="file_upload" runat="server" CssClass="btn btn-primary" Width="100%" />
                        </div>
                        <div class="col-md-2 form-group">
                            <asp:Button ID="btn_upload" runat="server" Text="Upload Data" Width="100%" class="btn btn-success" OnClientClick="javascript:showWait();" OnClick="btn_upload_Click"></asp:Button>
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:LinkButton ID="LNK_DOWNLOAD" runat="server" Width="100%" class="RepeaterLabel" OnClientClick="javascript:showWait();"
                                OnClick="LNK_DOWNLOAD_Click" Visible="true">( Download Sample File Formate )</asp:LinkButton>
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:LinkButton ID="LNK_REASON" runat="server" Width="100%" class="RepeaterLabel" OnClientClick="javascript:showWait();"
                                OnClick="LNK_REASON_Click" Visible="true">( Download Reason Master )</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 form-group">
                            <asp:Label ID="lblupload_error" runat="server" Text="" CssClass="RepeaterLabel"></asp:Label>
                        </div>
                    </div>
                </div>

        </div>
            </section>
        </div>

</asp:Content>

