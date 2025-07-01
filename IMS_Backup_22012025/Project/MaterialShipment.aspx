<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="MaterialShipment.aspx.cs" Inherits="Project_MaterialShipment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Material Shipment</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Issue Id : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlIssueId" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="Issueid" TextField="Issueid" Theme="Office2003Blue" AutoPostBack="true" OnTextChanged="ddlIssueId_TextChanged"  TextFormatString="{0}"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            <Columns>
                                    <dx:ListBoxColumn Caption="Issueid" FieldName="Issueid" />
                                    <dx:ListBoxColumn Caption="Location" FieldName="ReUserName" />
                                </Columns>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct Issueid, ReUserName from tbl_MaterialIssue where Issueid not in (select Issueid from tbl_MaterialShipment) and UserId=@UserId and BranchId=@BranchId Union  select distinct TemIssueId as Issueid, ReUserName from tbl_TemporaryStockIssue where TemIssueId not in (select Issueid from tbl_MaterialShipment) and UserId=@UserId and BranchId=@BranchId">
                               <SelectParameters>
                        <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                       <asp:SessionParameter Name="UserId" SessionField="UserId" />
                    </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td style="font-weight: bold;">Shipment Date:
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>

                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Mode Of Shipment :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlCourierName" runat="server"
                                DataSourceID="SqlDataSource3" Height="30px" Width="250px"
                                ValueField="CourierName" TextField="CourierName" Theme="Office2003Blue" OnTextChanged="ddlCourierName_TextChanged" AutoPostBack="true"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct CourierName from tbl_CourierName "></asp:SqlDataSource>
                        </td>
                        <td style="font-weight: bold;">Consignment Number : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtConsignmentNumber" Enabled="false"  OnTextChanged="txtConsignmentNumber_TextChanged" AutoPostBack="true" Height="30px" Width="250px"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Person Name : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPersonName" Enabled="false" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Contact No : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtContactNo" Height="30px" Enabled="false" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="font-weight: bold;">Bus Route : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtBusRoute" Enabled="false" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Bus Registration <br /> Number: </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtBusReg" Height="30px" Enabled="false" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Train / Transporter Name : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTrainName" Height="30px" Enabled="false" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Train / Truck No : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTrainNo" Height="30px" Enabled="false" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Coach No : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCoachNo" Height="30px" Enabled="false" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Remarks : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Style="resize: none" Height="60px" Width="250px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="font-weight: bold;">File Upload : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:FileUpload runat="server" accept=".png,.jpg,.jpeg,.pdf" ID="FileUpload1" />
                        </td>
                    </tr>

                    <tr style="visibility: hidden">
                        <td style="font-weight: bold;">Courier Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpCourierDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                </table>
                <div align="center">


                    <asp:GridView ID="gvItemDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" PageSize="100">

                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="25px" Visible="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="ItemId" HeaderText="Item Id" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                             <asp:BoundField DataField="WarrantyPeriod" HeaderText="Power Supply" />
                            <asp:BoundField DataField="ReqToQty" HeaderText="ReqToQty" Visible="false" />
                            <asp:BoundField DataField="POPQty" HeaderText="POPQty" Visible="false" />
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtItemQty" Enabled="false" Width="70px" Text='<%#Eval("Qty") %>'></asp:TextBox>
                                    <asp:Label runat="server" ID="lblItemId" Visible="false" Text='<%#Eval("ItemId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRate" runat="server" Width="70px" Text='0' Font-Bold="False"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle ForeColor="Black" BackColor="#7BA4E0" />
                        <RowStyle HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#7BA4E0" Font-Bold="True" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />

                    </asp:GridView>

                </div>

                <br />
                <div align="center">
                    <asp:GridView ID="gv_Barc" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="StockInId" HeaderText="StockInId" Visible="false" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" Visible="false" />
                            <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                            <asp:BoundField DataField="ProcessId" HeaderText="ProcessId" Visible="false" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" Visible="false"/>
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                            <asp:BoundField DataField="Row" HeaderText="Row" Visible="false" />
                            <asp:BoundField DataField="Rack" HeaderText="Rack" Visible="false" />
                            <asp:BoundField DataField="Shelf" HeaderText="Shelf" Visible="false" />
                            <asp:BoundField DataField="Warranty" HeaderText="Warranty (Months)" Visible="false" />
                            <asp:BoundField DataField="WarrantyTo1" HeaderText="W_Date" Visible="false" />
                            <asp:BoundField DataField="CoderLifeTo1" HeaderText="CL_Date" Visible="false" />

                            <asp:TemplateField HeaderText="Action" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="btn_SupplierEdit3" runat="server" CommandName="Delete" Text="Delete" CssClass="w3-btn w3-red" />
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
                </div>
                <br />
                <table>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

