<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="UserBranchMapping.aspx.cs" Inherits="Project_UserBranchMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">User & Branch Mapping</h3>
            </div>
            <br />
            
            <div align="center">
                <table cellspacing="15px" align="center">
                   <tr>
                       <td style="font-weight:bold;">
                           Username :
                       </td>
                       <td>
                           <asp:TextBox runat="server" Enabled="false" ID="txtUsername" Height="30px" Width="250px"></asp:TextBox>
                       </td>
                       <td>
                           <asp:Button runat="server" ID="btnAdd" Text="Add User" OnClick="btnAdd_Click"  CssClass="w3-btn w3-blue"/>
                       </td>
                   </tr>
                   <tr>
                       <td style="font-weight:bold;">
                           User Group :
                       </td>
                       <td>
                           <asp:TextBox runat="server" Enabled="false" ID="txtUserGroup"  Height="30px" Width="250px"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td style="font-weight:bold;">
                           Branch :
                       </td>
                        <td>
                           <asp:DropDownList runat="server" ID="ddlBranch"   Height="30px" Width="250px" AppendDataBoundItems="true">
                               <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                           </asp:DropDownList>
                       </td>
                   </tr>
                    
                    <tr>

                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save"  OnClick="btnSave_Click"  CssClass="w3-btn w3-green" Height="36px"/>
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click"  Height="36px" CssClass="w3-btn w3-orange"/>
                        </td>
                    </tr>
                </table>
                </div>
            <div align="center">
                    <dx:ASPxGridView ID="ASPxGridView2" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="ID" OnRowCommand="ASPxGridView2_RowCommand">
                         <Columns>
                        <dx:GridViewDataTextColumn Caption="ADD" VisibleIndex="0" Width="0px"  Visible="false">
                            <DataItemTemplate>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CommandArgument='<%#Eval("ID")%>' CommandName="Delete" />
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn FieldName="UserId" Caption="Username" ReadOnly="True" Settings-AutoFilterCondition="Contains" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="UserGroup" Caption="User Group" ReadOnly="True" Settings-AutoFilterCondition="Contains" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name" ReadOnly="True" Settings-AutoFilterCondition="Contains" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>                   
                        <dx:GridViewDataTextColumn FieldName="BranchName" Caption="BranchName" ReadOnly="True" Settings-AutoFilterCondition="Contains" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>  
                    </Columns>
                        <SettingsBehavior AllowSelectSingleRowOnly="True" />
                        <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="true" ShowHeaderFilterButton="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />

                        </SettingsPager>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select (m.UserId+','+m.BranchId) as 'ID',m.UserId,b.BranchName,u.FullName,g.UserGroup,m.BranchId from dbo.tbl_UserBranchMapping m
                join dbo.tbl_Branch b on b.BranchId=m.BranchId
                join dbo.tbl_User u on u.UserName=m.UserId
                join dbo.tbl_UserGroups g on g.UserGroupId=u.UserGroup"></asp:SqlDataSource>
                <br />
                </div>
            <br />            
            </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <div class="w3-container">
        <div id="id03" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('id03').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">User Details</h3>
                    </header>
                </div>
                <br />
                <br />

                <div align="center">
                    <br />
                    <dx:ASPxGridView ID="ASPxGridView1" Width="100%" Theme="Office2003Blue" EnableRowsCache="true" SettingsPager-PageSize="10" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" KeyFieldName="ID" OnRowCommand="ASPxGridView1_RowCommand">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="ADD" VisibleIndex="0" Width="0px">
                            <DataItemTemplate>
                                <asp:Button runat="server" ID="btnEnquiryId" Text="ADD" CommandArgument='<%#Eval("UserName")%>' CommandName="ADD" />
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="UserName" Caption="User Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>                       
                        <dx:GridViewDataTextColumn FieldName="UserGroup" Caption="User Group" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>                        
                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Full Name" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>  
                        <dx:GridViewDataTextColumn FieldName="PhoneNumber" Caption="Phone Number" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>    
                        <dx:GridViewDataTextColumn FieldName="MailId" Caption="Mail Id" ReadOnly="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>  
                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" />
                    <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="true" ShowHeaderFilterButton="True" />
                    <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                        <PageSizeItemSettings Items="50, 100" />
                    </SettingsPager>
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select (u.UserName+','+g.UserGroup) as 'ID',g.UserGroup,b.BranchName,u.FullName,u.MailId,u.PhoneNumber,u.UserName from tbl_User u
                    join dbo.tbl_UserGroups g on g.UserGroupId=u.UserGroup
                    join dbo.tbl_Branch b on b.BranchId=u.BranchId"> 
                </asp:SqlDataSource>

                    <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

