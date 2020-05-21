<%@ Page Title="" Language="C#" MasterPageFile="~/mainContent.master" AutoEventWireup="true" CodeFile="EaseBuzz_Data.aspx.cs" Inherits="EaseBuzz_Data" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ MasterType VirtualPath="~/mainContent.master" %>

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lbl_error" CssClass="lbl" runat="server" ForeColor="Red"> </asp:Label>


    <div class="col-md-12" id="MAIN_PANEL" runat="server" style="margin: 0; padding: 0;">
        <section class="panel" style="max-width: 100%;">

            <header class="panel-heading">
                Easebuzz Data
            </header>
            <div class="panel-body" style="border: 0px solid #ccc !important;">

                <div class="row">

                    <div class="col-md-12 form-group">


                        <div class="col-md-2 form-group">
                            <telerik:RadDatePicker ID="demo1" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                                AutoPostBack="false" ShowPopupOnFocus="True"
                                Skin="Silk" Visible="true" Calendar-Skin="Silk" ToolTip="From Date" DateInput-EmptyMessage="From Date">
                            </telerik:RadDatePicker>
                        </div>
                        <div class="col-md-2 form-group">
                            <telerik:RadDatePicker ID="demo2" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                                ShowPopupOnFocus="True" Skin="Silk" Visible="true" Calendar-Skin="Silk" ToolTip="To Date"
                                DateInput-EmptyMessage="To Date">
                            </telerik:RadDatePicker>
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:Button ID="btn_new" runat="server" Text="Show Data" class="btn btn-primary" OnClick="btn_new_Click"></asp:Button>

                            <asp:Button ID="btn_Export" runat="server" Text="Export" Width="80px" class="btn btn-primary" OnClick="btn_Export_Click" Visible="false"></asp:Button>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="col-md-12 form-group">


                        <telerik:RadGrid ID="grid_data" runat="server" ShowStatusBar="True" AllowPaging="TRUE" Visible="false"
                            AllowSorting="True" GroupingEnabled="False" AllowFilteringByColumn="false" AutoGenerateColumns="true"
                            EnableHeaderContextMenu="True" Skin="WebBlue" OnNeedDataSource="grid_data_NeedDataSource"
                            Width="100%" GridLines="Both" PageSize="100" AlternatingItemStyle-HorizontalAlign="Center"
                            ExportSettings-ExportOnlyData="True" ExportSettings-HideStructureColumns="True">

                            <MasterTableView GridLines="Both" HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" AllowFilteringByColumn="false"
                                ClientDataKeyNames="ENTITY_ID" ItemStyle-HorizontalAlign="Center" CommandItemDisplay="None" CommandItemSettings-ShowAddNewRecordButton="False" CommandItemSettings-ShowRefreshButton="False">
                                <HeaderStyle Width="150px" />
                                <%--<Columns>--%>

                                <%-- <telerik:GridBoundColumn DataField="ENTITY_CODE" UniqueName="ENTITY_CODE" HeaderText="ENTITY CODE" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="BANK_ACCOUNT" UniqueName="BANK_ACCOUNT" HeaderText="BANK ACCOUNT" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PAN_NUMBER" UniqueName="PAN_NUMBER" HeaderText="PAN NUMBER " HeaderStyle-Width="100px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn> 
                                        <telerik:GridBoundColumn DataField="CERTI_NAME" UniqueName="CERTI_NAME" HeaderText="CERTI NAME" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MOBILE_NO" UniqueName="MOBILE_NO" HeaderText="MOBILE NO" HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EMAIL" UniqueName="EMAIL" HeaderText="EMAIL" HeaderStyle-Width="70px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="BANK_NAME" UniqueName="BANK_NAME" HeaderText="BANK NAME" HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BRANCH_NAME" UniqueName="BRANCH_NAME" HeaderText="BRANCH NAME" HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                --%>


                                <%--</Columns>--%>
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

        </section>
    </div>




</asp:Content>

