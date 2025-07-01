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
using System.Collections;

public partial class Project_FaultyEntry : System.Web.UI.Page
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
            try
            {
                UserId = Session["UserId"].ToString();
                
                string UserName = DBAccess.FetchDatasingle("select ug.UserGroup from tbl_User u join tbl_UserGroups ug on u.UserGroup=ug.UserGroupId  where UserName='" + UserId + "'");
                if (UserName == "SuperAdmin" || BranchId =="BR102")
                {
                    txtFaultyDate.Date = DateTime.Now;
                    row.Visible = false;

                }
                else
                {
                    txtFaultyDate.Date = DateTime.Now;
                    txtFaultyDate.Enabled = false;
                }

                BranchId = Session["BranchId"].ToString();

                if (BranchId == "BR102")
                {
                    row.Visible = false;

                }
                else
                {
                    row.Visible = true;
                }
            }
            catch { }
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
        dtCol.ColumnName = "SrBarVodeID";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Barcode";
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
        dtCol.ColumnName = "Row";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Rack";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Shelf";
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
        dtCol.ColumnName = "HSNCode";
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
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("FaultyEntry.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FaultyId = DBAccess.FetchDatatable("select [dbo].[fn_FaultyId]()").Rows[0][0].ToString();

        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;


        string FaultyDate = txtFaultyDate.Text;
        string Barcode = txtBarcode.Text;
        string Remarks = "";
        try
        {
            Remarks = txtRemarks.Text;
        }
        catch { }
        string FaultyReason = txtFaultyReason.Text;
        string FaultyReId = txtFaultyReason.SelectedItem.Value.ToString();
        string ReBranchId = txtRegion.Text;

        int n = ClassFaultyEntry.Save(FaultyId, FaultyDate, Barcode, Remarks, FaultyReId, FaultyReason, UserId, BranchId, DOE);
        for (int i = 0; i < gvItemDetails.Rows.Count; i++)
        {
            string FaultyDeId = DBAccess.FetchDatatable("select [dbo].[fn_FaultyDeId]()").Rows[0][0].ToString();
            decimal Qtyb = 1;
            string SrBarVodeID = gvItemDetails.Rows[i].Cells[2].Text.ToString();
            string ItemId = gvItemDetails.Rows[i].Cells[1].Text.ToString();
            string barcode = gvItemDetails.Rows[i].Cells[3].Text.ToString();

            string Row = gvItemDetails.Rows[i].Cells[5].Text.ToString();
            string Rack = gvItemDetails.Rows[i].Cells[6].Text.ToString();
            string Shelf = gvItemDetails.Rows[i].Cells[7].Text.ToString();


            int n1 = ClassFaultyEntry.SaveDetails(FaultyId, FaultyDeId, FaultyDate, ItemId, SrBarVodeID, barcode, UserId, BranchId, DOE);


            bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set Status1='Faulty', Qty='0' , Status2='Faulty' where SrBarVodeID='" + SrBarVodeID + "' and Barcode='" + barcode + "' and Row='" + Row + "' and Rack='" + Rack + "' and Shelf= '" + Shelf + "' and BranchId='" + BranchId + "'");

            bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty-'" + Qtyb + "' where ItemId='" + ItemId + "' and BranchId='" + BranchId + "' and Row='" + Row + "' and Rack='" + Rack + "' and Shelf= '" + Shelf + "'");
        }
        if (n == 1)
        {
            if (chk.Checked == true)
            {
                bool UpdateBranch = DBAccess.SaveData(@"update tbl_FaultyEntry set BranchId='" + ReBranchId + "', ReBranchId = '" + BranchId + "' where  FaultyId='" + FaultyId + "'");
            }

            txtCode.Text = FaultyId;
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id04').style.display='block'", true);

           
        }
        else if (n == -1)
        {

            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Faulty Entry is already exist.');", true);
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Faulty Entry is not saved. Try again.');", true);
            return;
        }

    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        string Barcode = txtBarcode.Text;

        DataTable dtI = (DataTable)ViewState["ItemDetails"];
        DataTable dt11 = DBAccess.FetchDatatable("SELECT * FROM tbl_RackStockInBarCodeDetails SIB LEFT JOIN tbl_ItemMaster IM ON IM.ItemId=SIB.ItemId WHERE SIB.Barcode='" + Barcode + "' and SIB.BranchID='" + BranchId + "' and (Status1='' or status1 is null) and (Status1='' or status2 is null)");
        if (dt11 != null && dt11.Rows.Count > 0)
        {
            DataRow drH = dtI.NewRow();

            drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
            drH["SrBarVodeID"] = dt11.Rows[0]["SrBarVodeID"].ToString();
            drH["Barcode"] = dt11.Rows[0]["Barcode"].ToString();
            drH["Category"] = dt11.Rows[0]["Category"].ToString();
            drH["Type"] = dt11.Rows[0]["Type"].ToString();
            drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
            drH["Make"] = dt11.Rows[0]["Make"].ToString();
            drH["Model"] = dt11.Rows[0]["Model"].ToString();
            drH["Unit"] = dt11.Rows[0]["Unit"].ToString();
            drH["HSNCode"] = dt11.Rows[0]["HSNCode"].ToString();
            drH["Row"] = dt11.Rows[0]["Row"].ToString();
            drH["Rack"] = dt11.Rows[0]["Rack"].ToString();
            drH["Shelf"] = dt11.Rows[0]["Shelf"].ToString();

            dtI.Rows.Add(drH);

            ViewState["ItemDetails"] = dtI;
            gvItemDetails.DataSource = dtI;
            gvItemDetails.DataBind();
        }
        else
        {
            txtBarcode.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Barcode Not Available.');", true);
            return;
        }
        txtBarcode.Text = "";
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (chk.Checked)
        {
            //txtRegion.Visible = true;
            txtRegion.Text = "BR102";
        }
        else
        {
            txtRegion.Text = null;
            //txtRegion.Visible = false;
        }
       
    }


    protected void btnCodeSerch_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Faulty Entry is saved successfully..');window.location='../Project/FaultyEntry.aspx';", true);
    }
}

