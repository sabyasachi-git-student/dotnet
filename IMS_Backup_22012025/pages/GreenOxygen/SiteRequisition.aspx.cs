using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class pages_GreenOxygen_SiteRequisition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ReferenceEquals(ViewState["ItemDetails"], null))
        {
            ViewState["ItemDetails"] = getItemDataTable();
        }

        string RequisitionId = Request.QueryString["RequisitionId"];

        if(!IsPostBack)
        {
            if (RequisitionId != null)
            {
                FatchQueryStringData(RequisitionId);
                btnSave.Text = "Approve";
            }

            LoadCompany();
            LoadBranch();
            txtEstimatedBy.Text = Session["UserId"].ToString();
            txtEstimatedBy.Enabled = false;


            Bind_ddlBOQ();
            BindUnit();

            string UserId = "";
            try
            {
                UserId = Session["UserId"].ToString();
                
                string UserName = DBAccess.FetchDatasingle("select ug.UserGroup from tbl_User u join tbl_UserGroups ug on u.UserGroup=ug.UserGroupId  where UserName='" + UserId + "'");
                if (UserName == "SuperAdmin")
                {
                    dtpRequiredBy.Date = DateTime.Now;
                }
                else
                {
                    dtpRequiredBy.Date = DateTime.Now;
                    dtpRequiredBy.Enabled = false;
                }
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
        }
    }

    public void FatchQueryStringData(string RequisitionId)
    {
        DataTable dt = DBAccess.FetchDatatable("select  RequisitionPriority,WorkStage,WorkCatagory,WorkGroup, RequisitionId, Dates, ProjectId, ProjectTypeId, ProjectSubTypeId, ProcessId, Area, UnitId, UserId, CompanyId, DOE, ApproveStatus, EstimationFormulaId, RequiredByDate from [dbo].[tbl_SiteRequisition] where RequisitionId='" + RequisitionId + "'");
        if (dt.Rows.Count > 0)
        {
            ddlRequisitionPriority.Text = dt.Rows[0]["RequisitionPriority"].ToString();
            txtEstimatedBy.Text = dt.Rows[0]["UserId"].ToString();
            string ProcessId = dt.Rows[0]["ProcessId"].ToString();
            string WorkCatagory = dt.Rows[0]["WorkCatagory"].ToString();
            string WorkGroup = dt.Rows[0]["WorkGroup"].ToString();
            string ProcessName = DBAccess.FetchDatasingle("select ProcessName from [dbo].[tbl_ProcessMaster] where ProcessId='" + ProcessId + "'");
            //ddlProcess.Text = ProcessId;
            //dllWorkStage.Text = dt.Rows[0]["WorkStage"].ToString();
            ddl_BOQname.Text = WorkGroup;
            ddl_item.Text = WorkCatagory;
            txtArea.Text = dt.Rows[0]["Area"].ToString();
            string UnitId = dt.Rows[0]["UnitId"].ToString();
            string unitName = DBAccess.FetchDatasingle("select UnitName from [dbo].[tbl_LandUnitMaster] where RowId='" + UnitId + "'");
            ddlUnit.SelectedItem.Text = unitName;
            dtpRequiredBy.Text = Convert.ToDateTime(dt.Rows[0]["RequiredByDate"]).ToString();

            DataTable FatchDate = DBAccess.FetchDatatable("select e.EntryDate,e.EndDate from [tbl_SiteRequisition] s join [tbl_Estimation] e on s.EstimationFormulaId=e.EstimationId  where s.RequisitionId='" + RequisitionId + "'");
            if (FatchDate.Rows.Count > 0)
            {
                TextBox1.Text = Convert.ToDateTime(FatchDate.Rows[0]["EntryDate"]).ToString();
                TextBox2.Text = Convert.ToDateTime(FatchDate.Rows[0]["EndDate"]).ToString();
                
            }
            string SessionREqn = "";
            try
            {
                SessionREqn = Session["RequisitionId"].ToString();
            }
            catch
            { }
            if (SessionREqn == "")
            {
                Session["RequisitionId"] = RequisitionId;
            }



        }
        string ProjectId = "";
        string UserId = "";
        try
        {
            UserId = Session["UserId"].ToString();
            ProjectId = Session["BranchId"].ToString();
            Session["ProjectId"] = ProjectId;
        }
        catch { }
        string Project = DBAccess.FetchDatasingle("select BranchName from tbl_Branch where BranchId='" + ProjectId + "'");

        //lblProject.Text = Project;

        string EstmationId = DBAccess.FetchDatasingle("select EstimationFormulaId from [tbl_SiteRequisition] where RequisitionId='" + RequisitionId + "'");

        //        DataTable dtt = DBAccess.FetchDatatable(@"select  b.EstimationId,i.ItemId,i.ItemName,br.BrandName,cm.CatagoryName,gr.GroupName,'' as Qty,i.ModelNo,i.Remarks,i.HSNCode,
        //		 u.UnitName,convert(decimal(18,2),(b.Qty-isnull((select sum(s.Qty) from [tbl_SiteRequisitionRawMaterialApproval] s
        //		  join [dbo].[tbl_SiteRequisitionApproval] ra  on s.RequisitionApprovalId=ra.RequisitionApprovalId  where s.ItemId=b.ItemId and ra.RequisitionId in (select RequisitionId from dbo.[tbl_SiteRequisition] where EstimationFormulaId=b.EstimationId)),0))) as 'RQty' 
        //from [dbo].[tbl_EstimationRawMaterial] b 
        //		 join tbl_ItemMaster i on b.ItemId=i.ItemId 
        //		 join tbl_BrandMaster br on i.BrandId=br.BrandId 
        //		 join tbl_CatagoryMaster cm on i.CatagoryId=cm.CatagoryId 
        //		 join tbl_GroupMaster gr on i.GroupId=gr.GroupId  
        //		 join tbl_UnitMaster u on i.UnitId=u.UnitID where b.EstimationId='"+EstmationId+"'");

        DataTable dtt = DBAccess.FetchDatatable(@" select distinct i.ItemId,i.ItemName,su.UnitName as SecondanyUnit,cm.CatagoryName,gr.GroupName,( select estp.Qty from tbl_EstimationRawMaterial estp where ItemId=b.ItemId and EstimationId=src.EstimationFormulaId and ProjectId=src.ProjectId) as EstMatedQty,'' as Qty,i.Remarks,i.HSNCode,
		 u.UnitName,'' AS RQty
		  from tbl_SiteRequisition src 
		  join [dbo].[tbl_SiteRequisitionRawMaterial] b on src.RequisitionId=b.RequisitionId
		 
          join tbl_ItemMaster i on b.ItemId=i.ItemId 
		  left join tbl_UnitMaster su on i.SeconderyUnit=su.UnitID
		
		 join tbl_CatagoryMaster cm on i.CatagoryId=cm.CatagoryId 
		 join tbl_GroupMaster gr on i.GroupId=gr.GroupId  
		 join tbl_UnitMaster u on i.UnitId=u.UnitID where src.RequisitionId='" + RequisitionId + "'");
        DataTable dts = (DataTable)ViewState["ItemDetails"];
        for (int i = 0; i < dtt.Rows.Count; i++)
        {
            string NA = "NA";

            DataRow drH = dts.NewRow();
            string ItemId = dtt.Rows[i]["ItemId"].ToString();
            if (ItemId == "NA")
            {
                drH["ItemId"] = NA;
            }
            else
            {
                drH["ItemId"] = ItemId;
            }
            drH["ItemName"] = dtt.Rows[i]["ItemName"].ToString();

            string GroupName = dtt.Rows[i]["GroupName"].ToString();
            if (GroupName == "")
            {
                drH["GroupName"] = NA;
            }
            else
            {
                drH["GroupName"] = GroupName;
            }
            string CatagoryName = dtt.Rows[i]["CatagoryName"].ToString();
            if (CatagoryName == "")
            {
                drH["CatagoryName"] = NA;
            }
            else
            {
                drH["CatagoryName"] = CatagoryName;
            }
            string SecondanyUnit = dtt.Rows[i]["SecondanyUnit"].ToString();
            if (SecondanyUnit == "")
            {
                drH["SecondanyUnit"] = NA;
            }
            else
            {
                drH["SecondanyUnit"] = SecondanyUnit;
            }
            decimal RemainingQty = Convert.ToDecimal(dtt.Rows[i]["EstMatedQty"].ToString());
            string Qty = DBAccess.FetchDatasingle("select Qty from [dbo].[tbl_SiteRequisitionRawMaterial] where RequisitionId='" + RequisitionId + "' and ItemId='" + ItemId + "'");
            decimal Rqty = Convert.ToDecimal(Qty);
            drH["HSNCode"] = dtt.Rows[i]["HSNCode"].ToString();
            drH["Qty"] = Rqty.ToString("G29"); ;
            drH["RQty"] = RemainingQty.ToString("G29"); ;
            drH["GRNQty"] = FatchGRNQty(ItemId);
            drH["UnitName"] = dtt.Rows[i]["UnitName"].ToString();
            dts.Rows.Add(drH);


        }


        ViewState["ItemDetails"] = dts;

        this.gv_StockInDetails.DataSource = (DataTable)ViewState["ItemDetails"];
        this.gv_StockInDetails.DataBind();

        decimal GrandTotal = 0;

        DataTable dst = (DataTable)ViewState["ServiceTBL"];
        DataTable dt1 = DBAccess.FetchDatatable(@"select sm.ServiceItemId,sm.ServiceItemName,sg.ServiceGroup,u.UnitName,sc.ServiceCatagory,sm.Remarks,sa.Qty,sa.Rate,sa.Amount from
[tbl_RequsitionServiceItemDetails] sa
 join tbl_ServiceItemMaster sm  on sa.ServiceId=sm.ServiceItemId
join tbl_ServiceGroupMaster sg on sm.ServiceGroupId=sg.ServiceGroupId
join tbl_ServiceCatagoryMaster sc on sm.ServiceCatagoryId=sc.ServiceCatagoryId
join tbl_UnitMaster u on sm.UnitId=u.UnitID where sa.RequisitionId='" + RequisitionId + "'");
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            decimal TotalAmount = 0;
            DataRow drH = dst.NewRow();
            drH["ServiceGroup"] = dt1.Rows[i]["ServiceGroup"].ToString();
            drH["ServiceItemId"] = dt1.Rows[i]["ServiceItemId"].ToString();
            drH["ServiceItemName"] = dt1.Rows[i]["ServiceItemName"].ToString();
            drH["UnitName"] = dt1.Rows[i]["UnitName"].ToString();
            drH["ServiceCatagory"] = dt1.Rows[i]["ServiceCatagory"].ToString();

            drH["Remarks"] = dt1.Rows[i]["Remarks"].ToString();
            drH["Qty"] = dt1.Rows[i]["Qty"].ToString();


            drH["Rate"] = dt1.Rows[i]["Rate"].ToString();
            drH["Amount"] = dt1.Rows[i]["Amount"].ToString();
            TotalAmount = Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString());
            GrandTotal = GrandTotal + TotalAmount;
            dst.Rows.Add(drH);

            ViewState["ServiceTBL"] = dst;
        }

        //grdItemService.DataSource = ViewState["ServiceTBL"] as DataTable;

        //grdItemService.DataBind();
        //try
        //{
        //    ((TextBox)grdItemService.FooterRow.FindControl("txtTotal")).Text = GrandTotal.ToString();

        //}
        //catch
        //{ }


        //DataTable Manepower = DBAccess.FetchDatatable("select  r.RequisitionId,r.NoOfMan as  NoOFManPower, r.Category  as ManCatagory,m.ManPowerCatagory, r.TimeRequired, r.Costing as Cost, r.UserId, r.ProjectId, r.CompanyId, r.DOE from [dbo].[tbl_RequisitionManPower] r join tbl_ManPowerCatagoryMaster m on r.Category=m.ManPowerCatagoryId where RequisitionId='" + RequisitionId + "'");
        //for (int j = 0; j < Manepower.Rows.Count; j++)
        //{
        //    gvManPower.DataSource = Manepower;
        //    gvManPower.DataBind();
        //    ViewState["tblLabour"] = Manepower as DataTable;
        //}
        //DataTable Time = DBAccess.FetchDatatable("select Time as Days from [dbo].[tbl_SiteRequisition] where RequisitionId='" + RequisitionId + "'");
        //try
        //{


        //    int Days = Convert.ToInt32(Time.Rows[0]["Days"].ToString());
        //    if (Days != 0)
        //    {
        //        if (Time.Rows.Count > 0)
        //        {
        //            gvOthersDetails.DataSource = Time;
        //            gvOthersDetails.DataBind();
        //            ViewState["OtherTable"] = Time as DataTable;
        //        }

        //    }
        //}
        //catch
        //{

        //}
    }


    protected DataTable getItemDataTable()
    {
        DataTable oTable = new DataTable("ItemDetails");
        DataColumn dtCol = new DataColumn();
        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemId";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "ItemName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "SecondanyUnit";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "CatagoryName";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "GroupName";
        oTable.Columns.Add(dtCol);


        dtCol = new DataColumn();
        dtCol.ColumnName = "HSNCode";
        oTable.Columns.Add(dtCol);
        dtCol = new DataColumn();
        dtCol.ColumnName = "UnitName";
        oTable.Columns.Add(dtCol);
        dtCol = new DataColumn();
        dtCol.ColumnName = "RQty";
        oTable.Columns.Add(dtCol);
        dtCol = new DataColumn();
        dtCol.ColumnName = "Qty";
        oTable.Columns.Add(dtCol);

        dtCol = new DataColumn();
        dtCol.ColumnName = "GRNQty";
        oTable.Columns.Add(dtCol);

        return oTable;
    }

    public void LoadCompany()
    {
        
        string UserId = Session["UserId"].ToString();

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
    public void LoadBranch()
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
        LoadBranch();
    }
    protected void ddl_Project_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_Project.Text = ddl_Project.SelectedItem.ToString();
    }

    public void Bind_ddlBOQ()
    {
        DataTable dt = DBAccess.FetchDatatable("SELECT BoqId, BOQName FROM [dbo].[tbl_BOQ]");
        ddl_BOQname.DataSource = dt;
        ddl_BOQname.DataValueField = "BoqId";
        ddl_BOQname.DataTextField = "BOQName";
        ddl_BOQname.DataBind();
        ddl_BOQname.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddl_BOQname_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        string BoqId = ddl_BOQname.SelectedValue.ToString();

        DataTable dt = DBAccess.FetchDatatable("SELECT BoqId, BoqDetailsSlNo  FROM [dbo].[tbl_BOQDetails] where BoqId= '" + BoqId + "' ");
        ddl_Srno.DataSource = dt;
        ddl_Srno.DataValueField = "BoqDetailsSlNo";
        ddl_Srno.DataTextField = "BoqDetailsSlNo";
        ddl_Srno.DataBind();
        ddl_Srno.Items.Insert(0, new ListItem("--Select--", "0"));
    }

   

    protected void ddl_Srno_TextChanged(object sender, EventArgs e)
    {
        string BoqId = ddl_BOQname.SelectedValue.ToString();
        string Srno = ddl_Srno.SelectedValue.ToString();


        DataTable dt = DBAccess.FetchDatatable("select BoqDetailsId, Unit, ItemDescripton from tbl_BoqDetails where BoqId= '" + BoqId + "' and BoqDetailsSlNo= '" + Srno + "' ");
        ddl_item.DataSource = dt;
        ddl_item.ValueField = "ItemDescripton";
        ddl_item.TextField = "ItemDescripton";
        ddl_item.DataBind();

       // ddl_item.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlUnit.DataSource = dt;
        ddlUnit.DataValueField = "Unit";
        ddlUnit.DataTextField = "Unit";
        ddlUnit.DataBind();


    }

    protected void ddl_item_TextChanged(object sender, EventArgs e)
    {

      string UserId = Session["UserId"].ToString();
        string ProjectId, CompanyId;

        CompanyId = ddl_Company.SelectedValue.ToString();
        ProjectId = ddl_Project.SelectedValue.ToString();

        if (CompanyId == null || CompanyId == "-Select-")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Company Name ');", true);
            return;
        }
         
        if(ProjectId == null || ProjectId == "-Select-")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select  Branch');", true);
            return;
        }
       


        string WorkGroup = ddl_BOQname.SelectedItem.ToString();
        String Srno = ddl_Srno.Text;
        string WorkCatagory = ddl_item.Text;

        DataTable fatchPrevious = DBAccess.FetchDatatable("select * from tbl_SiteRequisition where WorkCatagory='" + WorkCatagory + "' and WorkGroup='" + WorkGroup + "' and ProjectId='" + ProjectId + "' and ApproveStatus is null and RejectedStatus is null");
        if (fatchPrevious.Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Approve Previous Item Of Works!');", true);
            return;
        }

        string EstimationId = "";
        DataTable dt = DBAccess.FetchDatatable("  select e.EstimationId, e.Dates,e.EntryDate,e.EndDate, e.ProjectId, e.ProcessType, e.ProjectTypeId, e.ProjectSubTypeId, e.ProcessId, e.Area, e.UnitId,e.Days, e.UserId, e.CompanyId, e.DOE, e.EstimationFormulaId, e.RevId from [dbo].[tbl_Estimation] e left join tbl_LandUnitMaster l on e.UnitId=l.RowId where WorkCatagory='" + WorkCatagory + "' and WorkGroup='" + WorkGroup + "' and ProjectId='" + ProjectId + "' ");

        if (dt.Rows.Count > 0)
        {

            EstimationId = dt.Rows[0]["EstimationId"].ToString();
            ViewState["EstimationId"] = EstimationId;
            //ddlUnit.SelectedValue = dt.Rows[0]["UnitId"].ToString();
            txtArea.Text = dt.Rows[0]["Area"].ToString();
            TextBox1.Text = Convert.ToDateTime(dt.Rows[0]["EntryDate"]).ToString("dd/MM/yyyy");
            TextBox2.Text = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("dd/MM/yyyy");


            txtArea.Enabled = false;
            ddlUnit.Enabled = false;


        }


        DataTable FatchItems = DBAccess.FetchDatatable(@"select  b.EstimationId,i.ItemId,i.ItemName,su.UnitName as SecondanyUnit,cm.CatagoryName,gr.GroupName,'' as Qty,i.Remarks,i.HSNCode,
		 u.UnitName,
		 convert(decimal(18,2),((select Quantity from tbl_ActualEstimatedDetails where ItemId=b.ItemId and EstimationId=b.EstimationId)-isnull((select sum(s.Qty) from [tbl_SiteRequisitionRawMaterialApproval] s
		  join [dbo].[tbl_SiteRequisitionApproval] ra  on s.RequisitionApprovalId=ra.RequisitionApprovalId 
		   where s.ItemId=b.ItemId and ra.RequisitionId in (select RequisitionId from dbo.[tbl_SiteRequisition] 
		   where EstimationFormulaId=b.EstimationId)),0))) as 'RQty' 
from [dbo].[tbl_EstimationRawMaterial] b 
		 join tbl_ItemMaster i on b.ItemId=i.ItemId 
		 left join tbl_UnitMaster su on i.SeconderyUnit=su.UnitID
		 join tbl_CatagoryMaster cm on i.CatagoryId=cm.CatagoryId 
		 join tbl_GroupMaster gr on i.GroupId=gr.GroupId  
		 join tbl_UnitMaster u on i.UnitId=u.UnitID where b.EstimationId='" + EstimationId + "'");
        if (FatchItems.Rows.Count > 0)
        {
            for (int i = 0; i < FatchItems.Rows.Count; i++)
            {
                string ItemId = FatchItems.Rows[i]["ItemId"].ToString();
                string RemainingQty = DBAccess.FetchDatasingle(@"select (e.Qty)-isnull(convert(decimal(18,2),(select sum(a.Qty) from tbl_SiteRequisitionRawMaterialApproval a
join tbl_SiteRequisitionApproval ei on a.RequisitionApprovalId=ei.RequisitionApprovalId where a.ItemId=d.ItemId and d.EstimationId=ei.EstimationFormulaId)),0) as dueQty from tbl_EstimationRawMaterial  e
join tbl_ActualEstimatedDetails d on e.EstimationId=d.EstimationId and e.ItemId=d.ItemId where e.EstimationId='" + EstimationId + "' and d.ItemId='" + ItemId + "'");

                var VRemainingQty = Convert.ToDouble(String.Format("{0:0.00}", RemainingQty));
                
                DataTable dts = (DataTable)ViewState["ItemDetails"];
                DataRow drH = dts.NewRow();
                drH["GroupName"] = FatchItems.Rows[i]["GroupName"].ToString();
                drH["ItemId"] = FatchItems.Rows[i]["ItemId"].ToString();
                drH["ItemName"] = FatchItems.Rows[i]["ItemName"].ToString();
                drH["CatagoryName"] = FatchItems.Rows[i]["CatagoryName"].ToString();
                drH["SecondanyUnit"] = FatchItems.Rows[i]["SecondanyUnit"].ToString();

                drH["HSNCode"] = FatchItems.Rows[i]["HSNCode"].ToString();
                drH["RQty"] = VRemainingQty.ToString();
                drH["Qty"] = VRemainingQty.ToString();
                drH["GRNQty"] = FatchGRNQty(ItemId);
                drH["UnitName"] = FatchItems.Rows[i]["UnitName"].ToString();


                dts.Rows.Add(drH);

                ViewState["ItemDetails"] = dts;


            }
            this.gv_StockInDetails.DataSource = (DataTable)ViewState["ItemDetails"];
            this.gv_StockInDetails.DataBind();
            //txtQty1.Text = "";
            foreach (GridViewRow row in gv_StockInDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("chbxItem");
                    if (chkrow.Checked == true)
                        chkrow.Checked = false;
                }
            }
        }



        //DataTable Manpower = DBAccess.FetchDatatable(" select b.EstimationId,b.NoOfMan as NoOFManPower, b.Category as ManCatagory,cm.ManPowerCatagory, b.LabourId as ManPowerCatagory, b.TimeRequired,b.UserId, b.ProjectId, b.CompanyId, b.DOE,'' as Cost, b.RevId from [dbo].[tbl_EstimationManPower] b join [tbl_Estimation] bud on b.EstimationId=bud.EstimationId join [dbo].[tbl_ManPowerCatagoryMaster] cm on b.LabourId=cm.ManPowerCatagoryId where  bud.EstimationId='" + EstimationId + "'");
        //if (Manpower.Rows.Count > 0)
        //{
        //    gvManPower.DataSource = Manpower;
        //    gvManPower.DataBind();
        //    ViewState["tblLabour"] = Manpower as DataTable;
        //}

        //DataTable Time = DBAccess.FetchDatatable("select Days from [tbl_Estimation] where EstimationId='" + EstimationId + "'");
        //if (Time.Rows.Count > 0)
        //{
        //    gvOthersDetails.DataSource = Time;
        //    gvOthersDetails.DataBind();
        //    ViewState["OtherTable"] = Time as DataTable;
        //}


    }


    public string FatchGRNQty(string ItemId)
    {
        string UserId = "";
        string CompanyId = "";
        string ProjectId = "";

        try
        {
            UserId = Session["UserId"].ToString();
        }
        catch
        {

        }
        try
        {
            ProjectId = ddl_Project.SelectedValue.ToString();
        }
        catch
        {
            
        }
        try
        {
            CompanyId = ddl_Company.SelectedValue.ToString();
        }
        catch
        {
            
        }

        //string ItemOfWorkss = ddlProcess.Text;
        //if (ItemOfWorkss == "")
        //{

        //}



        string WorkGroup = ddl_BOQname.Text;
        if (WorkGroup == "")
        {

        }
        string WorkCatagory = ddl_item.Text;
        if (WorkCatagory == "")
        {

        }


        string Qty = DBAccess.FetchDatasingle(@"select distinct sum ( sd.Quantity) from tbl_StockInItemDetails sd
join tbl_StockIn s on  s.StockInId=sd.StockInId
join tbl_PurchaseOrderApproval p on s.PO_Id=p.PO_ApprovalId
join tbl_PurchaseOrderApprovalQuotationDetails pa on p.PO_ApprovalId=pa.PO_ApprovalId
join tbl_ApproveQuotation aq on pa.QuotationId=aq.ApprovedQuotationId
join tbl_SiteRequisitionApproval sa on aq.RequisitionId=sa.RequisitionApprovalId where sa.WorkCatagory='" + WorkCatagory + "' and sa.WorkGroup='" + WorkGroup + "' and sa.ProjectId='" + ProjectId + "' and sd.ItemId='" + ItemId + "'");
        if (Qty == "")
        {
            Qty = "0";
        }
        return Qty;
    }

    protected void gv_StockInDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gv_StockInDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gv_StockInDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    private void BindUnit()
    {
        DataTable dt = DBAccess.FetchDatatable("select UnitName,RowId from [tbl_LandUnitMaster]");
        if (dt.Rows.Count > 0)
        {
            ddlUnit.DataSource = dt;
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "RowId";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("-----Select-----", "0"));
        }
    }

    public string FetchInvoiceNo(string Dates)
    {

        string UserId = Session["UserId"].ToString();
        string CompanyId = "";
        string ProjectId = "";

        try
        {
            ProjectId = ddl_Project.SelectedValue.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select  Branch');", true);
        }
        try
        {
            CompanyId = ddl_Company.SelectedValue.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select Company Name ');", true);
        }


        string SiteCode = DBAccess.FetchDatasingle("select SiteCode from [dbo].[tbl_Branch] where BranchId='" + ProjectId + "'");

        string voucherno = "";
        DateTime dt = DateTime.Now;
        try
        {
            //dt = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dt = DateTime.ParseExact(Dates, "MM/dd/yyyy", CultureInfo.InvariantCulture);
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
            string dtINV = DBAccess.FetchDatasingle("select RequisitionId from  [dbo].[tbl_SiteRequisition] order by RowId desc");


            if (dtINV != "")
            {
                string CountId = dtINV.Substring(17);
                int a = 0;
                try
                {
                    a = Convert.ToInt32(CountId);
                    a = a + 1;
                }
                catch { }
                voucherno = SiteCode + "/" + "STRQ/" + FinancialYear + "/" + a.ToString();
                ViewState["InvoiceCount"] = a;
                ViewState["FinancialYear"] = FinancialYear;
            }
            else
            {
                voucherno = SiteCode + "/" + "STRQ/" + FinancialYear + "/" + 1.ToString();
                ViewState["InvoiceCount"] = 1;
                ViewState["FinancialYear"] = FinancialYear;
            }
        }
        catch
        {
            voucherno = "";
            //try
            //{
            //    dt = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //}
            //catch { }
        }
        return voucherno;
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

        if (ddlRequisitionPriority.SelectedIndex > 0)
        {

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select Requisition Priority.');", true);
            return;
        }
        string RequisitionPriority = ddlRequisitionPriority.SelectedValue.ToString();


        string Dates = DateTime.Now.ToString("MM/dd/yyyy");


       

        string WorkStage = "";
        try
        {
            WorkStage = "";
        }
        catch
        {


        }

        string ItemOfWorkss = "";

        ItemOfWorkss = ddl_Srno.Text;

       // String ProcessId = ddl_item.Text;

        string WorkGroup = ddl_BOQname.SelectedItem.ToString();
        if (WorkGroup == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select  work Group.');", true);
            return;
        }
        string WorkCatagory = ddl_item.Text;
        if (WorkCatagory == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select  work Group.');", true);
            return;
        }
        //DataTable st = DBAccess.FetchDatatable("select  p.RowId,p.ProcessId,p.ProcessName,p.WorkCatagory,p.WorkGroup,p.ProcessType,p.ResourceRequired,p.UserId,p.DOE from  [tbl_ProcessMaster] p where p.ProcessName='" + ItemOfWorkss + "' and p.WorkCatagory='" + WorkCatagory + "' and p.WorkGroup='" + WorkGroup + "'");
        //if (st.Rows.Count > 0)
        //{

        //}
        //else
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This Item Of Work Is not a valid.');", true);
        //    return;
        //}




        string ProjectTypeId = "";

        int cnt = 0;
        if (gv_StockInDetails.Rows.Count > 0)
        {

            for (int i = 0; i < gv_StockInDetails.Rows.Count; i++)
            {
                CheckBox chk = gv_StockInDetails.Rows[i].FindControl("chbxItem") as CheckBox;
                if (chk.Checked == true)
                {
                    cnt = cnt + 1;
                }
            }
        }
        if (cnt == 0)
        {
            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('At first please check Items ');", true);
            //return;
        }
        for (int i = 0; i < gv_StockInDetails.Rows.Count; i++)
        {
            CheckBox chk = gv_StockInDetails.Rows[i].FindControl("chbxItem") as CheckBox;
            if (chk.Checked == true)
            {
                TextBox txtQty = gv_StockInDetails.Rows[i].FindControl("txtQty") as TextBox;
                decimal Qty = 0;
                try
                {
                    Qty = Convert.ToDecimal(txtQty.Text);
                }
                catch { }
                if (Qty == 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Valid Qty ');", true);
                    return;
                }

                string RemESTQty = gv_StockInDetails.Rows[i].Cells[8].Text;
                decimal RemQty = 0;
                try
                {
                    RemQty = Convert.ToDecimal(RemESTQty);
                }
                catch
                {

                }
                if (Qty > RemQty)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Requisition Qty can't be more then estimated Qty. ! ');", true);
                    return;
                }


            }
        }


        //decimal GrandTotal = 0;
        //for (int i = 0; i < grdItemService.Rows.Count; i++)
        //{
        //    decimal Total = 0;

        //    string Qty = ((TextBox)grdItemService.Rows[i].FindControl("txtServiceQty")).Text;
        //    string Rate = ((TextBox)grdItemService.Rows[i].FindControl("txtServiceRate")).Text;

        //    try
        //    {
        //        Total = Math.Round((Convert.ToDecimal(Qty) * Convert.ToDecimal(Rate)), 2);
        //        GrandTotal = GrandTotal + Total;
        //    }
        //    catch
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Check Service Item Grid');", true);
        //        return;
        //    }
        //    ((TextBox)grdItemService.Rows[i].FindControl("txtServiceAmount")).Text = Total.ToString();
        //}
        //try
        //{
        //    ((TextBox)grdItemService.FooterRow.FindControl("txtTotal")).Text = GrandTotal.ToString();
        //}
        //catch
        //{ }

        string ProjectSubTypeId = "";
        string ProcessId = "";
        ProcessId = ddl_Srno.Text;

        if (btnSave.Text != "Approve")
        {

            DataTable Prc = DBAccess.FetchDatatable("select * from [dbo].[tbl_Estimation] where ProcessId='" + ProcessId + "'");
            if (Prc.Rows.Count > 0)
            {
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('At first !please Complete Estimation for this Process.');", true);
                return;
            }
        }
        else
        {
            string process = ddl_Srno.Text;
            //ProcessId = DBAccess.FetchDatasingle("select ProcessId from tbl_ProcessMaster where ProcessName='" + process + "'");
        }
        string EstimationFormulaId = DBAccess.FetchDatasingle("select EstimationId from tbl_Estimation where  WorkCatagory='" + WorkCatagory + "' and WorkGroup='" + WorkGroup + "' and ProjectId='" + ProjectId + "'");


        decimal Area = 0;
        try
        {
            Area = Convert.ToDecimal(txtArea.Text);
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Area must be numeric.');", true);
            return;
        }
        String UnitId = ddlUnit.Text;
       
        decimal TotalCost = 0;

        string RequiredByDate = dtpRequiredBy.Text;
        bool Save = false;
        string RequisitionId = "";




        if (btnSave.Text == "Save")
        {
            DataTable dt = DBAccess.FetchDatatable("select * from tbl_SiteRequisition where RejectedStatus is null and workGroup='" + WorkGroup + "' and WorkCatagory='" + WorkCatagory + "' and ProjectId='" + ProjectId + "' and ProcessId='" + ProcessId + "'");
            if (dt.Rows.Count > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry This Item Of Work  already exist.');", true);
                //return;
            }


            RequisitionId = FetchInvoiceNo(Dates);
            int Time = 0;

            DateTime DOE = DateTime.Now;

//            Save = DBAccess.SaveData(@"insert into [dbo].[tbl_SiteRequisition](RequisitionPriority,RequisitionId,Dates,ProjectId,WorkStage,ProjectTypeId,ProjectSubTypeId,WorkGroup, WorkCatagory,ProcessId,
//                Area,UnitId,UserId,CompanyId,DOE,Time,EstimationFormulaId,RequiredByDate)
//                values('" + RequisitionPriority + "','" + RequisitionId + "',convert(datetime,'" + Dates + "',103),'" + ProjectId + "','" + WorkStage + "','" + ProjectTypeId + "','" + ProjectSubTypeId + "','" + WorkGroup + "','" + WorkCatagory + "', '" + ProcessId + "', '" + Area + "','" + UnitId + "','" + UserId + "','" + CompanyId + "',convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103),'" + Time + "','" + EstimationFormulaId + "',convert(datetime,'" + RequiredByDate + "',103))");

         //Save = ClassSiteRequisition.SaveRequisition(RequisitionPriority, RequisitionId, Dates, 
                                             // ProjectId, WorkStage,                                             
                                             // ProjectSubTypeId, ProjectTypeId, WorkGroup, 
                                             // WorkCatagory, ProcessId, 
                                             //s Area, UnitId, UserId, CompanyId, DOE,Time, EstimationFormulaId, RequiredByDate);
        //}
        //else if (btnSave.Text == "Update")
        //{
            //try
            //{
                //RequisitionId = ViewState["RequisitionId"].ToString();
            //}
            //catch { }
            //Save = DBAccess.SaveData(@"update [dbo].[tbl_SiteRequisition] set RequisitionPriority='" + RequisitionPriority + "',Dates=convert(datetime,'" + Dates + "',103),ProjectId='" + ProjectId + "',WorkStage='" + WorkStage + "',ProjectTypeId='" + ProjectTypeId + "',ProjectSubTypeId='" + ProjectSubTypeId + "',WorkGroup='" + WorkGroup + "',WorkCatagory='" + WorkCatagory + "',ProcessId='" + ProcessId + "', Area='" + Area + "',UnitId='" + UnitId + "',UserId='" + UserId + "',CompanyId='" + CompanyId + "',DOE=convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103),EstimationFormulaId='" + EstimationFormulaId + "',RequiredByDate=convert(datetime,'" + RequiredByDate + "',103) where RequisitionId='" + RequisitionId + "'");
            //if (Save == true)
            //{
                //int d = ClassSiteRequisition.DeleteRequisition(RequisitionId);
            //}

            bool Delete = DBAccess.SaveData("Delete from tbl_SiteRequisitionRawMaterial where RequisitionId='" + RequisitionId + "'");

            bool Delete1 = DBAccess.SaveData("Delete from tbl_RequisitionManPower where RequisitionId='" + RequisitionId + "'");
            bool Delete2 = DBAccess.SaveData("Delete from tbl_RequsitionServiceItemDetails where RequisitionId='" + RequisitionId + "'");

        }
        else if (btnSave.Text == "Approve")
        {
            RequisitionId = Request.QueryString["RequisitionId"];
            DateTime DOE = DateTime.Now;
            int Status = 0;
            string RequisitionApprovalId = DBAccess.FetchDatatable("select [dbo].[fn_RequsitionApprovalId]()").Rows[0][0].ToString();
            int Time = 0;
            //int SaveApproval = ClassRequisitionApproval.SaveApproval(RequisitionApprovalId, RequisitionPriority, RequisitionId, Dates, ProjectId, ProjectTypeId, ProjectSubTypeId, WorkGroup, WorkCatagory, ProcessId, WorkStage, Area, UnitId, UserId, CompanyId, DOE, Time, Status, EstimationFormulaId, RequiredByDate);

            //if (SaveApproval > 0)
            //{
                //bool UpdateStatus = DBAccess.SaveData("update [dbo].[tbl_SiteRequisition] set ApproveStatus=1 where RequisitionId='" + RequisitionId + "'");

                //decimal TotalItemPrice = 0;
                //decimal TotalnamPowerCost = 0;
                //decimal TotalMachineCost = 0;
                //if (gv_StockInDetails.Rows.Count > 0)
                //{
                    //for (int i = 0; i < gv_StockInDetails.Rows.Count; i++)
                    //{
                        //CheckBox chk = gv_StockInDetails.Rows[i].FindControl("chbxItem") as CheckBox;
                        //if (chk.Checked == true)
                        //{
                            //TextBox txtQty = gv_StockInDetails.Rows[i].FindControl("txtQty") as TextBox;


                            //string ItemId = gv_StockInDetails.Rows[i].Cells[1].Text;
                            //decimal Qty = 0;
                            //try
                            //{
                                //Qty = Convert.ToDecimal(txtQty.Text);
                            //}
                            //catch { }
                            //int Days = 0;
                            //try
                            //{

                            //}
                            //catch { }

                            //string HSNCode = gv_StockInDetails.Rows[i].Cells[5].Text;
                            //string ItemName = gv_StockInDetails.Rows[i].Cells[3].Text;
                            //string Unit = gv_StockInDetails.Rows[i].Cells[6].Text;

                            //int SaveApprovalRowMaterial = ClassRequisitionApproval.SaveRowMaterial(RequisitionApprovalId, ItemId, Qty, UserId, ProjectId, CompanyId, DOE, Days, ItemName, Unit, HSNCode);

                        }
                    }

                }
                //DataTable MainPower = ViewState["tblLabour"] as DataTable;
                //if (MainPower != null)
                //{
                //    if (MainPower.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < gvManPower.Rows.Count; i++)
                //        {


                //            string Category = gvManPower.Rows[i].Cells[1].Text;
                //            string CatagoryId = DBAccess.FetchDatasingle("select ManPowerCatagoryId from tbl_ManPowerCatagoryMaster where ManPowerCatagory='" + Category + "'");

                //            string TimeRequired = gvManPower.Rows[i].Cells[1].Text;
                //            //TextBox txttCost = gvManPower.Rows[i].FindControl("txtCost") as TextBox;
                //            decimal Costing = 0;
                //            int SaveMainPower = ClassRequisitionApproval.SaveManPowerAppruval(RequisitionApprovalId, CatagoryId, TimeRequired, Costing, UserId, ProjectId, CompanyId, DOE);
                //        }
                //    }
                //}

                //if (gvOthersDetails.Rows.Count > 0)
                //{

                //    try
                //    {
                //        Time = Convert.ToInt32(gvOthersDetails.Rows[0].Cells[0].Text);
                //    }
                //    catch
                //    {

                //    }

                //    bool updateTime = DBAccess.SaveData("update [tbl_SiteRequisitionApproval] set Time='" + Time + "' where RequisitionApprovalId='" + RequisitionApprovalId + "'");


                //}

                //DataTable ServiceTBL = ViewState["ServiceTBL"] as DataTable;

                //if (grdItemService.Rows.Count > 0)
                //{
                //    for (int i = 0; i < grdItemService.Rows.Count; i++)
                //    {
                //        TextBox txtServiceQty = grdItemService.Rows[i].FindControl("txtServiceQty") as TextBox;
                //        TextBox txtServiceRate = grdItemService.Rows[i].FindControl("txtServiceRate") as TextBox;
                //        TextBox txtServiceAmount = grdItemService.Rows[i].FindControl("txtServiceAmount") as TextBox;
                //        string ServiceId = grdItemService.Rows[i].Cells[0].Text;
                //        decimal Qty = 0;
                //        try
                //        {
                //            Qty = Convert.ToDecimal(txtServiceQty.Text);
                //        }
                //        catch { }
                //        decimal Rate = 0;
                //        try
                //        {
                //            Rate = Convert.ToDecimal(txtServiceRate.Text);
                //        }
                //        catch { }

                //        decimal Amount = 0;
                //        try
                //        {
                //            Amount = Convert.ToDecimal(txtServiceAmount.Text);
                //        }
                //        catch { }

                //        bool SaveService = DBAccess.SaveData("insert into [dbo].[tbl_ApproveRequsitionServiceItemDetails] ( RequisitionApprovalId, ServiceId, Qty, Rate, Amount, UserId, ProjectId, CompanyId, DOE ) values('" + RequisitionApprovalId + "', '" + ServiceId + "', '" + Qty + "', '" + Rate + "', '" + Amount + "','" + UserId + "','" + ProjectId + "','" + CompanyId + "', convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103))");


                //    }
                //}




                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Site Requisition is Approve successfully..');window.location='../Project/RequisitionApproval.aspx';", true);
            //}



        //}

        //if (btnSave.Text != "Approve")
        //{


        //    if (Save == true)
        //    {

        //        decimal TotalItemPrice = 0;
        //        decimal TotalnamPowerCost = 0;
        //        decimal TotalMachineCost = 0;
        //        if (gv_StockInDetails.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < gv_StockInDetails.Rows.Count; i++)
        //            {
        //                CheckBox chk = gv_StockInDetails.Rows[i].FindControl("chbxItem") as CheckBox;
        //                if (chk.Checked == true)
        //                {
        //                    TextBox txtQty = gv_StockInDetails.Rows[i].FindControl("txtQty") as TextBox;


        //                    string ItemId = gv_StockInDetails.Rows[i].Cells[1].Text;
        //                    decimal Qty = 0;
        //                    try
        //                    {
        //                        Qty = Convert.ToDecimal(txtQty.Text);
        //                    }
        //                    catch { }
        //                    int Days = 0;
        //                    try
        //                    {

        //                    }
        //                    catch { }

        //                    string HSNCode = gv_StockInDetails.Rows[i].Cells[5].Text;
        //                    string ItemName = gv_StockInDetails.Rows[i].Cells[2].Text;
        //                    string Unit = gv_StockInDetails.Rows[i].Cells[6].Text;
        //                    bool Save1 = DBAccess.SaveData(@"insert into [dbo].[tbl_SiteRequisitionRawMaterial](RequisitionId,ItemId,Qty,UserId,ProjectId,CompanyId,DOE,Days,ItemName,Unit,HSNCode)
        //                values('" + RequisitionId + "','" + ItemId + "','" + Qty + "','" + UserId + "','" + ProjectId + "','" + CompanyId + "',convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103),'" + Days + "','" + ItemName + "','" + Unit + "','" + HSNCode + "')");
        //                    if (Save1 == true)
        //                    {

        //                    }
        //                }
        //            }
        //        }
        //        try
        //        {


                    //DataTable MainPower = ViewState["tblLabour"] as DataTable;

                    //if (gvManPower.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < gvManPower.Rows.Count; i++)
                    //    {
                    //        decimal NoOfMan = 0;
                    //        DateTime DOE = DateTime.Now;

                    //        string Category = gvManPower.Rows[i].Cells[1].Text;
                    //        string CatagoryId = DBAccess.FetchDatasingle("select ManPowerCatagoryId from tbl_ManPowerCatagoryMaster where ManPowerCatagory='" + Category + "'");
                    //        TextBox txtNoManPower = gvManPower.Rows[i].FindControl("txtNoManPower") as TextBox;
                    //        string TimeRequired = gvManPower.Rows[i].Cells[2].Text;
                    //        try
                    //        {
                    //            NoOfMan = Convert.ToDecimal(txtNoManPower.Text);
                    //        }
                    //        catch
                    //        { }

                    //        decimal Costing = 0;
                    //        try
                    //        {
                    //            Costing = 0;
                    //        }
                    //        catch
                    //        {
                    //        }
                    //        int SaveMainPower = ClassRequisitionApproval.SaveManPower(RequisitionId, CatagoryId, TimeRequired, Costing, UserId, ProjectId, CompanyId, DOE, NoOfMan);
                    //    }
                    //}


                    //DataTable ServiceTBL = ViewState["ServiceTBL"] as DataTable;

                    //if (grdItemService.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < grdItemService.Rows.Count; i++)
                    //    {
                    //        TextBox txtServiceQty = grdItemService.Rows[i].FindControl("txtServiceQty") as TextBox;
                    //        TextBox txtServiceRate = grdItemService.Rows[i].FindControl("txtServiceRate") as TextBox;
                    //        TextBox txtServiceAmount = grdItemService.Rows[i].FindControl("txtServiceAmount") as TextBox;
                    //        string ServiceId = grdItemService.Rows[i].Cells[0].Text;
                    //        decimal Qty = 0;
                    //        try
                    //        {
                    //            Qty = Convert.ToDecimal(txtServiceQty.Text);
                    //        }
                    //        catch { }
                    //        decimal Rate = 0;
                    //        try
                    //        {
                    //            Rate = Convert.ToDecimal(txtServiceRate.Text);
                    //        }
                    //        catch { }

                    //        decimal Amount = 0;
                    //        try
                    //        {
                    //            Amount = Convert.ToDecimal(txtServiceAmount.Text);
                    //        }
                    //        catch { }

                    //        bool SaveService = DBAccess.SaveData("insert into [dbo].[tbl_RequsitionServiceItemDetails] ( RequisitionId, ServiceId, Qty, Rate, Amount, UserId, ProjectId, CompanyId, DOE ) values('" + RequisitionId + "', '" + ServiceId + "', '" + Qty + "', '" + Rate + "', '" + Amount + "','" + UserId + "','" + ProjectId + "','" + CompanyId + "', convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103))");


                    //    }
                    //}
                //}
                //catch
                //{

                //}

                //if (gvOthersDetails.Rows.Count > 0)
                //{
                //    int Time = 0;
                //    try
                //    {
                //        Time = Convert.ToInt32(gvOthersDetails.Rows[0].Cells[0].Text);
                //    }
                //    catch
                //    {

                //    }

                //    bool updateTime = DBAccess.SaveData("update [tbl_SiteRequisition] set Time='" + Time + "' where RequisitionId='" + RequisitionId + "'");


                //}

        //        if (btnSave.Text == "Update")
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Site Requisition is Update successfully..');window.location='../GreenOxygen/SiteRequisition.aspx';", true);

        //        }
        //        else
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Site Requisition is saved successfully..');window.location='../GreenOxygen/SiteRequisition.aspx';", true);

        //        }

        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Site Requisition is not saved.Try again.');", true);
        //        return;
        //    }

        //}




    //}

    //protected void btnClear_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("SiteRequisition.aspx");
    //}











   

    
//}