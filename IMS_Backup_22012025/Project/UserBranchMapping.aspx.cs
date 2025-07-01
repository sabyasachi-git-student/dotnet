using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_UserBranchMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBranch();
        }
    }

    protected void ASPxGridView2_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName == "Delete")
        {
            object obj = e.KeyValue;
            string s = obj.ToString();
            string UserId = "";
            string BranchId = "";
            try
            {
                BranchId = s.Split(',')[1].ToString();
                UserId = s.Split(',')[0].ToString();
            }
            catch { }
            int n = ClassUserBranchMapping.Delete(UserId, BranchId);
            if (n == 1)
            {
                DataTable dt = ClassUserBranchMapping.FetchCurrentBranchId(UserId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Session["BranchId"] = dt.Rows[0]["BranchId"].ToString();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Data deleted successfully');window.location='../Project/UserBranchMapping.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! You cannot delete as only one branch is assigned to this user.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data not deleted. Try again.');", true);
                return;
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "lightbox_open('id03')", true);
    }

    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName == "ADD")
        {
            object obj = e.KeyValue;
            string s = obj.ToString();
            string Username = "";
            string UserGroup = "";
            try
            {
                Username = s.Split(',')[0].ToString();
                UserGroup = s.Split(',')[1].ToString();
            }
            catch { }
            txtUserGroup.Text = UserGroup;
            txtUsername.Text = Username;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UserId = txtUsername.Text;
        if (UserId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please add a Username');", true);
            return;
        }
        if (ddlBranch.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select a Branch.');", true);
            return;
        }
        string BranchId = ddlBranch.SelectedValue.ToString();
        string EnterBy = "";
        try
        {
            EnterBy = Session["UserId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;
        int n = ClassUserBranchMapping.Save(UserId, BranchId, EnterBy, DOE);
        if (n == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Data saved successfully');window.location='../Project/UserBranchMapping.aspx';", true);
        }
        else if (n == -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Branch & User combinaiton is already exist.');", true);
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data not saved. Try again.');", true);
            return;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserBranchMapping.aspx");
    }

    private void LoadBranch()
    {
        DataTable dt = DBAccess.FetchDatatable("select BranchId,BranchName from dbo.tbl_Branch");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
        }
    }
}