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
public partial class Project_RequisitionProjectAF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetailsApp"], null))
        {
            ViewState["ItemDetailsApp"] = getItemApp();
        }
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

            string ReqProId = Request.QueryString["ReqProId"];
            if (ReqProId != null)
            {
                FatchDataFromApproval(ReqProId, "Approval");
            }
            else
            {

            }

        }
    }

    protected void ChkFor_CheckedChanged(object sender, EventArgs e)
    {
        tblFor.Visible = true;

        if (ChkFor.Checked == true)
        {
            string ReqProId = Request.QueryString["ReqProId"];
            if (ReqProId != null)
            {
                FatchDataFromForword(ReqProId, "Approval");
            }
            else
            {

            }
        }
        else
        {
            tblFor.Visible = false;
        }
    }
    public void FatchDataFromApproval(string ReqProId, string Status)
    {
        DataTable dt = DBAccess.FetchDatatable(@"select *, convert(varchar,RequisitionDate,103) as RequisitionDate1 from tbl_RequisitionProject where ReqProId='" + ReqProId + "'");
        if (dt.Rows.Count > 0)
        {
            txtReqId1.Text = dt.Rows[0]["BranchId"].ToString();
            txtBranchName1.Text = dt.Rows[0]["Status4"].ToString();
            txtUserGroup1.Text = dt.Rows[0]["Status5"].ToString();
            lblUserGroupId1.Text = dt.Rows[0]["UserGroupId"].ToString();
            txtReqUser1.Text = dt.Rows[0]["UserId"].ToString();
            dtpDate1.Date = Convert.ToDateTime(dt.Rows[0]["RequisitionDate1"].ToString());
            ddlRequisitionPur1.Text = dt.Rows[0]["RequisitionPurpose"].ToString();
            ddlRequisitionPurId1.Text = dt.Rows[0]["PrioritiesId"].ToString();
            ddlRequisitionto1.Text = dt.Rows[0]["ReUserName"].ToString();
            ddlRequisitiontoId1.Text = dt.Rows[0]["ReUserId"].ToString();
            ddlRequToGrp1.Text = dt.Rows[0]["ReUserGroupName"].ToString();
            ddlRequToGrpId1.Text = dt.Rows[0]["ReUserGroupId"].ToString();
            ddlRequToUser1.Text = dt.Rows[0]["Status"].ToString();
            txtRemarks1.Text = dt.Rows[0]["Remarks"].ToString();

        }

        DataTable dt1 = (DataTable)ViewState["ItemDetailsApp"];
        DataTable Items = DBAccess.FetchDatatable(@"select a.*,b.* ,c.*,'' as rate from tbl_RequisitionProjectDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId left join tbl_HSNCodeMaster c on b.HSNCode=c.HSNCode  where ReqProId='" + ReqProId + "'");

        if (Items.Rows.Count > 0)
        {
            for (int i = 0; i < Items.Rows.Count; i++)
            {
                string ReqProItemId = Items.Rows[i]["ReqProItemId"].ToString();
                Session["ReqTetItemId"] = ReqProItemId.ToString();
                string ItemId = Items.Rows[i]["ItemId"].ToString();
                string Category = Items.Rows[i]["Category"].ToString();
                string Type = Items.Rows[i]["Type"].ToString();
                string ItemName = Items.Rows[i]["ItemName"].ToString();
                string Make = Items.Rows[i]["Make"].ToString();
                string Model = Items.Rows[i]["Model"].ToString();
                string Qty = Items.Rows[i]["Qty"].ToString();
                string rate = Items.Rows[i]["rate"].ToString();
                string HSNCode = Items.Rows[i]["HSNCode"].ToString();
                string CGST = Items.Rows[i]["CGST"].ToString();
                string IGST = Items.Rows[i]["IGST"].ToString();
                string SGST = Items.Rows[i]["SGST"].ToString();
                string ReqToQty = Items.Rows[i]["ReqToQty"].ToString();
                string POPQty = Items.Rows[i]["POPQty"].ToString();

                DataRow drH = dt1.NewRow();
                drH["ReqProItemId"] = Items.Rows[i]["ReqProItemId"].ToString();
                drH["ItemId"] = Items.Rows[i]["ItemId"].ToString();
                drH["Category"] = Items.Rows[i]["Category"].ToString();
                drH["Type"] = Items.Rows[i]["Type"].ToString();
                drH["Model"] = Items.Rows[i]["Model"].ToString();
                drH["Unit"] = Items.Rows[i]["Unit"].ToString();
                drH["Qty"] = Items.Rows[i]["Qty"].ToString();
                drH["ItemName"] = Items.Rows[i]["ItemName"].ToString();
                drH["Make"] = Items.Rows[i]["Make"].ToString();
                drH["rate"] = Items.Rows[i]["rate"].ToString();
                drH["HSNCode"] = Items.Rows[i]["HSNCode"].ToString();
                drH["CGST"] = Items.Rows[i]["CGST"].ToString();
                drH["IGST"] = Items.Rows[i]["IGST"].ToString();
                drH["SGST"] = Items.Rows[i]["SGST"].ToString();
                drH["ReqToQty"] = Items.Rows[i]["ReqToQty"].ToString();
                drH["POPQty"] = Items.Rows[i]["POPQty"].ToString();

                dt1.Rows.Add(drH);
            }
            ViewState["ItemDetailsApp"] = dt1;
            gvItemDetailsApp.DataSource = ViewState["ItemDetailsApp"] as DataTable;
            gvItemDetailsApp.DataBind();
            ViewState["ReqProId"] = ReqProId;
            tblApp.Visible = true;
            tblFor.Visible = false;
            btnChooseRawMaterial.Visible = false;

            #region ButtonTextChange

            if (Status == "Approval")
            {
                btnSave.Text = "Approve And Forword";
            }
            //else if (Status == "Revision")
            //{
            //    btnSave.Text = "SAVE";
            //}
            //else if (Status == "ShortClose")
            //{
            //    btnSave.Text = "ShortClose";
            //}

            #endregion ButtonTextChange

        }


    }
    public void FatchDataFromForword(string ReqProId, string Status)
    {

        DataTable dt1 = (DataTable)ViewState["ItemDetails"];
        DataTable Items = DBAccess.FetchDatatable(@"select a.*,b.* ,c.*,'' as rate from [tbl_RequisitionProject] a join tbl_ItemMaster b on a.ItemId=b.ItemId left join tbl_HSNCodeMaster c on b.HSNCode=c.HSNCode  where ReqProId='" + ReqProId + "'");

        if (Items.Rows.Count > 0)
        {
            for (int b = 0; b < gvItemDetailsApp.Rows.Count; b++)
            {

                TextBox txtItemQty = (TextBox)gvItemDetailsApp.Rows[b].FindControl("txtItemQty");
                decimal AppQty1 = Convert.ToDecimal(txtItemQty.Text);
                Session["AppQty"] = AppQty1;

                for (int i = 0; i < Items.Rows.Count; i++)
                {
                    string ReqProItemId = Items.Rows[i]["ReqProItemId"].ToString();
                    Session["ReqProItemId"] = ReqProItemId.ToString();
                    string ItemId = Items.Rows[i]["ItemId"].ToString();
                    string Category = Items.Rows[i]["Category"].ToString();
                    string Type = Items.Rows[i]["Type"].ToString();
                    string ItemName = Items.Rows[i]["ItemName"].ToString();
                    string Make = Items.Rows[i]["Make"].ToString();
                    string Model = Items.Rows[i]["Model"].ToString();
                    string Qty = Items.Rows[i]["Qty"].ToString();
                    string rate = Items.Rows[i]["rate"].ToString();
                    string HSNCode = Items.Rows[i]["HSNCode"].ToString();
                    string CGST = Items.Rows[i]["CGST"].ToString();
                    string IGST = Items.Rows[i]["IGST"].ToString();
                    string SGST = Items.Rows[i]["SGST"].ToString();
                    string ReqToQty = Items.Rows[i]["ReqToQty"].ToString();
                    string POPQty = Items.Rows[i]["POPQty"].ToString();
                    decimal Qty1 = Convert.ToDecimal(Qty);
                    decimal AppQty = Convert.ToDecimal(Session["AppQty"]);
                    decimal Total = Qty1 - AppQty;
                    string ReqQty1 = DBAccess.FetchDatasingle("select Qty from tbl_RequisitionProjectDetails where ReqProId='" + ReqProId + "' and ReqProItemId='" + ReqProItemId + "'");
                    decimal ReqQty = Convert.ToDecimal(ReqQty1);

                    if (Total == 0 | Total < 0)
                    {
                        tblFor.Visible = false;
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Its 0 Not able to Forword.');", true);
                        return;

                    }
                    if (Total > ReqQty)
                    {
                        tblFor.Visible = false;
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It's More Than Requisitions Qty.');", true);
                        return;

                    }

                    DataRow drH = dt1.NewRow();
                    drH["ReqProItemId"] = Items.Rows[i]["ReqProItemId"].ToString();
                    drH["ItemId"] = Items.Rows[i]["ItemId"].ToString();
                    drH["Category"] = Items.Rows[i]["Category"].ToString();
                    drH["Type"] = Items.Rows[i]["Type"].ToString();
                    drH["ItemName"] = Items.Rows[i]["ItemName"].ToString();
                    drH["Make"] = Items.Rows[i]["Make"].ToString();
                    drH["Model"] = Items.Rows[i]["Model"].ToString();
                    drH["Unit"] = Items.Rows[i]["Unit"].ToString();
                    drH["Qty"] = Items.Rows[i]["Qty"].ToString();
                    drH["rate"] = Items.Rows[i]["rate"].ToString();
                    drH["HSNCode"] = Items.Rows[i]["HSNCode"].ToString();
                    drH["CGST"] = Items.Rows[i]["CGST"].ToString();
                    drH["IGST"] = Items.Rows[i]["IGST"].ToString();
                    drH["SGST"] = Items.Rows[i]["SGST"].ToString();
                    drH["ReqToQty"] = Items.Rows[i]["ReqToQty"].ToString();
                    drH["POPQty"] = Items.Rows[i]["POPQty"].ToString();

                    dt1.Rows.Add(drH);
                }
                ViewState["ItemDetails"] = dt1;
                gvItemDetails.DataSource = ViewState["ItemDetails"] as DataTable;
                gvItemDetails.DataBind();
                ViewState["ReqProId"] = ReqProId;
                tblApp.Visible = true;

                btnChooseRawMaterial.Visible = false;

                #region ButtonTextChange

                if (Status == "Approval")
                {
                    btnSave.Text = "Approve And Forword";
                }
                //else if (Status == "Revision")
                //{
                //    btnSave.Text = "SAVE";
                //}
                //else if (Status == "ShortClose")
                //{
                //    btnSave.Text = "ShortClose";
                //}

                #endregion ButtonTextChange

            }
        }
    }
    protected DataTable getItem()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqProItemId";
        oTable.Columns.Add(dtCol);

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
    protected DataTable getItemApp()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqProItemId";
        oTable.Columns.Add(dtCol);


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
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
    }
    protected void btnAddServiceItem_Click(object sender, EventArgs e)
    {
        string ItemId = "";
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
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate, '' as POPQty, '' as ReqToQty from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode where rm.ItemId='" + ItemId + "'");
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
        Response.Redirect("RequisitionProjectAF.aspx");
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

        string RequToGrp = ddlRequToGrp.SelectedValue.ToString();

        DataTable dtt = DBAccess.FetchDatatable("select UserName from tbl_User where BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "'");
        if (dtt != null && dtt.Rows.Count > 0)
        {
            ddlRequToUser.DataSource = dtt;
            ddlRequToUser.Text = "UserName";
            ddlRequToUser.Value = "UserName";
            ddlRequToUser.DataBind();
            ddlRequToUser.SelectedIndex = 0;

        }
    }
    protected void gvItemDetailsApp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemDetailsApp.PageIndex = e.NewPageIndex;
        this.gvItemDetailsApp.DataSource = (DataTable)ViewState["ItemDetailsApp"];
        this.gvItemDetailsApp.DataBind();
    }
    protected void gvItemDetailsApp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt8 = new DataTable();
            dt8 = (DataTable)ViewState["ItemDetailsApp"];
            dt8.Rows[rowindex].Delete();
            dt8.AcceptChanges();
            ViewState["ItemDetailsApp"] = dt8;
            gvItemDetailsApp.DataSource = dt8;
            gvItemDetailsApp.DataBind();
        }
    }
    protected void gvItemDetailsApp_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
        string UserGroupId = "";
        try
        {
            UserGroupId = Session["UserGroupId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;

        if (btnSave.Text == "Save")
        {

            string ReqRegId = DBAccess.FetchDatatable("select [dbo].[fn_ReqRegId]()").Rows[0][0].ToString();
            string RequisitionDate = dtpDate.Text;
            string PrioritiesId = ddlRequisitionPur.Value.ToString();
            string RequisitionPurpose = ddlRequisitionPur.Text;
            string ReUserGroupId = ddlRequToGrp.SelectedValue.ToString();
            string ReUserGroupName = ddlRequToGrp.SelectedItem.ToString();
            string ReUserId = ddlRequisitionto.Value.ToString();
            string ReUserName = ddlRequisitionto.Text;
            string Remarks = txtRemarks.Text;
            string Status = ddlRequToUser.Text;
            string Status1 = "";
            string Status2 = "";
            string Status3 = "";
            string Status4 = txtBranchName.Text;
            string Status5 = txtUserGroup.Text;
            string Forword = "";
            string ReqPopId = "";
            string ReqSecId = "";
            string ReqTetId = "";

            int n = ClassRequisitions.RequisitionsGerSave(ReqRegId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, UserGroupId, UserId, BranchId, DOE, Forword, ReqPopId, ReqSecId, ReqTetId);

            if (n == 1)
            {
                for (int i = 0; i < gvItemDetails.Rows.Count; i++)
                {
                    string ReqRegItemId = DBAccess.FetchDatatable("select [dbo].[fn_ReqRegItemId]()").Rows[0][0].ToString();
                    string ItemId = gvItemDetails.Rows[i].Cells[1].Text;
                    string Category = gvItemDetails.Rows[i].Cells[2].Text;
                    string ItemName = gvItemDetails.Rows[i].Cells[3].Text;
                    string Make = gvItemDetails.Rows[i].Cells[4].Text;
                    string Model = gvItemDetails.Rows[i].Cells[5].Text;
                    string Unit = gvItemDetails.Rows[i].Cells[6].Text;
                    decimal ReqToQty = 0;
                    decimal POPQty = 0;
                    string Status6 = "";

                    try
                    {
                        ReqToQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[7].Text);
                        POPQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[8].Text);
                    }
                    catch
                    { }
                    TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                    TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                    decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                    decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                    int m = ClassRequisitions.RequisitionsRegSaveDetails(ReqRegId, ReqRegItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                    if (m == 1)
                    {

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionProjectAF..aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Sent Successfully. Try again.');", true);
                        return;
                    }
                }
            }
        }


        else if (btnSave.Text == "Approve And Forword")
        {
            
            string ReqProAppId = DBAccess.FetchDatatable("select [dbo].[ReqProAppId]()").Rows[0][0].ToString();
            string ReqProId = Request.QueryString["ReqProId"];
            //string ReqRegId = DBAccess.FetchDatatable("select [dbo].[fn_ReqRegId]()").Rows[0][0].ToString();

          
            string RequisitionDate1 = dtpDate1.Text;
            string PrioritiesId1 = ddlRequisitionPurId1.Text;
            string RequisitionPurpose1 = ddlRequisitionPur1.Text;
            string ReUserGroupId1 = lblUserGroupId1.Text;
            string ReUserGroupName1 = ddlRequToGrp1.Text;
            string Remarks1 = txtRemarks1.Text;
            string ReUserId1 = ddlRequisitiontoId1.Text;
            string ReUserName1 = ddlRequisitionto1.Text;
            string Status11 = ddlRequToUser1.Text;
            string Status12 = "";
            string Status13 = "";
            string Status14 = "";
            string Status15 = txtBranchName1.Text;
            string Status16 = txtUserGroup1.Text;
            string Forword = "";
            string FrorwordFrom = txtPopId.Text;
            if (FrorwordFrom == "")
            {
                Forword = "";
            }
            else
            {
                Forword = "Yes";
            }



            int na = ClassRequisitions.RequisitionsProjectSaveApp(ReqProAppId, ReqProId, RequisitionDate1, PrioritiesId1, RequisitionPurpose1, ReUserGroupId1, ReUserGroupName1, ReUserId1, ReUserName1, Remarks1, Status11, Status12, Status13, Status14, Status15, Status16, UserGroupId, UserId, BranchId, DOE);

            if (na == 1)
            {
               
                for (int i = 0; i < gvItemDetailsApp.Rows.Count; i++)
                {
                    string ReqProItemId = gvItemDetailsApp.Rows[i].Cells[1].Text;
                    string ItemId = gvItemDetailsApp.Rows[i].Cells[2].Text;
                    string Category = gvItemDetailsApp.Rows[i].Cells[3].Text;
                    string ItemName = gvItemDetailsApp.Rows[i].Cells[5].Text;
                    string Make = gvItemDetailsApp.Rows[i].Cells[6].Text;
                    string Model = gvItemDetailsApp.Rows[i].Cells[7].Text;
                    string Unit = gvItemDetailsApp.Rows[i].Cells[8].Text;
                    decimal ReqToQty = 0;
                    decimal POPQty = 0;
                    string Status6 = "";

                    try
                    {
                        ReqToQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[9].Text);
                        POPQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[10].Text);
                    }
                    catch
                    { }
                    TextBox txtItemQty = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtItemQty");
                    TextBox txtRate = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtRate");
                    decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                    decimal Qty1 = Convert.ToDecimal(txtRate.Text);



                    DataTable ReqQty1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionProjectDetailsApproval where ReqProId='" + ReqProId + "' and ReqProItemId='" + ReqProItemId + "'");
                      if (ReqQty1.Rows.Count > 0)
                      {
                          for (int a = 0; a < ReqQty1.Rows.Count; a++)
                          {
                              decimal ReqQty = 0;
                              try
                              {
                                  ReqQty = Convert.ToDecimal(ReqQty1.Rows[a]["Qty"]);
                                  if (Qty == ReqQty)
                                  {

                                  }
                                  else
                                  {
                                      bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionProjectApproval]  where ReqProAppId='" + ReqProAppId + "' ");
                                      bool DeleteDe = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionProjectDetailsApproval]  where ReqProAppId='" + ReqProAppId + "' ");
                                      ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It Is More Than Requisitions Qty.');", true);
                                      return;
                                  }
                              }
                              catch
                              {

                              }
                          }
                      }

                      int ma = ClassRequisitions.RequisitionsProjectSaveDetailsApp(ReqProAppId, ReqProId, ReqProItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);


                }

                if (na == 1)
                {

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionProjectAF.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Sent Successfully. Try again.');", true);
                    return;
                }
            }
        }

    }
}
