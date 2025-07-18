﻿<%@ Page Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="FaultyEntry.aspx.cs" Inherits="Project_FaultyEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Faulty Entry</h3>
            </div>
            <br />
            <div align="center">
                <table cellspacing="15px" align="center">
                    <tr>
                        <td style="font-weight: bold;">Faulty Date : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="txtFaultyDate" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="font-weight: bold;">Barcode :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtBarcode" Height="30px" Width="250px" OnTextChanged="txtBarcode_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Remarks :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks"  Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Faulty Type : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="txtFaultyReason" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="FaultyReId" TextField="FaultyType" Theme="Office2003Blue" 
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDownList">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select Distinct FaultyReId,FaultyType from tbl_FaultyReasonMaster"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr runat="server" id="row">
                         <td style="font-weight: bold;">Wants To Forward To Region : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:CheckBox  runat="server" ID="chk" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true"/>
                        </td>
                        <td>
                             <asp:TextBox runat="server" ID="txtRegion" Visible="false"  Height="30px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                   
                </table>
            </div>
            
            <br />
                <div align="center">
                    <br />
                     <asp:Label runat="server" ID="lblError11" Style=" font-size: large; color: red; font-weight: bold;"></asp:Label><br /><br />
                        <asp:GridView ID="gvItemDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" OnPageIndexChanging="gvItemDetails_PageIndexChanging"
                        PageSize="100" OnRowCommand="gvItemDetails_RowCommand" ShowFooter="true" OnRowDeleting="gvItemDetails_RowDeleting" OnRowDataBound="gvItemDetails_RowDataBound">

                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="20px" Height="20px"
                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>

<ItemStyle Width="25px"></ItemStyle>
                            </asp:TemplateField> 
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="Row" HeaderText="Row" />
                            <asp:BoundField DataField="Rack" HeaderText="Rack" />
                            <asp:BoundField DataField="Shelf" HeaderText="Shelf" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Make" HeaderText="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                            
                            


                        </Columns>
                        <FooterStyle ForeColor="Black" BackColor="#7BA4E0" />
                        <RowStyle HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#7BA4E0" Font-Bold="True" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />

                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="SELECT * FROM tbl_RackStockInBarCodeDetails SIB LEFT JOIN tbl_ItemMaster IM ON IM.ItemId=SIB.ItemId WHERE SIB.Barcode=@Barcode">
                         <SelectParameters>
                                    <asp:SessionParameter SessionField="Barcode" Name="Barcode" />
                                </SelectParameters>
                    </asp:SqlDataSource>
                    <br />
                    <br />
                <table>
                    <tr>

                        <td>
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="w3-btn w3-orange" />
                        </td>
                    </tr>
                </table>



                </div>
            
        </div>
            

            

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="w3-container">
        <div id="id04" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4">
               
                <br />
                <br />

                <div align="center">
                    <table>
                        <tr>
                            <td style="font-weight: bold;">Your Faulty Code :
                            </td>
                            <td>
                                <asp:Label runat="server" ID="txtCode" Height="30px"  Enabled="false" style="font-weight: bold; font-size:large; color:red; align-content:center" Width="250px"></asp:Label>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnCodeSerch" Text="Done" Height="36px" OnClick="btnCodeSerch_Click" CssClass="w3-btn w3-blue" />

                            </td>
                        </tr>
                    </table>

                </div>
                <br />
                <br />
            </div>
        </div>
    </div>

</asp:Content>
