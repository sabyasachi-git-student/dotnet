<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="LatLongItemReport.aspx.cs" Inherits="Project_LatLongItemReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Item Serch</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Location :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlBranch" runat="server"
                                DataSourceID="SqlDataSource1" Height="30px" Width="250px"
                                ValueField="BranchId" TextField="BranchName" Theme="Office2003Blue" AutoPostBack="true" OnTextChanged="ddlBranch_TextChanged"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct BranchId, BranchName from tbl_Branch">
                                <SelectParameters>
                                    <%-- <asp:SessionParameter SessionField="BranchId" Name="BranchId" />--%>
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLat" runat="server" Width="250px" Height="30px" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLong" runat="server" Width="250px" Height="30px" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlItem" runat="server"
                                DataSourceID="SqlDataSource4" Height="30px" Width="250px" TextFormatString="{0}"
                                ValueField="ItemId" TextField="ItemName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10">
                                <Columns>
                                    <dx:ListBoxColumn Caption="ItemId" FieldName="ItemId" />
                                    <dx:ListBoxColumn Caption="ItemName" FieldName="ItemName" />
                                    <dx:ListBoxColumn Caption="Make" FieldName="Make" />
                                    <dx:ListBoxColumn Caption="Model" FieldName="Model" />

                                </Columns>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select * from tbl_ItemMaster">
                                <%--<SelectParameters> 
                                    <asp:SessionParameter SessionField="StockInId" Name="StockInId" />
                                </SelectParameters>--%>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
                <div align="center">

                    <asp:Label runat="server" ID="lblError11" Style="font-size: large; color: red; font-weight: bold;"></asp:Label><br />

                    <asp:GridView ID="gv_Barc" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Available Branch">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtProcessName" runat="server" Enabled="false" Text='<%#Eval("BranchName") %>' Width="100px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemId">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtProcessName" runat="server" Enabled="false" Text='<%#Eval("ItemId") %>' Width="100px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemName">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEstimationId" runat="server" Enabled="false" Text='<%#Eval("ItemName") %>' Width="100px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Make">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEstimationDate" runat="server" Enabled="false" Text='<%#Eval("Make") %>' Width="100px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Model">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEndDate" runat="server" Enabled="false" Text='<%#Eval("Model") %>' Width="100px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtWorkCatagory" runat="server" Enabled="false" Text='<%#Eval("Qty") %>' Width="100px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="distance">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtdistance" runat="server" Enabled="false" Text='<%#Eval("distance") %>' Width="100px"></asp:TextBox>
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
                    <tr>

                        <td colspan="6" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Serch" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>

                </table>

                <table>
                </table>
            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

