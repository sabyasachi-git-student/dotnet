<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="PageAccessPanel.aspx.cs" Inherits="Project_PageAccessPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 128px;
        }

        .auto-style2 {
            width: 209px;
        }

        .auto-style3 {
            width: 88px;
        }

        .auto-style4 {
            width: 93px;
        }
    </style>
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 79%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server"
        HeaderText="Page Access" Theme="BlackGlass" Width="100%"
        Height="251px">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <div>
                    <table width="99%">
                        <tr>
                            <td class="auto-style1">Branch
                            </td>
                            <td class="auto-style2">
                                <asp:DropDownList ID="ddl_Branch" runat="server" Height="22px" Width="150px"></asp:DropDownList>
                            </td>
                            <td class="auto-style4">Group
                            </td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txt_Group" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style3">Email Id</td>
                            <td>
                                <asp:TextBox ID="Txt_Email" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style1">UserName
                            </td>
                            <td class="auto-style2">
                                <asp:DropDownList ID="ddl_UserName" runat="server" Height="22px" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddl_UserName_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td class="auto-style4">Password
                            </td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txt_Password" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style3">Phone No.</td>
                            <td>
                                <asp:TextBox ID="txt_PhoneNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>


                        <tr>
                            <td class="auto-style1">&nbsp;</td>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style4">&nbsp;</td>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style3">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>


                        <tr>

                            <td colspan="6">


                                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" RenderMode="Lightweight" Width="100%" Theme="BlackGlass" ActiveTabIndex="1" OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged">
                                    <TabPages>
                                        <dx:TabPage Name="PageAccess" Text="Page Access">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

                                                    <table width="100%">
                                                        <tr>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td>Search:
                                                            <asp:TextBox ID="txt_searchContent" runat="server" AutoPostBack="True" OnTextChanged="txt_searchContent_TextChanged" placeHolder="Search Content"></asp:TextBox>
                                                            </td>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="400px" Width="100%">
                                                                        <asp:GridView ID="Grv_Usergrid" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="true" OnRowEditing="Grv_Usergrid_RowEditing">

                                                                            <AlternatingRowStyle BackColor="#DCDCDC" />

                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Content">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_content" runat="server" Text='<%# Eval("ContentName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="PageLink">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_PageLink" runat="server" Text='<%# Eval("ContentLink") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                                <asp:TemplateField HeaderText="ContentType">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_ContentType" runat="server" Text='<%# Eval("ContentType") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>



                                                                                <asp:TemplateField HeaderText="Visible">
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="chkALL" runat="server" Text="Visible" OnCheckedChanged="chkALL_CheckedChanged" AutoPostBack="true" />

                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chk_Permission" runat="server" Text="Active" />
                                                                                        <asp:Label ID="lblstatus" runat="server" Visible="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chk_Visible" runat="server" Text="Update" />
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>


                                                                            </Columns>




                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#252529" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                            <SortedDescendingHeaderStyle BackColor="#000065" />




                                                                        </asp:GridView>

                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                    </table>

                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>


                                        <dx:TabPage Name="Edit" Text="Edit Section">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="400px" Width="100%">
                                                        <asp:GridView ID="Gv_PageEdit" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                                            BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowCommand="Gv_PageEdit_RowCommand" Width="100%" OnRowEditing="Gv_PageEdit_RowEditing">
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="User Id" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_userId" runat="server" Text='<%#Eval("UserInfoId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Branch">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_Branch" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Group">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_userGroup" runat="server" Text='<%#Eval("UserGroup")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="User Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_userName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText=" Password">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_Password" runat="server" Text='<%#Eval("UserPassword")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="EmailId">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_EmailId" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Phone no">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_PhoneNo" runat="server" Text='<%#Eval("Telephone")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btn_PageEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("UserInfoId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                            </Columns>

                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#252529" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#000065" />

                                                        </asp:GridView>

                                                    </asp:Panel>








                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>


                                        <dx:TabPage Name="  " Text="  ">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowCommand="Gv_PageEdit_RowCommand" Width="100%">
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="User Id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_userId" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Branch">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Branch" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Group">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_userGroup" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_userName" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Password">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Password" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="EmailId">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_EmailId" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Phone no">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_PhoneNo" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btn_PageEdit" runat="server" Text="Edit" CommandName="Edit" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>

                                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                        <HeaderStyle BackColor="#0E0E10" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#000065" />

                                                    </asp:GridView>

                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>




                                    </TabPages>





                                </dx:ASPxPageControl>


                            </td>



                        </tr>

                        <tr>

                            <td colspan="6" align="right">

                                <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Submit" />
                                &nbsp;<asp:Button ID="btn_clear" runat="server" Text="Clear" Width="61px" OnClick="btn_clear_Click" />

                            </td>

                        </tr>




                    </table>

                </div>

            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

