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

public partial class Project_RequisitionNOCReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gv_EstRawMat_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string ID = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        string ReqNOCId = "";
        try
        {
            ReqNOCId = ID.Split(',')[0].ToString();
        }
        catch { }

        try
        {
            string Qry = "select a.ReqNOCId,a.ItemId,a.Qty,b.* from tbl_RequisitionNOCDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where ReqNOCId='" + ReqNOCId + "'";
            SqlDataSource4.SelectCommand = Qry;
        }
        catch
        {

        }

    }
}