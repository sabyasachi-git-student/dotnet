<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="ScrapReports.aspx.cs" Inherits="Project_ScrapReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Scrap Report</h3>
            </div>
            <br />

            <div align="center">
                <dx:ASPxGridView ID="gv_WarrantyExtensionReport" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true"  SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="ItemId">
                    <Columns>


                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="ItemId" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Make" Caption="Make" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Model" Caption="Model" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SrBarVodeID" Caption="SrBarVodeID" Visible="false" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Barcode" Caption="Barcode" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Warranty" Caption="Warranty" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CoderLife" Caption="Coder Life" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ScrapDate" Caption="Scrap Date" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Status4" Caption="Status" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />
                    </SettingsPager>
                    <Settings ShowGroupPanel="True" />
                    <SettingsCustomizationWindow Enabled="True" />
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="Select a.ItemId, i.ItemName, i.Make, i.Model, a.SrBarVodeID, a.Barcode, convert(Varchar,a.WarrantyTo,103) as Warranty, convert(Varchar,a.CoderLifeTo,103) as CoderLife, convert(Varchar,a.Warranty3rdDOE,103) as ScrapDate, a.Status4 from [dbo].[tbl_RackStockInBarCodeDetails] a Join tbl_ItemMaster i on a.ItemId=i.ItemId where a.Status4='Scrap-StockOut' and a.BranchId=@BranchId order by Id desc">
                    <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>

                </asp:SqlDataSource>

            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

