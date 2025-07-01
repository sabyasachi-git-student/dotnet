using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Web;
using System.IO;


public partial class Master_Project : MasterPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["UserId"] != null && Session["UserGroup"] != null)
        {
            bool PageTag = false;
            SqlDataSource1.SelectCommand = String.Format("SELECT ContentID, ContentName, ContentType, ContentLink, ParentContentID, ContentPosition FROM tbl_Contents AS a INNER JOIN tbl_Access AS b ON a.ContentID = b.ContentsId WHERE (a.ContentType <> 'root') AND ({0} = '1' AND ContentVisibility='V')", Session["UserGroup"]);
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            string[] uar=url.Split('/');
            string folder = String.Format("../{0}/{1}", url.Split('/')[uar.Length-2], url.Split('/')[uar.Length-1]);

            //DataTable dtAdminUser = DBAccess.FetchDatatable("select * from [Admin].[UserInfo] where UserName='" + Session["UserId"].ToString() + "'");
            //if (dtAdminUser != null && dtAdminUser.Rows.Count>0)
            //{
            //    DataTable dtBranch = DBAccess.FetchDatatable("select BranchId from tbl_Branch where BranchName='" + dtAdminUser.Rows[0]["BranchName"] + "'");
            //    if(dtBranch!=null && dtBranch.Rows.Count>0)
            //    {
            //        Session["BranchId"] = dtBranch.Rows[0]["BranchId"].ToString();
            //    }
            //}

            if (Convert.ToInt32(DBAccess.FetchData(String.Format("Select COUNT(*) from tbl_Contents a join tbl_Access b on a.ContentID=b.ContentsId and ContentLink='{0}' and {1}='1'", folder, Session["UserGroup"])).Tables[0].Rows[0][0]) == 1)
            {
                string PageName="";
                string status = "";
                ASPxLabel1.Text = ("Welcome, " + Session["UserId"].ToString().ToUpper());
                BuildMenu(ASPxMenu1, SqlDataSource1);

                //Page Access
                DataTable UserDetailsDt = new DataTable();
                UserDetailsDt = DBAccess.FetchData("Exec Get_UserPageAccessInfo @username = '" + Session["UserId"].ToString().ToUpper() + "'").Tables[0];

                for (int i = 0; i < UserDetailsDt.Rows.Count; i++)
                {
                     PageName = UserDetailsDt.Rows[i][5].ToString();
                     status = UserDetailsDt.Rows[i][6].ToString();

                     if (PageName=="../Project/Quotationlist1.aspx")
                     {
                         
                     }

                    if (UserDetailsDt.Rows[i][5].ToString().Trim() == folder )//&& UserDetailsDt.Rows[i][6].ToString() == "True")
                    {
                        PageTag = true;
                        break;
                    }
                }

                if (PageTag == false)
                {
                    Session["PageAccess"] = "False";
                    Response.Redirect("../Project/Home.aspx");

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You Have Not Permission Access'); window.location='../Project/Home.aspx';", true);
                }
                //*****************************
            }
            else
            {
                Response.Redirect("../Project/Home.aspx");
            }

        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBranch();
        }
    }

    private void LoadBranch()
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
        DataTable dt = ClassUserBranchMapping.SaveAndFetchBranch(UserId, BranchId, UserId);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlBranch.Items.Clear();
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();

            ddlBranch.SelectedValue = BranchId;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string a = Path.GetFileName(Request.Url.AbsolutePath);
        Session["BranchId"] = ddlBranch.SelectedValue.ToString();
        Response.Redirect("../Project/" + a);
    }
    protected void BuildMenu(ASPxMenu menu, SqlDataSource dataSource)
    {
        DataSourceSelectArguments arg = new DataSourceSelectArguments();
        DataView dataView = dataSource.Select(arg) as DataView;
        dataView.Sort = "ParentContentID,ContentPosition";

        Dictionary<string, DevExpress.Web.MenuItem> menuItems =
            new Dictionary<string, DevExpress.Web.MenuItem>();

        for (int i = 0; i < dataView.Count; i++)
        {
            DataRow row = dataView[i].Row;
            DevExpress.Web.MenuItem item = CreateMenuItem(row);
            string itemID = row["ContentID"].ToString();
            string parentID = row["ParentContentID"].ToString();

            if (menuItems.ContainsKey(parentID))
                menuItems[parentID].Items.Add(item);
            else
            {
                if (parentID == "0")
                menu.Items.Add(item);
            }
            menuItems.Add(itemID, item);
        }
    }

    private DevExpress.Web.MenuItem CreateMenuItem(DataRow row)
    {
        DevExpress.Web.MenuItem ret = new DevExpress.Web.MenuItem { Text = row["ContentName"].ToString(), NavigateUrl = row["ContentLink"].ToString() };
        return ret;
    }


    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["UserId"] = null;
        Response.Redirect("../Login.aspx");

    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
}
