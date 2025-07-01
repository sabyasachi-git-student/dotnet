using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Project_MaterialIssueReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
    }

    protected void rackTagging_BeforePerformDataSelect(object sender, EventArgs e)
    {
        DataHelper123 dh = new DataHelper123();

        string IssueId = (sender as DevExpress.Web.ASPxGridView).GetMasterRowKeyValue().ToString();
        //string ItemId = DBAccess.FetchDatasingle("select Shelf from tbl_RackStockInBarCodeDetails where ItemId='" + Shelf + "'");

        (sender as DevExpress.Web.ASPxGridView).DataSource = dh.getDataTable("select a.*, convert(varchar,a.WarrantyTo,103) as WarrantyTo1, convert(varchar,a.CoderLifeTo,103) as CoderLifeTo1, b.ItemName, b.Make, b.Model from tbl_TemporaryStockIssueBarCodeDetails a join tbl_ItemMaster b on a.itemId=b.ItemId where a.TemIssueId='" + IssueId + "'");
        //(sender as DevExpress.Web.ASPxGridView).DataBind();

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
}