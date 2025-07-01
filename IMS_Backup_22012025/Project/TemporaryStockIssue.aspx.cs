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
public partial class Project_TemporaryStockIssue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItem();
        }
        if (ReferenceEquals(ViewState["TempBarcode"], null))
        {
            ViewState["TempBarcode"] = getBarcode();
        }
        if (!IsPostBack)
        {
            Session["ReqtransAppId"] = "";
            string UserId = "";
            string BranchId = "";
            string UserGroup = "";
            Session["UserGroupId1"] = "";
            try
            {
                UserGroup = Session["UserGroupId"].ToString();
                string UserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='" + UserGroup + "'");
                txtUserGroup.Text = UserGroupName.ToString();
                string ToUserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='UG15'");
            }
            catch

            { }
            try
            {
                UserId = Session["UserId"].ToString();
                txtReqUser.Text = UserId.ToString();

                string UserName = DBAccess.FetchDatasingle("select ug.UserGroup from tbl_User u join tbl_UserGroups ug on u.UserGroup=ug.UserGroupId  where UserName='" + UserId + "'");
                if (UserName == "SuperAdmin")
                {
                    dtpDate.Date = DateTime.Now;
                }
                else
                {
                    dtpDate.Date = DateTime.Now;
                    dtpDate.Enabled = false;
                }
            }
            catch

            { }
            try
            {
                BranchId = Session["BranchId"].ToString();
                string BranchName = DBAccess.FetchDatasingle("select BranchName from tbl_Branch where BranchId='" + BranchId + "'");
                txtBranchName.Text = BranchName.ToString();
                Session["BranchName"] = BranchName.ToString();
                txtReqId.Text = BranchId.ToString();
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
                return;
            }

        }
             
    }

    protected void ddlStockInId_TextChanged(object sender, EventArgs e)
    {
       
        string ReqtransAppId = ddlStockInId.Value.ToString();
        Session["ReqtransAppId"] = ReqtransAppId;
        ddlItem.DataBind();

        string BranchIdSec = ddlRequisitionto.ValueField;
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        DataTable dtI = (DataTable)ViewState["ItemDetails"];
        DataTable dt11 = DBAccess.FetchDatatable("select a.ReqtransAppId as ReqAppId, a.ReqtransId as ReqId,a.ItemId,a.Qty,b.*,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdSec + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as POPQty, (select sum(Qty) from tbl_RackStockInBarcodeDetails  where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as ReqToQty from tbl_RequisitionTransferDetailsApproval a join tbl_ItemMaster b on a.ItemId=b.ItemId join dbo.tbl_HSNCodeMaster h on b.HSNCode=h.HSNCode where ReqtransAppId='" + ReqtransAppId + "'");
        if (dt11.Rows.Count > 0)
        {
            for (int i = 0; i < dt11.Rows.Count; i++)
            {
                DataRow drH = dtI.NewRow();
                drH["ItemId"] = dt11.Rows[i]["ItemId"].ToString();
                drH["Category"] = dt11.Rows[i]["Category"].ToString();
                drH["Type"] = dt11.Rows[i]["Type"].ToString();
                drH["ItemName"] = dt11.Rows[i]["ItemName"].ToString();
                drH["Make"] = dt11.Rows[i]["Make"].ToString();
                drH["Model"] = dt11.Rows[i]["Model"].ToString();
                drH["Unit"] = dt11.Rows[i]["Unit"].ToString();
                drH["Qty"] = dt11.Rows[i]["Qty"].ToString();
                drH["rate"] = dt11.Rows[i]["rate"].ToString();
                drH["HSNCode"] = dt11.Rows[i]["HSNCode"].ToString();
                drH["CGST"] = dt11.Rows[i]["CGST"].ToString();
                drH["IGST"] = dt11.Rows[i]["IGST"].ToString();
                drH["SGST"] = dt11.Rows[i]["SGST"].ToString();
                drH["ReqToQty"] = dt11.Rows[i]["ReqToQty"].ToString();
                drH["POPQty"] = dt11.Rows[i]["POPQty"].ToString();
                txtReqPopId.Text = dt11.Rows[i]["ReqId"].ToString();
                txtReqPopAppId.Text = dt11.Rows[i]["ReqAppId"].ToString();
                dtI.Rows.Add(drH);

                ViewState["ItemDetails"] = dtI;
                gvItemDetails.DataSource = dtI;
                gvItemDetails.DataBind();
                
            }
            DataTable dt2 = DBAccess.FetchDatatable(" select ReqtransAppId as ReqAppId,  ReqtransId as ReqId, convert(varchar,RequisitionDate,103) as RequisitionDate, RequisitionPurpose, Status5 as FromGr, Status4 as Location, Status3 from tbl_RequisitionTransferApproval where ReqtransAppId='" + ReqtransAppId + "'");
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    ddlRequisitionto.Text = dt2.Rows[i]["Location"].ToString();
                    ddlRequisitionto.Enabled = false;
                    ddlRequToGrp.Text = dt2.Rows[i]["FromGr"].ToString();
                    ddlRequToUser.Text = dt2.Rows[i]["Status3"].ToString();
                    ddlRequToUser.Enabled = false;
                    ddlRequisitionPur.Text = dt2.Rows[i]["RequisitionPurpose"].ToString();
                    ddlRequisitionPur.Enabled = false;
                }
            }
            ddlStockInId.Enabled = false;
        }

    }
   
    protected void gv_Barc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_Barc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_Barc.EditIndex = e.NewEditIndex;
    }
    protected void gv_Barc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Barc.PageIndex = e.NewPageIndex;
        gv_Barc.DataSource = ViewState["TempBarcode"];
        gv_Barc.DataBind();
    }
    protected void gv_Barc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = gvr.RowIndex;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["TempBarcode"];
            dt.Rows[rowindex].Delete();
            dt.AcceptChanges();
            ViewState["TempBarcode"] = dt;
            gv_Barc.DataSource = dt;
            gv_Barc.DataBind();
        }
    }
    protected DataTable getBarcode()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();
        dtCol.ColumnName = "StockInId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Type";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "SrBarVodeID";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Barcode";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Row";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Rack";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Shelf";
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
        dtCol.ColumnName = "ReqToQty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "POPQty";
        oTable.Columns.Add(dtCol);

        return oTable;
    }
    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {

        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        string Barcode = txtBarcode.Text;
        string ItemId = "";
        try
        {
            ItemId = ddlItem.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select Item First!');", true);
            txtBarcode.Text = "";
            return;
                        
        }        

        if (Barcode != "" & ItemId != "")
        {
            DataTable AppQtn = new DataTable();
            DataTable dt8 = (DataTable)ViewState["TempBarcode"];
            if (dt8 != null && dt8.Rows.Count > 0)
            {
                for (int i = 0; i < dt8.Rows.Count; i++)
                {
                    if (Barcode == dt8.Rows[i]["Barcode"].ToString())
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);

                        txtBarcode.Text = "";
                        return;
                        
                    }
                }
            }


            DataTable dtI = (DataTable)ViewState["TempBarcode"];
            DataTable dt11 = DBAccess.FetchDatatable("SELECT SIB.*,Convert(Varchar,SIB.WarrantyTo,103) as WarrantyDate, Convert(Varchar,SIB.CoderLifeTo,103) as CoderLifeDate, IM.ItemName FROM tbl_RackStockInBarCodeDetails SIB LEFT JOIN tbl_ItemMaster IM ON IM.ItemId=SIB.ItemId WHERE SIB.Barcode='" + Barcode + "'and SIB.ItemId='" + ItemId + "' and SIB.BranchId='" + BranchId + "' and (SIB.Status1='' or SIB.Status1 is null) and (SIB.Status2='' or SIB.Status2 is null)");
            if (dt11 != null && dt11.Rows.Count > 0)
            {
                DataRow drH = dtI.NewRow();
                drH["StockInId"] = dt11.Rows[0]["StockInId"].ToString();
                drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
               
                drH["SrBarVodeID"] = dt11.Rows[0]["SrBarVodeID"].ToString();
                drH["Barcode"] = dt11.Rows[0]["Barcode"].ToString();
                drH["Warranty"] = dt11.Rows[0]["Warranty"].ToString();
                drH["WarrantyTo"] = dt11.Rows[0]["WarrantyDate"].ToString();
                drH["CoderLifeTo"] = dt11.Rows[0]["CoderLifeDate"].ToString();
                drH["Row"] = dt11.Rows[0]["Row"].ToString();
                drH["Rack"] = dt11.Rows[0]["Rack"].ToString();
                drH["Shelf"] = dt11.Rows[0]["Shelf"].ToString();
                dtI.Rows.Add(drH);

                ViewState["TempBarcode"] = dtI;
                gv_Barc.DataSource = dtI;
                gv_Barc.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Wrong Barcode.');", true);
                return;
            }
            txtBarcode.Text = "";
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UserId = "";
        try
        {
            UserId = Session["UserId"].ToString();
        }
        catch { }
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        string UserGroupId = "";
        try
        {
            UserGroupId = Session["UserGroupId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;

        string TemIssueId = DBAccess.FetchDatatable("select [dbo].[fn_TemIssueId]()").Rows[0][0].ToString();
        string RequisitionDate = dtpDate.Text;
        string PrioritiesId = ddlRequisitionPur.Value.ToString();
        string RequisitionPurpose = ddlRequisitionPur.Text;
        string ReUserGroupId = ddlRequToGrp.Text;
        string ReUserGroupName = ddlRequToGrp.Text;
        string ReUserId = ddlRequisitionto.Value.ToString();
        string ReUserName = ddlRequisitionto.Text;
        string Remarks = txtRemarks.Text;
        string Status = ddlRequToUser.Text;
        string Status1 = "";
        string Status2 = "";
        string Status3 = txtReqUser.Text;
        string Status4 = txtBranchName.Text;
        string Status5 = txtUserGroup.Text;
        string ReqAppId = txtReqPopAppId.Text;
        string ReqId = txtReqPopId.Text;
        string Status7 = "";
        string Status8 = "";
        //int n = ClassMaterialIssue.MaterialIssueSave(IssueId, ReqAppId, ReqId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, UserGroupId, UserId, BranchId, DOE);
        bool SaveDetails = DBAccess.SaveData(@" insert into [dbo].[tbl_TemporaryStockIssue] values ('" + TemIssueId + "','" + ReqAppId + "','" + ReqId + "', convert(datetime,'" + RequisitionDate + "',103), '" + PrioritiesId + "', '" + RequisitionPurpose + "', '" + ReUserGroupId + "', '" + ReUserGroupName + "','" + ReUserId + "', '" + ReUserName + "', '" + Remarks + "','" + Status + "','" + Status1 + "','" + Status2 + "','" + Status3 + "','" + Status4 + "', '" + Status5 + "', '', '','" + UserGroupId + "','" + BranchId + "', '" + UserId + "', convert(datetime,'" + DOE + "',103),'','','','','','')");
             
        if (SaveDetails == true)
        {
            for (int i = 0; i < gvItemDetails.Rows.Count; i++)
            {
                string TemIssueItemId = DBAccess.FetchDatatable("select [dbo].[fn_TemIssueItemId]()").Rows[0][0].ToString();
                string ItemId = gvItemDetails.Rows[i].Cells[1].Text;
                string Category = gvItemDetails.Rows[i].Cells[2].Text;
                string ItemName = gvItemDetails.Rows[i].Cells[4].Text;
                string Make = gvItemDetails.Rows[i].Cells[5].Text;
                string Model = gvItemDetails.Rows[i].Cells[6].Text;
                string Unit = gvItemDetails.Rows[i].Cells[7].Text;
                decimal ReqToQty = 0;
                decimal POPQty = 0;
                string Status6 = "";

                try
                {
                    ReqToQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[8].Text);
                    POPQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[9].Text);
                }
                catch
                { }
                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                //int m = ClassMaterialIssue.MaterialIssueSaveDetails(IssueId, IssueItemId, ReqAppId, ReqId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);
                bool MaterialIssueSaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_TemporaryStockIssueDetails] values ('" + TemIssueId + "', '" + TemIssueItemId + "', '" + ReqAppId + "','" + ReqId + "', '" + ItemId + "', '" + Category + "', '" + ItemName + "', '" + Make + "', '" + Model + "', '" + Unit + "','" + ReqToQty + "','" + POPQty + "', '" + Qty + "', '" + Qty1 + "', '" + Status6 + "' , '" + BranchId + "', '" + UserId + "',convert(datetime,'" + DOE + "',103))");

                if (MaterialIssueSaveDetails == true)
                {
                    if (ViewState["TempBarcode"] != null)
                    {
                        DataTable dt1 = ViewState["TempBarcode"] as DataTable;
                        for (int a = 0; a < gv_Barc.Rows.Count; a++)
                        {
                            for (int j = 0; j < dt1.Rows.Count; j++, a++)
                            {
                                string ItemId1 = dt1.Rows[j]["ItemId"].ToString();
                                string TemIssueItemBrId = DBAccess.FetchDatatable("select  [dbo].[fn_TemIssueItemBrId]()").Rows[0][0].ToString();
                                string StockInId = dt1.Rows[j]["StockInId"].ToString();
                                string SrBarVodeID = dt1.Rows[j]["SrBarVodeID"].ToString();
                                string Barcode = dt1.Rows[j]["Barcode"].ToString();
                                decimal Qtyb = 1;

                                string ProcessId = "";
                                string Row1 = dt1.Rows[j]["Row"].ToString();
                                string Rack1 = dt1.Rows[j]["Rack"].ToString();
                                string Shelf1 = dt1.Rows[j]["Shelf"].ToString();
                                string Warranty = dt1.Rows[j]["Warranty"].ToString();
                                string WarrantyTo = dt1.Rows[j]["WarrantyTo"].ToString();
                                string CoderLifeTo = dt1.Rows[j]["CoderLifeTo"].ToString();



                                bool SaveDetailsBar = DBAccess.SaveData(@"insert into [dbo].[tbl_TemporaryStockIssueBarCodeDetails] values ('" + TemIssueId + "','" + TemIssueItemId + "','" + TemIssueItemBrId + "','" + StockInId + "','" + SrBarVodeID + "', '" + ItemId1 + "', '" + Barcode + "', '" + ProcessId + "','" + Row1 + "','" + Rack1 + "', '" + Shelf1 + "', '" + Warranty + "',convert(datetime,'" + WarrantyTo + "',103),convert(datetime,'" + CoderLifeTo + "',103),'','','','','','', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");

                                bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set Status1='TemIssue', Qty='0' where SrBarVodeID='" + SrBarVodeID + "' and Row='" + Row1 + "' and Rack='" + Rack1 + "' and Shelf= '" + Shelf1 + "' and BranchId='" + BranchId + "'  and StockInId='" + StockInId + "'");

                                bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty-'" + Qtyb + "' where ItemId='" + ItemId1 + "' and Row='" + Row1 + "' and Rack='" + Rack1 + "' and Shelf= '" + Shelf1 + "' and BranchId='" + BranchId + "'  and StockInId='" + StockInId + "'");

                            }
                            ViewState["TempBarcode"] = null;
                        }


                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Issue Done Successfully.');window.location='../Project/TemporaryStockIssue.aspx';", true);

                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Issue Not Done. Try again.');", true);
                    return;
                }
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("TemporaryStockIssue.aspx");
    }
}