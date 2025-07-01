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

public partial class Project_RequisitionNOC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItem();
        }
        this.ddlRequUserFor.DataBind();

        if (!IsPostBack)
        {
            Session["Requisitionto"] = null;
            Session["RequisitionFor"]= null;
            Session["ProjectId"] = null;
            
            ddlRequToGrp.Text = "";

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
        string ProjectId = "";
        try
        {
            ProjectId = ddlProject.Value.ToString();
            Session["ProjectId"] = ProjectId;
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Project First');", true);
            return;
        }
        string Requisitionto = "";
        try
        {
            Requisitionto = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to First');", true);
            return;
        }

        string RequisitionFor = "";
        try
        {
            RequisitionFor = ddlRequisitionFor.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition For First');", true);
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
            BranchId = ddlRequisitionto.Value.ToString();
        }
        catch { }
        string ItemId = "";
        string RequisitiontoBranchId = "";
        try
        {
            RequisitiontoBranchId = ddlRequisitionFor.Value.ToString();
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
                string ProjectId = ddlProject.Value.ToString();
                DataTable dtI = (DataTable)ViewState["ItemDetails"];
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty, h.CGST, h.IGST, h.SGST, h.CESS, '' as RQty, '' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + BranchId + "' and ProjectId='" + ProjectId + "') as POPQty,(select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + RequisitiontoBranchId + "' and ProjectId='" + ProjectId + "') as ReqToQty from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode  where rm.ItemId='" + ItemId + "'");
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
        Response.Redirect("RequisitionNOC.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UserId = "";
        try
        {
            UserId = ddlRequUserFor.Text;
        }
        catch { }

        string BranchId = ddlRequisitionFor.Value.ToString();
        if (BranchId == "")
        {
             ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition For Locaation .');", true);
            return;
        }
        

        string UserGroupId = ddlRequToGrpFor.Value.ToString();
        if (UserGroupId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition Fro Group and User Name .');", true);
            return;
        }

        DateTime DOE = DateTime.Now;

        string ReqNOCId = DBAccess.FetchDatatable("select [dbo].[fn_ReqNOCId]()").Rows[0][0].ToString();
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
        string ReUserGroupId = ddlRequToGrp.Value.ToString();
        if (ReUserGroupId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition To Group and User Name .');", true);
            return;
        }
        string ReUserGroupName = ddlRequToGrp.SelectedItem.ToString();

        string ReUserId = ddlRequisitionto.Value.ToString();
        if (ReUserId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition To Location .');", true);
            return;
        }

        string ReUserName = ddlRequisitionto.Text;
        string Remarks = txtRemarks.Text;
        string Status = ddlRequToUser.Text;
        string Status1 = Session["UserId"].ToString();
        string Status2 = Session["BranchId"].ToString();
        string Status3 = ddlProject.Text;
        string Status33 = "";
        try
        {
            Status33 = ddlProject.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Project.');", true);
            return;
        }

        string Status4 = ddlRequisitionFor.Text;
        string Status5 = ddlRequToGrpFor.Text;
        string ReasonOfPriority = ddlReasonOfPriority.Text;
        string TransportationMode = ddlTransportation.Text;

        if (gvItemDetails.Rows.Count == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Item First');", true);
            return;
        }


        int n = ClassRequisitions.RequisitionsNOCSave(ReqNOCId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status33, Status4, Status5, UserGroupId, UserId, BranchId, DOE, ReasonOfPriority, TransportationMode);


        if (n == 1)
        {
            for (int i = 0; i < gvItemDetails.Rows.Count; i++)
            {
                string ReqNOCItemId = DBAccess.FetchDatatable("select [dbo].[fn_ReqNOCItemId]()").Rows[0][0].ToString();
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
                decimal Qty = 0;
                try
                {
                    Qty = Convert.ToDecimal(txtItemQty.Text);
                }
                catch
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter Qty');", true);
                    return;
                }

                decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                if (Qty > POPQty)
                {
                    bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionNOC]  where ReqNOCId='" + ReqNOCId + "' ");
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It Is More Than Stock In Hand (To) Qty..');", true);
                    return;
                }

                if (Qty == 0)
                {
                    bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionNOC]  where ReqNOCId='" + ReqNOCId + "'");
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisition Qty Must Be More Than 0..');", true);
                    return;
                }

                int m = ClassRequisitions.RequisitionsNOCSaveDetails(ReqNOCId, ReqNOCItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                if (m == 1)
                {

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionNOC.aspx';", true);
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
        string Requisitionto = "";
        try
        {
            Requisitionto = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to First');", true);
            return;
        }
        string RequToGrp = ddlRequToGrp.Value.ToString();

        DataTable dtt = DBAccess.FetchDatatable("select a.UserName,a.FullName from tbl_User a where a.BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "' union select a.UserName, a.FullName from tbl_User a join tbl_UserBranchMapping c on a.UserName=c.UserId where c.BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "'");
        if (dtt != null && dtt.Rows.Count > 0)
        {
            ddlRequToUser.DataSource = dtt;
            ddlRequToUser.Text = "UserName";
            ddlRequToUser.Value = "UserName";
            ddlRequToUser.DataBind();
            ddlRequToUser.SelectedIndex = 0;
            txtRequToUser.Text = dtt.Rows[0]["FullName"].ToString();
        }
    }


    protected void ddlRequisitionto_TextChanged(object sender, EventArgs e)
    {
        string Requisitionto = ddlRequisitionto.Value.ToString();
        Session["Requisitionto"] = Requisitionto.ToString();

        ddlRequToGrp.SelectedIndex = -1;
        ddlRequToUser.Value = null;
        ddlRequToUser.Items.Clear();        
    }
    protected void ddlRequisitionFor_TextChanged(object sender, EventArgs e)
    {
        string RequisitionFor = ddlRequisitionFor.Value.ToString();
        Session["RequisitionFor"] = RequisitionFor.ToString();

        ddlRequToGrpFor.SelectedIndex = -1;
        ddlRequUserFor.Value = null;
        ddlRequUserFor.Items.Clear();

    }
    protected void ddlRequToGrpFor_TextChanged(object sender, EventArgs e)
    {
        string RequisitionFor = "";
        try
        {
            RequisitionFor = ddlRequisitionFor.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition For First');", true);
            return;
        }
        string RequUserFor = ddlRequToGrpFor.Value.ToString();

        DataTable dttFor = DBAccess.FetchDatatable("select a.UserName, a.FullName from tbl_User a  where a.BranchId='" + RequisitionFor + "' and UserGroup='" + RequUserFor + "' union  select a.UserName, a.FullName from tbl_User a join tbl_UserBranchMapping c on a.UserName=c.UserId where c.BranchId='" + RequisitionFor + "' and UserGroup='" + RequUserFor + "'");
        if (dttFor != null && dttFor.Rows.Count > 0)
        {
            ddlRequUserFor.DataSource = dttFor;
            ddlRequUserFor.Text = "UserName";
            ddlRequUserFor.Value = "UserName";
            ddlRequUserFor.DataBind();
            ddlRequUserFor.SelectedIndex = 0;
            txtRequUserFor.Text = dttFor.Rows[0]["FullName"].ToString();

        }
    }
    protected void ddlRequisitionPur_TextChanged(object sender, EventArgs e)
    {
        string Reson = ddlRequisitionPur.Text;

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

    protected void btnCodeSerch_Click(object sender, EventArgs e)
    {
        string FaultyId = txtCode.Text;
        Session["FaultyId"] = FaultyId;

        DataTable dttFor = DBAccess.FetchDatatable("select FaultyId from tbl_FaultyEntry where FaultyId='" + FaultyId + "'");

        if (dttFor != null && dttFor.Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id05').style.display='block'", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Faulty ID Not Available');", true);
            return;
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
            BranchId = ddlRequisitionto.Value.ToString();
        }
        catch { }
        string ItemId = "";
        string RequisitiontoBranchId = "";
        try
        {
            RequisitiontoBranchId = ddlRequisitionFor.Value.ToString();
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
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty, h.CGST, h.IGST, h.SGST, h.CESS, '' as RQty, '' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as POPQty,(select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + RequisitiontoBranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as ReqToQty from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode  where rm.ItemId='" + ItemId + "'");
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
    
}