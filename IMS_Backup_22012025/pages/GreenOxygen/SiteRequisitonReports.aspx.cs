using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
//using System.Windows.Forms;
using System.IO;
using System.Text;

public partial class pages_GreenOxygen_SiteRequisitonReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCompany();
            LoadBranch();

            string CompanyId = ddl_Company.SelectedValue.ToString();
            string ProjectId = ddl_Project.SelectedValue.ToString();

            if (CompanyId == "" || CompanyId == "-Select-")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Company Name ');", true);
                return;
            }
            if (ProjectId == "" || ProjectId == "-Select-")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Project Name ');", true);
                return;
            }
        }
    }



    public void LoadCompany()
    {
        string UserId = Session["UserId"].ToString();

        DataTable dt = DBAccess.FetchDatatable(@"select distinct mp.CompanyId ,com.CompanyName from [tbl_UserBranchMapping] mp
		 join tbl_CompanyMaster com on  mp.CompanyId=com.CompanyId where mp.UserId='" + UserId + "'");

        if (dt != null && dt.Rows.Count > 0)
        {
            ddl_Company.Items.Clear();

            ddl_Company.DataSource = dt;
            ddl_Company.DataTextField = "CompanyName";
            ddl_Company.DataValueField = "CompanyId";
            ddl_Company.DataBind();
            ddl_Company.Items.Insert(0, "-Select-");
        }
    }
    public void LoadBranch()
    {
        string UserId = Session["UserId"].ToString(); ;

        string CompanyId = "";
        try
        {
            CompanyId = ddl_Company.SelectedItem.Value.ToString();
        }
        catch
        {

        }
        string CompanyName = DBAccess.FetchDatasingle("select  CompanyName from [dbo].[tbl_CompanyMaster] where CompanyId='" + CompanyId + "'");
        DataTable dt = DBAccess.FetchDatatable(@"select  m.UserId, m.BranchId,m.CompanyId,b.BranchName from  [dbo].[tbl_UserBranchMapping] m
		left join dbo.tbl_Branch b on m.BranchId=b.BranchId 
		left join [dbo].[tbl_CompanyMaster] com on m.CompanyId=com.CompanyId where m.UserId='" + UserId + "' and m.CompanyId='" + CompanyId + "'");
        if (dt != null && dt.Rows.Count > 0)
        {

            ddl_Project.Items.Clear();
            ddl_Project.DataSource = dt;
            ddl_Project.DataTextField = "BranchName";
            ddl_Project.DataValueField = "BranchId";
            ddl_Project.DataBind();
            ddl_Project.Items.Insert(0, "-Select-");
        }
    }
    protected void ddl_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranch();
    }
    protected void ddl_Project_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    private DataTable getRequisitionData()
    {
        string data = "";

        string UserId = Session["UserId"].ToString();
        string ProjectId = ddl_Project.SelectedValue.ToString();
        string CompanyId = ddl_Company.SelectedValue.ToString();

        string fromdate = txtFromDate.Text;
        string todate = txtToDate.Text;

            DataTable dt;
            string FullName;
            string RequisitionId;
            string SiteCode;
            string EstimationDate;
            string BranchName;
            string WorkGroup;
            string WorkCatagory;
            string ProcessName;
            string RequiredByDate;
            string ApproveStatus;

            dt = DBAccess.FetchDatatable("SELECT distinct est.RequisitionId,est.RequisitionPriority,est.WorkCatagory,                                                          est.WorkGroup,convert(varchar,est.Dates,103) as 'EstimationDate', est.UserId,                                                        pcm.ProcessName,est.WorkStage,est.Time, br.SiteCode,f.FormulaTitle,brc.BranchName ,su.FullName,                                      'ApproveStatus'=case when est.ApproveStatus =1 then 'Approved' else (case when                                                        est.RejectedStatus=1 then 'Rejected' else 'Not Checked' end) end, 'RequiredByDate'=case when                                         RequiredByDate='1900-01-01 00:00:00.000' then '' else convert(varchar,RequiredByDate,103) end                                       FROM [dbo].[tbl_SiteRequisition] est join tbl_Branch br on est.ProjectId=br.BranchId left join                                        tbl_ProcessMaster pcm on est.ProcessId=pcm.ProcessName join tbl_User su on                                                           est.UserId=su.UserName join tbl_Branch brc on est.ProjectId=brc.BranchId left join                                                   dbo.tbl_EstimationFormula f on f.EstimationFormulaId=est.EstimationFormulaId where                                                   est.ProjectId='" + ProjectId + "' and convert(varchar,est.RequiredByDate,1) BETWEEN '" + fromdate + "' and '" + todate + "'");

            FullName = dt.Rows[0]["FullName"].ToString();
            RequisitionId = dt.Rows[0]["RequisitionId"].ToString();
            SiteCode = dt.Rows[0]["SiteCode"].ToString();
            EstimationDate = dt.Rows[0]["EstimationDate"].ToString();
            UserId = dt.Rows[0]["UserId"].ToString();
            BranchName = dt.Rows[0]["BranchName"].ToString();
            WorkGroup = dt.Rows[0]["WorkGroup"].ToString();
            WorkCatagory = dt.Rows[0]["WorkCatagory"].ToString();
            ProcessName = dt.Rows[0]["ProcessName"].ToString();
            RequiredByDate = dt.Rows[0]["RequiredByDate"].ToString();
            ApproveStatus = dt.Rows[0]["ApproveStatus"].ToString();

            //data += "<tr><td>" + FullName + "</td><td>" + RequisitionId + "</td><td>" + SiteCode + "</td><td>" + EstimationDate + "</td><td>" + UserId + "</td><td>" + BranchName + "</td><td>" + WorkGroup + "</td><td>" + WorkCatagory + "</td><td>" + ProcessName + "</td><td>" + RequiredByDate + "</td><td>" + ApproveStatus + "</td></tr>";
            
            return dt;
        
    }

    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string UserId = Session["UserId"].ToString();;
        string CompanyId = ddl_Company.SelectedValue.ToString();
        string ProjectId = ddl_Project.SelectedValue.ToString();

        if(CompanyId == "" || CompanyId == "-Select-")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Company Name ');", true);
            return;
        }
        else
        {
            if(ProjectId == "" || ProjectId == "-Select-")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Project Name ');", true);
                return;
            }
            else
            {
                string fromdate = txtFromDate.Text;
                if (fromdate == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select From Date ');", true);
                    return;
                }
                else
                {
                    string todate = txtToDate.Text;
                    if (todate == "")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select To Date ');", true);
                        return;
                    }
                    else
                    {
                        DataTable dt = getRequisitionData();
                        StringBuilder html = new StringBuilder();

                        html.Append("<thead>");
                        html.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            html.Append("<th>");
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        }
                        html.Append("</tr>");
                        html.Append("</thead>");

                        html.Append("<tbody>");
                        foreach (DataRow row in dt.Rows)
                        {
                            html.Append("<tr>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                html.Append("<td>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                            html.Append("</tr>");
                        }
                        html.Append("</tbody>");

                        html.Append("<tfoot>");
                        html.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            html.Append("<th>");
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        }
                        html.Append("</tr>");
                        html.Append("</tfoot>");


                        PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                    }
                }
            }
        }


        //try
        //{
        //    CompanyId = ddl_Company.SelectedValue.ToString();
        //    try
        //    {
        //        ProjectId = ddl_Project.SelectedValue.ToString();

        //        string fromdate = txtFromDate.Text;
        //        if (fromdate == "")
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select From Date ');", true);
        //            return;
        //        }
        //        else
        //        {
        //            string todate = txtToDate.Text;
        //            if (todate == "")
        //            {
        //                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select To Date ');", true);
        //                return;
        //            }
        //            else
        //            {
        //                DataTable dt = getRequisitionData();
        //                StringBuilder html = new StringBuilder();

        //                html.Append("<table id='example1' class='table table-bordered table-striped'");
        //                html.Append("<thead>");
        //                html.Append("<tr>");
        //                foreach (DataColumn column in dt.Columns)
        //                {
        //                    html.Append("<th>");
        //                    html.Append(column.ColumnName);
        //                    html.Append("</th>");
        //                }
        //                html.Append("</tr>");
        //                html.Append("</thead>");

        //                html.Append("<tbody>");
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    html.Append("<tr>");
        //                    foreach (DataColumn column in dt.Columns)
        //                    {
        //                        html.Append("<td>");
        //                        html.Append(row[column.ColumnName]);
        //                        html.Append("</td>");
        //                    }
        //                    html.Append("</tr>");
        //                }
        //                html.Append("</tbody>");

        //                html.Append("<tfoot>");
        //                html.Append("<tr>");
        //                foreach (DataColumn column in dt.Columns)
        //                {
        //                    html.Append("<th>");
        //                    html.Append(column.ColumnName);
        //                    html.Append("</th>");
        //                }
        //                html.Append("</tr>");
        //                html.Append("</tfoot>");

        //                html.Append("</table>");

        //                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select  Branch');", true);
        //        return;
        //    }
        //}
        //catch
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Company Name ');", true);
        //    return;
        //}

    }







    
}