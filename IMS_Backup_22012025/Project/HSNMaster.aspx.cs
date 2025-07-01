using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_HSNMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

   
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        int RowId = 0;
        try
        {
            RowId = Convert.ToInt32(obj);
        }
        catch { }
        DataTable dt = ClassHSNCodeMaster.FetchForEdit(RowId);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtHSN.Text = dt.Rows[0]["HSNCode"].ToString();
            try
            {
                ddlCGST.Text = dt.Rows[0]["CGST"].ToString();
            }
            catch { }
            try
            {
                ddlSGST.Text = dt.Rows[0]["SGST"].ToString();
            }
            catch { }
            try
            {
                ddlIGST.Text = dt.Rows[0]["IGST"].ToString();
            }
            catch { }
            ddlCGST.Enabled = false;
            ddlSGST.Enabled = false;
            //txtCESS.Text = dt.Rows[0]["CESS"].ToString();
            txtHSN.Enabled = false;
            ViewState["RowId"] = RowId;
            btnSave.Text = "Update";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string HSNCode = "";
        if (txtHSN.Text != "")
        {
            HSNCode = txtHSN.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter HSN/SAC Code.');", true);
            return;
        }
        decimal CGST = 0;
        try
        {
            CGST = Convert.ToDecimal(ddlCGST.Text);
        }
        catch { }
        decimal SGST = 0;
        try
        {
            SGST = Convert.ToDecimal(ddlSGST.Text);
        }
        catch { }
        decimal IGST = 0;
        try
        {
            IGST = Convert.ToDecimal(ddlIGST.Text);
        }
        catch { }

        decimal CESS = 0;
        
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
        if (btnSave.Text != "Update")
        {
            int n = ClassHSNCodeMaster.Save(HSNCode, CGST, SGST, IGST, CESS, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('HSN Code is saved successfully..');window.location='../Project/HSNMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This HSN Code is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! HSN Code is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            int RowId = 0;
            try
            {
                RowId = Convert.ToInt32(ViewState["RowId"]);
            }
            catch { }
            int n = ClassHSNCodeMaster.Update(RowId, HSNCode, CGST, SGST, IGST, CESS, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('HSN Code is updated successfully..');window.location='../Project/HSNMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! HSN Code is not updated. Try again.');", true);
                return;
            }
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("HSNMaster.aspx");
    }
    
    protected void ddlIGST_TextChanged(object sender, EventArgs e)
    {

        decimal IGST = Convert.ToDecimal(ddlIGST.Text);

        decimal twotax = IGST / 2;
        ddlCGST.Text = twotax.ToString();
        ddlCGST.Enabled = false;
        ddlSGST.Text = twotax.ToString();
        ddlSGST.Enabled = false;
    }
}