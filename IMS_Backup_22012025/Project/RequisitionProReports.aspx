<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="RequisitionProReports.aspx.cs" Inherits="Project_RequisitionProReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Project Requisition Reports</h3>
            </div>
            <br />
            <div align="center">
                <dx:ASPxGridView ID="gv_Estimation" runat="server" Theme="Office2003Blue" Width="100%" AutoGenerateColumns="False" KeyFieldName="ReqProId" DataSourceID="DS_Estimation">
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ReqProId" ReadOnly="True" Caption="ReqPopId" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="RequisitionDate" ReadOnly="True" Caption="Requisition Date" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="RequisitionPurpose" ReadOnly="True" Caption="Requisition Purpose" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReUserName" ShowInCustomizationForm="True" Caption="Requisition To (Location)">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Location" ShowInCustomizationForm="True" Caption="Requisition For (Location)">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Project" ShowInCustomizationForm="True" Caption="Project">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Status" ShowInCustomizationForm="True" Caption="Status">
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
                                    <dx:TabPage Name="ItemsDetails" Text="Items Details">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <table class="dxflInternalEditorTable">
                                                    <tr>
                                                        <td class="dxflEmptyItem"></td>
                                                        <td class="dxflEmptyItem"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <dx:ASPxGridView ID="gv_EstRawMat" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" OnBeforePerformDataSelect="gv_EstRawMat_BeforePerformDataSelect" Theme="Office2003Blue" Width="100%">
                                                                <Columns>

                                                                    <dx:GridViewDataTextColumn FieldName="ReqProId" ShowInCustomizationForm="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Category" Caption="Category" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Type" Caption="Type" ReadOnly="True" Visible="false">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="ItemName" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Item Name" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Make" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Make" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Model" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Model" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Unit" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Unit" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="HSNCode" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="HSN /SAC Code" Visible="false" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="ReOrderLevel" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="ReOrder" Visible="false" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="CriticalLevel" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Critical Level" Visible="false" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="WarrantyPeriod" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Warranty Period" Visible="false" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Remarks" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Remarks" Visible="false" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="SpaceUnit" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="SpaceUnit" Visible="false" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="CoderLife" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="CoderLife" Visible="false" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Extra1" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Extra1" Visible="false" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Extra2" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Extra2" Visible="false" ReadOnly="True">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Qty" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Qty" ReadOnly="True">
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
                <asp:SqlDataSource ID="DS_Estimation" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select ReqProId, convert(varchar,RequisitionDate,103) as RequisitionDate, RequisitionPurpose, 
Status5 as FromGr, Status4 as Location, ReUserGroupName, ReUserName, Status as ReUser, Status1 as Project, Status4 as Location,
case when ReqProId in (select ReqAppId from tbl_MaterialIssue) then 'Issue'
when ReqProId in (select ReqProId from tbl_RequisitionProjectApp_Reject) then 'Reject'
Else 'Not Checked' end as Status
 from tbl_RequisitionProject order by rowid desc">
                    <%--<SelectParameters>
                        <asp:SessionParameter Name="ProjectId" SessionField="ProjectId" />
                        <asp:SessionParameter Name="FromDate" SessionField="FromDate" />
                        <asp:SessionParameter Name="ToDate" SessionField="ToDate" />
                    </SelectParameters>--%>
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"></asp:SqlDataSource>

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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

