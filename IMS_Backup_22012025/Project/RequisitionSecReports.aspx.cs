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
public partial class Project_RequisitionSecReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
            string Qry = "select a.ReqSecId,a.ItemId,a.Qty,b.* from tbl_RequisitionSectionDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where ReqSecId='" + ReqSecId + "'";
            SqlDataSource3.SelectCommand = Qry;
        }
        catch
        {

        }

    }
    protected void gv_Estimation_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;


        string ReqSecId = e.GetValue("ReqSecId").ToString();

        string dt1 = DBAccess.FetchDatasingle(@"select Status6 from tbl_RequisitionSection where ReqSecId='" + ReqSecId + "'");
        if (dt1 == "Approve")
        {
            e.Row.BackColor = Color.LightGreen;
        }
        if (dt1 == "Reject")
        {
            e.Row.BackColor = Color.LightCoral;
        }
        if (dt1 == "")
        {
            e.Row.BackColor = Color.White;
        }


    }
}