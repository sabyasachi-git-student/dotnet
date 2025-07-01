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
public partial class Project_blockInventory : System.Web.UI.Page
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
            Session["ItemIdStock"] = null;
            Session["ProjectId"] = null;

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
    protected void ddlProOp_TextChanged(object sender, EventArgs e)
    {
        string ProOp = ddlProOp.Text;
        if (ProOp == "ProjectWise")
        {
            txtAvilStock.Text = "";
            ddlProject.Visible = true;
            AddBarCode.Visible = true;
            txtAvilStock.Visible = true;
            lblstock.Visible = true;
            lblBlock.Visible = true;
            txtBlockStock.Visible = true;
            lbl1.Visible = true;

        }
        else
        {
            ddlProject.Visible = false;
            AddBarCode.Visible = true;
            txtAvilStock.Visible = true;
            lblstock.Visible = true;
            lblBlock.Visible = true;
            txtBlockStock.Visible = true;

            string BranchId = Session["BranchId"].ToString();
            string ItemId = Session["ItemIdStock"].ToString();

            string dt21 = DBAccess.FetchDatasingle("SELECT distinct (select sum(qty) from tbl_RackStockInBarCodeDetails where ItemId=rsd.ItemId and BranchId=rsd.BranchId and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as Qty FROM tbl_RackStockInDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID where rsd.BranchId='" + BranchId + "' and rsd.ItemId='" + ItemId + "'");

            if (dt21 == "")
            {
                txtAvilStock.Text = "0";
            }
            else
            {
                txtAvilStock.Text = dt21.ToString();
            }

            string dt21A = DBAccess.FetchDatasingle("SELECT count ( bb.barcode) as BlockQty FROM tbl_RackStockInDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID    join tbl_RackStockInBarCodeDetails bb on rsd.ActualStockInId=bb.ActualStockInId join tbl_StockIn s on rsd.StockInId=s.StockInId where rsd.BranchId='" + BranchId + "'   and rsd.ItemId='" + ItemId + "' and bb.Status1 ='ItemBlock' and bb.Status2 ='ItemBlock' and bb.Status3 ='ItemBlock' and bb.Status4 ='ItemBlock'");

            if (dt21A == "")
            {
                txtBlockStock.Text = "0";
            }
            else
            {
                txtBlockStock.Text = dt21A.ToString();
            }
        }
    }
    protected void ASPxComboBox1_TextChanged(object sender, EventArgs e)
    {
        string BranchId = Session["BranchId"].ToString();
        string Project = ddlProject.Value.ToString();
        Session["ProjectId"] = Project;
        string ItemId = Session["ItemIdStock"].ToString();

        string dt21 = DBAccess.FetchDatasingle("SELECT Distinct (select sum(qty) from tbl_RackStockInBarCodeDetails where ProjectId=rsd.ProjectId and ItemId=rsd.ItemId and (Status1 is null or Status1 ='' ) and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='') and ProjectId=rsd.ProjectId) as Qty,rsd.ProjectId,rsd.ProjectName FROM tbl_RackStockInDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID  where rsd.BranchId='" + BranchId + "'  and rsd.ProjectId='" + Project + "' and rsd.ItemId='" + ItemId + "'");

        if (dt21 == "")
        {
            txtAvilStock.Text = "0";
        }
        else
        {
            txtAvilStock.Text = dt21.ToString();
        }

        string dt21A = DBAccess.FetchDatasingle("SELECT count ( bb.barcode) as BlockQty FROM tbl_RackStockInDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID    join tbl_RackStockInBarCodeDetails bb on rsd.ActualStockInId=bb.ActualStockInId join tbl_StockIn s on rsd.StockInId=s.StockInId where rsd.BranchId='" + BranchId + "'  and s.ProjectId='" + Project + "' and rsd.ItemId='" + ItemId + "' and bb.Status1 ='ItemBlock' and bb.Status2 ='ItemBlock' and bb.Status3 ='ItemBlock' and bb.Status4 ='ItemBlock'");

        if (dt21A == "")
        {
            txtBlockStock.Text = "0";
        }
        else
        {
            txtBlockStock.Text = dt21A.ToString();
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("blockInventory.aspx");
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

        string BlockId = DBAccess.FetchDatatable("select [dbo].[fn_BlockId]()").Rows[0][0].ToString();
        string BlockDate = dtpDate.Text;
        string Remarks = txtRemarks.Text;
        string BlockOption = ddlProOp.SelectedValue.ToString();
        string ProjectId = "";
        try
        {
            ProjectId = ddlProject.Value.ToString();
        }
        catch
        {

        }
        string ProjectName = ddlProject.Text;

        if (gv_Barc.Rows.Count == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please verify Barcode First');", true);
            return;
        }

        for (int i = 0; i < gvItemDetails.Rows.Count; i++)
        {
            string ItemId = gvItemDetails.Rows[i].Cells[1].Text;
            string Category = gvItemDetails.Rows[i].Cells[2].Text;
            string ItemName = gvItemDetails.Rows[i].Cells[4].Text;
            string Make = gvItemDetails.Rows[i].Cells[5].Text;
            string Model = gvItemDetails.Rows[i].Cells[6].Text;
            string Unit = gvItemDetails.Rows[i].Cells[7].Text;

            TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
            TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
            decimal Qty = Convert.ToDecimal(txtItemQty.Text);
            decimal Qty1 = Convert.ToDecimal(txtRate.Text);

            bool Save = DBAccess.SaveData(@"insert into [dbo].[tbl_blockInventoryDetails] values ('" + BlockId + "', convert(datetime,'" + BlockDate + "',103),'" + Remarks + "', '" + BlockOption + "', '" + ProjectId + "', '" + ProjectName + "', '" + ItemId + "', '" + ItemName + "', '" + Qty + "','','','','', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");

            if (Save == true)
            {
                if (ViewState["TempBarcode"] != null)
                {
                    DataTable dt1 = ViewState["TempBarcode"] as DataTable;
                    for (int a = 0; a < gv_Barc.Rows.Count; a++)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++, a++)
                        {
                            string ItemId1 = dt1.Rows[j]["ItemId"].ToString();
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
                           
                            string ProcessId = dt1.Rows[j]["ProcessId"].ToString();
                            string Row1 = dt1.Rows[j]["Row"].ToString();
                            string Rack1 = dt1.Rows[j]["Rack"].ToString();
                            string Shelf1 = dt1.Rows[j]["Shelf"].ToString();
                            string Warranty = dt1.Rows[j]["Warranty"].ToString();
                            string WarrantyTo = dt1.Rows[j]["WarrantyTo"].ToString();
                            string CoderLifeTo = dt1.Rows[j]["CoderLifeTo"].ToString();



                            bool SaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_blockInventoryBarCodeDetails] values ('" + BlockId + "', '" + StockInId + "','" + SrBarVodeID + "', '" + ItemId1 + "', '" + Barcode + "', '" + ProcessId + "','', '" + UserId + "','" + BranchId + "', convert(datetime,'" + DOE + "',103))");

                            bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set Status1='ItemBlock', Status2='ItemBlock', Status3='ItemBlock', Status4='ItemBlock', Qty='0' where SrBarVodeID='" + SrBarVodeID + "' and ProcessId='" + ProcessId + "' and Row='" + Row1 + "' and Rack='" + Rack1 + "' and Shelf= '" + Shelf1 + "' and BranchId='" + BranchId + "' and StockInId='" + StockInId + "'");

                            bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty-'" + Qtyb + "' where ItemId='" + ItemId1 + "' and RackSpace='" + ProcessId + "' and Row='" + Row1 + "' and Rack='" + Rack1 + "' and Shelf= '" + Shelf1 + "' and BranchId='" + BranchId + "' and StockInId='" + StockInId + "'");

                        }
                        ViewState["TempBarcode"] = null;
                    }


                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Material Blocked Successfully.');window.location='../Project/blockInventory.aspx';", true);

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Material Block Not Done. Try again.');", true);
                return;
            }
        }

    }

    protected void btnChooseRequisition_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);

    }
    protected void btnAddServiceItem_Click(object sender, EventArgs e)
    {
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        string ItemId = "";
        List<object> fieldValues = gvItem.GetSelectedFieldValues(new string[] { "ItemId" });
        if (fieldValues.Count == 1)
        {
            DataTable AppQtn = new DataTable();

            for (int j = 0; j < fieldValues.Count; j++)
            {
                ItemId = fieldValues[j].ToString();
                txtReqPopAppId.Text = ItemId.ToString();

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
                DataTable dt11 = DBAccess.FetchDatatable("select Distinct a.ItemId, b.Category, b.ItemName, b.Make, b.Model, b.Unit, '' as Qty from tbl_RackStockInBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.BranchId='" + BranchId + "' and (a.Status1 is null or a.Status1 ='') and (a.Status2 is null or a.Status2 ='') and (a.Status3 is null or a.Status3 ='') and (a.Status4 is null or a.Status4 ='') and a.ItemId = '" + ItemId + "'");
                if (dt11.Rows.Count > 0)
                {
                    for (int i = 0; i < dt11.Rows.Count; i++)
                    {
                        DataRow drH = dtI.NewRow();
                        drH["ItemId"] = dt11.Rows[i]["ItemId"].ToString();
                        Session["ItemIdStock"] = drH["ItemId"].ToString();
                        drH["Category"] = dt11.Rows[i]["Category"].ToString();
                        drH["ItemName"] = dt11.Rows[i]["ItemName"].ToString();
                        drH["Make"] = dt11.Rows[i]["Make"].ToString();
                        drH["Model"] = dt11.Rows[i]["Model"].ToString();
                        drH["Unit"] = dt11.Rows[i]["Unit"].ToString();
                        drH["Qty"] = dt11.Rows[i]["Qty"].ToString();

                        dtI.Rows.Add(drH);

                        ViewState["ItemDetails"] = dtI;
                        gvItemDetails.DataSource = dtI;
                        gvItemDetails.DataBind();
                        btnStockOut.Visible = false;
                        TrIssueOption.Visible = true;
                        TrIssueOption1.Visible = true;


                    }
                }

            }
            gvItem.Selection.UnselectAll();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Not More Than One Item');", true);
        }
    }
    protected void AddBarCode_Click(object sender, EventArgs e)
    {
        btnStockOut.Visible = false;
        ddlProOp.Enabled = false;
        ddlProject.Enabled = false;

        string Status = ddlProOp.Text;
        if (Status == "GeneralStock")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03B').style.display='block'", true);
        }
        else
        {
            #region  ProjectWiseIssue
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03A').style.display='block'", true);
            #endregion ProjectWiseIssue
        }
    }    
    protected void btnaddProject_Click(object sender, EventArgs e)
    {
        string RackSpace = "";
        try
        {
            RackSpace = ddlProject.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Project First');", true);
            return;
        }
        string StockInId = "";
        List<object> fieldValues = ASPxGridView1.GetSelectedFieldValues(new string[] { "SrBarVodeID" });
        decimal Qty = 0;
        for (int i = 0; i < gvItemDetails.Rows.Count; i++)
        {
            TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
            CheckBox CheckchbxSelect = (CheckBox)gvItemDetails.Rows[i].FindControl("chbxSelect");

            Qty = Convert.ToDecimal(txtItemQty.Text);
            //CheckchbxSelect.Checked = false;
        }


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
                    drH["ProcessId"] = dt11.Rows[0]["ProcessId"].ToString();
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
            ASPxGridView1.Selection.UnselectAll();
        }

        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please select Proper Qty Barcode');", true);
            return;
        }
    }
    protected void btnaddGenaral_Click(object sender, EventArgs e)
    {
        string StockInId = "";
        List<object> fieldValues = ASPxGridView2.GetSelectedFieldValues(new string[] { "SrBarVodeID" });
        decimal Qty = 0;
        for (int i = 0; i < gvItemDetails.Rows.Count; i++)
        {
            TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
            CheckBox CheckchbxSelect = (CheckBox)gvItemDetails.Rows[i].FindControl("chbxSelect");

            Qty = Convert.ToDecimal(txtItemQty.Text);
            //CheckchbxSelect.Checked = false;
        }


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
                    drH["ProcessId"] = dt11.Rows[0]["ProcessId"].ToString();
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
            ASPxGridView1.Selection.UnselectAll();
        }

        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please select Proper Qty Barcode');", true);
            return;
        }
    }
    protected void btnStockOut_Click(object sender, EventArgs e)
    {
        btnStockOut.Visible = false;
        ddlProOp.Enabled = false;
        ddlProject.Enabled = false;

        string Status = ddlProOp.Text;


        if (Status == "GeneralStock")
        {
            #region  LocationWiseIssue

            for (int a = 0; a < gvItemDetails.Rows.Count; a++)
            {
                decimal AvilStock = Convert.ToDecimal(txtAvilStock.Text);
                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[a].FindControl("txtItemQty");
                decimal IssueQty1 = 0;
                try
                {
                    IssueQty1 = Convert.ToDecimal(txtItemQty.Text);
                    Session["IssueQty"] = IssueQty1;
                    txtItemQty.Enabled = false;
                }

                catch
                {

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter Qty');", true);

                }
                Session["ItemId"] = gvItemDetails.Rows[a].Cells[1].Text;

                string ItemId = Session["ItemId"].ToString();
                decimal IssueQty = IssueQty1;
                string ProjectId = Session["BranchId"].ToString();


                if (AvilStock < IssueQty1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Qty More Than Avilable Stock');", true);
                }
                else
                {
                    DataTable dtItem = DBAccess.FetchDatatable(@"select distinct a.*,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate ,b.ItemName,b.Type as Type1,'' as EnterBarcode from tbl_RackStockInBarCodeDetails a join [tbl_RackStockInDetails] c on a.ActualStockInId=c.ActualStockInId join tbl_ItemMaster b on a.ItemId=b.ItemId      where a.ItemId='" + ItemId + "' and a.BranchId='" + ProjectId + "'  and a.Qty>0 and c.AvailableQty>0 and (Status1='' or status1 is null) order by a.Id desc");
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
            #endregion LocationWiseIssue
        }

        else
        {
            #region  ProjectWiseIssue

            for (int a = 0; a < gvItemDetails.Rows.Count; a++)
            {
                decimal AvilStock = Convert.ToDecimal(txtAvilStock.Text);
                TextBox txtItemQty = (TextBox)gvItemDetails.Rows[a].FindControl("txtItemQty");


                decimal IssueQty1 = 0;
                try
                {
                    IssueQty1 = Convert.ToDecimal(txtItemQty.Text);
                    Session["IssueQty"] = IssueQty1;
                    txtItemQty.Enabled = false;
                }

                catch
                {

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter Qty');", true);

                }


                Session["ItemId"] = gvItemDetails.Rows[a].Cells[1].Text;

                string ItemId = Session["ItemId"].ToString();
                decimal IssueQty = IssueQty1;
                string ProjectId = Session["BranchId"].ToString();
                String BrProjectId = "";
                try
                {
                    BrProjectId = ddlProject.Value.ToString();
                }
                catch
                {

                }
                string ProjectName = ddlProject.Text;

                if (AvilStock < IssueQty1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Qty More Than Avilable Stock');", true);
                }
                else
                {
                    DataTable dtItem = DBAccess.FetchDatatable(@"select distinct a.*,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate ,b.ItemName,b.Type as Type1,'' as EnterBarcode from tbl_RackStockInBarCodeDetails a join [tbl_RackStockInDetails] c on a.ActualStockInId=c.ActualStockInId join tbl_ItemMaster b on a.ItemId=b.ItemId where a.ItemId='" + ItemId + "' and a.BranchId='" + ProjectId + "' and a.ProjectId='" + BrProjectId + "' and a.ProjectName='" + ProjectName + "'  and a.Qty>0 and c.AvailableQty>0  and (a.Status1 is null or a.Status1 ='') and (a.Status2 is null or a.Status2 ='') and (a.Status3 is null or a.Status3 ='') and (a.Status4 is null or a.Status4 ='')  order by a.Id desc");
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
    }
}