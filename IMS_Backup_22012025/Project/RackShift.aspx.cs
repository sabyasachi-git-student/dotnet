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
public partial class Project_RackShift : System.Web.UI.Page
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

            string UserId = "";
            string BranchId = "";

            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
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
    protected void ddlRackSpace_TextChanged(object sender, EventArgs e)
    {
        string ItemId = ddlItem.Value.ToString();
        string BranchId = Session["BranchId"].ToString();

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
        string RackQty = DBAccess.FetchDatasingle("select  sum(qty) as Qty  from tbl_RackStockInDetails  where RackSpace='" + RackSpace + "' and BranchId ='" + BranchId + "'");
        string RackSpaceQty = DBAccess.FetchDatasingle("select  4 - sum(TotalItemUnit) as Qty  from tbl_RackStockInDetails where RackSpace='" + RackSpace + "' and BranchId ='" + BranchId + "' ");

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

    protected void txtRackQty_TextChanged(object sender, EventArgs e)
    {
        decimal Qty = Convert.ToDecimal(txtRackQty.Text);
        decimal StockQty = Convert.ToDecimal(txtStockQty.Text);
        decimal SpaceUnit = Convert.ToDecimal(lblItemSpaceUnit.Text);

        if (Qty > StockQty)
        {
            txtRackQty.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! More Than Sotck Qty');", true);
            return;
        }

        decimal Total = Qty * SpaceUnit;
        lblTotalSpaceUnit.Text = Total.ToString();


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

        trItemUnit.Visible = true;
        //string StockInId = ddlStockInId.Value.ToString();
        string ItemId = ddlItem.Value.ToString();
        Session["ItemId"] = ItemId;
        DataTable StockInQty1 = DBAccess.FetchDatatable("SELECT rsd.ItemId,im.ItemName,im.Make,im.Model,rsd.RackSpace,rsd.Row,rsd.Rack,rsd.Shelf, Sum(rsd.ItemUnit) as ItemUnit, Sum(a.Qty) as Qty FROM tbl_RackStockInDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID  join tbl_RackStockInBarCodeDetails a on a.ActualStockInId=rsd.ActualStockInId where rsd.BranchId='" + BranchId + "'  and (a.Status1='' or a.Status1 is null)and (a.Status2='' or a.Status2 is null) and (a.Status3='' or a.Status3 is null) and (a.Status4='' or a.Status4 is null)  group by rsd.ItemId,im.ItemName,im.Make,im.Model,rsd.RackSpace,rsd.Row,rsd.Rack,rsd.Shelf");
        DataTable RackQty1 = DBAccess.FetchDatatable("select ab.Stockinid, ab.ItemId, Sum(ab.Qty) as a FROM tbl_RackStockInDetails ab  where ab.ItemId='" + ItemId + "' group By ab.Stockinid, ab.ItemId");
        if (RackQty1 != null && RackQty1.Rows.Count > 0)
        {
            string Qty = RackQty1.Rows[0]["a"].ToString();
            decimal StockInQty = Convert.ToDecimal(StockInQty1.Rows[0]["Qty"].ToString());
            decimal RackQty = Convert.ToDecimal(Qty);
            decimal RestQty = StockInQty - RackQty;
            //txtStockQty.Text = StockInQty.ToString();
            //txtRackQtyEnter.Text = RestQty.ToString();
        }
        else
        {
            txtStockQty.Text = StockInQty1.Rows[0]["Qty"].ToString();
            //txtRackQtyEnter.Text = StockInQty1.Rows[0]["Qty"].ToString();
            ddlRackSpace.Text = "";
            txtAvailableQty.Text = "";
            lblItemSpaceUnit.Text = "";
        }
        string ItemSpaceUnit = DBAccess.FetchDatasingle("select SpaceUnit from tbl_ItemMaster where  ItemId='" + ItemId + "' ");
        lblItemSpaceUnit.Text = ItemSpaceUnit.ToString();

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

    protected void btn_AddConPers_Click(object sender, EventArgs e)
    {
        string StockInId = "";
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


        decimal RackSpaceUnit = Convert.ToDecimal(txtRackSpace.Text);


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
        }

        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please select Proper Qty Barcode');", true);
            return;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("RackShift.aspx");
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
        string ProcessIdOld = ASPxComboBox1.Value.ToString();

        if (ViewState["TempStockIn"] != null)
        {
            DataTable dt = ViewState["TempStockIn"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string GRNNo = "";
                    string Status = "";
                    string ProjectId = ASPxComboBox2.Value.ToString();
                    string ProjectName = ASPxComboBox2.Text;
                    string FromRack = ASPxComboBox1.Value.ToString();

                    string ActualStockInId = DBAccess.FetchDatatable("select  [dbo].[fn_ActualStockInId]()").Rows[0][0].ToString();
                    string Row = dt.Rows[i]["Row"].ToString();
                    string Rack = dt.Rows[i]["Rack"].ToString();
                    string Shelf = dt.Rows[i]["Shelf"].ToString();
                    string StockInId = DBAccess.FetchDatatable("select  [dbo].[fn_RackStockInId]()").Rows[0][0].ToString();
                    string ItemId = dt.Rows[i]["ItemId"].ToString();
                    decimal RackQty = Convert.ToDecimal(dt.Rows[i]["RackQty"].ToString());
                    decimal AvailableQty = RackQty;
                    string RackSpace = dt.Rows[i]["RackSpace"].ToString();
                    decimal ItemUnit = Convert.ToDecimal(lblItemSpaceUnit.Text);
                    decimal TotalItemUnit = Convert.ToDecimal(lblTotalSpaceUnit.Text);

                    if (gv_Barc.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please select Item Assign');", true);
                        return;
                    }

                    bool Save = DBAccess.SaveData(@"insert into [dbo].[tbl_RackStockInDetails] values ('" + ActualStockInId + "', '" + StockInId + "', '" + GRNNo + "','" + ItemId + "','" + RackQty + "','" + AvailableQty + "','" + RackSpace + "','" + Row + "','" + Rack + "', '" + Shelf + "','" + ItemUnit + "', '" + TotalItemUnit + "', '" + Status + "', '" + ProjectId + "', '" + ProjectName + "', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");


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
                                    string StockInIdA = dt1.Rows[j]["StockInId"].ToString();


                                    bool SaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_RackShiftStockInBarCodeDetails] values ('" + ActualStockInId + "','" + ActualStockInDetailsId + "','" + StockInIdA + "','" + SrBarVodeID + "', '" + ItemId1 + "', '" + Barcode + "','','" + Qty + "','" + ProcessId + "','" + Row1 + "','" + Rack1 + "', '" + Shelf1 + "', '" + Warranty + "',convert(datetime,'" + WarrantyTo + "',103),convert(datetime,'" + CoderLifeTo + "',103),'','','','','" + FromRack + "','','','','','', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");

                                    bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set ActualStockInId = '" + ActualStockInId + "', ActualStockInDetailsId = '" + ActualStockInDetailsId + "', StockInId='" + StockInId + "', ProcessId='" + ProcessId + "',  Row='" + Row1 + "' , Rack='" + Rack1 + "' , Shelf= '" + Shelf1 + "' where SrBarVodeID='" + SrBarVodeID + "' and BranchId='" + BranchId + "' and StockInId='" + StockInIdA + "'  and ProjectId='" + ProjectId + "'");

                                    bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty-'" + Qty + "' where ItemId='" + ItemId1 + "' and BranchId='" + BranchId + "' and StockInId='" + StockInIdA + "' and RackSpace='" + ProcessIdOld + "' and ProjectId='" + ProjectId + "'");


                                }
                            }
                            ViewState["TempBarcode"] = null;

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Rack Shifting Save successfully.'); window.location='../Project/RackShift.aspx';", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Stock In Not Done');", true);
                        return;
                    }
                }
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Add Above Item');", true);
                return;
            }
        }
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
        string ItemId = ddlItem.Value.ToString();
        string ProcessId = ASPxComboBox1.Value.ToString();
        Session["ProcessId"] = ProcessId;
        Session["ItemId"] = ItemId;


    }

    protected void ASPxComboBox2_TextChanged(object sender, EventArgs e)
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
        string ItemId = ddlItem.Value.ToString();
        string ProcessId = ASPxComboBox1.Value.ToString();
        string ProjectId = ASPxComboBox2.Value.ToString();
        Session["ProjectId"] = ProjectId;

        DataTable rackShelf = DBAccess.FetchDatatable("SELECT rsd.ItemId,im.ItemName,im.Make,im.Model,rsd.GRNNo,rsd.ItemId,rsd.RackSpace,rsd.Row,rsd.Rack,rsd.Shelf, rsd.ProjectId, rsd.ProjectName, rsd.ItemUnit,rsd.BranchId FROM tbl_RackStockInDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID where rsd.BranchId='" + BranchId + "' and rsd.RackSpace='" + ProcessId + "'");
        DataTable StockInQty1 = DBAccess.FetchDatatable("SELECT rsd.ItemId,im.ItemName,im.Make,im.Model,rsd.RackSpace,rsd.Row,rsd.Rack,rsd.Shelf, Sum(rsd.ItemUnit) as ItemUnit, (select Sum(Qty) from tbl_RackStockInBarCodeDetails where BranchId='" + BranchId + "' and ProjectId='" + ProjectId + "' and ProcessId='" + ProcessId + "' and ItemId='" + ItemId + "') as Qty FROM tbl_RackStockInDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID  join tbl_RackStockInBarCodeDetails a on a.ActualStockInId=rsd.ActualStockInId where rsd.BranchId='" + BranchId + "' and rsd.ProjectId='" + ProjectId + "' and rsd.RackSpace='" + ProcessId + "' and rsd.ItemId='" + ItemId + "' and (a.Status1='' or a.Status1 is null)and (a.Status2='' or a.Status2 is null) and (a.Status3='' or a.Status3 is null) and (a.Status4='' or a.Status4 is null)  group by rsd.ItemId,im.ItemName,im.Make,im.Model,rsd.RackSpace,rsd.Row,rsd.Rack,rsd.Shelf");
        DataTable RackQty1 = DBAccess.FetchDatatable("select ab.Stockinid, ab.ItemId, Sum(ab.Qty) as a FROM tbl_RackStockInDetails ab  where ab.ItemId='" + ItemId + "' group By ab.Stockinid, ab.ItemId");
        if (RackQty1 != null && RackQty1.Rows.Count > 0)
        {
            string Qty = RackQty1.Rows[0]["a"].ToString();
            decimal StockInQty = Convert.ToDecimal(StockInQty1.Rows[0]["Qty"].ToString());
            decimal RackQty = Convert.ToDecimal(Qty);
            decimal RestQty = StockInQty - RackQty;
            txtStockQty.Text = StockInQty.ToString();
            txtRow.Text = rackShelf.Rows[0]["Row"].ToString();
            txtRack.Text = rackShelf.Rows[0]["Rack"].ToString();
            txtShelf.Text = rackShelf.Rows[0]["Shelf"].ToString();

            //txtRackQtyEnter.Text = RestQty.ToString();
        }
        else
        {
            txtStockQty.Text = StockInQty1.Rows[0]["Qty"].ToString();
            //txtRackQtyEnter.Text = StockInQty1.Rows[0]["Qty"].ToString();
            ddlRackSpace.Text = "";
            txtAvailableQty.Text = "";
            lblItemSpaceUnit.Text = "";
        }
        string ItemSpaceUnit = DBAccess.FetchDatasingle("select SpaceUnit from tbl_ItemMaster where  ItemId='" + ItemId + "' ");
        lblItemSpaceUnit.Text = ItemSpaceUnit.ToString();
    }
}