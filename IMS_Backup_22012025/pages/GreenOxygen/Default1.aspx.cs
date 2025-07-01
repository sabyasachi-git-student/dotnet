using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
//using System.Windows.Forms;
using System.IO;


public partial class pages_GreenOxygen_Default1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItem();
        }
       
        if (!IsPostBack)
        {
            RequisitionPurpose();
            BranchLoad();
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

    public void BranchLoad()
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
        DataTable dt = ClassUserBranchMapping.SaveAndFetchBranch(UserId, BranchId, UserId);
        if (dt != null && dt.Rows.Count > 0)
        {

            ddl_Project.Items.Clear();
            ddl_Project.DataSource = dt;
            ddl_Project.DataTextField = "BranchName";
            ddl_Project.DataValueField = "BranchId";
            ddl_Project.DataBind();
        }
    }

    protected DataTable getItem()
    {
        DataTable oTable = new DataTable();
        DataColumn dtCol = new DataColumn();

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

    private void RequisitionPurpose()
    {
        DataTable dt = DBAccess.FetchDatatable("select Distinct ID,PurposeName from tbl_PurposeReqMaster");
        if (dt.Rows.Count > 0)
        {
            ddlRequisitionPur.DataSource = dt;
            ddlRequisitionPur.DataTextField = "PurposeName";
            ddlRequisitionPur.DataValueField = "ID";
            ddlRequisitionPur.DataBind();
            ddlRequisitionPur.Items.Insert(0, new ListItem("-----Select-----", "0"));
        }
    }

    public void RequisitiontoBranch()
    {
        string BranchName = txtBranchName.Text;

        DataTable dt = DBAccess.FetchDatatable("select Distinct a.SectionId, b.BranchId from tbl_HigherKeyMapping a join tbl_Branch b on a.SectionId=b.BranchName where POPId='"+BranchName+"'");
        ddlRequisitionto.DataSource = dt;
        ddlRequisitionto.DataValueField = "BranchId";
        ddlRequisitionto.DataTextField = "SectionId";
        ddlRequisitionto.DataBind();
        ddlRequisitionto.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btn_AddList_Click(object sender, EventArgs e)
    {
        //DataTable dt1 = (DataTable)ViewState["AddList"];
        //DataRow dr = dt1.NewRow();


        //string ItemName = ddl_ItemName.SelectedItem.ToString();
        //if (ItemName == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select ItemName!');", true);
        //    return;
        //}

        //string GroupName = ddl_Group.SelectedItem.ToString();
        //if (GroupName == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select GroupName!');", true);
        //    return;
        //}

        //string CategoryName = ddl_Category.SelectedItem.ToString();
        //if (CategoryName == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select Category!');", true);
        //    return;
        //}

        //string ItemUnit = txtItemUnit.Text;

        //Decimal Qty  ;
        //if (txt_Qty.Text == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Quentity!');", true);
        //    return;
        //}
        //else
        //{
        //    Qty = Convert.ToDecimal(txt_Qty.Text);
        //}

        //decimal Rate;
        //if (txt_Rate.Text == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Rate!');", true);
        //    return;
        //}
        //else
        //{
        //    Rate=Convert.ToDecimal(txt_Rate.Text);
        //}

        //decimal AmtTotal = Convert.ToDecimal(txt_Total.Text);



        //dr["ItemName"] = ItemName;
        //dr["GroupName"] = GroupName;
        //dr["CategoryName"] = CategoryName;
        //dr["ItemUnit"] = ItemUnit;
        //dr["Qty"] = Qty;
        //dr["Rate"] = Rate;
        //dr["AmtTotal"] = AmtTotal;

        //dt1.Rows.Add(dr);
        //ViewState["AddList"] = dt1;
        //grdExtraWorkList.DataSource = ViewState["AddList"] as DataTable;
        //grdExtraWorkList.DataBind();

        ////DropDownList1.SelectedIndex = 0;

        ////if (BOQmode == "With BOQ")
        ////{
        ////    ddl_BOQname.SelectedIndex = 0;

        ////    ddl_item.SelectedIndex = 0;
        ////}
        //txt_Remarks.Enabled = false;
        //txt_WorkDes.Enabled = false;
        //txt_Date.Enabled = false;
        //DropDownList1.Enabled = false;
        //ddl_BOQname.Enabled = false;
        //ddl_item.Enabled = false;
        //ddl_Srno.Enabled = false;
        //ddl_AccUnit.Enabled = false;
        ////txt_Date.Text = "";
        ////txt_AddUnit.Text = "";
        //ddl_ItemName.SelectedIndex = 0;
        //ddl_Group.SelectedIndex = 0;
        //ddl_Category.SelectedIndex = 0;
        //txtItemUnit.Text = "";
        //txt_Qty.Text = "";
        //txt_Rate.Text = "";
        //txt_Total.Text = "";

        ////ddl_Srno.SelectedIndex = 0;
        //// ddl_item.Text = "";

        //this.btn_AddList.Focus();
    }

    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //string UserId = "";
        //try
        //{
        //    UserId = Session["UserId"].ToString();
        //}
        //catch
        //{

        //}
        //string CompanyId = ddl_Company.SelectedValue.ToString();
        //string ProjectId = ddl_Project.SelectedValue.ToString();

        //if (CompanyId == "" || CompanyId == "-Select-")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Company Name ');", true);
        //    return;
        //}
        //if (ProjectId == "" || ProjectId == "-Select-")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Project Name ');", true);
        //    return;
        //}

        //string FileUpload = string.Empty; ;
        //Guid gd;
        //gd = Guid.NewGuid();
        //string guid = gd.ToString();
        //string FileName3 = "";
        //if (FileUpload1.HasFile)
        //{
        //    try
        //    {
        //        FileName3 = Path.GetFileName(FileUpload1.FileName);
        //        string strPath = Server.MapPath("~");
        //        strPath = strPath + "\\Project\\IMG\\" + guid + '_' + FileName3;
        //        FileUpload = guid + '_' + FileName3;
        //        FileUpload1.SaveAs(strPath);
        //    }
        //    catch
        //    {
        //        FileName3 = "";
        //    }
        //}
        //DateTime DOE = DateTime.Now;
        //string EXTRWRID = DBAccess.FetchDatatable("select [dbo].[fn_BOQExtrWrkID]()").Rows[0][0].ToString();
        //string BOQmode = DropDownList1.SelectedItem.ToString();
        //if (BOQmode == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select BOQ Mode!');", true);
        //    return;
        //}
        //string UserDate = txt_Date.Text;
        //string BOQID = ddl_BOQname.SelectedValue.ToString();
        //string Srno = ddl_Srno.SelectedValue.ToString();
        //string BOQname = ddl_BOQname.SelectedItem.ToString();
        //string ItemDescription = ddl_item.Text;
        //string WorkDescription = txt_WorkDes.Text;
        //string Remark = txt_Remarks.Text;
        //string AccUnit = ddl_AccUnit.Text;

        //int savelist1 = ClassBOQExtraWork.SaveMain(EXTRWRID, BOQmode, UserDate, BOQID, BOQname, Srno, ItemDescription, FileUpload, WorkDescription, Remark, AccUnit, DOE, UserId, CompanyId, ProjectId);
        //if (savelist1 == 1)
        //{
        //    DataTable fatchdata = ViewState["AddList"] as DataTable;
        //    try
        //    {
        //        for (int i = 0; i < fatchdata.Rows.Count; i++)
        //        {
        //            string ItemName = fatchdata.Rows[i]["ItemName"].ToString();
        //            string GroupName = fatchdata.Rows[i]["GroupName"].ToString();
        //            string CategoryName = fatchdata.Rows[i]["CategoryName"].ToString();
        //            string ItemUnit = fatchdata.Rows[i]["ItemUnit"].ToString();
        //            Decimal Qty = Convert.ToDecimal(fatchdata.Rows[i]["Qty"].ToString());
        //            decimal Rate = Convert.ToDecimal(fatchdata.Rows[i]["Rate"].ToString());
        //            decimal AmtTotal = Convert.ToDecimal(fatchdata.Rows[i]["AmtTotal"].ToString());


        //            int savelist = ClassBOQExtraWork.Save(EXTRWRID, BOQmode, BOQID, BOQname, Srno, ItemDescription, ItemName, GroupName, CategoryName, ItemUnit, Qty, Rate, AmtTotal, DOE, UserId, CompanyId, ProjectId);
        //        }
        //    }
        //    catch
        //    {
        //        bool BOQExtraWorkMain = DBAccess.SaveData("delete from tbl_BOQExtraWorkMain where Extrwrid='" + EXTRWRID + "'");
        //        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data not Saved Please Check Properly!');", true);
        //        return;
        //    }

        //    DataTable fatchdataLabour = ViewState["AddListLabour"] as DataTable;
        //    try
        //    {
        //        for (int i = 0; i < fatchdata.Rows.Count; i++)
        //        {
        //            string LabourType = fatchdataLabour.Rows[i]["LabourType"].ToString();
        //            decimal LabourNo = Convert.ToDecimal(fatchdataLabour.Rows[i]["LabourNo"].ToString());
        //            decimal LabourRate = Convert.ToDecimal(fatchdataLabour.Rows[i]["LabourRate"].ToString());
        //            decimal TotalRate = Convert.ToDecimal(fatchdataLabour.Rows[i]["TotalRate"].ToString());

        //            int savelistlabour = ClassBOQExtraWork.SaveLabour(EXTRWRID, BOQmode, BOQID, BOQname, Srno, ItemDescription, LabourType, LabourNo, LabourRate, TotalRate, DOE, UserId, CompanyId, ProjectId);
        //        }
        //    }
        //    catch
        //    {
        //        bool BOQExtraWorkMain = DBAccess.SaveData("delete from tbl_BOQExtraWorkMain where Extrwrid='" + EXTRWRID + "'");
        //        bool BOQExtraWork = DBAccess.SaveData("delete from tbl_BOQExtraWork where Extrwrid='" + EXTRWRID + "'");
        //        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data not Saved Please Check Properly!');", true);
        //        return;
        //    }
        //}

        //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Extra Work save Successfully .');window.location='../GreenOxygen/Default1.aspx';", true);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default1.aspx");
    }

    
  
    protected void ddlRequToGrp_TextChanged(object sender, EventArgs e)
    {
        string Requisitionto = "";
        try
        {
            Requisitionto = ddlRequisitionto.DataValueField.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Select Requisition to First');", true);
            return;
        }
        string RequToGrp = ddlRequToGrp.SelectedValue.ToString();

        DataTable dtt = DBAccess.FetchDatatable("select UserName from tbl_User where BranchId='" + Requisitionto + "' and UserGroup='" + RequToGrp + "'");
        if (dtt != null && dtt.Rows.Count > 0)
        {
            ddlRequToUser.DataSource = dtt;
            ddlRequToUser.DataValueField = "UserName";
            ddlRequToUser.DataTextField = "UserName";
            ddlRequToUser.DataBind();
            ddlRequToUser.Items.Insert(0, new ListItem("--Select--", "0"));
            
        }
    }
}