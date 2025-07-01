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

public partial class Project_SupplierMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["TDSTable"], null))
        {
            ViewState["TDSTable"] = getTDSTable();
        }
        if (ReferenceEquals(ViewState["TempStockIn"], null))
        {
            ViewState["TempStockIn"] = getDataTable();
        }
        if (ReferenceEquals(ViewState["Dynamic_Image_2"], null))
        {
            ViewState["Dynamic_Image_2"] = CraeteUploadImageTable();
        }
        if (!IsPostBack)
        {
            LoadTDS();
            LoadState();
            Session["txtNumber"] = null;
            Session["MultipleAddress1"] = null;
            Session["BankDetails"] = null;
        }
    }

    private void LoadTDS()
    {
        DataTable dt = ClassTDSMasterVendor.FetchTDS();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlTDS.DataSource = dt;
            ddlTDS.DataTextField = "Section";
            ddlTDS.DataValueField = "Percentage";
            ddlTDS.DataBind();
        }
    }
    protected DataTable getTDSTable()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();
        dtCol.ColumnName = "Section";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Percentage";
        oTable.Columns.Add(dtCol);

        return oTable;
    }
    private void LoadState()
    {
        ddlState.Items.Clear();
        ddlState.Items.Add("--Select--");
        DataTable dt = DBAccess.FetchDatatable("select StateName,StateCode from [dbo].[tbl_StateMaster] order by StateName asc");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlState.DataSource = dt;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateCode";
            ddlState.DataBind();
        }
    }

    #region multipleNumber
    protected void gvmultiplenumber_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //Retrieve the table from the session object.
        DataTable dt = (DataTable)Session["txtNumber"];

        //Update the values.
        GridViewRow row = gvmultiplenumber.Rows[e.RowIndex];
        dt.Rows.Remove(dt.Rows[row.DataItemIndex]);
        Session["txtNumber"] = null;
        Session["txtNumber"] = dt;
        gvmultiplenumber.DataSource = dt;
        gvmultiplenumber.DataBind();
        //finally assign to session also.


    }
    public class DynamicMultipleNumber
    {
        public DynamicMultipleNumber() { }
        public DataTable CreateDataSource1()
        {
            DataTable dt = new DataTable();
            DataColumn identity = new DataColumn("IDNumber", typeof(int));
            dt.Columns.Add(identity);


            dt.Columns.Add("Cat", typeof(string));
            dt.Columns.Add("txtNumber", typeof(string));

            return dt;
        }
        //This is the AddRow method to add a new row in Table dt 
        public void AddRow(int id, string txtNumber, DataTable dt, string Cat)
        {
            dt.Rows.Add(new object[] { id, Cat, txtNumber });
        }
    }

    private void SaveContactNo(string SupplierId)
    {
        if (btn_CompanyDetailsSubmit.Text == "UPDATE")
        {
            if (ViewState["SupplierId"] != null)
            {
                int n = ClassSupplierMaster.SupplierContactNoDelete(SupplierId);
                if (n == 1)
                {

                }
            }
        }
        if (Session["txtNumber"] != null)
        {
            DataTable dt = Session["txtNumber"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string ContactNo = dt.Rows[i]["txtNumber"].ToString();
                    int n = ClassSupplierMaster.SupplierContactNoSave(SupplierId, ContactNo);
                    if (n == 1)
                    {

                    }
                }


            }
        }
    }
    protected void btnContactAdd_Click(object sender, EventArgs e)
    {

        DynamicMultipleNumber Dt = new DynamicMultipleNumber();
        DataTable d = null;
        //If session is not there means first time addinf record.
        if (Session["txtNumber"] == null)
        {
            d = Dt.CreateDataSource1();
            Session["IDNumber"] = 1;
        }
        //else we are converting session of datatable to data table and increament the column  id by 1;
        else
        {
            Session["IDNumber"] = Convert.ToInt16(Session["IDNumber"]) + 1;
            d = (DataTable)Session["txtNumber"];
        }

        //Assign both fields with data
        int ID = Convert.ToInt16(Session["IDNumber"]);
        string txtnumber = txtContactNo.Text;
        string Cat = "";
        //string Cat = txtNumberCategory.SelectedItem.Text;
        //call AddNew method and pass id,name and object of datatable
        Dt.AddRow(ID, txtnumber, d, Cat);

        //Again bind it with gridview
        gvmultiplenumber.DataSource = d;
        gvmultiplenumber.DataBind();
        Session["txtNumber"] = d;
        txtContactNo.Text = "";


    }
    protected void gvmultiplenumber_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvmultiplenumber.PageIndex = e.NewPageIndex;
        gvmultiplenumber.DataSource = Session["txtNumber"];
        gvmultiplenumber.DataBind();
    }
    protected void gvmultiplenumber_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvmultiplenumber.EditIndex = e.NewEditIndex;
        gvmultiplenumber.DataSource = Session["txtNumber"];
        gvmultiplenumber.DataBind();
    }
    protected void gvmultiplenumber_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvmultiplenumber.EditIndex = -1;
        gvmultiplenumber.DataSource = Session["txtNumber"];
        gvmultiplenumber.DataBind();
    }
    protected void gvmultiplenumber_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataTable dt = (DataTable)Session["txtNumber"];
        GridViewRow row = gvmultiplenumber.Rows[e.RowIndex];

        dt.Rows[row.DataItemIndex]["Cat"] = ((DropDownList)(row.FindControl("drpeditcat"))).SelectedItem.Text;
        dt.Rows[row.DataItemIndex]["txtNumber"] = ((TextBox)(row.Cells[3].Controls[0])).Text;

        //Reset the edit index.gvmultipleaddress
        gvmultiplenumber.EditIndex = -1;
        Session["txtNumber"] = dt;
        //Bind data to the GridView control.
        gvmultiplenumber.DataSource = dt;
        gvmultiplenumber.DataBind();
    }
    protected void gvmultiplenumber_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                //Label lblEditcat = (Label)e.Row.FindControl("lblEditcat");

                //DropDownList drpeditcat = (DropDownList)e.Row.FindControl("drpeditcat");
                //DataTable dtCat = DBAccess.FetchData("select * from Customer_Type Order by Id Desc").Tables[0];
                //drpeditcat.DataSource = dtCat;
                //drpeditcat.DataValueField = "ID";
                //drpeditcat.DataTextField = "Name";
                //drpeditcat.DataBind();
                //drpeditcat.Items.Insert(0, "---Select---");
                //ListItem itm = (ListItem)drpeditcat.Items.FindByText(lblEditcat.Text.ToString());
                //if (itm != null)
                //{
                //    drpeditcat.SelectedValue = itm.Value;
                //}


            }
        }
    }
    #endregion


    #region multiplewebsite
    DataTable dtwebsite = new DataTable();
    protected void btnWebSiteAdd_Click(object sender, EventArgs e)
    {
        if (dtwebsite.Columns.Count == 0)
        {
            dtwebsite.Columns.Add((new DataColumn("website")));
        }
        if (ViewState["website"] != null)
        {
            dtwebsite = (DataTable)ViewState["website"];
        }
        dtwebsite.Rows.Add(txtWebsite.Text);
        gv_website.DataSource = dtwebsite;
        gv_website.DataBind();
        ViewState["website"] = dtwebsite;
        txtWebsite.Text = "";
    }

    private void SaveWebsite(string SupplierId)
    {
        if (btn_CompanyDetailsSubmit.Text == "UPDATE")
        {
            if (ViewState["SupplierId"] != null)
            {
                int n = ClassSupplierMaster.SupplierWebsite_Delete(SupplierId);
                if (n == 1)
                {

                }
            }
        }
        if (ViewState["website"] != null)
        {
            DataTable dt = ViewState["website"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Website = dt.Rows[i]["website"].ToString();
                    int n = ClassSupplierMaster.SupplierWebsiteSave(SupplierId, Website);
                }
            }
        }
    }
    protected void gv_website_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_website.EditIndex = -1;
        gv_website.DataSource = ViewState["website"];
        gv_website.DataBind();
    }
    protected void gv_website_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["website"];

        //Update the values.
        GridViewRow row = gv_website.Rows[e.RowIndex];
        dt.Rows.Remove(dt.Rows[row.DataItemIndex]);
        ViewState["website"] = null;
        ViewState["website"] = dt;
        gv_website.DataSource = dt;
        gv_website.DataBind();
    }

    #endregion


    #region multipleAddress
    public class DynamicMultipleAddress1
    {
        public DynamicMultipleAddress1() { }
        public DataTable CreateDataSource1()
        {
            DataTable dt = new DataTable();
            DataColumn identity = new DataColumn("ID", typeof(int));
            dt.Columns.Add(identity);

            dt.Columns.Add("Address1", typeof(string));
            dt.Columns.Add("Cat", typeof(string));

            return dt;
        }
        //This is the AddRow method to add a new row in Table dt 
        public void AddRow(int id, string name, DataTable dt, string Cat)
        {
            dt.Rows.Add(new object[] { id, name, Cat });
        }
    }


    private void SaveAddress(string SupplierId)
    {
        if (btn_CompanyDetailsSubmit.Text == "UPDATE")
        {
            if (ViewState["SupplierId"] != null)
            {
                int n = ClassSupplierMaster.SupplierAddress_Delete(SupplierId);
                if (n == 1)
                {

                }
            }
        }
        if (Session["MultipleAddress1"] != null)
        {
            DataTable dt = Session["MultipleAddress1"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Address = dt.Rows[i]["Address1"].ToString();
                    string Cat = dt.Rows[i]["Cat"].ToString();
                    int n = ClassSupplierMaster.SupplierAdddressSave(SupplierId, Address, Cat);
                    if (n == 1)
                    {

                    }
                }
            }
        }
    }
    protected void btnMultipleAddress_Click(object sender, EventArgs e)
    {
        string Address = txtAddressCategory.Text;
        if(Address=="")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Address Category.');", true);
            return;
        }

        DynamicMultipleAddress1 Dt = new DynamicMultipleAddress1();
        DataTable d = null;
        //If session is not there means first time addinf record.
        if (Session["MultipleAddress1"] == null)
        {
            d = Dt.CreateDataSource1();
            Session["IDClm1"] = 1;
        }
        //else we are converting session of datatable to data table and increament the column  id by 1;
        else
        {
            Session["IDClm1"] = Convert.ToInt16(Session["IDClm1"]) + 1;
            d = (DataTable)Session["MultipleAddress1"];
        }

        //Assign both fields with data
        int ID = Convert.ToInt16(Session["IDClm1"]);
        string name = txtAddress.Text;
        string Cat = txtAddressCategory.Text;
        //call AddNew method and pass id,name and object of datatable
        Dt.AddRow(ID, name, d, Cat);

        //Again bind it with gridview
        gvmultipleaddress.DataSource = d;
        gvmultipleaddress.DataBind();
        //finally assign to session also.
        Session["MultipleAddress1"] = d;
        txtAddress.Text = "";
        txtAddressCategory.Text = "";

    }
    protected void gvmultipleaddress_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //Retrieve the table from the session object.
        DataTable dt = (DataTable)Session["MultipleAddress1"];

        //Update the values.
        GridViewRow row = gvmultipleaddress.Rows[e.RowIndex];
        dt.Rows.Remove(dt.Rows[row.DataItemIndex]);
        Session["MultipleAddress1"] = null;
        Session["MultipleAddress1"] = dt;
        gvmultipleaddress.DataSource = dt;
        gvmultipleaddress.DataBind();
        //finally assign to session also.


    }
    protected void gvmultipleaddress_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvmultipleaddress.PageIndex = e.NewPageIndex;
        gvmultipleaddress.DataSource = Session["MultipleAddress1"];
        gvmultipleaddress.DataBind();
    }

    protected void gvmultipleaddress_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvmultipleaddress.EditIndex = -1;
        gvmultipleaddress.DataSource = Session["MultipleAddress1"];
        gvmultipleaddress.DataBind();
    }

    #endregion

    #region Email

    DataTable dtEmail = new DataTable();
    protected void btnAddEmail_Click(object sender, EventArgs e)
    {

        if (dtEmail.Columns.Count == 0)
        {
            dtEmail.Columns.Add((new DataColumn("Email")));
            dtEmail.Columns.Add((new DataColumn("active")));
        }
        if (ViewState["email"] != null)
        {
            dtEmail = (DataTable)ViewState["email"];
        }
        DataRow row = dtEmail.NewRow();
        row["Email"] = txtEmailId.Text;

        dtEmail.Rows.Add(row);

        gv_Email.DataSource = dtEmail;
        gv_Email.DataBind();
        ViewState["email"] = dtEmail;
        txtEmailId.Text = "";
    }
    private void SaveEmailId(string SupplierId)
    {
        if (btn_CompanyDetailsSubmit.Text == "UPDATE")
        {
            if (ViewState["SupplierId"] != null)
            {
                int n = ClassSupplierMaster.SupplierEmail_Delete(SupplierId);
                if (n == 1)
                {

                }
            }
        }
        if (ViewState["email"] != null)
        {
            DataTable dt = ViewState["email"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Email = dt.Rows[i]["Email"].ToString();
                    int n = ClassSupplierMaster.SupplierEmailSave(SupplierId, Email);
                }
            }
        }
    }
    protected void gv_Email_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_Email.EditIndex = -1;
        gv_Email.DataSource = ViewState["email"];
        gv_Email.DataBind();
    }
    protected void gv_Email_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["email"];

        //Update the values.
        GridViewRow row = gv_Email.Rows[e.RowIndex];
        dt.Rows.Remove(dt.Rows[row.DataItemIndex]);
        ViewState["email"] = null;
        ViewState["email"] = dt;
        gv_Email.DataSource = dt;
        gv_Email.DataBind();
    }
    #endregion

    #region BankDetails
    public class DynamicMultipleConcernedBankAddress
    {
        public DynamicMultipleConcernedBankAddress() { }
        public DataTable CreateDataSource4()
        {
            DataTable dt = new DataTable();
            DataColumn identity = new DataColumn("CID", typeof(int));
            dt.Columns.Add(identity);
            dt.Columns.Add("Cbahn", typeof(string));
            dt.Columns.Add("Cban", typeof(string));
            dt.Columns.Add("Cbanname", typeof(string));
            dt.Columns.Add("Cbanbranch", typeof(string));
            dt.Columns.Add("CbanAddress", typeof(string));
            dt.Columns.Add("Cbanifsccode", typeof(string));



            return dt;
        }
        //This is the AddRow method to add a new row in Table dt 
        public void AddRow(int id, DataTable dt, string Cbahn, string cban, string cbanname, string cbanbranch, string cbanAddress, string cbanifsccode)
        {
            dt.Rows.Add(new object[] { id, Cbahn, cban, cbanname, cbanbranch, cbanAddress, cbanifsccode });
        }
    }
    private void SaveBankDetails(string SupplierId)
    {
        if (btn_CompanyDetailsSubmit.Text == "UPDATE")
        {
            if (ViewState["SupplierId"] != null)
            {
                int n = ClassSupplierMaster.SupplierBank_Delete(SupplierId);
                if (n == 1)
                {

                }
            }
        }
        if (Session["BankDetails"] != null)
        {
            DataTable dt = Session["BankDetails"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string AccountHolderName = dt.Rows[i]["Cbahn"].ToString();
                    string BranchName = dt.Rows[i]["cbanbranch"].ToString();
                    string AccountNumber = dt.Rows[i]["cban"].ToString();
                    string BankAddress = dt.Rows[i]["cbanAddress"].ToString();
                    string BankName = dt.Rows[i]["cbanname"].ToString();
                    string IFSCCode = dt.Rows[i]["cbanifsccode"].ToString();

                    int n = ClassSupplierMaster.SupplierBankDetailsSave(SupplierId, AccountHolderName, BranchName, AccountNumber, BankAddress, BankName, IFSCCode);
                }
            }
        }
    }
    protected void btnBankAdd_Click(object sender, EventArgs e)
    {
        DynamicMultipleConcernedBankAddress Dt = new DynamicMultipleConcernedBankAddress();
        DataTable d = null;
        //If session is not there means first time addinf record.
        if (Session["BankDetails"] == null)
        {
            d = Dt.CreateDataSource4();
            Session["IDClm3"] = 1;
        }
        //else we are converting session of datatable to data table and increament the column  id by 1;
        else
        {
            Session["IDClm3"] = Convert.ToInt16(Session["IDClm3"]) + 1;
            d = (DataTable)Session["BankDetails"];
        }

        //Assign both fields with data
        int ID = Convert.ToInt16(Session["IDClm3"]);


        string Cbahn = txtBankAccountHolderName.Text;
        string cban = txtBankAccountNumber.Text;
        string cbanname = txtBankName.Text;
        string cbanbranch = txtBankBranchName.Text;
        string cbanAddress = txtBankAddress.Text;
        string cbanifsccode = txtIFSCCode.Text;
        //call AddNew method and pass id,name and object of datatable
        Dt.AddRow(ID, d, Cbahn, cban, cbanname, cbanbranch, cbanAddress, cbanifsccode);

        //Again bind it with gridview
        grdbankdetails.DataSource = d;
        grdbankdetails.DataBind();
        //finally assign to session also.
        Session["BankDetails"] = d;

        txtBankAccountHolderName.Text = "";
        txtBankAccountNumber.Text = "";
        txtBankName.Text = "";
        txtBankBranchName.Text = "";
        txtBankAddress.Text = "";
        txtIFSCCode.Text = "";


    }
    protected void grdbankdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //Retrieve the table from the session object.
        DataTable dt = (DataTable)Session["BankDetails"];

        //Update the values.
        GridViewRow row = grdbankdetails.Rows[e.RowIndex];
        dt.Rows.Remove(dt.Rows[row.DataItemIndex]);
        Session["BankDetails"] = null;
        Session["BankDetails"] = dt;
        grdbankdetails.DataSource = Session["BankDetails"];
        grdbankdetails.DataBind();
        //finally assign to session also.


    }
    protected void grdbankdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdbankdetails.PageIndex = e.NewPageIndex;
        grdbankdetails.DataSource = Session["BankDetails"];
        grdbankdetails.DataBind();
    }

    protected void grdbankdetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdbankdetails.EditIndex = -1;
        grdbankdetails.DataSource = Session["BankDetails"];
        grdbankdetails.DataBind();
    }
    #endregion

    #region Concerrnperson
    DataTable dtConPer = new DataTable();

    protected DataTable getDataTable()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();
        dtCol.ColumnName = "ConcernPersonName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ConcernPersonID";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "DesignationID";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "Designation";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "BoardDeskNo";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "DirectDeskNo";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "E_mail";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "E_mailid2";
        oTable.Columns.Add(dtCol);


        dtCol = new DataColumn();
        dtCol.ColumnName = "MobileNo1";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "MobileNo2";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "MobileNo3";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ResidentalAdress";
        oTable.Columns.Add(dtCol);


        return oTable;
    }

    private void SaveConcernperson(string SupplierId)
    {
        if (btn_CompanyDetailsSubmit.Text == "UPDATE")
        {
            if (ViewState["SupplierId"] != null)
            {
                int n = ClassSupplierMaster.SupplierConcernPerson_Delete(SupplierId);
            }
        }
        if (ViewState["TempStockIn"] != null)
        {
            DataTable dt = ViewState["TempStockIn"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string ConcernPersonName = dt.Rows[i]["ConcernPersonName"].ToString();
                    string ConcernPersonID = dt.Rows[i]["ConcernPersonID"].ToString();
                    string Designation = dt.Rows[i]["Designation"].ToString();
                    string BoardDeskNo = dt.Rows[i]["BoardDeskNo"].ToString();
                    string DirectDeskNo = dt.Rows[i]["DirectDeskNo"].ToString();
                    string E_mail = dt.Rows[i]["E_mail"].ToString();
                    string E_mailid2 = dt.Rows[i]["E_mailid2"].ToString();
                    string MobileNo1 = dt.Rows[i]["MobileNo1"].ToString();
                    string MobileNo2 = dt.Rows[i]["MobileNo2"].ToString();
                    string MobileNo3 = dt.Rows[i]["MobileNo3"].ToString();
                    string ResidentalAdress = dt.Rows[i]["ResidentalAdress"].ToString();
                    int n = ClassSupplierMaster.SupplierConcernPersonSave(ConcernPersonID, SupplierId, ConcernPersonName, Designation, BoardDeskNo, DirectDeskNo, E_mail, E_mailid2, MobileNo1, MobileNo2, MobileNo3, ResidentalAdress);
                }
            }
        }
    }
    private void ClearConcern()
    {
        txt_concernedpersonname.Text = "";
        ddl_cpnDesigation.SelectedIndex = 0;
        txtMobile1.Text = "";
        txtMobile2.Text = "";
        txtMobile3.Text = "";
        //txtBoardDeskNo.Text = "";
        //txtDirectDeskNo.Text = "";
        txtEmailID1.Text = "";
        txtalternateEmail.Text = "";
        txt_ConcernedPersonAddress.Text = "";
        btn_AddConPers.Text = "Add Concern Person";

    }
    protected void btn_AddConPers_Click(object sender, EventArgs e)
    {
        string Designation = "";
        string DesignationId = "";
        if (ddl_cpnDesigation.Text != "")
        {
            Designation = ddl_cpnDesigation.Text;
            DesignationId = ddl_cpnDesigation.Value.ToString();
        }
        else
        {
            DesignationId = "";
            Designation = "";
        }
        if (txt_concernedpersonname.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Enter Concerned Person Name');", true);
            return;
        }
        if (DesignationId == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Enter Concerned Person Designation');", true);
            return;
        }
        if (txtMobile1.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Enter Concerned Person Mobile No');", true);
            return;
        }
        if (txt_ConcernedPersonAddress.Text == "")
        {
            
        }
        if (txtEmailID1.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Enter Concerned Person Email');", true);
            return;
        }

        if (btn_AddConPers.Text != "Update Concern Person")
        {
            DataTable dt = (DataTable)ViewState["TempStockIn"];
            DataRow drH = dt.NewRow();
            drH["ConcernPersonName"] = txt_concernedpersonname.Text;
            drH["ConcernPersonID"] = "";
            drH["Designation"] = Designation;
            drH["DesignationID"] = DesignationId;
            drH["BoardDeskNo"] = "";
            drH["DirectDeskNo"] ="";
            drH["E_mail"] = txtEmailID1.Text;
            drH["E_mailid2"] = txtalternateEmail.Text;
            drH["MobileNo1"] = txtMobile1.Text;
            drH["MobileNo2"] = txtMobile2.Text;
            drH["MobileNo3"] = txtMobile3.Text;
            drH["ResidentalAdress"] = txt_ConcernedPersonAddress.Text;
            dt.Rows.Add(drH);

            ViewState["TempStockIn"] = dt;

            this.gv_ConcPer.DataSource = (DataTable)ViewState["TempStockIn"];
            this.gv_ConcPer.DataBind();
        }
        else
        {
            DataTable dt1 = (DataTable)ViewState["TempStockIn"];
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                int i = 0;
                if (ViewState["Rows"] != null)
                {
                    try
                    {
                        i = Convert.ToInt32(ViewState["Rows"]);
                    }
                    catch { }
                }
                dt1.Rows.RemoveAt(i);
            }
            DataTable dt = dt1;
            DataRow drH = dt.NewRow();
            string ConcernPersonId = "";
            if (ViewState["connPersonID"] != null)
            {
                ConcernPersonId = ViewState["connPersonID"].ToString();
            }
            drH["ConcernPersonName"] = txt_concernedpersonname.Text;
            drH["ConcernPersonID"] = ConcernPersonId;
            drH["Designation"] = Designation;
            drH["DesignationID"] = DesignationId;
            drH["BoardDeskNo"] ="";
            drH["DirectDeskNo"] ="";
            drH["E_mail"] = txtEmailID1.Text;
            drH["E_mailid2"] = txtalternateEmail.Text;
            drH["MobileNo1"] = txtMobile1.Text;
            drH["MobileNo2"] = txtMobile2.Text;
            drH["MobileNo3"] = txtMobile3.Text;
            drH["ResidentalAdress"] = txt_ConcernedPersonAddress.Text;
            dt.Rows.Add(drH);

            ViewState["TempStockIn"] = dt;

            this.gv_ConcPer.DataSource = (DataTable)ViewState["TempStockIn"];
            this.gv_ConcPer.DataBind();
        }
        ClearConcern();


    }

    protected void gv_ConcPer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = gvr.RowIndex;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["TempStockIn"];
            dt.Rows[rowindex].Delete();
            dt.AcceptChanges();
            ViewState["TempStockIn"] = dt;
            gv_ConcPer.DataSource = dt;
            gv_ConcPer.DataBind();
        }
        if (e.CommandName == "Edit")
        {

            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = gvr.RowIndex;
            fillConcernPerson(gvr.RowIndex);
        }

    }
    protected void fillConcernPerson(int rowindex)
    {
        //concern person
        DataTable dt_concernPersonDetails = (DataTable)ViewState["TempStockIn"];

        if (dt_concernPersonDetails.Rows.Count > 0)
        {
            txt_concernedpersonname.Text = dt_concernPersonDetails.Rows[rowindex]["ConcernPersonName"].ToString();
            txtMobile2.Text = dt_concernPersonDetails.Rows[rowindex]["MobileNo2"].ToString();
            txtMobile1.Text = dt_concernPersonDetails.Rows[rowindex]["MobileNo1"].ToString();
            txtMobile3.Text = dt_concernPersonDetails.Rows[rowindex]["MobileNo3"].ToString();
            //txtBoardDeskNo.Text = dt_concernPersonDetails.Rows[rowindex]["BoardDeskNo"].ToString();
            //txtDirectDeskNo.Text = dt_concernPersonDetails.Rows[rowindex]["DirectDeskNo"].ToString();
            txtEmailID1.Text = dt_concernPersonDetails.Rows[rowindex]["E_mail"].ToString();
            txtalternateEmail.Text = dt_concernPersonDetails.Rows[rowindex]["E_mailid2"].ToString();
            txt_ConcernedPersonAddress.Text = dt_concernPersonDetails.Rows[rowindex]["ResidentalAdress"].ToString();

            if (dt_concernPersonDetails.Rows[rowindex]["Designation"] != "" && dt_concernPersonDetails.Rows[rowindex]["DesignationId"] != "")
            {
                ddl_cpnDesigation.Value = dt_concernPersonDetails.Rows[rowindex]["DesignationId"].ToString();
            }
            ViewState["connPersonID"] = dt_concernPersonDetails.Rows[rowindex]["ConcernPersonID"].ToString();
            btn_AddConPers.Text = "Update Concern Person";


            ViewState["Rows"] = rowindex;



        }
    }
    protected void gv_ConcPer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gv_ConcPer_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_ConcPer.EditIndex = e.NewEditIndex;


    }
    protected void gv_ConcPer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ConcPer.PageIndex = e.NewPageIndex;
        gv_ConcPer.DataSource = ViewState["TempStockIn"];
        gv_ConcPer.DataBind();
    }
    #endregion

    protected void btn_CompanyDetailsSubmit_Click(object sender, EventArgs e)
    {
        string UserId = "";
        if (Session["UserId"] != null)
        {
            UserId = Session["UserId"].ToString();
        }
        string GodownId = "";
        if (Session["BranchId"] != null)
        {
            GodownId = Session["BranchId"].ToString();
        }
        string VendorFor = "";
        if (ddlVendorFor.SelectedIndex > 0)
        {
            VendorFor = ddlVendorFor.SelectedValue.ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Select Vendor For.');", true);
            return;
        }
        string MsmeNonMsme = "";
        if (ddlMsmeNonMsme.SelectedIndex > 0)
        {
            MsmeNonMsme = ddlMsmeNonMsme.SelectedValue.ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Select MSME OR NON MSME.');", true);
            return;
        }
        string ScStOrNot = "";
        if (ddlScStOrNot.SelectedIndex > 0)
        {
            ScStOrNot = ddlScStOrNot.SelectedValue.ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Select SC_ST OR NON-SC_ST.');", true);
            return;
        }
        string WomenOwner = "";
        if (ddlWomenOwner.SelectedIndex > 0)
        {
            WomenOwner = ddlWomenOwner.SelectedValue.ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Select Women Owner Yes Or No.');", true);
            return;
        }
        if (txtCompanyName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please enter Company Name.');", true);
            return;
        }
        string StateCode = "";
        if (ddlState.SelectedIndex > 0)
        {
            StateCode = ddlState.SelectedValue.ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Select a state.');", true);
            return;
        }
        DataTable dtContact = Session["txtNumber"] as DataTable;
        if (dtContact != null && dtContact.Rows.Count > 0)
        {

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please add atleast one Contact No.');", true);
            return;
        }


        DataTable dtemail = ViewState["email"] as DataTable;
        if (dtemail != null && dtemail.Rows.Count > 0)
        {

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please add atleast one Email Id.');", true);
            return;
        }
        string PanCard = txtPan.Text;
        if (txtPan.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please enter PAN No.');", true);
            return;
        }
        if (VendorFor == "Service")
        {
            if (txtPan.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please enter PAN No.');", true);
                return;
            }
            if (FileUpload1.HasFile)
            {

            }
            else
            {
                if (btn_CompanyDetailsSubmit.Text != "UPDATE")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please browse PAN Card.');", true);
                    return;
                }
                else
                {
                    if (Session["PANCard"] != null)
                    {
                        if (Session["PANCard"].ToString() != "")
                        {

                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please browse PAN Card.');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please browse PAN Card.');", true);
                        return;
                    }
                }
            }
        }
        if (btn_CompanyDetailsSubmit.Text != "UPDATE")
        {
            DataTable FatchPan = DBAccess.FetchDatatable("select PAN from tbl_SupplierMasterEntry where PAN='" + PanCard + "'");
            if (FatchPan.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Pan No Already Exist');", true);
                return;
            }
            DataTable GSTNox = DBAccess.FetchDatatable("select GSTNo from tbl_SupplierMasterEntry where GSTNo='" + PanCard + "'");
            if (GSTNox.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Pan No Already Exist');", true);
                return;
            }
        }


        Regex regex = new Regex("([A-Z]){5}([0-9]){4}([A-Z]){1}$");
        if (!regex.IsMatch(txtPan.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Enter Valid PAN Card No.');", true);
            return;
        }
         string GSTNo=txtGST.Text;
        if(GSTNo!="")
        {
        Regex GST = new Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$");
        if (!GST.IsMatch(txtGST.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Enter Valid GST No.');", true);
            return;
        }

        string GetPan = GSTNo.Substring(2, 10);
            if(PanCard!=GetPan)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Enter Valid GST No.');", true);
                return;
            }
        string Statecode = GSTNo.Substring(0,2);
            if(StateCode!=Statecode)
            {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('This GST No Not Valid for This State');", true);
                      return;
            }
        }


     
           

        Guid gd;
        gd = Guid.NewGuid();
        string guid = gd.ToString();

        string FileName3 = "";

        if (FileUpload1.HasFile)
        {
            try
            {
                FileName3 = Path.GetFileName(FileUpload1.FileName);
                string strPath = Server.MapPath("~");
                strPath = strPath + "\\Project\\IMG\\" + guid + '_' + FileName3;
                FileUpload1.SaveAs(strPath);
                PanCard = guid + '_' + FileName3;
            }
            catch
            {
                FileName3 = "";
            }
        }
        else
        {
            if (btn_CompanyDetailsSubmit.Text == "UPDATE")
            {
                if (Session["PANCard"] != null)
                {
                    PanCard = Session["PANCard"].ToString();
                }                
            }            
        }
        if(PanCard=="")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please browse PAN Card.');", true);
            return;
        }

        ////////////////////////////

        Guid gd1;
        gd1 = Guid.NewGuid();
        string guid1 = gd1.ToString();
        string GSTINDocuments = "";
        string FileName31 = "";

        if (FileUpload2.HasFile)
        {
            try
            {
                FileName31 = Path.GetFileName(FileUpload2.FileName);
                string strPath = Server.MapPath("~");
                strPath = strPath + "\\Project\\IMG\\" + guid1 + '_' + FileName31;
                FileUpload2.SaveAs(strPath);
                GSTINDocuments = guid1 + '_' + FileName31;
            }
            catch
            {
                FileName31 = "";
            }
        }
        else
        {
            if (btn_CompanyDetailsSubmit.Text == "UPDATE")
            {
                if (Session["GSTINDocuments"] != null)
                {
                    GSTINDocuments = Session["GSTINDocuments"].ToString();
                }
            }
        }
        if (GSTINDocuments == "")
        {
         
        }

        Guid gd2;
        gd2 = Guid.NewGuid();
        string guid2 = gd2.ToString();
        string BankDocuments = "";
        string FileName32 = "";

        if (FileUpload2.HasFile)
        {
            try
            {
                FileName32 = Path.GetFileName(FileUpload3.FileName);
                string strPath = Server.MapPath("~");
                strPath = strPath + "\\Project\\IMG\\" + guid2 + '_' + FileName32;
                FileUpload3.SaveAs(strPath);
                BankDocuments = guid2 + '_' + FileName32;
            }
            catch
            {
                FileName32 = "";
            }
        }
        else
        {
            if (btn_CompanyDetailsSubmit.Text == "UPDATE")
            {
                if (Session["BankDocuments"] != null)
                {
                    BankDocuments = Session["BankDocuments"].ToString();
                }
            }
        }

        ////////////////////////////


        string S = "";
        string SupplierId = "";
        DataTable dt = DBAccess.FetchDatatable("select top(1) SupplierId from tbl_SupplierMasterEntry order by RowId desc");
        if (dt != null && dt.Rows.Count > 0)
        {
            S = dt.Rows[0][0].ToString();
            int Counts = 0;
            try
            {
                string a = S.Split('-')[1];
                Counts = Convert.ToInt32(a);
            }
            catch { }
            Counts = Counts + 1;
            SupplierId = "V-" + Counts.ToString();
        }
        else
        {
            SupplierId = "V-" + 1.ToString();
        }


        //string SupplierId = DBAccess.FetchData("select [dbo].[fn_SupplierId]('V',5)").Tables[0].Rows[0][0].ToString();
        string GroupId = DBAccess.FetchData("select [dbo].[fn_SupplierGroupId]('VGroup',5)").Tables[0].Rows[0][0].ToString();
        string GroupName = txtGroupName.Text;
        if (txtGroupName.Text != "")
        {
            int m = ClassSupplierMaster.SupplierGroupSave(GroupId, GroupName, UserId, GodownId);
            if (m == 1)
            {

            } 
            else if (m == -1)
            {
                GroupId = txtGroupName.SelectedItem.Value.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('This Group Name Is not Valid');", true);
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Enter Group Name');", true);
            return;
        }
        string CompanyType = ddlCompanyType.Text;
        if (ddlCompanyType.Text != "")
        {
            int m = ClassSupplierMaster.SupplierCompanyTypeSave(CompanyType, UserId, GodownId);
            if (m == 1)
            {

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Enter Company Type');", true);
            return;
        }

        string FaxNumber = "";
        decimal CreditLimitAmount = 0;
        try
        {
            CreditLimitAmount = Convert.ToDecimal(txtCreditLimitAmount.Text);
        }
        catch { }
        int CreditLimitDay = 0;
        try
        {
            CreditLimitDay = Convert.ToInt32(txtCreditLimitDays.Text);
        }
        catch { }

        string PAN = txtPan.Text;
    
      
        string CompanyName = txtCompanyName.Text;
        bool VerifiedStatus = false;
        if (chbxVerified.Checked == true)
        {
            VerifiedStatus = true;
        }

        bool CompositionScheme = false;
        if (chbxCompositionScheme.Checked == true)
        {
            CompositionScheme = true;
        }
        string ChangeDate = DateTime.Now.ToString("dd/MM/yyyy");
        if (btn_CompanyDetailsSubmit.Text != "UPDATE")
        {
            int n = ClassSupplierMaster.Save(SupplierId, GroupId, CompanyType, CompanyName, FaxNumber, CreditLimitAmount, CreditLimitDay, PAN, GSTNo, UserId, GodownId, StateCode, VendorFor, VerifiedStatus, PanCard, BankDocuments, GSTINDocuments, CompositionScheme, ChangeDate, MsmeNonMsme, ScStOrNot, WomenOwner);
            if (n == 1)
            {
               
                //ContactSave
                SaveContactNo(SupplierId);

                //Website
                SaveWebsite(SupplierId);

                //EmailId
                SaveEmailId(SupplierId);

                //Address
                SaveAddress(SupplierId);

                //BankDetails
                SaveBankDetails(SupplierId);

                //ConcernPerson
                SaveConcernperson(SupplierId);

                SaveScanCopy(SupplierId);
                
                //SaveTDS
                SaveTDS(SupplierId);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Vendor & Contractor is Send for Verification ');window.location='../Project/SupplierMaster.aspx';", true);

            }
            else if (n == -1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('This Company Name Is Already Exist!');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Supplier Not Saved! Try Again');", true);
                return;
            }
        }
        else
        {
            if (ViewState["SupplierId"] != null)
            {
                SupplierId = ViewState["SupplierId"].ToString();
                int n = ClassSupplierMaster.Update(SupplierId, GroupId, CompanyType, CompanyName, FaxNumber, CreditLimitAmount, CreditLimitDay, PAN, GSTNo, UserId, GodownId, StateCode, VendorFor, VerifiedStatus, PanCard, BankDocuments, GSTINDocuments, CompositionScheme, ChangeDate,MsmeNonMsme,ScStOrNot,WomenOwner);
                if (n == 1)
                {
                    //ContactSave
                    SaveContactNo(SupplierId);

                    //Website
                    SaveWebsite(SupplierId);

                    //EmailId
                    SaveEmailId(SupplierId);

                    //Address
                    SaveAddress(SupplierId);

                    //BankDetails
                    SaveBankDetails(SupplierId);

                    //ConcernPerson
                    SaveConcernperson(SupplierId);

                    SaveScanCopy(SupplierId);

                    //SaveTDS
                    SaveTDS(SupplierId);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully');window.location='../Project/SupplierMaster.aspx';", true);

                }
                else if (n == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('This Company Name Is Already Exist!');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Supplier Not Updated! Try Again');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Supplier Not Updated As Supplier Id Is Not Fetched Properly ! Try Again');", true);
                return;
            }
        }
    }

    private void SaveTDS(string SupplierId)
    {
        if (btn_CompanyDetailsSubmit.Text == "UPDATE")
        {
            if (ViewState["SupplierId"] != null)
            {
                int n = ClassTDSMasterVendor.DeleteSupplierTDS(SupplierId);
            }
        }
        if (ViewState["TDSTable"] != null)
        {
            DataTable dt = ViewState["TDSTable"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Section = dt.Rows[i]["Section"].ToString();
                    decimal Percentage = 0;
                    try
                    {
                        Percentage = Convert.ToDecimal(dt.Rows[i]["Percentage"]);
                    }
                    catch { }
                    int n = ClassTDSMasterVendor.SaveSupplierTDS(SupplierId, Section, Percentage);
                }
            }
        }
    }
    protected void btn_Corres_Click(object sender, EventArgs e)
    {
        string Title = txtTitle.Text;
        if (Title == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Title.');", true);
            return;
        }
        Guid gd;


        gd = Guid.NewGuid();
        string guid = gd.ToString();

        string FileName3 = "";

        if (fuDoc.HasFile)
        {
            try
            {
                FileName3 = Path.GetFileName(fuDoc.FileName);
                string strPath = Server.MapPath("~");
                strPath = strPath + "\\Project\\IMG\\" + guid + '_' + FileName3;
                fuDoc.SaveAs(strPath);



            }
            catch
            {
                FileName3 = "";
            }
        }

        DataTable dt = (DataTable)ViewState["Dynamic_Image_2"];
        DataRow dr = dt.NewRow();
        dr["Image"] = FileName3;
        dr["guid"] = guid;
        dr["Title"] = Title;
        dt.Rows.Add(dr);
        dt.AcceptChanges();
        dt = (DataTable)ViewState["Dynamic_Image_2"];

        gv_Correspondance.DataSource = dt;
        gv_Correspondance.DataBind();

        txtTitle.Text = "";
        txtTitle.Focus();
    }
    protected void gv_Correspondance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {



    }
    protected void gv_Correspondance_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Delete")
        {
            DataTable dt = (DataTable)ViewState["Dynamic_Image_2"];
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            string imagepath = dt.Rows[rowindex]["Image"].ToString();
            string strPath = Server.MapPath("/Project/IMG/");
            strPath = strPath + imagepath;
            FileInfo file = new FileInfo(strPath);
            if (file.Exists)
            {
                file.Delete();
            }
            dt.Rows[rowindex].Delete();
            dt.AcceptChanges();
            dt = (DataTable)ViewState["Dynamic_Image_2"];

            gv_Correspondance.DataSource = dt;
            gv_Correspondance.DataBind();
        }
    }
    public DataTable CraeteUploadImageTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Image", typeof(string));
        dt.Columns.Add("guid", typeof(string));
        dt.Columns.Add("Title", typeof(string));
        dt.AcceptChanges();
        return dt;
    }
    protected void ASPxGridView1_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        object id = e.KeyValue;
        string SupplierId = id.ToString();

        if (e.CommandArgs.CommandName == "Edit")
        {
            ViewState["SupplierId"] = SupplierId;

            DataSet ds = ClassSupplierMaster.FetchSupplierEntry(SupplierId);
            if (ds != null)
            {
                try
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        try
                        {
                            ddlVendorFor.SelectedValue = dt.Rows[0]["VendorFor"].ToString();
                        }
                        catch { }
                        bool VerifiedStatus = false;
                        try
                        {
                            VerifiedStatus = Convert.ToBoolean(dt.Rows[0]["VerifiedStatus"]);
                        }
                        catch { }
                        if (VerifiedStatus == true)
                        {
                            chbxVerified.Checked = true;
                        }
                        else
                        {
                            chbxVerified.Checked = false;
                        }
                        txtGroupName.Value = dt.Rows[0]["GroupId"].ToString();
                        ddlCompanyType.Value = dt.Rows[0]["CompanyType"].ToString();
                        txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                        ddlMsmeNonMsme.Text = dt.Rows[0]["MsmeNonMsme"].ToString();
                        ddlScStOrNot.Text = dt.Rows[0]["ScStOrNot"].ToString();
                        ddlWomenOwner.Text = dt.Rows[0]["WomenOwner"].ToString();
                        //txtFaxNumber.Text = dt.Rows[0]["FaxNumber"].ToString();
                        try
                        {
                            ddlState.SelectedValue = dt.Rows[0]["StateCode"].ToString();
                        }
                        catch { }
                        txtCreditLimitAmount.Text = dt.Rows[0]["CreditLimitAmount"].ToString();
                        txtCreditLimitDays.Text = dt.Rows[0]["CreditLimitDay"].ToString();
                        txtPan.Text = dt.Rows[0]["PAN"].ToString();
                        txtGST.Text = dt.Rows[0]["GSTNo"].ToString();

                        Session["PANCard"] = dt.Rows[0]["PANCard"].ToString();
                        Session["BankDocuments"] = dt.Rows[0]["BankDocuments"].ToString();
                        Session["GSTINDocuments"] = dt.Rows[0]["GSTINDocuments"].ToString();

                        bool CompositionScheme = false;
                        try
                        {
                            CompositionScheme = Convert.ToBoolean(dt.Rows[0]["CompositionScheme"]);
                        }
                        catch { }
                        if (CompositionScheme == true)
                        {
                            chbxCompositionScheme.Checked = true;
                        }
                        else
                        {
                            chbxCompositionScheme.Checked = false;
                        }

                        btn_CompanyDetailsSubmit.Text = "UPDATE";
                    }
                }
                catch { }
                try
                {
                    DataTable dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Session["txtNumber"] = dt;
                        gvmultiplenumber.DataSource = dt;
                        gvmultiplenumber.DataBind();
                    }
                }
                catch { }

                try
                {
                    if (dtwebsite.Columns.Count == 0)
                    {
                        dtwebsite.Columns.Add((new DataColumn("website")));
                    }
                    if (ViewState["website"] != null)
                    {
                        dtwebsite = (DataTable)ViewState["website"];
                    }
                    DataTable dt = ds.Tables[2];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dtwebsite = dt;
                        ViewState["website"] = dt;
                        gv_website.DataSource = dt;
                        gv_website.DataBind();

                    }
                }
                catch { }

                try
                {
                    DataTable dt = ds.Tables[3];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvmultipleaddress.DataSource = dt;
                        gvmultipleaddress.DataBind();
                        Session["MultipleAddress1"] = dt;
                    }
                }
                catch { }

                try
                {
                    if (dtEmail.Columns.Count == 0)
                    {
                        dtEmail.Columns.Add((new DataColumn("Email")));
                        dtEmail.Columns.Add((new DataColumn("active")));
                    }
                    if (ViewState["email"] != null)
                    {
                        dtEmail = (DataTable)ViewState["email"];
                    }
                    DataTable dt = ds.Tables[4];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gv_Email.DataSource = dt;
                        gv_Email.DataBind();
                        ViewState["email"] = dt;
                        dtEmail = dt;
                    }
                }
                catch { }

                try
                {
                    DataTable dt = ds.Tables[6];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        grdbankdetails.DataSource = dt;
                        grdbankdetails.DataBind();
                        Session["BankDetails"] = dt;
                    }
                }
                catch { }

                try
                {
                    DataTable dt = ds.Tables[5];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataTable dt2 = getDataTable();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataRow drH = dt2.NewRow();
                            drH["ConcernPersonName"] = dt.Rows[j]["ConcernPersonName"].ToString();
                            drH["ConcernPersonID"] = dt.Rows[j]["ConcernPersonID"].ToString();
                            drH["Designation"] = dt.Rows[j]["Designation"].ToString();
                            drH["DesignationID"] = dt.Rows[j]["Designation"].ToString();
                            drH["BoardDeskNo"] = dt.Rows[j]["BoardDeskNo"].ToString();
                            drH["DirectDeskNo"] = dt.Rows[j]["DirectDeskNo"].ToString();
                            drH["E_mail"] = dt.Rows[j]["E_mail"].ToString();
                            drH["E_mailid2"] = dt.Rows[j]["E_mailid2"].ToString();
                            drH["MobileNo1"] = dt.Rows[j]["MobileNo1"].ToString();
                            drH["MobileNo2"] = dt.Rows[j]["MobileNo2"].ToString();
                            drH["MobileNo3"] = dt.Rows[j]["MobileNo3"].ToString();
                            drH["ResidentalAdress"] = dt.Rows[j]["ResidentalAdress"].ToString();
                            dt2.Rows.Add(drH);
                        }
                        ViewState["TempStockIn"] = dt2;

                        this.gv_ConcPer.DataSource = (DataTable)ViewState["TempStockIn"];
                        this.gv_ConcPer.DataBind();
                    }
                }
                catch { }

                try
                {
                    DataTable dt = ds.Tables[7];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataTable dt2 = CraeteUploadImageTable();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataRow drH = dt2.NewRow();
                            drH["image"] = dt.Rows[j]["image"].ToString();
                            drH["guid"] = dt.Rows[j]["guid"].ToString();
                            drH["Title"] = dt.Rows[j]["Title"].ToString();
                            dt2.Rows.Add(drH);
                        }
                        ViewState["Dynamic_Image_2"] = dt2;

                        this.gv_Correspondance.DataSource = (DataTable)ViewState["Dynamic_Image_2"];
                        this.gv_Correspondance.DataBind();
                    }
                }
                catch { }

                try
                {
                    DataTable dt = ds.Tables[8];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ViewState["TDSTable"] = getTDSTable();
                        DataTable dt2 = getTDSTable();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataRow drH = dt2.NewRow();
                            drH["Section"] = dt.Rows[j]["Section"].ToString();
                            drH["Percentage"] = dt.Rows[j]["Percentage"].ToString();
                            dt2.Rows.Add(drH);
                        }
                        ViewState["TDSTable"] = dt2;

                        this.gvTDS.DataSource = (DataTable)ViewState["TDSTable"];
                        this.gvTDS.DataBind();
                    }
                }
                catch { }

            }
        }

      
        if (e.CommandArgs.CommandName == "Active")
        {

            DateTime DOE = DateTime.Now;
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
            int n = ClassStaff.ActiveSupplier(SupplierId, DOE, UserId, BranchId);
            if (n == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Supplier activated successfully.'); window.location='../Project/SupplierMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Supplier is not activated');", true);
                return;
            }

        }
        if (e.CommandArgs.CommandName == "Deactive")
        {

            DateTime DOE = DateTime.Now;
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
            int n = ClassStaff.DeActiveSupplier(SupplierId, DOE, UserId, BranchId);
            if (n == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Supplier deactivated successfully.'); window.location='../Project/SupplierMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Supplier is not deactivated');", true);
                return;
            }

        }

        if (e.CommandArgs.CommandName == "Credentials")
        {

            DataTable dt = DBAccess.FetchDatatable(@"select * from [dbo].[tbl_SupplierConcernPersonDetails] where SupplierId='" + SupplierId + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                gvDocuments1.DataSource = dt;
                gvDocuments1.DataBind();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "JsModal", "lightbox_open('Items_Popup')", true);



        }

        if (e.CommandArgs.CommandName == "Bank")
        {
            string Link = DBAccess.FetchDatasingle("select BankDocuments from tbl_SupplierMasterEntry where SupplierId='" + SupplierId + "'");
            if (Link != "")
            {
                DownloadFile(Link);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' ! Sorry don't have documents Found');", true);
                return;
            }
        }
        if (e.CommandArgs.CommandName == "Pan")
        {
            string Link = DBAccess.FetchDatasingle("select PANCard from tbl_SupplierMasterEntry where SupplierId='" + SupplierId + "'");
            if (Link != "")
            {
                DownloadFile(Link);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' ! Sorry don't have documents Found');", true);
                return;
            }
        }
        if (e.CommandArgs.CommandName == "GST")
        {
            string Link = DBAccess.FetchDatasingle("select GSTINDocuments from tbl_SupplierMasterEntry where SupplierId='" + SupplierId + "'");
            if (Link != "")
            {
                DownloadFile(Link);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' ! Sorry don't have documents Found');", true);
                return;
            }
        }

    }
    protected void gvDocuments1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button lnk = (Button)sender;
        string LINK = lnk.CommandArgument.ToString();
        DownloadFile(LINK);
    }
    public void DownloadFile(string File)
    {
        string filePath = "IMG/" + File;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
    protected void btn_cancelCompanyDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierMaster.aspx");
    }

    private void SaveScanCopy(string SupplierId)
    {
        if (btn_CompanyDetailsSubmit.Text == "UPDATE")
        {
            if (ViewState["SupplierId"] != null)
            {
                int n = ClassSupplierMaster.SupplierScanCopy_Delete(SupplierId);
            }
        }
        if (ViewState["Dynamic_Image_2"] != null)
        {
            DataTable dt = ViewState["Dynamic_Image_2"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string image = dt.Rows[i]["image"].ToString();
                    string guid = dt.Rows[i]["guid"].ToString();
                    string Title = dt.Rows[i]["Title"].ToString();

                    int n = ClassSupplierMaster.SupplierScanCopy(image, guid, Title, SupplierId);
                }
            }
        }
    }
    protected void Gv_SupplierSummary_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            Button btnActive = (Button)Gv_SupplierSummary.FindRowCellTemplateControl(e.VisibleIndex, null, "btnActive");
            Button btnDeActive = (Button)Gv_SupplierSummary.FindRowCellTemplateControl(e.VisibleIndex, null, "btnDeActive");
            //ImageButton imgMail = (ImageButton)Gv_SupplierSummary.FindRowCellTemplateControl(e.VisibleIndex, null, "imgMail");
            string activestatus = e.GetValue("DeActive").ToString();
            //string VerifiedStatus = e.GetValue("ApproveStatus").ToString();
            if (activestatus == "Active")
            {
                btnActive.Visible = false;
                btnDeActive.Visible = true;
            }
            else
            {
                btnActive.Visible = true;
                btnDeActive.Visible = false;
            }
            //if (VerifiedStatus == "Verified")
            //{
            //    //imgMail.Visible = false;
            //}
            //else
            //{
            //    //imgMail.Visible = true;
            //}
            string UserGroup = "";
            try
            {
                UserGroup = Session["UserGroup"].ToString();
            }
            catch { }

            ImageButton imbEdit = (ImageButton)Gv_SupplierSummary.FindRowCellTemplateControl(e.VisibleIndex, null, "imbEdit");
            if (UserGroup == "SuperAdmin" || UserGroup == "Management")
            {
                imbEdit.Visible = true;
            }
            else
            {
                imbEdit.Visible = false;
            }

        }
    }
    
    private static Random random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptions());
    }
    protected void btnXlsxExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptions());
    }

    protected void btnAddTDS_Click(object sender, EventArgs e)
    {
        string Section = "";
        decimal Percentage = 0;
        if (ddlTDS.SelectedIndex > 0)
        {
            try
            {
                Percentage = Convert.ToDecimal(ddlTDS.SelectedValue);
            }
            catch { }
            Section = ddlTDS.SelectedItem.Text;

            DataTable dt8 = (DataTable)ViewState["TDSTable"];
            if (dt8 != null && dt8.Rows.Count > 0)
            {

            }


            DataTable dt = (DataTable)ViewState["TDSTable"];
            DataRow drH = dt.NewRow();
            drH["Section"] = Section;
            drH["Percentage"] = Percentage.ToString();
            dt.Rows.Add(drH);

            ViewState["TDSTable"] = dt;

            this.gvTDS.DataSource = (DataTable)ViewState["TDSTable"];
            this.gvTDS.DataBind();

            ddlTDS.SelectedIndex = 0;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select TDS Section!');", true);
            return;
        }
    }

    protected void gvTDS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTDS.PageIndex = e.NewPageIndex;
        this.gvTDS.DataSource = (DataTable)ViewState["TDSTable"];
        this.gvTDS.DataBind();
    }
    protected void gvTDS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt8 = new DataTable();
            dt8 = (DataTable)ViewState["TDSTable"];
            dt8.Rows[rowindex].Delete();
            dt8.AcceptChanges();
            ViewState["TDSTable"] = dt8;
            gvTDS.DataSource = dt8;
            gvTDS.DataBind();
        }
    }
    protected void gvTDS_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Download")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.


            //Fetch value of Country




        }


    }
}