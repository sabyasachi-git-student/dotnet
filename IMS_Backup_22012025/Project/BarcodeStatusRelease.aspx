<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="BarcodeStatusRelease.aspx.cs" Inherits="Project_BarcodeStatusRelease" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Material Release</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr> <td>
                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Enter Item Name ..." Width="250px" Height="30px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Item Name Serch" CssClass="w3-btn w3-red" />
                        </td>
                        <td style="font-weight: bold;">Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>

                        <td style="font-weight: bold;">Reason :
                        </td>
                        <td>
                            <asp:TextBox  ID="ddlProject" runat="server" Height="30px" Width="250px"></asp:TextBox>
                            <%--<dx:ASPxComboBox
                                DataSourceID="SqlDataSource1" Height="30px" Width="250px"
                                ValueField="ProjectId" TextField="ProjectName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct ProjectId, ProjectName from tbl_ProjectMaster">
                                <SelectParameters>
                                   <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>
                            </asp:SqlDataSource>--%>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="6" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Release" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                   
                </table>
                <div align="center">
                  
                    <asp:Label runat="server" ID="lblError11" Style="font-size: large; color: red; font-weight: bold;"></asp:Label><br />
                  
                    <asp:GridView ID="gv_Barc" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_Barc_RowCommand" OnRowDeleting="gv_Barc_RowDeleting" OnRowEditing="gv_Barc_RowEditing" OnPageIndexChanging="gv_Barc_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Action">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkALL" runat="server" Text="ALL" AutoPostBack="true" OnCheckedChanged="chkALL_CheckedChanged" />

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                                <asp:Label ID="lblstatus" runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StockInId">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEstimationId" runat="server" Enabled="false" Text='<%#Eval("StockInId") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ItemId">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEstimationDate" runat="server" Enabled="false" Text='<%#Eval("ItemId") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ItemName">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEntryDate" runat="server" Enabled="false" Text='<%#Eval("ItemName") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Make">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEndDate" runat="server" Enabled="false" Text='<%#Eval("Make") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Model">
                            <ItemTemplate>
                                <asp:TextBox ID="txtProcessName" runat="server" Enabled="false" Text='<%#Eval("Model") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SrBarVodeId">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWorkCatagory" runat="server" Enabled="false" Text='<%#Eval("SrBarVodeId") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Barcode">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWorkGroup" runat="server" Enabled="false" Text='<%#Eval("Barcode") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Warreanty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWorkGroup1" runat="server" Enabled="false" Text='<%#Eval("WarrantyDate") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CoderLife">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWorkGroup2" runat="server" Enabled="false" Text='<%#Eval("CoderLifeDate") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Blue" />
                        <HeaderStyle BackColor="blue" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" Height="5px" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>

                    <br />

                </div>
                <table>
                </table>
            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

