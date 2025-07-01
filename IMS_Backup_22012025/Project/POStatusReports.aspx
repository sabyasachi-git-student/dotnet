<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="POStatusReports.aspx.cs" Inherits="Project_POStatusReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">PO Report</h3>
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
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkPODate" Style="font-weight: bold;" AutoPostBack="true" OnCheckedChanged="chkPODate_CheckedChanged" runat="server" Text="  Search By PO Date" /></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="chkPOID" Style="font-weight: bold;" AutoPostBack="true" OnCheckedChanged="chkPOID_CheckedChanged" runat="server" Text="  Search By POID" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" runat="server" id="trPOdate" visible="false">
                <table cellspacing="15px" align="center">
                    <tr >
                        <td style="font-weight: bold;">From PO Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpFromDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="font-weight: bold;">To PO Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpToDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" runat="server" id="trPOID" visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtPOID" runat="server" placeholder="Enter POID" Width="250px" Height="30px"></asp:TextBox>
                        </td>
                         <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnPOID" OnClick="btnPOID_Click" Text="Search" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear1" Text="Clear" OnClick="btnClear1_Click" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                        </tr>
                </table>
            </div>
            <br />
            <div align="center">

                <div align="center" style="overflow: auto; width: 1050px;">
                    <dx:ASPxGridView ID="grid" runat="server" KeyFieldName="OrderID" OnBeforePerformDataSelect="grid_BeforePerformDataSelect" DataSourceID="SqlDataSource1" Width="100%" Theme="Office2003Blue"
                        AutoGenerateColumns="false">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="1">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Category" Caption="Category Of Material" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="2">
                                <HeaderStyle Wrap="True"></HeaderStyle>

                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemName" ReadOnly="True" Caption="Item Name" CellStyle-Wrap="True" ShowInCustomizationForm="True" VisibleIndex="3">
                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Make" ReadOnly="True" Caption="Make" ShowInCustomizationForm="True" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Model" ReadOnly="True" Caption="Model" CellStyle-Wrap="True" ShowInCustomizationForm="True" VisibleIndex="5">
                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="POID" Caption="PO No" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="6">
                                <HeaderStyle Wrap="True"></HeaderStyle>

                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PODate" Caption="PO Date" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="7">
                                <HeaderStyle Wrap="True"></HeaderStyle>

                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CompanyName" Caption="Name Of Supplier" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="8">
                                <HeaderStyle Wrap="True"></HeaderStyle>

                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ProjectName" Caption="Project Name" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="9">
                                <HeaderStyle Wrap="True"></HeaderStyle>

                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Qty" Caption="Qty" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="10">
                                <HeaderStyle Wrap="True"></HeaderStyle>

                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Unit" Caption="Unit" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="11">
                                <HeaderStyle Wrap="True"></HeaderStyle>

                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Rate" Caption="Rate" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="12">
                                <HeaderStyle Wrap="True"></HeaderStyle>

                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Warranty" Caption="Warranty" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="13">
                                <HeaderStyle Wrap="True"></HeaderStyle>

                                <CellStyle Wrap="True"></CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFooter="true" ShowHeaderFilterButton="true" />
                        <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="True" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />
                        </SettingsPager>
                        <%--                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="CompanyName" SummaryType="Count" />
                        <dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" />
                    </TotalSummary>--%>
                    </dx:ASPxGridView>


                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="
 select distinct a.POID, convert(varchar,a.PODate,103) as PODate, sm.CompanyName, i.Category, pd.ItemId, i.ItemName, i.Make, i.Model,
b.Qty, i.Unit,  pd.Rate, a.ProjectName, b.Warranty  from tbl_StockIn a  
join tbl_StockInDetails b on a.StockInId=b.StockInId
join tbl_PurchaseBillEntry p on b.StockInId=p.StockInId  join tbl_PurchaseBillDetails pd on p.PurchaseBillId=pd.PurchaseBillId  
join tbl_ItemMaster i on b.ItemId=i.ItemId join tbl_SupplierMasterEntry sm on a.SupplierId=sm.SupplierId">
                        <SelectParameters>
                            <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="">
                    <SelectParameters>
                        <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>
            <div class="options" style="visibility: hidden">
                <div class="options-item">
                    <dx:ASPxCheckBox ID="hFModeCheckBox" runat="server" Checked="true" AutoPostBack="true" Text="Enable CheckedList mode" />
                </div>
            </div>
            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

