<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="StockTransfer.aspx.cs" Inherits="Project_StockTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Project Wise Stock Transfer</h3>
            </div>
            <br /> 

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                </table>

                <table cellspacing="15px" align="center">

                    <tr>
                        <td style="font-weight: bold;">Project From:
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlItem" runat="server" OnTextChanged="ddlItem_TextChanged"
                                DataSourceID="SqlDataSource4" Height="30px" Width="250px" AutoPostBack="True" TextFormatString="{0}"
                                ValueField="ProjectId" TextField="ProjectName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10">
                                <%-- <Columns>
                                    <dx:ListBoxColumn Caption="ItemName" FieldName="ItemName" />
                                    <dx:ListBoxColumn Caption="Make" FieldName="Make" />
                                    <dx:ListBoxColumn Caption="Model" FieldName="Model" />
                                     <dx:ListBoxColumn Caption="ItemId" FieldName="ItemId" />
                                </Columns>--%>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct sd.ProjectId, sd.ProjectName  from tbl_RackStockInDetails sd where sd.BranchId=@BranchId">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                        <td style="font-weight: bold;">Item :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Height="30px" Width="250px" Theme="Office2003Blue" AutoPostBack="true" OnTextChanged="ASPxComboBox1_TextChanged"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" DataSourceID="SqlDataSource2" TextField="ItemName" ValueField="ItemId"
                                TextFormatString=" {0}" CallbackPageSize="20" EnableCallbackMode="True">
                                <Columns>
                                    <dx:ListBoxColumn Caption="ItemName" FieldName="ItemName" />
                                    <dx:ListBoxColumn Caption="Make" FieldName="Make" />
                                    <dx:ListBoxColumn Caption="Model" FieldName="Model" />
                                    <dx:ListBoxColumn Caption="ItemId" FieldName="ItemId" />
                                </Columns>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct sd.ItemId, i.ItemName, i.Make, i.Model  from tbl_RackStockInDetails sd join tbl_ItemMaster i on sd.ItemId=i.ItemId where sd.BranchId=@BranchId and sd.ProjectId=@ProjectId">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="ProjectId" Name="ProjectId" />
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                    </tr>

                    <tr>

                        <td style="font-weight: bold;">Stock In Hand Qty :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtStockQty" Height="30px" Width="250px" Enabled="false"></asp:TextBox>

                        </td>
                        <td style="font-weight: bold;">Qty To Be Transfered :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRackQty" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>

                        <td style="font-weight: bold;">Assign To Project:
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlRackSpace" runat="server" Height="30px" Width="250px" Theme="Office2003Blue"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" OnTextChanged="ddlRackSpace_TextChanged"
                                AutoPostBack="true" DataSourceID="SqlDataSource1" TextField="ProjectName" ValueField="ProjectId"
                                TextFormatString=" {0}" CallbackPageSize="20" EnableCallbackMode="True">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct sd.ProjectId, sd.ProjectName  from tbl_ProjectMaster sd where  sd.ProjectId !=@ProjectId">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                    <asp:SessionParameter SessionField="ProjectId" Name="ProjectId" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td style="font-weight: bold;">Available Qty :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtProAvail" Height="30px" Width="250px" Enabled="false"></asp:TextBox>

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
                            <asp:Button runat="server" ID="AddBarCode" Text="Barcode Assign" OnClick="AddBarCode_Click" Height="36px" Width="170px" CssClass="w3-btn w3-pink" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="gv_Barc" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_Barc_RowCommand" OnRowDeleting="gv_Barc_RowDeleting" OnRowEditing="gv_Barc_RowEditing" OnPageIndexChanging="gv_Barc_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="StockInId" HeaderText="StockInId" Visible="false" />
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

                    </tr>
                </table>
                <table>
                    <tr>

                        <td colspan="6" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" Height="36px" CssClass="w3-btn w3-green" />
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
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select a.StockInId, b.ItemName, a.SrBarVodeID, a.Barcode, a.Warranty, Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate , 
b.ItemName from tbl_RackStockInBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where  a.ItemId=@ItemId and (a.Status1='' or a.Status1 is null)
and (a.Status2='' or a.Status2 is null) and (a.Status3='' or a.Status3 is null) and (a.Status4='' or a.Status4 is null) and a.BranchId=@BranchId and a.ProjectId=@ProjectId">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                            <asp:SessionParameter SessionField="ProjectId" Name="ProjectId" />
                            <asp:SessionParameter SessionField="ItemId" Name="ItemId" />
                        </SelectParameters>

                    </asp:SqlDataSource>
                    <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

