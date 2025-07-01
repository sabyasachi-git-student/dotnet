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

public partial class Project_WithOutWarrantyFaultyRec : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ddlFaultyId_TextChanged(object sender, EventArgs e)
    {
        string FaultyId = ddlFaultyId.Text;

        DataTable dt = DBAccess.FetchDatatable(@"select *, convert(varchar, RMADate,103) as RMADate1 from tbl_FaultyWarrantyExpired where FaultyId='" + FaultyId + "'");
        if (dt.Rows.Count > 0)
        {
            txtRMANumber.Text = dt.Rows[0]["RmaNo"].ToString();
            dtpRMADate.Date = Convert.ToDateTime(dt.Rows[0]["RMADate1"].ToString());
            txtOEMName.Text = dt.Rows[0]["OEM"].ToString();
        }

        DataTable dt1 = DBAccess.FetchDatatable(@"select * from tbl_FaultyWarrantyExpiredDetails a join tbl_FaultyWarrantyExpired b on a.FaultyWarExId=b.FaultyWarExId where b.FaultyId='" + FaultyId + "'");
        if (dt1.Rows.Count > 0)
        {
            gvFaultyItemDetails.DataSource = dt1;
            gvFaultyItemDetails.DataBind();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("WithOutWarrantyFaultyRec.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FaultyId = ddlFaultyId.Text;
        string ReceiveDate = dtpDate.Text;

        if (FaultyId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select FaultyId First');", true);
            return;
        }
        if (ReceiveDate == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Date');", true);
            return;
        }

        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Faulty Item Receive Successfully.');window.location='../Project/WithOutWarrantyFaultyRec.aspx';", true);
    }
}