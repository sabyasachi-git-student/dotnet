<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="Project_ItemMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Item Master</h3>
            </div>
            <br />

            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Category : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ddlCategory" Height="30px" Width="250px"></asp:TextBox>

                            <%--<asp:DropDownList runat="server" ID="ddlCategory" Theme="Office2003Blue" Style="font-size: small;" Height="30px" Width="250px">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="New" Value="New"></asp:ListItem>
                                <asp:ListItem Text="Repaired" Value="Repaired"></asp:ListItem>
                                <asp:ListItem Text="Installed" Value="Installed"></asp:ListItem>
                                <asp:ListItem Text="Faulty" Value="Faulty"></asp:ListItem>
                                <asp:ListItem Text="Non-repairable" Value="Non-repairable"></asp:ListItem>
                            </asp:DropDownList>--%>
                        </td>
                        <td style="font-weight: bold;">Barcode Availability :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlType" Theme="Office2003Blue" Style="font-size: small;" Height="30px" Width="250px">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="No" Value="Barcode"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="NonBarcode"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Item Name :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtItemName" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Make :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlMake" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="Make" TextField="Make" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct Make from tbl_MakeMaster"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Model :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="txtModel" runat="server"
                                DataSourceID="SqlDataSource7" Height="30px" Width="250px"
                                ValueField="Model" TextField="Model" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource7" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct Model from tbl_ModelMaster"></asp:SqlDataSource>
                           
                        </td>
                        <td style="font-weight: bold;">Unit :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlUnit" runat="server"
                                DataSourceID="SqlDataSource3" Height="30px" Width="250px"
                                ValueField="Unit" TextField="Unit" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct Unit from tbl_ItemMaster"></asp:SqlDataSource>
                        </td>
                    </tr>

                    <tr>

                        <td style="font-weight: bold;">HSN Code :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlHSNCode" runat="server"
                                DataSourceID="SqlDataSource4" Height="30px" Width="250px"
                                ValueField="HSNCode" TextField="HSNCode" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select * from tbl_HSNCodeMaster order by rowid desc"></asp:SqlDataSource>
                        </td>
                        <td style="font-weight: bold;">Re-order Level :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReorder" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Critical Level :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCriticalLevel" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Scrap Group Tagging :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlScrapGroup" runat="server"
                                DataSourceID="SqlDataSource5" Height="30px" Width="250px"
                                ValueField="GroupName" TextField="GroupName" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource5" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct GroupName from tbl_ScrapGroupMaster"></asp:SqlDataSource>
                        </td>
                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Power Supply :
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="txtWarrantyPeriod" runat="server"
                                DataSourceID="SqlDataSource6" Height="30px" Width="250px"
                                ValueField="PowerSupply" TextField="PowerSupply" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource6" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct PowerSupply from [tbl_PowerSupplyMaster]"></asp:SqlDataSource>
                            
                        </td>
                        <td style="font-weight: bold;">Coder Life(in months):</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCoderLife" Height="30px" Width="250px"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                        <td style="font-weight: bold;">Space Unit :</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSpaceUnit" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Product ID/Item Code :</td>
                        <td>
                                 <dx:ASPxComboBox ID="txtExtra1" runat="server"
                                DataSourceID="SqlDataSource8" Height="30px" Width="250px"
                                ValueField="ItemCode" TextField="ItemCode" Theme="Office2003Blue"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource8" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct ItemCode from [tbl_ItemCodeMaster]"></asp:SqlDataSource>

                        </td>
                       
                    </tr>
                    <tr >
                         <td style="font-weight: bold;">Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Height="60px" Style="resize: none" Width="250px"></asp:TextBox>
                        </td>

                        <td style="font-weight: bold; visibility:hidden;">Extra 2 :</td>
                        <td>
                            <asp:TextBox runat="server" Visible="false" ID="txtExtra2" Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>


                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" Height="36px" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" Height="36px" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="overflow: auto; width: 1100px;">
                <dx:ASPxGridView ID="ASPxGridView1" Theme="Office2003Blue" Font-Bold="true" EnableRowsCache="true" SettingsPager-PageSize="8" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="ItemId" OnRowCommand="ASPxGridView1_RowCommand" Style="overflow: scroll;">
                    <Columns>
                         <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Category" Caption="Category" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="BarcodeAvailability" Caption="Barcode Availability" HeaderStyle-Wrap="True" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ItemName" Caption="Item Name" ReadOnly="True" CellStyle-Wrap="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Make" Caption="Make" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Model" Caption="Model" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Unit" Caption="Unit" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="HSNCode" Caption="HSN Code" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReOrderLevel" Caption="ReOrder Level" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CriticalLevel" Caption="Critical Level" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="GroupName" Caption="Scrap Group" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="WarrantyPeriod" Caption="Power Supply" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SpaceUnit" Caption="Space Unit" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CoderLife" Caption="Coder Life" ReadOnly="True" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="Edit"  Width="0px">
                            <DataItemTemplate>
                                <asp:ImageButton ID="imbEdit" runat="server"
                                    CommandName="Edit"
                                    ImageUrl="../Image/edit.png" Width="20px" Height="20px" CommandArgument='<%#Eval("RowId")%>' ClientIDMode="Static" />
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>

                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" />
                    <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />

                    </SettingsPager>
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select a.*,b.GroupName,case when a.type='Barcode' then 'No' else 'Yes' end as BarcodeAvailability from [dbo].[tbl_ItemMaster] a join tbl_ScrapGroupMaster b on a.ScrapGroup=b.GroupName  order by rowid desc"></asp:SqlDataSource>
                <br />
            </div><br />

             <div align="center">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnXlsExport" runat="server" OnClick="btnXlsExport_Click" Text="Export to XLS" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="btnXlsxExport" runat="server" OnClick="btnXlsxExport_Click" Text="Export to XLSX" UseSubmitBehavior="False">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
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

