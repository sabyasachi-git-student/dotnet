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
using DevExpress.XtraGrid;

public partial class Project_ScrapStockOut : System.Web.UI.Page
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

        DataTable dt = DBAccess.FetchDatatable("select a.StockInId, a.ItemId, i.ItemName, i.Make, i.Model, a.SrBarVodeId, a.Barcode, convert(varchar, a.WarrantyTo,103) as Warreanty, convert(varchar, a.CoderLifeTo,103) as CoderLife  From [tbl_RackStockInBarCodeDetails] a join tbl_ItemMaster i on a.Itemid=i.ItemId where a.Status3='Non-Repair' and a.BranchId='" + ProjectId + "' and a.SrBarVodeId in (Select SrBarVodeId from tbl_ScrapAssign) and  Status4='Scrap'");
        if (dt.Rows.Count > 0)
        {
            grdStockAudit.DataSource = dt;
            grdStockAudit.DataBind();
        }

    }
    protected void grdStockAudit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("chk");
            chk.Enabled = true;
        }
    }
    protected void chkALL_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkALL = grdStockAudit.HeaderRow.FindControl("chkALL") as CheckBox;
        if (chkALL.Checked == true)
        {
            for (int i = 0; i < grdStockAudit.Rows.Count; i++)
            {
                CheckBox chk = grdStockAudit.Rows[i].FindControl("chk") as CheckBox;
                Label lblstatus = (Label)grdStockAudit.Rows[i].FindControl("lblstatus") as Label;
                if (lblstatus.Text == "Approve" || lblstatus.Text == "Absent")
                {

                    chk.Enabled = false;


                }
                else { chk.Checked = true; }
            }
        }
        if (chkALL.Checked == false)
        {
            for (int i = 0; i < grdStockAudit.Rows.Count; i++)
            {
                CheckBox chk = grdStockAudit.Rows[i].FindControl("chk") as CheckBox;
                chk.Checked = false;

            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
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
        DateTime DOE = DateTime.Now;

        string ProcessAssignDate = dttAnniversaryDate.Text;
        if (ProcessAssignDate == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Lead Assign Date');", true);
            return;
        }
        string ScrapStockOutId = DBAccess.FetchDatatable("select [dbo].[fn_ScrapStockOutId]()").Rows[0][0].ToString();

        for (int i = 0; i < grdStockAudit.Rows.Count; i++)
        {
            CheckBox chk = grdStockAudit.Rows[i].FindControl("chk") as CheckBox;
            if (chk.Checked == true)
            {
                TextBox ItemId1 = (TextBox)grdStockAudit.Rows[i].FindControl("txtEstimationDate");
                TextBox SrBarVodeId1 = (TextBox)grdStockAudit.Rows[i].FindControl("txtWorkCatagory");
                TextBox Barcode1 = (TextBox)grdStockAudit.Rows[i].FindControl("txtWorkGroup");

                string ItemId = ItemId1.Text;
                string SrBarVodeId = SrBarVodeId1.Text;
                string Barcode = Barcode1.Text;

                bool ScrapAssign = DBAccess.SaveData(@"Insert into tbl_ScrapStockOut Values ('" + ScrapStockOutId + "', convert(datetime,'" + ProcessAssignDate + "',103),'" + ItemId + "', '" + SrBarVodeId + "', '" + Barcode + "',  '" + UserId + "', '" + ProjectId + "', convert(datetime,'" + DOE + "',103))");

                bool ProcessMasterAssign = DBAccess.SaveData(@"update [tbl_RackStockInBarCodeDetails] set Warranty3rdDOE= convert(datetime,'" + ProcessAssignDate + "',103), Status4='Scrap-StockOut' where SrBarVodeID='" + SrBarVodeId + "' and BranchId='" + ProjectId + "'");
            }
        }
        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Scrap Stock Out Done Successfully . ');window.location='../Project/ScrapStockOut.aspx';", true);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Scrap.aspx");
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
        DataTable dt = DBAccess.FetchDatatable("select a.StockInId, a.ItemId, i.ItemName, i.Make, i.Model, a.SrBarVodeId, a.Barcode, convert(varchar, a.WarrantyTo,103) as Warreanty, convert(varchar, a.CoderLifeTo,103) as CoderLife  From [tbl_RackStockInBarCodeDetails] a join tbl_ItemMaster i on a.Itemid=i.ItemId where a.Status3='Non-Repair' and a.BranchId='" + ProjectId + "' and a.SrBarVodeId in (Select SrBarVodeId from tbl_ScrapAssign) and  Status4='Scrap' and i.ItemName LIKE '%" + txtSearch.Text + "%'");
        if (dt.Rows.Count > 0)
        {
            grdStockAudit.DataSource = dt;
            grdStockAudit.DataBind();
            //txtSearch.Text = "";
        }
        else
        {
            txtSearch.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Not Available');", true);
            return;
        }

    }
}