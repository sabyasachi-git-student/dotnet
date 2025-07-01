<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="GRNNoUpdate.aspx.cs" Inherits="Project_GRNNoUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">GRN NO Update</h3>
            </div>
            <br />

            <div align="center">

                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" OnRowCommand="ASPxGridView1_RowCommand" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="StockInId">
                    <Columns>
                         <dx:GridViewDataTextColumn FieldName="StockInId" Caption="Stock In Id" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="POID" Caption="PO ID" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ChallanNo" Caption="Challan No" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="InvoiceNo" Caption="Invoice No" ReadOnly="True" Visible="false" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="CompanyName" Caption="Supplier Name"  ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="GRNNo" Caption="GRN No" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Update" VisibleIndex="7" Width="0px">
                            <DataItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" CommandName="UpdateGRN" Text="Update" CssClass="w3-btn w3-blue" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="Select distinct rsd.StockInId, s.POID, s.ChallanNo, s.InvoiceNo, rsd.GRNNo, sm.CompanyName FROM tbl_RackStockInDetails rsd 
join tbl_ItemMaster im on im.ItemId=rsd.ItemID  
join tbl_StockIn s on rsd.StockInId=s.StockInId
join tbl_SupplierMasterEntry sm on s.SupplierId=sm.SupplierId
where (rsd.GRNNo is Null or rsd.GRNNo='') and rsd.BranchId=@BranchId">
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

    <div class="w3-container">
        <div id="id03" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('id03').style.display='none';"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Enter GRN No</h3>
                    </header>
                </div>
                <br />
                <br />
                <div align="center">

                    <br />
                    <table>
                        <tr>
                            <td style="font-weight: bold;">StockIn Id : <span style="color: red;">*</span>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtSOId" Enabled="false" Height="30px" Width="100px"></asp:TextBox>
                                <asp:Label runat="server" ID="lblSOId" Visible="false"></asp:Label>
                            </td>
                           
                            <td style="font-weight: bold;">Date : 
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="150px">
                                </dx:ASPxDateEdit>
                            </td>
                             <td style="font-weight: bold;">GRN No : </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtGRNNo"  Height="30px" Width="150px"></asp:TextBox>
                            </td>
                            <td style="font-weight: bold;">Remarks : </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Style="resize: none" Height="60px" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"  Text="Update" CssClass="w3-btn w3-blue" />
                            </td>
                        </tr>
                    </table>
                
                    <div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>

