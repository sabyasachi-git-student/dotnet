using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";

        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = DBAccess.FetchData(String.Format("Exec loGin @userId='{0}',@passWord='{1}',@groupId='{2}',@BranchId='{3}'", txtUserName.Text.Trim(), txtPassword.Text.Trim(), "", "")).Tables[0];
            if ("Logged In" == dt.Rows[0][2].ToString())
            {
                Session["UserId"] = dt.Rows[0][0].ToString();
                string userGroupid = dt.Rows[0][1].ToString();
                Session["BranchId"] = dt.Rows[0][3].ToString();
                Session["UserGroup"] = DBAccess.FetchData("Select UserGroup From tbl_UserGroups Where UserGroupId='" + userGroupid + "'").Tables[0].Rows[0][0].ToString();
                Session["UserGroupId"] = userGroupid.ToString();
                Session["UserGUID"] = Guid.NewGuid();
                Session["Deltemp"] = null;

                try
                {
                    Response.Redirect("~/Project/Home.aspx");
                }
                catch (Exception ex)
                {
                    // ASPxWebControl.RedirectOnCallback("~/Project/Home.aspx");
                }

            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check Id Or Password.');", true);
                return;
            }
        }

        catch
        {

        }

    }
    protected void btnResLogin_Click(object sender, EventArgs e)
    {
        DataTable dt = DBAccess.FetchData(String.Format("Exec loGin @userId='{0}',@passWord='{1}',@groupId='{2}',@BranchId='{3}'", txtUserName.Text.Trim(), txtPassword.Text.Trim(), "", "")).Tables[0];
        if ("Logged In" == dt.Rows[0][2].ToString())
        {
            Session["UserId"] = dt.Rows[0][0].ToString();
            string userGroupid = dt.Rows[0][1].ToString();
            Session["BranchId"] = dt.Rows[0][3].ToString();
            Session["UserGroup"] = DBAccess.FetchData("Select UserGroup From tbl_UserGroups Where UserGroupId='" + userGroupid + "'").Tables[0].Rows[0][0].ToString();
            Session["UserGroupId"] = userGroupid.ToString();
            Session["UserGUID"] = Guid.NewGuid();
            Session["Deltemp"] = null;

            try
            {
                Response.Redirect("~/pages/GreenOxygen/Home.aspx");
            }
            catch (Exception ex)
            {
              
            }

        }
        else
        {
           
        }
    }
}