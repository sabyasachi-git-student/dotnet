<%@ Page Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="FaultyItemInWarranty.aspx.cs" Inherits="Project_FaultyItemInWarranty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Sending of Faulty Item In Warranty</h3>
            </div>
            <br />

            <div align="center">
                

                <%--<table align="center">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnChooseRawMaterial" Text="Choose Item"  CssClass="w3-btn w3-blue" OnClick="btnChooseFaultyItem_Click" />
                        </td>
                    </tr>
                </table>--%>
                <br />
               <table cellspacing="15px" align="center" >
                   <tr>
                        <td style="font-weight: bold;">Select Faulty Id : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlFaultyId" runat="server"
                                Height="30px" Width="250px"
                                ValueField="FaultyId" TextField="FaultyId" Theme="Office2003Blue"
                                DataSourceID="SqlDataSource1"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" AutoPostBack="true" DropDownStyle="DropDown" OnTextChanged="ddlFaultyId_TextChanged">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct fe.FaultyId FROM tbl_FaultyEntry fe join tbl_FaultyEntryDetails fed on fe.FaultyId=fed.FaultyId join tbl_ItemMaster im on fed.ItemId=im.ItemId join tbl_RackStockInBarCodeDetails sib on fed.Barcode=sib.Barcode where fe.FaultyDate<sib.WarrantyTo and fed.SrBarVodeID Not in (select SrBarVodeID from tbl_FaultyInWarrantyDetails) and fe.BranchId=@BranchId">

                                 <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            </td>
                            <td>
                           <asp:Button runat="server" ID="btnChooseFaultyItem" Text="Choose Item" ForeColor="White" CssClass="w3-btn w3-blue" OnClick="btnChooseFaultyItem_Click" Height="36px"/>
                       </td>
                   </tr>   
                   <tr>
                       <td style="font-weight: bold;">To OEM :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOEM"  Height="30px" Width="250px"></asp:TextBox>
                        </td>
                       <td style="font-weight: bold;">Pick up arranged by OEM :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlPickup" Theme="Office2003Blue" Style="font-size: small;" Height="30px" Width="250px" AutoPostBack="true" OnTextChanged="ToggleRowSelection">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                       </tr>
                       <tr id="Corr" runat="server">
                           <td style="font-weight: bold;">Courier Company Name : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlCorComName" runat="server"
                                Height="30px" Width="250px"
                                ValueField="CorrierName" TextField="CorrierName" Theme="Office2003Blue"
                                DataSourceID="SqlDataSource2"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" AutoPostBack="true"  DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct CorrierName from tbl_FaultyInWarranty"></asp:SqlDataSource>
                            </td>
                           <td style="font-weight: bold;">Consignment No :  <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCnNo"  Height="30px" Width="250px"></asp:TextBox>
                        </td>
                           </tr>
                        <tr id="Corrier" runat="server">
                        <td style="font-weight: bold;">Consignment Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtCnDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                       </tr>
                       <tr>
                        <td style="font-weight: bold;">RMA No :  <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRmaNo"  Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">RMA Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtRmaDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                       </tr>
                       <tr>
                            <td style="font-weight: bold;">
                                             Image : <span style="color: red;">*</span>
                                        </td>
                                        <td>
                                            <asp:FileUpload runat="server" accept=".png,.jpg,.jpeg,.pdf" ID="FileUpload1"  />
                                        </td>
                       </tr>
                   </table>
                <br/>
                <div align="center" style="overflow:auto;width:900px;">
                    <asp:GridView ID="gvFaultyItemDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" OnPageIndexChanging="gvFaultyItemDetails_PageIndexChanging"
                        PageSize="100" OnRowCommand="gvFaultyItemDetails_RowCommand" ShowFooter="true" OnRowDeleting="gvFaultyItemDetails_RowDeleting" OnRowDataBound="gvFaultyItemDetails_RowDataBound">

                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>

<ItemStyle Width="25px"></ItemStyle>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="WarrantyPeriod" HeaderText="Warranty Period" />
                            <asp:BoundField DataField="WarrantyTo" HeaderText="WarrantyTo" />
                            <asp:BoundField DataField="CoderLifeTo" HeaderText="CoderLifeTo" />
                            
                            


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

                        <td>
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" /><%----%>
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="w3-btn w3-orange" /><%----%>
                        </td>
                    </tr>
                </table>  
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
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

                <div align="center" style="overflow:auto;width:900px;">
                    <br />
                    <dx:ASPxGridView ID="gvItem" Width="100%" EnableRowsCache="true" Font-Bold="true" SettingsPager-PageSize="10" Theme="Office2003Blue" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" KeyFieldName="Barcode">
                        <Columns>
                            <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" ShowClearFilterButton="true" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox runat="server" ID="chbxSelect" AutoPostBack="true"></dx:ASPxCheckBox>
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            
                           <dx:GridViewDataTextColumn FieldName="FaultyId" Caption="FaultyId" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SrBarVodeID" Caption="SrBarVodeID" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Barcode" Caption="Barcode" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn FieldName="ItemName" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Item Name" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Make" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Make" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="WarrantyPeriod" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Warranty Period" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="WarrantyTo" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Warranty To" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CoderLifeTo" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="CoderLife To" ReadOnly="True" VisibleIndex="2">
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
                                            <dx:ASPxButton ID="btnAddServiceItem" runat="server" CssClass="btn" HorizontalAlign="Left"  Text="ADD" OnClick="btnAddServiceItem_Click"><%----%>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>


                            </FooterRow>
                        </Templates>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select distinct fe.FaultyId,fed.SrBarVodeID,fed.Barcode,im.ItemId,im.ItemName,im.Make,im.WarrantyPeriod,convert(varchar,sib.WarrantyTo,103) as WarrantyTo,convert(Varchar,sib.CoderLifeTo,103) as CoderLifeTo FROM tbl_FaultyEntry fe join tbl_FaultyEntryDetails fed on fe.FaultyId=fed.FaultyId join tbl_ItemMaster im on fed.ItemId=im.ItemId join tbl_RackStockInBarCodeDetails sib on fed.Barcode=sib.Barcode where fe.FaultyDate<=sib.WarrantyTo and fe.FaultyId=@FaultyId and fed.SrBarVodeID Not in (select SrBarVodeID from tbl_FaultyInWarrantyDetails)">
                        <SelectParameters>
                                    <asp:SessionParameter SessionField="FaultyId" Name="FaultyId" />
                                </SelectParameters>
                    </asp:SqlDataSource>
                    <br />




                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
