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
public partial class Project_RackTaggingReport : System.Web.UI.Page
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

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptions());
    }
    protected void btnXlsxExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptions());
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
        ASPxGridViewExporter2.WriteXlsToResponse(new XlsExportOptions());
    }
    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter2.WriteXlsxToResponse(new XlsxExportOptions());
    }
}