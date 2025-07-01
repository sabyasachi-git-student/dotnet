<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="SupplierMaster.aspx.cs" Inherits="Project_SupplierMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/jscript" src="../js/jquery.min.js"></script>
   
    <script type="text/javascript">

        $(document).ready(function () {
            var checkCompanyShow = $("#<%=CompanyHidden.ClientID%>").val();
            if (checkCompanyShow == 1)
                $("#<%=CompanyShow.ClientID%>").show();
            else
                $("#<%=CompanyShow.ClientID%>").hide();
            var CompanyBankShowHidden = $("#<%=CompanyBankShowHidden.ClientID%>").val();
            if (CompanyBankShowHidden == 1)
                $("#<%=CompanyBankShow.ClientID%>").show();
            else
                $("#<%=CompanyBankShow.ClientID%>").hide();
            var CompanyLegalShowHidden = $("#<%=CompanyLegalShowHidden.ClientID%>").val();
            if (CompanyLegalShowHidden == 1)
                $("#<%=CompanyLegalShow.ClientID%>").show();
            else
                $("#<%=CompanyLegalShow.ClientID%>").hide();
            var ConcernedPersonShowHidden = $("#<%=ConcernedPersonShowHidden.ClientID%>").val();
            if (ConcernedPersonShowHidden == 1)
                $("#<%=ConcernedPersonShow.ClientID%>").show();
            else
                $("#<%=ConcernedPersonShow.ClientID%>").hide();
            $("#<%=btn_CompanyDetailsSubmit.ClientID%>").click(function () {
                $("#<%=CompanyLegalShow.ClientID%>").hide();
                $("#<%=CompanyLegalShowHidden.ClientID%>").val("2");
            });



            $("#NextCompany").click(function () {

                $("#<%=CompanyShow.ClientID%>").hide();
                $("#<%=CompanyHidden.ClientID%>").val("2");
                $("#<%=CompanyBankShow.ClientID%>").show();
                $("#<%=CompanyBankShowHidden.ClientID%>").val("1");
            });
            $("#NextCompanyBank").click(function () {
                $("#<%=CompanyBankShow.ClientID%>").hide();
                $("#<%=CompanyBankShowHidden.ClientID%>").val("2");
                $("#<%=CompanyLegalShow.ClientID%>").show();
                $("#<%=CompanyLegalShowHidden.ClientID%>").val("1");

            });
            $("#NextItemAssign").click(function () {


                $("#<%=CompanyLegalShow.ClientID%>").hide();
                $("#<%=CompanyLegalShowHidden.ClientID%>").val("2");


            });

            $("#CompanyShowAllt").click(function () {

                $("#<%=CompanyShow.ClientID%>").show();
                $("#<%=CompanyBankShow.ClientID%>").show();
                $("#<%=CompanyLegalShow.ClientID%>").show();
                $("#<%=ConcernedPersonShow.ClientID%>").show();
                $("#<%=CompanyHidden.ClientID%>").val("1");
                $("#<%=CompanyBankShowHidden.ClientID%>").val("1");
                $("#<%=CompanyLegalShowHidden.ClientID%>").val("1");
                $("#<%=ConcernedPersonShowHidden.ClientID%>").val("1");
                $("#<%=followupHidden.ClientID%>").val("1");

            });
            $("#CompanyShowHideAllt").click(function () {
                $("#<%=CompanyShow.ClientID%>").hide();
                $("#<%=CompanyBankShow.ClientID%>").hide();
                $("#<%=CompanyLegalShow.ClientID%>").hide();
                $("#<%=ConcernedPersonShow.ClientID%>").hide();
                $("#<%=CompanyHidden.ClientID%>").val("2");
                $("#<%=CompanyBankShowHidden.ClientID%>").val("2");
                $("#<%=CompanyLegalShowHidden.ClientID%>").val("2");
                $("#<%=ConcernedPersonShowHidden.ClientID%>").val("2");
                $("#<%=followupHidden.ClientID%>").val("2");

            });
            $("#CompanyShowt").click(function () {
                $("#<%=CompanyHidden.ClientID%>").val("1");
                $("#<%=CompanyShow.ClientID%>").show();
            });
            $("#CompanyHidet").click(function () {
                $("#<%=CompanyHidden.ClientID%>").val("2");
                $("#<%=CompanyShow.ClientID%>").hide();
            });
            $("#CompanyBankShowt").click(function () {
                $("#<%=CompanyBankShowHidden.ClientID%>").val("1");
                $("#<%=CompanyBankShow.ClientID%>").show();
            });
            $("#CompanyBankHidet").click(function () {
                $("#<%=CompanyBankShowHidden.ClientID%>").val("2");
                $("#<%=CompanyBankShow.ClientID%>").hide();
            });
            $("#CompanyLegalShowt").click(function () {
                $("#<%=CompanyLegalShowHidden.ClientID%>").val("1");
                $("#<%=CompanyLegalShow.ClientID%>").show();
            });
            $("#CompanyLegalHidet").click(function () {
                $("#<%=CompanyLegalShow.ClientID%>").hide();
                $("#<%=CompanyLegalShowHidden.ClientID%>").val("2");
            });
            $("#ConcernedPersonShowt").click(function () {
                $("#<%=ConcernedPersonShowHidden.ClientID%>").val("1");
                $("#<%=ConcernedPersonShow.ClientID%>").show();
            });
            $("#ConcernedPersonHidet").click(function () {
                $("#<%=ConcernedPersonShowHidden.ClientID%>").val("2");
                $("#<%=ConcernedPersonShow.ClientID%>").hide();
            });




        });
    </script>
    <style type="text/css">
        .back {
            background-color: white;
            border-color: black;
            margin-top: 5px;
            border: 2px solid black;
        }

        .resize {
            resize: none;
        }

        .Next {
            background-color: #003366;
            color: White;
            height: 32px;
            width: 75px;
        }

        .hidden {
            display: none;
        }

        .gridView {
            table-layout: fixed;
        }

        .col {
            word-wrap: break-word;
        }
    </style>
   

    <script type="text/javascript">
        window.document.onkeydown = function (e) {
            if (!e) {
                e = event;
            }
            if (e.keyCode == 27) {
                lightbox_close();
            }
        }
        function lightbox_open(Divname) {
            window.scrollTo(0, 0);
            document.getElementById(Divname).style.display = 'block';
            document.getElementById('fade').style.display = 'block';
            return false;
        }
        function lightbox_close() {
            document.getElementById(Divname).style.display = 'none';
            document.getElementById('fade').style.display = 'none';
            return false;
        }

    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
           <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Supplier Master</h3>
            </div>
            <div align="center">
                <table cellspacing="15px" align="center">
                
                    <tr>
                        <td>
                            <asp:HiddenField ID="CompanyHidden" runat="server" Value="2" />
                            <asp:HiddenField ID="CompanyBankShowHidden" runat="server" Value="2" />
                            <asp:HiddenField ID="CompanyLegalShowHidden" runat="server" Value="2" />
                            <asp:HiddenField ID="ConcernedPersonShowHidden" runat="server" Value="2" />
                            <asp:HiddenField ID="ItemShowHidden" runat="server" Value="2" />
                            <asp:HiddenField ID="followupHidden" runat="server" Value="2" />

                            <asp:ScriptManager ID="ScriptManager" runat="server">
                            </asp:ScriptManager>
                        </td>
                        
                    </tr>
                   
                    <tr>
                        <td colspan="3">
                            <div id="Company" class="back" align="center">
                                <table class="dxflInternalEditorTable_Metropolis">
                                    <tr>
                                       
                                        <td style="font-size: large;" ><strong>Company Details</strong></td>
                                    </tr>
                                </table>
                            </div>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div id="CompanyShow" runat="server" class="back">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100%;">
                                            <table cellspacing="15px" align="center">
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                         <asp:CheckBox runat="server" Visible="false" ID="chbxCompositionScheme" Font-Bold="true" Text="Composition Scheme" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">
                                                        Supplier For : <span style="color: red;">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlVendorFor" Height="30px" Width="250px">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Product" Value="Product"></asp:ListItem>
                                                            <asp:ListItem Text="Service" Value="Service"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="font-weight: bold;">
                                                        MSME/NON MSME : <span style="color: red;">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlMsmeNonMsme" Height="30px" Width="250px">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="MSME" Value="MSME"></asp:ListItem>
                                                            <asp:ListItem Text="NON MSME" Value="NON MSME"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox runat="server" Visible="false" ID="chbxVerified" Font-Bold="true" Text="Verified" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                   
                                                    <td style="font-weight: bold;">
                                                        SC_ST/NON-SC_ST : <span style="color: red;">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlScStOrNot" Height="30px" Width="250px">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="SC_ST" Value="SC_ST"></asp:ListItem>
                                                            <asp:ListItem Text="NON-SC_ST" Value="NON-SC_ST"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="font-weight: bold;">
                                                        Women Owner : <span style="color: red;">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlWomenOwner" Height="30px" Width="250px">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">Group Name :
                                                        <span style="color: red;">*</span>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="txtGroupName" runat="server" DropDownWidth="200px" Height="30px" Width="250px" Theme="Office2003Blue" DropDownStyle="DropDown" IncrementalFilteringMode="Contains"
                                                            DataSourceID="SqlDataSource1111" TextField="GroupName" ValueField="GroupId" />
                                                        <asp:SqlDataSource ID="SqlDataSource1111" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from [dbo].[tbl_SupplierGroupMaster]"></asp:SqlDataSource>
                                                    </td>
                                                    <td style="font-weight: bold;">Company Type :
                                                        <span style="color: red;">*</span>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="ddlCompanyType" runat="server" Height="30px" Width="250px" Theme="Office2003Blue" DropDownStyle="DropDown" IncrementalFilteringMode="Contains"
                                                            DataSourceID="SqlDataSource1" TextField="CompanyType" ValueField="CompanyType" />
                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from [dbo].[tbl_SupplierCompanyType]"></asp:SqlDataSource>


                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">Company Name :
                                                        <span style="color: red;">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompanyName" runat="server" Height="30px" Width="250px"></asp:TextBox>

                                                    </td>
                                                     <td style="font-weight: bold;">State : 
                                                         <span style="color: red;">*</span>
                                                     </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlState"  AppendDataBoundItems="true" runat="server" Height="30px" Width="250px">
                                                            <asp:ListItem>--Select--</asp:ListItem>                               
                                                        </asp:DropDownList></td>
                                                    <%--<td><strong>Fax Number :</strong></td>
                                                    <td>
                                                        <asp:TextBox ID="txtFaxNumber" runat="server" Width="195px"></asp:TextBox>
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td><strong>Credit Limit (Amount) :</strong></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCreditLimitAmount" runat="server" Height="30px" Width="250px"></asp:TextBox>
                                                    </td>
                                                    <td><strong>Credit Limit (Days) :</strong></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCreditLimitDays" runat="server" Height="30px" Width="250px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;"><strong>Contact No. :
                                                        <span style="color: red;">*</span>
                                                                                   </strong></td>
                                                    <td>
                                                        <asp:TextBox ID="txtContactNo" runat="server" Height="30px" Width="250px"></asp:TextBox>
                                                        <asp:Button ID="btnContactAdd" runat="server"  ForeColor="White" OnClientClick="return validateContactNo();" OnClick="btnContactAdd_Click" Height="36px" Text="ADD" CssClass="w3-btn w3-blue"  />
                                                    </td>

                                                    <td style="font-weight: bold;">WebSite :</td>
                                                    <td>
                                                        <asp:TextBox ID="txtWebsite" runat="server" Height="30px" Width="250px"></asp:TextBox>
                                                        <asp:Button ID="btnWebSiteAdd" runat="server" OnClick="btnWebSiteAdd_Click" OnClientClick="return validateWebsite();" ForeColor="White" Height="36px" Text="ADD" CssClass="w3-btn w3-blue"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gvmultiplenumber" runat="server" AllowPaging="True" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="IDNumber" EmptyDataText="No records has been added." GridLines="Vertical" OnPageIndexChanging="gvmultiplenumber_PageIndexChanging" OnRowCancelingEdit="gvmultiplenumber_RowCancelingEdit" OnRowDataBound="gvmultiplenumber_RowDataBound" OnRowDeleting="gvmultiplenumber_RowDeleting" OnRowEditing="gvmultiplenumber_RowEditing" OnRowUpdating="gvmultiplenumber_RowUpdating" PageSize="5">
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                            <Columns>
                                                                <asp:BoundField DataField="IDNumber" HeaderText="Address No." Visible="False">
                                                                    <ItemStyle Width="150px" />
                                                                </asp:BoundField>

                                                                <asp:TemplateField Visible="false" HeaderText="Category">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList ID="drpeditcat" runat="server" Width="143px">
                                                                        </asp:DropDownList>

                                                                        <asp:Label ID="lblEditcat" runat="server" Text='<%# Bind("Cat") %>' Visible="false"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcat" runat="server" Visible="false" Text='<%# Bind("Cat") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="txtNumber" HeaderText="Number">
                                                                    <ItemStyle Width="150px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#373745" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" Height="5px" />
                                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td colspan="2">
<asp:GridView ID="gv_website" runat="server" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" AllowPaging="True" OnRowCancelingEdit="gv_website_RowCancelingEdit" OnRowDeleting="gv_website_RowDeleting" PageSize="5">
                                                            <Columns>
                                                                <asp:BoundField DataField="website" HeaderText="website" />
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#373745" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" Height="5px" />
                                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold;">Address Category :</td>
                                                    <td>
                                                        <asp:TextBox ID="txtAddressCategory" runat="server" Height="30px" Width="250px"></asp:TextBox>
                                                    </td>
                                                     <td style="font-weight: bold;">Address : <span style="color: red;">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtAddress" runat="server" Style="resize: none;" TextMode="MultiLine" Height="30px" Width="250px"></asp:TextBox>
                                                        <asp:Button ID="btnMultipleAddress" runat="server" OnClick="btnMultipleAddress_Click" ForeColor="White" Height="36px" Text="ADD" CssClass="w3-btn w3-blue" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gvmultipleaddress" runat="server" CssClass="gridView" AllowPaging="True" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="ID" EmptyDataText="No records has been added." GridLines="Vertical" OnPageIndexChanging="gvmultipleaddress_PageIndexChanging" OnRowCancelingEdit="gvmultipleaddress_RowCancelingEdit" OnRowDeleting="gvmultipleaddress_RowDeleting" PageSize="5">
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ID" HeaderText="Address No." Visible="False">
                                                                    <ItemStyle Width="150px" />
                                                                </asp:BoundField>

                                                                <asp:TemplateField HeaderText="Category">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList ID="drpeditcat" runat="server" Width="143px">
                                                                        </asp:DropDownList>

                                                                        <asp:Label ID="lblEditcat" runat="server" Text='<%# Bind("Cat") %>' Visible="false"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Cat") %>'></asp:Label>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="Address1" ItemStyle-Width="30px" HeaderText="Address">
                                                                    <ItemStyle Width="50px" />
                                                                </asp:BoundField>
                                                            </Columns>

                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#373745" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" Height="5px" />
                                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                                        </asp:GridView>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                   
                                                    <td style="font-weight: bold;"><strong>Email Id : <span style="color: red;">*</span></strong></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailId" runat="server" Height="30px" Width="250px"></asp:TextBox>
                                                        <asp:Button ID="btnAddEmail" runat="server" ForeColor="White" OnClientClick="return validateEmail();" Height="36px" OnClick="btnAddEmail_Click" Text="ADD" CssClass="w3-btn w3-blue" />
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                <td colspan="2">
                                                        <asp:GridView ID="gv_Email" runat="server" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" OnRowCancelingEdit="gv_Email_RowCancelingEdit" OnRowDeleting="gv_Email_RowDeleting">
                                                            <Columns>
                                                                <asp:BoundField DataField="Email" HeaderText="Email" />

                                                                <asp:TemplateField HeaderText="Active" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:CheckBox ID="chkeditCemail" runat="server" />

                                                                        <asp:Label ID="lblEditactive" runat="server" Text='<%# Bind("active") %>' Visible="false"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("active") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#373745" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" Height="5px" />
                                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                               
                                                 <tr style="visibility:hidden">
                                                    <td style="font-weight:bold;">
                                                        TDS Section :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlTDS" AppendDataBoundItems="true" Height="30px" Width="250px">
                                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:Button ID="btnAddTDS" runat="server" BackColor="#003366" OnClick="btnAddTDS_Click" ForeColor="White" Height="22px" Text="ADD" />
                                                    </td>
                                                </tr>
                                               <tr>
                                                   <td colspan="4" align="left">
                                                       <asp:GridView ID="gvTDS" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10" AllowPaging="True" OnPageIndexChanging="gvTDS_PageIndexChanging"   
                                                        PageSize="100" OnRowCommand="gvTDS_RowCommand" OnRowDeleting="gvTDS_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Section" HeaderText="Section" />
                                                            <asp:BoundField DataField="Percentage" HeaderText="Percentage" />
                                                        </Columns>                                                       
                                                        <RowStyle HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                    </asp:GridView>
                                                   </td>
                                               </tr>
                                               
                                            </table>
                                        </td>
                                    </tr>


                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left">
                            <div id="CompanyBank" class="back">
                                <table class="dxflInternalEditorTable_Metropolis">
                                    <tr>
                                       <%-- <td class="auto-style254">
                                            <input type="button" id="CompanyBankShowt" value="Show" />
                                            <input type="button" id="CompanyBankHidet" value="Hide" />
                                        </td>--%>
                                        <td align="center" class="auto-style225"><strong>Company Bank Details</strong></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div id="CompanyBankShow" runat="server" class="back">
                                <table cellspacing="15px" align="center">

                                    <tr>
                                        <td style="font-weight: bold;">Bank Account Holder Name :</td>
                                        <td>
                                            <asp:TextBox ID="txtBankAccountHolderName" runat="server" ForeColor="#3399FF"  Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                        <td style="font-weight: bold;">Bank Branch Name :</td>
                                        <td>
                                            <asp:TextBox ID="txtBankBranchName" runat="server"  Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold;">Bank Account Number :</td>
                                        <td>
                                            <asp:TextBox ID="txtBankAccountNumber" runat="server"  Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                        <td style="font-weight: bold;">Bank Address : </td>
                                        <td>
                                            <asp:TextBox ID="txtBankAddress" runat="server"  Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold;">Bank Name :</td>
                                        <td>
                                            <asp:TextBox ID="txtBankName" runat="server"  Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                        <td style="font-weight: bold;">IFSC Code :</td>
                                        <td>
                                            <asp:TextBox ID="txtIFSCCode" runat="server"  Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>

                                        <td colspan="4" align="center">

                                            <asp:Button runat="server" ID="btnBankAdd" Text="ADD" OnClick="btnBankAdd_Click" Height="36px" Width="100px" CssClass="w3-btn w3-blue" />
                                            
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:GridView ID="grdbankdetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="CID" EmptyDataText="No records has been added." GridLines="Vertical" OnPageIndexChanging="grdbankdetails_PageIndexChanging" OnRowCancelingEdit="grdbankdetails_RowCancelingEdit" OnRowDeleting="grdbankdetails_RowDeleting" PageSize="5" Width="100%">
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                <Columns>
                                                    <asp:BoundField DataField="CID" HeaderText="Address No." Visible="False">
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cbahn" HeaderText="Bank A/c Holder Name">
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cban" HeaderText="Bank Account Number">
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cbanname" HeaderText="Bank Name">
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cbanbranch" HeaderText="Bank Branch Name">
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CbanAddress" HeaderText="Bank Address">
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cbanifsccode" HeaderText="IFSC Code">
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle BackColor="#373745" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" Height="5px" />
                                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#000065" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                  
                                </table>
                            </div>
                        </td>
                    </tr>
                   



                    <tr>
                        <td align="left" colspan="3">
                            <div id="ConcernedPerson" class="back">
                                <table class="dxflInternalEditorTable_Metropolis">
                                    <tr>
                                        <td class="auto-style254" >
                                           
                                        </td>
                                        <td align="center" class="auto-style225"><strong>Concerned Person Details</strong></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div id="ConcernedPersonShow" runat="server" class="back">
                                <table cellspacing="15px" align="center">
                                    <tr align="center">
                                        <td>
                                           <strong> <span style="color: red;  ">* One Concerned Person is Mandatory</span></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td><strong>Concerned Person Name <span style="color: red;">*</span> </strong></td>
                                        <td>
                                            <asp:TextBox ID="txt_concernedpersonname" runat="server"  Height="30px" Width="250px"></asp:TextBox>

                                        </td>
                                        <td><strong>Designation <span style="color: red;">*</span> </strong></td>
                                        <td>
                                            
                                            <dx:ASPxComboBox ID="ddl_cpnDesigation" runat="server" Height="30px" Width="250px" Theme="Office2003Blue" DropDownStyle="DropDown" IncrementalFilteringMode="Contains"
                                                            DataSourceID="SqlDataSource2" TextField="Designation" ValueField="Designation" />
                                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from [dbo].[tbl_DesignationMaster]"></asp:SqlDataSource>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Mobile 1 <span style="color: red;">*</span></strong></td>
                                        <td>
                                            <asp:TextBox ID="txtMobile1" runat="server" placeholder="Number" AutoCompleteType="Search" MaxLength="10"  Height="30px" Width="250px"></asp:TextBox>


                                        </td>
                                        <td><strong>Mobile 2</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtMobile2" runat="server" MaxLength="10"  Height="30px" Width="250px"></asp:TextBox>

                                        </td>
                                    </tr>


                                    <tr>
                                        <td><strong>Mobile 3</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtMobile3" runat="server" MaxLength="10"  Height="30px" Width="250px"></asp:TextBox>

                                        </td>
                                     
                                          <td style="font-weight: bold;">Email ID <span style="color: red;">*</span></td>
                                        <td>
                                            <asp:TextBox ID="txtEmailID1" runat="server"  Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                     
                                    <tr>
                                        <td><strong>Address</strong> <span style="color: red;"></span> </td>
                                        <td>
                                            <asp:TextBox ID="txt_ConcernedPersonAddress" runat="server" Style="resize: none;" TextMode="MultiLine"  Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <strong>Alternate Email ID</strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtalternateEmail" runat="server"  Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td colspan="4" align="center">
                                            <asp:Button runat="server" ID="btn_AddConPers" Text="Add Concern Person" OnClick="btn_AddConPers_Click" Height="36px" Width="170px" CssClass="w3-btn w3-blue" />
                                            
                                        </td>
                                    </tr>


                                    <tr>
                                        <td class="auto-style148" colspan="4">
                                            <asp:GridView ID="gv_ConcPer" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_ConcPer_RowCommand" OnRowDeleting="gv_ConcPer_RowDeleting" OnRowEditing="gv_ConcPer_RowEditing" OnPageIndexChanging="gv_ConcPer_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="ConcernPersonName" HeaderText="ConcernPersonName" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                    <asp:BoundField DataField="BoardDeskNo" HeaderText="BoardDeskNo" />
                                                    <asp:BoundField DataField="DirectDeskNo" HeaderText="DirectDeskNo" />
                                                    <asp:BoundField DataField="MobileNo1" HeaderText="MobileNo1" />
                                                    <asp:BoundField DataField="MobileNo2" HeaderText="MobileNo2" />
                                                    <asp:BoundField DataField="MobileNo3" HeaderText="MobileNo3" />
                                                    <asp:BoundField DataField="E_mail" HeaderText="EmailID" />
                                                    <asp:BoundField DataField="E_mailid2" HeaderText="Alternate EmailID" />
                                                    <asp:BoundField DataField="ResidentalAdress" HeaderText="Address" />

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>


                                                            <asp:Button ID="btn_SupplierEdit2" runat="server" CommandName="Edit" Text="Edit" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btn_SupplierEdit3" runat="server" CommandName="Delete" Text="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle BackColor="#373745" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" Height="5px" />
                                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#000065" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                    <%--Dev--%>
                     <tr>
                        <td align="left" colspan="3">
                            <div  class="back">
                                <table class="dxflInternalEditorTable_Metropolis">
                                    <tr>
                                        
                                        <td align="center" class="auto-style225"><strong>Credentials Upload</strong></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                      <tr>
                        <td colspan="3">
                            <div id="Div1" runat="server" class="back">
                                <table style="font-weight:bold;" cellspacing="15px" align="center">
                                    <tr>
                                        <td>
                                            Title :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtTitle" Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image :
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuDoc"  accept=".png,.jpg,.jpeg,.pdf" runat="server" />
                                            <asp:Button runat="server" ID="btn_Corres" Text="Add"  OnClick="btn_Corres_Click" CssClass="w3-btn w3-blue"  Height="36px" Width="75px" />
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                        <asp:GridView runat="server" ID="gv_Correspondance" OnRowDeleting="gv_Correspondance_RowDeleting" OnRowCommand="gv_Correspondance_RowCommand" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="25px" Height="25px"
                                                                CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Image" HeaderText="Image" />
                                                    <asp:BoundField DataField="Title" HeaderText="Title" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                </div>

                        </td>
                      </tr>
                    <tr>
                        <td colspan="3">
                            <div id="CompanyLegal" class="back">

                                <table class="dxflInternalEditorTable_Metropolis">
                                    <tr>
                                      
                                        <td align="center" class="auto-style225"><strong>Company Legal Compliances</strong></td>
                                    </tr>
                                </table>

                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div id="CompanyLegalShow" runat="server" class="back">
                                <table cellspacing="15px" align="center">

                                    <tr>
                                        <td style="font-weight: bold;">PAN No. : <span style="color: red;">*</span></td>
                                        <td>
                                            <asp:TextBox ID="txtPan" runat="server" Height="30px" Width="250px"></asp:TextBox>
                                        </td>
                                        <td>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan"  
                    Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card No" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}"></asp:RegularExpressionValidator>  
                                        </td>
                                        <td style="font-weight: bold;">GSTIN :</td>
                                        <td>
                                            <asp:TextBox ID="txtGST" runat="server" Height="30px" Width="250px"></asp:TextBox>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtGST"  
                    Display="Dynamic" ForeColor="Red" ErrorMessage="InValid GSTIN " ValidationExpression="^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold;">
                                            PAN Card : <span style="color: red;">*</span>
                                        </td>
                                        <td>
                                            <asp:FileUpload runat="server" accept=".png,.jpg,.jpeg,.pdf" ID="FileUpload1"  />
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                          <td style="font-weight: bold;">
                                            GSTIN Document :
                                        </td>
                                        <td>
                                            <asp:FileUpload runat="server" accept=".png,.jpg,.jpeg,.pdf"   ID="FileUpload2" />
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold;">
                                            Bank Document :
                                        </td>
                                        <td>
                                            <asp:FileUpload runat="server" accept=".png,.jpg,.jpeg,.pdf"  ID="FileUpload3" />
                                        </td>
                                    </tr>
                                   
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div align="center">

                                <table >
                                    <tr>
                                        <td> 
                                            <asp:Button runat="server" ID="btn_CompanyDetailsSubmit" Text="Save" OnClick="btn_CompanyDetailsSubmit_Click" Height="36px" Width="100px" CssClass="w3-btn w3-green" />
                                            
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btn_cancelCompanyDetails" Text="Clear" OnClick="btn_cancelCompanyDetails_Click" Height="36px" Width="100px"  CssClass="w3-btn w3-orange" />
                                        
                                        </td>


                                    </tr>
                                </table>

                            </div>
                        </td>
                    </tr>
                    <%--Dev2--%>
                     <tr>
                        <td colspan="3">
                            <div class="back" align="center" style="overflow:auto;width:1000px;">
                                <strong><span class="auto-style255">Vendor Details<br />
                                </span></strong>
                                <br />
                                <dx:ASPxGridView ID="Gv_SupplierSummary" Theme="Office2003Blue" EnableRowsCache="true" SettingsPager-PageSize="10" EnablePagingGestures="True" OnHtmlRowPrepared="Gv_SupplierSummary_HtmlRowPrepared" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" OnRowCommand="ASPxGridView1_RowCommand" KeyFieldName="SupplierId" >
                                    <Columns>

                                        
                                        <dx:GridViewDataTextColumn FieldName="VendorFor" Caption="Supplier For" ReadOnly="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="MsmeNonMsme" Caption="Msme/Non-Msme" ReadOnly="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ScStOrNot" Caption="Sc_St/Non-Sc_St" ReadOnly="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="WomenOwner" Caption="Women Owner" ReadOnly="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="GroupName" ReadOnly="True" VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CompanyType" Caption="Company Type" ReadOnly="True" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CompanyName" Caption="Supplier" ReadOnly="True"        VisibleIndex="7">
                                        </dx:GridViewDataTextColumn>
                                      <dx:GridViewDataTextColumn FieldName="StateName" Caption="State" ReadOnly="True"        VisibleIndex="8">
                                        </dx:GridViewDataTextColumn>
                                       
                                        <dx:GridViewDataTextColumn FieldName="DeActive" Caption="Status" ReadOnly="True" VisibleIndex="9">
                                        </dx:GridViewDataTextColumn>
                                        
                                        <dx:GridViewDataTextColumn Caption="" VisibleIndex="10" Width="0px">
                                            <DataItemTemplate>
                                                
                                          <%--    <asp:Button runat="server" Text="Credentials" CommandName="ScanCopy" />--%>
                                                <asp:Button runat="server" ID="btnActive" Text="Active" CommandName="Active" />
                                                <asp:Button runat="server" ID="btnDeActive" Text="Deactive" CommandName="Deactive" />

                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Concerned Person Credentials"  VisibleIndex="11" Width="0px">
                                                                                    <DataItemTemplate>
                                                                                        <asp:ImageButton ID="ibmCredentials" runat="server"
                                                                                        CommandName="Credentials"
                                                                                        ImageUrl="../Images/view.png" Width="20px" Height="20px" CommandArgument='<%#Eval("SupplierId")%>' ClientIDMode="Static" />
                                                                                    </DataItemTemplate>

                                                                                </dx:GridViewDataTextColumn>


                                                                                 <dx:GridViewDataTextColumn Caption="Bank Doc"  VisibleIndex="12" Width="0px">
                                                                                    <DataItemTemplate>
                                                                                        <asp:ImageButton ID="ibmBank" runat="server"
                                                                                        CommandName="Bank"
                                                                                        ImageUrl="../Images/downloadicon.png" Width="20px" Height="20px" CommandArgument='<%#Eval("SupplierId")%>' ClientIDMode="Static" />
                                                                                    </DataItemTemplate>

                                                                                </dx:GridViewDataTextColumn>
                                                                                 <dx:GridViewDataTextColumn Caption="Pan Card"  VisibleIndex="9" Width="0px">
                                                                                    <DataItemTemplate>
                                                                                        <asp:ImageButton ID="ibmPan" runat="server"
                                                                                        CommandName="Pan"
                                                                                        ImageUrl="../Images/downloadicon.png" Width="20px" Height="20px" CommandArgument='<%#Eval("SupplierId")%>' ClientIDMode="Static" />
                                                                                    </DataItemTemplate>

                                                                                </dx:GridViewDataTextColumn>
                                                                                 <dx:GridViewDataTextColumn Caption="GST Doc"  VisibleIndex="9" Width="0px">
                                                                                    <DataItemTemplate>
                                                                                        <asp:ImageButton ID="ibmGST" runat="server"
                                                                                        CommandName="GST"
                                                                                        ImageUrl="../Images/downloadicon.png" Width="20px" Height="20px" CommandArgument='<%#Eval("SupplierId")%>' ClientIDMode="Static" />
                                                                                    </DataItemTemplate>

                                                                                </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="" VisibleIndex="13" Width="0px">
                                            <DataItemTemplate>
                                                <asp:ImageButton ID="imbEdit" runat="server"
                                                    CommandName="Edit"
                                                    ImageUrl="../Image/edit.png" Width="30px" Height="30px" CommandArgument='<%#Eval("SupplierId")%>' ClientIDMode="Static" />
                                                                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowSelectSingleRowOnly="True" />
                                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRow="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                                        <PageSizeItemSettings Items="50, 100" />

                                    </SettingsPager>
                                </dx:ASPxGridView>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select st.StateName,c.CompanyType,c.WomenOwner,c.MsmeNonMsme,c.ScStOrNot,c.CompanyName,c.SupplierId,s.GroupName,c.VendorFor,
                                'ApproveStatus'=case when  c.VerifiedStatus =0 then 'Not Checked' else (case when c.Reject=1 then 'Rejected' else 'Approved' end) end,
								'DeActive'=case when (DeActive=0 or DeActive is null) then 'Active' else 'Deactive ' end from dbo.tbl_SupplierMasterEntry c
                                join dbo.tbl_SupplierGroupMaster s on s.GroupId=c.GroupId join dbo.tbl_StateMaster st on st.StateCode=c.StateCode order by c.Rowid desc"></asp:SqlDataSource>
                                <br />
                                
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnXlsExport" runat="server" OnClick="btnXlsExport_Click" Text="Export to XLS"  UseSubmitBehavior="False">
                                            </dx:ASPxButton>                                                
                                            <dx:ASPxButton ID="btnXlsxExport" runat="server" OnClick="btnXlsxExport_Click" Text="Export to XLSX"  UseSubmitBehavior="False">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="Gv_SupplierSummary"> </dx:ASPxGridViewExporter>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                </table>

</div>
            </div>
               </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
  <div class="w3-modal"">
        <div class="w3-modal-content w3-animate-top w3-card-4"  style="width:369px">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('Group4').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Documents</h3>
                    </header>
                </div>
            <br/>
            <br/>
                <div  style=" overflow:auto;">  
                <table>

                <asp:GridView ID="gvDocuments" CssClass="ChildGrid" runat="server" OnRowCommand="gvDocuments_RowCommand" AutoGenerateColumns="false">
                    <Columns>

                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href='<%# Eval("Images") %>' target="_blank">
                                    <img src='<%# Eval("Images") %>' width="120px" height="56px" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                    </Columns>
                </asp:GridView>
                
                </table>
                      </div>
            </div>
         </div>
  <div id='Items_Popup' class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-4" style="width:756px">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('Items_Popup').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Concern Person Details</h3>
                    </header>
                </div>
            <br/>
            <br/>
                <div align="center">

                    <dx:ASPxGridView ID="gvDocuments1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" KeyFieldName="SupplierId">
                    <Columns>

                        <dx:GridViewDataTextColumn FieldName="ConcernPersonName" Caption="Concern Person Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Designation" Caption="Designation" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MobileNo1" Caption="Mobile 1" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MobileNo2" Caption="Mobile 2" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MobileNo3" Caption="Mobile 3" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="E_mail" Caption="Email ID" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="E_mailid2" Caption="Alternate Email ID" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>

                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" />
                    <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="true" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />

                    </SettingsPager>
                </dx:ASPxGridView>
                <%--<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from  [dbo].[tbl_SupplierConcernPersonDetails] where SupplierId=@SupplierId">
                     <SelectParameters>
                                    <asp:SessionParameter SessionField="SupplierId" Name="SupplierId" />
                                </SelectParameters>
                    </asp:SqlDataSource>--%>
              




                      </div>
            </div>
         </div>
</asp:Content>

