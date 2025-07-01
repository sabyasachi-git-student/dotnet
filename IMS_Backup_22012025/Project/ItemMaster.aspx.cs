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
public partial class Project_ItemMaster : System.Web.UI.Page
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

        string ItemId = DBAccess.FetchDatatable("select  [dbo].[fn_ItemId]()").Rows[0][0].ToString();

        string Category = "";
        if (ddlCategory.Text != "")
        {
            Category = ddlCategory.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Category.');", true);
            return;
        }

        string Type ="";
        try
        {
            Type = ddlType.SelectedValue.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Type.');", true);
            return;
        }

        string ItemName = "";
        if (txtItemName.Text != "")
        {
            ItemName = txtItemName.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Item Name.');", true);
            return;
        }

        string Unit = "";
        if (ddlUnit.Text != "")
        {
            Unit = ddlUnit.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Unit.');", true);
            return;
        }

        string SpaceUnit = "";
        if (txtSpaceUnit.Text != "")
        {
            SpaceUnit = txtSpaceUnit.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Space Unit.');", true);
            return;
        }

        
        string CoderLife = txtCoderLife.Text;
        string Extra1 = txtExtra1.Text;
        string Extra2 = txtExtra2.Text;
        string Make = ddlMake.Text;
        string Model = txtModel.Text;
        string HSNCode = ddlHSNCode.Text;
        string ReOrderLevel = txtReorder.Text;
        string CriticalLevel = txtCriticalLevel.Text;
        string ScrapGroup = ddlScrapGroup.Value.ToString();
        string WarrantyPeriod = txtWarrantyPeriod.Text;
        string Remarks = txtRemarks.Text;


        if (btnSave.Text != "Update")
        {
            int n = MastersSave.ItemSave(ItemId, Category, Type, ItemName, Make, Model, Unit, HSNCode, ReOrderLevel, CriticalLevel, ScrapGroup, WarrantyPeriod, Remarks,  UserId, BranchId, DOE, SpaceUnit, CoderLife, Extra1 , Extra2);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Item Master is saved successfully..');window.location='../Project/ItemMaster.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Item Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Item Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {
            string UpdateItemId = ViewState["ItemId"].ToString();


            int n = MastersSave.ItemUpdate(UpdateItemId, Category, Type, ItemName, Make, Model, Unit, HSNCode, ReOrderLevel, CriticalLevel, ScrapGroup, WarrantyPeriod, Remarks, UserId, BranchId, DOE, SpaceUnit,CoderLife,Extra1,Extra2);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Item Master is updated successfully..');window.location='../Project/ItemMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Item Master is not updated. Try again.');", true);
                return;
            }
        }
    }
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string ItemId = obj.ToString();
        ViewState["ItemId"] = ItemId;

        DataTable dt = MastersSave.ItemFetchForEdit(ItemId);

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlCategory.Text = dt.Rows[0]["Category"].ToString();
            ddlType.Text = dt.Rows[0]["Type"].ToString();
            txtItemName.Text = dt.Rows[0]["ItemName"].ToString();
            ddlMake.Text = dt.Rows[0]["Make"].ToString();
            txtModel.Text = dt.Rows[0]["Model"].ToString();
            ddlUnit.Text = dt.Rows[0]["Unit"].ToString();
            ddlHSNCode.Text = dt.Rows[0]["HSNCode"].ToString();
            txtReorder.Text = dt.Rows[0]["ReOrderLevel"].ToString();
            txtCriticalLevel.Text = dt.Rows[0]["CriticalLevel"].ToString();
            ddlScrapGroup.Text = dt.Rows[0]["ScrapGroup"].ToString();
            txtWarrantyPeriod.Text = dt.Rows[0]["WarrantyPeriod"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            txtSpaceUnit.Text = dt.Rows[0]["SpaceUnit"].ToString();
            txtCoderLife.Text = dt.Rows[0]["CoderLife"].ToString();
            txtExtra1.Text = dt.Rows[0]["Extra1"].ToString();
            txtExtra2.Text = dt.Rows[0]["Extra2"].ToString();
            ViewState["ItemId"] = ItemId;
            btnSave.Text = "Update";

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ItemMaster.aspx");
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