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

public partial class Project_DTRReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["startdate2"] = null;
            Session["enddate2"] = null;

            dtpFromDate.Date = DateTime.Now;
            dtpToDate.Date = DateTime.Now;
        }

    }
    protected void ASPxGridView2_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string BranchId = "";
        if (Session["BranchId"] != null)
        {
            BranchId = Session["BranchId"].ToString();
        }
        if (Session["startdate2"] != null && Session["enddate2"] != null)
        {
            SqlDataSource2.SelectCommand = @"select b.IssueId, a.ItemId, a.ItemName, im.Make,im.Model, a.Qty, CONVERT(varchar,b.IssueDate,103) as IssueDate,  b.RequisitionPurpose, b.ReUserGroupName, b.ReUserName, b.Status5, b.UserId  from tbl_MaterialIssueDetails a
join tbl_MaterialIssue b on a.IssueId=b.IssueId join tbl_ItemMaster im on im.ItemId=a.ItemID where CONVERT(datetime,b.IssueDate,103) between CONVERT(datetime,'" + Session["startdate2"].ToString() + "',103) and CONVERT(datetime,'" + Session["enddate2"].ToString() + "',103)";
        }
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

        ASPxGridView2.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("DTRReports.aspx");
    }


    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptions());
    }
    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptions());
    }
}