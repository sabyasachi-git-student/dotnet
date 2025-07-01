<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="blockInventoryReport.aspx.cs" Inherits="Project_blockInventoryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Block Inventory Reports</h3>
            </div>
            <br />
            <div align="center">
                <br />
                <div style="overflow: auto;">

                    <dx:ASPxGridView ID="gv_Estimation" runat="server" Theme="Office2003Blue" Width="100%" AutoGenerateColumns="False" KeyFieldName="BlockId" DataSourceID="DS_Estimation">
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="BlockId" ReadOnly="True" Caption="Block Id" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="BlockDate" ReadOnly="True" Caption="Bloc kDate" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemId" ReadOnly="True" Caption="Item Id" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemName" ReadOnly="True" Caption="Item Name" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Make" ReadOnly="True" Caption="Make" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Model" ReadOnly="True" Caption="Model" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Qty" ReadOnly="True" Caption="Qty" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="BlockOption" ReadOnly="True" Caption="Block Option" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ProjectName" ShowInCustomizationForm="True" Caption="Project Name">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Remarks" ShowInCustomizationForm="True" Caption="Remarks">
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
                                        <dx:TabPage Name="RawMaterials" Text="Raw Materials">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <table class="dxflInternalEditorTable">
                                                        <tr>
                                                            <td class="dxflEmptyItem"></td>
                                                            <td class="dxflEmptyItem"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <dx:ASPxGridView ID="gv_EstRawMat" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" OnBeforePerformDataSelect="gv_EstRawMat_BeforePerformDataSelect" Theme="Office2003Blue" Width="100%">
                                                                    <Columns>

                                                                        <dx:GridViewDataTextColumn FieldName="BlockId" ShowInCustomizationForm="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemName" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Item Name" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Make" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Make" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Model" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Model" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="barcode" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="barcode" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>

                                                                    <Settings ShowFilterRow="True" ShowFooter="True" />
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
                    <asp:SqlDataSource ID="DS_Estimation" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select a.BlockId, convert(varchar,a.BlockDate,103) as BlockDate, a.Remarks, a.BlockOption, a.ProjectName,
a.ItemId, B.ItemName, b.Make, b.Model, b.Category, a.Qty from tbl_blockInventoryDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.BranchId=@BranchId">
                        <SelectParameters>
                            <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                            <%--<asp:SessionParameter Name="FromDate" SessionField="FromDate" />
                        <asp:SessionParameter Name="ToDate" SessionField="ToDate" />--%>
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"></asp:SqlDataSource>
                </div>

                <table cellspacing="15px" align="center" style="visibility: hidden">
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqId" Visible="false" Enabled="false" Height="30px" Style="resize: none" Width="125px"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtBranchName" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserGroup" Enabled="false" Height="30px" Style="resize: none" Width="130px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqUser" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

