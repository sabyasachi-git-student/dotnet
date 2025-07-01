<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="RegionMaster.aspx.cs" Inherits="Project_RegionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Region Master</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Region Name : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRegionName" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Manager Name :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtManagerName"  Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Phone No :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPhoneNo" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Email ID :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEmailID" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Latitude :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtLatitude" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Longitude :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtLongitude" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>

                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="RegionId" OnRowCommand="ASPxGridView1_RowCommand">
                    <Columns>

                        <dx:GridViewDataTextColumn FieldName="RegionName" Caption="Region Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ManagerName" Caption="Manager Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PhoneNo" Caption="Phone No" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EmailID" Caption="Email ID" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Latitude" Caption="Latitude" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Longitude" Caption="Longitude" ReadOnly="True" VisibleIndex="1">
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from  [dbo].[tbl_RegionMaster]  order by rowid desc"></asp:SqlDataSource>
                <br />
            </div>
            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

