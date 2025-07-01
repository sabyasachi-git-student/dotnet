using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;
using DevExpress.Web;
using DevExpress.Data.Linq;


public partial class Project_POStatusReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridHeaderFilterMode headerFilterMode = hFModeCheckBox.Checked ? GridHeaderFilterMode.CheckedList : GridHeaderFilterMode.List;
        foreach (GridViewDataColumn column in grid.DataColumns)
            column.SettingsHeaderFilter.Mode = headerFilterMode;
        if (!IsPostBack)
        {
            Session["startdate2"] = null;
            Session["enddate2"] = null;
            Session["POId"] = null;

            dtpFromDate.Date = DateTime.Now;
            dtpToDate.Date = DateTime.Now;
        }
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptions());
    }
    protected void btnXlsxExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptions());
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DateTime startdate;
        DateTime enddate;

        startdate = Convert.ToDateTime(dtpFromDate.Value).Date;
        enddate = Convert.ToDateTime(dtpToDate.Value).Date;

        string startdate2 = startdate.ToString("dd/MM/yyyy");
        string enddate2 = enddate.ToString("dd/MM/yyyy");

        Session["startdate2"] = startdate2;
        Session["enddate2"] = enddate2;

        grid.DataBind();
    }
    protected void btnPOID_Click(object sender, EventArgs e)
    {
        string POId = txtPOID.Text;
        Session["POId"] = POId;

        grid.DataBind();
    }
  
    protected void grid_BeforePerformDataSelect(object sender, EventArgs e)
    {
        String BranchId = "";
        if (Session["BranchId"] != null)
        {
            BranchId = Session["BranchId"].ToString();
        }
        if (Session["startdate2"] != null && Session["enddate2"] != null)
        {
            SqlDataSource1.SelectCommand = @"select distinct a.POID, convert(varchar,a.PODate,103) as PODate, sm.CompanyName, i.Category, pd.ItemId, i.ItemName, i.Make, i.Model,
b.Qty, i.Unit,  pd.Rate, a.ProjectName, b.Warranty  from tbl_StockIn a join tbl_StockInDetails b on a.StockInId=b.StockInId
join tbl_PurchaseBillEntry p on b.StockInId=p.StockInId  join tbl_PurchaseBillDetails pd on p.PurchaseBillId=pd.PurchaseBillId  
join tbl_ItemMaster i on b.ItemId=i.ItemId join tbl_SupplierMasterEntry sm on a.SupplierId=sm.SupplierId where CONVERT(datetime,a.PODate,103) between CONVERT(datetime,'" + Session["startdate2"].ToString() + "',103) and CONVERT(datetime,'" + Session["enddate2"].ToString() + "',103)";
        }

        if (Session["POId"] != null)
        {
            SqlDataSource1.SelectCommand = @"select distinct a.POID, convert(varchar,a.PODate,103) as PODate, sm.CompanyName, i.Category, pd.ItemId, i.ItemName, i.Make, i.Model,
b.Qty, i.Unit,  pd.Rate, a.ProjectName, b.Warranty  from tbl_StockIn a  join tbl_StockInDetails b on a.StockInId=b.StockInId
join tbl_PurchaseBillEntry p on b.StockInId=p.StockInId  join tbl_PurchaseBillDetails pd on p.PurchaseBillId=pd.PurchaseBillId  
join tbl_ItemMaster i on b.ItemId=i.ItemId join tbl_SupplierMasterEntry sm on a.SupplierId=sm.SupplierId where a.POID= '" + Session["POId"].ToString() + "'";
        }
    }
    protected void chkPODate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPODate.Checked == true)
        {
            trPOdate.Visible = true;
            trPOID.Visible = false;
        }
        else
        {
            trPOdate.Visible = false;
            trPOID.Visible = false;
        }
    }
    protected void chkPOID_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPOID.Checked == true)
        {
            trPOID.Visible = true;
            trPOdate.Visible = false;
        }
        else
        {
            trPOdate.Visible = false;
            trPOID.Visible = false;
        }
    }   
    protected void btnClear1_Click(object sender, EventArgs e)
    {
        Response.Redirect("POStatusReports.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("POStatusReports.aspx");
    }

}