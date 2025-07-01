using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_ReasonsFoPriorities : System.Web.UI.Page
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

        string PrioritiesId = DBAccess.FetchDatatable("select  [dbo].[fn_PrioritiesId]()").Rows[0][0].ToString();

        string PrioritiesName = "";
        if (txtPrioritiesName.Text != "")
        {
            PrioritiesName = txtPrioritiesName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Purpose Name.');", true);
            return;
        }



        if (btnSave.Text != "Update")
        {
            int n = MastersSave.PrioritiesSave(PrioritiesId, PrioritiesName, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Reasons Fo Priorities Master is saved successfully..');window.location='../Project/ReasonsFoPriorities.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Reasons Fo Priorities Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Reasons Fo Priorities Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdatePrioritiesId = ViewState["PrioritiesId"].ToString();


            int n = MastersSave.PrioritiesUpdate(UpdatePrioritiesId, PrioritiesName, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Reasons Fo Priorities Master is updated successfully..');window.location='../Project/ReasonsFoPriorities.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Reasons Fo Priorities Master is not updated. Try again.');", true);
                return;
            }
        }
    }

    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string PrioritiesId = obj.ToString();
        ViewState["PrioritiesId"] = PrioritiesId;

        DataTable dt = MastersSave.PrioritiesFetchForEdit(PrioritiesId);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtPrioritiesName.Text = dt.Rows[0]["PrioritiesName"].ToString();

            ViewState["PrioritiesId"] = PrioritiesId;
            btnSave.Text = "Update";

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReasonsFoPriorities.aspx");
    }
}