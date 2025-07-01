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
using System.Globalization;

public partial class pages_GreenOxygen_BOQExtraWorkReports : System.Web.UI.Page
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
    


    private DataTable getRequisitionData()
    {
        string data = "";

        string UserId = Session["UserId"].ToString();
        string ProjectId = ddl_Project.SelectedValue.ToString();
        string CompanyId = ddl_Company.SelectedValue.ToString();

        string fromdate = txtFromDate.Text;
        string todate = txtToDate.Text;

            DataTable dt;


            dt = DBAccess.FetchDatatable("select BOQmode, BOQname, Srno, ItemDescription, WorkDescription as WorkDes,AccUnit, (select Sum (be.AmtTotal) from tbl_BOQExtraWork be where be.EXTRWRID=bm.EXTRWRID) as ItemTotal,(select Sum (bel.TotalRate) from tbl_BOQExtraWorkLabour bel where bel.EXTRWRID=bm.EXTRWRID) as LabourTotal,Remark, CONVERT(varchar, UserDate, 103) as Date from tbl_BOQExtraWorkMain bm where bm.ProjectId='" + ProjectId + "' and convert(varchar,bm.UserDate,1) BETWEEN '" + fromdate + "' and '" + todate + "'");

            if (dt.Rows.Count > 0)
            {
                string BOQmode = dt.Rows[0]["BOQmode"].ToString();
                string BOQname = dt.Rows[0]["BOQname"].ToString();
                string Srno = dt.Rows[0]["Srno"].ToString();
                string ItemDescription = dt.Rows[0]["ItemDescription"].ToString();
                string WorkDes = dt.Rows[0]["WorkDes"].ToString();
                string AccUnit = dt.Rows[0]["AccUnit"].ToString();
                string ItemTotal = dt.Rows[0]["ItemTotal"].ToString();
                string LabourTotal = dt.Rows[0]["LabourTotal"].ToString();
                string Remark = dt.Rows[0]["Remark"].ToString();
                string Date = dt.Rows[0]["Date"].ToString();
                
            }
            //else
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' No Record Avilavail ');", true);
            //}

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