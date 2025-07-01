<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="FaultyItemReceice.aspx.cs" Inherits="Project_FaultyItemReceice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Faulty Item Receive</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Faulty Id : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlFaultyId" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="FaultyId" TextField="FaultyId" Theme="Office2003Blue" AutoPostBack="true" OnTextChanged="ddlFaultyId_TextChanged"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:Label runat="server" ID="lbl" Visible="false"></asp:Label>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct FaultyId from tbl_FaultyInWarranty where BranchId=@BranchId and FaultyId not in (select FaultyId from tbl_FaultyItemReceice) union all select distinct FaultyId from tbl_FaultyWarrantyExpired where BranchId=@BranchId and FaultyId not in (select FaultyId from tbl_FaultyItemReceice)">
                                
                                 <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>

                            </asp:SqlDataSource>
                        </td>
                        <td style="font-weight: bold;">Receive Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                            
                        </td>

                    </tr>

                    <tr>
                        <td style="font-weight: bold;">RMA Number :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRMANumber" Enabled="false" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">RMA Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpRMADate" Enabled="false" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Consignment No : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtConsignmentNo" Enabled="false" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Consignment Date : </td>
                        <td>
                           <dx:ASPxDateEdit ID="dtpConsignmentDate" Enabled="false" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">OEM Name : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOEMName" Enabled="false" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Remarks : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Style="resize: none" Height="60px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>

                </table>

                <div align="center" style="overflow: auto; width: 900px;">
                    <asp:GridView ID="gvFaultyItemDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" PageSize="100" >

                        <Columns>

                            <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                            <asp:BoundField DataField="WarrantyPeriod" HeaderText="Warranty Period" />
                            <asp:BoundField DataField="WarrantyTo1" HeaderText="WarrantyTo" />
                            <asp:BoundField DataField="CoderLifeTo1" HeaderText="CoderLifeTo" />
                             <asp:BoundField DataField="Staus" HeaderText="CoderLife" />
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="dropd" OnTextChanged="dropd_TextChanged" AutoPostBack="true">
                                        <asp:ListItem Text="--Select Status--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Replace" Value="Replace"></asp:ListItem>
                                        <asp:ListItem Text="Repair" Value="Repair"></asp:ListItem>
                                        <asp:ListItem Text="Non-Repair" Value="Non-Repair"></asp:ListItem>
                                        <%--<asp:ListItem Text="Scrap" Value="Scrap"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtNewBarcode" Width="100px" OnTextChanged="txtNewBarcode_TextChanged" AutoPostBack="true" Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <FooterStyle ForeColor="Black" BackColor="#7BA4E0" />
                        <RowStyle HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#7BA4E0" Font-Bold="True" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />

                    </asp:GridView>

                    <br />

                </div>

                <table>
                    <tr>

                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div>

            <br />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

