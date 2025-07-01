using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using DevExpress.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;

public partial class Project_RackTag_BarcodeGen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["TempStockIn"], null))
        {
            ViewState["TempStockIn"] = getDataTable();
        }

        if (ReferenceEquals(ViewState["TempBarcode"], null))
        {
            ViewState["TempBarcode"] = getBarcode();
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
                string ToUserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='UG15'");
            }
            catch

            { }
            try
            {
                UserId = Session["UserId"].ToString();

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
                Session["BranchName"] = BranchName.ToString();
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
                return;
            }
            Session["StockInId"] = "";

        }

    }

    protected DataTable getDataTable()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();
        dtCol.ColumnName = "StockInId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "StockQty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "RackQty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "RackSpace";
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

        return oTable;
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

        return oTable;
    }
    protected void ddlStockInId_TextChanged(object sender, EventArgs e)
    {
        string StockInId = ddlStockInId.Value.ToString();
        Session["StockInId"] = StockInId;

        DataTable StockIn = DBAccess.FetchDatatable("select  ProjectId, ProjectName FROM tbl_StockIn  where StockInId='" + StockInId + "' union all select p.ProjectId, a.Status1 as ProjectName FROM tbl_MaterialIssue a join tbl_ProjectMaster p on a.Status1=p.ProjectName join tbl_MaterialReceive mr on a.IssueId=mr.IssueId where mr.ReceiveId='" + StockInId + "' union all select p.ProjectId, b.Status1 as ProjectName FROM tbl_TemporaryStockIssue a join tbl_RequisitionTransferApproval b on a.ReqAppId = b.ReqtransAppId join tbl_ProjectMaster p on b.Status1=p.ProjectName where a.TemIssueId='" + StockInId + "' union all select b.ProjectId, b.ProjectName as ProjectName FROM tbl_FaultyItemReceiceDetails a join tbl_RackStockInBarCodeDetails b on a.Barcode = b.Barcode  where a.FaultyRecvId='" + StockInId + "'");
        if (StockIn != null && StockIn.Rows.Count > 0)
        {
            txtProject.Text = StockIn.Rows[0]["ProjectName"].ToString();
            txtProjectId.Text = StockIn.Rows[0]["ProjectId"].ToString();

        }

        ddlItem.DataBind();

    }

    private void ClearConcern()
    {
        ddlStockInId.Text = "";
        ddlItem.Text = "";
        txtStockQty.Text = "";
        txtRackQtyEnter.Text = "";
        ddlRackSpace.Text = "";
        txtAvailableQty.Text = "";
        lblItemSpaceUnit.Text = "";
        lblTotalSpaceUnit.Text = "";
        txtRackQtyEnter.Text = "";
        txtRackQty.Text = "";



    }
    protected void ddlItem_TextChanged(object sender, EventArgs e)
    {
        trItemUnit.Visible = true;
        string StockInId = ddlStockInId.Value.ToString();
        string ItemId = ddlItem.Value.ToString();
        Session["ItemId"] = ItemId;
        string StockInQty1 = DBAccess.FetchDatasingle(" select distinct Qty from tbl_StockInDetails where StockInId='" + StockInId + "' and ItemId='" + ItemId + "'  Union all  select distinct Qty from [tbl_MaterialReceiveDetails] where ReceiveId='" + StockInId + "' and ItemId='" + ItemId + "' Union all  select distinct Qty from tbl_TemporaryStockIssueDetails where TemIssueId='" + StockInId + "' and ItemId='" + ItemId + "' Union all  select distinct 1 as  Qty from tbl_FaultyItemReceiceDetails where FaultyRecvId='" + StockInId + "' and ItemId='" + ItemId + "'");
        DataTable RackQty1 = DBAccess.FetchDatatable("select  ab.Stockinid, ab.ItemId, Sum(ab.Qty) as a FROM tbl_RackStockInDetails ab  where ab.StockInId='" + StockInId + "' and ab.ItemId='" + ItemId + "' group By ab.Stockinid, ab.ItemId");
        if (RackQty1 != null && RackQty1.Rows.Count > 0)
        {
            string Qty = RackQty1.Rows[0]["a"].ToString();
            decimal StockInQty = Convert.ToDecimal(StockInQty1);
            decimal RackQty = Convert.ToDecimal(Qty);
            decimal RestQty = StockInQty - RackQty;
            txtStockQty.Text = StockInQty.ToString();
            txtRackQtyEnter.Text = RestQty.ToString();
        }
        else
        {
            txtStockQty.Text = StockInQty1;
            txtRackQtyEnter.Text = StockInQty1;
            ddlRackSpace.Text = "";
            txtAvailableQty.Text = "";
            lblItemSpaceUnit.Text = "";
        }
        string ItemSpaceUnit = DBAccess.FetchDatasingle("select SpaceUnit from tbl_ItemMaster where  ItemId='" + ItemId + "' ");
        lblItemSpaceUnit.Text = ItemSpaceUnit.ToString();

    }
    protected void gv_ConcPer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = gvr.RowIndex;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["TempStockIn"];
            dt.Rows[rowindex].Delete();
            dt.AcceptChanges();
            ViewState["TempStockIn"] = dt;
            gv_ConcPer.DataSource = dt;
            gv_ConcPer.DataBind();
        }

    }
    protected void gv_ConcPer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_ConcPer_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_ConcPer.EditIndex = e.NewEditIndex;
    }
    protected void gv_ConcPer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ConcPer.PageIndex = e.NewPageIndex;
        gv_ConcPer.DataSource = ViewState["TempStockIn"];
        gv_ConcPer.DataBind();
    }
    protected void btn_AddConPers_Click(object sender, EventArgs e)
    {
        string StockInId = ddlStockInId.Value.ToString();
        string ItemId = ddlItem.Value.ToString();
        decimal StockQty = Convert.ToDecimal(txtStockQty.Text);
        decimal RackQty = Convert.ToDecimal(txtRackQty.Text);
        string RackSpace = "";
        try
        {
            RackSpace = ddlRackSpace.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Rack First');", true);
            return;
        }
        decimal RackQtyEnter = Convert.ToDecimal(txtRackQtyEnter.Text);

        decimal RackSpaceUnit = Convert.ToDecimal(txtRackSpace.Text);
        decimal TotalItemUnit = Convert.ToDecimal(lblTotalSpaceUnit.Text);

        if (chkManual.Checked == true)
        {

        }
        else
        {
            if (RackSpaceUnit < TotalItemUnit)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Not Space in Shelf');", true);
                return;
            }
        }

        if (RackQtyEnter < RackQty)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! More Than StockIn Qty');", true);
            return;
        }

        if (btn_AddConPers.Text != "Update Concern Person")
        {
            DataTable dtt = DBAccess.FetchDatatable("select * from tbl_RackSpaceMaster where ProcessId='" + RackSpace + "'");
            if (dtt != null && dtt.Rows.Count > 0)
            {
                string Row = dtt.Rows[0]["Row"].ToString();
                string Rack = dtt.Rows[0]["Rack"].ToString();
                string Shelf = dtt.Rows[0]["Shelf"].ToString();

                DataTable dt = (DataTable)ViewState["TempStockIn"];
                DataRow drH = dt.NewRow();
                drH["StockInId"] = StockInId;
                drH["ItemId"] = ItemId;
                drH["StockQty"] = StockQty;
                drH["RackQty"] = RackQty;
                drH["RackSpace"] = RackSpace;
                drH["Row"] = Row;
                drH["Rack"] = Rack;
                drH["Shelf"] = Shelf;



                dt.Rows.Add(drH);

                ViewState["TempStockIn"] = dt;
                this.gv_ConcPer.DataSource = (DataTable)ViewState["TempStockIn"];
                this.gv_ConcPer.DataBind();
            }

        }
        else
        {
        }

        //ClearConcern();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UserId = "";
        if (Session["UserId"] != null)
        {
            UserId = Session["UserId"].ToString();
        }
        string BranchId = "";
        if (Session["BranchId"] != null)
        {
            BranchId = Session["BranchId"].ToString();
        }
        DateTime DOE = DateTime.Now;
        if (ViewState["TempStockIn"] != null)
        {
            DataTable dt = ViewState["TempStockIn"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string GRNNo = txtGRNNo.Text;
                    string Project = txtProject.Text;
                    string ProjectId = txtProjectId.Text;
                    string Status = "";
                    if (chkManual.Checked == true)
                    {
                        Status = "Manual";
                    }

                    string ActualStockInId = DBAccess.FetchDatatable("select  [dbo].[fn_ActualStockInId]()").Rows[0][0].ToString();
                    string Row = dt.Rows[i]["Row"].ToString();
                    string Rack = dt.Rows[i]["Rack"].ToString();
                    string Shelf = dt.Rows[i]["Shelf"].ToString();
                    string StockInId = dt.Rows[i]["StockInId"].ToString();
                    string ItemId = dt.Rows[i]["ItemId"].ToString();
                    decimal RackQty = Convert.ToDecimal(dt.Rows[i]["RackQty"].ToString());
                    decimal AvailableQty = RackQty;
                    string RackSpace = dt.Rows[i]["RackSpace"].ToString();
                    decimal ItemUnit = Convert.ToDecimal(lblItemSpaceUnit.Text);
                    decimal TotalItemUnit = Convert.ToDecimal(lblTotalSpaceUnit.Text);

                    bool Save = DBAccess.SaveData(@"insert into [dbo].[tbl_RackStockInDetails] values ('" + ActualStockInId + "', '" + StockInId + "', '" + GRNNo + "','" + ItemId + "','" + RackQty + "','" + AvailableQty + "','" + RackSpace + "','" + Row + "','" + Rack + "', '" + Shelf + "','" + ItemUnit + "', '" + TotalItemUnit + "', '" + Status + "',  '" + ProjectId + "',  '" + Project + "','" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");
                    bool UpdateStock = DBAccess.SaveData(@"update tbl_StockIn set GRNNo='" + GRNNo + "' where StockInId ='" + StockInId + "' ");

                    string ActQty = DBAccess.FetchDatasingle("select sum(CurrentStock) from tbl_ActualStock where ItemId='" + ItemId + "' and BranchId='" + BranchId + "'");

                    if (ActQty == "")
                    {
                        bool SaveActQty = DBAccess.SaveData(@"Insert into tbl_ActualStock values ('" + ItemId + "', '" + RackQty + "', '" + BranchId + "')");

                    }
                    else
                    {
                        bool UpdateActQty = DBAccess.SaveData(@"update tbl_ActualStock set CurrentStock = (CurrentStock + '" + RackQty + "') where ItemId='" + ItemId + "' and BranchId='" + BranchId + "'");

                    }

                    if (Save == true)
                    {
                        if (ViewState["TempBarcode"] != null)
                        {
                            DataTable dt1 = ViewState["TempBarcode"] as DataTable;
                            if (dt1 != null && dt1.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    string ActualStockInDetailsId = DBAccess.FetchDatatable("select  [dbo].[fn_ActualStockInDetailsId]()").Rows[0][0].ToString();
                                    string SrBarVodeID = dt1.Rows[j]["SrBarVodeID"].ToString();
                                    string ItemId1 = dt1.Rows[j]["ItemId"].ToString();
                                    string Barcode = dt1.Rows[j]["Barcode"].ToString();
                                    decimal Qty = 1;
                                    string ProcessId = ddlRackSpace.Value.ToString();
                                    string Row1 = dt1.Rows[j]["Row"].ToString();
                                    string Rack1 = dt1.Rows[j]["Rack"].ToString();
                                    string Shelf1 = dt1.Rows[j]["Shelf"].ToString();
                                    string Warranty = dt1.Rows[j]["Warranty"].ToString();
                                    string WarrantyTo = dt1.Rows[j]["WarrantyTo"].ToString();
                                    string CoderLifeTo = dt1.Rows[j]["CoderLifeTo"].ToString();



                                    bool SaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_RackStockInBarCodeDetails] values ('" + ActualStockInId + "','" + ActualStockInDetailsId + "','" + StockInId + "','" + SrBarVodeID + "', '" + ItemId1 + "', '" + Barcode + "','','" + Qty + "','" + ProcessId + "','" + Row1 + "','" + Rack1 + "', '" + Shelf1 + "', '" + Warranty + "',convert(datetime,'" + WarrantyTo + "',103),convert(datetime,'" + CoderLifeTo + "',103),'','','','','','','','', '" + ProjectId + "',  '" + Project + "', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");
                                    bool UpdateBarcode = DBAccess.SaveData(@"update tbl_StockInBarcodes set Status='Tag' where SrBarVodeID='" + SrBarVodeID + "'");
                                    bool UpdateBarcodeR = DBAccess.SaveData(@"update [tbl_MaterialReceiveBarCodeDetails] set Status1='Tag' where SrBarVodeID='" + SrBarVodeID + "'");

                                }
                            }
                            ViewState["TempBarcode"] = null;

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Rack StockIn Details Save successfully.'); window.location='../Project/RackTag_BarcodeGen.aspx';", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Stock In Not Done');", true);
                        return;
                    }
                }
            }
        }
    }
    protected void ddlRackSpace_TextChanged(object sender, EventArgs e)
    {
        string ItemId = ddlItem.Value.ToString();
       
        string RackSpace = "";
        try
        {
            RackSpace = ddlRackSpace.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Rack First');", true);
            return;
        }
        string RackQty = DBAccess.FetchDatasingle("select  sum(qty) as Qty  from tbl_RackStockInBarCodeDetails  where ProcessId='" + RackSpace + "' and (Status1='' or Status1 is null) and (Status2='' or Status2 is null) and (Status3='' or Status3 is null) and (Status4='' or Status4 is null)");
        string RackSpaceQty = DBAccess.FetchDatasingle("select  4 - sum(TotalItemUnit) as Qty  from tbl_RackStockInDetails where RackSpace='" + RackSpace + "' ");

        if (RackSpaceQty == "")
        {
            txtRackSpace.Text = "4";
        }
        else
        {
            txtRackSpace.Text = RackSpaceQty.ToString();
        }

        if (RackQty == "")
        {
            txtAvailableQty.Text = "0";
        }
        else
        {
            txtAvailableQty.Text = RackQty.ToString();
        }
        trRackSpace.Visible = true;



    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("RackTag_BarcodeGen.aspx");
    }
    protected void txtRackQty_TextChanged(object sender, EventArgs e)
    {
        decimal Qty = Convert.ToDecimal(txtRackQty.Text);
        decimal SpaceUnit = Convert.ToDecimal(lblItemSpaceUnit.Text);

        decimal Total = Qty * SpaceUnit;
        lblTotalSpaceUnit.Text = Total.ToString();
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
        
        string RackSpace = "";
        try
        {
            RackSpace = ddlRackSpace.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Rack First');", true);
            return;
        }
        string StockInId = "";
        List<object> fieldValues = gvItem.GetSelectedFieldValues(new string[] { "SrBarVodeID" });
        decimal Qty = Convert.ToDecimal(txtRackQty.Text);

        if (fieldValues.Count == Qty)
        {
            DataTable AppQtn = new DataTable();

            for (int j = 0; j < fieldValues.Count; j++)
            {
                StockInId = fieldValues[j].ToString();

                DataTable dt8 = (DataTable)ViewState["TempBarcode"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        if (StockInId == dt8.Rows[i]["SrBarVodeID"].ToString())
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
                            return;
                        }
                    }
                }

                DataTable dtI = (DataTable)ViewState["TempBarcode"];
                DataTable dt11 = DBAccess.FetchDatatable("select distinct a.StockInId,a.ItemId,b.ItemName,a.Type,Barcode, a.SrBarVodeID,a.Warranty,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate from tbl_StockInBarcodes a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.SrBarVodeID ='" + StockInId + "' and (a.Status='' or a.Status is null) Union all select distinct a.StockInId,a.ItemId,b.ItemName,'' as Type,Barcode, a.SrBarVodeID,a.Warranty,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as oderLifeDate from tbl_MaterialReceiveBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.SrBarVodeID ='" + StockInId + "' and (a.Status1='' or a.Status1 is null) Union all select distinct a.TemIssueId as StockInId,a.ItemId,b.ItemName,'' as Type,Barcode, a.SrBarVodeID,a.Warranty,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as oderLifeDate from tbl_TemporaryStockIssueBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.SrBarVodeID ='" + StockInId + "' and a.Status1='Receive' Union all select distinct a.FaultyRecvId as StockInId, a.ItemId,b.ItemName,'' as Type, a.Barcode, a.SrBarVodeID, r.Warranty as Warranty,Convert(Varchar,a.Extra,103) as WarrantyDate, Convert(Varchar,a.Extra1,103) as oderLifeDate from tbl_FaultyItemReceiceDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId join tbl_RackStockInBarCodeDetails r on a.SrBarVodeID=r.SrBarVodeID where a.SrBarVodeID ='" + StockInId + "'");
                if (dt11 != null && dt11.Rows.Count > 0)
                {

                    DataTable dtt = DBAccess.FetchDatatable("select * from tbl_RackSpaceMaster where ProcessId='" + RackSpace + "'");
                    if (dtt != null && dtt.Rows.Count > 0)
                    {
                        string Row = dtt.Rows[0]["Row"].ToString();
                        string Rack = dtt.Rows[0]["Rack"].ToString();
                        string Shelf = dtt.Rows[0]["Shelf"].ToString();
                        DataRow drH = dtI.NewRow();
                        drH["StockInId"] = dt11.Rows[0]["StockInId"].ToString();
                        drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                        drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                        drH["Type"] = dt11.Rows[0]["Type"].ToString();
                        drH["SrBarVodeID"] = dt11.Rows[0]["SrBarVodeID"].ToString();
                        drH["Barcode"] = dt11.Rows[0]["Barcode"].ToString();
                        drH["Warranty"] = dt11.Rows[0]["Warranty"].ToString();
                        drH["WarrantyTo"] = dt11.Rows[0]["WarrantyDate"].ToString();
                        drH["CoderLifeTo"] = dt11.Rows[0]["CoderLifeDate"].ToString();

                        drH["Row"] = Row;
                        drH["Rack"] = Rack;
                        drH["Shelf"] = Shelf;
                        dtI.Rows.Add(drH);

                        ViewState["TempBarcode"] = dtI;
                        gv_Barc.DataSource = dtI;
                        gv_Barc.DataBind();
                    }
                }
            }
        }

        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please select Proper Qty Barcode');", true);
            return;
        }
    }
    protected void AddBarCode_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
    }
}