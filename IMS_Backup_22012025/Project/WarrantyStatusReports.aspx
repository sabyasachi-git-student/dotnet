<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="WarrantyStatusReports.aspx.cs" Inherits="Project_WarrantyStatusReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Warranty Status Reports</h3>
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
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="WarrantyStatusReports"></dx:ASPxGridViewExporter>
                        </td>
                    </tr>
                </table>
            </div>

            <div align="center">
                <table>
                    <tr>
                         <td>
                            <asp:CheckBox ID="chkCateory" Style="font-weight: bold;" AutoPostBack="true" OnCheckedChanged="chkCateory_CheckedChanged" runat="server" Text="  Search By Category" /></td>
                        <td> </td><td></td><td></td>
                             <td> <asp:CheckBox ID="chkPODate" Style="font-weight: bold;" AutoPostBack="true" OnCheckedChanged="chkPODate_CheckedChanged" runat="server" Text="  Search By Warranty Date Upto" /></td>
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
                    <tr>
                        <td style="font-weight: bold;">Warranty Date Upto :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpFromDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
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
            <div align="center" runat="server" id="trCat" visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCategory" runat="server" placeholder="Enter Category" Width="250px" Height="30px"></asp:TextBox>
                        </td>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnCategory" OnClick="btnCategory_Click" Text="Search" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear2" Text="Clear" OnClick="btnClear2_Click" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div><br />

            <div align="center" style="overflow: auto; width: 1050px;">
                <dx:ASPxGridView ID="WarrantyStatusReports" runat="server" Theme="Office2003Blue" OnBeforePerformDataSelect="WarrantyStatusReports_BeforePerformDataSelect" SettingsPager-PageSize="15" Width="100%" AutoGenerateColumns="False" KeyFieldName="StockInId" DataSourceID="SqlDataSource1">
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="StockInId" ReadOnly="True" Caption="Stock In Id" Visible="false" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="POID" ReadOnly="True" Caption="PO No." ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PODate" ReadOnly="True" Caption="PO Date" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="GRNNo" ReadOnly="True" Caption="GRN No" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="GrnDate" ReadOnly="True" Caption="GRN Date" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="StockInId" ReadOnly="True" Caption="Stock In Id" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="StockInDate" ReadOnly="True" Caption="StockIn Date" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CompanyName" ReadOnly="True" CellStyle-Wrap="True" Caption="Supplier Name" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ProjectName" ReadOnly="True" CellStyle-Wrap="True" Caption="Project" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemId" ReadOnly="True" Caption="Item Id" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Category" ReadOnly="True" Caption="Category Of The Material" HeaderStyle-Wrap="True" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" ReadOnly="True" CellStyle-Wrap="True" Caption="Item Name" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Make" ReadOnly="True" Caption="Make" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Model" ReadOnly="True" CellStyle-Wrap="True" Caption="Model" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="WarrantyPeriod" ReadOnly="True" Caption="Power Supply" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Qty" ReadOnly="True" Caption="Qty" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Unit" ReadOnly="True" Caption="Unit" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Warranty" ReadOnly="True" Caption="Warranty in Months" HeaderStyle-Wrap="True" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="WarrantyTo" ReadOnly="True" Caption="Warranty Valid Upto" HeaderStyle-Wrap="True" ShowInCustomizationForm="True">
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
                                                                    <dx:GridViewDataTextColumn FieldName="CompanyName" Caption="Supplier" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="ContactPerson" Caption="Contact   Person" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="ContactNumber" Caption="Contact Number" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="EmailId" Caption="Email Id" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Address" Caption="Address" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>

                                                                <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowFilterRow="true" ShowHeaderFilterButton="True" />

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

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand=""></asp:SqlDataSource>
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

