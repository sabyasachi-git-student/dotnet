using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Project_SectionMaster : System.Web.UI.Page
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

        string SectionId = DBAccess.FetchDatatable("select  [dbo].[fn_SectionId]()").Rows[0][0].ToString();

        string SectionName = "";
        if (txtSectionName.Text != "")
        {
            SectionName = txtSectionName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Section Name.');", true);
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
            int n = MastersSave.SectionSave(SectionId, SectionName, ManagerName, PhoneNo, EmailID, Latitude, Longitude, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Section Master is saved successfully..');window.location='../Project/SectionMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Section Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Section Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdateSectionId = ViewState["SectionId"].ToString();


            int n = MastersSave.SectionUpdate(UpdateSectionId, SectionName, ManagerName, PhoneNo, EmailID, Latitude, Longitude, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Section Master is updated successfully..');window.location='../Project/SectionMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Section Master is not updated. Try again.');", true);
                return;
            }
        }

    }
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string SectionId = obj.ToString();
        ViewState["SectionId"] = SectionId;

        DataTable dt = MastersSave.SectionFetchForEdit(SectionId);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtSectionName.Text = dt.Rows[0]["SectionName"].ToString();
            txtManagerName.Text = dt.Rows[0]["ManagerName"].ToString();
            txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
            txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
            txtLatitude.Text = dt.Rows[0]["Latitude"].ToString();
            txtLongitude.Text = dt.Rows[0]["Longitude"].ToString();


            ViewState["SectionId"] = SectionId;
            btnSave.Text = "Update";

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("SectionMaster.aspx");
    }
   
    protected void txtSectionName_TextChanged(object sender, EventArgs e)
    {
        string SectionName = txtSectionName.Text;
        string dt1 = DBAccess.FetchDatasingle(@"SELECT u.UserName FROM tbl_User u LEFT JOIN tbl_Branch b ON b.BranchId = u.BranchId LEFT JOIN tbl_UserGroups ug ON ug.UserGroupid=u.UserGroup WHERE b.BranchName='" + SectionName + "'");

        if (dt1 == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! No Data Available.');", true);
            return;
        }
        else
        {
            txtManagerName.Text = dt1;

        }
    }
}