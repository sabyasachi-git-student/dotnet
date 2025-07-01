<%@ Page Title="Add User" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true"
    CodeFile="AddUser.aspx.cs" Inherits="Project_AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
               
                <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" FullscreenMode="True" 
                    Height="100%" Width="100%" EnableTheming="True" Theme="RedWine">
                    <Panes>
                        <dx:SplitterPane AllowResize="True" ScrollBars="Auto">
                            <ContentCollection>
                                <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxPanel ID="ASPxPanel2" runat="server" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="cp"
                                                    OnCallback="ASPxCallbackPanel1_Callback" Theme="RedWine" Width="100%" 
                                                    EnableDefaultAppearance="False" Height="100%" LoadingPanelText="" 
                                                    ShowLoadingPanel="False" ShowLoadingPanelImage="False">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxGridView ID="ASPxGridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                                                                EnableTheming="True" KeyFieldName="UserGroupId" OnCommandButtonInitialize="ASPxGridView3_CommandButtonInitialize"
                                                                OnRowDeleting="ASPxGridView3_RowDeleting" OnRowInserting="ASPxGridView3_RowInserting"
                                                                Theme="BlackGlass" Width="100%">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn FieldName="UserGroupId" ShowInCustomizationForm="True"
                                                                        Visible="False" VisibleIndex="0">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="UserGroup" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewCommandColumn ShowNewButton="true" ShowDeleteButton="true" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                        <%--<NewButton Visible="True">
                                                                        </NewButton>
                                                                        <DeleteButton Visible="True">
                                                                        </DeleteButton>--%>
                                                                    </dx:GridViewCommandColumn>
                                                                </Columns>
                                                                <Settings ShowFilterRow="True" ShowGroupPanel="True" />

<Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>
                                                            </dx:ASPxGridView>
                                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                                                DeleteCommand="Delete [tbl_UserGroups] where [UserGroupId]=@UserGroupId" SelectCommand="SELECT [UserGroupId],[UserGroup] FROM [tbl_UserGroups] Where [UserGroupId]!='I'">
                                                                <DeleteParameters>
                                                                    <asp:Parameter Name="UserGroupId" />
                                                                </DeleteParameters>
                                                            </asp:SqlDataSource>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxCallbackPanel>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxPanel>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane AllowResize="True" ScrollBars="Auto">
                            <ContentCollection>
                                <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxPanel ID="ASPxPanel3" runat="server" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2"
                                                    EnableTheming="True" Theme="BlackGlass" Width="100%" KeyFieldName="UserName"
                                                    OnRowDeleting="ASPxGridView2_RowDeleting" OnRowInserting="ASPxGridView2_RowInserting"
                                                    OnRowUpdating="ASPxGridView2_RowUpdating" OnCellEditorInitialize="ASPxGridView2_CellEditorInitialize">
                                                    <ClientSideEvents EndCallback="function(s, e) {
	cp.PerformCallback();
}" />
<ClientSideEvents EndCallback="function(s, e) {
	cp.PerformCallback();
}"></ClientSideEvents>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="FullName" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="UserName" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Password" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="PhoneNumber" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn FieldName="UserGroup" ShowInCustomizationForm="True"
                                                            VisibleIndex="2">
                                                            <PropertiesComboBox DataSourceID="SqlDataSource3" TextField="UserGroup" ValueField="UserGroupId"
                                                                IncrementalFilteringMode="Contains">
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>

                                                        <dx:GridViewDataTextColumn FieldName="MailId" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>

                                                         <dx:GridViewDataComboBoxColumn FieldName="BranchId"  Caption="Location"  ShowInCustomizationForm="True"
                                                            VisibleIndex="4">
                                                            <PropertiesComboBox DataSourceID="SqlDataSource4" TextField="BranchName" ValueField="BranchId"
                                                                IncrementalFilteringMode="Contains">
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>

                                                        <dx:GridViewDataComboBoxColumn FieldName="StateName"  Caption="State"  ShowInCustomizationForm="True"
                                                            VisibleIndex="5">
                                                            <PropertiesComboBox DataSourceID="SqlDataSource5" TextField="StateName" ValueField="StateCode"
                                                                IncrementalFilteringMode="Contains">
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>

                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowEditButton="true" ShowNewButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" VisibleIndex="5">
                                                         <%--   <EditButton Visible="True">
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                            </DeleteButton>
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>--%>
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />

                                                <Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>
                                                </dx:ASPxGridView>

                                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                                    DeleteCommand="Delete [tbl_User] where UserName=@UserName" InsertCommand="Insert Into [tbl_User](UserName,Password,UserGroup,MailId,BranchId,FullName,PhoneNumber,StateName) values(@UserName,@Password,@UserGroup,@MailId,@BranchId,@FullName,@PhoneNumber,@StateName)"
                                                    SelectCommand="SELECT [UserName], [Password], [UserGroup],[MailId],BranchId,FullName,PhoneNumber,StateName FROM [tbl_User]" UpdateCommand="Update [tbl_User] set Password=@Password,UserGroup=@UserGroup,MailId=@MailId,BranchId=@BranchId,FullName=@FullName,PhoneNumber=@PhoneNumber, StateName=@StateName Where UserName=@UserName">
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="UserName" />
                                                    </DeleteParameters>
                                                    <InsertParameters>
                                                        <asp:Parameter Name="UserName" />
                                                        <asp:Parameter Name="UserGroup" />
                                                        <asp:Parameter Name="Password" />
                                                        <asp:Parameter Name="MailId" />
                                                        <asp:Parameter Name="BranchId" />
                                                        <asp:Parameter Name="FullName" />
                                                        <asp:Parameter Name="PhoneNumber" />
                                                        <asp:Parameter Name="StateName" />

                                                    </InsertParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Name="Password" />
                                                        <asp:Parameter Name="UserGroup" />
                                                        <asp:Parameter Name="UserName" />
                                                        <asp:Parameter Name="MailId" />
                                                        <asp:Parameter Name="BranchId" />
                                                        <asp:Parameter Name="FullName" />
                                                        <asp:Parameter Name="PhoneNumber" />
                                                        <asp:Parameter Name="StateName" />

                                                    </UpdateParameters>
                                                </asp:SqlDataSource>

                                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                                    SelectCommand="SELECT [UserGroup], [UserGroupId] FROM [tbl_UserGroups]"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                                    SelectCommand=" SELECT BranchId, BranchName FROM [dbo].[tbl_Branch]"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"
                                                    SelectCommand="select * from tbl_StateMaster"></asp:SqlDataSource>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxPanel>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                </dx:ASPxSplitter>
                <br />
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
