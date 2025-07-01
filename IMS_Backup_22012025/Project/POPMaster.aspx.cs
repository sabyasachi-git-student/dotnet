using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;
public partial class Project_POPMaster : System.Web.UI.Page
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

        string POPId = DBAccess.FetchDatatable("select  [dbo].[fn_POPId]()").Rows[0][0].ToString();

        string POPName = "";
        if (txtPOPName.Text != "")
        {
            POPName = txtPOPName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter POP Name.');", true);
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
            int n = MastersSave.POPSave(POPId, POPName, ManagerName, PhoneNo, EmailID, Latitude, Longitude, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('POP Master is saved successfully..');window.location='../Project/POPMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This POP Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! POP Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdatePOPId = ViewState["POPId"].ToString();


            int n = MastersSave.POPUpdate(UpdatePOPId, POPName, ManagerName, PhoneNo, EmailID, Latitude, Longitude, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('POP Master is updated successfully..');window.location='../Project/POPMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! POP Master is not updated. Try again.');", true);
                return;
            }
        }

    }
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string POPId = obj.ToString();
        ViewState["POPId"] = POPId;

        DataTable dt = MastersSave.POPFetchForEdit(POPId);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtPOPName.Text = dt.Rows[0]["POPName"].ToString();
            txtPOPName.Enabled = false;
            txtManagerName.Text = dt.Rows[0]["ManagerName"].ToString();
            txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
            txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
            txtLatitude.Text = dt.Rows[0]["Latitude"].ToString();
            txtLongitude.Text = dt.Rows[0]["Longitude"].ToString();


            ViewState["POPId"] = POPId;
            btnSave.Text = "Update";

        }
    }
  
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("POPMaster.aspx");
    }

    protected void txtPOPName_TextChanged(object sender, EventArgs e)
    {
        string POPName = txtPOPName.Text;
        string dt1 = DBAccess.FetchDatasingle(@"SELECT u.UserName FROM tbl_User u LEFT JOIN tbl_Branch b ON b.BranchId = u.BranchId LEFT JOIN tbl_UserGroups ug ON ug.UserGroupid=u.UserGroup WHERE b.BranchName='" + POPName + "'");

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

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptions());
    }
    protected void btnXlsxExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptions());
    }
}