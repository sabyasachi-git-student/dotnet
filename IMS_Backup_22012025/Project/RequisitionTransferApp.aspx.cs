﻿using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraPrinting;

public partial class Project_RequisitionTransferApp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string UserId = "";
            string BranchId = "";
            string UserGroup = "";
            Session["UserGroupId1"] = "";
            try
            {
                UserGroup = Session["UserGroupId"].ToString();
                string UserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='" + UserGroup + "'");
                txtUserGroup.Text = UserGroupName.ToString();
                string ToUserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='UG15'");
            }
            catch

            { }
            try
            {
                UserId = Session["UserId"].ToString();
                txtReqUser.Text = UserId.ToString();

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
            try
            {
                BranchId = Session["BranchId"].ToString();
                string BranchName = DBAccess.FetchDatasingle("select BranchName from tbl_Branch where BranchId='" + BranchId + "'");
                txtBranchName.Text = BranchName.ToString();
                Session["BranchName"] = BranchName.ToString();
                txtReqId.Text = BranchId.ToString();
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
                return;
            }

        }
    }


    protected void gv_EstRawMat_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string ID = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        string ReqtransId = "";
        try
        {
            ReqtransId = ID.Split(',')[0].ToString();
        }
        catch { }

        try
        {
            string Qry = "select a.ReqtransId,a.ItemId,a.Qty,b.* from tbl_RequisitionTransferDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where ReqtransId='" + ReqtransId + "'";
            SqlDataSource3.SelectCommand = Qry;
        }
        catch
        {

        }

    }
    protected void gv_Estimation_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string ReqtransId = obj.ToString();

        string Date = "";

        string Remarks = "Approve Done";

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
        string Status1 = "Approve";
        string UserName = txtReqUser.Text;
        string UserGroupName = txtUserGroup.Text;
        string BranchName = txtBranchName.Text;

        if (e.CommandArgs.CommandName == "Reject")
        {
            txtSOId.Text = ReqtransId;
            Session["ReqtransId"] = txtSOId.Text;

            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
        }

        if (e.CommandArgs.CommandName == "Approve")
        {
            Response.Redirect(string.Format("~/Project/TemporaryStockRequisition.aspx?ReqtransId={0}", ReqtransId));
            //bool SaveItem = DBAccess.SaveData("insert into [dbo].[tbl_RequisitionTetApp_Reject] values('" + ReqPopId + "', convert(datetime,'" + Date + "',103), '" + Remarks + "', '" + Status1 + "',  '" + UserId + "',  '" + UserName + "', '" + UserGroupId + "', '" + UserGroupName + "','" + BranchName + "','" + BranchId + "',  convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103))");
            //bool UpdateReqTet = DBAccess.SaveData("update tbl_RequisitionPop set Status6 = '" + Status1 + "',  Status7 = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) where ReqPopId='" + ReqPopId + "'");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string ReqtransId = txtSOId.Text;
        if (ReqtransId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select Id.!');", true);
            return;
        }

        string Date = dtpDate.Text;
        if (Date == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select Date!');", true);
            return;
        }

        string Remarks = txtRemarks.Text;

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
        string Status1 = "Reject";
        string UserName = txtReqUser.Text;
        string UserGroupName = txtUserGroup.Text;
        string BranchName = txtBranchName.Text;
        int SaveItem = ClassRequisitionApproval.ReqTransferAppSave(ReqtransId, Date, Remarks, Status1, UserId, UserName, UserGroupId, UserGroupName, BranchName, BranchId, DOE);
        bool UpdateReqPop = DBAccess.SaveData("update tbl_RequisitionTransfer set Status6 = '" + Status1 + "',  Status7 = convert(datetime,'" + Date + "',103) where ReqtransId='" + ReqtransId + "'");
        if (SaveItem == 1)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Rejected Successfully..');window.location='../Project/RequisitionTransferApp.aspx';", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Status is not done. Try again.');", true);
            return;
        }

    }
}