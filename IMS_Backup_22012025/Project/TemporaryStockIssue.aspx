<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="TemporaryStockIssue.aspx.cs" Inherits="Project_TemporaryStockIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Temporary Stock Issue</h3>
            </div>
            <br />
             <div align="center">
                <table>
                    <tr>
                       
                        <td>
                            <asp:TextBox runat="server" ID="txtReqPopAppId" Height="30px" Visible="false" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqPopId"  Height="30px" Visible="false" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                </table>
            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Issue From : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqId" Visible="false" Enabled="false" Height="30px" Style="resize: none" Width="125px"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtBranchName" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserGroup" Enabled="false" Height="30px" Style="resize: none" Width="130px"></asp:TextBox>
                            <asp:Label runat="server" ID="lblUserGroupId"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqUser" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Issue Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                       
                        <td style="font-weight: bold;">Transfer Id : <span style="color: red;">*</span>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ddlStockInId" runat="server" AutoPostBack="True"
                            DataSourceID="SqlDataSource1"
                            OnSelectedIndexChanged="ddlStockInId_TextChanged" TextField="ReqtransAppId"
                            Theme="Office2003Blue" ValueField="ReqtransAppId" Height="30px" Width="250px">
                            <%--   <Columns>
                                    <dx:ListBoxColumn Caption="ReqtransAppId" FieldName="ReqtransAppId" />
                                    <dx:ListBoxColumn Caption="PO No" FieldName="" />
                                </Columns>--%>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                            ConnectionString="<%$ ConnectionStrings:Profit %>"
                            SelectCommand="select ReqtransAppId From tbl_RequisitionTransferApproval where Status=@UserId">
                               <SelectParameters>
                                    <asp:SessionParameter Name="UserId" SessionField="UserId" />
                                </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Issue to :
                        </td>
                        <td>

                            <dx:ASPxComboBox ID="ddlRequisitionto" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="BranchId" TextField="SectionId" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct a.SectionId, b.BranchId from tbl_HigherKeyMapping a join tbl_Branch b on a.SectionId=b.BranchName where POPId=@BranchName">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchName" Name="BranchName" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Label runat="server" ID="ddlRequisitiontoId1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlRequToGrp" Enabled="false" Height="30px" Style="resize: none" Width="130px"></asp:TextBox>

                            <%-- <asp:DropDownList runat="server" ID="ddlRequToGrp" Theme="Office2003Blue" Style="font-size: small;" Height="30px" Width="125px">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="SecIncharge" Value="UG15"></asp:ListItem>
                                <asp:ListItem Text="TerritoryMgr" Value="UG14"></asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:Label runat="server" ID="ddlRequToGrpId"></asp:Label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlRequToUser" runat="server"
                                Height="30px" Width="250px" ValueField="UserName" TextField="UserName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>

                        </td>

                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Height="60px" Style="resize: none" Width="250px"></asp:TextBox>
                        </td>
                        
                         <td style="font-weight: bold;">Issue Purpose :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlRequisitionPur" runat="server"
                                DataSourceID="SqlDataSource6" Height="30px" Width="250px"
                                ValueField="ID" TextField="PurposeName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource6" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct ID,PurposeName from tbl_PurposeReqMaster"></asp:SqlDataSource>
                            <asp:Label runat="server" ID="ddlRequisitionPurId"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
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

                    <asp:BoundField DataField="ItemId" HeaderText="Item Id" />
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                    <asp:BoundField DataField="Make" HeaderText="Make" />
                    <asp:BoundField DataField="Model" HeaderText="Model" />
                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                    <asp:BoundField DataField="ReqToQty" HeaderText="Stock In Hand (To)" />
                    <asp:BoundField DataField="POPQty" HeaderText="Stock In Hand (From)" />
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
        <div align="center">
            <table cellspacing="15px" align="center">
                <tr>
                    
                    <td style="font-weight: bold;">Item :
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ddlItem" runat="server"
                            DataSourceID="SqlDataSource4" Height="30px" Width="250px" 
                            ValueField="ItemId" TextField="ItemName" Theme="Office2003Blue" 
                            IncrementalFilteringMode="Contains" CallbackPageSize="10">
                            <Columns>
                                <dx:ListBoxColumn Caption="ItemName" FieldName="ItemName" />
                                <dx:ListBoxColumn Caption="Make" FieldName="Make" />
                                <dx:ListBoxColumn Caption="Model" FieldName="Model" />
                            </Columns>

                        </dx:ASPxComboBox>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                            ConnectionString="<%$ ConnectionStrings:Profit %>"
                            SelectCommand=" select distinct sd.ItemId, i.ItemName, i.Make, i.Model  from tbl_RequisitionTransferDetailsApproval sd join tbl_ItemMaster i on sd.ItemId=i.ItemId where sd.ReqtransAppId=@ReqtransAppId">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ReqtransAppId" Name="ReqtransAppId" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </td>
                     <td style="font-weight: bold;">Enter Barcode :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtBarcode" Height="30px" Width="250px" AutoPostBack="true" OnTextChanged="txtBarcode_TextChanged"></asp:TextBox>
                    </td>
                     
                </tr>
             
            </table>
            <div align="center">
                <br />
                <asp:Label runat="server" ID="lblError11" Style="font-size: large; color: red; font-weight: bold;"></asp:Label><br />
                <br />
                <asp:GridView ID="gv_Barc" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_Barc_RowCommand" OnRowDeleting="gv_Barc_RowDeleting" OnRowEditing="gv_Barc_RowEditing" OnPageIndexChanging="gv_Barc_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                            <ItemTemplate>
                                <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                    CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                            </ItemTemplate>

                            <ItemStyle Width="25px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StockInId" HeaderText="Stock In Id" />
                        <asp:BoundField DataField="ItemId" HeaderText="ItemId" Visible="false" />
                        <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" Visible="false" />
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                        <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                        <asp:BoundField DataField="Row" HeaderText="Row" />
                        <asp:BoundField DataField="Rack" HeaderText="Rack" />
                        <asp:BoundField DataField="Shelf" HeaderText="Shelf" />
                        <asp:BoundField DataField="Warranty" HeaderText="Warranty" Visible="false"/>
                        <asp:BoundField DataField="WarrantyTo" HeaderText="W_Date" Visible="false"/>
                        <asp:BoundField DataField="CoderLifeTo" HeaderText="CL_Date" Visible="false"/>
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

                <br />
                <br />
                <table>
                    <tr>

                        <td>
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>



            </div>
        </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

