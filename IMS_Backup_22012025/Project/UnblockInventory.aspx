<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="UnblockInventory.aspx.cs" Inherits="Project_UnblockInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Unblock Inventory</h3>
            </div>
            <br />
            <div align="center">
                <br />
                <div style="overflow: auto;">

                    <dx:ASPxGridView ID="gv_Estimation" runat="server" Theme="Office2003Blue" Width="100%" AutoGenerateColumns="False" KeyFieldName="BlockId" OnRowCommand="gv_Estimation_RowCommand" DataSourceID="DS_Estimation">
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="BlockId" ReadOnly="True" Caption="Block Id" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="BlockDate" ReadOnly="True" Caption="Block Date" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemId" ReadOnly="True" Caption="Item Id" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemName" ReadOnly="True" Caption="Item Name" CellStyle-Wrap="True" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Make" ReadOnly="True" Caption="Make" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Model" ReadOnly="True" Caption="Model" CellStyle-Wrap="True" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Qty" ReadOnly="True" Caption="Qty" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="BlockOption" ReadOnly="True" Caption="Block Option" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ProjectName" ShowInCustomizationForm="True" Caption="Project Name" CellStyle-Wrap="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Remarks" ShowInCustomizationForm="True" Caption="Remarks" CellStyle-Wrap="True">
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="Cancel" VisibleIndex="10" Width="0px">
                                <DataItemTemplate>
                                    <asp:Button runat="server" ID="btnCancel" Text="Unblock" CommandArgument='<%#Eval("BlockId")%>' CommandName="Cancel" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>

                        </Columns>

                        <SettingsBehavior AllowSelectSingleRowOnly="True" />
                        <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowFilterRow="true" ShowHeaderFilterButton="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />

                        </SettingsPager>
                        <Templates>
                            <DetailRow>
                                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" RenderMode="Lightweight" Theme="Office2003Blue">
                                    <TabPages>
                                        <dx:TabPage Name="RawMaterials" Text="Raw Materials">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <table class="dxflInternalEditorTable">
                                                        <tr>
                                                            <td class="dxflEmptyItem"></td>
                                                            <td class="dxflEmptyItem"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <dx:ASPxGridView ID="gv_EstRawMat" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" OnBeforePerformDataSelect="gv_EstRawMat_BeforePerformDataSelect" Theme="Office2003Blue" Width="100%">
                                                                    <Columns>

                                                                        <dx:GridViewDataTextColumn FieldName="BlockId" ShowInCustomizationForm="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemName" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Item Name" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Make" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Make" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Model" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Model" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="barcode" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="barcode" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>

                                                                    <Settings ShowFilterRow="True" ShowFooter="True" />
                                                                </dx:ASPxGridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                </dx:ASPxPageControl>
                            </DetailRow>
                        </Templates>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="DS_Estimation" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select a.BlockId, convert(varchar,a.BlockDate,103) as BlockDate, a.Remarks, a.BlockOption, a.ProjectName,
a.ItemId, B.ItemName, b.Make, b.Model, b.Category, a.Qty from tbl_blockInventoryDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId 
where a.BranchId='BR102' and a.BlockId in (Select BlockId from tbl_blockInventoryBarCodeDetails where (Status is null or status ='')) order by a.rowid desc">
                        <SelectParameters>
                            <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                            <%--<asp:SessionParameter Name="FromDate" SessionField="FromDate" />
                        <asp:SessionParameter Name="ToDate" SessionField="ToDate" />--%>
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"></asp:SqlDataSource>
                </div>

                <table cellspacing="15px" align="center" style="visibility: hidden">
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqId" Visible="false" Enabled="false" Height="30px" Style="resize: none" Width="125px"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtBranchName" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserGroup" Enabled="false" Height="30px" Style="resize: none" Width="130px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqUser" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="w3-container">
        <div id="id03B" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('id03B').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Block Barcode Details</h3>
                    </header>
                </div>
                <br />
                <br />
                <div align="center" style="visibility:hidden;">
                    <asp:TextBox runat="server" ID="txtBlockId" Enabled="false" Height="30px" Width="250px"></asp:TextBox>
                </div>

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
                            <dx:GridViewDataTextColumn Caption="" Name="Date" VisibleIndex="9" Width="0px">
                                <DataItemTemplate>
                                    <asp:Label runat="server" ID="lblSrBarVodeID" Visible="false" Text='<%#Eval("SrBarVodeID") %>'></asp:Label>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="" Name="Date" VisibleIndex="9" Width="0px">
                                <DataItemTemplate>
                                    <asp:Label runat="server" ID="lblBarcode" Visible="false" Text='<%#Eval("Barcode") %>'></asp:Label>
                                </DataItemTemplate>
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
                                            <dx:ASPxButton ID="btnUmblockBarcode" runat="server" OnClick="btnUmblockBarcode_Click" CssClass="btn" HorizontalAlign="Left" Text="Unblock">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>


                            </FooterRow>
                        </Templates>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select bl.StockInId, bl.BlockId, b.ItemName, bl.SrBarVodeID, bl.Barcode, a.Warranty, Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate , 
b.ItemName from tbl_RackStockInBarCodeDetails a 
join tbl_blockInventoryBarCodeDetails bl on a.SrBarVodeID=bl.SrBarVodeID
join tbl_ItemMaster b on a.ItemId=b.ItemId 
where  bl.BlockId=@SaleInvoiceNo and (bl.Status='' or bl.Status is null) and a.BranchId=@BranchId">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                            <asp:SessionParameter SessionField="SaleInvoiceNo" Name="SaleInvoiceNo" />
                        </SelectParameters>

                    </asp:SqlDataSource>
                    <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

