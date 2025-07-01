using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_ScrapMaster : System.Web.UI.Page
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

        string ScrapId = DBAccess.FetchDatatable("select  [dbo].[fn_ScrapId]()").Rows[0][0].ToString();

        string GroupName = "";
        if (ddlGroupName.Text != "")
        {
            GroupName = ddlGroupName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Group Name.');", true);
            return;
        }

        string ItemName = "";
        if (txtItemName.Text != "")
        {
            ItemName = txtItemName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Item Name.');", true);
            return;
        }

        string Unit = "";
        if (ddlUnit.Text != "")
        {
            Unit = ddlUnit.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Unit.');", true);
            return;
        }

        if (btnSave.Text != "Update")
        {
            int n = MastersSave.ScrapSave(ScrapId, GroupName, ItemName, Unit, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Scrap Master is saved successfully..');window.location='../Project/ScrapMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Scrap Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Scrap Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdateScrapId = ViewState["ScrapId"].ToString();


            int n = MastersSave.ScrapUpdate(UpdateScrapId, GroupName, ItemName, Unit, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Scrap Master is updated successfully..');window.location='../Project/ScrapMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Scrap Master is not updated. Try again.');", true);
                return;
            }
        }
    }
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string ScrapId = obj.ToString();
        ViewState["ScrapId"] = ScrapId;

        DataTable dt = MastersSave.ScrapFetchForEdit(ScrapId);

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlGroupName.Text = dt.Rows[0]["GroupName"].ToString();
            txtItemName.Text = dt.Rows[0]["ItemName"].ToString();
            ddlUnit.Text = dt.Rows[0]["Unit"].ToString();
            

            ViewState["ScrapId"] = ScrapId;
            btnSave.Text = "Update";

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ScrapMaster.aspx");
    }
}