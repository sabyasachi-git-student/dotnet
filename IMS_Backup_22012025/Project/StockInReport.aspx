<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="StockInReport.aspx.cs" Inherits="Project_StockInReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">StockIn Report</h3>
            </div>
            <br />

            <div align="center">

                
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" Theme="Office2003Blue" Width="100%" AutoGenerateColumns="False" KeyFieldName="ItemId" DataSourceID="SqlDataSource1" OnBeforePerformDataSelect="ASPxGridView1_BeforePerformDataSelect">
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
               
                    <Columns>
                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="Poid" ReadOnly="True" Caption="Po id" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="GRNNo" ReadOnly="True" Caption="GRN No" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemId" ReadOnly="True" Caption="Item Id" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" ReadOnly="True" Caption="Item Name"  ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Category" ReadOnly="True" Caption="Category" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Type" ReadOnly="True" Caption="Type" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Make" ReadOnly="True" Caption="Make" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Model" ReadOnly="True" Caption="Model" CellStyle-Wrap="True" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Unit" ReadOnly="True" Caption="Unit" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="HSNCode" ReadOnly="True" Caption="HSN Code" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ScrapGroup" ReadOnly="True" Caption="Scrap Group" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="WarrantyPeriod" ReadOnly="True" Caption="Power Supply" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Quantity" ReadOnly="True" Caption="Quantity" ShowInCustomizationForm="True">
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
                                    <dx:TabPage Name="ProductsGrid" Text="Items">
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
                                                                    <dx:GridViewDataTextColumn FieldName="StockInDate" Caption="Stock In Date" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Quantity" Caption="Quantity" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="ProjectName" Caption="Project Name" CellStyle-Wrap="True" HeaderStyle-Wrap="True" ReadOnly="True">
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
                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select si.StockInDate,sum(sid.Qty) as Quantity
from 
tbl_StockInDetails sid inner join tbl_StockIn si on sid.StockInId=si.StockInId
where sid.itemid=@itemid
group by si.StockInDate">
      <SelectParameters>
       
                <asp:Parameter Name="itemid" Type="String" />

           
        </SelectParameters>       
           
            </asp:SqlDataSource>--%>
            </div>

            <br />
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

