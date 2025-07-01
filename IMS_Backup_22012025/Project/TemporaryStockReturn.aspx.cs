using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using DevExpress.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;
public partial class Project_TemporaryStockReturn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string UserId = "";
            try
            {
                UserId = Session["UserId"].ToString();
            }
            catch { }
            string BranchId = "";
            try
            {
                BranchId = Session["BranchId"].ToString();
            }
            catch { }

            string UserGroupId = "";
            try
            {
                UserGroupId = Session["UserGroupId"].ToString();
            }
            catch { }

              try
            {
                UserId = Session["UserId"].ToString();

                string UserName = DBAccess.FetchDatasingle("select ug.UserGroup from tbl_User u join tbl_UserGroups ug on u.UserGroup=ug.UserGroupId  where UserName='" + UserId + "'");
                if (UserName == "SuperAdmin")
                {
                    dtpDate.Date = DateTime.Now;
                }
                else
                {
                    dtpDate.Date = DateTime.Now;
                    dtpDate.Enabled = false;
                }
            }
            catch
              { }
        }
    }


    protected void ddlIssueId_TextChanged(object sender, EventArgs e)
    {
        string ReqtransAppId = ddlIssueId.Value.ToString();
        Session["ReqtransAppId"] = ReqtransAppId;


        DataTable dt11 = DBAccess.FetchDatatable("select a.TemIssueId as ReqId,a.ItemId,a.Qty,b.*,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate, '' as POPQty, '' as ReqToQty from tbl_TemporaryStockIssueDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId join dbo.tbl_HSNCodeMaster h on b.HSNCode=h.HSNCode where a.TemIssueId='" + ReqtransAppId + "'");
        if (dt11.Rows.Count > 0)
        {
            for (int i = 0; i < dt11.Rows.Count; i++)
            {
                gvItemDetails.DataSource = dt11;
                gvItemDetails.DataBind();

            }
        }

        DataTable dt11A = DBAccess.FetchDatatable("select distinct a.*,Convert(Varchar,a.WarrantyTo,103) as WarrantyDate, Convert(Varchar,a.CoderLifeTo,103) as CoderLifeDate ,b.ItemName from tbl_TemporaryStockIssueBarCodeDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where a.TemIssueId ='" + ReqtransAppId + "'");
        if (dt11A != null && dt11A.Rows.Count > 0)
        {

            gv_Barc.DataSource = dt11A;
            gv_Barc.DataBind();
        }

        DataTable dt2 = DBAccess.FetchDatatable(" select * from tbl_TemporaryStockIssue where TemIssueId='" + ReqtransAppId + "'");
        if (dt2.Rows.Count > 0)
        {
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                txt1.Text = dt2.Rows[i]["ReqAppId"].ToString();
                txt2.Text = dt2.Rows[i]["ReqId"].ToString();
            }
        }
    }
    protected void gv_Barc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv_Barc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_Barc.EditIndex = e.NewEditIndex;
    }
    protected void gv_Barc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Barc.PageIndex = e.NewPageIndex;
        gv_Barc.DataSource = ViewState["TempBarcode"];
        gv_Barc.DataBind();
    }
    protected void gv_Barc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = gvr.RowIndex;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["TempBarcode"];
            dt.Rows[rowindex].Delete();
            dt.AcceptChanges();
            ViewState["TempBarcode"] = dt;
            gv_Barc.DataSource = dt;
            gv_Barc.DataBind();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("TemporaryStockReturn.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UserId = "";
        try
        {
            UserId = Session["UserId"].ToString();
        }
        catch { }
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        string UserGroupId = "";
        try
        {
            UserGroupId = Session["UserGroupId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;

        string TemReturnId = DBAccess.FetchDatatable("select [dbo].[fn_TemReturnId]()").Rows[0][0].ToString();
        string TemIssueId = ddlIssueId.Text;
        string ReqAppId = txt1.Text;
        string ReqId = txt2.Text;
        string IssueDate = dtpDate.Text;
        string Remarks = txtRemarks.Text;

        bool SaveDetails = DBAccess.SaveData(@" insert into [dbo].[tbl_TemporaryStockReturn] values ('" + TemReturnId + "', '" + TemIssueId + "', '" + ReqAppId + "','" + ReqId + "', convert(datetime,'" + IssueDate + "',103), '" + Remarks + "','','','','','','" + BranchId + "', '" + UserId + "', convert(datetime,'" + DOE + "',103))");

        bool UpdateTemporaryStockIssue = DBAccess.SaveData(@"update tbl_TemporaryStockIssue set ReturnStatus='Return', ReturnDate=convert(datetime,'" + DOE + "',103), ReturnUser='" + UserId + "' where TemIssueId='" + TemIssueId + "'");

        bool UpdateTemporaryStockIssueBarCodeDetails = DBAccess.SaveData(@"update tbl_TemporaryStockIssueBarCodeDetails set Status2='Return' where TemIssueId='" + TemIssueId + "'");

        //if (SaveDetails == true)
        //{
        //    for (int i = 0; i < gv_Barc.Rows.Count; i++)
        //    {
        //        string SrBarVodeID = gv_Barc.Rows[i].Cells[5].Text;
        //        string Barcode = gv_Barc.Rows[i].Cells[6].Text;

        //        bool UpdateRackBarCodeDetails = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set Status1='', Status2='' where SrBarVodeID='" + SrBarVodeID + "' and Barcode='" + Barcode + "' ");
        //    }
        //}

        if (SaveDetails == true)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Material Return saved successfully..');window.location='../Project/TemporaryStockReturn.aspx';", true);
        }

        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Return Not Save. Try again.');", true);
            return;
        }
    }
}