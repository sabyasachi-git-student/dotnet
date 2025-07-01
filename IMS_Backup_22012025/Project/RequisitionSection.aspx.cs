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


public partial class Project_RequisitionSection : System.Web.UI.Page
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
            string ReqPopId = Request.QueryString["ReqPopId"];
            if (ReqPopId != null)
            {
                FatchDataFromApproval(ReqPopId, "Approval");
            }
            else
            {

            }
        }
    }

    public void FatchDataFromApproval(string ReqPopId, string Status)
    {
        DataTable dt = DBAccess.FetchDatatable(@"select *, convert(varchar,RequisitionDate,103) as RequisitionDate1 from tbl_RequisitionPop where ReqPopId='" + ReqPopId + "'");
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
            ddlProject1.Text = dt.Rows[0]["Status1"].ToString();
            ddlProjectId1.Text = dt.Rows[0]["Status2"].ToString();



        }

        string BranchIdPop = txtReqId1.Text;
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        DataTable dt1 = (DataTable)ViewState["ItemDetailsApp"];
        DataTable Items = DBAccess.FetchDatatable(@"select a.*,b.* ,c.*,'' as rate,(select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdPop + "' and (Status1 is Null or status1='') and (Status2 is Null or status2='')) as POPQty1, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is Null or status1='') and (Status2 is Null or status2='')) as ReqToQty1 from tbl_RequisitionPopDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId left join tbl_HSNCodeMaster c on b.HSNCode=c.HSNCode  where ReqPopId='" + ReqPopId + "'");

        if (Items.Rows.Count > 0)
        {
            for (int i = 0; i < Items.Rows.Count; i++)
            {
                string ReqPopItemId = Items.Rows[i]["ReqPopItemId"].ToString();
                Session["ReqPopItemId"] = ReqPopItemId.ToString();
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
                string ReqToQty = Items.Rows[i]["ReqToQty1"].ToString();
                string POPQty = Items.Rows[i]["POPQty1"].ToString();

                DataRow drH = dt1.NewRow();
                drH["ReqPopItemId"] = Items.Rows[i]["ReqPopItemId"].ToString();
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
                drH["ReqToQty"] = Items.Rows[i]["ReqToQty1"].ToString();
                drH["POPQty"] = Items.Rows[i]["POPQty1"].ToString();

                dt1.Rows.Add(drH);
            }
            ViewState["ItemDetailsApp"] = dt1;
            gvItemDetailsApp.DataSource = ViewState["ItemDetailsApp"] as DataTable;
            gvItemDetailsApp.DataBind();
            ViewState["ReqPopId"] = ReqPopId;
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
    public void FatchDataFromForword(string ReqPopId, string Status)
    {
        DataTable dt = DBAccess.FetchDatatable(@"select *, convert(varchar,RequisitionDate,103) as RequisitionDate1 from tbl_RequisitionPop where ReqPopId='" + ReqPopId + "'");
        if (dt.Rows.Count > 0)
        {
            ddlRequisitionPur.Value = dt.Rows[0]["PrioritiesId"].ToString();
            ddlProject.Value = dt.Rows[0]["Status1"].ToString();
            ddlRequisitionPur.Enabled = false;
            ddlProject.Enabled = false;
        }

        string BranchIdPop = txtReqId1.Text;
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        //DataTable Items = DBAccess.FetchDatatable(@"select a.*,b.* ,c.*,'' as rate,(select sum(AvailableQty) from tbl_RackStockInDetails where ItemId=a.ItemId and BranchId='" + BranchIdPop + "') as POPQty1, (select sum(AvailableQty) from tbl_RackStockInDetails where ItemId=a.ItemId and BranchId='" + BranchId + "') as ReqToQty1 from tbl_RequisitionPopDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId   where ReqPopId='" + ReqPopId + "'");

        DataTable dt1 = (DataTable)ViewState["ItemDetails"];
        DataTable Items = DBAccess.FetchDatatable(@"select a.*,b.* ,c.*,'' as rate,(select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdPop + "' and (Status1 is Null or status1='') and (Status2 is Null or status2='')) as POPQty1, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is Null or status1='') and (Status2 is Null or status2='')) as ReqToQty1 from tbl_RequisitionPopDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId left join tbl_HSNCodeMaster c on b.HSNCode=c.HSNCode  where ReqPopId='" + ReqPopId + "'");

        if (Items.Rows.Count > 0)
        {
            for (int i = 0; i < Items.Rows.Count; i++)
            {
                for (int b = 0; b < gvItemDetailsApp.Rows.Count; b++, i++)
                {

                    TextBox txtItemQty = (TextBox)gvItemDetailsApp.Rows[b].FindControl("txtItemQty");
                    decimal AppQty1 = Convert.ToDecimal(txtItemQty.Text);
                    CheckBox chk = (CheckBox)gvItemDetailsApp.Rows[b].FindControl("chk");
                    Session["AppQty"] = AppQty1;
                    if (chk.Checked == true)
                    {

                        string ReqPopItemId = Items.Rows[i]["ReqPopItemId"].ToString();
                        Session["ReqPopItemId"] = ReqPopItemId.ToString();
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
                        string ReqToQty = Items.Rows[i]["ReqToQty1"].ToString();
                        string POPQty = Items.Rows[i]["POPQty1"].ToString();
                        decimal Qty1 = Convert.ToDecimal(Qty);
                        decimal AppQty = Convert.ToDecimal(Session["AppQty"]);
                        decimal Total = Qty1 - AppQty;
                        string ReqQty1 = DBAccess.FetchDatasingle("select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId + "' and ReqPopItemId='" + ReqPopItemId + "'");
                        decimal ReqQty = Convert.ToDecimal(ReqQty1);


                        if (Total == 0)
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
                        drH["ReqPopItemId"] = Items.Rows[i]["ReqPopItemId"].ToString();
                        drH["ItemId"] = Items.Rows[i]["ItemId"].ToString();
                        drH["Category"] = Items.Rows[i]["Category"].ToString();
                        drH["Type"] = Items.Rows[i]["Type"].ToString();
                        drH["ItemName"] = Items.Rows[i]["ItemName"].ToString();
                        drH["Make"] = Items.Rows[i]["Make"].ToString();
                        drH["Model"] = Items.Rows[i]["Model"].ToString();
                        drH["Unit"] = Items.Rows[i]["Unit"].ToString();
                        drH["Qty"] = Total;
                        drH["rate"] = Items.Rows[i]["rate"].ToString();
                        drH["HSNCode"] = Items.Rows[i]["HSNCode"].ToString();
                        drH["CGST"] = Items.Rows[i]["CGST"].ToString();
                        drH["IGST"] = Items.Rows[i]["IGST"].ToString();
                        drH["SGST"] = Items.Rows[i]["SGST"].ToString();
                        drH["ReqToQty"] = Items.Rows[i]["ReqToQty1"].ToString();
                        drH["POPQty"] = Items.Rows[i]["POPQty1"].ToString();

                        dt1.Rows.Add(drH);
                        ViewState["ItemDetails"] = dt1;
                        gvItemDetails.DataSource = ViewState["ItemDetails"] as DataTable;
                        gvItemDetails.DataBind();
                        ViewState["ReqPopId"] = ReqPopId;
                        tblApp.Visible = true;

                        btnChooseRawMaterial.Visible = false;


                    }
                }
            }


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
    protected DataTable getItem()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqPopItemId";
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
        dtCol.ColumnName = "ReqPopItemId";
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
    protected void ChkFor_CheckedChanged(object sender, EventArgs e)
    {
        tblFor.Visible = true;

        if (ChkFor.Checked == true)
        {
            string ReqPopId = Request.QueryString["ReqPopId"];
            if (ReqPopId != null)
            {
                FatchDataFromForword(ReqPopId, "Approval");
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
    protected void btnAddServiceItem_Click(object sender, EventArgs e)
    {
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch
        {

        }
        string RequisitiontoBranchId = "";
        try
        {
            RequisitiontoBranchId = ddlRequisitionto.Value.ToString();
        }
        catch
        {

        }
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
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + BranchId + "' and (Status1 is Null or status1='') and (Status2 is Null or status2='')) as POPQty, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + RequisitiontoBranchId + "'  and (Status1 is Null or status1='') and (Status2 is Null or status2='')) as ReqToQty  from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode where rm.ItemId='" + ItemId + "'");
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
        Response.Redirect("RequisitionSection.aspx");
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
            string ReqSecId = DBAccess.FetchDatatable("select [dbo].[fn_ReqSecId]()").Rows[0][0].ToString();
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
            string ReUserId = ddlRequisitionto.Value.ToString();
            string ReUserName = ddlRequisitionto.Text;
            string Remarks = txtRemarks.Text;
            string Status = ddlRequToUser.Text;
            string Status1 = ddlProject.Text;
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
            string Forword = "";
            string FrorwordFrom = "";


            int n = ClassRequisitions.RequisitionsSecSave(ReqSecId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, UserGroupId, UserId, BranchId, DOE, Forword, FrorwordFrom);


            if (n == 1)
            {
                for (int i = 0; i < gvItemDetails.Rows.Count; i++)
                {


                    string ReqSecItemId = DBAccess.FetchDatatable("select [dbo].[fn_ReqSecItemId]()").Rows[0][0].ToString();
                    string ItemId = gvItemDetails.Rows[i].Cells[2].Text;
                    string Category = gvItemDetails.Rows[i].Cells[3].Text;
                    string ItemName = gvItemDetails.Rows[i].Cells[5].Text;
                    string Make = gvItemDetails.Rows[i].Cells[6].Text;
                    string Model = gvItemDetails.Rows[i].Cells[7].Text;
                    string Unit = gvItemDetails.Rows[i].Cells[8].Text;
                    decimal ReqToQty = 0;
                    try
                    {
                        ReqToQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[9].Text);

                    }
                    catch
                    { }

                    decimal POPQty = 0;
                    try
                    {

                        POPQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[10].Text);
                    }
                    catch
                    { }
                    string Status6 = "";
                    TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                    TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                    decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                    decimal Qty1 = 0;
                    try
                    {
                        Qty1 = Convert.ToDecimal(txtRate.Text);
                    }
                    catch
                    {

                    }

                    //string ReqQty1 = DBAccess.FetchDatasingle("select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId + "' and ReqPopItemId='" + ReqPopItemId + "'");
                    //decimal ReqQty = Convert.ToDecimal(ReqQty1);

                    //if (Qty > ReqQty)
                    //{
                    //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It's More Than Requisitions Qty.');", true);
                    //    return;
                    //}

                    int m = ClassRequisitions.RequisitionsSecSaveDetails(ReqSecId, ReqSecItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                    if (m == 1)
                    {

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionSection.aspx';", true);
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
            #region SecSave&POPSaveApp
            if (ChkFor.Checked == true)
            {
                string ReqPopAppId = DBAccess.FetchDatatable("select [dbo].[fn_ReqPopAppId]()").Rows[0][0].ToString();
                string ReqPopId = Request.QueryString["ReqPopId"];
                string ReqSecId = DBAccess.FetchDatatable("select [dbo].[fn_ReqSecId]()").Rows[0][0].ToString();
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
                string ReUserId = ddlRequisitionto.Value.ToString();
                string ReUserName = ddlRequisitionto.Text;
                string Remarks = txtRemarks.Text;
                string Status = ddlRequToUser.Text;
                string Status1 = ddlProject.Text;
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
                string Forword = "Yes";
                string FrorwordFrom = Request.QueryString["ReqPopId"];

                string RequisitionDate1 = dtpDate1.Text;
                string PrioritiesId1 = ddlRequisitionPurId1.Text;
                string RequisitionPurpose1 = ddlRequisitionPur1.Text;
                string ReUserGroupId1 = lblUserGroupId1.Text;
                string ReUserGroupName1 = ddlRequToGrp1.Text;
                string Remarks1 = txtRemarks1.Text;
                string ReUserId1 = ddlRequisitiontoId1.Text;
                string ReUserName1 = ddlRequisitionto1.Text;
                string Status11 = ddlRequToUser1.Text;
                string Status12 = ddlProject1.Text;
                string Status13 = ddlProjectId1.Text;
                string Status14 = txtReqUser1.Text;
                string Status15 = txtBranchName1.Text;
                string Status16 = txtUserGroup1.Text;

                int n = ClassRequisitions.RequisitionsSecSave(ReqSecId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, UserGroupId, UserId, BranchId, DOE, Forword, FrorwordFrom);
                int na = ClassRequisitions.RequisitionsPOPSaveApp(ReqPopAppId, ReqPopId, RequisitionDate1, PrioritiesId1, RequisitionPurpose1, ReUserGroupId1, ReUserGroupName1, ReUserId1, ReUserName1, Remarks1, Status11, Status12, Status13, Status14, Status15, Status16, UserGroupId, UserId, BranchId, DOE);

            #endregion
                #region SecSaveDetails
                if (n == 1 & na == 1)
                {
                    for (int i = 0; i < gvItemDetails.Rows.Count; i++)
                    {
                        string ReqSecItemId = DBAccess.FetchDatatable("select [dbo].[fn_ReqSecItemId]()").Rows[0][0].ToString();
                        string ReqPopItemId = gvItemDetailsApp.Rows[i].Cells[1].Text;
                        string ItemId = gvItemDetailsApp.Rows[i].Cells[2].Text;
                        string Category = gvItemDetailsApp.Rows[i].Cells[3].Text;
                        string ItemName = gvItemDetailsApp.Rows[i].Cells[5].Text;
                        string Make = gvItemDetailsApp.Rows[i].Cells[6].Text;
                        string Model = gvItemDetailsApp.Rows[i].Cells[7].Text;
                        string Unit = gvItemDetailsApp.Rows[i].Cells[8].Text;
                        decimal ReqToQty = 0;
                        try
                        {
                            ReqToQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[9].Text);

                        }
                        catch
                        { }

                        decimal POPQty = 0;
                        try
                        {

                            POPQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[10].Text);
                        }
                        catch
                        { }
                        string Status6 = "";
                        TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                        TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                        decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                        Session["SecQty"] = Qty;
                        decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                        DataTable ReqQty1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId + "' and ReqPopItemId='" + ReqPopItemId + "'");

                        if (ReqQty1.Rows.Count > 0)
                        {
                            for (int a = 0; a < ReqQty1.Rows.Count; a++)
                            {
                                decimal ReqQty = 0;

                                try
                                {
                                    ReqQty = Convert.ToDecimal(ReqQty1.Rows[a]["Qty"]);


                                    if (Qty > ReqQty | ReqToQty < Qty)
                                    {
                                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionSection]  where ReqSecId='" + ReqSecId + "' ");
                                        bool DeleteDe = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionSectionDetails]  where ReqSecId='" + ReqSecId + "' ");
                                        bool DeleteApp = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "'");

                                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It Is More Than Requisitions Qty.');", true);
                                        return;
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }


                        int m = ClassRequisitions.RequisitionsSecSaveDetails(ReqSecId, ReqSecItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                    }
                }
                #endregion
                #region PopApprovalDetails
                if (n == 1)
                {
                    for (int i = 0; i < gvItemDetailsApp.Rows.Count; i++)
                    {
                        string ReqPopItemId = gvItemDetailsApp.Rows[i].Cells[1].Text;
                        string ItemId = gvItemDetailsApp.Rows[i].Cells[2].Text;
                        string Category = gvItemDetailsApp.Rows[i].Cells[3].Text;
                        string ItemName = gvItemDetailsApp.Rows[i].Cells[5].Text;
                        string Make = gvItemDetailsApp.Rows[i].Cells[6].Text;
                        string Model = gvItemDetailsApp.Rows[i].Cells[7].Text;
                        string Unit = gvItemDetailsApp.Rows[i].Cells[8].Text;
                        decimal ReqToQty = 0;
                        try
                        {
                            ReqToQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[9].Text);

                        }
                        catch
                        { }

                        decimal POPQty = 0;
                        try
                        {

                            POPQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[10].Text);
                        }
                        catch
                        { }
                        string Status6 = "";


                        TextBox txtItemQty = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtItemQty");
                        TextBox txtRate = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtRate");
                        decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                        decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                        DataTable ReqQty1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId + "' and ReqPopItemId='" + ReqPopItemId + "'");

                        if (ReqQty1.Rows.Count > 0)
                        {
                            for (int a = 0; a < ReqQty1.Rows.Count; a++)
                            {
                                decimal ReqQty = 0;
                                decimal SecQty = 0;
                                decimal total = 0;
                                try
                                {
                                    ReqQty = Convert.ToDecimal(ReqQty1.Rows[a]["Qty"]);
                                    SecQty = Convert.ToDecimal(Session["SecQty"]);
                                    total = SecQty + Qty;

                                    if (Qty > ReqQty | ReqToQty < Qty)
                                    {
                                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                        return;
                                    }
                                    if (total > ReqQty)
                                    {
                                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                        bool Delete1 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionSection]  where ReqSecId='" + ReqSecId + "' ");
                                        bool Delete2 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionSectionDetails]  where ReqSecId='" + ReqSecId + "' ");
                                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                        return;
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }

                        int ma = ClassRequisitions.RequisitionsPOPSaveDetailsApp(ReqPopAppId, ReqPopId, ReqPopItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                    }

                    if (n == 1 | na == 1)
                    {
                        string Status9 = "Approve";
                        bool UpdateReqPop = DBAccess.SaveData("update tbl_RequisitionPop set Status6 = '" + Status9 + "',  Status7 = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) where ReqPopId='" + ReqPopId + "'");
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionSection.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Sent Successfully. Try again.');", true);
                        return;
                    }
                }

                #endregion
            }
            else
            {
                #region POPSaveApp

                string ReqPopAppId = DBAccess.FetchDatatable("select [dbo].[fn_ReqPopAppId]()").Rows[0][0].ToString();
                string ReqPopId = Request.QueryString["ReqPopId"];
                string ReqSecId = DBAccess.FetchDatatable("select [dbo].[fn_ReqSecId]()").Rows[0][0].ToString();

                string RequisitionDate1 = dtpDate1.Text;
                string PrioritiesId1 = ddlRequisitionPurId1.Text;
                string RequisitionPurpose1 = ddlRequisitionPur1.Text;
                string ReUserGroupId1 = lblUserGroupId1.Text;
                string ReUserGroupName1 = ddlRequToGrp1.Text;
                string Remarks1 = txtRemarks1.Text;
                string ReUserId1 = ddlRequisitiontoId1.Text;
                string ReUserName1 = ddlRequisitionto1.Text;
                string Status11 = ddlRequToUser1.Text;
                string Status12 = ddlProject1.Text;
                string Status13 = ddlProjectId1.Text;
                string Status14 = txtReqUser1.Text;
                string Status15 = txtBranchName1.Text;
                string Status16 = txtUserGroup1.Text;

                int na = ClassRequisitions.RequisitionsPOPSaveApp(ReqPopAppId, ReqPopId, RequisitionDate1, PrioritiesId1, RequisitionPurpose1, ReUserGroupId1, ReUserGroupName1, ReUserId1, ReUserName1, Remarks1, Status11, Status12, Status13, Status14, Status15, Status16, UserGroupId, UserId, BranchId, DOE);
                #endregion
                #region POPSaveDetailsApp
                if (na == 1)
                {

                    for (int i = 0; i < gvItemDetailsApp.Rows.Count; i++)
                    {
                        string ReqPopItemId = gvItemDetailsApp.Rows[i].Cells[1].Text;
                        string ItemId = gvItemDetailsApp.Rows[i].Cells[2].Text;
                        string Category = gvItemDetailsApp.Rows[i].Cells[3].Text;
                        string ItemName = gvItemDetailsApp.Rows[i].Cells[5].Text;
                        string Make = gvItemDetailsApp.Rows[i].Cells[6].Text;
                        string Model = gvItemDetailsApp.Rows[i].Cells[7].Text;
                        string Unit = gvItemDetailsApp.Rows[i].Cells[8].Text;
                        decimal ReqToQty = 0;
                        try
                        {
                            ReqToQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[9].Text);

                        }
                        catch
                        { }

                        decimal POPQty = 0;
                        try
                        {

                            POPQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[10].Text);
                        }
                        catch
                        { }
                        string Status6 = "";


                        TextBox txtItemQty = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtItemQty");
                        TextBox txtRate = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtRate");
                        decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                        decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                        DataTable ReqQty1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId + "' and ReqPopItemId='" + ReqPopItemId + "'");
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
                                    if (ReqToQty < Qty)
                                    {
                                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                        bool DeleteDe = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopDetailsApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");

                                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                        return;
                                    }
                                    else
                                    {
                                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                        bool DeleteDe = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopDetailsApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");

                                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                        return;
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }

                        int ma = ClassRequisitions.RequisitionsPOPSaveDetailsApp(ReqPopAppId, ReqPopId, ReqPopItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                    }

                    if (na == 1)
                    {
                        string Status9 = "Approve";
                        bool UpdateReqPop = DBAccess.SaveData("update tbl_RequisitionPop set Status6 = '" + Status9 + "',  Status7 = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) where ReqPopId='" + ReqPopId + "'");
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionSection.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Sent Successfully. Try again.');", true);
                        return;
                    }
                }
                #endregion
            }
        }
    }
    protected void ddlRequToGrp_TextChanged(object sender, EventArgs e)
    {
        string PopName = txtBranchName.Text;
        string RequToGrp = ddlRequToGrp.SelectedValue.ToString();
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        DataTable dt1 = DBAccess.FetchDatatable("select Distinct a.TerrytoryId as Id, b.BranchId from tbl_HigherKeyMapping a join tbl_Branch b on a.TerrytoryId=b.BranchName where a.SectionId='" + PopName + "' ");
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            ddlRequisitionto.DataSource = dt1;
            ddlRequisitionto.Text = "Id";
            ddlRequisitionto.Value = "BranchId";
            ddlRequisitionto.DataBind();
            ddlRequisitionto.SelectedIndex = 0;
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
    protected void gvItemDetailsApp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvItemDetailsApp.Rows.Count; i++)
        {

            CheckBox chk = gvItemDetailsApp.Rows[i].FindControl("chk") as CheckBox;
            TextBox txtItemQty = gvItemDetailsApp.Rows[i].FindControl("txtItemQty") as TextBox;


            if (chk.Checked == true)
            {
                txtItemQty.Enabled = true;
                row_wantstoF.Visible = true;
                chk.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {

            }
        }
    }

    protected void btn_WantsToForword_Click(object sender, EventArgs e)
    {
        tblFor.Visible = true;
        ChkFor.Checked = true;
        if (ChkFor.Checked == true)
        {
            string ReqPopId = Request.QueryString["ReqPopId"];
            if (ReqPopId != null)
            {
                FatchDataFromForword(ReqPopId, "Approval");
            }
            else
            {

            }
        }
        else
        {
            tblFor.Visible = false;
        }
        btn_WantsToForword.Visible = false;
        btnSave.Enabled = true;
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

    protected void btnCodeSerch_Click(object sender, EventArgs e)
    {
        string FaultyId = txtCode.Text;
        Session["FaultyId"] = FaultyId;

        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id05').style.display='block'", true);

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
}