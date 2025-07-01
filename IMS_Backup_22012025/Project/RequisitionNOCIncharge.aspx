<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="RequisitionNOCIncharge.aspx.cs" Inherits="Project_RequisitionNOCIncharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Material Requisition NOC</h3>
            </div>
            <br />

            <div align="center" runat="server" id="tblApp" visible="false">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Requisition For : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqId1" Visible="false" Enabled="false" Height="30px" Style="resize: none" Width="125px"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtBranchName1" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserGroup1" Enabled="false" Height="30px" Style="resize: none" Width="130px"></asp:TextBox>
                            <asp:Label runat="server" ID="lblUserGroupId1" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqUser1" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Requisition Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate1" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Enabled="false" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="font-weight: bold;">Requisition Purpose :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlRequisitionPur1" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>
                            <asp:Label runat="server" ID="ddlRequisitionPurId1" Visible="false"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Requisition to :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlRequisitionto1" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>
                            <asp:Label runat="server" ID="ddlRequisitiontoId1" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlRequToGrp1" Enabled="false" Height="30px" Style="resize: none" Width="130px"></asp:TextBox>
                            <asp:Label runat="server" ID="ddlRequToGrpId1" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlRequToUser1" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Reason Of Priority : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlReasonOfPriority" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td style="font-weight: bold;">Proposed Mode Of
                            <br />
                            Transportation :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlTransportation" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks1" TextMode="MultiLine" Enabled="false" Height="60px" Style="resize: none" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Project :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlProject1" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>
                            <asp:Label runat="server" ID="ddlProjectId1" Visible="false"></asp:Label>

                        </td>
                        <%--  <td style="font-weight: bold;">Wants to Forword :
                        </td>--%>
                        <td>
                            <asp:CheckBox runat="server" ID="ChkFor" Visible="false" AutoPostBack="true" OnCheckedChanged="ChkFor_CheckedChanged" />
                        </td>
                    </tr>
                    <tr style="visibility: hidden">
                        <td style="font-weight: bold;">From :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPopId" Enabled="false" Height="30px" Style="resize: none" Width="100px"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtSecId" Enabled="false" Height="30px" Style="resize: none" Width="100px"></asp:TextBox>

                        </td>

                        <td style="font-weight: bold;">from1:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtForword" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                </table>
                <div align="center">
                    <asp:GridView ID="gvItemDetailsApp" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" OnPageIndexChanging="gvItemDetailsApp_PageIndexChanging"
                        PageSize="100" OnRowCommand="gvItemDetailsApp_RowCommand" ShowFooter="true" OnRowDeleting="gvItemDetailsApp_RowDeleting">

                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="25px" Visible="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReqNOCItemId" HeaderText="ReqItemId" Visible="false" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                            <asp:BoundField DataField="ReqToQty" HeaderText="Stock In Hand (For)" />
                            <asp:BoundField DataField="POPQty" HeaderText="Stock In Hand (To)" />
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtItemQty" Width="70px" Text='<%#Eval("Qty") %>'></asp:TextBox>
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

                    <br />

                </div>
            </div>
            <div align="center" runat="server" id="tblFor" visible="true">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Requisition From : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqId" Visible="false" Enabled="false" Height="30px" Style="resize: none" Width="125px"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtBranchName" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserGroup" Enabled="false" Height="30px" Style="resize: none" Width="130px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqUser" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Requisition Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="font-weight: bold;">Requisition Purpose :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlRequisitionPur" runat="server"
                                DataSourceID="SqlDataSource6" Height="30px" Width="250px"
                                ValueField="ID" TextField="PurposeName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource6" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct ID,PurposeName from tbl_PurposeReqMaster"></asp:SqlDataSource>
                        </td>

                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Requisition to :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlRequisitionto" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="BranchId" TextField="TerrytoryId" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct a.TerrytoryId, b.BranchId from tbl_HigherKeyMapping a join tbl_Branch b on a.TerrytoryId=b.BranchName">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchName" Name="BranchName" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td>

                            <asp:DropDownList runat="server" ID="ddlRequToGrp" AutoPostBack="true" OnTextChanged="ddlRequToGrp_TextChanged" Theme="Office2003Blue" Style="font-size: small;" Height="30px" Width="125px">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="TerritoryMgr" Value="UG14"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlRequToUser" runat="server"
                                Height="30px" Width="250px" ValueField="UserName" TextField="UserName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>

                        </td>

                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Height="60px" Style="resize: none" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnChooseRawMaterial" Text="Choose Item" OnClick="btnChooseRawMaterial_Click" CssClass="w3-btn w3-blue" />
                        </td>
                    </tr>
                </table>

                <br />
                <div align="center">
                    <asp:GridView ID="gvItemDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" OnPageIndexChanging="gvItemDetails_PageIndexChanging"
                        PageSize="100" OnRowCommand="gvItemDetails_RowCommand" ShowFooter="true" OnRowDeleting="gvItemDetails_RowDeleting">

                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="25px" Visible="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReqNOCItemId" HeaderText="ReqItemId" Visible="false" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="Type" HeaderText="Type" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                            <asp:BoundField DataField="ReqToQty" HeaderText="ReqToQty" />
                            <asp:BoundField DataField="POPQty" HeaderText="POPQty" />
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtItemQty" Width="70px" Text='<%#Eval("Qty") %>'></asp:TextBox>
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

                    <br />

                </div>
            </div>
            <div align="center">
                <table>
                    <tr>

                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" Height="36px" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" Height="36px" OnClick="btnClear_Click" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div>

            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="w3-container">
        <div id="id03" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4" style="width: 1200px;">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('id03').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Item Details</h3>
                    </header>
                </div>
                <br />
                <br />

                <div align="center" style="overflow: auto; width: 1200px;">
                    <br />
                    <dx:ASPxGridView ID="gvItem" Width="100%" EnableRowsCache="true" Font-Bold="true" SettingsPager-PageSize="10" Theme="Office2003Blue" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" KeyFieldName="ItemId">
                        <Columns>
                            <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" ShowClearFilterButton="true" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox runat="server" ID="chbxSelect" AutoPostBack="true"></dx:ASPxCheckBox>
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Category" Caption="Category" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Type" Caption="Type" ReadOnly="True" VisibleIndex="1">
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
                            <dx:GridViewDataTextColumn FieldName="ReOrderLevel" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="ReOrder" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CriticalLevel" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Critical Level" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ScrapName" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Scrap Name" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="WarrantyPeriod" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Warranty Period" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Remarks" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Remarks" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SpaceUnit" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="SpaceUnit" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CoderLife" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="CoderLife" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Extra1" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Extra1" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Extra2" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Extra2" ReadOnly="True" VisibleIndex="2">
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
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select a.*,  b.ItemName as ScrapName  from tbl_ItemMaster a join tbl_ScrapMaster b on a.ScrapGroup=b.ScrapId  order by a.rowid desc"></asp:SqlDataSource>
                    <br />

                </div>
            </div>
        </div>
    </div>

</asp:Content>

