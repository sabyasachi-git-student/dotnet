using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using DevExpress.Web;

public partial class Project_NotificationUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string UserId = "";
            if (Session["UserId"] != null)
            {
                UserId = Session["UserId"].ToString();
            }

            string ProjectId = "";
            string CompanyId = "";



            try
            {
                CompanyId = Session["CompanyId"].ToString();
            }

            catch
            {

            }
            try
            {
                ProjectId = Session["BranchId"].ToString();
            }

            catch
            {

            }
            Session["ProjectId"] = ProjectId;
            FarchData();
        }
    }


    public void FarchData()
    {
        string userid = "";
        if (Session["userid"] != null)
        {
            userid = Session["userid"].ToString();
        }
        string projectid = "";
        string companyid = "";
        try
        {
            companyid = Session["companyid"].ToString();
        }
        catch
        {

        }
        try
        {
            projectid = Session["branchid"].ToString();
        }
        catch
        {

        }
        Session["projectid"] = projectid;

        string NewSo = DBAccess.FetchDatasingle("select count (Distinct ReqPopId) from tbl_RequisitionPop where  ReUserId = '" + projectid + "' and (Status6 is null or Status6='')");
        if (NewSo != "")
        {
            lblNewSO.Text = NewSo;
        }

        string NewConvertSo = DBAccess.FetchDatasingle("select count (Distinct ReqSecId) from tbl_RequisitionSection where  ReUserId =  '" + projectid + "' and (Status6 is null or Status6='')");
        if (NewConvertSo != "")
        {
            lblNewConvertSO.Text = NewConvertSo;
        }

        string ActionDue = DBAccess.FetchDatasingle("select count (Distinct ReqTetId) from tbl_RequisitionTerrytory where  ReUserId =  '" + projectid + "' and (Status6 is null or Status6='')");
        if (ActionDue != "")
        {
            lblActionDue.Text = ActionDue;
        }

        string DispatchIn7 = DBAccess.FetchDatasingle("select count (Distinct ReqtransId) from tbl_RequisitionTransfer where  ReUserId =  '" + projectid + "' and (Status6 is null or Status6='')");
        if (DispatchIn7 != "")
        {
            lblDispatchIn7.Text = DispatchIn7;
        }

        string MaterialIssue = DBAccess.FetchDatasingle("select count  (Distinct ReqAppId) from (select ReqPopAppId as ReqAppId from tbl_RequisitionPopApproval where ReqPopAppId not in (select ReqAppId from tbl_MaterialIssue) and BranchId='" + projectid + "' union all select ReqSecAppId as ReqAppId from tbl_RequisitionSecApproval where ReqSecAppId not in (select ReqAppId from tbl_MaterialIssue) and BranchId='" + projectid + "' union all select ReqTetAppId as ReqAppId from tbl_RequisitionTetApproval where ReqTetAppId not in (select ReqAppId from tbl_MaterialIssue) and BranchId='" + projectid + "' union all select ReqNOCAppId as ReqAppId from tbl_RequisitionNOCApproval where ReqNOCAppId not in (select ReqAppId from tbl_MaterialIssue) and BranchId='" + projectid + "' union all select ReqProId as ReqAppId from tbl_RequisitionProject where ReqProId not in (select ReqAppId from tbl_MaterialIssue) and BranchId='" + projectid + "') as a");
        if (MaterialIssue != "")
        {
            lblMaterialIssue.Text = MaterialIssue;
        }

        string Shipment = DBAccess.FetchDatasingle("select count (distinct Issueid) from (select distinct Issueid from tbl_MaterialIssue where Issueid not in (select Issueid from tbl_MaterialShipment) and BranchId='" + projectid + "' Union  select distinct TemIssueId as Issueid from tbl_TemporaryStockIssue where TemIssueId not in (select Issueid from tbl_MaterialShipment) and BranchId='" + projectid + "') as b");
        if (Shipment != "")
        {
            lblShipment.Text = Shipment;
        }

        string GRNUpdate = DBAccess.FetchDatasingle("select count ( distinct StockInId) from (Select distinct rsd.StockInId, s.POID, s.ChallanNo, s.InvoiceNo, rsd.GRNNo, sm.CompanyName FROM tbl_RackStockInDetails rsd join tbl_ItemMaster im on im.ItemId=rsd.ItemID  join tbl_StockIn s on rsd.StockInId=s.StockInId join tbl_SupplierMasterEntry sm on s.SupplierId=sm.SupplierId where (rsd.GRNNo is Null or rsd.GRNNo='') and rsd.BranchId='" + projectid + "') as c");
        if (GRNUpdate != "")
        {
            lblGRNUpdate.Text = GRNUpdate;
        }

        string WarrantyNoti = DBAccess.FetchDatasingle("select count(Id) as a from tbl_RackStockInBarCodeDetails where BranchId='" + projectid + "' and  WarrantyTo < DATEADD(MM,4,GETDATE())");
        if (WarrantyNoti != "")
        {
            lblWarrantyNoti.Text = WarrantyNoti;
        }
        
    }
    protected void btnNewSO_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequisitionPopApp.aspx");

    }
    protected void btnNewConvertSO_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequisitionSecApp.aspx");
    }

    protected void Tet_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequisitionTetApp.aspx");
    }

    protected void btnDispatchIn7_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequisitionTransferApp.aspx");
    }
    protected void btnMaterialIssue_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaterialIssuePop.aspx");
    }
    protected void btnShipment_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaterialShipment.aspx");
    }
    protected void btnGRNUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect("GRNNoUpdate.aspx");
    }
    protected void btnWarrantyNoti_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
    }
}