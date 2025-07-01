using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
public partial class Project_RequisitionTerrytory_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetailsApp"], null))
        {
            ViewState["ItemDetailsApp"] = getItemApp();
        }
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItem();
        }

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

            string ReqSecId = Request.QueryString["ReqSecId"];
            if (ReqSecId != null)
            {
                FatchDataFromApprovalSec(ReqSecId, "Approval");
            }
            else
            {

            }

            string ReqPopId = Request.QueryString["ReqPopId"];
            if (ReqPopId != null)
            {
                FatchDataFromApprovalPop(ReqPopId, "Approval");
            }
            else
            {

            }

        }
    }

    protected void ChkFor_CheckedChanged(object sender, EventArgs e)
    {
       
        if (ChkFor.Checked == true)
        {
            string ReqSecId = Request.QueryString["ReqSecId"];
            string ReqPopId = Request.QueryString["ReqPopId"];
          

            if (ReqSecId != null)
            {
                FatchDataFromForwordSec(ReqSecId, "Approval");
            }
            else if (ReqPopId != null)
            {
                FatchDataFromForwordPop(ReqPopId, "Approval");
            }
        }
        else
        {
            tblFor.Visible = false;
           

        }
    }
    public void FatchDataFromApprovalSec(string ReqSecId, string Status)
    {
        DataTable dt = DBAccess.FetchDatatable(@"select *, convert(varchar,RequisitionDate,103) as RequisitionDate1 from tbl_RequisitionSection where ReqSecId='" + ReqSecId + "'");
        if (dt.Rows.Count > 0)
        {
            txtReqId1.Text = dt.Rows[0]["BranchId"].ToString();
            txtBranchName1.Text = dt.Rows[0]["Status4"].ToString();
            txtUserGroup1.Text = dt.Rows[0]["Status5"].ToString();
            lblUserGroupId1.Text = dt.Rows[0]["UserGroupId"].ToString();
            txtReqUser1.Text = dt.Rows[0]["UserId"].ToString();
            dtpDate1.Date = Convert.ToDateTime(dt.Rows[0]["RequisitionDate1"].ToString());
            ddlRequisitionPur1.Text = dt.Rows[0]["RequisitionPurpose"].ToString();
            ddlRequisitionPurId1.Text = dt.Rows[0]["PrioritiesId"].ToString();
            ddlRequisitionto1.Text = dt.Rows[0]["ReUserName"].ToString();
            ddlRequisitiontoId1.Text = dt.Rows[0]["ReUserId"].ToString();
            ddlRequToGrp1.Text = dt.Rows[0]["ReUserGroupName"].ToString();
            ddlRequToGrpId1.Text = dt.Rows[0]["ReUserGroupId"].ToString();
            ddlRequToUser1.Text = dt.Rows[0]["Status"].ToString();
            txtRemarks1.Text = dt.Rows[0]["Remarks"].ToString();
            txtForword.Text = dt.Rows[0]["Forword"].ToString();
            txtPopId.Text = dt.Rows[0]["FrorwordFrom"].ToString();
            ddlProject1.Text = dt.Rows[0]["Status1"].ToString();
            ddlProjectId1.Text = dt.Rows[0]["Status2"].ToString();

        }

        string BranchIdSec = txtReqId1.Text;
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        DataTable dt1 = (DataTable)ViewState["ItemDetailsApp"];
        DataTable Items = DBAccess.FetchDatatable(@"select a.*,b.* ,c.*,'' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdSec + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as POPQty1, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as ReqToQty1 from tbl_RequisitionSectionDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId left join tbl_HSNCodeMaster c on b.HSNCode=c.HSNCode  where ReqSecId='" + ReqSecId + "'");

        if (Items.Rows.Count > 0)
        {
            for (int i = 0; i < Items.Rows.Count; i++)
            {
                string ReqSecItemId = Items.Rows[i]["ReqSecItemId"].ToString();
                Session["ReqSecItemId"] = ReqSecItemId.ToString();
                string ItemId = Items.Rows[i]["ItemId"].ToString();
                string Category = Items.Rows[i]["Category"].ToString();
                string Type = Items.Rows[i]["Type"].ToString();
                string ItemName = Items.Rows[i]["ItemName"].ToString();
                string Make = Items.Rows[i]["Make"].ToString();
                string Model = Items.Rows[i]["Model"].ToString();
                string Qty = Items.Rows[i]["Qty"].ToString();
                string rate = Items.Rows[i]["rate"].ToString();
                string HSNCode = Items.Rows[i]["HSNCode"].ToString();
                string CGST = Items.Rows[i]["CGST"].ToString();
                string IGST = Items.Rows[i]["IGST"].ToString();
                string SGST = Items.Rows[i]["SGST"].ToString();
                string ReqToQty = Items.Rows[i]["ReqToQty1"].ToString();
                string POPQty = Items.Rows[i]["POPQty1"].ToString();


                DataRow drH = dt1.NewRow();
                drH["ReqPopItemId"] = Items.Rows[i]["ReqSecItemId"].ToString();
                drH["ItemId"] = Items.Rows[i]["ItemId"].ToString();
                drH["Category"] = Items.Rows[i]["Category"].ToString();
                drH["Type"] = Items.Rows[i]["Type"].ToString();
                drH["ItemName"] = Items.Rows[i]["ItemName"].ToString();
                drH["Make"] = Items.Rows[i]["Make"].ToString();
                drH["Model"] = Items.Rows[i]["Model"].ToString();
                drH["Unit"] = Items.Rows[i]["Unit"].ToString();
                drH["Qty"] = Items.Rows[i]["Qty"].ToString();
                drH["rate"] = Items.Rows[i]["rate"].ToString();
                drH["HSNCode"] = Items.Rows[i]["HSNCode"].ToString();
                drH["CGST"] = Items.Rows[i]["CGST"].ToString();
                drH["IGST"] = Items.Rows[i]["IGST"].ToString();
                drH["SGST"] = Items.Rows[i]["SGST"].ToString();
                drH["ReqToQty"] = Items.Rows[i]["ReqToQty1"].ToString();
                drH["POPQty"] = Items.Rows[i]["POPQty1"].ToString();

                dt1.Rows.Add(drH);
            }
            ViewState["ItemDetailsApp"] = dt1;
            gvItemDetailsApp.DataSource = ViewState["ItemDetailsApp"] as DataTable;
            gvItemDetailsApp.DataBind();
            ViewState["ReqSecId"] = ReqSecId;
            tblApp.Visible = true;
            tblFor.Visible = false;
            btnChooseRawMaterial.Visible = false;

            #region ButtonTextChange

            if (Status == "Approval")
            {
                btnSave.Text = "Approve And Forword";
            }
            //else if (Status == "Revision")
            //{
            //    btnSave.Text = "SAVE";
            //}
            //else if (Status == "ShortClose")
            //{
            //    btnSave.Text = "ShortClose";
            //}

            #endregion ButtonTextChange

        }


    }
    public void FatchDataFromForwordSec(string ReqSecId, string Status)
    {
        DataTable dt = DBAccess.FetchDatatable(@"select *, convert(varchar,RequisitionDate,103) as RequisitionDate1 from tbl_RequisitionSection where ReqSecId='" + ReqSecId + "'");
        if (dt.Rows.Count > 0)
        {
            ddlRequisitionPur.Value = dt.Rows[0]["PrioritiesId"].ToString();
            ddlProject.Value = dt.Rows[0]["Status1"].ToString();
            ddlRequisitionPur.Enabled = false;
            ddlProject.Enabled = false;
        }
        string BranchIdSec = txtReqId1.Text;
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        DataTable dt1 = (DataTable)ViewState["ItemDetails"];
        DataTable Items = DBAccess.FetchDatatable(@"select a.*,b.* ,c.*,'' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdSec + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as POPQty1, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as ReqToQty1 from tbl_RequisitionSectionDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId left join tbl_HSNCodeMaster c on b.HSNCode=c.HSNCode  where ReqSecId='" + ReqSecId + "'");

         if (Items.Rows.Count > 0)
        {
            for (int i = 0; i < Items.Rows.Count; i++)
            {
                for (int b = 0; b < gvItemDetailsApp.Rows.Count; b++, i++)
                {

                    TextBox txtItemQty = (TextBox)gvItemDetailsApp.Rows[b].FindControl("txtItemQty");
                    CheckBox chk = (CheckBox)gvItemDetailsApp.Rows[b].FindControl("chk");
                    decimal AppQty1 = Convert.ToDecimal(txtItemQty.Text);
                    Session["AppQty"] = AppQty1;
                    if (chk.Checked == true)
                    {

                        string ReqSecItemId = Items.Rows[i]["ReqSecItemId"].ToString();
                        Session["ReqSecItemId"] = ReqSecItemId.ToString();
                        string ItemId = Items.Rows[i]["ItemId"].ToString();
                        string Category = Items.Rows[i]["Category"].ToString();
                        string Type = Items.Rows[i]["Type"].ToString();
                        string ItemName = Items.Rows[i]["ItemName"].ToString();
                        string Make = Items.Rows[i]["Make"].ToString();
                        string Model = Items.Rows[i]["Model"].ToString();
                        string Qty = Items.Rows[i]["Qty"].ToString();
                        string rate = Items.Rows[i]["rate"].ToString();
                        string HSNCode = Items.Rows[i]["HSNCode"].ToString();
                        string CGST = Items.Rows[i]["CGST"].ToString();
                        string IGST = Items.Rows[i]["IGST"].ToString();
                        string SGST = Items.Rows[i]["SGST"].ToString();
                        string ReqToQty = Items.Rows[i]["ReqToQty1"].ToString();
                        string POPQty = Items.Rows[i]["POPQty1"].ToString();

                        decimal Qty1 = Convert.ToDecimal(Qty);
                        decimal AppQty = Convert.ToDecimal(Session["AppQty"]);
                        decimal Total = Qty1 - AppQty;
                        string ReqQty1 = DBAccess.FetchDatasingle("select Qty from tbl_RequisitionSectionDetails where ReqSecId='" + ReqSecId + "' and ReqSecItemId='" + ReqSecItemId + "'");
                        decimal ReqQty = Convert.ToDecimal(ReqQty1);

                        if (Total == 0)
                        {
                            tblFor.Visible = false;
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Its 0 Not able to Forword.');", true);
                            return;

                        }
                        if (Total > ReqQty)
                        {
                            tblFor.Visible = false;
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It's More Than Requisitions Qty.');", true);
                            return;

                        }

                        DataRow drH = dt1.NewRow();
                        drH["ReqPopItemId"] = Items.Rows[i]["ReqSecItemId"].ToString();
                        drH["ItemId"] = Items.Rows[i]["ItemId"].ToString();
                        drH["Category"] = Items.Rows[i]["Category"].ToString();
                        drH["Type"] = Items.Rows[i]["Type"].ToString();
                        drH["ItemName"] = Items.Rows[i]["ItemName"].ToString();
                        drH["Make"] = Items.Rows[i]["Make"].ToString();
                        drH["Model"] = Items.Rows[i]["Model"].ToString();
                        drH["Unit"] = Items.Rows[i]["Unit"].ToString();
                        drH["Qty"] = Total;
                        drH["rate"] = Items.Rows[i]["rate"].ToString();
                        drH["HSNCode"] = Items.Rows[i]["HSNCode"].ToString();
                        drH["CGST"] = Items.Rows[i]["CGST"].ToString();
                        drH["IGST"] = Items.Rows[i]["IGST"].ToString();
                        drH["SGST"] = Items.Rows[i]["SGST"].ToString();
                        drH["ReqToQty"] = Items.Rows[i]["ReqToQty1"].ToString();
                        drH["POPQty"] = Items.Rows[i]["POPQty1"].ToString();

                        dt1.Rows.Add(drH);
                    }
                    ViewState["ItemDetails"] = dt1;
                    gvItemDetails.DataSource = ViewState["ItemDetails"] as DataTable;
                    gvItemDetails.DataBind();
                    ViewState["ReqSecId"] = ReqSecId;
                    tblApp.Visible = true;

                    btnChooseRawMaterial.Visible = false;

                    #region ButtonTextChange

                    if (Status == "Approval")
                    {
                        btnSave.Text = "Approve And Forword";
                    }
                    //else if (Status == "Revision")
                    //{
                    //    btnSave.Text = "SAVE";
                    //}
                    //else if (Status == "ShortClose")
                    //{
                    //    btnSave.Text = "ShortClose";
                    //}

                    #endregion ButtonTextChange
                }
            }
        }
    }
    public void FatchDataFromApprovalPop(string ReqPopId, string Status)
    {
        DataTable dt = DBAccess.FetchDatatable(@"select *, convert(varchar,RequisitionDate,103) as RequisitionDate1 from tbl_RequisitionPop where ReqPopId='" + ReqPopId + "'");
        if (dt.Rows.Count > 0)
        {
            txtReqId1.Text = dt.Rows[0]["BranchId"].ToString();
            txtBranchName1.Text = dt.Rows[0]["Status4"].ToString();
            txtUserGroup1.Text = dt.Rows[0]["Status5"].ToString();
            lblUserGroupId1.Text = dt.Rows[0]["UserGroupId"].ToString();
            txtReqUser1.Text = dt.Rows[0]["UserId"].ToString();
            dtpDate1.Date = Convert.ToDateTime(dt.Rows[0]["RequisitionDate1"].ToString());
            ddlRequisitionPur1.Text = dt.Rows[0]["RequisitionPurpose"].ToString();
            ddlRequisitionPurId1.Text = dt.Rows[0]["PrioritiesId"].ToString();
            ddlRequisitionto1.Text = dt.Rows[0]["ReUserName"].ToString();
            ddlRequisitiontoId1.Text = dt.Rows[0]["ReUserId"].ToString();
            ddlRequToGrp1.Text = dt.Rows[0]["ReUserGroupName"].ToString();
            ddlRequToGrpId1.Text = dt.Rows[0]["ReUserGroupId"].ToString();
            ddlRequToUser1.Text = dt.Rows[0]["Status"].ToString();
            txtRemarks1.Text = dt.Rows[0]["Remarks"].ToString();
            txtPopId.Text = dt.Rows[0]["ReqPopId"].ToString();
            ddlProject1.Text = dt.Rows[0]["Status1"].ToString();
            ddlProjectId1.Text = dt.Rows[0]["Status2"].ToString();


        }

        string BranchIdPop = txtReqId1.Text;
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        DataTable dt1 = (DataTable)ViewState["ItemDetailsApp"];
        DataTable Items = DBAccess.FetchDatatable(@" select a.*,b.* ,c.*,'' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdPop + "'  and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as POPQty1,  (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchId + "'  and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as ReqToQty1   from tbl_RequisitionPopDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId left join tbl_HSNCodeMaster c on b.HSNCode=c.HSNCode  where ReqPopId='" + ReqPopId + "'");

        if (Items.Rows.Count > 0)
        {
            for (int i = 0; i < Items.Rows.Count; i++)
            {
                string ReqPopItemId = Items.Rows[i]["ReqPopItemId"].ToString();
                Session["ReqPopItemId"] = ReqPopItemId.ToString();
                string ItemId = Items.Rows[i]["ItemId"].ToString();
                string Category = Items.Rows[i]["Category"].ToString();
                string Type = Items.Rows[i]["Type"].ToString();
                string ItemName = Items.Rows[i]["ItemName"].ToString();
                string Make = Items.Rows[i]["Make"].ToString();
                string Model = Items.Rows[i]["Model"].ToString();
                string Qty = Items.Rows[i]["Qty"].ToString();
                string rate = Items.Rows[i]["rate"].ToString();
                string HSNCode = Items.Rows[i]["HSNCode"].ToString();
                string CGST = Items.Rows[i]["CGST"].ToString();
                string IGST = Items.Rows[i]["IGST"].ToString();
                string SGST = Items.Rows[i]["SGST"].ToString();
                string ReqToQty = Items.Rows[i]["ReqToQty1"].ToString();
                string POPQty = Items.Rows[i]["POPQty1"].ToString();
                

                DataRow drH = dt1.NewRow();
                drH["ReqPopItemId"] = Items.Rows[i]["ReqPopItemId"].ToString();
                drH["ItemId"] = Items.Rows[i]["ItemId"].ToString();
                drH["Category"] = Items.Rows[i]["Category"].ToString();
                drH["Type"] = Items.Rows[i]["Type"].ToString();
                drH["ItemName"] = Items.Rows[i]["ItemName"].ToString();
                drH["Make"] = Items.Rows[i]["Make"].ToString();
                drH["Model"] = Items.Rows[i]["Model"].ToString();
                drH["Unit"] = Items.Rows[i]["Unit"].ToString();
                drH["Qty"] = Items.Rows[i]["Qty"].ToString();
                drH["rate"] = Items.Rows[i]["rate"].ToString();
                drH["HSNCode"] = Items.Rows[i]["HSNCode"].ToString();
                drH["CGST"] = Items.Rows[i]["CGST"].ToString();
                drH["IGST"] = Items.Rows[i]["IGST"].ToString();
                drH["SGST"] = Items.Rows[i]["SGST"].ToString();
                drH["ReqToQty"] = Items.Rows[i]["ReqToQty1"].ToString();
                drH["POPQty"] = Items.Rows[i]["POPQty1"].ToString();

                dt1.Rows.Add(drH);
            }
            ViewState["ItemDetailsApp"] = dt1;
            gvItemDetailsApp.DataSource = ViewState["ItemDetailsApp"] as DataTable;
            gvItemDetailsApp.DataBind();
            ViewState["ReqPopId"] = ReqPopId;
            tblApp.Visible = true;
            tblFor.Visible = false;
            btnChooseRawMaterial.Visible = false;

            #region ButtonTextChange

            if (Status == "Approval")
            {
                btnSave.Text = "Approve And Forword";
            }
            //else if (Status == "Revision")
            //{
            //    btnSave.Text = "SAVE";
            //}
            //else if (Status == "ShortClose")
            //{
            //    btnSave.Text = "ShortClose";
            //}

            #endregion ButtonTextChange

        }


    }
    public void FatchDataFromForwordPop(string ReqPopId, string Status)
    {
        DataTable dt = DBAccess.FetchDatatable(@"select *, convert(varchar,RequisitionDate,103) as RequisitionDate1 from tbl_RequisitionPop where ReqPopId='" + ReqPopId + "'");
        if (dt.Rows.Count > 0)
        {
            ddlRequisitionPur.Value = dt.Rows[0]["PrioritiesId"].ToString();
            ddlProject.Value = dt.Rows[0]["Status1"].ToString();
            ddlRequisitionPur.Enabled = false;
            ddlProject.Enabled = false;
        }
        string BranchIdPop = txtReqId1.Text;
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }

        DataTable dt1 = (DataTable)ViewState["ItemDetails"];
        DataTable Items = DBAccess.FetchDatatable(@" select a.*,b.* ,c.*,'' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchIdPop + "'  and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as POPQty1,  (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=a.ItemId and BranchId='" + BranchId + "'  and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as ReqToQty1   from tbl_RequisitionPopDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId left join tbl_HSNCodeMaster c on b.HSNCode=c.HSNCode  where ReqPopId='" + ReqPopId + "'");

        if (Items.Rows.Count > 0)
        {
            for (int i = 0; i < Items.Rows.Count; i++)
            {
                for (int b = 0; b < gvItemDetailsApp.Rows.Count; b++, i++)
                {

                    TextBox txtItemQty = (TextBox)gvItemDetailsApp.Rows[b].FindControl("txtItemQty");
                    CheckBox chk = (CheckBox)gvItemDetailsApp.Rows[b].FindControl("chk");
                    decimal AppQty1 = Convert.ToDecimal(txtItemQty.Text);
                    Session["AppQty"] = AppQty1;
                    if (chk.Checked == true)
                    {

                        string ReqPopItemId = Items.Rows[i]["ReqPopItemId"].ToString();
                        Session["ReqPopItemId"] = ReqPopItemId.ToString();
                        string ItemId = Items.Rows[i]["ItemId"].ToString();
                        string Category = Items.Rows[i]["Category"].ToString();
                        string Type = Items.Rows[i]["Type"].ToString();
                        string ItemName = Items.Rows[i]["ItemName"].ToString();
                        string Make = Items.Rows[i]["Make"].ToString();
                        string Model = Items.Rows[i]["Model"].ToString();
                        string Qty = Items.Rows[i]["Qty"].ToString();
                        string rate = Items.Rows[i]["rate"].ToString();
                        string HSNCode = Items.Rows[i]["HSNCode"].ToString();
                        string CGST = Items.Rows[i]["CGST"].ToString();
                        string IGST = Items.Rows[i]["IGST"].ToString();
                        string SGST = Items.Rows[i]["SGST"].ToString();
                        string ReqToQty = Items.Rows[i]["ReqToQty1"].ToString();
                        string POPQty = Items.Rows[i]["POPQty1"].ToString();
                        decimal Qty1 = Convert.ToDecimal(Qty);
                        decimal AppQty = Convert.ToDecimal(Session["AppQty"]);
                        decimal Total = Qty1 - AppQty;
                        string ReqQty1 = DBAccess.FetchDatasingle("select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId + "' and ReqPopItemId='" + ReqPopItemId + "'");
                        decimal ReqQty = Convert.ToDecimal(ReqQty1);

                        if ( Total < 0)
                        {
                            tblFor.Visible = false;
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Its 0 Not able to Forword.');", true);
                            return;

                        }
                        if (Total > ReqQty)
                        {
                            tblFor.Visible = false;
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It's More Than Requisitions Qty.');", true);
                            return;

                        }

                        DataRow drH = dt1.NewRow();
                        drH["ReqPopItemId"] = Items.Rows[i]["ReqPopItemId"].ToString();
                        drH["ItemId"] = Items.Rows[i]["ItemId"].ToString();
                        drH["Category"] = Items.Rows[i]["Category"].ToString();
                        drH["Type"] = Items.Rows[i]["Type"].ToString();
                        drH["ItemName"] = Items.Rows[i]["ItemName"].ToString();
                        drH["Make"] = Items.Rows[i]["Make"].ToString();
                        drH["Model"] = Items.Rows[i]["Model"].ToString();
                        drH["Unit"] = Items.Rows[i]["Unit"].ToString();
                        drH["Qty"] = Total;
                        drH["rate"] = Items.Rows[i]["rate"].ToString();
                        drH["HSNCode"] = Items.Rows[i]["HSNCode"].ToString();
                        drH["CGST"] = Items.Rows[i]["CGST"].ToString();
                        drH["IGST"] = Items.Rows[i]["IGST"].ToString();
                        drH["SGST"] = Items.Rows[i]["SGST"].ToString();
                        drH["ReqToQty"] = Items.Rows[i]["ReqToQty1"].ToString();
                        drH["POPQty"] = Items.Rows[i]["POPQty1"].ToString();

                        dt1.Rows.Add(drH);
                    }
                }
            }
            ViewState["ItemDetails"] = dt1;
            gvItemDetails.DataSource = ViewState["ItemDetails"] as DataTable;
            gvItemDetails.DataBind();
            ViewState["ReqPopId"] = ReqPopId;
            tblApp.Visible = true;

            btnChooseRawMaterial.Visible = false;

            #region ButtonTextChange

            if (Status == "Approval")
            {
                btnSave.Text = "Approve And Forword";
            }
            //else if (Status == "Revision")
            //{
            //    btnSave.Text = "SAVE";
            //}
            //else if (Status == "ShortClose")
            //{
            //    btnSave.Text = "ShortClose";
            //}

            #endregion ButtonTextChange

        }
    }
    protected DataTable getItem()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqPopItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Category";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Type";
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
        dtCol.ColumnName = "Unit";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Qty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "rate";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "HSNCode";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "IGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "SGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqToQty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "POPQty";
        oTable.Columns.Add(dtCol);

        return oTable;
    }
    protected DataTable getItemApp()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqPopItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Category";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Type";
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
        dtCol.ColumnName = "Unit";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Qty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "rate";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "HSNCode";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "IGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "SGST";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ReqToQty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "POPQty";
        oTable.Columns.Add(dtCol);

        return oTable;
    }
    protected void gvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemDetails.PageIndex = e.NewPageIndex;
        this.gvItemDetails.DataSource = (DataTable)ViewState["ItemDetails"];
        this.gvItemDetails.DataBind();
    }
    protected void gvItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt8 = new DataTable();
            dt8 = (DataTable)ViewState["ItemDetails"];
            dt8.Rows[rowindex].Delete();
            dt8.AcceptChanges();
            ViewState["ItemDetails"] = dt8;
            gvItemDetails.DataSource = dt8;
            gvItemDetails.DataBind();
        }
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnChooseRawMaterial_Click(object sender, EventArgs e)
    {
        string ReUserId = "";
        try
        {
            ReUserId = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to.');", true);
            return;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
    }
    protected void btnAddServiceItem_Click(object sender, EventArgs e)
    {
        string RequisitiontoBranchId = "";
        try
        {
            RequisitiontoBranchId = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to BranchId');", true);
            return;
        }
        string BranchId = "";
        try
        {
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        string ItemId = "";
        List<object> fieldValues = gvItem.GetSelectedFieldValues(new string[] { "ItemId" });
        if (fieldValues.Count != 0)
        {
            DataTable AppQtn = new DataTable();

            for (int j = 0; j < fieldValues.Count; j++)
            {
                ItemId = fieldValues[j].ToString();

                DataTable dt8 = (DataTable)ViewState["ItemDetails"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        if (ItemId == dt8.Rows[i]["ItemId"].ToString())
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
                            return;
                        }
                    }
                }

                DataTable dtI = (DataTable)ViewState["ItemDetails"];
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty,h.CGST,h.IGST,h.SGST,h.CESS, '' as RQty, '' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where  ItemId=rm.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as POPQty, (select sum(Qty) from tbl_RackStockInBarcodeDetails where  ItemId=rm.ItemId and BranchId='" + RequisitiontoBranchId + "' and (Status1 is null or Status1 ='')  and (Status2 is null or Status2 ='')) as ReqToQty from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode where rm.ItemId='" + ItemId + "'");
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                    DataRow drH = dtI.NewRow();
                    drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                    drH["Category"] = dt11.Rows[0]["Category"].ToString();
                    drH["Type"] = dt11.Rows[0]["Type"].ToString();
                    drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                    drH["Make"] = dt11.Rows[0]["Make"].ToString();
                    drH["Model"] = dt11.Rows[0]["Model"].ToString();
                    drH["Unit"] = dt11.Rows[0]["Unit"].ToString();
                    drH["Qty"] = dt11.Rows[0]["Qty"].ToString();
                    drH["rate"] = dt11.Rows[0]["rate"].ToString();
                    drH["HSNCode"] = dt11.Rows[0]["HSNCode"].ToString();
                    drH["CGST"] = dt11.Rows[0]["CGST"].ToString();
                    drH["IGST"] = dt11.Rows[0]["IGST"].ToString();
                    drH["SGST"] = dt11.Rows[0]["SGST"].ToString();
                    drH["ReqToQty"] = dt11.Rows[0]["ReqToQty"].ToString();
                    drH["POPQty"] = dt11.Rows[0]["POPQty"].ToString();

                    dtI.Rows.Add(drH);

                    ViewState["ItemDetails"] = dtI;
                    gvItemDetails.DataSource = dtI;
                    gvItemDetails.DataBind();
                }

            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequisitionTerrytory..aspx");
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

        if (btnSave.Text == "Save")
        {

            string ReqTetId = DBAccess.FetchDatatable("select [dbo].[fn_ReqTetId]()").Rows[0][0].ToString();
            string RequisitionDate = dtpDate.Text;
            string PrioritiesId = "";
            try
            {
                PrioritiesId = ddlRequisitionPur.Value.ToString();
            }
            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition Purpose .');", true);
                return;
            }
            string RequisitionPurpose = ddlRequisitionPur.Text;
            string ReUserGroupId = ddlRequToGrp.SelectedValue.ToString();
            string ReUserGroupName = ddlRequToGrp.SelectedItem.ToString();
            string ReUserId = ddlRequisitionto.Value.ToString();
            string ReUserName = ddlRequisitionto.Text;
            string Remarks = txtRemarks.Text;
            string Status = ddlRequToUser.Text;
            string Status1 = ddlProject.Text;
            string Status2 = "";
            try
            {
                Status2 = ddlProject.Value.ToString();
            }
            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Project.');", true);
                return;
            }
            string Status3 = "";
            string Status4 = txtBranchName.Text;
            string Status5 = txtUserGroup.Text;
            string Forword = "";
            string ReqPopId = "";
            string ReqSecId = "";


            int n = ClassRequisitions.RequisitionsTetSave(ReqTetId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, UserGroupId, UserId, BranchId, DOE, Forword, ReqPopId, ReqSecId);


            if (n == 1)
            {
                for (int i = 0; i < gvItemDetails.Rows.Count; i++)
                {
                    string ReqTetItemId = DBAccess.FetchDatatable("select [dbo].[fn_ReqTetItemId]()").Rows[0][0].ToString();
                    string ItemId = gvItemDetails.Rows[i].Cells[2].Text;
                    string Category = gvItemDetails.Rows[i].Cells[3].Text;
                    string ItemName = gvItemDetails.Rows[i].Cells[5].Text;
                    string Make = gvItemDetails.Rows[i].Cells[6].Text;
                    string Model = gvItemDetails.Rows[i].Cells[7].Text;
                    string Unit = gvItemDetails.Rows[i].Cells[8].Text;
                    decimal ReqToQty = 0;
                    decimal POPQty = 0;
                    string Status6 = "";

                    try
                    {
                        ReqToQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[9].Text);
                        POPQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[10].Text);
                    }
                    catch
                    { }
                    TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                    TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                    decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                    decimal Qty1 = 0;
                    try { 
                     Qty1 = Convert.ToDecimal(txtRate.Text);
}
                    catch
                    {

                    }
                    if (ReqToQty < Qty)
                    {
                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It Is More Than Stock In Hand (To) Qty..');", true);
                        return;
                    }

                    //string ReqQtyPop1 = DBAccess.FetchDatasingle("select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId1 + "' and ReqPopItemId='" + ReqPopItemId1 + "'");
                    //decimal ReqQtyPop = Convert.ToDecimal(ReqQtyPop1);

                    //string ReqQtySec1 = DBAccess.FetchDatasingle("select Qty from tbl_RequisitionSectionDetails where ReqSecId='" + ReqSecId1 + "' and ReqPopItemId='" + ReqSecItemId1 + "'");
                    //decimal ReqQtySec = Convert.ToDecimal(ReqQtySec1);

                    //if (Qty > ReqQtyPop)
                    //{
                    //    bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                    //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It Is More Than Requisitions Qty.');", true);
                    //    return;
                    //}

                    //if (Qty > ReqQtySec)
                    //{
                    //    bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                    //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! It Is More Than Requisitions Qty.');", true);
                    //    return;
                    //}

                    int m = ClassRequisitions.RequisitionsTetSaveDetails(ReqTetId, ReqTetItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);

                    if (m == 1)
                    {

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionTerrytory..aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Sent Successfully. Try again.');", true);
                        return;
                    }
                }
            }
        }


        else if (btnSave.Text == "Approve And Forword")
        {

            if (ChkFor.Checked == true)
            {
                #region Save
                string ReqSecAppId = DBAccess.FetchDatatable("select [dbo].[fn_ReqSecAppId]()").Rows[0][0].ToString();
                string ReqPopAppId = DBAccess.FetchDatatable("select [dbo].[fn_ReqPopAppId]()").Rows[0][0].ToString();

                string ReqPopId = Request.QueryString["ReqPopId"];
                string ReqSecId = Request.QueryString["ReqSecId"];

                string ReqTetId = DBAccess.FetchDatatable("select [dbo].[fn_ReqTetId]()").Rows[0][0].ToString();
                string RequisitionDate = dtpDate.Text;
                string PrioritiesId = "";
                try
                {
                    PrioritiesId = ddlRequisitionPur.Value.ToString();
                }
                catch
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition Purpose .');", true);
                    return;
                }
                string RequisitionPurpose = ddlRequisitionPur.Text;
                string ReUserGroupId = ddlRequToGrp.SelectedValue.ToString();
                string ReUserGroupName = ddlRequToGrp.SelectedItem.ToString();
                string ReUserId = "";
                try
                {
                     ReUserId = ddlRequisitionto.Value.ToString();
                }
                catch
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to.');", true);
                    return;
                }
                string ReUserName = ddlRequisitionto.Text;
                string Remarks = txtRemarks.Text;
                string Status = ddlRequToUser.Text;
                string Status1 = ddlProject.Text;
                string Status2 = "";
                try
                {
                    Status2 = ddlProject.Value.ToString();
                }
                catch
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Project.');", true);
                    return;
                }
                string Status3 = txtReqUser.Text;
                string Status4 = txtBranchName.Text;
                string Status5 = txtUserGroup.Text;
                string ReqPopId1 = txtPopId.Text;


                string RequisitionDate1 = dtpDate1.Text;
                string PrioritiesId1 = ddlRequisitionPurId1.Text;
                string RequisitionPurpose1 = ddlRequisitionPur1.Text;
                string ReUserGroupId1 = lblUserGroupId1.Text;
                string ReUserGroupName1 = ddlRequToGrp1.Text;
                string Remarks1 = txtRemarks1.Text;
                string ReUserId1 = ddlRequisitiontoId1.Text;
                string ReUserName1 = ddlRequisitionto1.Text;
                string Status11 = ddlRequToUser1.Text;
                string Status12 = ddlProject1.Text; ;
                string Status13 = ddlProjectId1.Text; ;
                string Status14 = txtReqUser1.Text;
                string Status15 = txtBranchName1.Text;
                string Status16 = txtUserGroup1.Text;
                string Forword = "";
                string FrorwordFrom = txtPopId.Text;
                if (FrorwordFrom == "")
                {
                    Forword = "";
                }
                else
                {
                    Forword = "Yes";
                }


                int n = ClassRequisitions.RequisitionsTetSave(ReqTetId, RequisitionDate, PrioritiesId, RequisitionPurpose, ReUserGroupId, ReUserGroupName, ReUserId, ReUserName, Remarks, Status, Status1, Status2, Status3, Status4, Status5, UserGroupId, UserId, BranchId, DOE, Forword, ReqPopId1, ReqSecId);
                int na = 0;
                int naa = 0;
                if (ReqPopId != null)
                {
                    na = ClassRequisitions.RequisitionsPOPSaveApp(ReqPopAppId, ReqPopId, RequisitionDate1, PrioritiesId1, RequisitionPurpose1, ReUserGroupId1, ReUserGroupName1, ReUserId1, ReUserName1, Remarks1, Status11, Status12, Status13, Status14, Status15, Status16, UserGroupId, UserId, BranchId, DOE);

                }
                else
                {
                    naa = ClassRequisitions.RequisitionsSecSaveApp(ReqSecAppId, ReqSecId, RequisitionDate1, PrioritiesId1, RequisitionPurpose1, ReUserGroupId1, ReUserGroupName1, ReUserId1, ReUserName1, Remarks1, Status11, Status12, Status13, Status14, Status15, Status16, UserGroupId, UserId, BranchId, DOE, Forword, FrorwordFrom);

                }
                #endregion
                #region Save POP Section Details
                if (n == 1)
                {

                    for (int i = 0; i < gvItemDetails.Rows.Count; i++)
                    {
                        string ReqPopItemId1 = "";
                        try
                        {
                            ReqPopItemId1 = gvItemDetails.Rows[i].Cells[1].Text;
                        }
                        catch
                        {

                        }
                        string ReqSecItemId1 = "";
                        try
                        {
                            ReqSecItemId1 = gvItemDetails.Rows[i].Cells[1].Text;
                        }
                        catch
                        {

                        }

                        string ReqTetItemId = DBAccess.FetchDatatable("select [dbo].[fn_ReqTetItemId]()").Rows[0][0].ToString();
                        //string ReqSecItemId = gvItemDetails.Rows[i].Cells[1].Text;
                        string ItemId = gvItemDetails.Rows[i].Cells[2].Text;
                        string Category = gvItemDetails.Rows[i].Cells[3].Text;
                        string ItemName = gvItemDetails.Rows[i].Cells[5].Text;
                        string Make = gvItemDetails.Rows[i].Cells[6].Text;
                        string Model = gvItemDetails.Rows[i].Cells[7].Text;
                        string Unit = gvItemDetails.Rows[i].Cells[8].Text;

                        decimal ReqToQty = 0;
                        try
                        {
                            ReqToQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[9].Text);
                           
                        }
                        catch
                        { }

                        decimal POPQty = 0;
                        try
                        {
                            POPQty = Convert.ToDecimal(gvItemDetails.Rows[i].Cells[10].Text);
                        }
                        catch
                        { }
                        string Status6 = "";

                        TextBox txtItemQty = (TextBox)gvItemDetails.Rows[i].FindControl("txtItemQty");
                        TextBox txtRate = (TextBox)gvItemDetails.Rows[i].FindControl("txtRate");
                        decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                        Session["SecPopQty"] = Qty;
                        decimal Qty1 = Convert.ToDecimal(txtRate.Text);
                        //if (ReqToQty < Qty)
                        //{

                        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Stock Not Avail.');", true);
                        //    return;
                        //}

                        DataTable ReqQtyPop1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId + "' and ReqPopItemId='" + ReqPopItemId1 + "'");
                        if (ReqQtyPop1.Rows.Count > 0)
                        {
                            for (int a = 0; a < ReqQtyPop1.Rows.Count; a++)
                            {
                                decimal ReqQtyPop = 0;
                                try
                                {
                                    ReqQtyPop = Convert.ToDecimal(ReqQtyPop1.Rows[a]["Qty"]);
                                    if (Qty > ReqQtyPop | ReqToQty < Qty1)
                                    {
                                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                        bool DeleteDe = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                        bool DeleteApp = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                        return;
                                    }
                                    
                                   
                                }
                                catch
                                {
                                }
                            }
                        }

                        DataTable ReqQtySec1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionSectionDetails where ReqSecId='" + ReqSecId + "' and ReqSecItemId='" + ReqSecItemId1 + "'");
                        if (ReqQtySec1.Rows.Count > 0)
                        {
                            for (int a = 0; a < ReqQtySec1.Rows.Count; a++)
                            {
                                decimal ReqQtySec = 0;
                                try
                                {
                                    ReqQtySec = Convert.ToDecimal(ReqQtySec1.Rows[a]["Qty"]);
                                    if (Qty > ReqQtySec | ReqToQty < Qty1)
                                    {
                                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                        bool DeleteDe = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                        bool DeleteApp = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionSecApproval]  where ReqSecAppId='" + ReqSecAppId + "' ");
                                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                        return;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        int m = ClassRequisitions.RequisitionsTetSaveDetails(ReqTetId, ReqTetItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);


                    }
                }

                if (n == 1)
                {
                    for (int i = 0; i < gvItemDetailsApp.Rows.Count; i++)
                    {
                        string ReqSecItemId = "";
                        string ReqPopItemId = "";
                        try
                        {

                            ReqSecItemId = gvItemDetailsApp.Rows[i].Cells[1].Text;

                        }
                        catch
                        {

                        }
                        try
                        {
                            ReqPopItemId = gvItemDetailsApp.Rows[i].Cells[1].Text;
                        }
                        catch
                        {

                        }
                        string ItemId = gvItemDetailsApp.Rows[i].Cells[2].Text;
                        string Category = gvItemDetailsApp.Rows[i].Cells[3].Text;
                        string ItemName = gvItemDetailsApp.Rows[i].Cells[5].Text;
                        string Make = gvItemDetailsApp.Rows[i].Cells[6].Text;
                        string Model = gvItemDetailsApp.Rows[i].Cells[7].Text;
                        string Unit = gvItemDetailsApp.Rows[i].Cells[8].Text;
                        decimal ReqToQty = 0;
                        decimal POPQty = 0;
                        string Status6 = "";

                        try
                        {
                            ReqToQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[9].Text);
                            POPQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[10].Text);
                        }
                        catch
                        { }
                        TextBox txtItemQty = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtItemQty");
                        TextBox txtRate = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtRate");
                        decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                        decimal Qty1 = Convert.ToDecimal(txtRate.Text);
                       

                         DataTable ReqQtyPop1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId + "' and ReqPopItemId='" + ReqPopItemId + "'");
                         if (ReqQtyPop1.Rows.Count > 0)
                         {
                             for (int a = 0; a < ReqQtyPop1.Rows.Count; a++)
                             {
                                 decimal ReqQtyPop = 0;
                                 decimal PopQty = 0;
                                 decimal total = 0;
                                 try
                                 {
                                     ReqQtyPop = Convert.ToDecimal(ReqQtyPop1.Rows[a]["Qty"]);
                                     PopQty = Convert.ToDecimal(Session["SecPopQty"]);
                                     total = PopQty + Qty;

                                     if (Qty > ReqQtyPop | ReqToQty < Qty)
                                     {
                                         bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                         bool Delete1 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                         bool Delete2 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                         ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                         return;
                                     }
                                     if (total > ReqQtyPop | ReqToQty < Qty)
                                     {
                                         bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                         bool Delete1 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                         bool Delete2 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                         ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                         return;
                                     }
                                 }
                                 catch
                                 {
                                 }
                             }
                         }
                       DataTable ReqQtySec1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionSectionDetails where ReqSecId='" + ReqSecId + "' and ReqSecItemId='" + ReqSecItemId + "'");
                       if (ReqQtySec1.Rows.Count > 0)
                       {
                           for (int a = 0; a < ReqQtySec1.Rows.Count; a++)
                           {
                               decimal ReqQtySec = 0;
                               decimal SecQty = 0;
                               decimal total = 0;
                               try
                               {
                                   ReqQtySec = Convert.ToDecimal(ReqQtySec1.Rows[a]["Qty"]);
                                   SecQty = Convert.ToDecimal(Session["SecPopQty"]);
                                   total = SecQty + Qty;

                                   if (Qty > ReqQtySec | ReqToQty < Qty)
                                   {
                                       bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionSecApproval]  where ReqSecAppId='" + ReqPopAppId + "' ");
                                       bool Delete1 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                       bool Delete2 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                       ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                       return;
                                   }
                                   if (total > ReqQtySec | ReqToQty < Qty)
                                   {
                                       bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionSecApproval]  where ReqSecAppId='" + ReqPopAppId + "' ");
                                       bool Delete1 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                       bool Delete2 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                       ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                       return;
                                   }
                               }
                               catch
                               {
                               }
                           }
                       }

                        int ma = 0;
                        if (ReqPopId != null)
                        {
                            ma = ClassRequisitions.RequisitionsPOPSaveDetailsApp(ReqPopAppId, ReqPopId, ReqPopItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);
                            string Status9 = "Approve";
                            bool UpdateReqPop = DBAccess.SaveData("update tbl_RequisitionPop set Status6 = '" + Status9 + "',  Status7 = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) where ReqPopId='" + ReqPopId + "'");
                        }
                        else
                        {
                            ma = ClassRequisitions.RequisitionsSecSaveDetailsApp(ReqSecAppId, ReqSecId, ReqSecItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);
                            string Status9 = "Approve";
                            bool UpdateReqSec = DBAccess.SaveData("update tbl_RequisitionSection set Status6 = '" + Status9 + "',  Status7 = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) where ReqSecId='" + ReqSecId + "'");
                        }
                    }
                    if (n == 1 | na == 1)
                    {

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionTerrytory..aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Sent Successfully. Try again.');", true);
                        return;
                    }
                }

            }
                #endregion
            else
            {
                #region Approve Only
                string ReqPopAppId = DBAccess.FetchDatatable("select [dbo].[fn_ReqPopAppId]()").Rows[0][0].ToString();
                string ReqSecAppId = DBAccess.FetchDatatable("select [dbo].[fn_ReqSecAppId]()").Rows[0][0].ToString();

                string ReqPopId = "";
                try
                {
                    ReqPopId = Request.QueryString["ReqPopId"];
                }
                catch
                {

                }
                string ReqSecId = "";
                try
                {
                    ReqSecId = Request.QueryString["ReqSecId"];
                }
                catch
                {

                }

                string ReqTetId = DBAccess.FetchDatatable("select [dbo].[fn_ReqTetId]()").Rows[0][0].ToString();

                string RequisitionDate1 = dtpDate1.Text;
                string PrioritiesId1 = ddlRequisitionPurId1.Text;
                string RequisitionPurpose1 = ddlRequisitionPur1.Text;
                string ReUserGroupId1 = lblUserGroupId1.Text;
                string ReUserGroupName1 = ddlRequToGrp1.Text;
                string Remarks1 = txtRemarks1.Text;
                string ReUserId1 = ddlRequisitiontoId1.Text;
                string ReUserName1 = ddlRequisitionto1.Text;
                string Status11 = ddlRequToUser1.Text;
                string Status12 = ddlProject1.Text; ;
                string Status13 = ddlProjectId1.Text; ;
                string Status14 = txtReqUser1.Text;
                string Status15 = txtBranchName1.Text;
                string Status16 = txtUserGroup1.Text;
                string Forword = "";
                string FrorwordFrom = txtPopId.Text;
                if (FrorwordFrom == "")
                {
                    Forword = "";
                }
                else
                {
                    Forword = "Yes";
                }
                int na = 0;
                int naa = 0;

                if (ReqPopId != null)
                {
                    naa = ClassRequisitions.RequisitionsPOPSaveApp(ReqPopAppId, ReqPopId, RequisitionDate1, PrioritiesId1, RequisitionPurpose1, ReUserGroupId1, ReUserGroupName1, ReUserId1, ReUserName1, Remarks1, Status11, Status12, Status13, Status14, Status15, Status16, UserGroupId, UserId, BranchId, DOE);

                }
                else
                {
                    na = ClassRequisitions.RequisitionsSecSaveApp(ReqSecAppId, ReqSecId, RequisitionDate1, PrioritiesId1, RequisitionPurpose1, ReUserGroupId1, ReUserGroupName1, ReUserId1, ReUserName1, Remarks1, Status11, Status12, Status13, Status14, Status15, Status16, UserGroupId, UserId, BranchId, DOE, Forword, FrorwordFrom);

                }


                if (na == 1 | naa == 1)
                {
                    for (int i = 0; i < gvItemDetailsApp.Rows.Count; i++)
                    {
                        
                        string ReqPopItemId = "";
                        
                        try
                        {
                            ReqPopItemId = gvItemDetailsApp.Rows[i].Cells[1].Text;
                        }
                        catch
                        {

                        }
                        

                        string ItemId = gvItemDetailsApp.Rows[i].Cells[2].Text;
                        string Category = gvItemDetailsApp.Rows[i].Cells[3].Text;
                        string ItemName = gvItemDetailsApp.Rows[i].Cells[5].Text;
                        string Make = gvItemDetailsApp.Rows[i].Cells[6].Text;
                        string Model = gvItemDetailsApp.Rows[i].Cells[7].Text;
                        string Unit = gvItemDetailsApp.Rows[i].Cells[8].Text;
                        string Status6 = "";
                        decimal ReqToQty = 0;
                        try
                        {
                            ReqToQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[9].Text);
                        }
                        catch
                        { }
                        decimal POPQty = 0;
                        try
                        {
                            POPQty = Convert.ToDecimal(gvItemDetailsApp.Rows[i].Cells[10].Text);
                        }
                        catch
                        { }

                        TextBox txtItemQty = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtItemQty");
                        TextBox txtRate = (TextBox)gvItemDetailsApp.Rows[i].FindControl("txtRate");
                        decimal Qty = Convert.ToDecimal(txtItemQty.Text);
                        decimal Qty1 = Convert.ToDecimal(txtRate.Text);

                        DataTable ReqQtyPop1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionPopDetails where ReqPopId='" + ReqPopId + "' and ReqPopItemId='" + ReqPopItemId + "'");
                        if (ReqQtyPop1.Rows.Count > 0)
                        {

                            for (int a = 0; a < ReqQtyPop1.Rows.Count; a++)
                            {
                                decimal ReqQtyPop = 0;
                                try
                                {
                                    ReqQtyPop = Convert.ToDecimal(ReqQtyPop1.Rows[a]["Qty"]);
                                    if (Qty == ReqQtyPop )
                                    {
                                       
                                    }
                                    if (ReqToQty < Qty)
                                    {
                                        bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                        bool Delete1 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                        bool Delete2 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                        return;
                                    }
                                    else 
                                    {
                                        //bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionPopApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                        //bool Delete1 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                        //bool Delete2 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                        //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                        //return;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }

                        string ReqSecItemId = "";
                        try
                        {

                            ReqSecItemId = gvItemDetailsApp.Rows[i].Cells[1].Text;

                        }
                        catch
                        {

                        }

                       DataTable ReqQtySec1 = DBAccess.FetchDatatable(@"select Qty from tbl_RequisitionSectionDetails where ReqSecId='" + ReqSecId + "' and ReqSecItemId='" + ReqSecItemId + "'");
                       if (ReqQtySec1.Rows.Count > 0)
                       {
                           for (int a = 0; a < ReqQtySec1.Rows.Count; a++)
                           {
                               decimal ReqQtySec = 0;
                               try
                               {
                                   ReqQtySec = Convert.ToDecimal(ReqQtySec1.Rows[a]["Qty"]);
                                   if (Qty == ReqQtySec )
                                   {
                                    
                                   }
                                   if (ReqToQty < Qty)
                                   {
                                       bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionSecApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                       bool Delete1 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                       bool Delete2 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                       ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                       return;
                                   }
                                   else
                                   {
                                       //bool Delete = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionSecApproval]  where ReqPopAppId='" + ReqPopAppId + "' ");
                                       //bool Delete1 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytory]  where ReqTetId='" + ReqTetId + "' ");
                                       //bool Delete2 = DBAccess.SaveData(@"delete from [dbo].[tbl_RequisitionTerrytoryDetails]  where ReqTetId='" + ReqTetId + "' ");
                                       //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Check The Qty. Someting Went Wrong.');", true);
                                       //return;
                                   }
                               }
                               catch
                               {
                               }
                           }
                       }

                        int ma = 0;
                        int maa = 0;

                        if (ReqPopId != null)
                        {
                            maa = ClassRequisitions.RequisitionsPOPSaveDetailsApp(ReqPopAppId, ReqPopId, ReqPopItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);
                            string Status9 = "Approve";
                            bool UpdateReqPop = DBAccess.SaveData("update tbl_RequisitionPop set Status6 = '" + Status9 + "',  Status7 = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) where ReqPopId='" + ReqPopId + "'");
                        }
                        else
                        {
                            ma = ClassRequisitions.RequisitionsSecSaveDetailsApp(ReqSecAppId, ReqSecId, ReqSecItemId, ItemId, Category, ItemName, Make, Model, Unit, ReqToQty, POPQty, Qty, Qty1, Status6, UserId, BranchId, DOE);
                            string Status9 = "Approve";
                            bool UpdateReqSec = DBAccess.SaveData("update tbl_RequisitionSection set Status6 = '" + Status9 + "',  Status7 = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) where ReqSecId='" + ReqSecId + "'");
                        }

                    }

                    if (na == 1 | naa == 1)
                    {

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisitions Sent Successfully.');window.location='../Project/RequisitionTerrytory..aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Requisitions Sent Successfully. Try again.');", true);
                        return;
                    }
                }
            }

                #endregion
        }
    }
    protected void ddlRequToGrp_TextChanged(object sender, EventArgs e)
    {
        string PopName = txtBranchName.Text;
        string RequToGrp = ddlRequToGrp.SelectedValue.ToString();
        if (RequToGrp == "UG19")
        {
            DataTable dt1 = DBAccess.FetchDatatable("select Distinct a.RegionId  as Id, b.BranchId from tbl_HigherKeyMapping a join tbl_Branch b on a.RegionId=b.BranchName where TerrytoryId='" + PopName + "' ");
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                ddlRequisitionto.DataSource = dt1;
                ddlRequisitionto.Text = "Id";
                ddlRequisitionto.Value = "BranchId";
                ddlRequisitionto.DataBind();
                ddlRequisitionto.SelectedIndex = 0;
            }
        }
        else
        {
            DataTable dt1 = DBAccess.FetchDatatable("select Distinct UserName as Id, BranchId from tbl_User where UserGroup='" + RequToGrp + "' ");
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                ddlRequisitionto.DataSource = dt1;
                ddlRequisitionto.Text = "Id";
                ddlRequisitionto.Value = "BranchId";
                ddlRequisitionto.DataBind();
                ddlRequisitionto.SelectedIndex = 0;
            }
        }
        string Requisitionto ="";
        try
        {
            Requisitionto = ddlRequisitionto.Value.ToString();
        }
        catch{

        }
         
        DataTable dtt = DBAccess.FetchDatatable("select UserName from tbl_User where BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "'");
        if (dtt != null && dtt.Rows.Count > 0)
        {
            ddlRequToUser.DataSource = dtt;
            ddlRequToUser.Text = "UserName";
            ddlRequToUser.Value = "UserName";
            ddlRequToUser.DataBind();
            ddlRequToUser.SelectedIndex = 0;
        }
        else
        {
            ddlRequToUser.DataSource = "";
            ddlRequToUser.Text = "";
            ddlRequToUser.Value = "";
            ddlRequToUser.DataBind();
            ddlRequToUser.SelectedIndex = 0;
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Not Have UserId. Try again.');", true);
            return;

        }
    }
    protected void ddlRequisitionto_TextChanged(object sender, EventArgs e)
    {
        string PopName = txtBranchName.Text;
        string RequToGrp = ddlRequToGrp.SelectedValue.ToString();
        string Requisitionto = ddlRequisitionto.Value.ToString();


        DataTable dtt = DBAccess.FetchDatatable("select UserName from tbl_User where BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "'");
        if (dtt != null && dtt.Rows.Count > 0)
        {
            ddlRequToUser.DataSource = dtt;
            ddlRequToUser.Text = "UserName";
            ddlRequToUser.Value = "UserName";
            ddlRequToUser.DataBind();
            ddlRequToUser.SelectedIndex = 0;
        }
        else
        {
            ddlRequToUser.DataSource = "";
            ddlRequToUser.Text = "";
            ddlRequToUser.Value = "";
            ddlRequToUser.DataBind();
            ddlRequToUser.SelectedIndex = 0;
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Not Have UserId. Try again.');", true);
            return;

        }
    }
    protected void gvItemDetailsApp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemDetailsApp.PageIndex = e.NewPageIndex;
        this.gvItemDetailsApp.DataSource = (DataTable)ViewState["ItemDetailsApp"];
        this.gvItemDetailsApp.DataBind();
    }
    protected void gvItemDetailsApp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt8 = new DataTable();
            dt8 = (DataTable)ViewState["ItemDetailsApp"];
            dt8.Rows[rowindex].Delete();
            dt8.AcceptChanges();
            ViewState["ItemDetailsApp"] = dt8;
            gvItemDetailsApp.DataSource = dt8;
            gvItemDetailsApp.DataBind();
        }
    }
    protected void gvItemDetailsApp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvItemDetailsApp.Rows.Count; i++)
        {

            CheckBox chk = gvItemDetailsApp.Rows[i].FindControl("chk") as CheckBox;
            TextBox txtItemQty = gvItemDetailsApp.Rows[i].FindControl("txtItemQty") as TextBox;


            if (chk.Checked == true)
            {
                txtItemQty.Enabled = true;
                row_wantstoF.Visible = true;
                chk.Enabled = false;
                btnSave.Enabled = false;

            }
            else
            {
                //btnSave.Enabled = true;
                //btn_WantsToForword.Visible = false;
            }
        }
    }
    protected void btn_WantsToForword_Click(object sender, EventArgs e)
    {
        tblFor.Visible = true;
        ChkFor.Checked = true;
        if (ChkFor.Checked == true)
        {
            string ReqSecId = Request.QueryString["ReqSecId"];
            string ReqPopId = Request.QueryString["ReqPopId"];

            if (ReqSecId != null)
            {
                FatchDataFromForwordSec(ReqSecId, "Approval");
            }
            else if (ReqPopId != null)
            {
                FatchDataFromForwordPop(ReqPopId, "Approval");
            }
        }
        else
        {
            tblFor.Visible = false;
            
        }
        btn_WantsToForword.Visible = false;
        btnSave.Enabled = true;
    }

    protected void ddlRequisitionPur_TextChanged(object sender, EventArgs e)
    {
        string Reson = ddlRequisitionPur.Text;

        //btnChooseRawMaterial.Visible = false;

        if (Reson == "Faulty Replacement")
        {
            btnChooseRawMaterial.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id04').style.display='block'", true);
        }


    }
    protected void btnAddFaltyItem_Click(object sender, EventArgs e)
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
        string ItemId = "";
        string RequisitiontoBranchId = "";
        try
        {
            RequisitiontoBranchId = ddlRequisitionto.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to BranchId');", true);
            return;
        }

        List<object> fieldValues = ASPxGridView1.GetSelectedFieldValues(new string[] { "ItemId" });
        if (fieldValues.Count != 0)
        {
            DataTable AppQtn = new DataTable();

            for (int j = 0; j < fieldValues.Count; j++)
            {
                ItemId = fieldValues[j].ToString();

                DataTable dt8 = (DataTable)ViewState["ItemDetails"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        if (ItemId == dt8.Rows[i]["ItemId"].ToString())
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
                            return;
                        }
                    }
                }

                DataTable dtI = (DataTable)ViewState["ItemDetails"];
                DataTable dt11 = DBAccess.FetchDatatable("select  rm.ItemId, rm.*,'' as Qty, h.CGST, h.IGST, h.SGST, h.CESS, '' as RQty, '' as rate, (select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + BranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as POPQty,(select sum(Qty) from tbl_RackStockInBarcodeDetails where ItemId=rm.ItemId and BranchId='" + RequisitiontoBranchId + "' and (Status1 is null or Status1 ='') and (Status2 is null or Status2 ='')) as ReqToQty from  tbl_ItemMaster rm join dbo.tbl_HSNCodeMaster h on rm.HSNCode=h.HSNCode  where rm.ItemId='" + ItemId + "'");
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                    DataRow drH = dtI.NewRow();
                    drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                    drH["Category"] = dt11.Rows[0]["Category"].ToString();
                    drH["Type"] = dt11.Rows[0]["Type"].ToString();
                    drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                    drH["Make"] = dt11.Rows[0]["Make"].ToString();
                    drH["Model"] = dt11.Rows[0]["Model"].ToString();
                    drH["Unit"] = dt11.Rows[0]["Unit"].ToString();
                    drH["Qty"] = dt11.Rows[0]["Qty"].ToString();
                    drH["rate"] = dt11.Rows[0]["rate"].ToString();
                    drH["HSNCode"] = dt11.Rows[0]["HSNCode"].ToString();
                    drH["CGST"] = dt11.Rows[0]["CGST"].ToString();
                    drH["IGST"] = dt11.Rows[0]["IGST"].ToString();
                    drH["SGST"] = dt11.Rows[0]["SGST"].ToString();
                    drH["ReqToQty"] = dt11.Rows[0]["ReqToQty"].ToString();
                    drH["POPQty"] = dt11.Rows[0]["POPQty"].ToString();

                    dtI.Rows.Add(drH);

                    ViewState["ItemDetails"] = dtI;
                    gvItemDetails.DataSource = dtI;
                    gvItemDetails.DataBind();
                }

            }
        }
    }
    protected void btnCodeSerch_Click(object sender, EventArgs e)
    {
        string FaultyId = txtCode.Text;
        Session["FaultyId"] = FaultyId;

        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id05').style.display='block'", true);

    }
}