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

public partial class Project_StockTransfer : System.Web.UI.Page
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
            Session["ItemId"] = "";
            Session["ProjectId"] = "";
            string UserId = "";
            string BranchId = "";

            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();

            try
            {

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
        }
    }
    protected void ddlItem_TextChanged(object sender, EventArgs e)
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

        string ProjectId = ddlItem.Value.ToString();
        Session["ProjectId"] = ProjectId;
    }
    protected void ASPxComboBox1_TextChanged(object sender, EventArgs e)
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
        string ItemId = ASPxComboBox1.Value.ToString();
        string ProjectId = ddlItem.Value.ToString();
        Session["ProjectId"] = ProjectId;
        Session["ItemId"] = ItemId;



        DataTable StockInQty1 = DBAccess.FetchDatatable("SELECT a.ProjectId, a.ProjectName, rsd.ItemId,im.ItemName,im.Make,im.Model, Sum(rsd.ItemUnit) as ItemUnit, Sum(a.Qty) as Qty  FROM tbl_RackStockInDetails rsd  join tbl_ItemMaster im on im.ItemId=rsd.ItemID   join tbl_RackStockInBarCodeDetails a on a.ActualStockInId=rsd.ActualStockInId  where a.BranchId='" + BranchId + "' and a.ProjectId='" + ProjectId + "' and a.ItemId='" + ItemId + "' and (a.Status1='' or a.Status1 is null)and (a.Status2='' or a.Status2 is null) and (a.Status3='' or a.Status3 is null) and  (a.Status4='' or a.Status4 is null)  group by rsd.ItemId,im.ItemName,im.Make,im.Model,a.ProjectId, a.ProjectName");
        DataTable RackQty1 = DBAccess.FetchDatatable("select ab.Stockinid, ab.ItemId, Sum(ab.Qty) as a FROM tbl_RackStockInDetails ab  where ab.ItemId='" + ItemId + "' group By ab.Stockinid, ab.ItemId");

        if (StockInQty1 != null && StockInQty1.Rows.Count > 0)
        {
            string Qty = RackQty1.Rows[0]["a"].ToString();
            decimal StockInQty = Convert.ToDecimal(StockInQty1.Rows[0]["Qty"].ToString());
            decimal RackQty = Convert.ToDecimal(Qty);
            decimal RestQty = StockInQty - RackQty;
            txtStockQty.Text = StockInQty.ToString();


            //txtRackQtyEnter.Text = RestQty.ToString();
        }
        else
        {
            txtStockQty.Text = StockInQty1.Rows[0]["Qty"].ToString();
            //txtRackQtyEnter.Text = StockInQty1.Rows[0]["Qty"].ToString();
            ddlRackSpace.Text = "";
            txtAvailableQty.Text = "";
        }
    }
    protected void ddlRackSpace_TextChanged(object sender, EventArgs e)
    {

        string BranchId = "";
        if (Session["BranchId"] != null)
        {
            BranchId = Session["BranchId"].ToString();
        }
        DateTime DOE = DateTime.Now;
        string ItemId = ASPxComboBox1.Value.ToString();

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

        DataTable ProAvai = DBAccess.FetchDatatable("SELECT a.ProjectId, a.ProjectName, rsd.ItemId,im.ItemName,im.Make,im.Model, Sum(rsd.ItemUnit) as ItemUnit, Sum(a.Qty) as Qty  FROM tbl_RackStockInDetails rsd  join tbl_ItemMaster im on im.ItemId=rsd.ItemID   join tbl_RackStockInBarCodeDetails a on a.ActualStockInId=rsd.ActualStockInId  where a.BranchId='" + BranchId + "' and a.ProjectId='" + RackSpace + "' and a.ItemId='" + ItemId + "' and (a.Status1='' or a.Status1 is null)and (a.Status2='' or a.Status2 is null) and (a.Status3='' or a.Status3 is null) and  (a.Status4='' or a.Status4 is null)  group by rsd.ItemId,im.ItemName,im.Make,im.Model,a.ProjectId, a.ProjectName");

        if (ProAvai != null && ProAvai.Rows.Count > 0)
        {

            decimal StockInQty = Convert.ToDecimal(ProAvai.Rows[0]["Qty"].ToString());
            txtProAvail.Text = StockInQty.ToString();
        }

    }

    protected void AddBarCode_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
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
                DataTable dt11 = DBAccess.FetchDatatable("select distinct a.*,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate ,b.ItemName from tbl_RackStockInBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.SrBarVodeID ='" + StockInId + "'");
                if (dt11 != null && dt11.Rows.Count > 0)
                {

                    string Row = dt11.Rows[0]["Row"].ToString();
                    string Rack = dt11.Rows[0]["Rack"].ToString();
                    string Shelf = dt11.Rows[0]["Shelf"].ToString();
                    DataRow drH = dtI.NewRow();
                    drH["StockInId"] = dt11.Rows[0]["StockInId"].ToString();
                    drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                    drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                    //drH["Type"] = dt11.Rows[0]["Type"].ToString();
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

        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please select Proper Qty Barcode');", true);
            return;
        }
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockTransfer.aspx");
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

        string TransferId = DBAccess.FetchDatatable("select [dbo].[fn_TransferId]()").Rows[0][0].ToString();
        string TransferDate = dtpDate.Text;

        string FromProjectId = ddlItem.Value.ToString();
        string FromProject = ddlItem.Text;

        string ItemId = ASPxComboBox1.Value.ToString();
        string ItemName = ASPxComboBox1.Text;

        decimal QtyT = Convert.ToDecimal(txtRackQty.Text);

        string ToProjectId = ddlRackSpace.Value.ToString();
        string ToProject = ddlRackSpace.Text;

        string ActualStockInId = DBAccess.FetchDatatable("select  [dbo].[fn_ActualStockInId]()").Rows[0][0].ToString();
        string StockInId = DBAccess.FetchDatatable("select  [dbo].[fn_RackStockInId]()").Rows[0][0].ToString();
        string GRNNo = "";
        decimal ItemUnit = Convert.ToDecimal(DBAccess.FetchDatasingle(" select SpaceUnit from tbl_ItemMaster where ItemId = '" + ItemId + "'"));
        decimal TotalItemUnit = QtyT * ItemUnit;
        string RackSpace = "RSM-12";
        string Row = "Pr";
        string Rack = "Pra";
        string Shelf = "Prs";

        string Rowid = DBAccess.FetchDatasingle("select Top 1 Rowid from tbl_RackStockInDetails where ItemId='" + ItemId + "' and BranchId='" + BranchId + "' and ProjectId='" + ToProjectId + "' order by Rowid asc");

                
        // bool UpdateMainQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty+'" + QtyT + "' where ItemId='" + ItemId + "' and BranchId='" + BranchId + "' and ProjectId='" + ToProjectId + "' and RowId='" + Rowid + "'");

        bool Save = DBAccess.SaveData(@"insert into [dbo].[tbl_TransferStock] values ('" + TransferId + "',convert(datetime,'" + TransferDate + "',103), '" + FromProjectId + "', '" + FromProject + "', '" + ItemId + "', '" + ItemName + "', '" + QtyT + "', '" + ToProjectId + "','" + ToProject + "', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");

        bool SaveAct = DBAccess.SaveData(@"insert into [dbo].[tbl_RackStockInDetails] values ('" + ActualStockInId + "', '" + StockInId + "', '" + GRNNo + "','" + ItemId + "','" + QtyT + "','" + QtyT + "','" + RackSpace + "','" + Row + "','" + Rack + "', '" + Shelf + "','" + ItemUnit + "', '" + TotalItemUnit + "', '', '" + ToProjectId + "', '" + ToProject + "', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");


        if (Save == true)
        {
            if (ViewState["TempBarcode"] != null)
            {
                DataTable dt1 = ViewState["TempBarcode"] as DataTable;
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        string SrBarVodeID = dt1.Rows[j]["SrBarVodeID"].ToString();
                        string Barcode = dt1.Rows[j]["Barcode"].ToString();
                        string ActualStockInDetailsId = DBAccess.FetchDatasingle("select ActualStockInDetailsId from tbl_RackStockInBarCodeDetails where SrBarVodeID='" + SrBarVodeID + "' and Barcode='" + Barcode + "' and BranchID='" + BranchId + "' and ProjectId = '" + FromProjectId + "'");
                        string ActualStockInId1 = DBAccess.FetchDatasingle("select ActualStockInId from tbl_RackStockInBarCodeDetails where SrBarVodeID='" + SrBarVodeID + "' and Barcode='" + Barcode + "' and BranchID='" + BranchId + "' and ProjectId = '" + FromProjectId + "'");
                        string StockInIdA = DBAccess.FetchDatasingle("select StockInId from tbl_RackStockInBarCodeDetails where SrBarVodeID='" + SrBarVodeID + "' and Barcode='" + Barcode + "' and BranchID='" + BranchId + "' and ProjectId = '" + FromProjectId + "'");
                       
                        string ItemId1 = dt1.Rows[j]["ItemId"].ToString();
                        decimal Qty = 1;
                        string ProcessId = ddlRackSpace.Value.ToString();
                        string Row1 = dt1.Rows[j]["Row"].ToString();
                        string Rack1 = dt1.Rows[j]["Rack"].ToString();
                        string Shelf1 = dt1.Rows[j]["Shelf"].ToString();
                        string Warranty = dt1.Rows[j]["Warranty"].ToString();
                        string WarrantyTo = dt1.Rows[j]["WarrantyTo"].ToString();
                        string CoderLifeTo = dt1.Rows[j]["CoderLifeTo"].ToString();



                        bool SaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_RackTransferCodeDetails] values ('" + TransferId + "','" + ActualStockInId1 + "','" + ActualStockInDetailsId + "','" + StockInId + "','" + SrBarVodeID + "', '" + ItemId1 + "', '" + Barcode + "','" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");
                        bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set ActualStockInId = '" + ActualStockInId + "', StockInId='" + StockInId + "', ProcessId='" + RackSpace + "',  Row='" + Row + "' , Rack='" + Rack + "' , Shelf= '" + Shelf + "' , ProjectId='" + ToProjectId + "', ProjectName= '" + ToProject + "'  where SrBarVodeID='" + SrBarVodeID + "' and BranchId='" + BranchId + "' and StockInId='" + StockInIdA + "' ");

                        // bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set ProjectId='" + ToProjectId + "', ProjectName= '" + ToProject + "' where SrBarVodeID='" + SrBarVodeID + "' and BranchId='" + BranchId + "' and StockInId='" + StockInId + "' and  ProjectId = '" + FromProjectId + "'");

                        bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty-'" + Qty + "' where ItemId='" + ItemId1 + "' and BranchId='" + BranchId + "' and StockInId='" + StockInIdA + "'");
                    }
                }
                ViewState["TempBarcode"] = null;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Transfer Done successfully.'); window.location='../Project/StockTransfer.aspx';", true);
            }
        }

        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Transfer Not Done');", true);
            return;
        }
    }

}