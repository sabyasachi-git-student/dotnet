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

public partial class Project_MaterialShipment : System.Web.UI.Page
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
               
                string ToUserGroupName = DBAccess.FetchDatasingle("select UserGroup from tbl_UserGroups where UserGroupId='UG15'");
            }
            catch

            { }
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
            try
            {
                BranchId = Session["BranchId"].ToString();
                string BranchName = DBAccess.FetchDatasingle("select BranchName from tbl_Branch where BranchId='" + BranchId + "'");
               Session["BranchName"] = BranchName.ToString();
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
                return;
            }

       

        if (Session["RowId"] != null)
        {
            string RowId = Session["RowId"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "window", "window.open('DevExpressPrint.aspx?MetShipId=" + RowId + "&Types=Stall', 'window','HEIGHT=550,WIDTH=820,top=50,left=50,toolbar=no,scrollbars=yes,resizable=yes');", true);
            Session["RowId"] = null;
        }

        string MetShipId = Request.QueryString["MetShipId"];
        if (MetShipId != null)
        {
            FatchDataFromApproval(MetShipId, "Update");
        }
        else
        {

        }
        }
    }


    public void FatchDataFromApproval(string MetShipId, string Status)
    {
        DataTable dt = DBAccess.FetchDatatable(@"select a.*,
Convert(varchar,a.IssueDate,103) as IssueDate1, a.CourierName,Convert(varchar,a.IssueDate,103) as CourierDate1
 from tbl_MaterialShipment a join tbl_MaterialIssue b on a.IssueId=b.IssueId where a.MetShipId='" + MetShipId + "'");
        if (dt.Rows.Count > 0)
        {
            ddlIssueId.Text = dt.Rows[0]["IssueId"].ToString();
            dtpDate.Date = Convert.ToDateTime(dt.Rows[0]["IssueDate1"].ToString());
            ddlCourierName.Text = dt.Rows[0]["CourierName"].ToString();
            txtConsignmentNumber.Text = dt.Rows[0]["ConsignmentNumber"].ToString();
            txtPersonName.Text = dt.Rows[0]["PersonName"].ToString();
            txtContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
            txtBusRoute.Text = dt.Rows[0]["ShipmentTO"].ToString();
            txtBusReg.Text = dt.Rows[0]["Name"].ToString();
            txtTrainName.Text = dt.Rows[0]["TrainName"].ToString();
            txtTrainNo.Text = dt.Rows[0]["TrainNo"].ToString();
            txtCoachNo.Text = dt.Rows[0]["CoachNo"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            btnSave.Text = "Update";

            if (ddlCourierName.Text == "By Hand")
            {
                txtPersonName.Enabled = true;
                txtContactNo.Enabled = true;
                txtTrainName.Enabled = false;
                txtTrainNo.Enabled = false;
                txtCoachNo.Enabled = false;
                txtConsignmentNumber.Enabled = false;
                txtBusRoute.Enabled = false;
                txtBusReg.Enabled = false;
            }
            else if (ddlCourierName.Text == "By Courier")
            {
                txtConsignmentNumber.Enabled = true;
                txtPersonName.Enabled = true;
                txtContactNo.Enabled = false;
                txtTrainName.Enabled = false;
                txtTrainNo.Enabled = false;
                txtCoachNo.Enabled = false;
                txtBusRoute.Enabled = false;
                txtBusReg.Enabled = false;
            }
            else if (ddlCourierName.Text == "By Train")
            {
                txtPersonName.Enabled = true;
                txtContactNo.Enabled = true;
                txtTrainName.Enabled = true;
                txtTrainNo.Enabled = true;
                txtCoachNo.Enabled = true;
                txtConsignmentNumber.Enabled = false;
                txtBusRoute.Enabled = false;
                txtBusReg.Enabled = false;
            }
            else if (ddlCourierName.Text == "By Bus")
            {
                txtPersonName.Enabled = true;
                txtContactNo.Enabled = true;
                txtBusRoute.Enabled = true;
                txtBusReg.Enabled = true;
                txtTrainName.Enabled = false;
                txtTrainNo.Enabled = false;
                txtCoachNo.Enabled = false;
                txtConsignmentNumber.Enabled = false;
            }
        }

        
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
        DateTime DOE = DateTime.Now;

        string MetShipId = DBAccess.FetchDatatable("select [dbo].[fn_MetShipId]()").Rows[0][0].ToString();
        string IssueId = ddlIssueId.Text;
        string IssueDate = dtpDate.Text;
        string CourierName = ddlCourierName.Text;
        string ConsignmentNumber = txtConsignmentNumber.Text;
        string Remarks = txtRemarks.Text;
        string CourierDate = dtpCourierDate.Text;
        string PersonName = txtPersonName.Text;
        string ContactNo = txtContactNo.Text;
        string TrainName = txtTrainName.Text;
        string TrainNo = txtTrainNo.Text;
        string CoachNo = txtCoachNo.Text;
        string BusRoute = txtBusRoute.Text;
        string BusReg = txtBusReg.Text;


        if (ddlCourierName.Text == "By Hand")
        {
            if (PersonName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Person Name');", true);
                return;
            }
            if (ContactNo == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Contact No');", true);
                return;
            }            
        }

        if (ddlCourierName.Text == "By Bus")
        {
            if (PersonName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Person Name');", true);
                return;
            }
            if (ContactNo == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Contact No');", true);
                return;
            }
            if (BusRoute == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Bus Route');", true);
                return;
            }
            if (BusReg == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Bus Registration Number');", true);
                return;
            }
        }

        if (ddlCourierName.Text == "By Train")
        {
            if (PersonName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Person Name');", true);
                return;
            }
            if (ContactNo == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Contact No');", true);
                return;
            }
            if (TrainName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Train Name');", true);
                return;
            }
            if (TrainNo == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Train No');", true);
                return;
            }
            if (CoachNo == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Coach No');", true);
                return;
            }
        }

        if (ddlCourierName.Text == "By Courier")
        {
            if (ConsignmentNumber == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Consignment Number');", true);
                return;
            }
        }

        string BillNo = FetchInvoiceNo();

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

        if (btnSave.Text != "Update")
        { 
        if (ImageUpload == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Image Upload');", true);
            return;
        }
        }


        if (btnSave.Text != "Update")
        {

            bool UpdateReqPop = DBAccess.SaveData("insert into [dbo].[tbl_MaterialShipment] values('" + MetShipId + "', '" + IssueId + "', convert(datetime,'" + IssueDate + "',103), '" + CourierName + "', convert(datetime,'" + CourierDate + "',103), '" + ConsignmentNumber + "', '" + Remarks + "', '" + PersonName + "', '" + ContactNo + "', '" + TrainName + "', '" + TrainNo + "', '" + CoachNo + "', '0','" + BusRoute + "', '" + BusReg + "', '" + ImageUpload + "', '" + BillNo + "', '" + UserId + "',  '" + BranchId + "',  convert(datetime,'" + DOE + "',103))");

            if (UpdateReqPop == true)
            {
                Session["RowId"] = MetShipId;
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Material Shipment saved successfully..');window.location='../Project/MaterialShipment.aspx';", true);
            }
            
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Material Shipment Not Save. Try again.');", true);
                return;
            }
        }
        else
        {
             string MetShipIdUpdate = Request.QueryString["MetShipId"];
           

             bool UpdateReqPop1 = DBAccess.SaveData("update [dbo].[tbl_MaterialShipment] set  IssueDate=convert(datetime,'" + IssueDate + "',103), CourierName='" + CourierName + "', CourierDate= convert(datetime,'" + CourierDate + "',103), ConsignmentNumber='" + ConsignmentNumber + "', Remarks='" + Remarks + "', PersonName='" + PersonName + "', ContactNo='" + ContactNo + "', TrainName='" + TrainName + "', TrainNo='" + TrainNo + "', CoachNo='" + CoachNo + "', ShipmentTO='" + BusRoute + "', Name='" + BusReg + "', UserId='" + UserId + "',  BranchId='" + BranchId + "',  DOE=convert(datetime,'" + DOE + "',103) where MetShipId ='" + MetShipIdUpdate + "'");

            if (UpdateReqPop1 == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Material Shipment Updated successfully..');window.location='../Project/MaterialShipment.aspx';", true);
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Material Shipment Not Save. Try again.');", true);
                return;
            }
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaterialShipment.aspx");
    }


    protected void ddlIssueId_TextChanged(object sender, EventArgs e)
    {
        string IssueId = ddlIssueId.Text;

        DataTable dt1 = DBAccess.FetchDatatable(@"select a.*, b.WarrantyPeriod from tbl_MaterialIssueDetails a
join tbl_ItemMaster b on a.ItemId=b.ItemId where IssueId='" + IssueId + "' Union select a.*, b.WarrantyPeriod from tbl_TemporaryStockIssueDetails a join tbl_ItemMaster b on a.ItemId=b.ItemId where TemIssueId='" + IssueId + "'");
        if (dt1.Rows.Count > 0)
        {
            gvItemDetails.DataSource = dt1;
            gvItemDetails.DataBind();
        }

        DataTable dt2 = DBAccess.FetchDatatable(@"select a.*, convert(varchar,a.WarrantyTo,103) as WarrantyTo1, convert(varchar,a.CoderLifeTo,103) as CoderLifeTo1, b.ItemName from tbl_MaterialIssueBarCodeDetails a join tbl_ItemMaster b on a.itemId=b.ItemId where a.IssueId='" + IssueId + "' Union all select a.*, convert(varchar,a.WarrantyTo,103) as WarrantyTo1, convert(varchar,a.CoderLifeTo,103) as CoderLifeTo1, b.ItemName from tbl_TemporaryStockIssueBarCodeDetails a join tbl_ItemMaster b on a.itemId=b.ItemId where a.TemIssueId='" + IssueId + "'");
        if (dt2.Rows.Count > 0)
        {
            gv_Barc.DataSource = dt2;
            gv_Barc.DataBind();
            ViewState["TempBarcode"] = dt2;
        }
    }
    protected void txtConsignmentNumber_TextChanged(object sender, EventArgs e)
    {
        string ConsignmentNumber = txtConsignmentNumber.Text;
        string FeatchConsignmentNumber = DBAccess.FetchDatasingle("select ConsignmentNumber from tbl_MaterialShipment where ConsignmentNumber='" + ConsignmentNumber + "'");
        if (FeatchConsignmentNumber == "")
        {

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Already Have the Same Number');", true);
            return;
        }
    }
    protected void ddlCourierName_TextChanged(object sender, EventArgs e)
    {
        if (ddlCourierName.Text == "By Hand")
        {
            txtPersonName.Enabled = true;
            txtContactNo.Enabled = true;
            txtTrainName.Enabled = false;
            txtTrainNo.Enabled = false;
            txtCoachNo.Enabled = false;
            txtConsignmentNumber.Enabled = false;
            txtBusRoute.Enabled = false;
            txtBusReg.Enabled = false;
        }
        else if (ddlCourierName.Text == "By Courier")
        {
            txtConsignmentNumber.Enabled = true;
            txtPersonName.Enabled = true;
            txtContactNo.Enabled = false;
            txtTrainName.Enabled = false;
            txtTrainNo.Enabled = false;
            txtCoachNo.Enabled = false;
            txtBusRoute.Enabled = false;
            txtBusReg.Enabled = false;
        }
        else if (ddlCourierName.Text == "By Train")
        {
            txtPersonName.Enabled = true;
            txtContactNo.Enabled = true;
            txtTrainName.Enabled = true;
            txtTrainNo.Enabled = true;
            txtCoachNo.Enabled = true;
            txtConsignmentNumber.Enabled = false;
            txtBusRoute.Enabled = false;
            txtBusReg.Enabled = false;
        }
        else if (ddlCourierName.Text == "By Bus")
        {
            txtPersonName.Enabled = true;
            txtContactNo.Enabled = true;
            txtBusRoute.Enabled = true;
            txtBusReg.Enabled = true;
            txtTrainName.Enabled = false;
            txtTrainNo.Enabled = false;
            txtCoachNo.Enabled = false;
            txtConsignmentNumber.Enabled = false;
        }
        else if (ddlCourierName.Text == "By Transporter")
        {
            txtPersonName.Enabled = true;
            txtContactNo.Enabled = true;
            txtTrainName.Enabled = true;
            txtTrainNo.Enabled = true;
            txtCoachNo.Enabled = false;
            txtConsignmentNumber.Enabled = true;
            txtBusRoute.Enabled = false;
            txtBusReg.Enabled = false;
        }
    }

    public string FetchInvoiceNo()
    {

        string UserId = "";

        try
        {
            UserId = Session["UserId"].ToString();
        }
        catch { }
        string ProjectId = "";
        try
        {
            ProjectId = Session["BranchId"].ToString();
        }
        catch
        {

        }
        string CompanyId = "";
        try
        {
            CompanyId = Session["CompanyId"].ToString();
        }

        catch
        {

        }
        string SiteCode = DBAccess.FetchDatasingle("select BranchName from [dbo].[tbl_Branch] where BranchId='" + ProjectId + "'");

        string voucherno = "";
        DateTime dt = DateTime.Now;
        try
        {
            //dt = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //dt = DateTime.ParseExact(Dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string getcurrentyear = "";
            string getnextyear = "";
            int CurrentMonth = dt.Month;
            int CurrentYear = dt.Year;
            if (CurrentMonth < 4)
            {
                getcurrentyear = (CurrentYear - 1).ToString();
                getnextyear = dt.AddYears(0).ToString("yy");
            }
            else
            {
                getcurrentyear = dt.Year.ToString();
                getnextyear = dt.AddYears(1).ToString("yy");
            }
            string FinancialYear = getcurrentyear.Substring(getcurrentyear.Length - 2) + "-" + getnextyear;
            string dtINV = DBAccess.FetchDatasingle("select BillId from tbl_MaterialShipment order by RowId desc");


            if (dtINV != "")
            {
                string CountId = dtINV.Substring(13);
                int a = 0;
                try
                {
                    a = Convert.ToInt32(CountId);
                    a = a + 1;
                }
                catch { }
                voucherno = "No. RCIL/RMK/" + a.ToString();
                //voucherno1 = SiteCode + "/" + "STRQ/Petty/" + FinancialYear + "/" + a.ToString();
                ViewState["InvoiceCount"] = a;
                ViewState["FinancialYear"] = FinancialYear;
            }
            else
            {
                voucherno = "No. RCIL/RMK/" + 1.ToString();
                ViewState["InvoiceCount"] = 1;
                ViewState["FinancialYear"] = FinancialYear;
            }
        }
        catch
        {
            voucherno = "";

        }
        return voucherno;
    }
}