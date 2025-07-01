<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="NotificationUser.aspx.cs" Inherits="Project_NotificationUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../style/DashBordStyle.css" rel="stylesheet" />
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <!------ Include the above in your HEAD tag ---------->

    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>

    <div class="container" style="max-width: 1304px;">
        <div class="row">
            <div class="col-md-2">
                <div class="dbox dbox--color-1">
                    <div class="dbox__icon">
                        <i class="glyphicon glyphicon-cloud" style="background-color: green;"></i>
                    </div>
                    <div class="dbox__body" style="background-color: green;">
                        <span class="dbox__count">
                            <asp:Label ID="lblNewSO" runat="server" Text="0"></asp:Label>
                        </span>
                        <span class="dbox__title">Pop Requisition
                            <br />
                            Approval</span>
                    </div>

                    <div class="dbox__action">
                        <asp:Button ID="btnNewSO" OnClick="btnNewSO_Click" runat="server" CausesValidation="false" Text="More Info"
                            CssClass="dbox__action__btn" />

                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="dbox dbox--color-1">
                    <div class="dbox__icon">
                        <i class="glyphicon glyphicon-cloud" style="background-color: green;"></i>
                    </div>
                    <div class="dbox__body" style="background-color: green;">
                        <span class="dbox__count">
                            <asp:Label ID="lblNewConvertSO" runat="server" Text="0"></asp:Label>
                        </span>
                        <span class="dbox__title">Section Requisition
                            <br />
                            Approval</span>
                    </div>

                    <div class="dbox__action">
                        <asp:Button ID="btnNewConve2tSO" OnClick="btnNewConvertSO_Click" runat="server" CausesValidation="false" Text="More Info"
                            CssClass="dbox__action__btn" />

                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="dbox dbox--color-1">
                    <div class="dbox__icon">
                        <i class="glyphicon glyphicon-cloud" style="background-color: green;"></i>
                    </div>
                    <div class="dbox__body" style="background-color: green;">
                        <span class="dbox__count">
                            <asp:Label ID="lblActionDue" runat="server" Text="0"></asp:Label>
                        </span>
                        <span class="dbox__title">Territory Requisition
                            <br />
                            Approval</span>
                    </div>

                    <div class="dbox__action">
                        <asp:Button runat="server" CausesValidation="false" ID="Tet" OnClick="Tet_Click" Text="More Info"
                            CssClass="dbox__action__btn" />
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="dbox dbox--color-1">
                    <div class="dbox__icon">
                        <i class="glyphicon glyphicon-cloud" style="background-color: green;"></i>
                    </div>
                    <div class="dbox__body" style="background-color: green;">
                        <span class="dbox__count">
                            <asp:Label ID="lblDispatchIn7" runat="server" Text="0"></asp:Label></span>
                        <span class="dbox__title">Temporary Stock
                            <br />
                            Requisition Approval</span>
                    </div>

                    <div class="dbox__action">
                        <asp:Button runat="server" ID="btnDispatchIn7" OnClick="btnDispatchIn7_Click" CausesValidation="false" Text="More Info"
                            CssClass="dbox__action__btn" />
                    </div>
                </div>
            </div>

            <div class="col-md-2">
                <div class="dbox dbox--color-1">
                    <div class="dbox__icon">
                        <i class="glyphicon glyphicon-cloud" style="background-color: green;"></i>
                    </div>
                    <div class="dbox__body" style="background-color: green;">
                        <span class="dbox__count">
                            <asp:Label ID="lblMaterialIssue" runat="server" Text="0"></asp:Label></span>
                        <span class="dbox__title">Material Issue
                            <br />
                            Notificaton</span>
                    </div>

                    <div class="dbox__action">
                        <asp:Button runat="server" ID="btnMaterialIssue" OnClick="btnMaterialIssue_Click" CausesValidation="false" Text="More Info"
                            CssClass="dbox__action__btn" />
                    </div>
                </div>
            </div>

            <div class="col-md-2">
                <div class="dbox dbox--color-1">
                    <div class="dbox__icon">
                        <i class="glyphicon glyphicon-cloud" style="background-color: green;"></i>
                    </div>
                    <div class="dbox__body" style="background-color: green;">
                        <span class="dbox__count">
                            <asp:Label ID="lblShipment" runat="server" Text="0"></asp:Label></span>
                        <span class="dbox__title">Shipment
                            <br />
                            Notificaton</span>
                    </div>

                    <div class="dbox__action">
                        <asp:Button runat="server" ID="btnShipment" OnClick="btnShipment_Click" CausesValidation="false" Text="More Info"
                            CssClass="dbox__action__btn" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
             <div class="col-md-2">
                <div class="dbox dbox--color-1">
                    <div class="dbox__icon">
                        <i class="glyphicon glyphicon-cloud" style="background-color: green;"></i>
                    </div>
                    <div class="dbox__body" style="background-color: green;">
                        <span class="dbox__count">
                            <asp:Label ID="lblGRNUpdate" runat="server" Text="0"></asp:Label></span>
                        <span class="dbox__title">GRN Update <br />  Notificaton</span>
                    </div>

                    <div class="dbox__action">
                        <asp:Button runat="server" ID="btnGRNUpdate" OnClick="btnGRNUpdate_Click" CausesValidation="false" Text="More Info"
                            CssClass="dbox__action__btn" />
                    </div>
                </div>
            </div>

            <div class="row">
             <div class="col-md-2">
                <div class="dbox dbox--color-1">
                    <div class="dbox__icon">
                        <i class="glyphicon glyphicon-cloud" style="background-color: green;"></i>
                    </div>
                    <div class="dbox__body" style="background-color: green;">
                        <span class="dbox__count">
                            <asp:Label ID="lblWarrantyNoti" runat="server" Text="0"></asp:Label></span>
                        <span class="dbox__title">Warranty  <br />  Notification</span>
                    </div>

                    <div class="dbox__action">
                        <asp:Button runat="server" ID="btnWarrantyNoti" OnClick="btnWarrantyNoti_Click" CausesValidation="false" Text="More Info"
                            CssClass="dbox__action__btn" />
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="w3-container">
        <div id="id03" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-4" style="width:1200px;">
                <div class="w3-container w3-blue">
                    <header>
                        <span onclick="document.getElementById('id03').style.display='none'"
                            class="w3-button w3-display-topright w3-red w3-large w3-hover-red">&times;</span>
                        <h3 align="center">Item Details</h3>
                    </header>
                </div>
                <br />
                <br />

                <div align="center" style="overflow:auto; width:1200px;">
                    <br />
                    <dx:ASPxGridView ID="gvItem" Width="100%" EnableRowsCache="true" Font-Bold="true" SettingsPager-PageSize="10" Theme="Office2003Blue" EnablePagingGestures="True" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" KeyFieldName="ItemId">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="StockInId" Caption="StockIn Id" ReadOnly="True" Settings-AutoFilterCondition="Contains"  VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="POID" Caption="POID" ReadOnly="True" Settings-AutoFilterCondition="Contains"  VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemId" Caption="Item Id" Settings-AutoFilterCondition="Contains"  ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Category" Caption="Category" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemName" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Item Name" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Make" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Make" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Model" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Model" ReadOnly="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="Barcode" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Barcode" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="WarrantyTo" CellStyle-Wrap="True" HeaderStyle-Wrap="True" Caption="Warranty Date" ReadOnly="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                             
                        </Columns>
                        <%-- <SettingsBehavior AllowSelectSingleRowOnly="True" />--%>
                        <Settings ShowFilterRowMenuLikeItem="True" ShowFilterRowMenu="true" ShowFilterRow="true" ShowFooter="true" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" />
                        <SettingsPager ShowNumericButtons="true" PageSizeItemSettings-ShowAllItem="true" Position="Bottom" ShowDisabledButtons="true">
                            <PageSizeItemSettings Items="50, 100" />

                        </SettingsPager>
                        
                    </dx:ASPxGridView>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="Select r.StockInId, s.POID, r.ItemId, im.Category, im.ItemName, im.Make, im.Model, r.Barcode,r.Barcode, convert(varchar,r.WarrantyTo,103) as WarrantyTo FROM tbl_RackStockInBarCodeDetails r join tbl_ItemMaster im on im.ItemId=r.ItemID join tbl_StockIn s on r.StockInId=s.StockInId WHERE (r.Status1='' or r.Status1 is null) and (r.Status2='' or r.Status2 is null) and  r.BranchId=@BranchId and WarrantyTo < DATEADD(MM,4,GETDATE()) ">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <br />

                </div>
            </div>
        </div>
    </div>

</asp:Content>

