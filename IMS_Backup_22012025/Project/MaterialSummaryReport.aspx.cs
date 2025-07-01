using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Project_MaterialSummaryReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void txtSerial_TextChanged(object sender, EventArgs e)
    {
        if (txtSerial.Text != "")
        {
            string SerialNo = txtSerial.Text;
            DataTable dtGroup = DBAccess.FetchData("select top 1 a.ItemId, sm.CompanyName, i.Category, i.Type, i.ItemName, i.Make, i.Model, i.HSNCode, a.Row, a.Rack, a.Shelf, a.StockInId, Convert(varchar, a.WarrantyTo,103) as WarrantyTo, Convert(varchar, a.CoderLifeTo,103) as CoderLifeTo,Convert(varchar,b.StockInDate,103) as StockInDate, b.ChallanNo, b.InvoiceNo, b.ConsignmentNo, r.GRNNo, b.POID,a.branchId, s.branchName,case when a.Status1='' then 'Material Available' when a.Status1='Issue' then 'Material Issue' when a.Status1='InUse' then 'Material In Use' when a.Status1='Faulty' then 'Material Faulty' when a.Status1='Scrap' then 'Material Scrap' end as Status from tbl_RackStockInBarCodeDetails a join tbl_StockIn b on a.StockInId = b.StockInId join tbl_RackStockInDetails r on a.StockInId = r.StockInId join tbl_ItemMaster i on a.ItemId=i.ItemId join tbl_SupplierMasterEntry sm on b.SupplierId=sm.SupplierId join tbl_Branch s on s.branchId=a.BranchID where a.Barcode='" + SerialNo + "' order by a.id desc").Tables[0];
            if (dtGroup != null && dtGroup.Rows.Count > 0)
            {
                lblGroup.Text = dtGroup.Rows[0]["ItemId"].ToString();
                lblCategory.Text = dtGroup.Rows[0]["Category"].ToString();
                lblMake.Text = dtGroup.Rows[0]["Make"].ToString();
                lblModel.Text = dtGroup.Rows[0]["Model"].ToString();
                lblUnit.Text = dtGroup.Rows[0]["HSNCode"].ToString();
                lblItem.Text = dtGroup.Rows[0]["ItemName"].ToString();

                lblStockInId.Text = dtGroup.Rows[0]["StockInId"].ToString();
                lblStockInDate.Text = dtGroup.Rows[0]["StockInDate"].ToString();
                lblSupplier.Text = dtGroup.Rows[0]["CompanyName"].ToString();
                lblStatus.Text = dtGroup.Rows[0]["Status"].ToString();
                lblGRN.Text = dtGroup.Rows[0]["GRNNo"].ToString();
                lblInStock.Text = dtGroup.Rows[0]["POID"].ToString();
                lblInvoice.Text = dtGroup.Rows[0]["InvoiceNo"].ToString();
                lblInvoiceDate.Text = dtGroup.Rows[0]["ConsignmentNo"].ToString();

                lblLocation.Text = dtGroup.Rows[0]["branchName"].ToString();
                lblWarrantyDate.Text = dtGroup.Rows[0]["WarrantyTo"].ToString();

                div_IDetails.Visible = true;
                div_StockInDetails.Visible = true;
                div_PresentStatus.Visible = true;
                //LoadStockOut(SerialNo);
            }
            else
            {
                div_IDetails.Visible = false;
                div_StockOut.Visible = false;
                div_StockInDetails.Visible = false;
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Serial No is not Available!!');", true);
            }
        }
        else
        {
            div_IDetails.Visible = false;
            div_StockOut.Visible = false;
            div_StockInDetails.Visible = false;
        }
    }

    private void LoadStockOut(string SerialNo)
    {
        DataTable dtGroup = DBAccess.FetchData("exec StockoutDetailsBySerialNo @SerialNo='" + SerialNo + "'").Tables[0];
        if (dtGroup != null && dtGroup.Rows.Count > 0)
        {
            lblStockOut.Text = dtGroup.Rows[0]["StockOutID"].ToString();
            lblStockOutDate.Text = dtGroup.Rows[0]["Date"].ToString();
            lblSO.Text = dtGroup.Rows[0]["SO_ID"].ToString();
            lblSODate.Text = dtGroup.Rows[0]["SO_Date"].ToString();
            lblCustomer.Text = dtGroup.Rows[0]["CompanyName"].ToString();
            lblSale.Text = dtGroup.Rows[0]["SaleType"].ToString();
            lblCommissioning.Text = dtGroup.Rows[0]["Commissioning"].ToString();
            //lblInStock.Text = "No";
            div_StockOut.Visible = true;


            DataTable dt = DBAccess.FetchData("select top(1) DamageId,WarrantyStatus,SendStatus,convert(varchar,Dates,103) as 'Dates' from tbl_DamageCollection where SerialNo='" + SerialNo + "' order by RowID desc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                lblDamage.Text = "Yes";
                lblDamageId.Text = dt.Rows[0]["DamageId"].ToString();
                lblWarranty.Text = dt.Rows[0]["WarrantyStatus"].ToString();
                lblCollectionDate.Text = dt.Rows[0]["Dates"].ToString();
                bool SendStatus = false;
                try
                {
                    SendStatus = Convert.ToBoolean(dt.Rows[0]["SendStatus"]);
                }
                catch { }
                if (SendStatus == true)
                {
                    bool RecollectedStatus = false;
                    DataTable dt1 = DBAccess.FetchData("select SupplierName,SupplierId,convert(varchar,Dates,103) as 'Dates',RecollectStatus from  [dbo].[tbl_DamageToSupplier] where DamageId='" + lblDamageId.Text + "' and SerialNo='" + SerialNo + "'").Tables[0];
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {

                        lblSent.Text = "SUPPLIER";
                        lblSentDate.Text = dt1.Rows[0]["Dates"].ToString();
                        idSent.Visible = true;
                        lblCurrentStatus.Text = "Sent To Supplier";
                        try
                        {
                            RecollectedStatus = Convert.ToBoolean(dt1.Rows[0]["RecollectStatus"]);
                        }
                        catch { }
                    }
                    else
                    {
                        DataTable dt2 = DBAccess.FetchData("select RepairerId,RepairerName,convert(varchar,Dates,103) as 'Dates',RecollectStatus from  [dbo].[tbl_DamageToRepairer] where DamageId='" + lblDamageId.Text + "' and SerialNo='" + SerialNo + "'").Tables[0];
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            lblSent.Text = dt2.Rows[0]["RepairerName"].ToString() + " ( Repaier )";
                            lblSentDate.Text = dt2.Rows[0]["Dates"].ToString();
                            idSent.Visible = true;
                            lblCurrentStatus.Text = "Sent To Repairer";
                            try
                            {
                                RecollectedStatus = Convert.ToBoolean(dt2.Rows[0]["RecollectStatus"]);
                            }
                            catch { }
                        }

                    }
                    if (RecollectedStatus == true)
                    {
                        DataTable dt3 = DBAccess.FetchData("select RStatus,Remarks,convert(varchar,Dates,103) as 'Dates',SendToCustomerStatus,CollectFrom from [dbo].[tbl_DamageRecollect]  where DamageId='" + lblDamageId.Text + "' and SerialNo='" + SerialNo + "'").Tables[0];
                        if (dt3 != null && dt3.Rows.Count > 0)
                        {
                            lblRecollectionStatus.Text = dt3.Rows[0]["RStatus"].ToString();
                            lblRecollectionDate.Text = dt3.Rows[0]["Dates"].ToString();
                            lblCurrentStatus.Text = "Recollected from " + dt3.Rows[0]["CollectFrom"].ToString();
                            idRecollection.Visible = true;
                            bool SendToCustomerStatus = false;
                            try
                            {
                                SendToCustomerStatus = Convert.ToBoolean(dt3.Rows[0]["SendToCustomerStatus"]);
                            }
                            catch { }
                            if (SendToCustomerStatus == true)
                            {
                                DataTable dt4 = DBAccess.FetchData("select convert(varchar,Dates,103) as 'Dates' from [dbo].[tbl_SendToCustomer]  where DamageId='" + lblDamageId.Text + "' and SerialNo='" + SerialNo + "'").Tables[0];
                                if (dt4 != null && dt4.Rows.Count > 0)
                                {
                                    lblCustDate.Text = dt4.Rows[0]["Dates"].ToString();
                                    idSendCust.Visible = true;
                                }
                                lblCurrentStatus.Text = "Product sent to customer";
                            }
                            else
                            {
                                idSendCust.Visible = false;
                            }
                        }


                    }
                    else
                    {
                        idRecollection.Visible = false;
                    }

                }
                else
                {
                    lblCurrentStatus.Text = "Damage Collected from customer";
                    idSent.Visible = true;
                }
                div_Damage.Visible = true;
            }
            else
            {
                lblDamage.Text = "No";
                div_Damage.Visible = false;
            }
        }
        else
        {
            lblInStock.Text = "Yes";
        }
    }
}