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
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

public partial class Project_FaultyItemReceice : System.Web.UI.Page
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("FaultyItemReceice.aspx");
    }
    protected void ddlFaultyId_TextChanged(object sender, EventArgs e)
    {
        string FaultyId = ddlFaultyId.Text;

        DataTable dt = DBAccess.FetchDatatable(@"select FaultyId, FaultyInWarId as Id, OEM as Make, RmaNo, ConsignNo, convert(varchar, RMADate,103) as RMADate1, convert(varchar, ConsignDate,103) as ConsignDate1 from tbl_FaultyInWarranty where FaultyId='" + FaultyId + "' union all select FaultyId, FaultyWarExId as Id, ToRepairer as Make, RmaNo, ConsignNo, convert(varchar, RMADate,103) as RMADate1, convert(varchar, ConsignDate,103) as ConsignDate1 from tbl_FaultyWarrantyExpired where FaultyId='" + FaultyId + "'");
        if (dt.Rows.Count > 0)
        {
            txtRMANumber.Text = dt.Rows[0]["RmaNo"].ToString();
            dtpRMADate.Date = Convert.ToDateTime(dt.Rows[0]["RMADate1"].ToString());
            txtOEMName.Text = dt.Rows[0]["Make"].ToString();
            txtConsignmentNo.Text = dt.Rows[0]["ConsignNo"].ToString();
            dtpConsignmentDate.Date = Convert.ToDateTime(dt.Rows[0]["ConsignDate1"].ToString());
            lbl.Text = dt.Rows[0]["Id"].ToString();

        }

        DataTable dt1 = DBAccess.FetchDatatable(@"select a.*, convert(varchar,a.WarrantyTo,103) as WarrantyTo1, convert(varchar,a.CoderLifeTo,103) as CoderLifeTo1,
case when a.CoderLifeTo < getdate() then 'Out' else 'In' end as Staus        
		from tbl_FaultyInWarrantyDetails a join tbl_FaultyInWarranty b on a.FaultyInWarId=b.FaultyInWarId 
		where b.FaultyId='" + FaultyId + "' union all select a.*, convert(varchar,a.WarrantyTo,103) as WarrantyTo1, convert(varchar,a.CoderLifeTo,103) as CoderLifeTo1, case when a.CoderLifeTo < getdate() then 'Out' else 'In' end as Staus from tbl_FaultyWarrantyExpiredDetails a join tbl_FaultyWarrantyExpired b on a.FaultyWarExId=b.FaultyWarExId where b.FaultyId='" + FaultyId + "'");
        if (dt1.Rows.Count > 0)
        {
            gvFaultyItemDetails.DataSource = dt1;
            gvFaultyItemDetails.DataBind();
        }



        for (int i = 0; i < gvFaultyItemDetails.Rows.Count; i++)
        {
            DropDownList dropd = gvFaultyItemDetails.Rows[i].FindControl("dropd") as DropDownList;
            string Scrap = "";
            try
            {
                Scrap = gvFaultyItemDetails.Rows[i].Cells[7].Text.ToString();
            }

            catch
            {

            }

            if (Scrap != "In")
            {
                dropd.Text = "Non-Repair";
                dropd.Enabled = false;
            }
            else
            {
                dropd.Enabled = true;
            }
        }

    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;

        string FaultyRecvId = DBAccess.FetchDatatable("select [dbo].[fn_FaultyRecvId]()").Rows[0][0].ToString();
        string FaultyId = ddlFaultyId.Text;
        string FaultySub = lbl.Text;
        string RecDate = dtpDate.Text;
        string RMANumber = txtRMANumber.Text;
        string RMADate = dtpRMADate.Text;
        string ConsignmentNo = txtConsignmentNo.Text;
        string ConsignmentDate = dtpConsignmentDate.Text;
        string OEMName = txtOEMName.Text;
        string Remarks = txtRemarks.Text;

        bool SaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_FaultyItemReceice] values ('" + FaultyRecvId + "','" + FaultyId + "','" + FaultySub + "', convert(datetime,'" + RecDate + "',103), '" + RMANumber + "', convert(datetime,'" + RMADate + "',103), '" + ConsignmentNo + "', convert(datetime,'" + ConsignmentDate + "',103), '" + OEMName + "', '" + Remarks + "', '" + UserId + "', '" + BranchId + "', convert(datetime,'" + DOE + "',103))");

        if (SaveDetails == true)
        {
            for (int i = 0; i < gvFaultyItemDetails.Rows.Count; i++)
            {
                TextBox txtNewBarcode = (TextBox)gvFaultyItemDetails.Rows[i].FindControl("txtNewBarcode");
                DropDownList dropd = gvFaultyItemDetails.Rows[i].FindControl("dropd") as DropDownList;

                string SrBarVodeID = gvFaultyItemDetails.Rows[i].Cells[0].Text;
                string Barcode = gvFaultyItemDetails.Rows[i].Cells[1].Text;
                string ItemId = gvFaultyItemDetails.Rows[i].Cells[2].Text;
                string Status1 = dropd.Text;
                string NewBarcode = txtNewBarcode.Text;
                string Extra = gvFaultyItemDetails.Rows[i].Cells[5].Text;
                string Extra1 = gvFaultyItemDetails.Rows[i].Cells[6].Text;
                decimal Qtyb = 1;

                bool MaterialIssueSaveDetails = DBAccess.SaveData(@"insert into [dbo].[tbl_FaultyItemReceiceDetails] values ('" + FaultyRecvId + "', '" + SrBarVodeID + "', '" + Barcode + "','" + ItemId + "','" + Status1 + "', '" + NewBarcode + "', '" + Extra + "', '" + Extra1 + "', '" + UserId + "', '" + BranchId + "', convert(datetime,'" + DOE + "',103))");

                if (MaterialIssueSaveDetails == true)
                {
                    if (NewBarcode !="")
                    {
                        bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set   Barcode='" + NewBarcode + "', DeadBarcode='" + Barcode + "', Qty=1, Status1='',  Status2='', Status3='" + Status1 + "' where SrBarVodeID='" + SrBarVodeID + "' and Barcode='" + Barcode + "' ");

                        //bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty+'" + Qtyb + "' where ItemId='" + ItemId + "' and BranchId='" + BranchId + "'");
                    }
                    
                    if (Status1 == "Non-Repair")
                    {
                        bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set   Status3='" + Status1 + "' where SrBarVodeID='" + SrBarVodeID + "' and Barcode='" + Barcode + "' ");
                    }
                    else
                    {
                        bool UpdateBarcode = DBAccess.SaveData(@"update tbl_RackStockInBarCodeDetails set Status3='" + Status1 + "' where SrBarVodeID='" + SrBarVodeID + "' and Barcode='" + Barcode + "' ");
                        //bool UpdateQty = DBAccess.SaveData(@"update tbl_RackStockInDetails set AvailableQty = AvailableQty+'" + Qtyb + "' where ItemId='" + ItemId + "' and BranchId='" + BranchId + "'");
                    }
                }
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Received Done Successfully.');window.location='../Project/FaultyItemReceice.aspx';", true);
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Received Not Done. Try again.');", true);
            return;
        }
    }

    protected void dropd_TextChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvFaultyItemDetails.Rows.Count; i++)
        {
            DropDownList dropd = gvFaultyItemDetails.Rows[i].FindControl("dropd") as DropDownList;
            TextBox txtNewBarcode = gvFaultyItemDetails.Rows[i].FindControl("txtNewBarcode") as TextBox;


            if (dropd.Text == "Replace")
            {
                txtNewBarcode.Visible = true;
            }
            else
            {
                txtNewBarcode.Visible = false;
                txtNewBarcode.Text = "";
            }
        }
    }


    protected void txtNewBarcode_TextChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvFaultyItemDetails.Rows.Count; i++)
        {
            DropDownList dropd = gvFaultyItemDetails.Rows[i].FindControl("dropd") as DropDownList;
            TextBox txtNewBarcode = gvFaultyItemDetails.Rows[i].FindControl("txtNewBarcode") as TextBox;
            string Barcode = txtNewBarcode.Text;
            DataTable dt1 = DBAccess.FetchDatatable(@"select * from tbl_RackStockInBarCodeDetails where Barcode='" + Barcode + "'");
            if (dt1.Rows.Count > 0)
            {
                txtNewBarcode.Text = "";
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Barcode Already Exits.');", true);
                return;
            }
        }
    }
}