using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_ProjectMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        DateTime DOE = DateTime.Now;

        string ProjectId = DBAccess.FetchDatatable("select  [dbo].[fn_ProjectId]()").Rows[0][0].ToString();

        string ProjectName = "";
        if (txtProjectName.Text != "")
        {
            ProjectName = txtProjectName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Project Name.');", true);
            return;
        }
        string ProjectDescription = "";
        if (txtProjectDescription.Text != "")
        {
            ProjectDescription = txtProjectDescription.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Project Description.');", true);
            return;
        }

        string ProjectInName = "";
        if (txtProjectInName.Text != "")
        {
            ProjectInName = txtProjectInName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Project Incharge Name.');", true);
            return;
        }

        string MobileNo = "";
        if (txtMobileNo.Text != "")
        {
            MobileNo = txtMobileNo.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Project Description.');", true);
            return;
        }

        if (btnSave.Text != "Update")
        {
            int n = MastersSave.ProjectSave(ProjectId, ProjectName, ProjectDescription, ProjectInName, MobileNo, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Project Master is saved successfully..');window.location='../Project/ProjectMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Project Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Project Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdateProjectId = ViewState["ProjectId"].ToString();


            int n = MastersSave.ProjectUpdate(UpdateProjectId, ProjectName, ProjectDescription, ProjectInName, MobileNo, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Project Master is updated successfully..');window.location='../Project/ProjectMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Project Master is not updated. Try again.');", true);
                return;
            }
        }
    }
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string ProjectId = obj.ToString();
        ViewState["ProjectId"] = ProjectId;

        DataTable dt = MastersSave.ProjectFetchForEdit(ProjectId);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtProjectName.Text = dt.Rows[0]["ProjectName"].ToString();
            txtProjectDescription.Text = dt.Rows[0]["ProjectDescription"].ToString();
            txtProjectInName.Text = dt.Rows[0]["ProjectInName"].ToString();
            txtMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
            ViewState["ProjectId"] = ProjectId;
            btnSave.Text = "Update";

        }
    }
}