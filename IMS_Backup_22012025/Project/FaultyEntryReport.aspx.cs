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

public partial class Project_FaultyEntryReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gv_FaultyEntryDetails_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string ID = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        string FaultyId = "";
        try
        {
            FaultyId = ID.Split(',')[0].ToString();
        }
        catch { }

        try
        {
            string Qry = "select a.FaultyId,a.FaultyDeId,a.FaultyDate,a.ItemId, IM.ItemName,IM.Make, IM.Model,a.SrBarVodeId,a.Barcode from tbl_FaultyEntryDetails a join tbl_ItemMaster IM ON IM.ItemId=a.ItemId where FaultyId='" + FaultyId + "'";
            SqlDataSource3.SelectCommand = Qry;
        }
        catch
        {

        }

    }
    
}