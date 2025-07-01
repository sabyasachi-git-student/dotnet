<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteRequisition.aspx.cs" Inherits="pages_GreenOxygen_SiteRequisition" EnableEventValidation="False"%>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Pearl Corporation</title>

    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../plugins/fontawesome-free/css/all.min.css">
    <!-- daterange picker -->
    <link rel="stylesheet" href="../../plugins/daterangepicker/daterangepicker.css">
    <!-- iCheck for checkboxes and radio inputs -->
    <link rel="stylesheet" href="../../plugins/icheck-bootstrap/icheck-bootstrap.min.css">
    <!-- Bootstrap Color Picker -->
    <link rel="stylesheet" href="../../plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.min.css">
    <!-- Tempusdominus Bootstrap 4 -->
    <link rel="stylesheet" href="../../plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css">
    <!-- Select2 -->
    <link rel="stylesheet" href="../../plugins/select2/css/select2.min.css">
    <link rel="stylesheet" href="../../plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css">
    <!-- Bootstrap4 Duallistbox -->
    <link rel="stylesheet" href="../../plugins/bootstrap4-duallistbox/bootstrap-duallistbox.min.css">
    <!-- BS Stepper -->
    <link rel="stylesheet" href="../../plugins/bs-stepper/css/bs-stepper.min.css">
    <!-- dropzonejs -->
    <link rel="stylesheet" href="../../plugins/dropzone/min/dropzone.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../../dist/css/adminlte.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <!-- Site wrapper -->
        <div class="wrapper">
           <nav class="main-header navbar navbar-expand navbar-white navbar-light">
                <!-- Left navbar links -->
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link" data-widget="pushmenu" href="#" role="button"><i class="fas fa-bars"></i></a>
                    </li>
                    <li class="nav-item d-none d-sm-inline-block">
                        <a href="../GreenOxygen/Home.aspx" class="nav-link">Home</a>
                    </li>
                </ul>
                <!-- Right navbar links -->
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item d-none d-sm-inline-block">
                        <a href="../../login.aspx" class="nav-link">Log Out</a>
                    </li>

                </ul>
            </nav>

            <aside class="main-sidebar sidebar-dark-primary elevation-4">
                <!-- Brand Logo -->
                <a href="../GreenOxygen/Home.aspx" class="brand-link">
                    <img src="../../dist/img/AdminLTELogo.png" alt="AdminLTE Logo" class="brand-image img-circle elevation-3" style="opacity: .8">
                    <span class="brand-text font-weight-light">Pearl Corporation</span>
                </a>
                <!-- Sidebar -->
                <div class="sidebar">

                    <nav class="mt-2">
                        <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
                            <!-- Add icons to the links using the .nav-icon class
                                 with font-awesome or any other icon font library -->

                            <li class="nav-item">
                                <a href="../GreenOxygen/Home.aspx" class="nav-link">
                                    <i class="nav-icon fas fa-home"></i>
                                    <p>
                                        Home
                                    </p>
                                </a>
                            </li>

                            <li class="nav-item">
                                <a href="#" class="nav-link">
                                    <i class="nav-icon fas fa-edit"></i>
                                    <p>
                                        Entry
                                        <i class="right fas fa-angle-left"></i>
                                    </p>
                                </a>
                                <ul class="nav nav-treeview">
                                    <li class="nav-item">
                                        <a href="../GreenOxygen/BOQMeasurement.aspx" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>BOQ Mesurement</p>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="../GreenOxygen/Default1.aspx" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>Extra Work Entry</p>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="../GreenOxygen/SiteRequisition.aspx" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>Site Requisition</p>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="../GreenOxygen/BOQDetails_Calculation.aspx" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>Labour Details</p>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="#" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>User Task Feedback</p>
                                        </a>
                                    </li>


                                </ul>
                            </li>

                            <li class="nav-item">
                                <a href="#" class="nav-link">
                                    <i class="nav-icon fas fa-chart-pie"></i>
                                    <p>
                                        Reports
                                        <i class="right fas fa-angle-left"></i>
                                    </p>
                                </a>
                                <ul class="nav nav-treeview">
                                    <li class="nav-item">
                                        <a href="../GreenOxygen/BOQMeasurementReports.aspx" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>BOQ Mesurement Report</p>
                                        </a>
                                    </li>

                                    <li class="nav-item">
                                        <a href="../GreenOxygen/BOQExtraWorkReports.aspx" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>Extra Work Entry Report</p>
                                        </a>
                                    </li>

                                    <li class="nav-item">
                                        <a href="../GreenOxygen/SiteRequisitonReports.aspx" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>Site Requisition Report</p>
                                        </a>
                                    </li>

                                    <li class="nav-item">
                                        <a href="../GreenOxygen/BOQDetails_CalculationReports" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>Labour Details Report</p>
                                        </a>
                                    </li>

                                    <li class="nav-item">
                                        <a href="#" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>Estimation View</p>
                                        </a>
                                    </li>
                                </ul>
                            </li>

                            <li class="nav-item">
                                <a href="#" class="nav-link">
                                    <i class="nav-icon fas fa-chart-pie"></i>
                                    <p>
                                        DownLoads Files
                                        <i class="right fas fa-angle-left"></i>
                                    </p>
                                </a>
                                <ul class="nav nav-treeview">
                                    <li class="nav-item">
                                        <a href="../GreenOxygen/BOQMeasurementFileDownload.aspx" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>BOQ Measurement Files </p>
                                        </a>
                                    </li>

                                    <li class="nav-item">
                                        <a href="../GreenOxygen/BOQExtraWorkFilesDownload.aspx" class="nav-link">
                                            <i class="far fa-circle nav-icon"></i>
                                            <p>BOQ ExtraWork Files</p>
                                        </a>
                                    </li>
                                </ul>
                            </li>

                            <li class="nav-item">
                                <a href="../../login.aspx" class="nav-link">
                                    <i class="nav-icon fas fa-backspace"></i>
                                    <p>
                                        LogOut
                                    </p>
                                </a>
                            </li>
                        </ul>
                    </nav>
                    <!-- /.sidebar-menu -->
                </div>
                <!-- /.sidebar -->
            </aside>
            <div class="content-wrapper">
                <section class="content-header">
                    <div class="container-fluid">
                        <div class="row mb-2">
                            <div class="col-sm-10">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Company :	</label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="ddl_Company" AutoPostBack="true" class="form-control select2" Style="width: 100%; height: 40px;" DataValueField="CompanyId" DataTextField="CompanyName" OnSelectedIndexChanged="ddl_Company_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Project :</label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="ddl_Project" class="form-control select2" Style="width: 100%; height: 40px;" DataValueField="BranchId" DataTextField="BranchName" AutoPostBack="true" OnSelectedIndexChanged="ddl_Project_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.container-fluid -->
                </section>
                <!-- Main content -->
                <section class="content">
                    <!-- Default box -->
                    <div class="card">
                        <div class="card-header">
                            <div class="col-sm-12" align="center">
                                <div class="col-sm-4">
                                    <h3 class="card-title" style="font-size: 35px;">Requisition Entry</h3>
                                </div>
                            </div>

                        </div>

                        <!-- /.card-header -->
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Project :</label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lbl_Project"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Requisition Priority :</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" class="form-control select2" Style="width: 100%;" ID="ddlRequisitionPriority">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                            <asp:ListItem Text="Urgent" Value="Urgent"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Requisited By :</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtEstimatedBy" class="form-control" Style="width: 100%; height: 40px;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BOQ Name :	</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddl_BOQname" AutoPostBack="true" class="form-control select2" Style="width: 100%; height: 40px;" DataValueField="BoqId" DataTextField="BOQName" OnSelectedIndexChanged="ddl_BOQname_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Sr.No :</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddl_Srno" AutoPostBack="true" class="form-control select2" Style="width: 100%; height: 40px;" OnTextChanged="ddl_Srno_TextChanged">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Item Description :</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <dx:ASPxComboBox runat="server" ID="ddl_item" class="form-control select2" Style="width: 100%; height: 40px;" DataValueField="ItemDescripton" DataTextField="ItemDescripton" AutoPostBack="true" OnTextChanged="ddl_item_TextChanged" ValueType="System.String"></dx:ASPxComboBox>
                                        
                                    </div>
                                </div>
                                
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Area : </label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtArea" class="form-control" Style="width: 100%; height: 40px;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Expected
                                            <br />
                                            Start Date : 
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="input-group date" id="dttEntryDate" data-target-input="nearest">
                                            <asp:TextBox runat="server" ID="TextBox1" class="form-control datetimepicker-input" data-target="#reservationdate" Style="width: 88%; height:40px;" Enabled="false"></asp:TextBox>
                                            <div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                                <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Expected
                                            <br />
                                            End Date :</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="input-group date" id="dttEndDate" data-target-input="nearest">
                                            <asp:TextBox runat="server" ID="TextBox2" class="form-control datetimepicker-input" data-target="#reservationdate" Style="width: 88%; height:40px;" Enabled="false"></asp:TextBox>
                                            <div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                                <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Accounting Unit :</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddlUnit" Enabled="false" class="form-control select2" Style="width: 100%; height:40px;" DataValueField="UnitName" DataTextField="UnitName"></asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Date : 
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                       
                                             <dx:ASPxDateEdit ID="dtpRequiredBy" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Style="width: 100%; height:40px;">
                                                                    </dx:ASPxDateEdit>
                                           <%-- <asp:TextBox runat="server" ID="dtpRequiredBy" class="form-control datetimepicker-input" data-target="#reservationdate"></asp:TextBox>--%>
                                            
                                        </div>
                                    
                                </div>

                            </div>


                            <div class="row">
                                <div class="col-12 col-sm-12">
                                    <div class="card card-primary card-tabs">
                                        <div class="card-header p-0 pt-1">
                                            <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                                                <li class="nav-item">
                                                    <a class="nav-link active" id="custom-tabs-one-home-tab" data-toggle="pill" href="#custom-tabs-one-home" role="tab" aria-controls="custom-tabs-one-home" aria-selected="true">Raw Material</a>


                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" id="custom-tabs-one-profile-tab" data-toggle="pill" href="#custom-tabs-one-profile" role="tab" aria-controls="custom-tabs-one-profile" aria-selected="false" style="visibility:hidden">Profile</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" id="custom-tabs-one-messages-tab" data-toggle="pill" href="#custom-tabs-one-messages" role="tab" aria-controls="custom-tabs-one-messages" aria-selected="false" style="visibility:hidden">Messages</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" id="custom-tabs-one-settings-tab" data-toggle="pill" href="#custom-tabs-one-settings" role="tab" aria-controls="custom-tabs-one-settings" aria-selected="false" style="visibility:hidden">Settings</a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="card-body">
                                            <div class="tab-content" id="custom-tabs-one-tabContent" style="overflow: scroll;">
                                                <div class="tab-pane fade show active" id="custom-tabs-one-home" role="tabpanel" aria-labelledby="custom-tabs-one-home-tab">
                                                    <div>
                                                        <asp:GridView ID="gv_StockInDetails" runat="server" Width="90%" AutoGenerateColumns="False" BackColor="White"
                                                                                        BorderColor="#94b6e8" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="15"
                                                                                        AllowPaging="True" GridLines="Vertical" OnPageIndexChanging="gv_StockInDetails_PageIndexChanging"
                                                                                        PageSize="100" OnRowCommand="gv_StockInDetails_RowCommand" OnRowDeleting="gv_StockInDetails_RowDeleting">
                                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox runat="server" BackColor="Yellow" ID="chbxItem" Checked="true" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:BoundField DataField="ItemId" HeaderText="Item Id" />
                                                                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                                                            <asp:BoundField DataField="GroupName" HeaderText="Group" />
                                                                                            <asp:BoundField DataField="CatagoryName" HeaderText="Catagory Name" />


                                                                                            <asp:BoundField DataField="HSNCode" HeaderText="HSN Code" />
                                                                                            <asp:BoundField DataField="UnitName" HeaderText="Primary Unit" />
                                                                                            <asp:BoundField DataField="SecondanyUnit" HeaderText="Secondary Unit" />
                                                                                            <asp:BoundField DataField="RQty" HeaderText="Remaining Qty" />
                                                                                            <asp:BoundField DataField="GRNQty" HeaderText="GRN Qty" />
                                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="txtQty" onkeyup="checkDec(this);" Width="80px" Text='<%#Eval("Qty") %>'></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblQty" Visible="false" Text='<%#Eval("Qty") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField Visible="false" HeaderText="Action">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/Delete.png" Width="25px" Height="25px"
                                                                                                        CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            
                                                                                        </Columns>
                                                                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                                        <HeaderStyle BackColor="#859F6C" Font-Bold="True" ForeColor="Black" />
                                                                                        <PagerStyle BackColor="#94b6e8" ForeColor="Black" HorizontalAlign="Center" />
                                                                                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" />
                                                                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                                        <SortedDescendingHeaderStyle BackColor="#000065" />
                                                                                    </asp:GridView>
                                                    </div>
                                                    
                 
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.card -->
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-lg-12" align="center">
                                    <div class="col-lg-6">
                                        <div class="btn-group w-100">
                                            <asp:Button runat="server" ID="btnSave" Text="Save" Font-Bold="true" class="btn btn-success col fileinput-button" OnClick="btnSave_Click"/>
                                            <div style="width: 20px;"></div>
                                            <%-- <asp:Button runat="server" ID="btnClear" Text="Clear" Font-Bold="true" class="btn btn-warning col cancel" OnClick="btnClear_Click"/> --%>                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- /.card-body -->
                </section>
            </div>

        </div>
        <!-- /.content-wrapper -->

        <!-- Control Sidebar -->
        <aside class="control-sidebar control-sidebar-dark">
            <!-- Control sidebar content goes here -->
        </aside>














        <!-- jQuery -->
        <script src="../../plugins/jquery/jquery.min.js"></script>
        <!-- Bootstrap 4 -->
        <script src="../../plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
        <!-- Select2 -->
        <script src="../../plugins/select2/js/select2.full.min.js"></script>
        <!-- Bootstrap4 Duallistbox -->
        <script src="../../plugins/bootstrap4-duallistbox/jquery.bootstrap-duallistbox.min.js"></script>
        <!-- InputMask -->
        <script src="../../plugins/moment/moment.min.js"></script>
        <script src="../../plugins/inputmask/jquery.inputmask.min.js"></script>
        <!-- date-range-picker -->
        <script src="../../plugins/daterangepicker/daterangepicker.js"></script>
        <!-- bootstrap color picker -->
        <script src="../../plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.min.js"></script>
        <!-- Tempusdominus Bootstrap 4 -->
        <script src="../../plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js"></script>
        <!-- Bootstrap Switch -->
        <script src="../../plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
        <!-- BS-Stepper -->
        <script src="../../plugins/bs-stepper/js/bs-stepper.min.js"></script>
        <!-- dropzonejs -->
        <script src="../../plugins/dropzone/min/dropzone.min.js"></script>
        <!-- AdminLTE App -->
        <script src="../../dist/js/adminlte.min.js"></script>
        <!-- AdminLTE for demo purposes -->
        <script src="../../dist/js/demo.js"></script>

        <script>
            $(function () {
                //Initialize Select2 Elements
                $('.select2').select2()

                //Initialize Select2 Elements
                $('.select2bs4').select2({
                    theme: 'bootstrap4'
                })

                //Datemask dd/mm/yyyy
                $('#datemask').inputmask('dd/mm/yyyy', { 'placeholder': 'dd/mm/yyyy' })
                //Datemask2 mm/dd/yyyy
                $('#datemask2').inputmask('mm/dd/yyyy', { 'placeholder': 'mm/dd/yyyy' })
                //Money Euro
                $('[data-mask]').inputmask()

                //Date picker
                $('#reservationdate').datetimepicker({
                    format: 'L'
                });

                //Date and time picker
                $('#reservationdatetime').datetimepicker({ icons: { time: 'far fa-clock' } });

                //Date range picker
                $('#reservation').daterangepicker()
                //Date range picker with time picker
                $('#reservationtime').daterangepicker({
                    timePicker: true,
                    timePickerIncrement: 30,
                    locale: {
                        format: 'MM/DD/YYYY hh:mm A'
                    }
                })
                //Date range as a button
                $('#daterange-btn').daterangepicker(
                  {
                      ranges: {
                          'Today': [moment(), moment()],
                          'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                          'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                          'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                          'This Month': [moment().startOf('month'), moment().endOf('month')],
                          'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                      },
                      startDate: moment().subtract(29, 'days'),
                      endDate: moment()
                  },
                  function (start, end) {
                      $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'))
                  }
                )

                //Timepicker
                $('#timepicker').datetimepicker({
                    format: 'LT'
                })

                //Bootstrap Duallistbox
                $('.duallistbox').bootstrapDualListbox()

                //Colorpicker
                $('.my-colorpicker1').colorpicker()
                //color picker with addon
                $('.my-colorpicker2').colorpicker()

                $('.my-colorpicker2').on('colorpickerChange', function (event) {
                    $('.my-colorpicker2 .fa-square').css('color', event.color.toString());
                })

                $("input[data-bootstrap-switch]").each(function () {
                    $(this).bootstrapSwitch('state', $(this).prop('checked'));
                })

            })
            // BS-Stepper Init
            document.addEventListener('DOMContentLoaded', function () {
                window.stepper = new Stepper(document.querySelector('.bs-stepper'))
            })

            // DropzoneJS Demo Code Start
            Dropzone.autoDiscover = false

            // Get the template HTML and remove it from the doumenthe template HTML and remove it from the doument
            var previewNode = document.querySelector("#template")
            previewNode.id = ""
            var previewTemplate = previewNode.parentNode.innerHTML
            previewNode.parentNode.removeChild(previewNode)

            var myDropzone = new Dropzone(document.body, { // Make the whole body a dropzone
                url: "/target-url", // Set the url
                thumbnailWidth: 80,
                thumbnailHeight: 80,
                parallelUploads: 20,
                previewTemplate: previewTemplate,
                autoQueue: false, // Make sure the files aren't queued until manually added
                previewsContainer: "#previews", // Define the container to display the previews
                clickable: ".fileinput-button" // Define the element that should be used as click trigger to select files.
            })

            myDropzone.on("addedfile", function (file) {
                // Hookup the start button
                file.previewElement.querySelector(".start").onclick = function () { myDropzone.enqueueFile(file) }
            })

            // Update the total progress bar
            myDropzone.on("totaluploadprogress", function (progress) {
                document.querySelector("#total-progress .progress-bar").style.width = progress + "%"
            })

            myDropzone.on("sending", function (file) {
                // Show the total progress bar when upload starts
                document.querySelector("#total-progress").style.opacity = "1"
                // And disable the start button
                file.previewElement.querySelector(".start").setAttribute("disabled", "disabled")
            })

            // Hide the total progress bar when nothing's uploading anymore
            myDropzone.on("queuecomplete", function (progress) {
                document.querySelector("#total-progress").style.opacity = "0"
            })

            // Setup the buttons for all transfers
            // The "add files" button doesn't need to be setup because the config
            // `clickable` has already been specified.
            document.querySelector("#actions .start").onclick = function () {
                myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED))
            }
            document.querySelector("#actions .cancel").onclick = function () {
                myDropzone.removeAllFiles(true)
            }
            // DropzoneJS Demo Code End
        </script>
    </form>
</body>
</html>
