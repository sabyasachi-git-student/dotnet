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
public partial class Project_MaterialIssuePop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItem();
        }
        if (ReferenceEquals(ViewState["TempBarcode"], null))
        {
            ViewState["TempBarcode"] = getBarcode();
        }

        if (!IsPostBack)
        {
            ProjectTr.Visible = false;
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaterialIssuePop.aspx");
    }
    protected DataTable getBarcode()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();
        dtCol.ColumnName = "StockInId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Type";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "SrBarVodeID";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Barcode";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Row";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Rack";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Shelf";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Warranty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "WarrantyTo";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CoderLifeTo";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "EnterBarcode";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ProcessId";
        oTable.Columns.Add(dtCol);

        return oTable;
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
        dtCol.ColumnName = "WarrantyPeriod";
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
    protected void btnChooseRequisition_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);

    }
    protected void gv_EstRawMat_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string ID = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        string ReqPopAppId = "";
        try
        {
            ReqPopAppId = ID.Split(',')[0].ToString();
        }
        catch { }

        try
        {
            string Qry = "select a.ReqPopAppId as ReqAppId, a.ReqPopId as ReqId,a.ItemId,a.Qty,b.* from tbl_RequisitionPopDetailsApproval a join tbl_ItemMaster b on a.ItemId=b.ItemId where ReqPopId='" + ReqPopAppId + "' union all select a.ReqSecAppId as ReqAppId, a.ReqSecId as ReqId,a.ItemId,a.Qty,b.* from tbl_RequisitionSecDetailsApproval a join tbl_ItemMaster b on a.ItemId=b.ItemId where ReqSecAppId='" + ReqPopAppId + "' union all select a.ReqTetAppId as ReqAppId, a.ReqTetId as ReqId,a.ItemId,a.Qty,b.* from tbl_RequisitionTetDetailsApproval a join tbl_ItemMaster b on a.ItemId=b.ItemId where ReqTetAppId='" + ReqPopAppId + "' union all select a.ReqNOCAppId as ReqAppId, a.ReqNOCId as ReqId,a.ItemId,a.Qty,b.* from tbl_RequisitionNOCDetailsApproval a join tbl_ItemMaster b on a.ItemId=b.ItemId where ReqNOCAppId='" + ReqPopAppId + "' union all select a.ReqProId as ReqAppId, a.ReqProId as ReqId,a.ItemId,a.Qty,b.* from [tbl_RequisitionProjectDetails] a join tbl_ItemMaster b on a.ItemId=b.ItemId where ReqProId='" + ReqPopAppId + "'";
            SqlDataSource3.SelectCommand = Qry;
        }
        catch
        {

        }
    }
    protected void gv_Barc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_Barc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_Barc.EditIndex = e.NewEditIndex;
    }
    protected void gv_Barc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Barc.PageIndex = e.NewPageIndex;
        gv_Barc.DataSource = ViewState["TempBarcode"];
        gv_Barc.DataBind();
    }
    protected void gv_Barc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = gvr.RowIndex;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["TempBarcode"];
            dt.Rows[rowindex].Delete();
            dt.AcceptChanges();
            ViewState["TempBarcode"] = dt;
            gv_Barc.DataSource = dt;
            gv_Barc.DataBind();
        }
    }
    protected void btnAddServiceItem_Click(object sender, EventArgs e)
    {
        string BranchIdSec = ddlRequisitionto.ValueField;
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        string ReqAppId = "";
        List<object> fieldValues = gvItem.GetSelectedFieldValues(new string[] { "ReqAppId" });
        if (fieldValues.Count != 0)
        {
            DataTable AppQtn = new DataTable();

            for (int j = 0; j < fieldValues.Count; j++)
            {
                ReqAppId = fieldValues[j].ToString();
                txtReqPopAppId.Text = ReqAppId.ToString();

                DataTable dt8 = (DataTable)ViewState["ItemDetails"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        if (ReqAppId == dt8.Rows[i]["ReqAppId"].ToString())
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
                            return;
                        }
                    }
                }

                DataTable dtI = (DataTable)ViewState["ItemDetails"];
                DataTable dt11 = DBAccess.FetchDatatable("select a.ReqPopAppId as ReqAppId, a.ReqPopId as ReqId,a.ItemId,a.Qty,b.*,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate,   "+
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdSec + "' and (Status1 is null or Status1 ='')  "+
@" and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as POPQty,   "+
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails  where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='')  "+
@" and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as ReqToQty  "+
@" from tbl_RequisitionPopDetailsApproval a join tbl_ItemMaster b on a.ItemId=b.ItemId join dbo.tbl_HSNCodeMaster h   on b.HSNCode=h.HSNCode  "+
@" where ReqPopAppId='" + ReqAppId + "'   "+
@" union all  "+
@" select a.ReqSecAppId as ReqAppId, a.ReqSecId as ReqId,a.ItemId,a.Qty,b.*, h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate,  "+
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdSec + "' and (Status1 is null or Status1 ='') and  "+
@" (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as POPQty,   "+
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails  where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='')  " +
@" and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as ReqToQty "+
@" from tbl_RequisitionSecDetailsApproval a  join tbl_ItemMaster b on a.ItemId=b.ItemId join dbo.tbl_HSNCodeMaster h on b.HSNCode=h.HSNCode 
where ReqSecAppId='" + ReqAppId + "' "+
@" union all "+
@" select a.ReqTetAppId as ReqAppId, a.ReqTetId as ReqId,a.ItemId,a.Qty,b.*,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate, "+ 
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdSec + "' and (Status1 is null or Status1 ='') "+
@" and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as POPQty, "+
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') "+
@" and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as ReqToQty "+
@" from tbl_RequisitionTetDetailsApproval a join tbl_ItemMaster b on a.ItemId=b.ItemId join dbo.tbl_HSNCodeMaster h on b.HSNCode=h.HSNCode"+
@" where ReqTetAppId='" + ReqAppId + "'  "+
@" union all " +
@" select a.ReqtransAppId as ReqAppId, a.ReqtransId as ReqId,a.ItemId,a.Qty,b.*,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate,"+
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdSec + "' and (Status1 is null or Status1 ='') "+
@" and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as POPQty, "+
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails  where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='')"+
@" and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as ReqToQty "+
@" from tbl_RequisitionTransferDetailsApproval a join tbl_ItemMaster b on a.ItemId=b.ItemId  join dbo.tbl_HSNCodeMaster h on b.HSNCode=h.HSNCode "+
@" where ReqtransAppId='" + ReqAppId + "'  "+
@" union all "+
@" select a.ReqNOCAppId as ReqAppId, a.ReqNOCId as ReqId,a.ItemId,a.Qty,b.*,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate, "+
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdSec + "' and "+
@" (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as POPQty,"+ 
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails  where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and "+
@" (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as ReqToQty "+
@" from tbl_RequisitionNOCDetailsApproval a join tbl_ItemMaster b on a.ItemId=b.ItemId join dbo.tbl_HSNCodeMaster h on b.HSNCode=h.HSNCode "+
@" where ReqNOCAppId='" + ReqAppId + "' "+
@" union all "+
@" select a.ReqProId as ReqAppId, a.ReqProId as ReqId,a.ItemId,a.Qty,b.*,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate,"+ 
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdSec + "' and "+
@" (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as POPQty, "+
@" (select sum(Qty) from tbl_RackStockInBarcodeDetails  where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and "+
@" (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as ReqToQty "+
@" from tbl_RequisitionProjectDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId join dbo.tbl_HSNCodeMaster h on b.HSNCode=h.HSNCode "+
@" where ReqProId='" + ReqAppId + "'"); 
                if (dt11.Rows.Count > 0)
                {
                    for (int i = 0; i < dt11.Rows.Count; i++)
                    {
                        DataRow drH = dtI.NewRow();
                        drH["ItemId"] = dt11.Rows[i]["ItemId"].ToString();
                        Session["ItemIdStock"] = drH["ItemId"].ToString();
                        drH["Category"] = dt11.Rows[i]["Category"].ToString();
                        drH["Type"] = dt11.Rows[i]["Type"].ToString();
                        drH["ItemName"] = dt11.Rows[i]["ItemName"].ToString();
                        drH["Make"] = dt11.Rows[i]["Make"].ToString();
                        drH["Model"] = dt11.Rows[i]["Model"].ToString();
                        drH["Unit"] = dt11.Rows[i]["Unit"].ToString();
                        drH["WarrantyPeriod"] = dt11.Rows[i]["WarrantyPeriod"].ToString();
                        drH["Qty"] = dt11.Rows[i]["Qty"].ToString();
                        drH["rate"] = dt11.Rows[i]["rate"].ToString();
                        drH["HSNCode"] = dt11.Rows[i]["HSNCode"].ToString();
                        drH["CGST"] = dt11.Rows[i]["CGST"].ToString();
                        drH["IGST"] = dt11.Rows[i]["IGST"].ToString();
                        drH["SGST"] = dt11.Rows[i]["SGST"].ToString();
                        drH["ReqToQty"] = dt11.Rows[i]["ReqToQty"].ToString();
                        drH["POPQty"] = dt11.Rows[i]["POPQty"].ToString();
                        txtReqPopId.Text = dt11.Rows[i]["ReqId"].ToString();


                        //TextBox txtItemQty = (TextBox)gvItemDetails.Rows[b].FindControl("txtItemQty");
                        //decimal IssueQty1 = Convert.ToDecimal(txtItemQty.Text);
                        //Session["IssueQty"] = IssueQty1;
                        //Session["ItemId"] = dt11.Rows[0]["ItemId"].ToString();

                        //string ItemId = Session["ItemId"].ToString();
                        //decimal IssueQty = Convert.ToDecimal(Session["IssueQty"]);


                        dtI.Rows.Add(drH);

                        ViewState["ItemDetails"] = dtI;
                        gvItemDetails.DataSource = dtI;
                        gvItemDetails.DataBind();
                        btnStockOut.Visible = false;


                    }
                }
                DataTable dt2 = DBAccess.FetchDatatable(" select a.ReqPopAppId as ReqAppId,  a.ReqPopId as ReqId, convert(varchar,a.RequisitionDate,103) as RequisitionDate, a.RequisitionPurpose, a.Status5 as FromGr, a.Status4 as Location, a.UserId, a.Status3, a.status1, a.Remarks, b.Remarks as AppReq from " +
               @"  tbl_RequisitionPopApproval a join tbl_RequisitionPop b on a.ReqPopId=b.ReqPopId where ReqPopAppId='" + ReqAppId + "'  " +
@" union all  "+
@" select a.ReqSecAppId as ReqAppId,  a.ReqSecId as ReqId, convert(varchar,a.RequisitionDate,103) as RequisitionDate, a.RequisitionPurpose, a.Status5 as FromGr, a.Status4 as Location, a.Remarks, b.Remarks  as AppReq, a.UserId, a.Status3, a.status1  from tbl_RequisitionSecApproval a join tbl_RequisitionSection b on a.ReqSecId=b.ReqSecId  " +
@" where ReqSecAppId='" + ReqAppId + "'  " +
@" union all  "+
@" select a.ReqTetAppId as ReqAppId,  a.ReqtetId as ReqId, convert(varchar,a.RequisitionDate,103) as RequisitionDate, a.RequisitionPurpose, a.Status5 as FromGr, a.Status4 as Location, a.UserId, a.Status3, a.status1, a.Remarks, b.Remarks as AppReq from tbl_RequisitionTetApproval a join tbl_RequisitionTerrytory b on a.ReqTetId=b.ReqTetId " +
@"  where ReqTetAppId='" + ReqAppId + "'  " +
@" union all  "+
@" select a.ReqNOCAppId as ReqAppId,  a.ReqNOCId as ReqId, convert(varchar,a.RequisitionDate,103) as RequisitionDate, a.RequisitionPurpose, a.Status5 as FromGr, a.Status4 as Location, a.UserId, a.Status3, a.status1, a.Remarks, b.Remarks as AppReq from tbl_RequisitionNOCApproval a join tbl_RequisitionNOC b on a.ReqNOCId=b.ReqNOCId  " +
@"  where ReqNOCAppId='" + ReqAppId + "'  " +
@" union all  "+
@" select a.ReqtransAppId as ReqAppId, a.ReqtransId as ReqId, convert(varchar,a.RequisitionDate,103) as RequisitionDate, a.RequisitionPurpose, a.Status5 as FromGr, a.Status4 as Location, a.UserId, a.Status3, a.status1, a.Remarks, b.Remarks as AppReq from tbl_RequisitionTransferApproval a join tbl_RequisitionTransfer b on a.ReqtransId=b.ReqtransId " +
@" where ReqtransAppId='" + ReqAppId + "' " +
@" union all  "+
@" select ReqProId as ReqAppId,  ReqProId as ReqId, convert(varchar,RequisitionDate,103) as RequisitionDate, RequisitionPurpose, Status5 as FromGr, Status4 as Location,  " +
@" UserId, Status3, status1, Remarks, '' as AppReq from tbl_RequisitionProject where ReqProId='" + ReqAppId + "' ");
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        ddlRequisitionto.Text = dt2.Rows[i]["Location"].ToString();
                        ddlRequToGrp.Text = dt2.Rows[i]["FromGr"].ToString();
                        ddlRequToUser.Text = dt2.Rows[i]["Status3"].ToString();
                        ddlRequisitionPur.Text = dt2.Rows[i]["RequisitionPurpose"].ToString();
                        txtProject.Text = dt2.Rows[i]["Status1"].ToString();
                        string ProjectId = DBAccess.FetchDatasingle("Select ProjectId from tbl_ProjectMaster where ProjectName='" + txtProject.Text + "'");
                        if (ProjectId != "")
                        {
                            txtProjectId.Text=ProjectId;
                        }
                        ReqRemarks.Text = dt2.Rows[i]["Remarks"].ToString();
                        ReqAppRemarks.Text = dt2.Rows[i]["AppReq"].ToString();
                        ddlRequisitionPur.Enabled = false;
                    }
                }

            }

        }
    }
    protected void btnStockOut_Click(object sender, EventArgs e)
    {
        string Status = ddlProOp.Text;

        if (Status == "LocationWise")
        {
            for (int a = 0; a < gvItemDetails.Rows.Count; a++)
            {
                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[a].FindControl("txtItemQty");
                decimal IssueQty1 = Convert.ToDecimal(txtItemQty.Text);
                Session["IssueQty"] = IssueQty1;
                Session["ItemId"] = gvItemDetails.Rows[a].Cells[1].Text;

                string ItemId = Session["ItemId"].ToString();
                decimal IssueQty = Convert.ToDecimal(Session["IssueQty"]);
                string ProjectId = Session["BranchId"].ToString();

                DataTable dtItem = DBAccess.FetchDatatable(@"select distinct a.*,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate ,b.ItemName,b.Type as Type1,'' as EnterBarcode from tbl_RackStockInBarCodeDetails a join [tbl_RackStockInDetails] c on a.ActualStockInId=c.ActualStockInId join tbl_ItemMaster b on a.ItemId=b.ItemId      where a.ItemId='" + ItemId + "' and a.BranchId='" + ProjectId + "'  and a.Qty>0 and c.AvailableQty>0 and (Status1='' or status1 is null) order by a.WarrantyTo asc");
                String AvailableQty = DBAccess.FetchDatasingle(@"Select CurrentStock from tbl_ActualStock  where ItemId='" + ItemId + "' and BranchId='" + ProjectId + "'");

                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    for (int jj = 0; jj < dtItem.Rows.Count; jj++)
                    {
                        decimal StockQty = 0;
                        string StockInId = "";
                        string ActualStockInId = "";
                        string ActualStockInDetailsId = "";
                        decimal StockOutQty = 0;
                        decimal newQty;
                        try
                        {
                            StockQty = Convert.ToDecimal(dtItem.Rows[jj]["Qty"]);
                            StockInId = dtItem.Rows[jj]["StockInId"].ToString();
                            ActualStockInId = dtItem.Rows[jj]["ActualStockInId"].ToString();
                            ActualStockInDetailsId = dtItem.Rows[jj]["ActualStockInDetailsId"].ToString();

                        }
                        catch
                        {
                        }

                        String barAvaiQty = DBAccess.FetchDatasingle(@"select sum(Qty) as Qty from tbl_RackStockInBarCodeDetails  where ItemId='" + ItemId + "' and BranchId='" + ProjectId + "' and StockInId='" + StockInId + "' and ActualStockInId='" + ActualStockInId + "' and (Status1='' or status1 is null) ");


                        while (IssueQty > 0)
                        {
                            if (StockQty <= IssueQty)
                            {
                                newQty = (IssueQty - StockQty);
                                DataTable dtI = (DataTable)ViewState["TempBarcode"];
                                DataTable dt11 = DBAccess.FetchDatatable("select distinct a.*,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate ,b.ItemName,b.Type as Type1,'' as EnterBarcode from tbl_RackStockInBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.StockInId='" + StockInId + "' and a.ItemId='" + ItemId + "' and a.BranchId='" + ProjectId + "' and  a.ActualStockInId='" + ActualStockInId + "' and a.ActualStockInDetailsId='" + ActualStockInDetailsId + "' ");
                                if (dt11 != null && dt11.Rows.Count > 0)
                                {
                                    DataRow drH = dtI.NewRow();
                                    drH["StockInId"] = dt11.Rows[0]["StockInId"].ToString();
                                    drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                                    drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                                    drH["Type"] = dt11.Rows[0]["Type1"].ToString();
                                    drH["SrBarVodeID"] = dt11.Rows[0]["SrBarVodeID"].ToString();
                                    drH["ProcessId"] = dt11.Rows[0]["ProcessId"].ToString();
                                    drH["EnterBarcode"] = dt11.Rows[0]["EnterBarcode"].ToString();
                                    drH["Barcode"] = dt11.Rows[0]["Barcode"].ToString();
                                    drH["Warranty"] = dt11.Rows[0]["Warranty"].ToString();
                                    drH["WarrantyTo"] = dt11.Rows[0]["WarrantyDate"].ToString();
                                    drH["CoderLifeTo"] = dt11.Rows[0]["CoderLifeDate"].ToString();
                                    drH["Row"] = dt11.Rows[0]["Row"].ToString();
                                    drH["Rack"] = dt11.Rows[0]["Rack"].ToString();
                                    drH["Shelf"] = dt11.Rows[0]["Shelf"].ToString();
                                    dtI.Rows.Add(drH);

                                    ViewState["TempBarcode"] = dtI;
                                    gv_Barc.DataSource = dtI;
                                    gv_Barc.DataBind();
                                }

                                StockOutQty = newQty;
                                newQty = 0;
                            }
                            else
                            {
                                newQty = (IssueQty - StockQty);

                            }

                            IssueQty = StockOutQty;
                            break;
                        }

                    }
                }
                else
                {

                }
            }
        }

        else
        {
             #region  ProjectWiseIssue

            for (int a = 0; a < gvItemDetails.Rows.Count; a++)
            {
                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[a].FindControl("txtItemQty");
                decimal IssueQty1 = Convert.ToDecimal(txtItemQty.Text);
                Session["IssueQty"] = IssueQty1;
                Session["ItemId"] = gvItemDetails.Rows[a].Cells[1].Text;

                string ItemId = Session["ItemId"].ToString();
                decimal IssueQty = Convert.ToDecimal(Session["IssueQty"]);
                string ProjectId = Session["BranchId"].ToString();
                String BrProjectId = txtProjectId.Text;
                string ProjectName = txtProject.Text;

                DataTable dtItem = DBAccess.FetchDatatable(@"select distinct a.*,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate ,b.ItemName,b.Type as Type1,'' as EnterBarcode from tbl_RackStockInBarCodeDetails a join [tbl_RackStockInDetails] c on a.ActualStockInId=c.ActualStockInId join tbl_ItemMaster b on a.ItemId=b.ItemId where a.ItemId='" + ItemId + "' and a.BranchId='" + ProjectId + "' and a.ProjectId='" + BrProjectId + "' and a.ProjectName='" + ProjectName + "'  and a.Qty>0 and c.AvailableQty>0  and (a.Status1 is null or a.Status1 ='') and (a.Status2 is null or a.Status2 ='') and (a.Status3 is null or a.Status3 ='') and (a.Status4 is null or a.Status4 ='')  order by a.WarrantyTo asc");
                String AvailableQty = DBAccess.FetchDatasingle(@"Select CurrentStock from tbl_ActualStock  where ItemId='" + ItemId + "' and BranchId='" + ProjectId + "'");

                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    for (int jj = 0; jj < dtItem.Rows.Count; jj++)
                    {
                        decimal StockQty = 0;
                        string StockInId = "";
                        string ActualStockInId = "";
                        string ActualStockInDetailsId = "";
                        decimal StockOutQty = 0;
                        decimal newQty;
                        try
                        {
                            StockQty = Convert.ToDecimal(dtItem.Rows[jj]["Qty"]);
                            StockInId = dtItem.Rows[jj]["StockInId"].ToString();
                            ActualStockInId = dtItem.Rows[jj]["ActualStockInId"].ToString();
                            ActualStockInDetailsId = dtItem.Rows[jj]["ActualStockInDetailsId"].ToString();

                        }
                        catch
                        {
                        }

                        String barAvaiQty = DBAccess.FetchDatasingle(@"select sum(Qty) as Qty from tbl_RackStockInBarCodeDetails  where ItemId='" + ItemId + "' and BranchId='" + ProjectId + "' and StockInId='" + StockInId + "' and ActualStockInId='" + ActualStockInId + "' and ProjectId='" + BrProjectId + "' and ProjectName='" + ProjectName + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')");


                        while (IssueQty > 0)
                        {
                            if (StockQty <= IssueQty)
                            {
                                newQty = (IssueQty - StockQty);
                                DataTable dtI = (DataTable)ViewState["TempBarcode"];
                                DataTable dt11 = DBAccess.FetchDatatable("select distinct a.*,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate ,b.ItemName,b.Type as Type1,'' as EnterBarcode from tbl_RackStockInBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.StockInId='" + StockInId + "' and a.ItemId='" + ItemId + "' and a.BranchId='" + ProjectId + "' and  a.ActualStockInId='" + ActualStockInId + "' and a.ActualStockInDetailsId='" + ActualStockInDetailsId + "' and ProjectId='" + BrProjectId + "' and ProjectName='" + ProjectName + "'");
                                if (dt11 != null && dt11.Rows.Count > 0)
                                {
                                    DataRow drH = dtI.NewRow();
                                    drH["StockInId"] = dt11.Rows[0]["StockInId"].ToString();
                                    drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                                    drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                                    drH["Type"] = dt11.Rows[0]["Type1"].ToString();
                                    drH["SrBarVodeID"] = dt11.Rows[0]["SrBarVodeID"].ToString();
                                    drH["ProcessId"] = dt11.Rows[0]["ProcessId"].ToString();
                                    drH["EnterBarcode"] = dt11.Rows[0]["EnterBarcode"].ToString();
                                    drH["Barcode"] = dt11.Rows[0]["Barcode"].ToString();
                                    drH["Warranty"] = dt11.Rows[0]["Warranty"].ToString();
                                    drH["WarrantyTo"] = dt11.Rows[0]["WarrantyDate"].ToString();
                                    drH["CoderLifeTo"] = dt11.Rows[0]["CoderLifeDate"].ToString();
                                    drH["Row"] = dt11.Rows[0]["Row"].ToString();
                                    drH["Rack"] = dt11.Rows[0]["Rack"].ToString();
                                    drH["Shelf"] = dt11.Rows[0]["Shelf"].ToString();
                                    dtI.Rows.Add(drH);

                                    ViewState["TempBarcode"] = dtI;
                                    gv_Barc.DataSource = dtI;
                                    gv_Barc.DataBind();
                                }

                                StockOutQty = newQty;
                                newQty = 0;
                            }
                            else
                            {
                                newQty = (IssueQty - StockQty);

                            }

                            IssueQty = StockOutQty;
                            break;
                        }

                    }
                }
                else
                {

                }
            }
             #endregion ProjectWiseIssue
        }

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

        string IssueId = DBAccess.FetchDatatable("select [dbo].[fn_IssueId]()").Rows[0][0].ToString();
        string RequisitionDate = dtpDate.Text;
        string PrioritiesId = ddlRequisitionPur.Value.ToString();
        string RequisitionPurpose = ddlRequisitionPur.Text;
        string ReUserGroupId = ddlRequToGrp.Text;
        string ReUserGroupName = ddlRequToGrp.Text;
        string ReUserId = ddlRequisitionto.Value.ToString();
        string ReUserName = ddlRequisitionto.Text;
        string Remarks = txtRemarks.Text;
        string Status = ddlRequToUser.Text;
        string Status1 = txtProject.Text;
        string Status2 = "";
        string Status3 = "";
        string Status4 = txtBranchName.Text;
        string Status5 = txtUserGroup.Text;
        string ReqAppId = txtReqPopAppId.Text;
        string ReqId = txtReqPopId.Text;

        if (gv_Barc.Rows.Count == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please verify Barcode First');", true);
            return;
        }

        int n = ClassMaterialIssue.MaterialIssueSave(IssueId, ReqAppId, ReqId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, UserGroupId, UserId, BranchId, DOE);

        if (n == 1)
        {
            for (int i = 0; i < gvItemDetails.Rows.Count; i++)
            {
                string IssueItemId = DBAccess.FetchDatatable("select [dbo].[fn_IssueItemId]()").Rows[0][0].ToString();
                string ItemId = gvItemDetails.Rows[i].Cells[1].Text;
                string Category = gvItemDetails.Rows[i].Cells[2].Text;
                string ItemName = gvItemDetails.Rows[i].Cells[4].Text;
                string Make = gvItemDetails.Rows[i].Cells[5].Text;
                string Model = gvItemDetails.Rows[i].Cells[6].Text;
                string Unit = gvItemDetails.Rows[i].Cells[7].Text;
                decimal ReqToQty = 0;
                decimal POPQty = 0;
                string Status6 = "";

                try
                {
                    ReqToQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[8].Text);
                    POPQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[9].Text);
                }
                catch
                { }
                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                int m = ClassMaterialIssue.MaterialIssueSaveDetails(IssueId, IssueItemId, ReqAppId, ReqId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                if (m == 1)
                {
                    if (ViewState["TempBarcode"] != null)
                    {
                        DataTable dt1 = ViewState["TempBarcode"] as DataTable;
                        for (int a = 0; a < gv_Barc.Rows.Count; a++)
                        {
                            for (int j = 0; j < dt1.Rows.Count; j++, a++)
                            {
                                string ItemId1 = dt1.Rows[j]["ItemId"].ToString();
                                string IssueItemBrId = DBAccess.FetchDatatable("select  [dbo].[fn_IssueItemBrId]()").Rows[0][0].ToString();
                                string StockInId = dt1.Rows[j]["StockInId"].ToString();
                                string SrBarVodeID = dt1.Rows[j]["SrBarVodeID"].ToString();
                                string Barcode = dt1.Rows[j]["Barcode"].ToString();
                                decimal Qtyb = 1;
                                TextBox txtBarcodeEnter = (TextBox)gv_Barc.Rows[a].FindControl("txtBarcodeEnter");
                                string EnterBarCode = "";
                                try
                                {
                                    EnterBarCode = txtBarcodeEnter.Text;
                                }
                                catch
                                {
                                    //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter Barcode.');", true);
                                    //return;
                                }
                                if (EnterBarCode != Barcode)
                                {
                                    bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_MaterialIssue] where IssueId='" + IssueId + "' ");
                                    bool DeleteDe = DBAccess.SaveData(@"delete from [dbo].[tbl_MaterialIssueDetails]  where IssueId='" + IssueId + "' ");
                                    bool DeleteDeBar = DBAccess.SaveData(@"delete from [dbo].[tbl_MaterialIssueBarCodeDetails]  where IssueId='" + IssueId + "' ");


                                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter valid Barcode.');", true);
                                    return;
                                }

                                string ProcessId = dt1.Rows[j]["ProcessId"].ToString();
                                string Row1 = dt1.Rows[j]["Row"].ToString();
                                string Rack1 = dt1.Rows[j]["Rack"].ToString();
                                string Shelf1 = dt1.Rows[j]["Shelf"].ToString();
                                string Warranty = dt1.Rows[j]["Warranty"].ToString();
                                string WarrantyTo = dt1.Rows[j]["WarrantyTo"].ToString();
                                string CoderLifeTo = dt1.Rows[j]["CoderLifeTo"].ToString();



                                bool SaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_MaterialIssueBarCodeDetails] values ('" + IssueId + "','" + IssueItemId + "','" + IssueItemBrId + "','" + StockInId + "','" + SrBarVodeID + "', '" + ItemId1 + "', '" + Barcode + "', '" + ProcessId + "','" + Row1 + "','" + Rack1 + "', '" + Shelf1 + "', '" + Warranty + "',convert(datetime,'" + WarrantyTo + "',103),convert(datetime,'" + CoderLifeTo + "',103),'','','','','','', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");

                                bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set Status1='Issue', Qty='0' where SrBarVodeID='" + SrBarVodeID + "' and ProcessId='" + ProcessId + "' and Row='" + Row1 + "' and Rack='" + Rack1 + "' and Shelf= '" + Shelf1 + "' and BranchId='" + BranchId + "' and StockInId='" + StockInId + "'");

                                bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty-'" + Qtyb + "' where ItemId='" + ItemId1 + "' and RackSpace='" + ProcessId + "' and Row='" + Row1 + "' and Rack='" + Rack1 + "' and Shelf= '" + Shelf1 + "' and BranchId='" + BranchId + "' and StockInId='" + StockInId + "'");

                            }
                            ViewState["TempBarcode"] = null;
                        }


                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Issue Done Successfully.');window.location='../Project/MaterialIssuePop.aspx';", true);

                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Issue Not Done. Try again.');", true);
                    return;
                }
            }
        }
    }
    protected void ddlProOp_TextChanged(object sender, EventArgs e)
    {
        string ProOp = ddlProOp.Text;
        if (ProOp == "ProjectWise")
        {
            btnStockOut.Visible = true;
            ProjectTr.Visible = true;
        }
        else
        {
            btnStockOut.Visible = true;
            ProjectTr.Visible = false;
        }
    }
   
}