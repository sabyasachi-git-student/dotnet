using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

public partial class Project_MaterialShipmentReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string BranchId = "";
            try
            {
                BranchId = Session["BranchId"].ToString();
            }
            catch
            {

            }
        }

    }

    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object id = e.KeyValue;
        string RowId = id.ToString();


        if (e.CommandArgs.CommandName == "View")
        {
            DataTable dt = DBAccess.FetchDatatable(@"select ('IMG/'+ImageUpload) as 'Images' from tbl_MaterialShipment where MetShipId='" + RowId + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                gvDocuments.DataSource = dt;
                gvDocuments.DataBind();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "lightbox_open('Group4')", true);
        }

        if (e.CommandArgs.CommandName == "GatePass")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "window", "window.open('DevExpressPrint.aspx?MetShipId1=" + RowId + "&Types=Stall', 'window','HEIGHT=550,WIDTH=820,top=50,left=50,toolbar=no,scrollbars=yes,resizable=yes');", true);
        }

        if (e.CommandArgs.CommandName == "Deceleration")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "window", "window.open('DevExpressPrint.aspx?MetShipId=" + RowId + "&Types=Stall', 'window','HEIGHT=550,WIDTH=820,top=50,left=50,toolbar=no,scrollbars=yes,resizable=yes');", true);
        }

        if (e.CommandArgs.CommandName == "Update")
        {
            Response.Redirect(string.Format("~/Project/MaterialShipment.aspx?MetShipId={0}", RowId));
        }
    }
}