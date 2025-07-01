using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class Project_RequisitionPop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItem();
        }
        
        if (!IsPostBack)
        {
            string UserId = "";
            string BranchId = "";
            string UserGroup = "";
            Session["UserGroupId1"] = "";
            try
            {
                UserGroup = Session["UserGroupId"].ToString();
                string UserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='" + UserGroup + "'");
                txtUserGroup.Text = UserGroupName.ToString();
                string ToUserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='UG15'");
            }
            catch

            { }
            try
            {
                UserId = Session["UserId"].ToString();
                txtReqUser.Text = UserId.ToString();

                string UserName = DBAccess.FetchDatasingle("select ug.UserGroup from tbl_User u join tbl_UserGroups ug on u.UserGroup=ug.UserGroupId  where UserName='" + UserId + "'");
                if (UserName == "SuperAdmin")
                {
                    dtpDate.Date = DateTime.Now;
                }
                else
                {
                    dtpDate.Date = DateTime.Now;
                    dtpDate.Enabled = false;
                }
            }
            catch

            { }
            try
            {
                BranchId = Session["BranchId"].ToString();
                string BranchName = DBAccess.FetchDatasingle("select BranchName from tbl_Branch where BranchId='" + BranchId + "'");
                txtBranchName.Text = BranchName.ToString();
                Session["BranchName"] = BranchName.ToString();
                txtReqId.Text = BranchId.ToString();
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
                return;
            }

        }
    }
    protected DataTable getItem()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Category";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Type";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Make";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Model";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Unit";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Qty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "rate";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "HSNCode";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "IGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "SGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqToQty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "POPQty";
        oTable.Columns.Add(dtCol);

        return oTable;
    }
    protected void gvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemDetails.PageIndex = e.NewPageIndex;
        this.gvItemDetails.DataSource = (DataTable)ViewState["ItemDetails"];
        this.gvItemDetails.DataBind();
    }
    protected void gvItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt8 = new DataTable();
            dt8 = (DataTable)ViewState["ItemDetails"];
            dt8.Rows[rowindex].Delete();
            dt8.AcceptChanges();
            ViewState["ItemDetails"] = dt8;
            gvItemDetails.DataSource = dt8;
            gvItemDetails.DataBind();
        }
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnChooseRawMaterial_Click(object sender, EventArgs e)
    {
        string ReUserId = "";
        try
        {
            ReUserId = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to.');", true);
            return;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
    }
    protected void btnAddServiceItem_Click(object sender, EventArgs e)
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
        string ItemId = "";
        string RequisitiontoBranchId = "";
        try
        {
            RequisitiontoBranchId = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to BranchId');", true);
            return;
        }

        List<object> fieldValues = gvItem.GetSelectedFieldValues(new string[] { "ItemId" });
        if (fieldValues.Count != 0)
        {
            DataTable AppQtn = new DataTable();

            for (int j = 0; j < fieldValues.Count; j++)
            {
                ItemId = fieldValues[j].ToString();

                DataTable dt8 = (DataTable)ViewState["ItemDetails"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        if (ItemId == dt8.Rows[i]["ItemId"].ToString())
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
                            return;
                        }
                    }
                }

                DataTable dtI = (DataTable)ViewState["ItemDetails"];
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty, h.CGST, h.IGST, h.SGST, h.CESS, '' as RQty, '' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as POPQty,(select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + RequisitiontoBranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as ReqToQty from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode  where rm.ItemId='" + ItemId + "'");
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                    DataRow drH = dtI.NewRow();
                    drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                    drH["Category"] = dt11.Rows[0]["Category"].ToString();
                    drH["Type"] = dt11.Rows[0]["Type"].ToString();
                    drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                    drH["Make"] = dt11.Rows[0]["Make"].ToString();
                    drH["Model"] = dt11.Rows[0]["Model"].ToString();
                    drH["Unit"] = dt11.Rows[0]["Unit"].ToString();
                    drH["Qty"] = dt11.Rows[0]["Qty"].ToString();
                    drH["rate"] = dt11.Rows[0]["rate"].ToString();
                    drH["HSNCode"] = dt11.Rows[0]["HSNCode"].ToString();
                    drH["CGST"] = dt11.Rows[0]["CGST"].ToString();
                    drH["IGST"] = dt11.Rows[0]["IGST"].ToString();
                    drH["SGST"] = dt11.Rows[0]["SGST"].ToString();
                    drH["ReqToQty"] = dt11.Rows[0]["ReqToQty"].ToString();
                    drH["POPQty"] = dt11.Rows[0]["POPQty"].ToString();

                    dtI.Rows.Add(drH);

                    ViewState["ItemDetails"] = dtI;
                    gvItemDetails.DataSource = dtI;
                    gvItemDetails.DataBind();
                }

            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequisitionPop.aspx");
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

        string UserGroupId = "";
        try
        {
            UserGroupId = Session["UserGroupId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;

        string ReqPopId = DBAccess.FetchDatatable("select [dbo].[fn_ReqPopId]()").Rows[0][0].ToString();
        string RequisitionDate = dtpDate.Text;
        string PrioritiesId = "";
        try
        {
            PrioritiesId = ddlRequisitionPur.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition Purpose .');", true);
            return;
        }
        string RequisitionPurpose = ddlRequisitionPur.Text;
        string ReUserGroupId = ddlRequToGrp.SelectedValue.ToString();
        string ReUserGroupName = ddlRequToGrp.SelectedItem.ToString();
        string ReUserName = ddlRequisitionto.Text;
        string Remarks = txtRemarks.Text;
        string Status = ddlRequToUser.Text;
        string Status1 = ddlProject.Text;
        string ReUserId = "";
        try
        {
            ReUserId = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to.');", true);
            return;
        }
        string Status2 = "";
        try
        {
            Status2 = ddlProject.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Project.');", true);
            return;
        }

        string Status3 = "";
        string Status4 = txtBranchName.Text;
        string Status5 = txtUserGroup.Text;
        

        int n = ClassRequisitions.RequisitionsPOPSave(ReqPopId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, UserGroupId, UserId, BranchId, DOE);


        if (n == 1)
        {
            for (int i = 0; i < gvItemDetails.Rows.Count; i++)
            {
                string ReqPopItemId = DBAccess.FetchDatatable("select [dbo].[fn_ReqPopItemId]()").Rows[0][0].ToString();
                string ItemId = gvItemDetails.Rows[i].Cells[1].Text;
                string Category = gvItemDetails.Rows[i].Cells[2].Text;
                string ItemName = gvItemDetails.Rows[i].Cells[4].Text;
                string Make = gvItemDetails.Rows[i].Cells[5].Text;
                string Model = gvItemDetails.Rows[i].Cells[6].Text;
                string Unit = gvItemDetails.Rows[i].Cells[7].Text;
                string Status6 = "";
                decimal ReqToQty = 0;
                try
                {
                    ReqToQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[8].Text);
                    
                }
                catch
                { }
                decimal POPQty = 0;
                try
                {
                   
                    POPQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[9].Text);
                }
                catch
                { }
                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                decimal Qty = Convert.ToDecimal(txtItemQty.Text);

                //if (ReqToQty < Qty)
                //{
                //    bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPop]  where ReqPopId='" + ReqPopId + "' ");
                //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check the ReqToQty.');", true);
                //    return;
                //}

                decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                int m = ClassRequisitions.RequisitionsPOPSaveDetails(ReqPopId, ReqPopItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                if (m == 1)
                {

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionPop.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Sent Successfully. Try again.');", true);
                    return;
                }
            }
        }
    }
    protected void ddlRequToGrp_TextChanged(object sender, EventArgs e)
    {

        string PopName = txtBranchName.Text;
        string RequToGrp = ddlRequToGrp.SelectedValue.ToString();
        if (RequToGrp == "UG15")
        {
            DataTable dt1 = DBAccess.FetchDatatable("select Distinct a.SectionId  as Id, b.BranchId from tbl_HigherKeyMapping a join tbl_Branch b on a.SectionId=b.BranchName where POPId='" + PopName + "' ");
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                ddlRequisitionto.DataSource = dt1;
                ddlRequisitionto.Text = "Id";
                ddlRequisitionto.Value = "BranchId";
                ddlRequisitionto.DataBind();
                ddlRequisitionto.SelectedIndex = 0;
            }
        }
        else
        {
            DataTable dt1 = DBAccess.FetchDatatable("select Distinct a.TerrytoryId as Id, b.BranchId from tbl_HigherKeyMapping a join tbl_Branch b on a.TerrytoryId=b.BranchName where POPId='" + PopName + "' ");
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                ddlRequisitionto.DataSource = dt1;
                ddlRequisitionto.Text = "Id";
                ddlRequisitionto.Value = "BranchId";
                ddlRequisitionto.DataBind();
                ddlRequisitionto.SelectedIndex = 0;
            }
        }

        
        string Requisitionto = "";
        try
        {
            Requisitionto = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ddlRequToGrp.SelectedIndex = 0;
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please check Hierarchy Mapping.');", true);
            return;
        }
        DataTable dtt = DBAccess.FetchDatatable("select UserName from tbl_User where BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "' union select a.UserName from tbl_User a join tbl_UserBranchMapping c on a.UserName=c.UserId where c.BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "'");
         if (dtt != null && dtt.Rows.Count > 0)
         {
             ddlRequToUser.DataSource = dtt;
             ddlRequToUser.Text = "UserName";
             ddlRequToUser.Value = "UserName";
             ddlRequToUser.DataBind();
             ddlRequToUser.SelectedIndex = 0;
         }
         else
         {
             ddlRequToUser.DataSource = "";
             ddlRequToUser.Text = "";
             ddlRequToUser.Value = "";
             ddlRequToUser.DataBind();
             ddlRequToUser.SelectedIndex = 0;
             ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Not Have UserId. Try again.');", true);
             return;
             
         }
    }
    
    protected void ddlRequisitionto_TextChanged(object sender, EventArgs e)
    {
        string PopName = txtBranchName.Text;
        string RequToGrp = ddlRequToGrp.SelectedValue.ToString();
        string Requisitionto = ddlRequisitionto.Value.ToString();


        DataTable dtt = DBAccess.FetchDatatable("select UserName from tbl_User where BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "'union select a.UserName, a.FullName from tbl_User a join tbl_UserBranchMapping c on a.UserName=c.UserId where c.BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "'");
        if (dtt != null && dtt.Rows.Count > 0)
        {
            ddlRequToUser.DataSource = dtt;
            ddlRequToUser.Text = "UserName";
            ddlRequToUser.Value = "UserName";
            ddlRequToUser.DataBind();
            ddlRequToUser.SelectedIndex = 0;
        }
        else
        {
            ddlRequToUser.DataSource = "";
            ddlRequToUser.Text = "";
            ddlRequToUser.Value = "";
            ddlRequToUser.DataBind();
            ddlRequToUser.SelectedIndex = 0;
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Not Have UserId. Try again.');", true);
            return;
            
        }
    }

    protected void ddlRequisitionPur_TextChanged(object sender, EventArgs e)
    {
        string Reson = ddlRequisitionPur.Text;

        //btnChooseRawMaterial.Visible = false;

        if (Reson == "Faulty Replacement")
        {
            btnChooseRawMaterial.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id04').style.display='block'", true);
        }
        else
        {
            btnChooseRawMaterial.Visible = true;
        }

    }
    protected void btnAddFaltyItem_Click(object sender, EventArgs e)
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
        string ItemId = "";
        string RequisitiontoBranchId = "";
        try
        {
            RequisitiontoBranchId = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to BranchId');", true);
            return;
        }

        List<object> fieldValues = ASPxGridView1.GetSelectedFieldValues(new string[] { "ItemId" });
        if (fieldValues.Count != 0)
        {
            DataTable AppQtn = new DataTable();

            for (int j = 0; j < fieldValues.Count; j++)
            {
                ItemId = fieldValues[j].ToString();

                DataTable dt8 = (DataTable)ViewState["ItemDetails"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        if (ItemId == dt8.Rows[i]["ItemId"].ToString())
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
                            return;
                        }
                    }
                }

                DataTable dtI = (DataTable)ViewState["ItemDetails"];
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty, h.CGST, h.IGST, h.SGST, h.CESS, '' as RQty, '' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as POPQty,(select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + RequisitiontoBranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as ReqToQty from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode  where rm.ItemId='" + ItemId + "'");
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                    DataRow drH = dtI.NewRow();
                    drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                    drH["Category"] = dt11.Rows[0]["Category"].ToString();
                    drH["Type"] = dt11.Rows[0]["Type"].ToString();
                    drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                    drH["Make"] = dt11.Rows[0]["Make"].ToString();
                    drH["Model"] = dt11.Rows[0]["Model"].ToString();
                    drH["Unit"] = dt11.Rows[0]["Unit"].ToString();
                    drH["Qty"] = dt11.Rows[0]["Qty"].ToString();
                    drH["rate"] = dt11.Rows[0]["rate"].ToString();
                    drH["HSNCode"] = dt11.Rows[0]["HSNCode"].ToString();
                    drH["CGST"] = dt11.Rows[0]["CGST"].ToString();
                    drH["IGST"] = dt11.Rows[0]["IGST"].ToString();
                    drH["SGST"] = dt11.Rows[0]["SGST"].ToString();
                    drH["ReqToQty"] = dt11.Rows[0]["ReqToQty"].ToString();
                    drH["POPQty"] = dt11.Rows[0]["POPQty"].ToString();

                    dtI.Rows.Add(drH);

                    ViewState["ItemDetails"] = dtI;
                    gvItemDetails.DataSource = dtI;
                    gvItemDetails.DataBind();
                }

            }
        }
    }
    protected void btnCodeSerch_Click(object sender, EventArgs e)
    {
        string FaultyId = txtCode.Text;
        Session["FaultyId"] = FaultyId;

        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id05').style.display='block'", true);

    }
}