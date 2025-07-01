using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Project_StockInReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void productsGrid_BeforePerformDataSelect(object sender, EventArgs e)
    {

        DataHelper123 dh = new DataHelper123();

        string itemid = (sender as DevExpress.Web.ASPxGridView).GetMasterRowKeyValue().ToString();
        (sender as DevExpress.Web.ASPxGridView).DataSource = dh.getDataTable("select CONVERT(varchar(250),si.StockInDate,103) 'StockInDate', sum(sid.Qty) as Quantity, si.ProjectName from tbl_StockInDetails sid inner join tbl_StockIn si on sid.StockInId = si.StockInId where sid.itemid = '" + itemid + "' group by si.StockInDate,si.ProjectName");
        //(sender as DevExpress.Web.ASPxGridView).DataBind();

    }



    protected void ASPxGridView1_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string UserId = Session["UserId"].ToString();
        string ProjectId = Session["BranchId"].ToString();
        Session["ProjectId"] = ProjectId;

        string UserName = DBAccess.FetchDatasingle("select ug.UserGroup from tbl_User u join tbl_UserGroups ug on u.UserGroup=ug.UserGroupId  where UserName='" + UserId + "'");
        if (UserName == "SuperAdmin" || UserName == "Region")
        {
            SqlDataSource1.SelectCommand = @"select sid.Poid, rsid.GRNNo, sid.ItemId,sum(sid.Qty) as Quantity,im.ItemName,im.Category,im.Type,im.Make,im.Model,im.Unit,im.HSNCode,im.ScrapGroup, im.WarrantyPeriod
from  tbl_StockInDetails sid left join tbl_RackStockInDetails rsid on sid.StockInId = rsid.StockInId inner join tbl_ItemMaster im on sid.ItemId=im.ItemId 
group by sid.ItemId,im.ItemName,im.Category,im.Type,im.Make,im.Model,im.Unit,im.HSNCode,im.ScrapGroup,im.WarrantyPeriod,sid.Poid,rsid.GRNNo";

        }
        else
        {
            SqlDataSource1.SelectCommand = @"select sid.Poid,rsid.GRNNo,sid.ItemId,sum(sid.Qty) as Quantity,im.ItemName,im.Category,im.Type,im.Make,im.Model,im.Unit,im.HSNCode,im.ScrapGroup, im.WarrantyPeriod
from  tbl_StockInDetails sid left join tbl_RackStockInDetails rsid on sid.StockInId = rsid.StockInId inner join tbl_ItemMaster im on sid.ItemId=im.ItemId where sid.BranchId='" + ProjectId + "' group by sid.ItemId,im.ItemName,im.Category,im.Type,im.Make,im.Model,im.Unit,im.HSNCode,im.ScrapGroup,im.WarrantyPeriod,sid.Poid,rsid.GRNNo";

        }
    }
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