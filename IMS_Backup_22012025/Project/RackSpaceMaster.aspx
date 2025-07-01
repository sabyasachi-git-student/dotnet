<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="RackSpaceMaster.aspx.cs" Inherits="Project_RackSpaceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Rack Space Master</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Row : <span style="color: red;">*</span>
                        </td>
                        <td>
                            
                            <dx:ASPxComboBox ID="ddlRow" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="Row" TextField="Row" Theme="Office2003Blue" 
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct Row from tbl_RackSpaceMaster"></asp:SqlDataSource>
                        
                        </td>
                        <td style="font-weight: bold;">Rack :
                        </td>
                        <td>
                           <dx:ASPxComboBox ID="ddlRack" runat="server"
                                DataSourceID="SqlDataSource4" Height="30px" Width="250px"
                                ValueField="Rack" TextField="Rack" Theme="Office2003Blue" 
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct Rack from tbl_RackSpaceMaster"></asp:SqlDataSource>
                        </td>
                        <td style="font-weight: bold;">Shelf :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlShelf" runat="server"
                                DataSourceID="SqlDataSource3" Height="30px" Width="250px"
                                ValueField="Shelf" TextField="Shelf" Theme="Office2003Blue" 
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct Shelf from tbl_RackSpaceMaster"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="6" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" Height="36px" OnClick="btnSave_Click"  CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" Height="36px" OnClick="btnClear_Click" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="ProcessId" OnRowCommand="ASPxGridView1_RowCommand" >
                    <Columns>

                        <dx:GridViewDataTextColumn FieldName="Row" Caption="Row" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Rack" Caption="Rack" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Shelf" Caption="Shelf" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                            
                        <dx:GridViewDataTextColumn Caption="" VisibleIndex="3" Width="0px">
                            <DataItemTemplate>
                                <asp:ImageButton ID="imbEdit" runat="server"
                                    CommandName="Edit"
                                    ImageUrl="../Image/edit.png" Width="20px" Height="20px" CommandArgument='<%#Eval("RowId")%>' ClientIDMode="Static" />
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>

                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" />
                    <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="true" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />

                    </SettingsPager>
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from  [dbo].[tbl_RackSpaceMaster] where BranchId=@BranchId  order by rowid desc">

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
</asp:Content>

