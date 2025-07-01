<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="MaterialSummaryReport.aspx.cs" Inherits="Project_MaterialSummaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Material History Summary Report</h3>
            </div>
            <br />

            <div style="width: 100%;" align="center">
                <table align="center" cellspacing="15px">
                    <tr>
                        <td>Product Serial No :</td>
                        <td>
                            <asp:TextBox ID="txtSerial" runat="server" AutoPostBack="true" OnTextChanged="txtSerial_TextChanged"></asp:TextBox></td>
                    </tr>
                </table>
                <br />
                <div runat="server" id="div_IDetails" style="width: 700px;" visible="false" align="center">
                    <fieldset>
                        <legend><strong>Item Details</strong></legend>
                        <div>
                            <table cellspacing="15px">
                                <tr>
                                    <td><b>Item Id :</b></td>
                                    <td>
                                        <asp:Label ID="lblGroup" runat="server" Text=""></asp:Label></td>
                                    <td><b>Item Name :</b></td>
                                    <td>
                                        <asp:Label ID="lblItem" runat="server" Text=""></asp:Label></td>
                                   
                                </tr>
                                <tr>
                                     <td><b>Category :</b></td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label></td>
                                    <td><b>Make :</b></td>
                                    <td>
                                        <asp:Label ID="lblMake" runat="server" Text=""></asp:Label></td>
                                    
                                </tr>
                                <tr>
                                    <td><b>Model No. :</b></td>
                                    <td>
                                        <asp:Label ID="lblModel" runat="server" Text=""></asp:Label></td>
                                    <td><b>HSN Code :</b></td>
                                    <td>
                                        <asp:Label ID="lblUnit" runat="server" Text=""></asp:Label></td>
                                </tr>
                            </table>
                        </div>

                    </fieldset>
                </div>
                <br />
                <div runat="server" id="div_StockInDetails" style="width: 700px;" visible="false" align="center">
                    <fieldset>
                        <legend><strong>Stock In Details</strong></legend>
                        <div>
                            <table cellspacing="15px">
                                <tr>
                                    <td><b>Stock In Id :</b></td>
                                    <td>
                                        <asp:Label ID="lblStockInId" runat="server" Text=""></asp:Label></td>
                                    <td><b>Stock In Date :</b></td>
                                    <td>
                                        <asp:Label ID="lblStockInDate" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Invoice No. :</b></td>
                                    <td>
                                        <asp:Label ID="lblInvoice" runat="server" Text=""></asp:Label></td>
                                    <td><b>Consignment No :</b></td>
                                    <td>
                                        <asp:Label ID="lblInvoiceDate" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>GRN NO :</b></td>
                                    <td>
                                        <asp:Label ID="lblGRN" runat="server" Text=""></asp:Label></td>
                                    <td><b>PO No:</b></td>
                                    <td>
                                        <asp:Label ID="lblInStock" runat="server" Text=""></asp:Label></td>

                                </tr>
                                 <tr>
                                    <td><b>Supplier :</b></td>
                                   <td>
                                        <asp:Label ID="lblSupplier" runat="server" Text=""></asp:Label></td>
                                   
                                </tr>
                            </table>
                        </div>

                    </fieldset>
                </div>
                <br />

                <div runat="server" id="div_PresentStatus" style="width: 700px;" visible="false" align="center">
                    <fieldset>
                        <legend><strong>Present Status</strong></legend>
                        <div>
                            <table cellspacing="15px">
                                <tr>
                                    <td><b>Status :</b></td>
                                   <td>
                                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></td>
                                    <td><b>Present Location :</b></td>
                                   <td>
                                        <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Warranty Valid <br />Up To :</b></td>
                                   <td>
                                        <asp:Label ID="lblWarrantyDate" runat="server" Text=""></asp:Label></td>
                                   
                                </tr>
                            </table>
                        </div>

                    </fieldset>
                </div>

                <div runat="server" id="div_StockOut" style="width: 700px;" visible="false" align="center">
                    <fieldset>
                        <legend><strong>Stock Out Details</strong></legend>
                        <div>
                            <table cellspacing="15px">
                                <tr>
                                    <td><b>Stock Out Id :</b></td>
                                    <td>
                                        <asp:Label ID="lblStockOut" runat="server" Text=""></asp:Label></td>
                                    <td><b>Stock Out Date :</b></td>
                                    <td>
                                        <asp:Label ID="lblStockOutDate" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Sales Order Id :</b></td>
                                    <td>
                                        <asp:Label ID="lblSO" runat="server" Text=""></asp:Label></td>
                                    <td><b>Sales Order Date :</b></td>
                                    <td>
                                        <asp:Label ID="lblSODate" runat="server" Text=""></asp:Label></td>

                                </tr>
                                <tr>
                                    <td><b>Customer Name :</b></td>
                                    <td>
                                        <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label></td>
                                    <td><b>Sale Type :</b></td>
                                    <td>
                                        <asp:Label ID="lblSale" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Commissioning :</b></td>
                                    <td>
                                        <asp:Label ID="lblCommissioning" runat="server" Text=""></asp:Label></td>
                                    <td><b>Damage :</b></td>
                                    <td>
                                        <asp:Label ID="lblDamage" runat="server" Text=""></asp:Label></td>

                                </tr>
                            </table>
                        </div>

                    </fieldset>
                </div>
                <br />
                <div runat="server" id="div_Damage" style="width: 700px;" visible="false" align="center">
                    <fieldset>
                        <legend><strong>Damage Details</strong></legend>
                        <div>
                            <table cellspacing="15px">
                                <tr>
                                    <td><b>Damage Id :</b></td>
                                    <td>
                                        <asp:Label ID="lblDamageId" runat="server" Text=""></asp:Label></td>
                                    <td><b>Collection Date :</b></td>
                                    <td>
                                        <asp:Label ID="lblCollectionDate" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Warranty Status :</b></td>
                                    <td>
                                        <asp:Label ID="lblWarranty" runat="server" Text=""></asp:Label></td>
                                    <td><b>Current Status :</b></td>
                                    <td>
                                        <asp:Label ID="lblCurrentStatus" runat="server" Text=""></asp:Label></td>

                                </tr>
                                <tr id="idSent" runat="server" visible="false">
                                    <td><b>Sent To :</b></td>
                                    <td>
                                        <asp:Label ID="lblSent" runat="server" Text=""></asp:Label></td>
                                    <td><b>Sent Date :</b></td>
                                    <td>
                                        <asp:Label ID="lblSentDate" runat="server" Text=""></asp:Label></td>

                                </tr>
                                <tr id="idRecollection" runat="server" visible="false">
                                    <td><b>Recollection Status :</b></td>
                                    <td>
                                        <asp:Label ID="lblRecollectionStatus" runat="server" Text=""></asp:Label></td>
                                    <td><b>Recollection Date :</b></td>
                                    <td>
                                        <asp:Label ID="lblRecollectionDate" runat="server" Text=""></asp:Label></td>


                                </tr>
                                <tr id="idSendCust" runat="server" visible="false">
                                    <td><b>Sent Date To Customer :</b></td>
                                    <td>
                                        <asp:Label ID="lblCustDate" runat="server" Text=""></asp:Label></td>



                                </tr>
                            </table>
                        </div>

                    </fieldset>
                </div>
            </div>



            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

