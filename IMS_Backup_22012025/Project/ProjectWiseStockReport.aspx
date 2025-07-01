<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="ProjectWiseStockReport.aspx.cs" Inherits="Project_ProjectWiseStockReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Project Wise Stock In Hand Report</h3>
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
                          <td></td>
                        <td>
                            <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Go To Project Wise Item In Hand Barcode Report" CssClass="w3-btn w3-green" />

                        </td>
                    </tr>
                     <tr>
                         </tr>
                    <tr>
                        <td>
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
                        </td>
                    </tr>
                </table>
            </div><br />

            <div align="center">

                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="15" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="ItemId">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Category" ReadOnly="True" Caption="Category" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName"  CellStyle-Wrap="True" Caption="Item Name" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Make" Caption="Make" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Model" Caption="Model" CellStyle-Wrap="True"  ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="WarrantyPeriod" Caption="Power Supply" CellStyle-Wrap="True"  ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="Qty" Caption="Quantity" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>                       
                        <dx:GridViewDataTextColumn FieldName="ProjectName" Caption="Project Name" CellStyle-Wrap="True" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    
                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />
                    </SettingsPager>
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="
  SELECT distinct rsd.ItemId,im.ItemName,im.Make,im.Model,rsd.ItemId,im.Category, im.WarrantyPeriod,
 rsd.BranchId,rsd.ProjectId, rsd.ProjectName, sum(qty) as Qty
 FROM tbl_RackStockInBarCodeDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID    
  where rsd.BranchId='BR102'
  and (Status1='' or status1 is null) and (Status2='' or status2 is null)
 and (Status3='' or status3 is null) and (Status4='' or status4 is null) group by 
 rsd.ItemId,im.ItemName,im.Make,im.Model,rsd.ItemId,im.Category, im.WarrantyPeriod,
 rsd.BranchId,rsd.ProjectId, rsd.ProjectName">
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

