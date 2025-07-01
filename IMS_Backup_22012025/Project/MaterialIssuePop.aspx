<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="MaterialIssuePop.aspx.cs" Inherits="Project_MaterialIssuePop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Material Issue </h3>
            </div>
            <br />

            <div align="center">
                <table>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnChooseRequisition" OnClick="btnChooseRequisition_Click" Text="Choose Requisition" CssClass="w3-btn w3-blue" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqPopAppId" Visible="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqPopId" Visible="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                       
                    </tr>
                </table>
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Issue From : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqId" Visible="false" Enabled="false" Height="30px" Style="resize: none" Width="125px"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtBranchName" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserGroup" Enabled="false" Height="30px" Style="resize: none" Width="130px"></asp:TextBox>
                            <asp:Label runat="server" ID="lblUserGroupId"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReqUser" Enabled="false" Height="30px" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Issue Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="font-weight: bold;">Issue Purpose :
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
                            <asp:Label runat="server" ID="ddlRequisitionPurId"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Issue to :
                        </td>
                        <td>

                            <dx:ASPxComboBox ID="ddlRequisitionto" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="BranchId" TextField="SectionId" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct a.SectionId, b.BranchId from tbl_HigherKeyMapping a join tbl_Branch b on a.SectionId=b.BranchName where POPId=@BranchName">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchName" Name="BranchName" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Label runat="server" ID="ddlRequisitiontoId1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlRequToGrp" Enabled="false" Height="30px" Style="resize: none" Width="130px"></asp:TextBox>

                            <%-- <asp:DropDownList runat="server" ID="ddlRequToGrp" Theme="Office2003Blue" Style="font-size: small;" Height="30px" Width="125px">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="SecIncharge" Value="UG15"></asp:ListItem>
                                <asp:ListItem Text="TerritoryMgr" Value="UG14"></asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:Label runat="server" ID="ddlRequToGrpId"></asp:Label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlRequToUser" runat="server"
                                Height="30px" Width="250px" ValueField="UserName" TextField="UserName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>

                        </td>

                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Requision Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ReqRemarks" TextMode="MultiLine" Enabled="false" Height="45px" Style="resize: none" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Approval Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ReqAppRemarks" TextMode="MultiLine" Enabled="false" Height="45px" Style="resize: none" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Height="60px" Style="resize: none" Width="250px"></asp:TextBox>
                        </td>
                   
                    
                        <td style="font-weight: bold;">Issue Option :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlProOp" Theme="Office2003Blue" Style="font-size: small;" Height="30px" Width="250px" AutoPostBack="true" OnTextChanged="ddlProOp_TextChanged">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="General Stock" Value="LocationWise"></asp:ListItem>
                                <asp:ListItem Text="Project Wise" Value="ProjectWise"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="ProjectTr">
                        <td style="font-weight: bold;">Project :
                        </td>
                         <td>
                            <asp:TextBox runat="server" ID="txtProject" Height="30px" Enabled="false" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                        <td style="font-weight: bold; visibility:hidden">Project ID :
                        </td>
                         <td style="visibility:hidden">
                            <asp:TextBox runat="server" ID="txtProjectId" Height="30px" Enabled="false" Style="resize: none" Width="250px"></asp:TextBox>

                        </td>
                    </tr>
                </table>
                <br />
                <div align="center">
                    <asp:Button runat="server" ID="btnStockOut" Text="Verify Item" OnClick="btnStockOut_Click" Height="36px" Visible="false" CssClass="w3-btn w3-red" />
                    <br />
                    <br />

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

                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                            <asp:BoundField DataField="WarrantyPeriod" HeaderText="Power Supply" />
                            <asp:BoundField DataField="ReqToQty" HeaderText="Stock In Hand (To)" />
                            <asp:BoundField DataField="POPQty" HeaderText="Stock In Hand (From)" />
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
                <br />
                <div align="center">
                    <asp:GridView ID="gv_Barc" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_Barc_RowCommand" OnRowDeleting="gv_Barc_RowDeleting" OnRowEditing="gv_Barc_RowEditing">
                        <Columns>
                            <asp:BoundField DataField="StockInId" HeaderText="Stock In Id" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                            <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" Visible="false" />
                            <asp:BoundField DataField="ProcessId" HeaderText="ProcessId" Visible="false" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />

                            <asp:BoundField DataField="Row" HeaderText="Row" />
                            <asp:BoundField DataField="Rack" HeaderText="Rack" />
                            <asp:BoundField DataField="Shelf" HeaderText="Shelf" />
                            <asp:BoundField DataField="Warranty" HeaderText="Warranty (Months)" Visible="false" />
                            <asp:BoundField DataField="WarrantyTo" HeaderText="W_Date" Visible="false" />
                            <asp:BoundField DataField="CoderLifeTo" HeaderText="CL_Date" Visible="false" />

                            <asp:TemplateField HeaderText="Barcode">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtBarcode" Enabled="false" Width="170px" Text='<%#Eval("Barcode") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Enter Barcode">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtBarcodeEnter" Width="150px" Text='<%#Eval("EnterBarcode") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

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
                <br />
                <div align="center">
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" Height="36px" CssClass="w3-btn w3-green" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange" />

                            </td>
                        </tr>
                    </table>
                </div>
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
                    <dx:ASPxGridView ID="gvItem" Width="100%" EnableRowsCache="true" Font-Bold="true" SettingsPager-PageSize="10" Theme="Office2003Blue" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="DS_Estimation" KeyFieldName="ReqAppId">
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                        <Columns>
                            <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" ShowClearFilterButton="true" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox runat="server" ID="chbxSelect" AutoPostBack="true"></dx:ASPxCheckBox>
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="ReqAppId" ReadOnly="True" Caption="Req App Id" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ReqId" ReadOnly="True" Caption="Req Id" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RequisitionDate" ReadOnly="True" Caption="Requisition Date" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="AppDate" ReadOnly="True" Caption="Approval Date" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RequisitionPurpose" ReadOnly="True" Caption="Requisition Purpose" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FromGr" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="UserName" ShowInCustomizationForm="True">
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
                        <Templates>
                            <DetailRow>
                                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" RenderMode="Lightweight" Theme="Office2003Blue">
                                    <TabPages>
                                        <dx:TabPage Name="RawMaterials" Text="Item Details">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <table class="dxflInternalEditorTable">
                                                        <tr>
                                                            <td class="dxflEmptyItem"></td>
                                                            <td class="dxflEmptyItem"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <dx:ASPxGridView ID="gv_EstRawMat" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" OnBeforePerformDataSelect="gv_EstRawMat_BeforePerformDataSelect" Theme="Office2003Blue" Width="100%">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="ReqAppId" ShowInCustomizationForm="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ReqId" ShowInCustomizationForm="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Category" Caption="Category" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Type" Caption="Type" ReadOnly="True" Visible="false">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemName" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Item Name" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Make" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Make" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Model" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Model" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Unit" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Unit" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="HSNCode" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="HSN /SAC Code" Visible="false" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ReOrderLevel" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="ReOrder" Visible="false" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="CriticalLevel" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Critical Level" Visible="false" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="WarrantyPeriod" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Power Supply" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Remarks" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Remarks" ReadOnly="True" Visible="false">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SpaceUnit" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="SpaceUnit" ReadOnly="True" Visible="false">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="CoderLife" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="CoderLife" ReadOnly="True" Visible="false">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Extra1" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Extra1" ReadOnly="True" Visible="false">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Extra2" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Extra2" ReadOnly="True" Visible="false">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Qty" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Qty" ReadOnly="True">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>

                                                                    <Settings ShowFilterRow="True" ShowFooter="True" />
                                                                </dx:ASPxGridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                </dx:ASPxPageControl>
                            </DetailRow>
                        </Templates>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="DS_Estimation" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from (
select Rowid, ReqPopAppId as ReqAppId,  ReqPopId as ReqId, convert(varchar,RequisitionDate,103) as RequisitionDate, RequisitionPurpose, Status5 as FromGr, Status4 as Location, '' as UserName, convert(varchar,DOE,103) as AppDate, DOE from tbl_RequisitionPopApproval where ReqPopAppId not in (select ReqAppId from tbl_MaterialIssue) and UserId=@UserId and BranchId=@BranchId 
union all
select Rowid, ReqSecAppId as ReqAppId,  ReqSecId as ReqId, convert(varchar,RequisitionDate,103) as RequisitionDate, RequisitionPurpose, Status5 as FromGr, Status4 as Location,  '' as UserName, convert(varchar,DOE,103) as AppDate, DOE from tbl_RequisitionSecApproval where ReqSecAppId not in (select ReqAppId from tbl_MaterialIssue) and UserId=@UserId and BranchId=@BranchId 
union all
select Rowid, ReqTetAppId as ReqAppId,  ReqtetId as ReqId, convert(varchar,RequisitionDate,103) as RequisitionDate, RequisitionPurpose, Status5 as FromGr, Status4 as Location,  '' as UserName, convert(varchar,DOE,103) as AppDate, DOE from tbl_RequisitionTetApproval where ReqTetAppId not in (select ReqAppId from tbl_MaterialIssue) and UserId=@UserId and BranchId=@BranchId 
union all
select Rowid, ReqNOCAppId as ReqAppId,  ReqNOCId as ReqId, convert(varchar,RequisitionDate,103) as RequisitionDate, RequisitionPurpose, Status5 as FromGr, Status4 as Location, Status7 as UserName, convert(varchar,DOE,103) as AppDate, DOE from [tbl_RequisitionNOCApproval] where ReqNOCAppId not in (select ReqAppId from tbl_MaterialIssue) and ReqNOCId not in (select ReqNOCId from tbl_RequisitionNOCApp_Reject) and UserId=@UserId and BranchId=@BranchId
union all
select Rowid, ReqProId as ReqAppId,  ReqProId as ReqId, convert(varchar,RequisitionDate,103) as RequisitionDate, RequisitionPurpose, Status5 as FromGr, Status4 as Location, Status6 as UserName, convert(varchar,DOE,103) as AppDate, DOE from [tbl_RequisitionProject] where ReqProId not in (select ReqAppId from tbl_MaterialIssue) and UserId=@UserId and BranchId=@BranchId
) as a order by DOE desc">
                        <SelectParameters>
                            <asp:SessionParameter Name="BranchId" SessionField="BranchId" />
                            <asp:SessionParameter Name="UserId" SessionField="UserId" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>"></asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

