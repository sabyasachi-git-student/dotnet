<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="HSNMaster.aspx.cs" Inherits="Project_HSNMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">HSN/SAC Code Master</h3>
            </div>
            <br />
            
            <div align="center">
                <table cellspacing="15px" align="center">
                   <tr>
                        <td style="font-weight: bold;" >HSN / SAC : <span style="color: red;">*</span>
                        </td>
                       <td>
                            <asp:TextBox runat="server" ID="txtHSN" Height="30px" Width="150px" ></asp:TextBox>
                        </td>
                         <td style="font-weight: bold;">IGST(%) :
                        </td>
                        <td >
                              <asp:TextBox runat="server" ID="ddlIGST" AutoPostBack="true" OnTextChanged="ddlIGST_TextChanged" Height="30px" Width="150px"></asp:TextBox>
                           
                        </td>
                        
                    </tr>
                   
                    <tr>
                         <td style="font-weight: bold;">CGST(%) :
                        </td>
                        <td >
                            <asp:TextBox runat="server" ID="ddlCGST" Height="30px" Width="150px" ></asp:TextBox>
                           

                        </td>
                         <td style="font-weight: bold;">SGST(%) :
                        </td>
                        <td >
                            <asp:TextBox runat="server" ID="ddlSGST" Height="30px" Width="150px" ></asp:TextBox>
                         

                        </td>
                       
                        
                       
                    </tr>
                    
                    <tr>

                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange"/>
                        </td>
                    </tr>
                </table>
                </div>
            <div align="center">
                    <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="RowId" OnRowCommand="ASPxGridView1_RowCommand">
                        <Columns>

                            <dx:GridViewDataTextColumn FieldName="HSNCode" Caption="HSN/SAC" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn FieldName="CGST" Caption="CGST(%)" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SGST" Caption="SGST(%)" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="IGST" Caption="IGST(%)" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CESS" Caption="CESS(%)" ReadOnly="True" VisibleIndex="1">
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
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from  [dbo].[tbl_HSNCodeMaster] order by rowid desc"></asp:SqlDataSource>
                <br />
                </div>
            <br />            
            </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

