<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="RackShift.aspx.cs" Inherits="Project_RackShift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Rack Shifting </h3>
            </div>
            <br />

            <div align="center">

                <table cellspacing="15px" align="center">

                    <tr>
                        <td style="font-weight: bold;">Item :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlItem" runat="server"
                                DataSourceID="SqlDataSource4" Height="30px" Width="250px" AutoPostBack="True" TextFormatString="{0}"
                                ValueField="ItemId" TextField="ItemName" Theme="Office2003Blue" OnSelectedIndexChanged="ddlItem_TextChanged"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10">
                                <Columns>
                                    <dx:ListBoxColumn Caption="ItemName" FieldName="ItemName" />
                                    <dx:ListBoxColumn Caption="Make" FieldName="Make" />
                                    <dx:ListBoxColumn Caption="Model" FieldName="Model" />
                                     <dx:ListBoxColumn Caption="ItemId" FieldName="ItemId" />
                                </Columns>

                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct sd.ItemId, i.ItemName, i.Make, i.Model  from tbl_RackStockInDetails sd join tbl_ItemMaster i on sd.ItemId=i.ItemId where sd.BranchId=@BranchId">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                        <td style="font-weight: bold;">From Rack :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Height="30px" Width="250px" Theme="Office2003Blue" AutoPostBack="true" OnTextChanged="ASPxComboBox1_TextChanged"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" DataSourceID="SqlDataSource2" TextField="Shelf" ValueField="ProcessId"
                                TextFormatString=" {0}" CallbackPageSize="20" EnableCallbackMode="True">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="Row" Name="Lead Name" />   
                                    <dx:ListBoxColumn FieldName="Rack" Name="Mobile No" />
                                    <dx:ListBoxColumn FieldName="Shelf" Name="Mobile No" />
                                </Columns>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select ab.ProcessId, ab.Row, ab.Rack, ab.Shelf, ab.BranchID, ab.ItemId, Sum(ab.Qty) as a FROM tbl_RackStockInBarCodeDetails ab  where ab.ItemId=@ItemId group By ab.Row, ab.Rack, ab.Shelf, ab.BranchID, ab.ItemId, ab.ProcessId">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="ItemId" Name="ItemId" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                         
                        </td>
                        </tr>
                    <tr>
                        <td style="font-weight: bold;">Select Project :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Height="30px" Width="250px" Theme="Office2003Blue" AutoPostBack="true" OnTextChanged="ASPxComboBox2_TextChanged"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" DataSourceID="SqlDataSource5" TextField="ProjectName" ValueField="ProjectId"
                                TextFormatString=" {0}" CallbackPageSize="20" EnableCallbackMode="True">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct ab.ProjectId, ab.ProjectName FROM tbl_RackStockInBarCodeDetails ab  where ab.ItemId=@ItemId and ab.ProcessId=@ProcessId">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="ItemId" Name="ItemId" />
                                    <asp:SessionParameter SessionField="ProcessId" Name="ProcessId" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                         
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtRow" Height="30px" Width="250px" Visible="false" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRack" Height="30px" Width="250px" Visible="false" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtShelf" Height="30px" Width="250px" Visible="false" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trItemUnit" visible="false">
                        <td style="font-weight: bold;">rack space unit :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="lblItemSpaceUnit" Height="30px" Width="250px" Enabled="false"></asp:TextBox>
                        </td>

                        <td style="font-weight: bold;">StockIn Qty :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtStockQty" Height="30px" Width="250px" Enabled="false"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="lblTotalSpaceUnit" Height="30px" Width="250px" Visible="false"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Qty :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRackQty" Height="30px" Width="250px" AutoPostBack="true" OnTextChanged="txtRackQty_TextChanged"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">assign rack no :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlRackSpace" runat="server" Height="30px" Width="250px" Theme="Office2003Blue"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" OnTextChanged="ddlRackSpace_TextChanged"
                                AutoPostBack="true" DataSourceID="SqlDataSource1" TextField="Shelf" ValueField="ProcessId"
                                TextFormatString=" {0}" CallbackPageSize="20" EnableCallbackMode="True">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="Row" Name="Lead Name" />
                                    <dx:ListBoxColumn FieldName="Rack" Name="Mobile No" />
                                    <dx:ListBoxColumn FieldName="Shelf" Name="Mobile No" />
                                </Columns>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select * from  [dbo].[tbl_RackSpaceMaster] where BranchId=@BranchId and ProcessId!=@ProcessId order by rowid desc">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                     <asp:SessionParameter SessionField="ProcessId" Name="ProcessId" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>

                    </tr>

                    <tr runat="server" id="trRackSpace" visible="false">
                        <td
                            style="font-weight: bold;">available Rack Space Unit :</td>
                        <td>

                            <asp:TextBox runat="server" ID="txtRackSpace" Height="30px" Width="250px" Enabled="false"></asp:TextBox>

                        </td>
                        <td style="font-weight: bold;">total number of items
                            <br />
                            available On Rack :</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAvailableQty" Height="30px" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btn_AddConPers" Text="Add Above Item" OnClick="btn_AddConPers_Click" Height="36px" Width="170px" CssClass="w3-btn w3-blue" />
                            <asp:Button runat="server" ID="AddBarCode" Text="Item Assign" OnClick="AddBarCode_Click" Height="36px" Width="170px" CssClass="w3-btn w3-pink" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gv_Barc" runat="server" AutoGenerateColumns="False" Width="50%" OnRowCommand="gv_Barc_RowCommand" OnRowDeleting="gv_Barc_RowDeleting" OnRowEditing="gv_Barc_RowEditing" OnPageIndexChanging="gv_Barc_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="StockInId" HeaderText="StockInId"/>
                                    <asp:BoundField DataField="ItemId" HeaderText="ItemId" Visible="false" />
                                    <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                                    <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" Visible="false" />
                                    <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                                    <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                                    <asp:BoundField DataField="Row" HeaderText="Row" />
                                    <asp:BoundField DataField="Rack" HeaderText="Rack" />
                                    <asp:BoundField DataField="Shelf" HeaderText="Shelf" />
                                    <asp:BoundField DataField="Warranty" HeaderText="Warranty" />
                                    <asp:BoundField DataField="WarrantyTo" HeaderText="W_Date" />
                                    <asp:BoundField DataField="CoderLifeTo" HeaderText="CL_Date" />
                                    <asp:TemplateField HeaderText="Action">
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
                        </td>
                        <td colspan="2">
                            <asp:GridView ID="gv_ConcPer" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="50%" OnRowCommand="gv_ConcPer_RowCommand" OnRowDeleting="gv_ConcPer_RowDeleting" OnRowEditing="gv_ConcPer_RowEditing" OnPageIndexChanging="gv_ConcPer_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="StockInId" HeaderText="StockInId" Visible="false"/>
                                    <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                                    <asp:BoundField DataField="RackSpace" HeaderText="RackSpace" />
                                    <asp:BoundField DataField="Row" HeaderText="Row" />
                                    <asp:BoundField DataField="Rack" HeaderText="Rack" />
                                    <asp:BoundField DataField="Shelf" HeaderText="Shelf" />
                                    <asp:BoundField DataField="StockQty" HeaderText="StockQty" />
                                    <asp:BoundField DataField="RackQty" HeaderText="RackQty" />



                                    <asp:TemplateField HeaderText="Action">
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
                        </td>


                    </tr>

                    <tr>

                        <td colspan="6" align="center">
                            <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save" Height="36px" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" OnClick="btnClear_Click" Text="Clear" Height="36px" CssClass="w3-btn w3-orange" />

                        </td>
                    </tr>
                </table>
            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="w3-container">
        <div id="id03" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('id03').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Barcode Details</h3>
                    </header>
                </div>
                <br />
                <br />

                <div align="center">
                    <br />
                    <dx:ASPxGridView ID="gvItem" Width="100%" EnableRowsCache="true" Font-Bold="true" SettingsPager-PageSize="10" Theme="Office2003Blue" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" KeyFieldName="SrBarVodeID">
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
                                            <dx:ASPxButton ID="btnAddServiceItem" runat="server" CssClass="btn" OnClick="btnAddServiceItem_Click" HorizontalAlign="Left" Text="ADD">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>


                            </FooterRow>
                        </Templates>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand=" select a.StockInId, b.ItemName, a.SrBarVodeID, a.Barcode, a.Warranty, Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate , 
b.ItemName from tbl_RackStockInBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where  a.ItemId=@ItemId and (a.Status1='' or a.Status1 is null)
and (a.Status2='' or a.Status2 is null) and (a.Status3='' or a.Status3 is null) and (a.Status4='' or a.Status4 is null) and a.ProcessId=@ProcessId and a.BranchId=@BranchId and a.ProjectId=@ProjectId">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="ProcessId" Name="ProcessId" />
                            <asp:SessionParameter SessionField="ItemId" Name="ItemId" />
                            <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                            <asp:SessionParameter SessionField="ProjectId" Name="ProjectId" />

                        </SelectParameters>

                    </asp:SqlDataSource>
                    <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

