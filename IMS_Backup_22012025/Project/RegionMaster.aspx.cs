using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_RegionMaster : System.Web.UI.Page
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

        string RegionId = DBAccess.FetchDatatable("select  [dbo].[fn_RegionId]()").Rows[0][0].ToString();

        string RegionName = "";
        if (txtRegionName.Text != "")
        {
            RegionName = txtRegionName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Terrytory Name.');", true);
            return;
        }

        string ManagerName = "";
        if (txtManagerName.Text != "")
        {
            ManagerName = txtManagerName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Manager Name.');", true);
            return;
        }

        string PhoneNo = "";
        if (txtPhoneNo.Text != "")
        {
            PhoneNo = txtPhoneNo.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Phone No.');", true);
            return;
        }

        string EmailID = "";
        if (txtEmailID.Text != "")
        {
            EmailID = txtEmailID.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Email ID.');", true);
            return;
        }

        string Latitude = "";
        if (txtLatitude.Text != "")
        {
            Latitude = txtLatitude.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Latitude.');", true);
            return;
        }

        string Longitude = "";
        if (txtLongitude.Text != "")
        {
            Longitude = txtLongitude.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Longitude.');", true);
            return;
        }

        if (btnSave.Text != "Update")
        {
            int n = MastersSave.RegionSave(RegionId, RegionName, ManagerName, PhoneNo, EmailID, Latitude, Longitude, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Region Master is saved successfully..');window.location='../Project/RegionMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Region Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Region Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdateRegionId = ViewState["RegionId"].ToString();


            int n = MastersSave.RegionUpdate(UpdateRegionId, RegionName, ManagerName, PhoneNo, EmailID, Latitude, Longitude, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Region Master is updated successfully..');window.location='../Project/RegionMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Region Master is not updated. Try again.');", true);
                return;
            }
        }
        
    }
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string RegionId = obj.ToString();
        ViewState["RegionId"] = RegionId;

        DataTable dt = MastersSave.RegionFetchForEdit(RegionId);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtRegionName.Text = dt.Rows[0]["RegionName"].ToString();
            txtManagerName.Text = dt.Rows[0]["ManagerName"].ToString();
            txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
            txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
            txtLatitude.Text = dt.Rows[0]["Latitude"].ToString();
            txtLongitude.Text = dt.Rows[0]["Longitude"].ToString();


            ViewState["RegionId"] = RegionId;
            btnSave.Text = "Update";

        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegionMaster.aspx");
    }

}