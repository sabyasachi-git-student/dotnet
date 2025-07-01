<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="POPMaster.aspx.cs" Inherits="Project_POPMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">PoP Master</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">PoP Name : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="txtPOPName" runat="server" AutoPostBack="true" OnTextChanged="txtPOPName_TextChanged"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="Branchid" TextField="BranchName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct BranchId,BranchName from tbl_Branch"></asp:SqlDataSource>
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
                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="POPId" OnRowCommand="ASPxGridView1_RowCommand">
                    <Columns>

                        <dx:GridViewDataTextColumn FieldName="POPName" Caption="PoP Name" ReadOnly="True" VisibleIndex="1">
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from  [dbo].[tbl_POPMaster]  order by rowid desc"></asp:SqlDataSource>
                <br />
            </div>
            <br />
        
    <div align="center">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnXlsExport" runat="server" OnClick="btnXlsExport_Click" Text="Export to XLS" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="btnXlsxExport" runat="server" OnClick="btnXlsxExport_Click" Text="Export to XLSX" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
                        </td>
                    </tr>
                </table>
            </div><br />
            </div>
    
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

