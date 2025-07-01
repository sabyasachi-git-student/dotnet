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


public partial class Project_RequisitionPopReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gv_EstRawMat_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string ID = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        string ReqPopId = "";
        try
        {
            ReqPopId = ID.Split(',')[0].ToString();
        }
        catch { }

        try
        {
            string Qry = "select a.ReqPopId,a.ItemId,a.Qty,b.* from tbl_RequisitionPopDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where ReqPopId='" + ReqPopId + "'";
            SqlDataSource3.SelectCommand = Qry;
        }
        catch
        {

        }

    }
    protected void gv_Estimation_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;


        string ReqPopId = e.GetValue("ReqPopId").ToString();

        string dt1 = DBAccess.FetchDatasingle(@"select Status6 from tbl_RequisitionPop where ReqPopId='" + ReqPopId + "'");
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