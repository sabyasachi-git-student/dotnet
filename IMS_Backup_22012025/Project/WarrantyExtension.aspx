<%@ Page Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="WarrantyExtension.aspx.cs" Inherits="Project_WarrantyExtension" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Warranty Extension</h3>
            </div>
            <br />

            <div align="center">


                <%--<table align="center">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnChooseRawMaterial" Text="Choose Item"  CssClass="w3-btn w3-blue" OnClick="btnChooseFaultyItem_Click" />
                        </td>
                    </tr>
                </table>--%>
                <br />
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Extension Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpExtensionDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="font-weight: bold;">Select PO :<span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlPOID" runat="server"
                                Height="30px" Width="250px"
                                ValueField="POID" TextField="POID" Theme="Office2003Blue"
                                DataSourceID="SqlDataSource1"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" AutoPostBack="true" DropDownStyle="DropDown" OnTextChanged="ddlPOID_TextChanged">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct POID from tbl_StockIn where BranchId=@BranchId">

                                 <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td style="font-weight: bold;">File Upload : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:FileUpload runat="server" accept=".png,.jpg,.jpeg,.pdf" ID="FileUpload1" />
                        </td>
                    </tr>
                    
                </table>
                <br />
                <div align="center" style="overflow: auto; width: 1100px;">
                    <asp:GridView ID="gvWarrantyExtensionDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" OnPageIndexChanging="gvWarrantyExtensionDetails_PageIndexChanging"
                        PageSize="100" OnRowCommand="gvWarrantyExtensionDetails_RowCommand" ShowFooter="true" OnRowDeleting="gvWarrantyExtensionDetails_RowDeleting" OnRowDataBound="gvWarrantyExtensionDetails_RowDataBound">

                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>

                                <ItemStyle Width="25px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="POID" HeaderText="PO Id" />
                            <asp:BoundField DataField="StoCkInId" HeaderText="StoCkIn Id" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Supplier" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" />
                            <asp:BoundField DataField="AvailableQty" HeaderText="Quantity" />
                            <asp:BoundField DataField="WarrantyTo" HeaderText="Initial warranty upto" />
                            <asp:BoundField DataField="CoderLifeTo" HeaderText="CoderLife Upto" />
                            <asp:TemplateField HeaderText="Warranty Extension(in months)">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtWarEx1" Width="70px"></asp:TextBox><%--Text='<%#Eval("Qty") %>'--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Extended warranty" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtNewWarTo1" Width="80px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle ForeColor="Black" BackColor="#7BA4E0" />
                        <RowStyle HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#7BA4E0" Font-Bold="True" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />

                    </asp:GridView>

                    <br />

                </div>
                <br />
                <table>
                    <tr>

                        <td>
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="w3-btn w3-orange" />
                            <asp:Button runat="server" ID="btnCalculate" Text="Calculate" OnClick="btnCalculate_Click" CssClass="w3-btn w3-blue" visible="false"/>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

