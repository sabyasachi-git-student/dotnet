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
public partial class Project_WarrantyExtensionReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

   
    protected void gv_WarrantyExtensionReport_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        Button Update = (Button)gv_WarrantyExtensionReport.FindRowCellTemplateControl(e.VisibleIndex, null, "btnUpdate");

        string WarExId = e.GetValue("WarExId").ToString();

        string dt1 = DBAccess.FetchDatasingle(@"select Status1 from tbl_WarrantyExtension where  WarExId='" + WarExId + "'");
        if (dt1 == "")
        {
            e.Row.BackColor = Color.LightGreen;
            Update.Visible = true;
        }
        
    }

    protected void gv_WarrantyExtensionReport_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string WarExId = obj.ToString();

        if (e.CommandArgs.CommandName == "UpdateGRN")
        {
            string POID = DBAccess.FetchDatasingle(@"select POID from tbl_WarrantyExtension where  WarExId='" + WarExId + "'");

            txtPOID.Text = POID;
            Session["POID"] = txtPOID.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
        }

    }
}