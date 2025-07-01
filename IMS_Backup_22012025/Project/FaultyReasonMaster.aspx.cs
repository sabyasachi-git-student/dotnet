using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_FaultyReasonMaster : System.Web.UI.Page
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

        string FaultyReId = DBAccess.FetchDatatable("select  [dbo].[fn_FaultyReId]()").Rows[0][0].ToString();

        string FaultyReason = "";
        if (txtFaultyReason.Text != "")
        {
            FaultyReason = txtFaultyReason.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Faulty Reason.');", true);
            return;
        }
        string FaultyDescription = txtFaultyDescription.Text;
        //if (txtFaultyDescription.Text != "")
        //{
        //    FaultyDescription = txtFaultyDescription.Text;
        //}
        //else
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Faulty Description.');", true);
        //    return;
        //}
        string FaultyType = "";
        if (txtFaultyType.Text != "")
        {
            FaultyType = txtFaultyType.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Faulty Type.');", true);
            return;
        }




        if (btnSave.Text != "Update")
        {
            int n = MastersSave.FaultyReasonSave(FaultyReId,FaultyType, FaultyReason, FaultyDescription, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Faulty Reason Master is saved successfully..');window.location='../Project/FaultyReasonMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Faulty Reason Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Faulty Reason Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdateFaultyReId = ViewState["FaultyReId"].ToString();


            int n = MastersSave.FaultyReasonUpdate(UpdateFaultyReId, FaultyType, FaultyReason, FaultyDescription, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Faulty Reason Master is updated successfully..');window.location='../Project/FaultyReasonMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Faulty Reason Master is not updated. Try again.');", true);
                return;
            }
        }
    }
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string FaultyReId = obj.ToString();
        ViewState["FaultyReId"] = FaultyReId;

        DataTable dt = MastersSave.FaultyReasonFetchForEdit(FaultyReId);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtFaultyType.Text = dt.Rows[0]["FaultyType"].ToString();
            txtFaultyReason.Text = dt.Rows[0]["FaultyReason"].ToString();
            txtFaultyDescription.Text = dt.Rows[0]["FaultyDescription"].ToString();
            ViewState["FaultyReId"] = FaultyReId;
            btnSave.Text = "Update";

        }
    }
}