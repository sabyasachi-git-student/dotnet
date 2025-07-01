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
using DevExpress.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;

public partial class Project_WarrantyExtension : System.Web.UI.Page
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

                string UserName = DBAccess.FetchDatasingle("select ug.UserGroup from tbl_User u join tbl_UserGroups ug on u.UserGroup=ug.UserGroupId  where UserName='" + UserId + "'");
                if (UserName == "SuperAdmin")
                {
                    dtpExtensionDate.Date = DateTime.Now;
                }
                else
                {
                    dtpExtensionDate.Date = DateTime.Now;
                    dtpExtensionDate.Enabled = false;
                }
                BranchId = Session["BranchId"].ToString();
            }
            catch { }
        }
    }
    protected DataTable getItem()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "POID";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "StoCkInId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CompanyName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Make";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Model";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "AvailableQty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "WarrantyTo";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CoderLifeTo";
        oTable.Columns.Add(dtCol);

        return oTable;
    }

    protected void gvWarrantyExtensionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWarrantyExtensionDetails.PageIndex = e.NewPageIndex;
        this.gvWarrantyExtensionDetails.DataSource = (DataTable)ViewState["WarrantyExtension"];
        this.gvWarrantyExtensionDetails.DataBind();
    }

    protected void gvWarrantyExtensionDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt8 = new DataTable();
            dt8 = (DataTable)ViewState["WarrantyExtension"];
            dt8.Rows[rowindex].Delete();
            dt8.AcceptChanges();
            ViewState["WarrantyExtension"] = dt8;
            gvWarrantyExtensionDetails.DataSource = dt8;
            gvWarrantyExtensionDetails.DataBind();


        }
    }

    protected void gvWarrantyExtensionDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvWarrantyExtensionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnAddServiceItem_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnCalculate_Click(btnCalculate, e);
        string POID = ddlPOID.Text;
        string WarExId = DBAccess.FetchDatatable("select [dbo].[fn_WarExId]()").Rows[0][0].ToString();
        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;
        for (int j = 0; j < gvWarrantyExtensionDetails.Rows.Count; j++)
        {
            TextBox txtWarEx1 = (TextBox)gvWarrantyExtensionDetails.Rows[j].FindControl("txtWarEx1");
            TextBox txtNewWarTo1 = (TextBox)gvWarrantyExtensionDetails.Rows[j].FindControl("txtNewWarTo1");

            DataTable dt11 = DBAccess.FetchDatatable("select Distinct convert(varchar,a.WarrantyTo,103) 'WarrantyTo' from tbl_RackStockInBarCodeDetails a join tbl_RackStockInDetails b on a.ActualStockInId=b.ActualStockInId join tbl_StockIn c on c.StockInId=b.StockInId where c.POID='" + POID + "'");
            string datestring1 = dt11.Rows[0]["WarrantyTo"].ToString();
            DateTime dateTime1 = DateTime.Parse(datestring1);

            #region New Warranty Date1

            int Months1 = Convert.ToInt32(txtWarEx1.Text);
            DateTime WarrantyendDate1 = new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day).AddMonths(Months1);
            txtNewWarTo1.Text = WarrantyendDate1.ToString("dd/MM/yyyy");

            #endregion
           
            

            string txtWarE1 = txtWarEx1.Text;
            string txtNewWarT1 = txtNewWarTo1.Text;
            

            Session["Warranty1"] = txtWarE1.ToString();
            Session["WarrantyTo1"] = txtNewWarT1.ToString();

            string WarExDate = dtpExtensionDate.Text;
            string POId = gvWarrantyExtensionDetails.Rows[j].Cells[1].Text.ToString();
            string StoCkInId = gvWarrantyExtensionDetails.Rows[j].Cells[2].Text.ToString();
            string ItemId = gvWarrantyExtensionDetails.Rows[j].Cells[4].Text.ToString();
            string Quantity = gvWarrantyExtensionDetails.Rows[j].Cells[8].Text.ToString();
            string WarrantyTo = gvWarrantyExtensionDetails.Rows[j].Cells[9].Text.ToString();

            Guid gd;
            gd = Guid.NewGuid();
            string guid = gd.ToString();

            string FileName3 = "";
            string ImageUpload = "";
            if (FileUpload1.HasFile)
            {
                try
                {
                    FileName3 = Path.GetFileName(FileUpload1.FileName);
                    string strPath = Server.MapPath("~");
                    strPath = strPath + "\\Project\\IMG\\" + guid + '_' + FileName3;
                    FileUpload1.SaveAs(strPath);
                    ImageUpload = guid + '_' + FileName3;
                }
                catch
                {
                    FileName3 = "";
                }
            }

            int y = ClassWarrantyExtension.Save(WarExId, WarExDate, POId, StoCkInId, ItemId, Quantity, WarrantyTo, txtWarE1, txtNewWarT1, FileName3, UserId, BranchId, DOE);
            if (y == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Warranty Extension is saved successfully..');window.location='../Project/WarrantyExtension.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Warranty Extension is not saved. Try again.');", true);
                return;
            }
        }    
}


    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarrantyExtension.aspx");
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        string POID = ddlPOID.Text;

        if (gvWarrantyExtensionDetails.Rows.Count > 0)
        {

            int rowsCount = gvWarrantyExtensionDetails.Rows.Count;
            for (int j = 0; j < rowsCount; j++)
            {
                TextBox txtWarEx1 = (TextBox)gvWarrantyExtensionDetails.Rows[j].FindControl("txtWarEx1");
                TextBox txtNewWarTo1 = (TextBox)gvWarrantyExtensionDetails.Rows[j].FindControl("txtNewWarTo1");
                
                
              
        DataTable dt11 = DBAccess.FetchDatatable("select Distinct convert(varchar,a.WarrantyTo,103) 'WarrantyTo' from tbl_RackStockInBarCodeDetails a join tbl_RackStockInDetails b on a.ActualStockInId=b.ActualStockInId join tbl_StockIn c on c.StockInId=b.StockInId where c.POID='" + POID + "'");
        string datestring1 = dt11.Rows[0]["WarrantyTo"].ToString();
        DateTime dateTime1 = DateTime.Parse(datestring1);
                #region New Warranty Date1
                int Months1 = Convert.ToInt32(txtWarEx1.Text);
                DateTime WarrantyendDate1 = new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day).AddMonths(Months1);
                txtNewWarTo1.Text = WarrantyendDate1.ToString("dd/MM/yyyy");
                #endregion
               

            }
            



        }
    }   

    protected void ddlPOID_TextChanged(object sender, EventArgs e)
    {
        Session["POID"] = null;

        string POID = ddlPOID.Text;
        Session["POID"] = POID;

        DataTable dt11 = DBAccess.FetchDatatable("select Distinct c.POID, b.StoCkInId, s.CompanyName, a.ItemId, i.ItemName, i.Make, i.Model, b.AvailableQty, convert(varchar,a.WarrantyTo,103) 'WarrantyTo', convert(varchar,a.CoderLifeTo,103) 'CoderLifeTo'  from tbl_RackStockInBarCodeDetails a join tbl_RackStockInDetails b on a.ActualStockInId=b.ActualStockInId join tbl_StockIn c on c.StockInId=b.StockInId join tbl_ItemMaster i on a.ItemId=i.ItemId join tbl_SupplierMasterEntry s on c.SupplierId=s.SupplierId where c.POID='" + POID + "'");
        if (dt11 != null && dt11.Rows.Count > 0)
        {
            gvWarrantyExtensionDetails.DataSource = dt11;
            gvWarrantyExtensionDetails.DataBind();
        }
    }

}