<%@ Page Title="Content Assignment" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="ContentAssignment.aspx.cs" Inherits="Project_ContentAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Width="100%">
    <dx:ASPxTreeList runat="server" ID="ASPxTreeList1" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" EnableTheming="True" KeyFieldName="ContentID" 
            onnodeinserting="ASPxTreeList1_NodeInserting" ParentFieldName="ParentContentID" 
            Theme="BlackGlass"
            
            oncommandcolumnbuttoninitialize="ASPxTreeList1_CommandColumnButtonInitialize" 
            onnodeupdating="ASPxTreeList1_NodeUpdating" 
            onnodedeleting="ASPxTreeList1_NodeDeleting" Width="100%">
        <Columns>
            <dx:TreeListTextColumn FieldName="ContentID" Visible="False" VisibleIndex="0">
                <EditFormSettings Visible="False" />
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="ParentContentID" ReadOnly="True" 
                Visible="False" VisibleIndex="1">
                <EditFormSettings Visible="False" />
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="ContentName" VisibleIndex="2">
            </dx:TreeListTextColumn>
            <dx:TreeListComboBoxColumn FieldName="ContentType" VisibleIndex="3">
            </dx:TreeListComboBoxColumn>
            <dx:TreeListTextColumn FieldName="ContentLink" VisibleIndex="4">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="ContentVisibility" VisibleIndex="5">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="ContentPosition" VisibleIndex="6">
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
            SelectCommand="SELECT [ContentID], [ParentContentID], [ContentName], [ContentType], [ContentLink],Case When [ContentVisibility]='V' Then 'Visible' Else 'Invisible' End as [ContentVisibility],[ContentPosition] FROM [tbl_Contents] order by ContentPosition" 
            
            UpdateCommand="UPDATE tbl_Contents SET ContentName = @ContentName, ContentType = @ContentType, ContentLink = @ContentLink, ContentVisibility=@ContentVisibility, ParentContentID = @ParentContentID, ContentPosition =@ContentPosition WHERE (ContentID = @ContentID)" >
            <UpdateParameters>
                <asp:Parameter Name="ContentName" />
                <asp:Parameter Name="ContentType" />
                <asp:Parameter Name="ContentLink" />
                <asp:Parameter Name="ContentVisibility" />
                <asp:Parameter Name="ParentContentID" />
                <asp:Parameter Name="ContentPosition" />
                <asp:Parameter Name="ContentID" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

