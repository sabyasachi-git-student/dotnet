<%@ Page Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="FaultyEntryReport.aspx.cs" Inherits="Project_FaultyEntryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Faulty Entry Reports</h3>
            </div>
            <br />
            <div align="center">
                <br />
                <div style="overflow: auto; width: 1150px;">

                    <dx:ASPxGridView ID="gv_FaultyEntry" runat="server" Theme="Office2003Blue" Width="100%" AutoGenerateColumns="False" KeyFieldName="FaultyId" DataSourceID="FaultyEntry">
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                        <Columns>

                            <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="FaultyId" ReadOnly="True" Caption="FaultyId" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FaultyDate" ReadOnly="True" Caption="Faulty Date" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FaultyReason" ReadOnly="True" Caption="Faulty Reason" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Remarks" ReadOnly="True" Caption="Remarks" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SentStatus" ReadOnly="True" Caption="Sent Status" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="OEM" ReadOnly="True" Caption="OEM" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ToRepairer" ReadOnly="True" Caption="To Repairer" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PickUp" ReadOnly="True" Caption="Pick Up" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CorrierName" ReadOnly="True" Caption="Courier Name" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ConsignNo" ReadOnly="True" Caption="Consign. No" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ConsignDate" ReadOnly="True" Caption="Consign. Date" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RmaNo" ReadOnly="True" Caption="RMA No" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RmaDate" ReadOnly="True" Caption="RMA Date" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RecvStatus" ReadOnly="True" Caption="Receive Status" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RecDate" ReadOnly="True" Caption="Receive Date" ShowInCustomizationForm="True">
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
                                        <dx:TabPage Name="FaultyEntryDetails" Text="Faulty Entry Details">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <table class="dxflInternalEditorTable">
                                                        <tr>
                                                            <td class="dxflEmptyItem"></td>
                                                            <td class="dxflEmptyItem"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <dx:ASPxGridView ID="gv_FaultyEntryDetails" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" OnBeforePerformDataSelect="gv_FaultyEntryDetails_BeforePerformDataSelect" Theme="Office2003Blue" Width="100%">
                                                                    <Columns>

                                                                        <dx:GridViewDataTextColumn FieldName="FaultyId" ShowInCustomizationForm="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="FaultyDeId" Caption="Faulty Details Id" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="FaultyDate" Caption="Faulty Date" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Make" Caption="Make" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Model" Caption="Model" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SrBarVodeId" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="SrBarVodeId" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Barcode" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Barcode" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>

                                                                    <SettingsBehavior AllowSelectSingleRowOnly="True" />
                                                                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowFilterRow="true" ShowHeaderFilterButton="True" />
                                                                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                                                                        <PageSizeItemSettings Items="50, 100" />

                                                                    </SettingsPager>
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
                    <asp:SqlDataSource ID="FaultyEntry" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select distinct a.FaultyId,  convert(varchar,a.FaultyDate,103) as FaultyDate, a.FaultyReason, a.Remarks, a.BranchId, 
b.OEM, d.ToRepairer, B.PickUp, b.CorrierName, b.ConsignNo, convert(varchar,b.ConsignDate,103) as ConsignDate, b.RmaNo, convert(varchar,b.RmaDate,103) as RmaDate, 
convert(varchar,c.RecDate,103) as RecDate, Case when  a.FaultyId not in (Select FaultyId from [tbl_FaultyInWarranty]) then 'Not Taken' else 'Taken' end as SentStatus,
Case when  a.FaultyId not in (Select FaultyId from [tbl_FaultyItemReceice]) then 'Not Received' else 'Received' end as RecvStatus
from tbl_FaultyEntry a 
left join [tbl_FaultyInWarranty] b on a.FaultyId=b.FaultyId
left join [tbl_FaultyItemReceice] c on a.FaultyId = c.FaultyId 
left join [tbl_FaultyWarrantyExpired] d on a.FaultyId=d.FaultyId where a.BranchId=@BranchId">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"></asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

