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

public partial class Project_LatLongItemReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
        }
        catch
        {

        }
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch
        {

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ItemId = ddlItem.Value.ToString();
        decimal Lat = Convert.ToDecimal(txtLat.Text);
        decimal Long = Convert.ToDecimal(txtLong.Text);

        DataTable dt = DBAccess.FetchDatatable("SELECT * FROM (   SELECT b.BranchName, im.ItemName,im.Make,im.Model,rsd.GRNNo,rsd.ItemId,rsd.RackSpace,rsd.Row,rsd.Rack,rsd.Shelf, rsd.ItemUnit,rsd.BranchId,(select sum(qty) from tbl_RackStockInBarCodeDetails where StockInId=rsd.StockInId and ItemId=rsd.ItemId and ProcessId= rsd.RackSpace and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='') and (Status3 is null or Status3 ='') and (Status4 is null or Status4 ='')) as Qty,        (            (                (                    acos(                        sin(( '" + Lat + "' * pi() / 180))                        *                        sin(( b.ContactPerson * pi() / 180)) + cos(( '" + Lat + "' * pi() /180 ))                        *                        cos(( b.ContactPerson * pi() / 180)) * cos((( '" + Long + "' - b.ContactNo) * pi()/180)))                ) * 180/pi()            ) * 60 * 1.1515 * 1.609344        )    as distance FROM tbl_Branch b	join tbl_RackStockInDetails rsd on rsd.branchId=b.BranchId	join tbl_ItemMaster im on rsd.ItemId=im.ItemId where rsd.ItemId='" + ItemId + "') myTable WHERE distance <= 450");

        if (dt.Rows.Count > 0)
        {
            gv_Barc.DataSource = dt;
            gv_Barc.DataBind();
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("LatLongItemReport.aspx");
    }
    protected void ddlBranch_TextChanged(object sender, EventArgs e)
    {
        string BranchId = ddlBranch.Value.ToString();
        DataTable dt11 = DBAccess.FetchDatatable(" select ContactPerson, ContactNo from tbl_Branch where BranchId='" + BranchId + "'");
        if (dt11.Rows.Count > 0)
        {
            txtLat.Text = dt11.Rows[0]["ContactPerson"].ToString();
            txtLong.Text = dt11.Rows[0]["ContactNo"].ToString();
        }
    }
}