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

public partial class Project_TotalStockReportInUse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridHeaderFilterMode headerFilterMode = hFModeCheckBox.Checked ? GridHeaderFilterMode.CheckedList : GridHeaderFilterMode.List;
        foreach (GridViewDataColumn column in TotalStockReportInUse.DataColumns)
            column.SettingsHeaderFilter.Mode = headerFilterMode;
    }

    protected void productsGrid_BeforePerformDataSelect(object sender, EventArgs e)
    {

        DataHelper123 dh = new DataHelper123();
        string itemid = (sender as DevExpress.Web.ASPxGridView).GetMasterRowKeyValue().ToString();
        (sender as DevExpress.Web.ASPxGridView).DataSource = dh.getDataTable("select a.POID, convert(varchar,a.PODate,103) as PODate, sm.CompanyName, a.ProjectName, b.Qty, i.Unit, pd.Rate, b.Warranty  from tbl_StockIn a  join tbl_StockInDetails b on a.StockInId=b.StockInId   join tbl_RackStockInBarCodeDetails r on a.StockInId=r.StockInId join tbl_PurchaseBillEntry p on b.StockInId=p.StockInId  join tbl_PurchaseBillDetails pd on p.PurchaseBillId=pd.PurchaseBillId  join tbl_ItemMaster i on b.ItemId=i.ItemId join tbl_SupplierMasterEntry sm on a.SupplierId=sm.SupplierId where b.ItemId= '" + itemid + "' and   (select Count(Id) from tbl_RackStockInBarCodeDetails where ItemId=b.ItemId and BranchID= b.BranchId and Status1='InUse')>0");

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
}