using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Project_HigherKeyTag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnClear_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("HigherKeyTag.aspx");
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

        string MapId = DBAccess.FetchDatatable("select  [dbo].[fn_MapId]()").Rows[0][0].ToString();

        string RegionId = "";
        if (ddlRegion.Text != "")
        {
            RegionId = ddlRegion.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Region.');", true);
            return;
        }

        string TerrytoryId = "";
        if (ddlTerrytory.Text != "")
        {
            TerrytoryId = ddlTerrytory.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Terrytory.');", true);
            return;
        }

        string SectionId = "";
        if (ddlSection.Text != "")
        {
            SectionId = ddlSection.Text; 
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter Section.');", true);
            return;
        }

        string POPId = "";
        if (ddlPOP.Text != "")
        {
            POPId = ddlPOP.Text;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter POP.');", true);
            return;
        }

        if (btnSave.Text != "Update")
        {
            int n = MastersSave.HigherKeyMappingSave(MapId, RegionId, TerrytoryId, SectionId, POPId, UserId, BranchId, DOE);
            if (n == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Higher Key Mapping Master is saved successfully..');window.location='../Project/HigherKeyTag.aspx';", true);
            }
            else if (n == -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Higher Key Mapping Master is already exist.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorry! Higher Key Mapping Master is not saved. Try again.');", true);
                return;
            }
        }
        else
        {

        }
    }
}