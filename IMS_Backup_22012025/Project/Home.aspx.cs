using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using DevExpress.Web;

public partial class Project_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (  Session["PageAccess"] == "False")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You Have Not Permission Access');", true);
            Session["PageAccess"] = "True";
        }
        if (!IsPostBack)
        {
            string UserId = "";
            if (Session["UserId"] != null)
            {
                UserId = Session["UserId"].ToString();
            }  
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
      
        
            string UserId = "";
            if (Session["UserId"] != null)
            {
                UserId = Session["UserId"].ToString();
            }
           
         

        
    }

    
}