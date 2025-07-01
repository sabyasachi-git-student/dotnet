using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_RackSpaceMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("RackSpaceMaster.aspx");
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

        string ProcessId = DBAccess.FetchDatatable("select  [dbo].[fn_ProcessId]()").Rows[0][0].ToString();

        string Row = "";
        if (ddlRow.Text != "")
        {
            Row = ddlRow.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Row.');", true);
            return;
        }

        string Rack = "";
        if (ddlRack.Text != "")
        {
            Rack = ddlRack.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Rack.');", true);
            return;
        }

        string Shelf = "";
        if (ddlShelf.Text != "")
        {
            Shelf = ddlShelf.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Shelf.');", true);
            return;
        }

        if (btnSave.Text != "Update")
        {
            int n = MastersSave.RackSpaceSave(ProcessId, Row, Rack, Shelf, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Rack Space Master is saved successfully..');window.location='../Project/RackSpaceMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Rack Space Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Rack Space Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdateProcessId = ViewState["ProcessId"].ToString();


            int n = MastersSave.RackSpaceUpdate(UpdateProcessId, Row, Rack, Shelf, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Rack Space Master is updated successfully..');window.location='../Project/RackSpaceMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Rack Space Master is not updated. Try again.');", true);
                return;
            }
        }
    }

    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string ProcessId = obj.ToString();
        ViewState["ProcessId"] = ProcessId;

        DataTable dt = MastersSave.RackSpaceFetchForEdit(ProcessId);

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlRow.Text = dt.Rows[0]["Row"].ToString();
            ddlRack.Text = dt.Rows[0]["Rack"].ToString();
            ddlShelf.Text = dt.Rows[0]["Shelf"].ToString();


            ViewState["ProcessId"] = ProcessId;
            btnSave.Text = "Update";

        }
    }
}