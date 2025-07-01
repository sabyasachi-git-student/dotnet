<%@ Page Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="WarrantyExtensionReport.aspx.cs" Inherits="Project_WarrantyExtensionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Warranty Extension Report</h3>
            </div>
            <br />

            <div algin="center">
                <table>
                    <tr>
                        
                        <td>
                            <asp:TextBox runat="server" BackColor="LightGreen" Width="50px" Enabled="false"></asp:TextBox>
                        </td>
                        <td>Active Warranty</td>
                    </tr>

                </table>
            </div>

            <br />

            <div align="center">
                <dx:ASPxGridView ID="gv_WarrantyExtensionReport" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" OnRowCommand="gv_WarrantyExtensionReport_RowCommand" OnHtmlRowPrepared="gv_WarrantyExtensionReport_HtmlRowPrepared" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="WarExId">
                    <Columns>


                        <dx:GridViewDataTextColumn FieldName="POID" Caption="PO Id" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="WarExDate" Caption="Extension Date" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="StoCkInId" Caption="StockIn Id" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Qty" Caption="Quantity" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="WarrantyTo" Caption="Warranty To" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="WarrantyExtension" Caption="Warranty Extension" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="WarrantyTo1" Caption="New Warranty To" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Update" Width="0px">
                            <DataItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" CommandName="UpdateGRN" Text="View Barcodes" Visible="false" CssClass="w3-btn w3-blue" />
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />
                    </SettingsPager>
                    <Settings ShowGroupPanel="True" />
                    <SettingsCustomizationWindow Enabled="True" />
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select WarExId,POID,convert(varchar,WarExDate,103) 'WarExDate',StoCkInId,ItemId,Qty,convert(varchar,WarrantyTo,103) 'WarrantyTo',WarrantyExtension,convert(varchar,WarrantyTo1,103) 'WarrantyTo1' from tbl_WarrantyExtension order by RowId desc"></asp:SqlDataSource>

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
                        <span onclick="document.getElementById('id03').style.display='none';"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Barcode Details</h3>
                    </header>
                </div>
                <br />
                <br />
                <div align="center">
                    <asp:TextBox runat="server" ID="txtPOID" Visible="false"></asp:TextBox>
                    <dx:ASPxGridView ID="ASPxGridView2" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" KeyFieldName="WarExId">
                        <Columns>
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
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select distinct a.Barcode  from tbl_RackStockInBarCodeDetails a join tbl_RackStockInDetails b on a.ActualStockInId=b.ActualStockInId 
join tbl_StockIn c on c.StockInId=b.StockInId where c.POID=@POID">
                        <SelectParameters>
                            <asp:SessionParameter Name="POID" SessionField="POID" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <div>
                    </div>
                </div>
            </div>
        </div>

    </div>


</asp:Content>
