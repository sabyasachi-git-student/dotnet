<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="TemporaryStockReturn.aspx.cs" Inherits="Project_TemporaryStockReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Temporary Stock Return</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Issue Id : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlIssueId" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="TemIssueId" TextField="TemIssueId" Theme="Office2003Blue" AutoPostBack="true" OnTextChanged="ddlIssueId_TextChanged"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct a.TemIssueId, a.ReqAppId, a.ReqId, b.BranchId from tbl_TemporaryStockIssue a join tbl_Branch b on a.ReUserName=b.BranchName where Receive='Receive' and (ReturnStatus='' or ReturnStatus is null)  and b.BranchId=@BranchId">
                                 <SelectParameters>
                                    <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                                </SelectParameters>

                            </asp:SqlDataSource>
                            <asp:Label runat="server" ID="lblReqAppId" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblReqId" Visible="false"></asp:Label>
                        </td>
                        <td style="font-weight: bold;">Return Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>


                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Style="resize: none;" Height="60px" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txt1" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txt2" Visible="false"></asp:TextBox>
                        </td>
                    </tr>

                </table>
                <div align="center">


                    <asp:GridView ID="gvItemDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" PageSize="100">

                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="25px" Visible="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                            <asp:BoundField DataField="ReqToQty" HeaderText="ReqToQty" />
                            <asp:BoundField DataField="POPQty" HeaderText="POPQty" />
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtItemQty" Enabled="false" Width="70px" Text='<%#Eval("Qty") %>'></asp:TextBox>
                                    <asp:Label runat="server" ID="lblItemId" Visible="false" Text='<%#Eval("ItemId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRate" runat="server" Width="70px" Text='0' Font-Bold="False"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle ForeColor="Black" BackColor="#7BA4E0" />
                        <RowStyle HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#7BA4E0" Font-Bold="True" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />

                    </asp:GridView>

                </div>
                <br />
                <br />
                <div align="center">
                    <asp:GridView ID="gv_Barc" runat="server" AllowPaging="True" PageSize="500" AutoGenerateColumns="False" OnRowCommand="gv_Barc_RowCommand" OnRowDeleting="gv_Barc_RowDeleting" OnRowEditing="gv_Barc_RowEditing" OnPageIndexChanging="gv_Barc_PageIndexChanging" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="StockInId" HeaderText="StockInId" Visible="false" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" Visible="false" />
                            <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                            <asp:BoundField DataField="ProcessId" HeaderText="ProcessId" Visible="false" />
                            <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                            <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                            <asp:BoundField DataField="Row" HeaderText="Row" />
                            <asp:BoundField DataField="Rack" HeaderText="Rack" />
                            <asp:BoundField DataField="Shelf" HeaderText="Shelf" />
                            <asp:BoundField DataField="Warranty" HeaderText="Warranty (Months)" />
                            <asp:BoundField DataField="WarrantyTo" HeaderText="W_Date" />
                            <asp:BoundField DataField="CoderLifeTo" HeaderText="CL_Date" />

                            <asp:TemplateField HeaderText="Action" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="btn_SupplierEdit3" runat="server" CommandName="Delete" Text="Delete" CssClass="w3-btn w3-red" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Blue" />
                        <HeaderStyle BackColor="blue" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" Height="5px" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </div>
                <table>
                    <tr>

                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

