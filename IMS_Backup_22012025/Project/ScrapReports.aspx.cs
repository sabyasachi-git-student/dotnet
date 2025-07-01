using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Project_ScrapReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string UserId = "";
            string BranchId = "";
            try
            {
                UserId = Session["UserId"].ToString();

                BranchId = Session["BranchId"].ToString();
            }
            catch { }
        }
    }
}