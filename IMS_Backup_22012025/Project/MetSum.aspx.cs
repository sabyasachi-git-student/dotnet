using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Project_MetSum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["startdate2"] = null;
            Session["enddate2"] = null;

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
            SqlDataSource2.SelectCommand = @"select 'Material Issue' as Status, b.IssueId, a.ItemId, a.ItemName, a.Qty, CONVERT(varchar,b.IssueDate,103) as IssueDate,  b.RequisitionPurpose, b.ReUserGroupName, 
b.ReUserName, b.Status5, b.UserId  from tbl_MaterialIssueDetails a join tbl_MaterialIssue b on a.IssueId=b.IssueId where CONVERT(datetime,b.IssueDate,103) between CONVERT(datetime,'" + Session["startdate2"].ToString() + "',103) and CONVERT(datetime,'" + Session["enddate2"].ToString() + "',103) union all select 'Temporary Issue' as Status, b.TemIssueId as IssueId, a.ItemId, a.ItemName, a.Qty, CONVERT(varchar,b.IssueDate,103) as IssueDate,  b.RequisitionPurpose, b.ReUserGroupName, b.ReUserName, b.Status5, b.UserId  from tbl_TemporaryStockIssueDetails a join tbl_TemporaryStockIssue b on a.TemIssueId=b.TemIssueId where CONVERT(datetime,b.IssueDate,103) between CONVERT(datetime,'" + Session["startdate2"].ToString() + "',103) and CONVERT(datetime,'" + Session["enddate2"].ToString() + "',103)";
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
        Response.Redirect("MetSum.aspx");
    }
}