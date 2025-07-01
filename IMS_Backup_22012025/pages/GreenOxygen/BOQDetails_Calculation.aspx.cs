using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class pages_GreenOxygen_BOQDetails_Calculation : System.Web.UI.Page
{
    decimal Grandtotal = 0;
    decimal mistritol1, MICtol1, labtol1, mistritol2, MICtol2, labtol2, mistritol3, MICtol3, labtol3;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["AddList"], null))
        {
            ViewState["AddList"] = addData();
        }

        if (!IsPostBack)
        {
            LoadCompany();
            BranchLoad();
            BIndLabour();

            Bind_ddlBOQ();

            string CompanyId = ddl_Company.SelectedValue.ToString();
            string ProjectId = ddl_Project.SelectedValue.ToString();

            if (CompanyId == "" || CompanyId == "-Select-")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Company Name ');", true);
                return;
            }
            if (ProjectId == "" || ProjectId == "-Select-")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Project Name ');", true);
                return;
            }


        }
    }

    public void LoadCompany()
    {
        string UserId = Session["UserId"].ToString(); ;

        DataTable dt = DBAccess.FetchDatatable(@"select distinct mp.CompanyId ,com.CompanyName from [tbl_UserBranchMapping] mp
		 join tbl_CompanyMaster com on  mp.CompanyId=com.CompanyId where mp.UserId='" + UserId + "'");

        if (dt != null && dt.Rows.Count > 0)
        {
            ddl_Company.Items.Clear();

            ddl_Company.DataSource = dt;
            ddl_Company.DataTextField = "CompanyName";
            ddl_Company.DataValueField = "CompanyId";
            ddl_Company.DataBind();
            ddl_Company.Items.Insert(0, "-Select-");
        }
    }
    public void BranchLoad()
    {

        string UserId = Session["UserId"].ToString(); ;

        string CompanyId = "";
        try
        {
            CompanyId = ddl_Company.SelectedItem.Value.ToString();
        }
        catch
        {

        }
        string CompanyName = DBAccess.FetchDatasingle("select  CompanyName from [dbo].[tbl_CompanyMaster] where CompanyId='" + CompanyId + "'");
        DataTable dt = DBAccess.FetchDatatable(@"select  m.UserId, m.BranchId,m.CompanyId,b.BranchName from  [dbo].[tbl_UserBranchMapping] m
		left join dbo.tbl_Branch b on m.BranchId=b.BranchId 
		left join [dbo].[tbl_CompanyMaster] com on m.CompanyId=com.CompanyId where m.UserId='" + UserId + "' and m.CompanyId='" + CompanyId + "'");
        if (dt != null && dt.Rows.Count > 0)
        {

            ddl_Project.Items.Clear();
            ddl_Project.DataSource = dt;
            ddl_Project.DataTextField = "BranchName";
            ddl_Project.DataValueField = "BranchId";
            ddl_Project.DataBind();
            ddl_Project.Items.Insert(0, "-Select-");
        }
    }

    protected void ddl_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        BranchLoad();
    }
    protected void ddl_BOQname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_BOQName.Enabled = true;
        ddl_item.Enabled = true;

        string BoqId = ddl_BOQName.SelectedValue.ToString();

        DataTable dt = DBAccess.FetchDatatable("SELECT BoqId, BoqDetailsSlNo  FROM [dbo].[tbl_BOQDetails] where BoqId= '" + BoqId + "' ");
        ddl_SrNo.DataSource = dt;
        ddl_SrNo.DataValueField = "BoqDetailsSlNo";
        ddl_SrNo.DataTextField = "BoqDetailsSlNo";
        ddl_SrNo.DataBind();
        ddl_SrNo.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddl_SrNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BoqId = ddl_BOQName.SelectedValue.ToString();
        string Srno = ddl_SrNo.SelectedValue.ToString();


        DataTable dt = DBAccess.FetchDatatable("select bd.BoqDetailsId, bd.Unit, bd.Qty, bd.ItemDescripton, (select Sum(bdn.ExecQty) from [tbl_BOQDetail_New] bdn where bdn.Srno=bd.BoqDetailsSlNo) as qty1,(select bd.Qty-Sum(bdn.ExecQty) from [tbl_BOQDetail_New] bdn where bdn.Srno=bd.BoqDetailsSlNo) as balance from tbl_BoqDetails bd  where BoqId= '" + BoqId + "' and BoqDetailsSlNo= '" + Srno + "'");
        ddl_item.DataSource = dt;
        ddl_item.ValueField = "ItemDescripton";
        ddl_item.TextField = "ItemDescripton";
        ddl_item.DataBind();

        ddl_AccUnit.DataSource = dt;
        ddl_AccUnit.DataValueField = "Unit";
        ddl_AccUnit.DataTextField = "Unit";
        ddl_AccUnit.DataBind();

    }


    public void Bind_ddlBOQ()
    {
        DataTable dt = DBAccess.FetchDatatable("SELECT BoqId, BOQName FROM [dbo].[tbl_BOQ]");
        ddl_BOQName.DataSource = dt;
        ddl_BOQName.DataValueField = "BoqId";
        ddl_BOQName.DataTextField = "BOQName";
        ddl_BOQName.DataBind();
        ddl_BOQName.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void BIndLabour()
    {
        DataTable dt = DBAccess.FetchDatatable("select distinct LabourType from [tbl_BOQCalculationChild]");
        ddlLaborType.DataSource = dt;
        ddlLaborType.DataValueField = "LabourType";
        ddlLaborType.DataTextField = "LabourType";
        ddlLaborType.DataBind();
        ddlLaborType.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected DataTable addData()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();
        dtCol.ColumnName = "BOQMst_Id";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "BOQName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "BoqDetailsId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemDesName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "UserDate";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "LabourType";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "LabourNo";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "LabourRate";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "TotalRate";
        oTable.Columns.Add(dtCol);

        return oTable;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UserId = "";
        try
        {
            UserId = Session["UserId"].ToString();
        }
        catch
        {

        }
        string CompanyId = ddl_Company.SelectedValue.ToString();
        string ProjectId = ddl_Project.SelectedValue.ToString();

        if (CompanyId == "" || CompanyId == "-Select-")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Company Name ');", true);
            return;
        }
        if (ProjectId == "" || ProjectId == "-Select-")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Project Name ');", true);
            return;
        }

        string Unit = ddl_AccUnit.Text;


        DateTime DOE = DateTime.Now;

        if (btnSave.Text == "Save")
        {
            string BOQDTL_ID = DBAccess.FetchDatatable("select [dbo].[fn_BOQCalCulationId]()").Rows[0][0].ToString();
            string BOQMst_Id = ddl_BOQName.SelectedValue.ToString();
            string BOQName = ddl_BOQName.SelectedItem.ToString();
            string Srno = ddl_SrNo.Text;
            string ItemDesName = ddl_item.Text;
            string UserDate = txt_Date.Text;
            decimal TotalAmount = Convert.ToDecimal(txtAmountSave.Text);
            decimal ExecQty = Convert.ToDecimal(txtExecQty.Text);
            if (ExecQty == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Enter Exec Qty In Measurement On Selected Date!');", true);
                return;
            }
            decimal ExecQtyCalculate = Convert.ToDecimal(txtCalculate.Text);

            //int SaveDetails = BOQCalculationResponcive.save(BOQDTL_ID, BOQMst_Id, BOQName, Srno, ItemDesName, Unit, UserDate, DOE, UserId, CompanyId, ProjectId, TotalAmount, ExecQty, ExecQtyCalculate);
            int SaveDetails = 8;
            if (SaveDetails > 0)
            {
                DataTable FatcData = ViewState["AddList"] as DataTable;
                for (int i = 0; i < FatcData.Rows.Count; i++)
                {
                    string UserDate1 = FatcData.Rows[i]["UserDate"].ToString();

                    string LabourType = FatcData.Rows[i]["LabourType"].ToString();
                    Decimal LabourNo = Convert.ToDecimal(FatcData.Rows[i]["LabourNo"].ToString());
                    Decimal LabourRate = Convert.ToDecimal(FatcData.Rows[i]["LabourRate"].ToString());
                    Decimal TotalRate = Convert.ToDecimal(FatcData.Rows[i]["TotalRate"].ToString());

                    //int SaveOther = BOQCalculationChildResponcive.save(BOQDTL_ID, LabourType, LabourNo, LabourRate, TotalRate, DOE, UserId, CompanyId, ProjectId, UserDate1);

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Not Save .');window.location='../GreenOxygen/BOQDetails_Calculation.aspx';", true);
                return;
            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Labour Details save Successfully .');window.location='../GreenOxygen/BOQDetails_Calculation.aspx';", true);
            return;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("BOQDetails_Calculation.aspx");
    }

    protected void btn_AddList_Click(object sender, EventArgs e)
    {
        String BOQMst_Id = ddl_BOQName.SelectedValue.ToString();
        String BOQName = ddl_BOQName.SelectedItem.ToString();
        String Srno = ddl_SrNo.SelectedItem.ToString();
        String ItemDesName = ddl_item.Text;
        string UserDate = txt_Date.Text;

        String LabourType = ddlLaborType.SelectedItem.ToString();
        if (LabourType == "--Select--")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select Proper Type Of Labour');", true);
            return;
        }


        Decimal LabourNo = Convert.ToDecimal(txtLabour.Text);
        decimal LabourRate = Convert.ToDecimal(txtLabourRate.Text);
        decimal TotalRate = Convert.ToDecimal(txtLabourTotal.Text);

        DataTable dt1 = (DataTable)ViewState["AddList"];

        DataRow dr = dt1.NewRow();
        //dr["BOQDTL_ID"] = BOQDTL_ID;
        dr["BOQMst_Id"] = BOQMst_Id;
        dr["BOQName"] = BOQName;
        dr["BoqDetailsId"] = Srno;
        dr["ItemDesName"] = ItemDesName;
        dr["UserDate"] = UserDate;

        dr["LabourType"] = LabourType;
        dr["LabourNo"] = LabourNo;
        dr["LabourRate"] = LabourRate;
        dr["TotalRate"] = TotalRate;

        dt1.Rows.Add(dr);
        ViewState["AddList"] = dt1;
        grdBOQDeails.DataSource = ViewState["AddList"] as DataTable;
        grdBOQDeails.DataBind();

        ddlLaborType.SelectedIndex = 0;
        txtLabour.Text = "";
        txtLabourRate.Text = "";
        txtLabourTotal.Text = "";
    }



    /* Labour calculation part */
    protected void txtLabour_TextChanged(object sender, EventArgs e)
    {
        Decimal labNo = Convert.ToDecimal(txtLabour.Text);
        decimal labrate;
        if (txtLabourRate.Text == "")
        {
            labrate = 0;
        }
        else
        {
            labrate = Convert.ToDecimal(txtLabourRate.Text);
        }
        decimal labTotal = Convert.ToDecimal(labNo * labrate);
        txtLabourTotal.Text = labTotal.ToString();
    }
    protected void txtLabourRate_TextChanged(object sender, EventArgs e)
    {
        Decimal labno = Convert.ToDecimal(txtLabour.Text);
        if (labno == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Labour Value!');", true);
            return;
        }
        decimal labrate = Convert.ToDecimal(txtLabourRate.Text);
        if (labrate == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Labour Rate!');", true);
            return;
        }
        decimal TotalLabRate = (labno * labrate);
        txtLabourTotal.Text = Convert.ToString(TotalLabRate);
    }
    /* Labour calculation part end */


    protected void grdBOQDeails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["AddList"];
            dt.Rows[rowindex].Delete();
            dt.AcceptChanges();
            ViewState["AddList"] = dt;

            grdBOQDeails.DataSource = dt;
            grdBOQDeails.DataBind();
        }
    }
    protected void grdBOQDeails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdBOQDeails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void grdBOQDeails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }


    protected void txt_Date_DateChanged(object sender, EventArgs e)
    {
        string BoqId = ddl_BOQName.SelectedValue.ToString();
        string Srno = ddl_SrNo.SelectedValue.ToString();
        String UserDate = txt_Date.Text;

        DataTable dt = DBAccess.FetchDatatable("select bd.BoqDetailsId, bd.Unit, bd.Qty, bd.ItemDescripton, bdn.ExecQty, convert(varchar,bdn.UserDate,103) as UserDate, (select bd.Qty-Sum(bdn.ExecQty) from [tbl_BOQDetail_New] bdn where bdn.Srno=bd.BoqDetailsSlNo) as balance from tbl_BoqDetails bd  join [tbl_BOQDetail_New] bdn on bdn.Srno=bd.BoqDetailsSlNo where BoqId= '" + BoqId + "' and BoqDetailsSlNo= '" + Srno + "' and convert(varchar,bdn.UserDate,103)='" + UserDate + "' ");

        try
        {
            txtExecQty.Text = dt.Rows[0]["ExecQty"].ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Exec Qty In Measurement On Selected Date!');", true);
            return;
        }

    }
    protected void btnCalculateTotals_Click(object sender, EventArgs e)
    {
        decimal allTotal = 0;
        decimal all = 0;
        decimal exec = Convert.ToDecimal(txtExecQty.Text);

        for (int j = 0; j < grdBOQDeails.Rows.Count; j++)
        {
            //decimal TOTalAmount = Math.Round(Total + GSTAmount);
            string Amount = ((TextBox)grdBOQDeails.Rows[j].FindControl("txtTotalRate")).Text;
            decimal ConvertedAmount = Convert.ToDecimal(string.IsNullOrEmpty(Amount) ? "0" : Amount);

            //double Amount = Quantity * Rate;
            allTotal = Math.Round(allTotal + ConvertedAmount, 2);
            all = Math.Round(allTotal / exec, 2);

            txtCalculate.Text = all.ToString();

        }
        txtAmountSave.Text = allTotal.ToString();
        ((TextBox)grdBOQDeails.FooterRow.FindControl("txtAmount")).Text = allTotal.ToString();


    }
}