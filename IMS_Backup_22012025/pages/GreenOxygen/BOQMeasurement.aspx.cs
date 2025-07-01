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
public partial class pages_GreenOxygen_BOQMeasurement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCompany();
            LoadBranch();

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



    protected void ddlMeasurementFormula_TextChanged(object sender, EventArgs e)
    {
        string AreaOrVolum = "";
        try
        {
            AreaOrVolum = ddlMeasurementFormula.SelectedItem.Value.ToString();
        }
        catch
        {

        }
        if (AreaOrVolum == "Area")
        {
            AryaPart.Visible = false;
            VoluamPart.Visible = true;

        }
        if (AreaOrVolum == "volume")
        {
            AryaPart.Visible = false;
            VoluamPart.Visible = true;
        }
        if (AreaOrVolum == "PICS")
        {
            trRUNNINGLENGTH.Visible = false;
            AryaPart.Visible = false;
            trPICS.Visible = false;
            VoluamPart.Visible = true;
        }
        if (AreaOrVolum == "RUNNINGLENGTH")
        {
            trRUNNINGLENGTH.Visible = false;
            AryaPart.Visible = false;
            trPICS.Visible = false;
            VoluamPart.Visible = true;
        }
        if (AreaOrVolum == "0")
        {
            trRUNNINGLENGTH.Visible = false;
            AryaPart.Visible = false;
            trPICS.Visible = false;
            VoluamPart.Visible = false;
        }
    }
    protected void txtlength_TextChanged(object sender, EventArgs e)
    {
        decimal Length;
        if (txtlength.Text == "")
        {
            Length = 1;
        }
        else
        {
            Length = Convert.ToDecimal(txtlength.Text);
        }

        decimal Breadth;
        if (txtbreadth.Text == "")
        {
            Breadth = 1;
        }
        else
        {
            Breadth = Convert.ToDecimal(txtbreadth.Text);
        }

        decimal Height;
        if (txtheight.Text == "")
        {
            Height = 1;
        }
        else
        {
            Height = Convert.ToDecimal(txtheight.Text);
        }
        decimal Number;
        if (txtNumber.Text == "")
        {
            Number = 1;
        }
        else
        {
            Number = Convert.ToDecimal(txtNumber.Text);
        }
        decimal TotalVolum = Length * Breadth * Height * Number;
        txtvolume.Text = TotalVolum.ToString();
    }
    protected void txtbreadth_TextChanged(object sender, EventArgs e)
    {
        decimal Length;
        if (txtlength.Text == "")
        {
            Length = 1;
        }
        else
        {
            Length = Convert.ToDecimal(txtlength.Text);
        }

        decimal Breadth;
        if (txtbreadth.Text == "")
        {
            Breadth = 1;
        }
        else
        {
            Breadth = Convert.ToDecimal(txtbreadth.Text);
        }
        decimal Height;
        if (txtheight.Text == "")
        {
            Height = 1;
        }
        else
        {
            Height = Convert.ToDecimal(txtheight.Text);
        }

        decimal Number;
        if (txtNumber.Text == "")
        {
            Number = 1;
        }
        else
        {
            Number = Convert.ToDecimal(txtNumber.Text);
        }
        decimal TotalVolum = Length * Breadth * Height * Number;
        txtvolume.Text = TotalVolum.ToString();
    }
    protected void txtheight_TextChanged(object sender, EventArgs e)
    {
        decimal Length;
        if (txtlength.Text == "")
        {
            Length = 1;
        }
        else
        {
            Length = Convert.ToDecimal(txtlength.Text);
        }

        decimal Breadth;
        if (txtbreadth.Text == "")
        {
            Breadth = 1;
        }
        else
        {
            Breadth = Convert.ToDecimal(txtbreadth.Text);
        }

        decimal Height;
        if (txtheight.Text == "")
        {
            Height = 1;
        }
        else
        {
            Height = Convert.ToDecimal(txtheight.Text);
        }

        decimal Number;
        if (txtNumber.Text == "")
        {
            Number = 1;
        }
        else
        {
            Number = Convert.ToDecimal(txtNumber.Text);
        }
        decimal TotalVolum = Length * Breadth * Height * Number;
        txtvolume.Text = TotalVolum.ToString();
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

        string BOQName = ddl_BOQname.SelectedItem.ToString();
        if (BOQName == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select  BOQ Name!');", true);
            return;
        }

        string BOQMstId = ddl_BOQname.SelectedValue.ToString();
        if (BOQName == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select  BOQ Name!');", true);
            return;
        }
        string Srno = ddl_Srno.SelectedItem.ToString();
        if (Srno == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select  Srno !');", true);
            return;
        }
        string ItemDescription = ddl_item.Text;
        if (ItemDescription == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select  Item Description!');", true);
            return;
        }

        string MesurmentStd_AreaVol = ddlMeasurementFormula.Text;
        if (MesurmentStd_AreaVol == "-----Select-----")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select  Formula!');", true);
            return;
        }

        string AreaOrVolum = "";
        try
        {
            AreaOrVolum = ddlMeasurementFormula.SelectedItem.Value.ToString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select  Area / volume!');", true);
            return;
        }

        decimal Area = 0;
        if (AreaOrVolum == "Area")
        {
            try
            {
                Area = Convert.ToDecimal(txtvolume.Text);
            }
            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('volume must be numeric!');", true);
                return;
            }


        }
        else if (AreaOrVolum == "volume")
        {

            try
            {
                Area = Convert.ToDecimal(txtvolume.Text);
            }
            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('volume must be numeric!');", true);
                return;
            }

        }
        else if (AreaOrVolum == "PICS")
        {

            try
            {
                Area = Convert.ToDecimal(txtvolume.Text);
            }
            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('volume must be numeric!');", true);
                return;
            }

        }
        else if (AreaOrVolum == "RUNNINGLENGTH")
        {

            try
            {
                Area = Convert.ToDecimal(txtvolume.Text);
            }
            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('volume must be numeric!');", true);
                return;
            }

        }


        string UnitId = ddl_AccUnit.SelectedItem.ToString(); ;


        string FilesUpload = string.Empty; 
        Guid gd;
        gd = Guid.NewGuid();
        string guid = gd.ToString();
        string FileName3 = "";
        if (FileUpload1.HasFile)
        {
           
                FileName3 = Path.GetFileName(FileUpload1.FileName);
                string strPath = Server.MapPath("~");
                strPath = strPath + "\\Project\\IMG\\" + guid + '_' + FileName3;
                FilesUpload = guid + '_' + FileName3;
                FileUpload1.SaveAs(strPath);
            
           
        }
         else
            {
                FilesUpload = "";
            }

        decimal Length;
        if (txtlength.Text == "")
        {
            Length = 1;
        }
        else
        {
            Length = Convert.ToDecimal(txtlength.Text);
        }


        Decimal Breadth;
        if (txtbreadth.Text=="")
        {
            Breadth = 1;
        }
        else
        {
           Breadth= Convert.ToDecimal(txtbreadth.Text);
        }

        Decimal Height;
        if (txtheight.Text=="")
        {
            Height = 1;
        }
        else
        {
            Height=Convert.ToDecimal(txtheight.Text);
        }
        decimal Number;
        if (txtNumber.Text == "")
        {
            Number = 1;
        }
        else
        {
            Number = Convert.ToDecimal(txtNumber.Text);
        }
       
        Decimal ExecQty ;
        if (txtExecQty.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Enter Exec Qty');", true);
            return;
        }
        else
        {
            ExecQty = Convert.ToDecimal(txtExecQty.Text);
        }
        String UserDate = txt_Date.Text;

        DateTime DOE = DateTime.Now;

        if (btnSave.Text != "Update")
        {
            string BOQDetailsID = DBAccess.FetchDatatable("select [dbo].[fn_BOQDtlsId]()").Rows[0][0].ToString();

            //int n = ClassBOQDetails_File.save(BOQDetailsID, BOQName, Srno, ItemDescription, MesurmentStd_AreaVol, Area, UnitId, FilesUpload, DOE, UserId, CompanyId, ProjectId, Length, Breadth, Height, Number, ExecQty, UserDate);

            //if (n > 0)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Status Save Successfully .');window.location='../GreenOxygen/BOQMeasurement.aspx';", true);
            //}
        }


    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("BOQMeasurement.aspx");
    }
    protected void ddl_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranch();
    }

    protected void ddl_Srno_TextChanged(object sender, EventArgs e)
    {
        string BoqId = ddl_BOQname.SelectedValue.ToString();
        string Srno = ddl_Srno.SelectedValue.ToString();


        DataTable dt = DBAccess.FetchDatatable("select bd.BoqDetailsId, bd.Unit, bd.Qty, bd.ItemDescripton, (select Sum(bdn.ExecQty) from [tbl_BOQDetail_New] bdn where bdn.Srno=bd.BoqDetailsSlNo) as qty1,(select bd.Qty-Sum(bdn.ExecQty) from [tbl_BOQDetail_New] bdn where bdn.Srno=bd.BoqDetailsSlNo) as balance from tbl_BoqDetails bd  where BoqId= '" + BoqId + "' and BoqDetailsSlNo= '" + Srno + "' ");

        ddl_item.DataSource = dt;
        ddl_item.ValueField = "ItemDescripton";
        ddl_item.TextField = "ItemDescripton";
        ddl_item.DataBind();

        // ddl_item.Items.Insert(0, new ListItem("--Select--", "0"));

        ddl_AccUnit.DataSource = dt;
        ddl_AccUnit.DataValueField = "Unit";
        ddl_AccUnit.DataTextField = "Unit";
        ddl_AccUnit.DataBind();

        string balance = dt.Rows[0]["qty1"].ToString();
        String ExcQty = dt.Rows[0]["balance"].ToString();

        if (balance == "")
        {
            txtBoqQty.Text = dt.Rows[0]["Qty"].ToString();
        }
        else
        {
            txtBoqQty.Text = dt.Rows[0]["balance"].ToString();
        }
        
        

    }
    protected void txtExecQty_TextChanged(object sender, EventArgs e)
    {
        decimal BoqQty = Convert.ToDecimal(txtBoqQty.Text);
        decimal ExecQty = Convert.ToDecimal(txtExecQty.Text);
        decimal BalanceQty = BoqQty - ExecQty;

        txtBalanceQty.Text = BalanceQty.ToString();


    }
    protected void ddl_item_TextChanged(object sender, EventArgs e)
    {
        
    }
    protected void tctNumber_TextChanged(object sender, EventArgs e)
    {
        decimal Length;
        if (txtlength.Text == "")
        {
            Length = 1;
        }
        else
        {
            Length = Convert.ToDecimal(txtlength.Text);
        }

        decimal Breadth;
        if (txtbreadth.Text == "")
        {
            Breadth = 1;
        }
        else
        {
            Breadth = Convert.ToDecimal(txtbreadth.Text);
        }

        decimal Height;
        if (txtheight.Text == "")
        {
            Height = 1;
        }
        else
        {
            Height = Convert.ToDecimal(txtheight.Text);
        }
        decimal Number;
        if (txtNumber.Text == "")
        {
            Number = 1;
        }
        else
        {
            Number = Convert.ToDecimal(txtNumber.Text);
        }
        decimal TotalVolum = Length * Breadth * Height * Number;
        txtvolume.Text = TotalVolum.ToString();
    }
}