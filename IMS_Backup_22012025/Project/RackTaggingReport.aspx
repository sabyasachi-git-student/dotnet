<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="RackTaggingReport.aspx.cs" Inherits="Project_RackTaggingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Stock In Hand Reports</h3>
            </div>
            <br />
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnXlsExport" runat="server" OnClick="btnXlsExport_Click" Text="Export to XLS" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="btnXlsxExport" runat="server" OnClick="btnXlsxExport_Click" Text="Export to XLSX" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
                        </td>
                    </tr>
                </table>
            </div><br />
            <div align="center">

                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="ItemId">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Make" Caption="Make" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Model" Caption="Model" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="WarrantyPeriod" Caption="Power Supply" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="GRNNo" Caption="GRN No" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Qty" Caption="Quantity" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="RackSpace" Caption="Rack Space Unit" Visible="false" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Row" Caption="Row" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Rack" Caption="Rack" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Shelf" Caption="Shelf" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemUnit" Caption="Item Unit" ReadOnly="True" Visible="false" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />
                    </SettingsPager>
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="SELECT distinct rsd.ItemId,im.ItemName,im.Make,im.Model,im.WarrantyPeriod,rsd.GRNNo,rsd.ItemId,rsd.RackSpace,rsd.Row,rsd.Rack,rsd.Shelf,
                    rsd.ItemUnit,rsd.BranchId,(select sum(qty) from tbl_RackStockInBarCodeDetails where ItemId=rsd.ItemId and ProcessId= rsd.RackSpace) as Qty FROM tbl_RackStockInDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID   where rsd.BranchId=@BranchId">
                    <SelectParameters>
                        <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>

            <br />
        </div>
    </div>

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Item Wise Stock In Hand Reports</h3>
            </div>
            <br />
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="ASPxButton1" runat="server" OnClick="ASPxButton1_Click" Text="Export to XLS" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="ASPxButton2" runat="server" OnClick="ASPxButton2_Click"  Text="Export to XLSX" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="ASPxGridView2"></dx:ASPxGridViewExporter>
                        </td>
                    </tr>
                </table>
            </div><br />
            <div align="center">

                <dx:ASPxGridView ID="ASPxGridView2" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" KeyFieldName="ItemId">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Make" Caption="Make" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Model" Caption="Model" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="WarrantyPeriod" Caption="Power Supply" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="Row" Caption="Row" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Rack" Caption="Rack" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Shelf" Caption="Shelf" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SrBarVodeID" Caption="SrBarVodeID" ReadOnly="True" Visible="false" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Barcode" Caption="Barcode No" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />
                    </SettingsPager>
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="Select r.ItemId, im.ItemName, im.Make, im.Model,  im.WarrantyPeriod, r.SrBarVodeID,r.Barcode,r.Row,r.Rack,r.Shelf,r.Qty as Qty FROM tbl_RackStockInBarCodeDetails r join tbl_ItemMaster im on im.ItemId=r.ItemID WHERE (r.Status1='' or r.Status1 is null) and (r.Status2='' or r.Status2 is null) and  r.BranchId=@BranchId">
                    <SelectParameters>
                        <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

