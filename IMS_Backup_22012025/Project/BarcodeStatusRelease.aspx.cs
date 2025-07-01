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

public partial class Project_BarcodeStatusRelease : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FatchStock();
            string UserId = "";
            string ProjectId = "";
            string CompanyId = "";
            string UserGroup = "";

            try
            {
                UserGroup = Session["UserGroup"].ToString();
            }
            catch

            { }
            try
            {
                UserId = Session["UserId"].ToString();

            }
            catch

            { }
            try
            {
                ProjectId = Session["BranchId"].ToString();
            }

            catch
            {

            }
            try
            {
                CompanyId = Session["CompanyId"].ToString();
            }

            catch
            {

            }

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
        }
    }

    public void FatchStock()
    {
        string ProjectId = "";
        try
        {
            ProjectId = Session["BranchId"].ToString();
        }

        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
            return;
        }
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch
        {

        }

        DataTable dt = DBAccess.FetchDatatable("SELECT SIB.*,Convert(Varchar,SIB.WarrantyTo,103) as WarrantyDate, Convert(Varchar,SIB.CoderLifeTo,103) as CoderLifeDate, IM.ItemName, IM.Make, IM.Model  FROM tbl_RackStockInBarCodeDetails SIB LEFT JOIN tbl_ItemMaster IM ON IM.ItemId=SIB.ItemId WHERE  SIB.BranchId='" + BranchId + "' and SIB.Status1='InUse' and SIB.Status2='InUse'");
        if (dt.Rows.Count > 0)
        {
            gv_Barc.DataSource = dt;
            gv_Barc.DataBind();
        }

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
    protected void gv_Barc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Barc.PageIndex = e.NewPageIndex;
        gv_Barc.DataSource = ViewState["TempBarcode"];
        gv_Barc.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("BarcodeStatusRelease.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string UserId = "";
        string ProjectId = "";
        string CompanyId = "";

        try
        {
            UserId = Session["UserId"].ToString();
        }
        catch

        { }

        try
        {
            CompanyId = Session["CompanyId"].ToString();
        }

        catch
        {
        }
        try
        {
            ProjectId = Session["BranchId"].ToString();
        }

        catch
        {

        }
        DataTable dt = DBAccess.FetchDatatable("SELECT SIB.*,Convert(Varchar,SIB.WarrantyTo,103) as WarrantyDate, Convert(Varchar,SIB.CoderLifeTo,103) as CoderLifeDate, IM.ItemName, IM.Make, IM.Model FROM tbl_RackStockInBarCodeDetails SIB LEFT JOIN tbl_ItemMaster IM ON IM.ItemId=SIB.ItemId WHERE  SIB.BranchId='" + ProjectId + "' and SIB.Status1='InUse' and SIB.Status2='InUse'  and IM.ItemName LIKE '%" + txtSearch.Text + "%'");
        if (dt.Rows.Count > 0)
        {
            gv_Barc.DataSource = dt;
            gv_Barc.DataBind();
            //txtSearch.Text = "";
        }
        else
        {
            txtSearch.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Not Available');", true);
            return;
        }
    }
    protected void chkALL_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkALL = gv_Barc.HeaderRow.FindControl("chkALL") as CheckBox;
        if (chkALL.Checked == true)
        {
            for (int i = 0; i < gv_Barc.Rows.Count; i++)
            {
                CheckBox chk = gv_Barc.Rows[i].FindControl("chk") as CheckBox;
                Label lblstatus = (Label)gv_Barc.Rows[i].FindControl("lblstatus") as Label;
                if (lblstatus.Text == "Approve" || lblstatus.Text == "Absent")
                {

                    chk.Enabled = false;


                }
                else { chk.Checked = true; }
            }
        }
        if (chkALL.Checked == false)
        {
            for (int i = 0; i < gv_Barc.Rows.Count; i++)
            {
                CheckBox chk = gv_Barc.Rows[i].FindControl("chk") as CheckBox;
                chk.Checked = false;

            }
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
        string Date = dtpDate.Text;
        string Remarks = "";
        string PrioritiesId = "";
        string RequisitionPurpose = "";
        string ProjectId = "";
        string ProjectName = ddlProject.Text;

        if (btnSave.Text == "Release")
        {
            for (int i = 0; i < gv_Barc.Rows.Count; i++)
            {
                CheckBox chk = gv_Barc.Rows[i].FindControl("chk") as CheckBox;
                if (chk.Checked == true)
                {
                    TextBox ItemId1 = (TextBox)gv_Barc.Rows[i].FindControl("txtEstimationDate");
                    TextBox SrBarVodeId1 = (TextBox)gv_Barc.Rows[i].FindControl("txtWorkCatagory");
                    TextBox Barcode1 = (TextBox)gv_Barc.Rows[i].FindControl("txtWorkGroup");
                    TextBox EstimationId = (TextBox)gv_Barc.Rows[i].FindControl("txtEstimationId");

                    string StockInId = EstimationId.Text;
                    string ItemId = ItemId1.Text;
                    string SrBarVodeId = SrBarVodeId1.Text;
                    string Barcode = Barcode1.Text;
                    decimal Qty1 = 1;

                    bool SaveDetails = DBAccess.SaveData(@" insert into [dbo].[tbl_BarcodeInRelease] values ( convert(datetime,'" + Date + "',103), '" + SrBarVodeId + "', '" + Barcode + "', '" + PrioritiesId + "', '" + RequisitionPurpose + "', '" + ProjectId + "', '" + ProjectName + "','" + Remarks + "',  '" + UserId + "', '" + BranchId + "', convert(datetime,'" + DOE + "',103))");
                    bool UpdateRackBarCodeDetails = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set Status1='', Qty=1, Status2='' where SrBarVodeID='" + SrBarVodeId + "' and Barcode='" + Barcode + "' and BranchId='" + BranchId + "' and StockInId='" + StockInId + "' and UserId='" + UserId + "'");

                    //bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty+'" + Qty1 + "' where ItemId='" + ItemId + "' and BranchId='" + BranchId + "' and StockInId='" + StockInId + "'");

                    if (SaveDetails == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Saved successfully..');window.location='../Project/BarcodeStatusRelease.aspx';", true);
                    }

                }
            }

        }

        else
        {

        }

    }
}