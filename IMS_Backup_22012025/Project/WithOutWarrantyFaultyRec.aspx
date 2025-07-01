<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="WithOutWarrantyFaultyRec.aspx.cs" Inherits="Project_WithOutWarrantyFaultyRec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Faulty Item Receive Out Warranty</h3>
            </div>
            <br />
            
            <div align="center">
                <table cellspacing="15px" align="center">
                   <tr>
                        <td style="font-weight: bold;" >Faulty Id : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlFaultyId" runat="server"
                                DataSourceID="SqlDataSource2" Height="30px" Width="250px"
                                ValueField="FaultyId" TextField="FaultyId" Theme="Office2003Blue" AutoPostBack="true" OnTextChanged="ddlFaultyId_TextChanged"
                                IncrementalFilteringMode="Contains" CallbackPageSize="10" EnableCallbackMode="True" DropDownStyle="DropDown">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select distinct FaultyId from tbl_FaultyWarrantyExpired"></asp:SqlDataSource>
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
                             <asp:TextBox runat="server" ID="txtRMANumber" Enabled="false"  Height="30px" Width="250px"></asp:TextBox>
                        </td>
                         <td style="font-weight: bold;">RMA Date :
                        </td>
                        <td >
                             <dx:ASPxDateEdit ID="dtpRMADate" Enabled="false" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" runat="server" Height="30px" Width="250px">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                         <td style="font-weight: bold;">OEM Name : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOEMName" Enabled="false"  Height="30px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold;">Remarks : </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Style="resize:none"  Height="60px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                   
                    </table>
                     
              <div align="center" style="overflow:auto;width:900px;">
                    <asp:GridView ID="gvFaultyItemDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" CellPadding="3" CellSpacing="10"
                        AllowPaging="True" PageSize="100" >

                        <Columns>
                                                       
                            <asp:BoundField DataField="SrBarVodeID" HeaderText="SrBarVodeID" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" />
                            <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
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
                
                   <table>
                    <tr>

                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" ID="btnClear" OnClick="btnClear_Click"  Text="Clear" Height="36px" CssClass="w3-btn w3-orange"/>
                        </td>
                    </tr>
                </table>
                </div>
            
            <br />            
            </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

