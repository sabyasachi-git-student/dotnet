<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="HigherKeyTag.aspx.cs" Inherits="Project_HigherKeyTag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Hierarchy Mapping</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Region : </td>
                        <td>

                            <dx:ASPxComboBox ID="ddlRegion" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="BranchId" TextField="BranchName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct BranchId, BranchName from tbl_Branch where BranchName='Panihati'"></asp:SqlDataSource>

                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Territory :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlTerrytory" runat="server"
                                DataSourceID="SqlDataSource4" Height="30px" Width="250px"
                                ValueField="BranchId" TextField="TerrytoryName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct a.TerrytoryName, b.BranchId from tbl_TerrytoryMaster a join tbl_Branch b on b.BranchName=a.TerrytoryName"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Section :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlSection" runat="server"
                                DataSourceID="SqlDataSource3" Height="30px" Width="250px"
                                ValueField="BranchId" TextField="SectionName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct a.SectionName, b.BranchId from tbl_SectionMaster a join tbl_Branch b on b.BranchName=a.SectionName"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">PoP :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlPOP" runat="server"
                                DataSourceID="SqlDataSource5" Height="30px" Width="250px"
                                ValueField="BranchId" TextField="POPName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource5" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct a.POPName, b.BranchId from tbl_PopMaster a join tbl_Branch b on b.BranchName=a.POPName"></asp:SqlDataSource>
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
                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="MapId">
                    <Columns>

                        <dx:GridViewDataTextColumn FieldName="RegionId" Caption="Region Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TerrytoryId" Caption="Territory Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SectionId" Caption="Section Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="POPId" Caption="PoP Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="" VisibleIndex="3" Width="0px" Visible="false">
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="
select h.* from tbl_HigherKeyMapping h
order by h.rowid desc"></asp:SqlDataSource>
                <br />
            </div>
            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

