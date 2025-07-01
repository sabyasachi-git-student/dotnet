<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="ScrapMaster.aspx.cs" Inherits="Project_ScrapMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Scrap Master</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Group Name : <span style="color: red;">*</span>
                        </td>
                        <td>
                            
                            <dx:ASPxComboBox ID="ddlGroupName" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="GroupName" TextField="GroupName" Theme="Office2003Blue" 
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct GroupName from tbl_ScrapGroupMaster"></asp:SqlDataSource>
                        
                        </td>
                        <td style="font-weight: bold;">Item Name :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtItemName" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Unit :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlUnit" runat="server"
                                DataSourceID="SqlDataSource3" Height="30px" Width="250px"
                                ValueField="Unit" TextField="Unit" Theme="Office2003Blue" 
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct Unit from tbl_ScrapMaster"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="6" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" Height="36px" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" Height="36px" OnClick="btnClear_Click" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="ScrapId" OnRowCommand="ASPxGridView1_RowCommand" >
                    <Columns>

                        <dx:GridViewDataTextColumn FieldName="GroupName" Caption="Group Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Unit" Caption="Unit" ReadOnly="True" VisibleIndex="1">
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from  [dbo].[tbl_ScrapMaster]  order by rowid desc"></asp:SqlDataSource>
                <br />
            </div>
            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

