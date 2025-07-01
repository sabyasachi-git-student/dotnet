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


public partial class Project_TemporaryStockRequisition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItem();
        }
        if (ReferenceEquals(ViewState["ItemDetailsApp"], null))
        {
            ViewState["ItemDetailsApp"] = getItemApp();
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
            string ReqtransId = Request.QueryString["ReqtransId"];
            if (ReqtransId != null)
            {
                FatchDataFromApproval(ReqtransId, "Approval");
            }
            else
            {

            }
        }
    }

    public void FatchDataFromApproval(string ReqtransId, string Status)
    {
        string BranchId = Session["BranchId"].ToString();
        string BranchIdPop = Request.QueryString["ReqtransId"];
        DataTable dt = DBAccess.FetchDatatable(@"select a.*, convert(varchar,a.RequisitionDate,103) as RequisitionDate1, p.ProjectName from tbl_RequisitionTransfer a join tbl_ProjectMaster p on a.Status2=p.ProjectId where ReqtransId='" + ReqtransId + "'");
        if (dt.Rows.Count > 0)
        {
            txtReqId.Text = dt.Rows[0]["BranchId"].ToString();
            txtBranchName.Text = dt.Rows[0]["Status4"].ToString();
            txtUserGroup.Text = dt.Rows[0]["Status5"].ToString();
            // lblUserGroupId.Text = dt.Rows[0]["UserGroupId"].ToString();
            txtReqUser.Text = dt.Rows[0]["UserId"].ToString();
            dtpDate.Date = Convert.ToDateTime(dt.Rows[0]["RequisitionDate1"].ToString());
            ddlRequisitionPur.Text = dt.Rows[0]["RequisitionPurpose"].ToString();
            // ddlRequisitionPurId.Text = dt.Rows[0]["PrioritiesId"].ToString();
            ddlRequisitionto.Text = dt.Rows[0]["ReUserName"].ToString();
            // ddlRequisitiontoId.Text = dt.Rows[0]["ReUserId"].ToString();
            //ddlRequToGrp.Text = dt.Rows[0]["ReUserGroupName"].ToString();
            ddlRequToGrp.Text = dt.Rows[0]["ReUserGroupId"].ToString();
            ddlRequToUser.Text = dt.Rows[0]["Status"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            txtRemarks.Enabled = false;
            ddlProject.Text = dt.Rows[0]["ProjectName"].ToString();
            txtDay.Text = dt.Rows[0]["Days"].ToString();
            //txtForword.Text = dt.Rows[0]["Forword"].ToString();
            //txtPopId.Text = dt.Rows[0]["ReqPopId"].ToString();
            //txtSecId.Text = dt.Rows[0]["ReqSecId"].ToString();

            dtpDate.Enabled = false;
            ddlRequisitionPur.Enabled = false;
            txtReqUser.Enabled = false;
            ddlRequisitionto.Enabled = false;
            ddlRequToGrp.Enabled = false;
            ddlRequToUser.Enabled = false;
            txtDay.Enabled = false;
            ddlProject.Enabled = false;

        }
        DataTable dt1 = (DataTable)ViewState["ItemDetailsApp"];
        DataTable Items = DBAccess.FetchDatatable(@"select a.*,b.* ,c.*,'' as rate, (select sum(Qty) from tbl_RackStockInBarCodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdPop + "' and (Status1='' or Status1 is Null) and (Status1='' or Status1 is Null)) as POPQty1,(select sum(Qty) from tbl_RackStockInBarCodeDetails where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1='' or Status1 is Null) and (Status1='' or Status1 is Null)) as ReqToQty1 from tbl_RequisitionTransferDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId left join tbl_HSNCodeMaster c on b.HSNCode=c.HSNCode  where ReqtransId='" + ReqtransId + "'");

        if (Items.Rows.Count > 0)
        {
            for (int i = 0; i < Items.Rows.Count; i++)
            {
                string ReqtransItemId = Items.Rows[i]["ReqtransItemId"].ToString();
                Session["ReqtransItemId"] = ReqtransItemId.ToString();
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
                drH["ReqtransItemId"] = Items.Rows[i]["ReqtransItemId"].ToString();
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
            gvItemDetails.DataSource = ViewState["ItemDetailsApp"] as DataTable;
            gvItemDetails.DataBind();
            ViewState["ReqtransId"] = ReqtransId;
            //tblApp.Visible = true;
            //tblFor.Visible = false;
            btnChooseRawMaterial.Visible = false;

            #region ButtonTextChange

            if (Status == "Approval")
            {
                btnSave.Text = "Approve And Issuse";
            }
            //else if (Status == "Revision")
            //{
            //    btnSave.Text = "SAVE";
            //}

            #endregion ButtonTextChange

        }


    }
    protected DataTable getItemApp()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqtransItemId";
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

    protected DataTable getItem()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqtransItemId";
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
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty, h.CGST, h.IGST, h.SGST, h.CESS, '' as RQty, '' as rate, (select sum(Qty) from tbl_RackStockInBarCodeDetails where ItemId=rm.ItemId and BranchId='" + BranchId + "' and (Status1='' or Status1 is Null) and (Status1='' or Status1 is Null)) as POPQty,(select sum(Qty) from tbl_RackStockInBarCodeDetails where ItemId=rm.ItemId and BranchId='" + RequisitiontoBranchId + "' and (Status1='' or Status1 is Null) and (Status1='' or Status1 is Null)) as ReqToQty from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode  where rm.ItemId='" + ItemId + "'");
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
        Response.Redirect("TemporaryStockRequisition.aspx");
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
        string Status3 = txtReqUser.Text;
        string Status4 = txtBranchName.Text;
        string Status5 = txtUserGroup.Text;
        string Days = txtDay.Text;

        if (gvItemDetails.Rows.Count == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Item First');", true);
            return;
        }

        if (btnSave.Text == "Save")
        {
            string ReqtransId = DBAccess.FetchDatatable("select [dbo].[fn_ReqtransId]()").Rows[0][0].ToString();

            int n = ClassRequisitions.RequisitionTransfer(ReqtransId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, Days, UserGroupId, UserId, BranchId, DOE);


            if (n == 1)
            {
                for (int i = 0; i < gvItemDetails.Rows.Count; i++)
                {
                    string ReqtransItemId = DBAccess.FetchDatatable("select [dbo].[fn_ReqtransItemId]()").Rows[0][0].ToString();
                    string ItemId = gvItemDetails.Rows[i].Cells[2].Text;
                    string Category = gvItemDetails.Rows[i].Cells[3].Text;
                    string ItemName = gvItemDetails.Rows[i].Cells[5].Text;
                    string Make = gvItemDetails.Rows[i].Cells[6].Text;
                    string Model = gvItemDetails.Rows[i].Cells[7].Text;
                    string Unit = gvItemDetails.Rows[i].Cells[8].Text;
                    string Status6 = "";
                    decimal ReqToQty = 0;
                    try
                    {
                        ReqToQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[9].Text);

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

                    if (ReqToQty < Qty)
                    {
                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTransfer]  where ReqtransId='" + ReqtransId + "' ");
                        bool DeleteDe = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTransferDetails]  where ReqtransId='" + ReqtransId + "' ");
                       
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check the ReqToQty.');", true);
                        return;
                    }

                    decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                    int m = ClassRequisitions.RequisitionTransferDetails(ReqtransId, ReqtransItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                    if (m == 1)
                    {

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/TemporaryStockRequisition.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Sent Successfully. Try again.');", true);
                        return;
                    }
                }
            }
        }
        else
        {

            string ReqtransAppId = DBAccess.FetchDatatable("select [dbo].[fn_ReqtransAppId]()").Rows[0][0].ToString();
            string ReqtransId = Request.QueryString["ReqtransId"];


            int na = ClassRequisitions.RequisitionTransferApproval(ReqtransAppId, ReqtransId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, Days, UserGroupId, UserId, BranchId, DOE);

            if (na == 1)
            {
                for (int i = 0; i < gvItemDetails.Rows.Count; i++)
                {
                    string ReqtransItemId = gvItemDetails.Rows[i].Cells[1].Text;
                    string ItemId = gvItemDetails.Rows[i].Cells[2].Text;
                    string Category = gvItemDetails.Rows[i].Cells[3].Text;
                    string ItemName = gvItemDetails.Rows[i].Cells[5].Text;
                    string Make = gvItemDetails.Rows[i].Cells[6].Text;
                    string Model = gvItemDetails.Rows[i].Cells[7].Text;
                    string Unit = gvItemDetails.Rows[i].Cells[8].Text;
                    string Status6 = "";
                    decimal ReqToQty = 0;
                    try
                    {
                        ReqToQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[9].Text);

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
                    TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                    TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                    decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                    decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                    //string ReqQty1 = DBAccess.FetchDatasingle("");
                    //decimal ReqQty = Convert.ToDecimal(ReqQty1);

                    //DataTable ReqQty1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionTransferDetails where ReqtransId='" + ReqtransId + "' and ReqtransItemId='" + ReqtransItemId + "'");

                    if (Qty > ReqToQty)
                    {
                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTransferApproval]  where ReqtransId='" + ReqtransId + "' ");
                        bool DeleteDe = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTransferDetailsApproval]  where ReqtransId='" + ReqtransId + "' ");

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It Is More Than Requisitions Qty.');", true);
                        return;
                    }
                    else
                    {
                        
                    }


                    int ma = ClassRequisitions.RequisitionTransferDetailsApproval(ReqtransAppId, ReqtransId, ReqtransItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                    if (na == 1 && ma ==1)
                    {
                        string Status9 = "Approve";
                        bool UpdateReqPop = DBAccess.SaveData("update tbl_RequisitionTransfer set Status6 = '" + Status9 + "',  Status7 = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) where ReqtransId='" + ReqtransId + "'");
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisition Approved  Successfully.');window.location='../Project/RequisitionTransferApp.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Approved Successfully. Try again.');", true);
                        return;
                    }
                }
            }

        }
    }
    protected void ddlRequToGrp_TextChanged(object sender, EventArgs e)
    {
        string PopName = txtBranchName.Text;
        string RequToGrp = ddlRequToGrp.SelectedValue.ToString();

        if (RequToGrp == "UG19")
        {
            DataTable dt1 = DBAccess.FetchDatatable("select '--Select--'  as Id, '0' as BranchId union all select distinct a.RegionName, b.BranchId from tbl_RegionMaster a join tbl_Branch b on b.BranchName=a.RegionName");
            if (dt1 != null && dt1.Rows.Count > 0)
            {

                ddlRequisitionto.Text = "Id";
                ddlRequisitionto.Value = "BranchId";
                ddlRequisitionto.DataSource = dt1;
                ddlRequisitionto.DataBind();
            }
        }

        else if (RequToGrp == "UG15")
        {
            DataTable dt1 = DBAccess.FetchDatatable("select '--Select--'  as Id, '0' as BranchId union all select distinct a.SectionName, b.BranchId from tbl_SectionMaster a join tbl_Branch b on b.BranchName=a.SectionName");
            if (dt1 != null && dt1.Rows.Count > 0)
            {

                ddlRequisitionto.Text = "Id";
                ddlRequisitionto.Value = "BranchId";
                ddlRequisitionto.DataSource = dt1;
                ddlRequisitionto.DataBind();
            }
        }
        else if (RequToGrp == "UG14")
        {
            DataTable dt1 = DBAccess.FetchDatatable("select '--Select--'  as Id, '0' as BranchId union all select distinct a.TerrytoryName, b.BranchId from tbl_TerrytoryMaster a join tbl_Branch b on b.BranchName=a.TerrytoryName");
            if (dt1 != null && dt1.Rows.Count > 0)
            {

                ddlRequisitionto.Text = "Id";
                ddlRequisitionto.Value = "BranchId";
                ddlRequisitionto.DataSource = dt1;
                ddlRequisitionto.DataBind();
            }
        }
        else if (RequToGrp == "UG16")
        {
            DataTable dt1 = DBAccess.FetchDatatable("select '--Select--'  as Id, '0' as BranchId union all select distinct a.POPName, b.BranchId from tbl_POPMaster a join tbl_Branch b on b.BranchName=a.POPName");
            if (dt1 != null && dt1.Rows.Count > 0)
            {

                ddlRequisitionto.Text = "Id";
                ddlRequisitionto.Value = "BranchId";
                ddlRequisitionto.DataSource = dt1;
                ddlRequisitionto.DataBind();
            }
        }


    }
    protected void ddlRequisitionto_TextChanged(object sender, EventArgs e)
    {
        string PopName = txtBranchName.Text;
        string RequToGrp = ddlRequToGrp.SelectedValue.ToString();
        string Requisitionto = ddlRequisitionto.Value.ToString();


        DataTable dtt = DBAccess.FetchDatatable("select UserName from tbl_User where BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "'");
        if (dtt != null && dtt.Rows.Count > 0)
        {
            ddlRequToUser.DataSource = dtt;
            ddlRequToUser.Text = "UserName";
            ddlRequToUser.Value = "UserName";
            ddlRequToUser.DataBind();
        }
        else
        {
            ddlRequToUser.DataSource = "";
            ddlRequToUser.Text = "";
            ddlRequToUser.Value = "";
            ddlRequToUser.DataBind();
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Not Have UserId. Try again.');", true);
            return;

        }
    }
}
