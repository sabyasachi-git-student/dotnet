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

public partial class Project_FaultyItemWarrantyExpiired : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["FaultyItemDetails"], null))
        {
            ViewState["FaultyItemDetails"] = getItem();
        }
        if (!IsPostBack)
        {
            Session["FaultyId"] = "";
        }
    }
    #region getItem
    protected DataTable getItem()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

        dtCol = new DataColumn();
        dtCol.ColumnName = "FaultyId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "SrBarVodeID";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Barcode";
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
        dtCol.ColumnName = "WarrantyPeriod";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "WarrantyTo";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CoderLifeTo";
        oTable.Columns.Add(dtCol);

        return oTable;
    }
    #endregion
    protected void btnChooseFaultyItem_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
    }
    
    #region btnAddServiceItem_Click
    protected void btnAddServiceItem_Click(object sender, EventArgs e)
    {
        string Barcode = "";

        List<object> fieldValues = gvItem.GetSelectedFieldValues(new string[] { "Barcode" });
        


        if (fieldValues.Count != 0)
        {
            DataTable AppQtn = new DataTable();



            for (int j = 0; j < fieldValues.Count; j++)
            {
                Barcode = fieldValues[j].ToString();

                DataTable dt8 = (DataTable)ViewState["FaultyItemDetails"];
                if (dt8 != null && dt8.Rows.Count > 0)
                {
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        if (Barcode == dt8.Rows[i]["Barcode"].ToString())
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! This Item Is Already Added!');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "document.getElementById('id03').style.display='block'", true);
                            return;
                        }

                    }

                }

                DataTable dtI = (DataTable)ViewState["FaultyItemDetails"];
                DataTable dt11 = DBAccess.FetchDatatable("select fe.FaultyId,fed.SrBarVodeID,fed.Barcode,im.ItemId,im.ItemName,im.Make,im.WarrantyPeriod,convert(varchar,sib.WarrantyTo,103) as WarrantyTo,convert(Varchar,sib.CoderLifeTo,103) as CoderLifeTo  FROM tbl_FaultyEntry fe join tbl_FaultyEntryDetails fed on fe.FaultyId=fed.FaultyId join tbl_ItemMaster im on fed.ItemId=im.ItemId join tbl_RackStockInBarCodeDetails sib on fed.Barcode=sib.Barcode where fe.FaultyDate>sib.WarrantyTo and fed.Barcode='" + Barcode + "'");
                if (dt11 != null && dt11.Rows.Count > 0)
                {
                    DataRow drH = dtI.NewRow();
                    drH["FaultyId"] = dt11.Rows[0]["FaultyId"].ToString();
                    drH["SrBarVodeID"] = dt11.Rows[0]["SrBarVodeID"].ToString();
                    drH["Barcode"] = dt11.Rows[0]["Barcode"].ToString();
                    drH["ItemId"] = dt11.Rows[0]["ItemId"].ToString();
                    drH["ItemName"] = dt11.Rows[0]["ItemName"].ToString();
                    drH["Make"] = dt11.Rows[0]["Make"].ToString();
                    drH["WarrantyPeriod"] = dt11.Rows[0]["WarrantyPeriod"].ToString();
                    drH["WarrantyTo"] = dt11.Rows[0]["WarrantyTo"].ToString();
                    drH["CoderLifeTo"] = dt11.Rows[0]["CoderLifeTo"].ToString();

                    dtI.Rows.Add(drH);

                    ViewState["FaultyItemDetails"] = dtI;
                    gvFaultyItemDetails.DataSource = dtI;
                    gvFaultyItemDetails.DataBind();
                }

            }
        }
    }
    #endregion
    protected void gvFaultyItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFaultyItemDetails.PageIndex = e.NewPageIndex;
        this.gvFaultyItemDetails.DataSource = (DataTable)ViewState["FaultyItemDetails"];
        this.gvFaultyItemDetails.DataBind();
    }
    protected void gvFaultyItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt8 = new DataTable();
            dt8 = (DataTable)ViewState["FaultyItemDetails"];
            dt8.Rows[rowindex].Delete();
            dt8.AcceptChanges();
            ViewState["FaultyItemDetails"] = dt8;
            gvFaultyItemDetails.DataSource = dt8;
            gvFaultyItemDetails.DataBind();


        }
    }
    protected void gvFaultyItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvFaultyItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("FaultyItemWarrantyExpiired.aspx");
    }
    #region btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FaultyInWarId = DBAccess.FetchDatatable("select [dbo].[fn_FaultyWarExId]()").Rows[0][0].ToString();

        string UserId = "";
        string BranchId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            BranchId = Session["BranchId"].ToString();
        }
        catch { }
        DateTime DOE = DateTime.Now;
        string FaultyId = ddlFaultyId.Text;
        string ToRepairer = ddlToRepairer.Text;
        string CorComName = ddlCorComName.Text;
        string CnNo = txtCnNo.Text;
        if (CnNo == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Consignment No');", true);
            return;
        }

        string CnDate = dtCnDate.Text;
        string RmaNo = txtRmaNo.Text;
        if (RmaNo =="")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter RMA No.');", true);
            return;
        }

        string RmaDate = dtRmaDate.Text;

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
        int n = ClassFaultyEntry.FaultyWarExSave(FaultyId, FaultyInWarId, ToRepairer, CorComName, CnNo, CnDate, RmaNo, RmaDate, ImageUpload, UserId, BranchId, DOE);
        for (int i = 0; i < gvFaultyItemDetails.Rows.Count; i++)
        {
            string FaultyInWarDeId = DBAccess.FetchDatatable("select [dbo].[fn_FaultyWarExDeId]()").Rows[0][0].ToString();


            string SrBarVodeId = gvFaultyItemDetails.Rows[i].Cells[1].Text.ToString();
            string Barcode = gvFaultyItemDetails.Rows[i].Cells[2].Text.ToString();
            string ItemId = gvFaultyItemDetails.Rows[i].Cells[3].Text.ToString();
            string ItemName = gvFaultyItemDetails.Rows[i].Cells[4].Text.ToString();
            string WarrantyPeriod = gvFaultyItemDetails.Rows[i].Cells[6].Text.ToString();
            string WarrantyTo = gvFaultyItemDetails.Rows[i].Cells[7].Text.ToString();
            string CoderLifeTo = gvFaultyItemDetails.Rows[i].Cells[8].Text.ToString();
            int n1 = ClassFaultyEntry.SaveFaultyWarExDetails(FaultyInWarId, FaultyInWarDeId, SrBarVodeId, Barcode, ItemId, ItemName, WarrantyPeriod, WarrantyTo, CoderLifeTo, UserId, BranchId, DOE);
        }
        if (n == 1)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Faulty Items saved successfully..');window.location='../Project/FaultyItemWarrantyExpiired.aspx';", true);
        }
        else if (n == -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Faulty Items already exist.');", true);
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Faulty Items not saved. Try again.');", true);
            return;
        }
    }
    #endregion
    protected void ddlFaultyId_TextChanged(object sender, EventArgs e)
    {
        string FaultyId = ddlFaultyId.Text;
        Session["FaultyId"] = FaultyId;

    }
}
