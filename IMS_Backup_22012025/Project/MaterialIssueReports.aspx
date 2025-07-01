<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="MaterialIssueReports.aspx.cs" Inherits="Project_MaterialIssueReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Temporary Stock Transfer Reports</h3>
            </div>
            <br />

            <div align="center" style="overflow:auto; width:1100px;">

                <dx:ASPxGridView ID="ASPxGridView1" Width="100%" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="TemIssueId">
                    <Columns>
                         <dx:GridViewDataTextColumn FieldName="TemIssueId" Caption="TemIssueId" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReqtransAppId" Caption="ReqtransAppId" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="ReqtransId" Caption="ReqtransId" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="RequisitionDate" Caption="Requisition Date"  ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="RequisitionPurpose" Caption="Requisition Purpose" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FromGr" Caption="FromGr" ReadOnly="True" Settings-AutoFilterCondition="Contains" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Location" Caption="Location" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReUserGroupName" Caption="Re User Group Name" ReadOnly="True" Visible="false" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReUserName" Caption="Re User Name" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReUser" Caption="Re User" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IssueDate" Caption="Issue Date" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Receive" Caption="Receive" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReceiveDate" Caption="Receive Date" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TentetiveReturndate" Caption="Tentetive Return date" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReturnStatus" Caption="Return Status" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReturnDate" Caption="Return Date" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="StockReceiveDate" Caption="Stock Receive Date" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="StockReceiveStatus" Caption="Stock Receive Status" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>

                    </Columns>
                    <Templates>
                        <DetailRow>
                            <dx:ASPxGridView ID="rackTaggingGrid" runat="server"
                                AutoGenerateColumns="False"
                                KeyFieldName="Barcode"
                                Width="100%" OnBeforePerformDataSelect="rackTagging_BeforePerformDataSelect">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="ItemName" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Make" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Model" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="SrBarVodeID" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataColumn>                                   
                                    <dx:GridViewDataColumn FieldName="Row" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Rack" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Shelf" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Barcode" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataColumn>

                                </Columns>
                                <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                                <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                                    <PageSizeItemSettings Items="50, 100" />

                                </SettingsPager>
                            </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />

                    </SettingsPager>
                    <SettingsDetail ShowDetailRow="true" />
                    <Settings ShowGroupPanel="True" />
                    <SettingsCustomizationWindow Enabled="True" />
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select b.TemIssueId, a.ReqtransAppId,a.ReqtransId, convert(varchar,a.RequisitionDate,103) as RequisitionDate, a.RequisitionPurpose, 
a.Status5 as FromGr, a.Status4 as Location, a.ReUserGroupName, a.ReUserName, a.Status as ReUser, convert(varchar,b.IssueDate,103) as IssueDate,
b.Receive, convert(varchar,b.ReceiveDate,103) as ReceiveDate,convert(varchar,dateadd(dd,CAST(a.days as decimal(16,6)),b.ReceiveDate),103) as TentetiveReturndate,
b.ReturnStatus,  convert(varchar,b.ReturnDate,103) as ReturnDate,
(Select top 1 Convert(Varchar, DOE,103) as DOE from tbl_RackStockInDetails where StockInId=a.ReqtransId order by rowid desc) as StockReceiveDate,
case when a.ReqtransId in (select StockInId from tbl_RackStockInDetails) then 'StockReceive' else '' end as  StockReceiveStatus
 from [tbl_RequisitionTransferApproval] a
 join tbl_TemporaryStockIssue b on a.ReqtransAppId=b.ReqAppId 
 order by a.rowid desc">
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

