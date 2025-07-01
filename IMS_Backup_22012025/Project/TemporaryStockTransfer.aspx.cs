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

public partial class Project_TemporaryStockTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItem();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("TemporaryStockTransfer.aspx");
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

    protected void gvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemDetails.PageIndex = e.NewPageIndex;
        this.gvItemDetails.DataSource = (DataTable)ViewState["ItemDetails"];
        this.gvItemDetails.DataBind();
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
    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        string Barcode = txtBarcode.Text;

        if (Barcode != "")
        {
            DataTable AppQtn = new DataTable();

            DataTable dt8 = (DataTable)ViewState["ItemDetails"];
            if (dt8 != null && dt8.Rows.Count > 0)
            {
                for (int i = 0; i < dt8.Rows.Count; i++)
                {
                    if (Barcode == dt8.Rows[i]["Barcode"].ToString())
                    {
                        txtBarcode.Text = "";
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);
                        // ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
                        return;
                    }
                }
            }


            DataTable dtI = (DataTable)ViewState["ItemDetails"];
            DataTable dt11 = DBAccess.FetchDatatable("SELECT * FROM tbl_RackStockInBarCodeDetails SIB LEFT JOIN tbl_ItemMaster IM ON IM.ItemId=SIB.ItemId WHERE SIB.Barcode='" + Barcode + "'");
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

                dtI.Rows.Add(drH);

                ViewState["ItemDetails"] = dtI;
                gvItemDetails.DataSource = dtI;
                gvItemDetails.DataBind();
            }
            txtBarcode.Text = "";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string TransferDate = dtpTransferDate.Text;
        string TransferTo = ddlTransferTo.Text;

        if(TransferDate=="")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Date');", true);
            return;
        }
        if (TransferTo == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Transfer To');", true);
            return;
        }

        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Stock Transfer Successfully.');window.location='../Project/TemporaryStockTransfer.aspx';", true);
    }

}