using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;
using DevExpress.Web;
using DevExpress.Data.Linq;

public partial class Project_WarrantyStatusReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridHeaderFilterMode headerFilterMode = hFModeCheckBox.Checked ? GridHeaderFilterMode.CheckedList : GridHeaderFilterMode.List;
        foreach (GridViewDataColumn column in WarrantyStatusReports.DataColumns)
            column.SettingsHeaderFilter.Mode = headerFilterMode;

        if (!IsPostBack)
        {
            Session["startdate2"] = null;
            Session["POId"] = null;
            Session["Category"] = null;

            dtpFromDate.Date = DateTime.Now;
        }
    }

    protected void productsGrid_BeforePerformDataSelect(object sender, EventArgs e)
    {
        DataHelper123 dh = new DataHelper123();
        string itemid = (sender as DevExpress.Web.ASPxGridView).GetMasterRowKeyValue().ToString();
        (sender as DevExpress.Web.ASPxGridView).DataSource = dh.getDataTable("select s.CompanyName, (Select top 1 ConcernPersonName from tbl_SupplierConcernPersonDetails where s.SupplierId=SupplierId order by rowid asc) as ContactPerson, (Select top 1 ContactNo from tbl_SupplierContactNo where s.SupplierId=SupplierId order by rowid asc) as ContactNumber, (Select top 1 EmailId from tbl_SupplierEmail where s.SupplierId=SupplierId order by rowid asc) as EmailId, (Select top 1 Address from tbl_SupplierAddress where s.SupplierId=SupplierId order by rowid asc) as Address  from tbl_SupplierMasterEntry s   join tbl_StockIn ss on ss.SupplierId=s.SupplierId  where ss.StockInId='" + itemid + "'");
    }
    public class DataHelper123
    {
        public SqlConnection GetConnection()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Profit"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }

        public DataTable getDataTable(string query)
        {
            DataTable table = new DataTable("Table");
            SqlCommand cmd = new SqlCommand(query, GetConnection());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            return table;
        }

    }
    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptions());
    }
    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptions());
    }
    protected void WarrantyStatusReports_BeforePerformDataSelect(object sender, EventArgs e)
    {
        String BranchId = "";
        if (Session["BranchId"] != null)
        {
            BranchId = Session["BranchId"].ToString();
        }
        if (Session["startdate2"] != null)
        {
            SqlDataSource1.SelectCommand = @"select Distinct a.POID, convert(varchar,a.PODate,103) as PODate, gu.GRNNo, convert(varchar,gu.Date,103) as GrnDate, a.StockInId,  convert(varchar,a.StockInDate,103) as StockInDate,
 sm.CompanyName, a.ProjectName, b.ItemId, im.Category, im.ItemName, im.Make, im.Model, im.WarrantyPeriod, b.Qty, im.Unit, b.Warranty, convert(varchar,sb.WarrantyTo,103) as WarrantyTo
 from tbl_StockIn a
 join tbl_StockInDetails b on a.StockInId=b.StockInId
 join tbl_StockInBarcodes sb on a.StockInId=sb.StockInId
 join tbl_GRNUpdate gu on a.StockInId=gu.StockInId
 join tbl_PurchaseBillEntry p on b.StockInId=p.StockInId
 join tbl_PurchaseBillDetails pd on p.PurchaseBillId=pd.PurchaseBillId
 join tbl_ItemMaster im on b.ItemId=im.ItemId 
 join tbl_SupplierMasterEntry sm on a.SupplierId=sm.SupplierId where convert(varchar,sb.WarrantyTo,103) ='" + Session["startdate2"].ToString() + "'";
        }

        if (Session["POId"] != null)
        {
            SqlDataSource1.SelectCommand = @"select Distinct a.POID, convert(varchar,a.PODate,103) as PODate, gu.GRNNo, convert(varchar,gu.Date,103) as GrnDate, a.StockInId,  convert(varchar,a.StockInDate,103) as StockInDate,
 sm.CompanyName, a.ProjectName, b.ItemId, im.Category, im.ItemName, im.Make, im.Model, im.WarrantyPeriod, b.Qty, im.Unit, b.Warranty, convert(varchar,sb.WarrantyTo,103) as WarrantyTo
 from tbl_StockIn a
 join tbl_StockInDetails b on a.StockInId=b.StockInId
 join tbl_StockInBarcodes sb on a.StockInId=sb.StockInId
 join tbl_GRNUpdate gu on a.StockInId=gu.StockInId
 join tbl_PurchaseBillEntry p on b.StockInId=p.StockInId
 join tbl_PurchaseBillDetails pd on p.PurchaseBillId=pd.PurchaseBillId
 join tbl_ItemMaster im on b.ItemId=im.ItemId 
 join tbl_SupplierMasterEntry sm on a.SupplierId=sm.SupplierId where a.Poid= '" + Session["POId"].ToString() + "'";
        }

        if (Session["Category"] != null)
        {
            SqlDataSource1.SelectCommand = @"select Distinct a.POID, convert(varchar,a.PODate,103) as PODate, gu.GRNNo, convert(varchar,gu.Date,103) as GrnDate, a.StockInId,  convert(varchar,a.StockInDate,103) as StockInDate,
 sm.CompanyName, a.ProjectName, b.ItemId, im.Category, im.ItemName, im.Make, im.Model, im.WarrantyPeriod, b.Qty, im.Unit, b.Warranty, convert(varchar,sb.WarrantyTo,103) as WarrantyTo
 from tbl_StockIn a
 join tbl_StockInDetails b on a.StockInId=b.StockInId
 join tbl_StockInBarcodes sb on a.StockInId=sb.StockInId
 join tbl_GRNUpdate gu on a.StockInId=gu.StockInId
 join tbl_PurchaseBillEntry p on b.StockInId=p.StockInId
 join tbl_PurchaseBillDetails pd on p.PurchaseBillId=pd.PurchaseBillId
 join tbl_ItemMaster im on b.ItemId=im.ItemId 
 join tbl_SupplierMasterEntry sm on a.SupplierId=sm.SupplierId where im.Category='" + Session["Category"].ToString() + "'";
        }

    }
    protected void chkPODate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPODate.Checked == true)
        {
            trPOdate.Visible = true;
            trPOID.Visible = false;
            trCat.Visible = false;
            chkCateory.Checked=false;
            chkPOID.Checked = false;
        }
        else
        {
            trCat.Visible = false;
            trPOdate.Visible = false;
            trPOID.Visible = false;
            chkCateory.Checked = false;
            chkPOID.Checked = false;
        }
    }
    protected void chkPOID_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPOID.Checked == true)
        {
            trPOID.Visible = true;
            trPOdate.Visible = false;
            trCat.Visible = false;
            chkCateory.Checked = false;
            chkPODate.Checked = false;
        }
        else
        {
            trCat.Visible = false;
            trPOdate.Visible = false;
            trPOID.Visible = false;
            chkCateory.Checked = false;
            chkPODate.Checked = false;
        }
    }
    protected void chkCateory_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCateory.Checked == true)
        {
            trCat.Visible = true;
            trPOID.Visible = false;
            trPOdate.Visible = false;
            chkPODate.Checked = false;
            chkPOID.Checked = false;
        }
        else
        {
            trCat.Visible = false;
            trPOdate.Visible = false;
            trPOID.Visible = false;
            chkPODate.Checked = false;
            chkPOID.Checked = false;
        }
    }
    protected void btnClear1_Click(object sender, EventArgs e)
    {
        Response.Redirect("POStatusReports.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("POStatusReports.aspx");
    }
    protected void btnClear2_Click(object sender, EventArgs e)
    {
        Response.Redirect("POStatusReports.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string startdate;

        startdate = dtpFromDate.Text;       

        //string startdate2 = startdate.ToString("dd/MM/yyyy");

        Session["startdate2"] = startdate;


        WarrantyStatusReports.DataBind();
    }
    protected void btnPOID_Click(object sender, EventArgs e)
    {
        string POId = txtPOID.Text;
        Session["POId"] = POId;

        WarrantyStatusReports.DataBind();
    }
    protected void btnCategory_Click(object sender, EventArgs e)
    {
        string Category = txtCategory.Text;
        Session["Category"] = Category;

        WarrantyStatusReports.DataBind();
    }
   
}