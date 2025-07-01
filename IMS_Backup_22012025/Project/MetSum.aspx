<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="MetSum.aspx.cs" Inherits="Project_MetSum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Material Summary Reports</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">From Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpFromDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="font-weight: bold;">To Date :
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

                <br />
                <div align="center">
                    <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2"
                        EnableTheming="True" KeyFieldName="IssueId" Theme="Office2003Blue" Width="100%"
                        EnableCallbackAnimation="True" OnBeforePerformDataSelect="ASPxGridView2_BeforePerformDataSelect">
                        <Columns>
                           <dx:GridViewDataTextColumn FieldName="Status" Caption="Issue Type" ReadOnly="True" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="IssueDate" Caption="Issue Date" ReadOnly="True" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="IssueId" Caption="Issue Id" ReadOnly="True" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Qty" Caption="Qty" ReadOnly="True" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RequisitionPurpose" Caption="Requisition Purpose" ReadOnly="True" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="UserId" Caption="From" ReadOnly="True" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ReUserName" Caption="To" ReadOnly="True" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowSelectSingleRowOnly="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />
                        </SettingsPager>
                        <Settings ShowFilterRow="True" />
                        <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowFilterRow="true" ShowHeaderFilterButton="True" />
                      
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"></asp:SqlDataSource>
                    
                </div>
            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

