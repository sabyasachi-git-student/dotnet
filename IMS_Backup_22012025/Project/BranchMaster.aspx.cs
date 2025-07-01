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

public partial class Project_BranchMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        string BranchId = getBranchId();

        string BranchName="";
        if (txtBranchName.Text != "")
        {
            BranchName = txtBranchName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Branch Name.');", true);
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
        string Priority = "High";
        string ParentId = "Main";

        string dt11 = DBAccess.FetchDatasingle("select BranchName from tbl_Branch where BranchName='" + BranchName + "'");

        if (dt11 != "" )
 
        {
            txtLongitude.Text = "";
            txtLatitude.Text = "";
            txtBranchName.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Branch Name Already Exists.');", true);
            return;
        }


        if (btnSave.Text != "Update")
        {
            bool Save = DBAccess.SaveData(@"insert into [dbo].[tbl_Branch] values ('" + BranchId + "', '" + BranchName + "', '" + BranchName + "', '" + Latitude + "', '" + Longitude + "', '" + Priority + "', '" + ParentId + "')");

            if (Save == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Branch Name saved successfully..');window.location='../Project/BranchMaster.aspx';", true);
            }
            //else if (Save == -1)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This POP Master is already exist.');", true);
            //    return;
            //}
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! POP Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            //string UpdatePOPId = ViewState["POPId"].ToString();


            //int n = MastersSave.POPUpdate(UpdatePOPId, POPName, ManagerName, PhoneNo, EmailID, Latitude, Longitude, UserId, BranchId, DOE);
            //if (n == 1)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('POP Master is updated successfully..');window.location='../Project/BranchMaster.aspx';", true);
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! POP Master is not updated. Try again.');", true);
            //    return;
            //}
        }

    }

    protected string getBranchId()
    {
        Int32 serial = (Int32)DBAccess.FetchData("Select Coalesce(Max(Convert(int,Right(BranchId,(LEN(BranchId)-2)))),'0') from tbl_Branch where BranchId!='Main'").Tables[0].Rows[0][0];
        Int32 s = serial + 1;
        string brid = "GD" + s;
        return brid;
    }
 

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("BranchMaster.aspx");
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