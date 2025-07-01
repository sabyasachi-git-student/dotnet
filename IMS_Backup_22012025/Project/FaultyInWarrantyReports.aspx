<%@ Page Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="FaultyInWarrantyReports.aspx.cs" Inherits="Project_FaultyInWarrantyReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Faulty In Warranty Report</h3>
            </div>
            <br />

             <div align="center">
                   
        <dx:ASPxGridView ID="gv_InWarranty" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="FaultyInWarId"  >
                    <Columns>

                     
                        <dx:GridViewDataTextColumn FieldName="FaultyId" Caption="FaultyId" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FaultyInWarId" Caption="FaultyInWarId" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="OEM" Caption="OEM" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Pickup" Caption="Pickup" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CorrierName" Caption="CorrierName" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>                        
                        <dx:GridViewDataTextColumn FieldName="ConsignNo" Caption="ConsignNo" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ConsignDate" Caption="ConsignDate" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="RmaNo" Caption="RmaNo" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="RmaDate" Caption="RmaDate" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
 
                    </Columns>
         <Templates>
                <DetailRow>
                    <dx:ASPxGridView ID="FaultyInWarrantyGrid" runat="server" 
                            AutoGenerateColumns="False" 
                            KeyFieldName="FaultyInWarId"
                            Width="100%" OnBeforePerformDataSelect="FaultyInWarranty_BeforePerformDataSelect">
                          

                         <Columns>
                                <dx:GridViewDataColumn FieldName="FaultyInWarId" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="FaultyInWarDeId" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="SrBarVodeId" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Barcode" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ItemId" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ItemName" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="WarrantyPeriod" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="WarrantyTo" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="CoderLifeTo" Settings-AutoFilterCondition="Contains">
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
                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="SELECT FaultyId,FaultyInWarId,OEM,Pickup,CorrierName,ConsignNo,convert(varchar,ConsignDate,103) 'ConsignDate',RmaNo,convert(varchar,RmaDate,103) 'RmaDate' FROM tbl_FaultyInWarranty"></asp:SqlDataSource>
        
        


             </div>

            <br />
        </div>
    </div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
