<%@ Page Title="Branch Access" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="BranchAccess.aspx.cs" Inherits="Project_BranchAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="true" 
                    DataSourceID="SqlDataSource1" Theme="BlackGlass" KeyFieldName="BranchIds" 
                    OnRowUpdating="ASPxGridView1_RowUpdating" Width="100%">
                    <SettingsEditing Mode="Inline" />
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:Profit %>"></asp:SqlDataSource>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

