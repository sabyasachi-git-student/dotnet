<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="Scrap.aspx.cs" Inherits="Project_Scrap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Scrap Assign</h3>
            </div>
            <br />
            <div align="center">
                <table>
                   
                    <tr>
                        <td >
                             <asp:TextBox ID="txtSearch" runat="server" placeholder="Enter Item Name ..." Width="250px" Height="30px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button runat="server"  ID="btnSearch" OnClick="btnSearch_Click" Text="Item Name Serch" CssClass="w3-btn w3-red"/>
                        </td>
                        <td style="font-weight: bold;">Scrap Assign Date :<span style="color: red;">*</span></td>
                        <td>
                            <dx:ASPxDateEdit ID="dttAnniversaryDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            <asp:Button runat="server" Text="Scrap Assign" Font-Bold="true" ID="btnSave" CssClass="w3-btn w3-green" Width="118px" OnClick="btnSave_Click" />
                <asp:Button runat="server" Text="Clear" Font-Bold="true" ID="Button2"  CssClass="w3-btn w3-orange" OnClick="btnClear_Click" Width="76px" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />

            <div align="center"  style="width: 1100px; overflow: auto;">

                <asp:GridView ID="grdStockAudit" runat="server" AutoGenerateColumns="False" BackColor="white" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" ShowFooter="True" DataKeyNames="StockInId" OnRowDataBound="grdStockAudit_RowDataBound">
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
                                <asp:TextBox ID="txtWorkGroup1" runat="server" Enabled="false" Text='<%#Eval("Warreanty") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CoderLife">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWorkGroup2" runat="server" Enabled="false" Text='<%#Eval("CoderLife") %>' Width="100px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle ForeColor="Black" HorizontalAlign="Center" Font-Bold="true" BackColor="#7BA4E0" />
                    <RowStyle HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#7BA4E0" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                </asp:GridView>

            </div>

            <br />

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

