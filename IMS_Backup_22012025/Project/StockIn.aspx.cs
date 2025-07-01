using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Collections;



public partial class Project_StockIn : System.Web.UI.Page
{
    static ArrayList ArrLstVSBarcodes = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItem();
        }
       
        if (ReferenceEquals(ViewState["Dynamic_Image_2"], null))
        {
            ViewState["Dynamic_Image_2"] = CraeteUploadImageTable();
        }

        if (ReferenceEquals(ViewState["Barcode"], null))
        {
            ViewState["Barcode"] = Barcode();
        }
      
        if (!IsPostBack)
        {
            Session["POID"] = null;
            ArrLstVSBarcodes.Clear();

            string UserId = "";
            string BranchId = "";
            string UserGroup = "";
            Session["UserGroupId1"] = "";
            try
            {
                UserGroup = Session["UserGroupId"].ToString();
                string UserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='" + UserGroup + "'");

                string ToUserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='UG15'");
            }
            catch

            { }
            //try
            //{
            //    UserId = Session["UserId"].ToString();

            //    string UserName = DBAccess.FetchDatasingle("select ug.UserGroup from tbl_User u join tbl_UserGroups ug on u.UserGroup=ug.UserGroupId  where UserName='" + UserId + "'");
            //    if (UserName == "SuperAdmin")
            //    {
            //        dtpDate.Date = DateTime.Now;
            //    }
            //    else
            //    {
            //        dtpDate.Date = DateTime.Now;
            //        dtpDate.Enabled = false;
            //    }
            //}
            //catch

            //{ }
            try
            {
                BranchId = Session["BranchId"].ToString();
                string BranchName = DBAccess.FetchDatasingle("select BranchName from tbl_Branch where BranchId='" + BranchId + "'"); Session["BranchName"] = BranchName.ToString();

            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
                return;
            }

        }
    }

    protected void btn_Corres_Click(object sender, EventArgs e)
    {
        Guid gd;
        gd = Guid.NewGuid();
        string guid = gd.ToString();

        string FileName3 = "";

        if (fuDoc.HasFile)
        {
            try
            {
                FileName3 = Path.GetFileName(fuDoc.FileName);
                string strPath = Server.MapPath("~");
                strPath = strPath + "\\Project\\IMG\\" + guid + '_' + FileName3;
                fuDoc.SaveAs(strPath);

            }
            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Files');", true);
                return;
            }
        }



        DataTable dt = (DataTable)ViewState["Dynamic_Image_2"];
        DataRow dr = dt.NewRow();
        dr["Image"] = FileName3;
        dr["guid"] = guid;
        //dr["Title"] = Title;
        dt.Rows.Add(dr);
        dt.AcceptChanges();
        dt = (DataTable)ViewState["Dynamic_Image_2"];

        gv_Correspondance.DataSource = dt;
        gv_Correspondance.DataBind();

    }
    protected void gv_Correspondance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ToggleRowSelection(object sender, EventArgs e)
    {
        //ValidateCheckbox();
    }

    protected void ValidateCheckbox()
    {
        //bool IsVisible = false;

        //if (chkProjectRequirement.Checked)
        //{
        //    IsVisible = true;
        //}
        //else
        //{
        //    ddlProjectName.Text = null;
        //}
        //ddlProjectName.Visible = IsVisible;
        ////lblProjectName.Visible = IsVisible;
    }
    protected void gv_Correspondance_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Delete")
        {
            DataTable dt = (DataTable)ViewState["Dynamic_Image_2"];
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            string imagepath = dt.Rows[rowindex]["Image"].ToString();
            string strPath = Server.MapPath("/Project/IMG/");
            strPath = strPath + imagepath;
            FileInfo file = new FileInfo(strPath);
            if (file.Exists)
            {
                file.Delete();
            }
            dt.Rows[rowindex].Delete();
            dt.AcceptChanges();
            dt = (DataTable)ViewState["Dynamic_Image_2"];

            gv_Correspondance.DataSource = dt;
            gv_Correspondance.DataBind();
        }
    }
    public DataTable CraeteUploadImageTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Image", typeof(string));
        dt.Columns.Add("guid", typeof(string));
        dt.Columns.Add("Title", typeof(string));
        dt.AcceptChanges();
        return dt;
    }
    protected DataTable getItem()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Category";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Type";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Make";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Model";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Unit";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Qty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "rate";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "HSNCode";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "IGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "SGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Warranty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "txtWarrantyTo";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CoderLifeTo";
        oTable.Columns.Add(dtCol);

        return oTable;
    }

    protected DataTable Barcode()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "itemid";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "type";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "TempBarcode";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Warranty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "WarrantyTo";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CoderLifeTo";
        oTable.Columns.Add(dtCol);

        return oTable;
    }

    protected void btnBarCodeSave_Click(object sender, EventArgs e)
    {

        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;

        DataTable dtTempBarcodes = new DataTable();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {

            string Barcode = ((TextBox)GridView1.Rows[i].FindControl("txtBarCode")).Text;
            if (Barcode == "")
            {
                lblError11.Text = "Missing Barcode";
                ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('dvBarcodes').style.display='block'", true);
                return;
            }

            string SrBarVodeID = DBAccess.FetchDatasingle(" select Barcode from tbl_RackStockInBarCodeDetails where BranchId='" + BranchId + "' and Barcode='" + Barcode + "' ");

            if (SrBarVodeID != "")
            {
                lblError11.Text = " '" + SrBarVodeID + "' Already Have";
                ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('dvBarcodes').style.display='block'", true);
                return;
            }

           
            
            dtTempBarcodes = (DataTable)ViewState["Barcode"];
            DataRow drH = dtTempBarcodes.NewRow();

            drH["itemid"] = ViewState["ItemIdForBarcode"].ToString();
            drH["type"] = ViewState["typeForBarcode"].ToString();
            drH["Warranty"] = ViewState["Warranty"].ToString();
            drH["WarrantyTo"] = ViewState["WarrantyTo"].ToString();
            drH["CoderLifeTo"] = ViewState["CoderLifeTo"].ToString();

            drH["TempBarcode"] = Barcode;

            dtTempBarcodes.Rows.Add(drH);
        }
        string ViewStateName = "TempBarcodes" + ViewState["ItemIdForBarcode"].ToString() + ViewState["Warranty"].ToString() + ViewState["WarrantyTo"].ToString() + ViewState["CoderLifeTo"].ToString();
        
        
        ViewState[ViewStateName] = dtTempBarcodes;
        Gv_Barcode.DataSource = dtTempBarcodes;
        Gv_Barcode.DataBind();

        ArrLstVSBarcodes.Add(ViewState[ViewStateName]);

        int rowindex = Convert.ToInt32(ViewState["gvItemDetailsrowindex"].ToString());
        ((Button)gvItemDetails.Rows[rowindex].FindControl("btnBarcode")).Text = "Generated";
        ((Button)gvItemDetails.Rows[rowindex].FindControl("btnBarcode")).Enabled = false;


    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState.Text = "";
        string SupplierId = "";
        try
        {
            SupplierId = ddlSupplier.SelectedItem.Value.ToString();
        }
        catch { }
        if (SupplierId != "")
        {
            DataSet ds = ClassSupplierMaster.FetchSupplierEntry(SupplierId);
            if (ds != null)
            {
                try
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string StateCode = dt.Rows[0]["StateCode"].ToString();

                        ddlState.Value = StateCode;


                    }
                }
                catch { }
            }
        }
        else
        {

        }


    }
    protected void btnChooseRawMaterial_Click(object sender, EventArgs e)
    {
        string SupplierId = "";
        try
        {
            SupplierId = ddlSupplier.SelectedItem.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please select Supplier First!');", true);
            return;
        }

        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
    }
    protected void gvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemDetails.PageIndex = e.NewPageIndex;
        this.gvItemDetails.DataSource = (DataTable)ViewState["ItemDetails"];
        this.gvItemDetails.DataBind();
    }
    protected void gvItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt8 = new DataTable();
            dt8 = (DataTable)ViewState["ItemDetails"];
            dt8.Rows[rowindex].Delete();
            dt8.AcceptChanges();
            ViewState["ItemDetails"] = dt8;
            gvItemDetails.DataSource = dt8;
            gvItemDetails.DataBind();
        }

        else if (e.CommandName == "Barcode")
        {
            string UserId = "";
            string BranchId = "";
           
            try
            {
                UserId = Session["UserId"].ToString();
                BranchId = Session["BranchId"].ToString();
            }
            catch { }
            DateTime DOE = DateTime.Now;

            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["gvItemDetailsrowindex"] = rowindex;//For disabling Generate Button in gvItemDetails gridview from Popup of NonBarcodes Barcode after Barcodes are Generated in temp dt after Entry(by Scanning) this rowindex is required in btnBarCodeSave_Click Method)
            string quantity = ((TextBox)gvItemDetails.Rows[rowindex].FindControl("txtItemQty")).Text;
            if (quantity == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Quantity.!');", true);
                return;
            }
            else
            {
                string itemid = ((Label)gvItemDetails.Rows[rowindex].FindControl("lblItemId")).Text;
                string type = gvItemDetails.Rows[rowindex].Cells[2].Text;
                string Warranty = ((TextBox)gvItemDetails.Rows[rowindex].FindControl("txtWarranty")).Text;
                string WarrantyTo = ((TextBox)gvItemDetails.Rows[rowindex].FindControl("txtWarrantyTo")).Text;
                string CoderLifeTo = ((TextBox)gvItemDetails.Rows[rowindex].FindControl("txtCoderLifeTo")).Text;

                string stockinid = DBAccess.FetchDatatable("select [dbo].[fn_StockInId]()").Rows[0][0].ToString();


                ViewState["ItemIdForBarcode"] = itemid;
                ViewState["typeForBarcode"] = type;
                ViewState["StockInIdForBarcode"] = stockinid;
                ViewState["Warranty"] = Warranty;
                ViewState["WarrantyTo"] = WarrantyTo;
                ViewState["CoderLifeTo"] = CoderLifeTo;
                string txtWarrantyToState = ViewState["WarrantyTo"].ToString();

                if(txtWarrantyToState =="")
                {
                     ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Calculate The Grid First');", true);
                    return;

                }
                

                if (ViewState["typeForBarcode"].ToString() == "NonBarcode")
                {
                    DataTable dtBarcodes = new DataTable();
                    DataColumn dtCol = new DataColumn();

                    dtCol = new DataColumn();
                    dtCol.ColumnName = "RowNumber";
                    dtBarcodes.Columns.Add(dtCol);

                    dtCol = new DataColumn();
                    dtCol.ColumnName = "Barcode";
                    dtBarcodes.Columns.Add(dtCol);

                    for (int i = 1; i <= Convert.ToInt32(quantity); i++) //No. Of textboxes in Popup = Value of Quantity entered.
                    {
                        DataRow drH = dtBarcodes.NewRow();
                        drH["RowNumber"] = i.ToString();
                        drH["Barcode"] = "";
                        dtBarcodes.Rows.Add(drH);
                    }

                    GridView1.DataSource = dtBarcodes;
                    GridView1.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('dvBarcodes').style.display='block'", true);

                }

                else if (ViewState["typeForBarcode"].ToString() == "Barcode")
                {
                    DataTable dtTempBarcodes = new DataTable();
                    int Count = Convert.ToInt32(DBAccess.FetchDatatable("select [dbo].[fn_Barcode]()").Rows[0][0].ToString());
                    for (int i = 0; i < Convert.ToInt32(quantity); i++)
                    {

                        string Barcode = DateTime.Now.ToString("yyyy/MM/dd") + "/" + Count;
                        dtTempBarcodes = (DataTable)ViewState["Barcode"];
                        DataRow drH = dtTempBarcodes.NewRow();

                        drH["itemid"] = ViewState["ItemIdForBarcode"].ToString();
                        drH["type"] = ViewState["typeForBarcode"].ToString();
                        drH["Warranty"] = ViewState["Warranty"].ToString();
                        drH["WarrantyTo"] = ViewState["WarrantyTo"].ToString();
                        drH["CoderLifeTo"] = ViewState["CoderLifeTo"].ToString();

                        drH["TempBarcode"] = Barcode;

                        dtTempBarcodes.Rows.Add(drH);
                        Gv_Barcode.DataSource = dtTempBarcodes;
                        Gv_Barcode.DataBind();

                        bool SaveDetails = DBAccess.SaveData(@" insert into [dbo].[tbl_BarcodeGenarate] values ( '" + stockinid + "', '" + itemid + "', '" + type + "', '" + Barcode + "', '" + UserId + "', '" + BranchId + "', convert(datetime,'" + DOE + "',103))");

                        Count++;
                    }
                    string ViewStateName = "TempBarcodes" + itemid;
                    ViewState[ViewStateName] = dtTempBarcodes;
                    ArrLstVSBarcodes.Add(ViewState[ViewStateName]);

                    ((Button)gvItemDetails.Rows[rowindex].FindControl("btnBarcode")).Text = "Generated";
                    ((Button)gvItemDetails.Rows[rowindex].FindControl("btnBarcode")).Enabled = false;

                }

            }
        }
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnCalculate_Click(btnCalculate, e);
        TextBox txtTotalAmount = gvItemDetails.FooterRow.FindControl("txtTotalAmount") as TextBox;
        string StockInId = DBAccess.FetchDatatable("select [dbo].[fn_StockInId]()").Rows[0][0].ToString();
        string SupplierId = "";
        try
        {
            SupplierId = ddlSupplier.SelectedItem.Value.ToString();
        }
        catch { }
        if (SupplierId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select a Supplier.!');", true);
            return;
        }
        string ProjectId = "";
        string ProjectName = "";
        try
        {
            ProjectId = ddlProjectName.SelectedItem.Value.ToString();
            ProjectName = ddlProjectName.SelectedItem.Text;
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select a Project.!');", true);
            return;
        }
        string StockInDate = dtpDate.Text;
        if (StockInDate == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select Stock In Date.!');", true);
            return;
        }
        string POID = txtPO_ID.Text;
        if (POID == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Po No!');", true);
            return;
        }

        string StateCode = "";
        try
        {
            StateCode = ddlState.SelectedItem.Value.ToString();
        }
        catch { }
        if (StateCode == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select a State.!');", true);
            return;
        }
        string InvoiceNo = txtInvoiceNo.Text;
        string ChallanNo = txtChallanNo.Text;

        string InvoiceDate = dtpInvoiceDate.Text;

        string ChallanDate = dtpChallanDate.Text;


        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;
        if (gvItemDetails.Rows.Count > 0)
        {

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please add atleast one Item.!');", true);
            return;
        }
        string PurchaseBillId = DBAccess.FetchDatatable("select [dbo].[fn_PurchaseBillId]()").Rows[0][0].ToString();

        string ConsignmentNO = txtConsignmentNO.Text;
        if (ConsignmentNO == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Consignment NO.!');", true);
            return;
        }

        decimal GrandTotal = 0;
        try
        {
            GrandTotal = Convert.ToDecimal(txtTotalAmount.Text);
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Total Amount must be numeric.');", true);
            return;
        }
        if (GrandTotal <= 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Total Amount must be greater than zero.');", true);
            return;
        }

        decimal Freight = Convert.ToDecimal(txtFreigh.Text);
        decimal OtherCharges = Convert.ToDecimal(txtOtherCharges.Text);
        decimal TotalBillAmount = Convert.ToDecimal(txtTotalBillAmount.Text);
        decimal FreightCGST = Convert.ToDecimal(txtFreightCGST.Text);
        decimal FreightSGST = Convert.ToDecimal(txtFreightSGST.Text);
        decimal FreightIGST = Convert.ToDecimal(txtFreightIGST.Text);
        decimal FreightWithGST = Convert.ToDecimal(txtFreightWithGST.Text);
        string GRNNo = txtGRNNo.Text;
        string PODate = dtpPODate.Text;
        string DeliveryBy = txtDeliveryBy.Text;
        string ReceiveBy = txtReceiveBy.Text;
        string Mobile = txtMobile.Text;
        string VehicleNo = txtVehicleNo.Text;
        string RackSpaceId = "";
        string BarcodeStatus = "";
        int m = 0;
        /////int b = ClassStockIn.SaveBarcodes(StockInId, ItemId, Type, Barcode, UserId, BranchId, DOE);
        int n = ClassStockIn.Save(StockInId, GRNNo, SupplierId, StockInDate, ChallanNo, ChallanDate, InvoiceNo, InvoiceDate, POID, ConsignmentNO, PODate, DeliveryBy, ReceiveBy, Mobile, VehicleNo, StateCode, UserId, BranchId, DOE, ProjectId, ProjectName);

        int n1 = ClassPurchaseBillEntry.Save(PurchaseBillId, StockInId, SupplierId, InvoiceNo, InvoiceDate, StateCode, GrandTotal, Freight, OtherCharges, TotalBillAmount, FreightCGST, FreightSGST, FreightIGST, FreightWithGST, UserId, BranchId, DOE);

        if (n == 1)
        {
            if (ViewState["Dynamic_Image_2"] != null)
            {
                DataTable dt1 = ViewState["Dynamic_Image_2"] as DataTable;
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string image = dt1.Rows[i]["image"].ToString();
                        string guid1 = dt1.Rows[i]["guid"].ToString();

                        int nn = ClassBrokerVerificationScanCopy.LeadSiteVisitScanCopy(image, guid1, StockInId, GRNNo);
                    }
                }
            }

            for (int j = 0; j < gvItemDetails.Rows.Count; j++)
            {
                string StockInDetailsId = DBAccess.FetchDatatable("select [dbo].[fn_StockInDetailsId]()").Rows[0][0].ToString();

                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[j].FindControl("txtItemQty");
                TextBox txtItemRQty = (TextBox)gvItemDetails.Rows[j].FindControl("txtItemRQty");
                TextBox txtAmount = (TextBox)gvItemDetails.Rows[j].FindControl("txtAmount");
                TextBox txtRate = (TextBox)gvItemDetails.Rows[j].FindControl("txtRate");
                Label lblItemId = (Label)gvItemDetails.Rows[j].FindControl("lblItemId");

                CheckBox chbxCGST = gvItemDetails.Rows[j].FindControl("chbxCGST") as CheckBox;
                CheckBox chbxSGST = gvItemDetails.Rows[j].FindControl("chbxSGST") as CheckBox;
                CheckBox chbxIGST = gvItemDetails.Rows[j].FindControl("chbxIGST") as CheckBox;
                CheckBox chbxCESS = gvItemDetails.Rows[j].FindControl("chbxCESS") as CheckBox;

                TextBox txtCGST = (TextBox)gvItemDetails.Rows[j].FindControl("txtCGST");
                TextBox txtSGST = (TextBox)gvItemDetails.Rows[j].FindControl("txtSGST");
                TextBox txtIGST = (TextBox)gvItemDetails.Rows[j].FindControl("txtIGST");
                TextBox txtCESS = (TextBox)gvItemDetails.Rows[j].FindControl("txtCESS");

                TextBox txtTotal = (TextBox)gvItemDetails.Rows[j].FindControl("txtTotal");
                string ItemId = lblItemId.Text;
                string HSNCode = StockInDetailsId;

                TextBox txtWarranty = (TextBox)gvItemDetails.Rows[j].FindControl("txtWarranty");
                TextBox txtWarrantyTo = (TextBox)gvItemDetails.Rows[j].FindControl("txtWarrantyTo");
                TextBox txtCoderLifeTo = (TextBox)gvItemDetails.Rows[j].FindControl("txtCoderLifeTo");

                //#region CoderLife
                //string ItemCoderLife = DBAccess.FetchDatasingle("select distinct CoderLife from tbl_ItemMaster where ItemId='" + ItemId + "'");
                //int CoderLife = Convert.ToInt32(ItemCoderLife);
                //DateTime CoderLifeendDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(CoderLife);
                //txtCoderLifeTo.Text = CoderLifeendDate.ToString();

                //#endregion

                //#region Warranty Date
                //int Months = Convert.ToInt32(txtWarranty.Text);
                //DateTime WarrantyendDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(Months);
                //txtWarrantyTo.Text = WarrantyendDate.ToString();
                //#endregion

                string Warranty = txtWarranty.Text;
                string WarrantyTo = txtWarrantyTo.Text;
                string CoderLifeTo = txtCoderLifeTo.Text;

                Session["Warranty"] = Warranty.ToString();
                Session["WarrantyTo"] = WarrantyTo.ToString();
                Session["CoderLifeTo"] = CoderLifeTo.ToString();



                decimal RQty = 0;
                try
                {
                    RQty = Convert.ToDecimal(txtItemRQty.Text);
                }
                catch { }
                decimal Qty = 0;
                try
                {
                    Qty = Convert.ToDecimal(txtItemQty.Text);
                }
                catch { }

                if (POID != "")
                {
                    //if (Qty > RQty)
                    //{
                    //    bool Delete_tbl_StockIn = DBAccess.SaveData("delete from tbl_StockIn where StockInId='" + StockInId + "'");
                    //    bool Delete_tbl_StockInDetails = DBAccess.SaveData("delete from tbl_StockInDetails where StockInId='" + StockInId + "'");
                    //    bool Delete_tbl_PurchaseBillEntry = DBAccess.SaveData("delete from tbl_PurchaseBillEntry  where StockInId='" + StockInId + "'");
                    //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Check Quantity!');", true);
                    //    return;
                    //}
                }
                else
                {
                    Qty = Convert.ToDecimal(txtItemQty.Text);
                }

                decimal Rate = 0;
                try
                {
                    Rate = Convert.ToDecimal(txtRate.Text);
                }
                catch { }

                decimal Total = Math.Round((Qty * Rate), 2);
                txtAmount.Text = Total.ToString();
                decimal Amount = 0;
                try
                {
                    Amount = Convert.ToDecimal(txtAmount.Text);
                }
                catch { }


                decimal CGSTPer = 0;
                decimal CGSTAmount = 0;
                if (chbxCGST.Checked == true)
                {
                    try
                    {
                        CGSTPer = Convert.ToDecimal(chbxCGST.Text);
                    }
                    catch { }
                    CGSTAmount = Math.Round(((Total * CGSTPer) / 100), 2);
                    txtCGST.Text = CGSTAmount.ToString();
                }

                decimal SGSTPer = 0;
                decimal SGSTAmount = 0;
                if (chbxSGST.Checked == true)
                {
                    try
                    {
                        SGSTPer = Convert.ToDecimal(chbxSGST.Text);
                    }
                    catch { }
                    SGSTAmount = Math.Round(((Total * SGSTPer) / 100), 2);
                    txtSGST.Text = SGSTAmount.ToString();
                }

                decimal IGSTPer = 0;
                decimal IGSTAmount = 0;
                if (chbxIGST.Checked == true)
                {
                    try
                    {
                        IGSTPer = Convert.ToDecimal(chbxIGST.Text);
                    }
                    catch { IGSTPer = 0; }
                    IGSTAmount = Math.Round(((Total * IGSTPer) / 100), 2);
                    txtIGST.Text = IGSTAmount.ToString();

                    txtSGST.Text = "0";
                    txtCGST.Text = "0";
                    CGSTAmount = 0;
                    SGSTAmount = 0;
                    chbxSGST.Checked = false;
                    chbxCGST.Checked = false;
                }

                decimal CESSPer = 0;
                decimal CESSAmount = 0;

                Total = Total + CGSTAmount + SGSTAmount + IGSTAmount + CESSAmount;
                txtTotal.Text = Math.Round(Total, 2).ToString();
                GrandTotal = GrandTotal + Total;

                txtTotalAmount.Text = Math.Round(GrandTotal, 2).ToString();

                int y = ClassPurchaseBillEntry.SaveDetails(PurchaseBillId, ItemId, HSNCode, Qty, Rate, Amount, CGSTPer, CGSTAmount, SGSTPer, SGSTAmount, IGSTPer, IGSTAmount, CESSPer, CESSAmount, Total, UserId, BranchId, DOE);

               m = ClassStockIn.SaveDetails(StockInId, StockInDetailsId, ItemId, Qty, POID, BarcodeStatus, Warranty, UserId, BranchId, DOE);

                
                //    foreach (var vstate in ArrLstVSBarcodes)
                //    {
                        
                //        DataTable dt = (DataTable)vstate;
                //        for (int i = 0; i < dt.Rows.Count; i++)
                //        {
                //            string SrBarVodeID = DBAccess.FetchDatatable("select [dbo].[fn_SrBarVodeID]()").Rows[0][0].ToString();
                //            int b = ClassStockIn.SaveBarcodes(StockInId, SrBarVodeID, dt.Rows[i]["itemid"].ToString(), dt.Rows[i]["type"].ToString(), dt.Rows[i]["TempBarcode"].ToString(), dt.Rows[i]["Warranty"].ToString(), dt.Rows[i]["WarrantyTo"].ToString(), dt.Rows[i]["CoderLifeTo"].ToString(), UserId, BranchId, DOE);

                //        }
                //    }
                
                //ArrLstVSBarcodes.Clear();
                
            }

            if (m == 1)

                for (int a = 0; a < Gv_Barcode.Rows.Count; a++)
                {
                    string ItemIda = Gv_Barcode.Rows[a].Cells[0].Text;
                    string type = Gv_Barcode.Rows[a].Cells[1].Text;
                    string Warranty1 = Gv_Barcode.Rows[a].Cells[2].Text;
                    string WarrantyTo1 = Gv_Barcode.Rows[a].Cells[3].Text;
                    string CoderLifeTo1 = Gv_Barcode.Rows[a].Cells[4].Text;
                    string TempBarcode = Gv_Barcode.Rows[a].Cells[5].Text;


                    string SrBarVodeID = DBAccess.FetchDatatable("select [dbo].[fn_SrBarVodeID]()").Rows[0][0].ToString();
                    int b = ClassStockIn.SaveBarcodes(StockInId, SrBarVodeID, ItemIda, type, TempBarcode, Warranty1, WarrantyTo1, CoderLifeTo1, UserId, BranchId, DOE);
                }


            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Stock In is done successfully..');window.location='../Project/RackTag_BarcodeGen.aspx';", true);
        }

        else if (n == -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Challan No is already exist!');", true);
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Stock In is not done. Try again!');", true);
            return;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockIn.aspx");
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {

        int count = gvItemDetails.Rows.Count;
        if (count == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('At Least Add one Item !');", true);
            return;
        }
        if (gvItemDetails.Rows.Count > 0)
        {
            decimal Freight = Convert.ToDecimal(txtFreigh.Text);
            TextBox txtTotalAmount = (TextBox)gvItemDetails.FooterRow.FindControl("txtTotalAmount");
            txtTotalAmount.Text = "0";
            decimal GrandTotal = 0;
            int rowsCount = gvItemDetails.Rows.Count;
            for (int j = 0; j < rowsCount; j++)
            {
                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[j].FindControl("txtItemQty");
                TextBox txtAmount = (TextBox)gvItemDetails.Rows[j].FindControl("txtAmount");
                TextBox txtRate = (TextBox)gvItemDetails.Rows[j].FindControl("txtRate");
                Label lblItemId = (Label)gvItemDetails.Rows[j].FindControl("lblItemId");



                CheckBox chbxCGST = gvItemDetails.Rows[j].FindControl("chbxCGST") as CheckBox;
                CheckBox chbxSGST = gvItemDetails.Rows[j].FindControl("chbxSGST") as CheckBox;
                CheckBox chbxIGST = gvItemDetails.Rows[j].FindControl("chbxIGST") as CheckBox;

                TextBox txtCGST = (TextBox)gvItemDetails.Rows[j].FindControl("txtCGST");
                TextBox txtSGST = (TextBox)gvItemDetails.Rows[j].FindControl("txtSGST");
                TextBox txtIGST = (TextBox)gvItemDetails.Rows[j].FindControl("txtIGST");
                TextBox txtTotal = (TextBox)gvItemDetails.Rows[j].FindControl("txtTotal");

                TextBox txtWarranty = (TextBox)gvItemDetails.Rows[j].FindControl("txtWarranty");
                TextBox txtWarrantyTo = (TextBox)gvItemDetails.Rows[j].FindControl("txtWarrantyTo");
                TextBox txtCoderLifeTo = (TextBox)gvItemDetails.Rows[j].FindControl("txtCoderLifeTo");

                #region CoderLife
                string ItemId = lblItemId.Text;
                string ItemCoderLife = DBAccess.FetchDatasingle("select distinct CoderLife from tbl_ItemMaster where ItemId='" + ItemId + "'");
                int CoderLife = Convert.ToInt32(ItemCoderLife);
                DateTime CoderLifeendDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(CoderLife);
                txtCoderLifeTo.Text = CoderLifeendDate.ToString();
                #endregion

                #region Warranty Date
                int Months = Convert.ToInt32(txtWarranty.Text);
                DateTime WarrantyendDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(Months);
                txtWarrantyTo.Text = WarrantyendDate.ToString();
                #endregion

                decimal Qty = 0;
                try
                {
                    Qty = Convert.ToDecimal(txtItemQty.Text);
                }
                catch { }

                decimal Rate = 0;
                try
                {
                    Rate = Convert.ToDecimal(txtRate.Text);
                }
                catch { }
                decimal Total = Math.Round((Qty * Rate), 2);
                txtAmount.Text = Total.ToString();


                decimal CGST = 0;
                decimal CGSTValue = 0;
                decimal FreightCGST = 0;
                if (chbxCGST.Checked == true)
                {
                    try
                    {
                        CGST = Convert.ToDecimal(chbxCGST.Text);

                    }
                    catch { }
                    CGSTValue = Math.Round(((Total * CGST) / 100), 2);
                    txtCGST.Text = CGSTValue.ToString();
                    FreightCGST = (Freight * CGST) / 100;
                    txtFreightCGST.Text = FreightCGST.ToString();


                }
                else
                {
                    txtCGST.Text = "0";
                    txtFreightCGST.Text = "0";
                }
                decimal SGST = 0;
                decimal SGSTValue = 0;
                decimal FreightSGST = 0;
                if (chbxSGST.Checked == true)
                {
                    try
                    {
                        SGST = Convert.ToDecimal(chbxSGST.Text);
                    }
                    catch { }
                    SGSTValue = Math.Round(((Total * SGST) / 100), 2);
                    txtSGST.Text = SGSTValue.ToString();
                    FreightSGST = (Freight * SGST) / 100;
                    txtFreightSGST.Text = FreightSGST.ToString();
                }
                else
                {
                    txtSGST.Text = "0";
                    txtFreightSGST.Text = "0";
                }
                decimal IGST = 0;
                decimal IGSTValue = 0;
                decimal FreightIGST = 0;
                if (chbxIGST.Checked == true)
                {
                    try
                    {
                        IGST = Convert.ToDecimal(chbxIGST.Text);
                    }
                    catch
                    {
                        IGST = 0;
                        txtFreightIGST.Text = "0";

                    }

                    IGSTValue = Math.Round(((Total * IGST) / 100), 2);
                    txtIGST.Text = IGSTValue.ToString();
                    FreightIGST = (Freight * IGST) / 100;
                    txtFreightIGST.Text = FreightIGST.ToString();

                    txtSGST.Text = "0";
                    txtCGST.Text = "0";
                    CGSTValue = 0;
                    SGSTValue = 0;

                    chbxSGST.Checked = false;
                    chbxCGST.Checked = false;
                }
                else
                {
                    txtIGST.Text = "0";
                    txtFreightIGST.Text = "0";
                }



                Total = Total + CGSTValue + SGSTValue + IGSTValue;
                txtTotal.Text = Math.Round(Total, 2).ToString();
                GrandTotal = GrandTotal + Total;
                decimal FreightWithGST = 0;

                FreightWithGST = Freight + FreightCGST + FreightSGST + FreightIGST;
                txtFreightWithGST.Text = FreightWithGST.ToString();

                decimal OtherCharges = Convert.ToDecimal(txtOtherCharges.Text);
                decimal TotalBill = Math.Round(GrandTotal + FreightWithGST + OtherCharges);
                txtTotalBillAmount.Text = TotalBill.ToString();
            }
            txtTotalAmount.Text = Math.Round(GrandTotal, 2).ToString();
            btnCalculate.Visible = false;

        }
    }
    private void SelectGST()
    {
        string UserName = Session["UserId"].ToString();
        string UserState = DBAccess.FetchDatasingle("select StateName from tbl_User where UserName='" + UserName + "'");
        string StateCode = "";
        try
        {
            StateCode = ddlState.SelectedItem.Value.ToString();
        }
        catch { }
        if (StateCode != "")
        {
            if (gvItemDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvItemDetails.Rows.Count; i++)
                {
                    CheckBox chbxCGST = gvItemDetails.Rows[i].FindControl("chbxCGST") as CheckBox;
                    CheckBox chbxSGST = gvItemDetails.Rows[i].FindControl("chbxSGST") as CheckBox;
                    CheckBox chbxIGST = gvItemDetails.Rows[i].FindControl("chbxIGST") as CheckBox;
                    if (StateCode == UserState)
                    {
                        chbxCGST.Checked = true;
                        chbxSGST.Checked = true;
                        chbxIGST.Checked = false;
                    }
                    else
                    {
                        chbxCGST.Checked = false;
                        chbxSGST.Checked = false;
                        chbxIGST.Checked = true;
                    }
                }

            }


        }

    }
    protected void btnAddServiceItem_Click(object sender, EventArgs e)
    {
        string ItemId = "";
        List<object> fieldValues = gvItem.GetSelectedFieldValues(new string[] { "ItemId" });
        if (fieldValues.Count != 0)
        {
            DataTable AppQtn = new DataTable();

            for (int j = 0; j < fieldValues.Count; j++)
            {
                ItemId = fieldValues[j].ToString();

                DataTable dt8 = (DataTable)ViewState["ItemDetails"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        if (ItemId == dt8.Rows[i]["ItemId"].ToString())
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
                            return;
                        }
                    }
                }

                DataTable dtI = (DataTable)ViewState["ItemDetails"];
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode where rm.ItemId='" + ItemId + "'");
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                    DataRow drH = dtI.NewRow();
                    drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                    drH["Category"] = dt11.Rows[0]["Category"].ToString();
                    drH["Type"] = dt11.Rows[0]["Type"].ToString();
                    drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                    drH["Make"] = dt11.Rows[0]["Make"].ToString();
                    drH["Model"] = dt11.Rows[0]["Model"].ToString();
                    drH["Unit"] = dt11.Rows[0]["Unit"].ToString();
                    drH["Qty"] = dt11.Rows[0]["Qty"].ToString();
                    drH["rate"] = dt11.Rows[0]["rate"].ToString();
                    drH["HSNCode"] = dt11.Rows[0]["HSNCode"].ToString();
                    drH["CGST"] = dt11.Rows[0]["CGST"].ToString();
                    drH["IGST"] = dt11.Rows[0]["IGST"].ToString();
                    drH["SGST"] = dt11.Rows[0]["SGST"].ToString();
                    dtI.Rows.Add(drH);

                    ViewState["ItemDetails"] = dtI;
                    gvItemDetails.DataSource = dtI;
                    gvItemDetails.DataBind();
                }
                SelectGST();
            }
        }
    }

    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "NonBarcode")
            {
                ((Button)e.Row.FindControl("btnBarcode")).Text = "Enter Barcode";
            }


        }
    }
}