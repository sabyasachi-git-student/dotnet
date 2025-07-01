using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Project_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        string UserId = "";
        string BranchId = "";

        try
        {
            UserId = Session["UserId"].ToString();
        }
        catch

        { }
        try
        {
            BranchId = Session["BranchId"].ToString();
        }

        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
            return;
        }
        
        string CurrentPassword=txtCurrentPassword.Text;

        if(CurrentPassword=="")
        {
               ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Enter Current Password ');", true);
            return;
        }
        string NewPassword=txtNewPassword.Text;
         if(NewPassword=="")
        {
               ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Enter New Password ');", true);
            return;
        }

       DataTable dt= DBAccess.FetchDatatable("select * from tbl_User where UserName='"+UserId+"' and Password='"+CurrentPassword+"'");
       if (dt.Rows.Count > 0)
       {

           bool Save = DBAccess.SaveData("update  tbl_User set Password='"+NewPassword+"' where UserName='"+UserId+"' and Password='"+CurrentPassword+"'");
           if (Save == true)
           {

               bool SaveUnfo = DBAccess.SaveData("update Admin.UserInfo set UserPassword='" + NewPassword + "' where UserName='" + UserId + "'");

               ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Password Change Successfully '); window.location='../Login.aspx';", true);
           }
       }
       else
       {
           ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Sorry Current password does't match ! ');", true);
           return;
       }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}