using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

public partial class Project_MaterialReceive : System.Web.UI.Page
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
            try
            {
                UserId = Session["UserId"].ToString();

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
                string BranchNameA = DBAccess.FetchDatasingle("select BranchName from tbl_Branch where BranchId='" + BranchId + "'");
                Session["BranchName"] = BranchNameA.ToString();
                string BranchName = Session["BranchName"].ToString();

            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
                return;
            }

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

        dtCol = new DataColumn();
        dtCol.ColumnName = "EnterBarcode";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ProcessId";
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
    protected void ddlIssueId_TextChanged(object sender, EventArgs e)
    {

        string IssueId = ddlIssueId.Text;

        DataTable dt = DBAccess.FetchDatatable(@"select a.*, convert(varchar, a.CourierDate,103) as CourierDate1,mi.ReqAppId,mi.ReqId from tbl_MaterialShipment a join tbl_MaterialIssue mi on a.IssueId=mi.IssueId where a.IssueId='" + IssueId + "' Union all select a.*, convert(varchar, a.CourierDate,103) as CourierDate1,mi.ReqAppId,mi.ReqId from tbl_MaterialShipment a join tbl_TemporaryStockIssue mi on a.IssueId=mi.TemIssueId where a.IssueId='" + IssueId + "'");
        if (dt.Rows.Count > 0)
        {
            ddlCourierName.Text = dt.Rows[0]["CourierName"].ToString();
            dtpCourierDate.Date = Convert.ToDateTime(dt.Rows[0]["CourierDate1"].ToString());
            txtConsignmentNumber.Text = dt.Rows[0]["ConsignmentNumber"].ToString();
            lblReqAppId.Text = dt.Rows[0]["ReqAppId"].ToString();
            lblReqId.Text = dt.Rows[0]["ReqId"].ToString();
            txtPersonName.Text = dt.Rows[0]["PersonName"].ToString();
            txtContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
            txtTrainName.Text = dt.Rows[0]["TrainName"].ToString();
            txtTrainNo.Text = dt.Rows[0]["TrainNo"].ToString();
            txtCoachNo.Text = dt.Rows[0]["CoachNo"].ToString();
        }

        DataTable dt1 = DBAccess.FetchDatatable(@"select * from tbl_MaterialIssueDetails where IssueId='" + IssueId + "' Union all select * from tbl_TemporaryStockIssueDetails where TemIssueId='" + IssueId + "'");
        if (dt1.Rows.Count > 0)
        {
            gvItemDetails.DataSource = dt1;
            gvItemDetails.DataBind();
        }

        DataTable dt2 = DBAccess.FetchDatatable(@"select a.*, convert(varchar,a.WarrantyTo,103) as WarrantyTo1, convert(varchar,a.CoderLifeTo,103) as CoderLifeTo1, b.ItemName from tbl_MaterialIssueBarCodeDetails a join tbl_ItemMaster b on a.itemId=b.ItemId where a.IssueId='" + IssueId + "' Union all select a.*, convert(varchar,a.WarrantyTo,103) as WarrantyTo1, convert(varchar,a.CoderLifeTo,103) as CoderLifeTo1, b.ItemName from tbl_TemporaryStockIssueBarCodeDetails a join tbl_ItemMaster b on a.itemId=b.ItemId where a.TemIssueId='" + IssueId + "'");
        if (dt2.Rows.Count > 0)
        {
            gv_Barc.DataSource = dt2;
            gv_Barc.DataBind();
            ViewState["TempBarcode"] = dt2;
        }
    }
    protected void gv_Barc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Barc.PageIndex = e.NewPageIndex;

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
    protected void gv_Barc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_Barc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_Barc.EditIndex = e.NewEditIndex;
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
        string IssueId = ddlIssueId.Text;
        if (IssueId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select IssueId First');", true);
            return;
        }
        string ReceiveDate = dtpDate.Text;
        if (ReceiveDate == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Date');", true);
            return;
        }

        string ReceiveId = DBAccess.FetchDatatable("select [dbo].[fn_ReceiveId]()").Rows[0][0].ToString();
        String ReqAppId = lblReqAppId.Text;
        String ReqId = lblReqId.Text;
        string CourierName = ddlCourierName.Text;
        string CourierDate = dtpCourierDate.Text;
        string ConsNo = txtConsignmentNumber.Text;
        string Remarks = txtRemarks.Text;
        string Status1 = "";
        string Status2 = "";
        string PersonName = txtPersonName.Text;
        string ContactNo = txtContactNo.Text;
        string TrainName = txtTrainName.Text;
        string TrainNo = txtTrainNo.Text;
        string CoachNo = txtCoachNo.Text;

        if (ddlCourierName.Text == "ByHand" || ddlCourierName.Text == "ByTrain")
        {
            if (PersonName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Person Name');", true);
                return;
            }
            if (ContactNo == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Contact No');", true);
                return;
            }
            if (TrainName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Train Name');", true);
                return;
            }
            if (TrainNo == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Train No');", true);
                return;
            }
            if (CoachNo == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Coach No');", true);
                return;
            }
        }

        bool SaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_MaterialReceive] values ('" + ReceiveId + "','" + IssueId + "','" + ReqAppId + "','" + ReqId + "', convert(datetime,'" + ReceiveDate + "',103), '" + CourierName + "', convert(datetime,'" + CourierDate + "',103), '" + ConsNo + "', '" + Remarks + "', '" + PersonName + "', '" + ContactNo + "', '" + TrainName + "', '" + TrainNo + "', '" + CoachNo + "', '" + Status1 + "','" + Status2 + "', '" + BranchId + "', '" + UserId + "',convert(datetime,'" + DOE + "',103))");
        if (SaveDetails == true)
        {
            for (int i = 0; i < gvItemDetails.Rows.Count; i++)
            {
                string ReceiveDeId = DBAccess.FetchDatatable("select [dbo].[fn_ReceiveDeId]()").Rows[0][0].ToString();
                string ItemId = gvItemDetails.Rows[i].Cells[1].Text;
                string Category = gvItemDetails.Rows[i].Cells[2].Text;
                string ItemName = gvItemDetails.Rows[i].Cells[3].Text;
                string Make = gvItemDetails.Rows[i].Cells[4].Text;
                string Model = gvItemDetails.Rows[i].Cells[5].Text;
                string Unit = gvItemDetails.Rows[i].Cells[6].Text;
                decimal ReqToQty = 0;
                decimal POPQty = 0;
                string Status6 = "";

                try
                {
                    ReqToQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[7].Text);
                    POPQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[8].Text);
                }
                catch
                { }
                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                bool MaterialIssueSaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_MaterialReceiveDetails] values ('" + ReceiveId + "', '" + ReceiveDeId + "', '" + IssueId + "','" + ReqAppId + "','" + ReqId + "', '" + ItemId + "', '" + Category + "', '" + ItemName + "', '" + Make + "', '" + Model + "', '" + Unit + "','" + ReqToQty + "','" + POPQty + "', '" + Qty + "', '" + Qty1 + "', '" + Status6 + "' , '" + BranchId + "', '" + UserId + "',convert(datetime,'" + DOE + "',103))");


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
                                string RecvItemBrId = DBAccess.FetchDatatable("select  [dbo].[fn_RecvItemBrId]()").Rows[0][0].ToString();
                                string StockInId = dt1.Rows[j]["StockInId"].ToString();
                                string SrBarVodeID = dt1.Rows[j]["SrBarVodeID"].ToString();
                                string Barcode = dt1.Rows[j]["Barcode"].ToString();
                                decimal Qtyb = 1;
                                TextBox txtBarcodeEnter = (TextBox)gv_Barc.Rows[a].FindControl("txtBarcodeEnter");

                                string ProcessId = dt1.Rows[j]["ProcessId"].ToString();
                                string Row1 = dt1.Rows[j]["Row"].ToString();
                                string Rack1 = dt1.Rows[j]["Rack"].ToString();
                                string Shelf1 = dt1.Rows[j]["Shelf"].ToString();
                                string Warranty = dt1.Rows[j]["Warranty"].ToString();
                                string WarrantyTo = dt1.Rows[j]["WarrantyTo"].ToString();
                                string CoderLifeTo = dt1.Rows[j]["CoderLifeTo"].ToString();



                                bool SaveDetailsBar = DBAccess.SaveData(@"insert into [dbo].[tbl_MaterialReceiveBarCodeDetails] values ('" + ReceiveId + "', '" + IssueId + "','" + ReceiveDeId + "','" + RecvItemBrId + "','" + StockInId + "','" + SrBarVodeID + "', '" + ItemId1 + "', '" + Barcode + "', '" + ProcessId + "','" + Row1 + "','" + Rack1 + "', '" + Shelf1 + "', '" + Warranty + "',convert(datetime,'" + WarrantyTo + "',103),convert(datetime,'" + CoderLifeTo + "',103),'','','','','','', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");
                                bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set Status2='Receive' where SrBarVodeID='" + SrBarVodeID + "' and Barcode='" + Barcode + "' ");

                                bool UpdateTemporaryStockIssue = DBAccess.SaveData(@"update tbl_TemporaryStockIssue set Receive='Receive', ReceiveDate=convert(datetime,'" + DOE + "',103), ReceiveUser='" + UserId + "' where TemIssueId='" + IssueId + "'");
                                bool UpdateTemporaryStockIssueDetails = DBAccess.SaveData(@"update tbl_TemporaryStockIssueDetails set Status6='Receive' where TemIssueId='" + IssueId + "'");
                                bool UpdateTemporaryStockIssueBarCodeDetails = DBAccess.SaveData(@"update tbl_TemporaryStockIssueBarCodeDetails set Status1='Receive' where TemIssueId='" + IssueId + "'");

                                //bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty-'" + Qtyb + "' where ItemId='" + ItemId1 + "' and RackSpace='" + ProcessId + "' and Row='" + Row1 + "' and Rack='" + Rack1 + "' and Shelf= '" + Shelf1 + "' and BranchId='" + BranchId + "'");

                            }
                            ViewState["TempBarcode"] = null;
                        }


                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Received Done Successfully.');window.location='../Project/RackTag_BarcodeGen.aspx';", true);

                    }

                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Received Not Done. Try again.');", true);
                    return;
                }
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaterialReceive.aspx");
    }
}