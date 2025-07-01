<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="blockInventory.aspx.cs" Inherits="Project_blockInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Block Inventory</h3>
            </div>
            <br />

            <div align="center">
                <table>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnChooseRequisition" OnClick="btnChooseRequisition_Click" Text="Choose Item" CssClass="w3-btn w3-blue" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqPopAppId" Visible="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqPopId" Visible="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                </table>
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Block From : <span style="color: red;">*</span>
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
                        <td style="font-weight: bold;">Block Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="font-weight: bold;">Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>
                        </td>
                    </tr>


                    <tr runat="server" id="TrIssueOption" visible="false">
                        <td style="font-weight: bold;">Issue Option :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlProOp" Theme="Office2003Blue" Style="font-size: small;" Height="30px" Width="230px" AutoPostBack="true" OnTextChanged="ddlProOp_TextChanged">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="General Stock" Value="GeneralStock"></asp:ListItem>
                                <asp:ListItem Text="Project Wise" Value="ProjectWise"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                       <td>
                            <asp:Label runat="server" ID="lbl1" Text="Select Project" Style="font-weight: bold;" Visible="false" ></asp:Label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlProject" runat="server" Visible="false"
                                DataSourceID="SqlDataSource1" Height="30px" Width="250px"
                                ValueField="ProjectId" TextField="ProjectName" Theme="Office2003Blue" AutoPostBack="true" OnTextChanged="ASPxComboBox1_TextChanged"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="SELECT Distinct s.ProjectId,S.ProjectName FROM tbl_RackStockInDetails rsd 
join tbl_RackStockInBarCodeDetails a on rsd.ActualStockInId=a.ActualStockInId
join tbl_ItemMaster im on im.ItemId=rsd.ItemID   
join tbl_StockIn s on rsd.StockInId=s.StockInId where rsd.BranchId=@BranchId
and (a.Status1 is null or a.Status1 ='') and (a.Status2 is null or a.Status2 ='') 
and (a.Status3 is null or a.Status3 ='') and (a.Status4 is null or a.Status4 ='')">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Label runat="server" ID="Label1"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr runat="server" id="TrIssueOption1" visible="false">
                        <td>
                            <asp:Label runat="server" ID="lblstock" Text="Stock Available Qty" Style="font-weight: bold;" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAvilStock" Visible="false" Enabled="false" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblBlock" Text="Block Qty" Style="font-weight: bold;" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtBlockStock" Visible="false" Enabled="false" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <div align="center">
                    <asp:Button runat="server" ID="btnStockOut" Text="Verify Item" OnClick="btnStockOut_Click"  Height="36px" Visible="false" CssClass="w3-btn w3-red"/>
                     <asp:Button runat="server" ID="AddBarCode" Text="Barcode Assign" OnClick="AddBarCode_Click" Visible="false" Height="36px" Width="170px" CssClass="w3-btn w3-pink" />
                    <br />
                    <br />

                    <asp:GridView ID="gvItemDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" PageSize="100">

                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="false" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtItemQty" Width="70px" Text='<%#Eval("Qty") %>'></asp:TextBox>
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
                    <asp:GridView ID="gv_Barc" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_Barc_RowCommand" OnRowDeleting="gv_Barc_RowDeleting" OnRowEditing="gv_Barc_RowEditing" >
                        <Columns>
                            <asp:BoundField DataField="StockInId" HeaderText="Stock In Id" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" Visible="false" />
                            <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                            <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" Visible="false" />
                            <asp:BoundField DataField="ProcessId" HeaderText="ProcessId" Visible="false"/>
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />

                            <asp:BoundField DataField="Row" HeaderText="Row" />
                            <asp:BoundField DataField="Rack" HeaderText="Rack" />
                            <asp:BoundField DataField="Shelf" HeaderText="Shelf" />
                            <asp:BoundField DataField="Warranty" HeaderText="Warranty (Months)" Visible="false" />
                            <asp:BoundField DataField="WarrantyTo" HeaderText="W_Date" Visible="false" />
                            <asp:BoundField DataField="CoderLifeTo" HeaderText="CL_Date" Visible="false" />

                            <asp:TemplateField HeaderText="Barcode">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtBarcode" Enabled="false" Width="100px" Text='<%#Eval("Barcode") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Enter Barcode" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtBarcodeEnter" Width="150px" Text='<%#Eval("EnterBarcode") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

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
                <br />
                <br />
                <div align="center">
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" Height="36px" CssClass="w3-btn w3-green" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange" />

                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="id03" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-4" style="width: 1200px;">
            <div class="w3-container w3-blue">
                <header>
                        <span onclick="document.getElementById('id03').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Item Details</h3>
                    </header>
            </div>
            <br />


            <div align="center" style="overflow: auto; width: 1200px;">
                <br />
                <dx:ASPxGridView ID="gvItem" Width="100%" EnableRowsCache="true" Font-Bold="true" SettingsPager-PageSize="10" Theme="Office2003Blue" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="DS_Estimation" KeyFieldName="ItemId">

                    <Columns>
                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" ShowClearFilterButton="true" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox runat="server" ID="chbxSelect" AutoPostBack="true"></dx:ASPxCheckBox>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemId" ReadOnly="True" Caption="ItemId" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Category" ReadOnly="True" Caption="Category" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" ReadOnly="True" Caption="Item Name" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Make" ReadOnly="True" Caption="Make" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Model" Caption="Model" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Unit" Caption="Unit" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="AvailableQty" Caption="Available Qty" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>

                    </Columns>
                    <%-- <SettingsBehavior AllowSelectSingleRowOnly="True" />--%>
                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />

                    </SettingsPager>
                    <Templates>
                        <FooterRow>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddServiceItem" runat="server" OnClick="btnAddServiceItem_Click" CssClass="btn" HorizontalAlign="Left" Text="ADD">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>


                        </FooterRow>
                    </Templates>

                </dx:ASPxGridView>
                <asp:SqlDataSource ID="DS_Estimation" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select Distinct a.ItemId, b.Category, b.ItemName, b.Make, b.Model, b.Unit,
sum(qty) as AvailableQty
from tbl_RackStockInBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId 
where a.BranchId=@BranchId and
(a.Status1 is null or a.Status1 ='') and (a.Status2 is null or a.Status2 ='') and (a.Status3 is null or a.Status3 ='') and (a.Status4 is null or a.Status4 ='')
group by a.ItemId, b.Category, b.ItemName, b.Make, b.Model, b.Unit">
                    <SelectParameters>
                        <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"></asp:SqlDataSource>
            </div>
            <br />
        </div>
    </div>

    <div class="w3-container">
        <div id="id03A" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('id03A').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Barcode Details</h3>
                    </header>
                </div>
                <br />
                <br />

                <div align="center">
                    <br />
                    <dx:ASPxGridView ID="ASPxGridView1" Width="100%" EnableRowsCache="true" Font-Bold="true" SettingsPager-PageSize="10" Theme="Office2003Blue" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" KeyFieldName="SrBarVodeID">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" ShowClearFilterButton="true" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox runat="server" ID="chbxSelect" AutoPostBack="true"></dx:ASPxCheckBox>
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="StockInId" Caption="StockInId" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SrBarVodeID" Caption="SrBarVodeID" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Barcode" Caption="Barcode" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Warranty" Caption="Warranty" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="WarrantyDate" Caption="WarrantyDate" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CoderLifeDate" Caption="CoderLifeDate" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <%-- <SettingsBehavior AllowSelectSingleRowOnly="True" />--%>
                        <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />

                        </SettingsPager>
                        <Templates>
                            <FooterRow>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnaddProject" runat="server" CssClass="btn" OnClick="btnaddProject_Click" HorizontalAlign="Left" Text="ADD">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>


                            </FooterRow>
                        </Templates>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select a.StockInId, b.ItemName, a.SrBarVodeID, a.Barcode, a.Warranty, Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate , 
b.ItemName from tbl_RackStockInBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where  a.ItemId=@ItemIdStock and (a.Status1='' or a.Status1 is null)
and (a.Status2='' or a.Status2 is null) and (a.Status3='' or a.Status3 is null) and (a.Status4='' or a.Status4 is null) and a.BranchId=@BranchId and a.ProjectId=@ProjectId">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                            <asp:SessionParameter SessionField="ProjectId" Name="ProjectId" />
                            <asp:SessionParameter SessionField="ItemIdStock" Name="ItemIdStock" />
                        </SelectParameters>

                    </asp:SqlDataSource>
                    <br />
                </div>
            </div>
        </div>
    </div>

    <div class="w3-container">
        <div id="id03B" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('id03B').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Barcode Details</h3>
                    </header>
                </div>
                <br />
                <br />

                <div align="center">
                    <br />
                    <dx:ASPxGridView ID="ASPxGridView2" Width="100%" EnableRowsCache="true" Font-Bold="true" SettingsPager-PageSize="10" Theme="Office2003Blue" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" KeyFieldName="SrBarVodeID">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" ShowClearFilterButton="true" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox runat="server" ID="chbxSelect" AutoPostBack="true"></dx:ASPxCheckBox>
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="StockInId" Caption="StockInId" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SrBarVodeID" Caption="SrBarVodeID" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Barcode" Caption="Barcode" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Warranty" Caption="Warranty" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="WarrantyDate" Caption="WarrantyDate" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CoderLifeDate" Caption="CoderLifeDate" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <%-- <SettingsBehavior AllowSelectSingleRowOnly="True" />--%>
                        <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />

                        </SettingsPager>
                        <Templates>
                            <FooterRow>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnaddGenaral" runat="server" OnClick="btnaddGenaral_Click" CssClass="btn" HorizontalAlign="Left" Text="ADD">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>


                            </FooterRow>
                        </Templates>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select a.StockInId, b.ItemName, a.SrBarVodeID, a.Barcode, a.Warranty, Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate , 
b.ItemName from tbl_RackStockInBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where  a.ItemId=@ItemIdStock and (a.Status1='' or a.Status1 is null)
and (a.Status2='' or a.Status2 is null) and (a.Status3='' or a.Status3 is null) and (a.Status4='' or a.Status4 is null) and a.BranchId=@BranchId">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                            <asp:SessionParameter SessionField="ItemIdStock" Name="ItemIdStock" />
                        </SelectParameters>

                    </asp:SqlDataSource>
                    <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

