using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_PurposeReqMaster : System.Web.UI.Page
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

        string Id = DBAccess.FetchDatatable("select  [dbo].[fn_Id]()").Rows[0][0].ToString();

        string PurposeName = "";
        if (txtPurposeName.Text != "")
        {
            PurposeName = txtPurposeName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Purpose Name.');", true);
            return;
        }

        

        if (btnSave.Text != "Update")
        {
            int n = MastersSave.PurposeReqSave(Id, PurposeName, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Purpose of Requisition Master is saved successfully..');window.location='../Project/PurposeReqMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Purpose of Requisition Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Purpose of Requisition Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdateId = ViewState["Id"].ToString();


            int n = MastersSave.PurposeReqUpdate(UpdateId, PurposeName, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Purpose of Requisition Master is updated successfully..');window.location='../Project/PurposeReqMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Purpose of Requisition Master is not updated. Try again.');", true);
                return;
            }
        }
    }

    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string Id = obj.ToString();
        ViewState["Id"] = Id;

        DataTable dt = MastersSave.PurposeReqFetchForEdit(Id);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtPurposeName.Text = dt.Rows[0]["PurposeName"].ToString();

            ViewState["Id"] = Id;
            btnSave.Text = "Update";

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurposeReqMaster.aspx");
    }
}