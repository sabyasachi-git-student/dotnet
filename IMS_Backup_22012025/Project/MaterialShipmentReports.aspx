<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="MaterialShipmentReports.aspx.cs" Inherits="Project_MaterialShipmentReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Material Shipment Reports</h3>
            </div>
            <br />
          
                
            <div align="center">
                    <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="MetShipId" OnRowCommand="ASPxGridView1_RowCommand">
                        <Columns>

                            <dx:GridViewDataTextColumn FieldName="MetShipId" Caption="MetShip Id" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ReqId" Caption="Requsition Id" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="IssueId" Caption="Issue Id" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="IssueDate" Caption="Shipment Date" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CourierName" Caption="Courier Name" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CourierDate" Caption="Courier Date" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ConsignmentNumber" Caption="Consignment Number" HeaderStyle-Wrap="True" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" CellStyle-Wrap="True" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="" VisibleIndex="3" Width="0px">
                                <DataItemTemplate>
                                    <asp:ImageButton ID="imbEdit" runat="server"
                                        CommandName="View"
                                        ImageUrl="../Images/view.png" Width="20px" Height="20px" CommandArgument='<%#Eval("MetShipId")%>' ClientIDMode="Static" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="Update" VisibleIndex="3" Width="0px">
                                <DataItemTemplate>
                                   <asp:Button runat="server" ID="btnGatePass" CommandName="Update" Text="Update" CssClass="w3-btn w3-red" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="Gate Pass" VisibleIndex="3" Width="0px">
                                <DataItemTemplate>
                                   <asp:Button runat="server" ID="btnGatePass" CommandName="GatePass" Text="Gate Pass" CssClass="w3-btn w3-blue" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="Declaration" VisibleIndex="3" Width="0px">
                                <DataItemTemplate>
                                   <asp:Button runat="server" ID="btnDeceleration" CommandName="Deceleration" Text="Declaration" CssClass="w3-btn w3-orange" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>

                        </Columns>
                        <SettingsBehavior AllowSelectSingleRowOnly="True" />
                        <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="true" ShowHeaderFilterButton="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />

                        </SettingsPager>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select a.MetShipId, a.IssueId, Convert(varchar,a.IssueDate,103) as IssueDate, a.CourierName,Convert(varchar,a.IssueDate,103) as CourierDate, a.ConsignmentNumber, a.Remarks, b.ReqId from tbl_MaterialShipment a join tbl_MaterialIssue b on a.IssueId=b.IssueId  where b.BranchId=@BranchId order by a.rowid desc">
                        <SelectParameters>
                        <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                       
                    </SelectParameters>
                    </asp:SqlDataSource>
                <br />
                </div>
            <br />            
            </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div id='Group4' class="w3-modal"">
        <div class="w3-modal-content w3-animate-top w3-card-4"  style="width:369px">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('Group4').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Documents</h3>
                    </header>
                </div>
            <br/>
            <br/>
                <div  align="center">  
                <table>

                <asp:GridView ID="gvDocuments" CssClass="ChildGrid" runat="server"  AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href='<%# Eval("Images") %>' target="_blank">
                                    <img src='<%# Eval("Images") %>' width="120px" height="100px" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                    </Columns>
                </asp:GridView>
                <br /><br />
                </table>
                      </div>
            </div>
         </div>

</asp:Content>

