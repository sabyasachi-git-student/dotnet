using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Project_FaultyWarrantyExpiredReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void FaultyInWarranty_BeforePerformDataSelect(object sender, EventArgs e)
    {

        DataHelper123 dh = new DataHelper123();

        string FaultyInWarId = (sender as DevExpress.Web.ASPxGridView).GetMasterRowKeyValue().ToString();
        (sender as DevExpress.Web.ASPxGridView).DataSource = dh.getDataTable("SELECT fwed.* FROM tbl_FaultyWarrantyExpiredDetails fwed join tbl_FaultyWarrantyExpired fwe on fwed.FaultyWarExId=fwe.FaultyWarExId where fwe.FaultyWarExId ='" + FaultyInWarId + "'");
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