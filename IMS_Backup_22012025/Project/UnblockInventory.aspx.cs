using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraPrinting;

public partial class Project_UnblockInventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string UserId = Session["UserId"].ToString();
            string BranchId = Session["BranchId"].ToString();
        }
    }

    protected void gv_EstRawMat_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string ID = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        string ReqSecId = "";
        try
        {
            ReqSecId = ID.Split(',')[0].ToString();
        }
        catch { }

        try
        {
            string Qry = "select a.BlockId,a.barcode,a.ItemId, b.* from tbl_blockInventoryBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.BlockId='" + ReqSecId + "'";
            SqlDataSource3.SelectCommand = Qry;
        }
        catch
        {

        }

    }
    protected void gv_Estimation_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        object BlockIdId = e.KeyValue;
        string BlockId = BlockIdId.ToString();
        Session["SaleInvoiceNo"] = BlockId;
        txtBlockId.Text = BlockId;
        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        if (e.CommandArgs.CommandName == "Cancel")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "lightbox_open('id03B')", true);
        }
    }
    protected void btnUmblockBarcode_Click(object sender, EventArgs e)
    {
        string BlockId = txtBlockId.Text;
        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        List<object> fieldValues = ASPxGridView2.GetSelectedFieldValues(new string[] { "SrBarVodeID" });

        if (fieldValues.Count != 0)
        {
            for (int i = 0; i < fieldValues.Count; i++)
            {
                GridViewDataColumn CC = ASPxGridView2.Columns[4] as GridViewDataColumn;
                GridViewDataColumn BB = ASPxGridView2.Columns[5] as GridViewDataColumn;
                Label GridSrBarVodeID = ASPxGridView2.FindRowCellTemplateControl(i, CC, "lblSrBarVodeID") as Label;
                Label GridBarcode = ASPxGridView2.FindRowCellTemplateControl(i, BB, "lblBarcode") as Label;
                string SrBarVodeID = GridSrBarVodeID.Text;
                string Barcode = GridBarcode.Text;

                DataTable dt = DBAccess.FetchDatatable(@"Select * from tbl_blockInventoryDetails where BlockId='" + BlockId + "'");
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    string ItemId = dt.Rows[a]["ItemId"].ToString();
                    string ItemName = dt.Rows[a]["ItemName"].ToString();
                    decimal Qty = Convert.ToDecimal(dt.Rows[a]["Qty"].ToString());
                    string Status = "Unblock";
                    DateTime UnblockDOE = DateTime.Now;
                    decimal Qtyb = 1;

                    bool UpdateSemiStock = DBAccess.SaveData("Update tbl_blockInventoryDetails set  UnblockUser='" + UserId + "',  UnblockDOE= convert(datetime,'" + UnblockDOE + "',103) where BlockId='" + BlockId + "' and BranchId='" + BranchId + "' ");

                    string StockInId = DBAccess.FetchDatasingle(@"Select StockInId from [tbl_blockInventoryBarCodeDetails]   where Barcode='" + Barcode + "' and BlockId='" + BlockId + "' and BranchId='" + BranchId + "'");

                    bool SaveItem = DBAccess.SaveData("update [tbl_blockInventoryBarCodeDetails] set Status= 'Unblock'  where BLockId='" + BlockId + "' and  Barcode='" + Barcode + "' and BranchId='" + BranchId + "'");

                    bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set Status1='', Status2='', Status3='', Status4='', Qty='1' where Barcode='" + Barcode + "'  and BranchId='" + BranchId + "' and StockInId='" + StockInId + "'");

                    bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty+'" + Qtyb + "' where ItemId='" + ItemId + "'  and BranchId='" + BranchId + "' and StockInId='" + StockInId + "'");

                    if (UpdateSemiStock == true && SaveItem == true && UpdateBarcode == true && UpdateQty == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('block Inventory Done successfully.'); window.location='../Project/UnblockInventory.aspx';", true);

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('block Inventory Not Done');", true);
                        return;
                    }
                }


            }
        }

    }
}