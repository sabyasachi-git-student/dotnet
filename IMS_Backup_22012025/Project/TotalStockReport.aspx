<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="TotalStockReport.aspx.cs" Inherits="Project_TotalStockReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Total Stock In Hand Reports</h3>
            </div>
            <br />
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnXlsExport" runat="server" OnClick="ASPxButton1_Click" Text="Export to XLS" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="btnXlsxExport" runat="server" OnClick="ASPxButton2_Click" Text="Export to XLSX" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="TotalStockReportExport"></dx:ASPxGridViewExporter>

                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <dx:ASPxGridView ID="TotalStockReportExport" runat="server" Theme="Office2003Blue" SettingsPager-PageSize="15" Width="100%" AutoGenerateColumns="False" KeyFieldName="ItemId" DataSourceID="SqlDataSource1">
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ItemId" ReadOnly="True" Caption="Item Id" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Category" ReadOnly="True" Caption="Category Of The Material" HeaderStyle-Wrap="True" CellStyle-Wrap="True" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" ReadOnly="True" Caption="Item Name" CellStyle-Wrap="True" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Make" ReadOnly="True" Caption="Make" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Model" ReadOnly="True" Caption="Model" CellStyle-Wrap="True" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="WarrantyPeriod" ReadOnly="True" Caption="Power supply" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Qty" ReadOnly="True" Caption="Qty" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Unit" ReadOnly="True" Caption="Unit" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TerrytoryId" ReadOnly="True" Caption="Name Of The Territory" HeaderStyle-Wrap="True" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="BranchName" ReadOnly="True" Caption="PoP Name" HeaderStyle-Wrap="True" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FullName" ReadOnly="True" Caption="Name Of The PoP FE" HeaderStyle-Wrap="True" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PhoneNumber" ReadOnly="True" Caption="Mobile No Of PoP FE" HeaderStyle-Wrap="True" ShowInCustomizationForm="True">
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
                                    <dx:TabPage Name="ProductsGrid" Text="Details">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <table class="dxflInternalEditorTable">
                                                    <tr>
                                                        <td class="dxflEmptyItem"></td>
                                                        <td class="dxflEmptyItem"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <dx:ASPxGridView ID="ProductsGrid" runat="server" AutoGenerateColumns="False" KeyFieldName="StockInDate" Theme="Office2003Blue" Width="100%" OnBeforePerformDataSelect="productsGrid_BeforePerformDataSelect">

                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn FieldName="POID" Caption="PO No" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="PODate" Caption="PO Date" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="CompanyName" Caption="Supplier" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="ProjectName" Caption="Project Name" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Qty" Caption="Qty" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Unit" Caption="Unit" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Rate" Caption="Rate" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Warranty" Caption="Warranty" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>

                                                                <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowFilterRow="true" ShowHeaderFilterButton="True" />
                                                              <%--  <TotalSummary>
                                                                    <dx:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" />
                                                                </TotalSummary>--%>
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

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="SELECT distinct rsd.ItemId,im.Category,im.ItemName,im.Make,im.Model,im.WarrantyPeriod,(select sum(qty) from tbl_RackStockInBarCodeDetails where ItemId=rsd.ItemId and BranchID= rsd.BranchId and (Status1='' or Status1 is null) and (Status2='' or Status2 is null)  and (Status3='' or Status3 is null) and (Status4='' or Status4 is null)) as Qty,im.Unit,hk.TerrytoryId,b.BranchName,u.FullName, u.PhoneNumber FROM tbl_RackStockInDetails rsd  join tbl_ItemMaster im on im.ItemId=rsd.ItemID  join tbl_Branch b on rsd.BranchId=b.BranchId  left join tbl_HigherKeyMapping hk on b.BranchName=hk.POPId join tbl_User u on rsd.UserId=u.UserName where (select sum(qty) from tbl_RackStockInBarCodeDetails where ItemId=rsd.ItemId and BranchID= rsd.BranchId and (Status1='' or Status1 is null)  and (Status2='' or Status2 is null) and (Status3='' or Status3 is null) and (Status4='' or Status4 is null))>0"></asp:SqlDataSource>
            </div>

            <br />
        </div>
        <div class="options" style="visibility: hidden">
            <div class="options-item">
                <dx:ASPxCheckBox ID="hFModeCheckBox" runat="server" Checked="true" AutoPostBack="true" Text="Enable CheckedList mode" />
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

