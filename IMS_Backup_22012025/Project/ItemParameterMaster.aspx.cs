using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class Project_ItemParameterMaster : System.Web.UI.Page
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
                
            }
            catch

            { }
            try
            {
                UserId = Session["UserId"].ToString();
               
            }
            catch

            { }
            try
            {
                BranchId = Session["BranchId"].ToString();
                string BranchName = DBAccess.FetchDatasingle("select BranchName from tbl_Branch where BranchId='" + BranchId + "'");
               
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Please Select  Branch');", true);
                return;
            }

        }
    }
    protected void btnMake_Click(object sender, EventArgs e)
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

        string Make = txtMake.Text;
        if (Make == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter Make.');", true);
            return;
        }

        if (btnMake.Text == "Save")
        {
            bool SaveItem = DBAccess.SaveData("insert into [dbo].[tbl_MakeMaster] values('" + Make + "', '" + BranchId + "', '" + UserId + "',  convert(datetime,'" + DOE + "',103))");
            if (SaveItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Make Save Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Make Not Save. Try again.');", true);
                return;
            }
        }
        else
        {
            string RowId = ViewState["RowId"].ToString();
            bool UpdateItem = DBAccess.SaveData("Update [dbo].[tbl_MakeMaster] set Make='" + Make + "', BranchId='" + BranchId + "', UserId='" + UserId + "',  DOE=convert(datetime,'" + DOE + "',103) where RowId='" + RowId + "'");
            if (UpdateItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Make Updated Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Make Not Updated. Try again.');", true);
                return;
            }
        }

    }
    protected void ASPxGridView1_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string Rowid = obj.ToString();
        ViewState["RowId"] = Rowid;


        if (e.CommandArgs.CommandName == "MakeEdit")
        {
            string Make = DBAccess.FetchDatasingle("select Make from tbl_MakeMaster where Rowid='" + Rowid + "'");

            txtMake.Text = Make;
            btnMake.Text = "Update";
        }
    }
    protected void btnModel_Click(object sender, EventArgs e)
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

        string Model = txtModel.Text;
        if (Model == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter Model.');", true);
            return;
        }

        if (btnModel.Text == "Save")
        {
            bool SaveItem = DBAccess.SaveData("insert into [dbo].[tbl_ModelMaster] values('" + Model + "', '" + BranchId + "', '" + UserId + "',  convert(datetime,'" + DOE + "',103))");
            if (SaveItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Model Save Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Model Not Save. Try again.');", true);
                return;
            }
        }
        else
        {
            string RowId = ViewState["RowId"].ToString();
            bool UpdateItem = DBAccess.SaveData("Update [dbo].[tbl_ModelMaster] set Model='" + Model + "', BranchId='" + BranchId + "', UserId='" + UserId + "',  DOE=convert(datetime,'" + DOE + "',103) where RowId='" + RowId + "'");
            if (UpdateItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Model Updated Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Model Not Updated. Try again.');", true);
                return;
            }
        }
    }
    protected void ASPxGridView2_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string Rowid = obj.ToString();
        ViewState["RowId"] = Rowid;

        if (e.CommandArgs.CommandName == "ModelEdit")
        {
            string Model = DBAccess.FetchDatasingle("select Model from tbl_ModelMaster where Rowid='" + Rowid + "'");

            txtModel.Text = Model;
            btnModel.Text = "Update";
        }
    }
    protected void btnUnit_Click(object sender, EventArgs e)
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

        string Unit = txtUnit.Text;
        if (Unit == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter Unit.');", true);
            return;
        }

        if (btnUnit.Text == "Save")
        {
            bool SaveItem = DBAccess.SaveData("insert into [dbo].[tbl_UnitMaster] values('" + Unit + "', '" + BranchId + "', '" + UserId + "',  convert(datetime,'" + DOE + "',103))");
            if (SaveItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Unit Save Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Unit Not Save. Try again.');", true);
                return;
            }
        }
        else
        {
            string RowId = ViewState["RowId"].ToString();
            bool UpdateItem = DBAccess.SaveData("Update [dbo].[tbl_UnitMaster] set Unit='" + Unit + "', BranchId='" + BranchId + "', UserId='" + UserId + "',  DOE=convert(datetime,'" + DOE + "',103) where RowId='" + RowId + "'");
            if (UpdateItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Unit Updated Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Unit Not Updated. Try again.');", true);
                return;
            }
        }
    }
    protected void ASPxGridView3_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string Rowid = obj.ToString();
        ViewState["RowId"] = Rowid;

        if (e.CommandArgs.CommandName == "UnitEdit")
        {
            string Unit = DBAccess.FetchDatasingle("select Unit from tbl_UnitMaster where Rowid='" + Rowid + "'");

            txtUnit.Text = Unit;
            btnUnit.Text = "Update";
        }
    }
    protected void btnItemCode_Click(object sender, EventArgs e)
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

        string ItemCode = txtItemCode.Text;
        if (ItemCode == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter Item Code.');", true);
            return;
        }

        if (btnItemCode.Text == "Save")
        {
            bool SaveItem = DBAccess.SaveData("insert into [dbo].[tbl_ItemCodeMaster] values('" + ItemCode + "', '" + BranchId + "', '" + UserId + "',  convert(datetime,'" + DOE + "',103))");
            if (SaveItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Item Code Save Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Item Code Not Save. Try again.');", true);
                return;
            }
        }
        else
        {
            string RowId = ViewState["RowId"].ToString();
            bool UpdateItem = DBAccess.SaveData("Update [dbo].[tbl_ItemCodeMaster] set ItemCode='" + ItemCode + "', BranchId='" + BranchId + "', UserId='" + UserId + "',  DOE=convert(datetime,'" + DOE + "',103) where RowId='" + RowId + "'");
            if (UpdateItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Item Code Updated Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Item Code Not Updated. Try again.');", true);
                return;
            }
        }
    }
    protected void ASPxGridView4_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        object obj = e.KeyValue;
        string Rowid = obj.ToString();
        ViewState["RowId"] = Rowid;

        if (e.CommandArgs.CommandName == "ItemCodeEdit")
        {
            string ItemCode = DBAccess.FetchDatasingle("select ItemCode from tbl_ItemCodeMaster where Rowid='" + Rowid + "'");

            txtItemCode.Text = ItemCode;
            btnItemCode.Text = "Update";
        }
    }
    protected void btnPowerSupply_Click(object sender, EventArgs e)
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

        string PowerSupply = txtPowerSupply.Text;
        if (PowerSupply == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Please Enter Power Supply.');", true);
            return;
        }

        if (btnPowerSupply.Text == "Save")
        {
            bool SaveItem = DBAccess.SaveData("insert into [dbo].[tbl_PowerSupplyMaster] values('" + PowerSupply + "', '" + BranchId + "', '" + UserId + "',  convert(datetime,'" + DOE + "',103))");
            if (SaveItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Power Supply Save Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Power Supply Not Save. Try again.');", true);
                return;
            }
        }
        else
        {
            string RowId = ViewState["RowId"].ToString();
            bool UpdateItem = DBAccess.SaveData("Update [dbo].[tbl_PowerSupplyMaster] set PowerSupply='" + PowerSupply + "', BranchId='" + BranchId + "', UserId='" + UserId + "',  DOE=convert(datetime,'" + DOE + "',103) where RowId='" + RowId + "'");
            if (UpdateItem == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Power Supply Updated Successfully..');window.location='../Project/ItemParameterMaster.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Power Supply Not Updated. Try again.');", true);
                return;
            }
        }
    }
    protected void ASPxGridView5_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {

        object obj = e.KeyValue;
        string Rowid = obj.ToString();
        ViewState["RowId"] = Rowid;

        if (e.CommandArgs.CommandName == "PowerSupplyEdit")
        {
            string PowerSupply = DBAccess.FetchDatasingle("select PowerSupply from tbl_PowerSupplyMaster where Rowid='" + Rowid + "'");

            txtPowerSupply.Text = PowerSupply;
            btnPowerSupply.Text = "Update";
        }
    }
}