<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="BarcodeInUseReports.aspx.cs" Inherits="Project_BarcodeInUseReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Material In Use Report</h3>
            </div>
            <br />

            <div align="center">

                <dx:ASPxGridView ID="ASPxGridView2" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" KeyFieldName="ItemId">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Make" Caption="Make" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Model" Caption="Model"  ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="Row" Caption="Row" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Rack" Caption="Rack" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Shelf" Caption="Shelf" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="SrBarVodeID" Caption="SrBarVodeID" Visible="false" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Barcode" Caption="Barcode" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    
                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />

                    </SettingsPager>
                    <Settings ShowGroupPanel="True" />
                    <SettingsCustomizationWindow Enabled="True" />
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="Select r.ItemId, im.ItemName, im.Make, im.Model, r.SrBarVodeID,r.Barcode,r.Row,r.Rack,r.Shelf,r.Qty as Qty FROM tbl_RackStockInBarCodeDetails r join tbl_ItemMaster im on im.ItemId=r.ItemID WHERE (r.Status1='InUse') and (r.Status2='InUse') and  r.BranchId=@BranchId">
                    <SelectParameters>
                        <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

