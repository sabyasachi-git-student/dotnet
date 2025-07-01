<%@ Page Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="ProjectMaster.aspx.cs" Inherits="Project_ProjectMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Project Master</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Project Name :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtProjectName" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Project Description :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtProjectDescription" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Project Incharge Name :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtProjectInName" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Mobile No :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMobileNo" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>


                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" Height="36px"  OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                    
                </table>
            </div>
            <div align="center" style="overflow:auto;width:1100px;">
                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="ProjectId" OnRowCommand="ASPxGridView1_RowCommand" style="overflow:scroll;">
                    <Columns>

                        <dx:GridViewDataTextColumn FieldName="ProjectName" Caption="Project Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ProjectDescription" Caption="Project Description" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="ProjectInName" Caption="Project Incharge Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No" ReadOnly="True" VisibleIndex="1">
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from  [dbo].[tbl_ProjectMaster] order by rowid desc"></asp:SqlDataSource>
                <br />
            </div>
            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
