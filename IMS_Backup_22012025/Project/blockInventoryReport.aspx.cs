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

public partial class Project_blockInventoryReport : System.Web.UI.Page
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
}