<%@ Page Title="Branch Assignment" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="BranchAssignment.aspx.cs" Inherits="Project_BranchAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="Panel1" runat="server" Width="100%">
    <dx:ASPxTreeList runat="server" ID="ASPxTreeList1" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" EnableTheming="True" Width="100%" 
        KeyFieldName="BranchId" ParentFieldName="ParentId" Theme="BlackGlass" 
        oncommandcolumnbuttoninitialize="ASPxTreeList1_CommandColumnButtonInitialize" 
        onnodedeleting="ASPxTreeList1_NodeDeleting" 
        onnodeinserting="ASPxTreeList1_NodeInserting" 
        onnodeupdating="ASPxTreeList1_NodeUpdating">
        
        <Columns>
            <dx:TreeListTextColumn FieldName="BranchId" VisibleIndex="0" Visible="false">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="BranchName" VisibleIndex="1">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="BranchAddress" VisibleIndex="2">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="ContactPerson" VisibleIndex="3">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="ContactNo" VisibleIndex="4">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="Priority" VisibleIndex="5">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="ParentId" VisibleIndex="6" Visible="false">
            </dx:TreeListTextColumn>
            <dx:TreeListCommandColumn VisibleIndex="7">
                <EditButton Visible="True">
                </EditButton>
                <NewButton Visible="True">
                </NewButton>
                <DeleteButton Visible="True">
                </DeleteButton>
            </dx:TreeListCommandColumn>
        </Columns>
        
        <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" 
            ExpandCollapseAction="NodeDblClick" />
        <SettingsEditing AllowNodeDragDrop="True" Mode="EditForm" />
    </dx:ASPxTreeList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Profit %>" 
            
        
        SelectCommand="SELECT [BranchId], [BranchName], [BranchAddress], [ContactPerson], [ContactNo], [Priority], [ParentId] FROM [tbl_Branch]" 
        UpdateCommand="Update [tbl_Branch] set  [BranchName]=@BranchName, [BranchAddress]=@BranchAddress, [ContactPerson]=@ContactPerson, [ContactNo]=@ContactNo, [Priority]=@Priority, [ParentId] =@ParentId Where [BranchId]=@BranchId" >
            <UpdateParameters>
                <asp:Parameter Name="BranchName" />
                <asp:Parameter Name="BranchAddress" />
                <asp:Parameter Name="ContactPerson" />
                <asp:Parameter Name="ContactNo" />
                <asp:Parameter Name="Priority" />
                <asp:Parameter Name="ParentId" />
                <asp:Parameter Name="BranchId" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

