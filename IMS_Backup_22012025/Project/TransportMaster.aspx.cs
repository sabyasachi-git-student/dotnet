using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_TransportMaster : System.Web.UI.Page
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

        string TransportId = DBAccess.FetchDatatable("select  [dbo].[fn_TransportId]()").Rows[0][0].ToString();

        string TransportName = "";
        if (txtPrioritiesName.Text != "")
        {
            TransportName = txtPrioritiesName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Transport Name.');", true);
            return;
        }



        if (btnSave.Text != "Update")
        {
            int n = MastersSave.TransportSave(TransportId, TransportName, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Reasons Fo Transport Master is saved successfully..');window.location='../Project/TransportMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Reasons Fo Transport Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Reasons Fo Transport Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdateTransportId = ViewState["TransportId"].ToString();


            int n = MastersSave.TransportUpdate(UpdateTransportId, TransportName, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Reasons Fo Transport Master is updated successfully..');window.location='../Project/TransportMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Reasons Fo Transport Master is not updated. Try again.');", true);
                return;
            }
        }
    }
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string TransportId = obj.ToString();
        ViewState["TransportId"] = TransportId;

        DataTable dt = MastersSave.TransportFetchForEdit(TransportId);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtPrioritiesName.Text = dt.Rows[0]["TransportName"].ToString();

            ViewState["TransportId"] = TransportId;
            btnSave.Text = "Update";

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransportMaster.aspx");
    }
}