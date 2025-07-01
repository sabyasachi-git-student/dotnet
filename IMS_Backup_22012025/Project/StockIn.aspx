<%@ Page Title="Stock In" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="StockIn.aspx.cs" Inherits="Project_StockIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Stock In</h3>
            </div>
            <br />
            <div align="center">
                <table>
                    <tr style="visibility:hidden">
                       <td style="font-weight: bold;">GRN No. : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtGRNNo" runat="server" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Supplier : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlSupplier" runat="server"
                                DataSourceID="SqlDataSource1" Height="30px" Width="250px"
                                ValueField="SupplierId" TextField="CompanyName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select CompanyName,SupplierId from [dbo].[tbl_SupplierMasterEntry]"></asp:SqlDataSource>
                        </td>
                        <td style="font-weight: bold;">Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>


                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Challan No. : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtChallanNo" runat="server" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Challan Date :
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpChallanDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Invoice No. : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceNo" runat="server" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Invoice Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpInvoiceDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Delivery By : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeliveryBy" runat="server" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Receive By : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReceiveBy" runat="server" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Delivery Person Mob No : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Vehicle No : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtVehicleNo" runat="server" Height="30px" Width="250px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="font-weight: bold;">State : 
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlState" runat="server" Enabled="false"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="StateCode" TextField="StateName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select StateCode,StateName from dbo.tbl_StateMaster"></asp:SqlDataSource>
                        </td>
                        <td style="font-weight: bold;">File Upload :
                        </td>
                        <td>
                            <asp:FileUpload ID="fuDoc" accept=".png,.jpg,.jpeg,.pdf" runat="server" />
                            <asp:Button runat="server" ID="btn_Corres" Text="Add" OnClick="btn_Corres_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td style="font-weight: bold;">PO No : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPO_ID" runat="server" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">PO Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpPODate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>

                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Consignment NO : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtConsignmentNO" runat="server" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Project Required ?<span style="color: red;">*</span>
                             <%--<asp:CheckBox ID="chkProjectRequirement" runat="server" OnCheckedChanged="ToggleRowSelection"
                              Checked="true"  AutoPostBack="True" />--%>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlProjectName" runat="server"
                                DataSourceID="SqlDataSource4" Height="30px" Width="250px"
                                ValueField="ProjectId" TextField="ProjectName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select ProjectName,ProjectId from dbo.tbl_ProjectMaster"></asp:SqlDataSource>
                        </td>
                        
                        
                    </tr>

                </table>
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:GridView runat="server" ID="gv_Correspondance" OnRowDeleting="gv_Correspondance_RowDeleting" OnRowCommand="gv_Correspondance_RowCommand" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="25px" Height="25px"
                                                CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Image" HeaderText="Image" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="visibility: hidden;">
                        <td style="font-weight: bold;">Image :
                        </td>
                        <td>
                            <asp:FileUpload runat="server" ID="FileUpload1" />
                        </td>
                        <td>
                            <asp:Label ID="lblError" ControlToValidate="FileUpload1" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />

                <table>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnChooseRawMaterial" Text="Choose Item" OnClick="btnChooseRawMaterial_Click" CssClass="w3-btn w3-blue" />
                        </td>
                    </tr>
                </table>

                <br />
                <div align="center">
                    <asp:GridView ID="gvItemDetails" runat="server" Width="100%" AutoGenerateColumns="False" Style="white-space: normal;"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" OnPageIndexChanging="gvItemDetails_PageIndexChanging"
                        PageSize="100" OnRowCommand="gvItemDetails_RowCommand" ShowFooter="true" OnRowDeleting="gvItemDetails_RowDeleting" OnRowDataBound="gvItemDetails_RowDataBound">

                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="false" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>

<ItemStyle Width="25px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="Type" HeaderText="Type" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                            
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtItemQty" Width="70px" Text='<%#Eval("Qty") %>'></asp:TextBox>
                                    <asp:Label runat="server" ID="lblItemId"  Text='<%#Eval("ItemId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRate" runat="server" Width="70px" Text='<%#Eval("rate") %>' Font-Bold="False"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" Enabled="false" Width="70px"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Warranty(Months)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtWarranty" runat="server"  Width="70px"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="WarrantyTo" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtWarrantyTo" runat="server" Enabled="false" Width="140px"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="CoderLifeTo" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCoderLifeTo" runat="server" Enabled="false" Width="140px"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CGST">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbxCGST" Font-Bold="true" Text='<%#Eval("CGST")%>' />
                                                %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCGST" runat="server" Enabled="false" Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SGST">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbxSGST" Font-Bold="true" Text='<%#Eval("SGST")%>' />
                                                %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSGST" runat="server" Enabled="false" Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>


                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IGST">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbxIGST" Font-Bold="true" Text='<%#Eval("IGST")%>' />
                                                %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtIGST" runat="server" Enabled="false" Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="lblId" Text="Total Amount"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTotal" runat="server" Enabled="false" Width="70px"></asp:TextBox>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" Enabled="false" Width="70px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Barcode">
                                <ItemTemplate>
                                    <asp:Button ID="btnBarcode" runat="server" Text="Generate Barcode" CommandName="Barcode" CommandArgument="<%# Container.DataItemIndex %>" />

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

                <div align="center">
                    <asp:GridView ID="Gv_Barcode" runat="server" Width="100%" AutoGenerateColumns="False" Style="white-space: normal;" 
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" PageSize="500" ShowFooter="true" OnRowDeleting="gvItemDetails_RowDeleting" >

                        <Columns>
                            <asp:BoundField DataField="itemid" HeaderText="itemid" />
                            <asp:BoundField DataField="Type" HeaderText="Type" />
                            <asp:BoundField DataField="Warranty" HeaderText="Warranty" />
                            <asp:BoundField DataField="WarrantyTo" HeaderText="WarrantyTo" />
                            <asp:BoundField DataField="CoderLifeTo" HeaderText="CoderLifeTo" />
                            <asp:BoundField DataField="TempBarcode" HeaderText="TempBarcode" />
                        </Columns>
                        <FooterStyle ForeColor="Black" BackColor="#7BA4E0" />
                        <RowStyle HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#7BA4E0" Font-Bold="True" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />

                    </asp:GridView>

                    <br />

                </div>

                <br />
                <table>
                    <tr>
                        <td style="font-weight: bold;">Freight : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFreigh" Text="0.00" runat="server" Height="30px" Width="100px"></asp:TextBox>
                        </td>

                        <td style="font-weight: bold;">Other Charges : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherCharges" Text="0.00" runat="server" Height="30px" Width="100px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Total Bill Amount: <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalBillAmount" Text="0.00" runat="server" Enabled="false" Height="30px" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />

                <br />
                <table>
                    <tr>

                        <td>
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="w3-btn w3-orange" />
                            <asp:Button runat="server" ID="btnCalculate" Text="Calculate" OnClick="btnCalculate_Click" CssClass="w3-btn w3-blue" />
                        </td>
                    </tr>
                </table>

                <table>
                    <tr style="visibility: hidden">
                        <td style="font-weight: bold;">State : <span style="color: red;">*</span>
                        </td>
                        <td></td>
                        <td style="font-weight: bold;">FreightCGST : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFreightCGST" Text="0.00" runat="server" Height="30px" Enabled="false" Width="100px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">FreightSGST : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFreightSGST" Text="0.00" runat="server" Height="30px" Enabled="false" Width="100px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">FreightIGST : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFreightIGST" Text="0.00" runat="server" Height="30px" Enabled="false" Width="100px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">FreightWithGST : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFreightWithGST" runat="server" Height="30px" Enabled="false" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="w3-container">
        <div id="id03" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('id03').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Item Details</h3>
                    </header>
                </div>
                <br />
                <br />

                <div align="center">
                    <br />
                    <dx:ASPxGridView ID="gvItem" Width="100%" EnableRowsCache="true" Font-Bold="true" SettingsPager-PageSize="10" Theme="Office2003Blue" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" KeyFieldName="ItemId">
                        <Columns>
                            <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" ShowClearFilterButton="true" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox runat="server" ID="chbxSelect" AutoPostBack="true"></dx:ASPxCheckBox>
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" Settings-AutoFilterCondition="Contains" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Category" Caption="Category" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemName" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Item Name" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Make" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Make" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Model" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Model" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Unit" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Unit" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="HSNCode" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="HSN /SAC Code" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="WarrantyPeriod" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Power Supply" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <%-- <SettingsBehavior AllowSelectSingleRowOnly="True" />--%>
                        <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />

                        </SettingsPager>
                        <Templates>
                            <FooterRow>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnAddServiceItem" runat="server" CssClass="btn" HorizontalAlign="Left" OnClick="btnAddServiceItem_Click" Text="ADD">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>


                            </FooterRow>
                        </Templates>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from tbl_ItemMaster order by rowid desc"></asp:SqlDataSource>
                    <br />




                </div>
            </div>
        </div>
    </div>


    <div class="w3-container">
        <div id="dvBarcodes" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('dvBarcodes').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Enter Barcodes</h3>
                    </header>
                </div>
                <br />
                <br />

                <div align="center">
                    <br />
                     <asp:Label runat="server" ID="lblError11" Style=" font-size: large; color: red; font-weight: bold;"></asp:Label><br /><br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="50%">
                          <Columns>
                               <asp:BoundField DataField="RowNumber" HeaderText="RowNumber" />
                               <asp:TemplateField HeaderText="Barcode">
                                <ItemTemplate>
                                   
                                    <asp:TextBox ID="txtBarCode" runat="server" Width="170px" Text='<%#Eval("Barcode") %>' Font-Bold="False" ></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                          </Columns>

                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnBarCodeSave" runat="server" Text="Save" OnClick="btnBarCodeSave_Click" />



                </div>
            </div>
        </div>
    </div>

</asp:Content>

