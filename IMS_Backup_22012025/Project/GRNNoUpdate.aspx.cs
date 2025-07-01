using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Project_GRNNoUpdate : System.Web.UI.Page
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

    //protected void rackTagging_BeforePerformDataSelect(object sender, EventArgs e)
    //{

    //    DataHelper123 dh = new DataHelper123();

    //    string StockInId = (sender as DevExpress.Web.ASPxGridView).GetMasterRowKeyValue().ToString();
    //    //string ItemId = DBAccess.FetchDatasingle("select Shelf from tbl_RackStockInBarCodeDetails where ItemId='" + Shelf + "'");

    //    (sender as DevExpress.Web.ASPxGridView).DataSource = dh.getDataTable("Select im.ItemName,r.SrBarVodeID,r.Barcode,r.Row,r.Rack,r.Shelf,r.Qty as Qty FROM tbl_RackStockInBarCodeDetails r join tbl_ItemMaster im on im.ItemId=r.ItemID WHERE  r.StockInId='" + StockInId + "' ");
    //    //(sender as DevExpress.Web.ASPxGridView).DataBind();

    //}

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
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string StockInId = obj.ToString();

        if (e.CommandArgs.CommandName == "UpdateGRN")
        {
            txtSOId.Text = StockInId;
            Session["StockInId"] = txtSOId.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string StockInId = txtSOId.Text;
        if (StockInId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select SO Id.!');", true);
            return;
        }

        string Date = dtpDate.Text;
        if (Date == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select Date!');", true);
            return;
        }
        string GRNNo = txtGRNNo.Text;
        if (GRNNo == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter GRNNo!');", true);
            return;
        }

        string Remarks = txtRemarks.Text;

        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;

        bool SaveItem = DBAccess.SaveData("insert into [dbo].[tbl_GRNUpdate] values('" + StockInId + "', convert(datetime,'" + Date + "',103), '" + GRNNo + "',  '" + Remarks + "',  '" + UserId + "', '" + BranchId + "',  convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103))");

        if (SaveItem == true)
        {
            bool UpdateRackStockInDetails = DBAccess.SaveData(@"update [tbl_RackStockInDetails] set GRNNo= '" + GRNNo + "' where StockInId='" + StockInId + "'");

            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Status Save Successfully..');window.location='../Project/GRNNoUpdate.aspx';", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Status is not done. Try again.');", true);
            return;
        }

    }
}