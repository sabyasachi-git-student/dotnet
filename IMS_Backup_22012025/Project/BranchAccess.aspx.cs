using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Utils;
using System.Data;

public partial class Project_BranchAccess : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        GridViewDataTextColumn col1 = new GridViewDataTextColumn { FieldName = "BranchName", VisibleIndex = 0, ReadOnly = true };
        col1.EditFormSettings.Visible = DefaultBoolean.False;
        ASPxGridView1.Columns.Add(col1);

        GridViewDataTextColumn col2 = new GridViewDataTextColumn { FieldName = "BranchIds", VisibleIndex = 1, ReadOnly = true, Visible = false };
        col2.EditFormSettings.Visible = DefaultBoolean.False;
        ASPxGridView1.Columns.Add(col2);

        int i = 0;
        DataTable dt = DBAccess.FetchData("Select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='tbl_BranchAccess' and COLUMN_NAME!='rowid' and COLUMN_NAME!='BranchIds'").Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            GridViewDataCheckColumn colx = new GridViewDataCheckColumn { FieldName = dr[i].ToString(), VisibleIndex = i + 2 };
            ASPxGridView1.Columns.Add(colx);
            i = i++;
        }

        GridViewCommandColumn colc = new GridViewCommandColumn { VisibleIndex = i + 2, Visible = true };
        colc.ShowEditButton = true;
        ASPxGridView1.Columns.Add(colc);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = "Select distinct BranchName,b.* from tbl_Branch a join tbl_BranchAccess b on a.BranchId=b.BranchIds";
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dt = DBAccess.FetchData("Select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='tbl_BranchAccess' and COLUMN_NAME!='rowid' and COLUMN_NAME!='BranchIds'").Tables[0];
        int i;
        string qr = "";
        for (i = 0; i < dt.Rows.Count; i++)
        {
            int nv = (Int32)e.NewValues[dt.Rows[i][0].ToString()];
            if (i == 0)
            {
                qr = String.Format("{0} = {1}", dt.Rows[i][0], nv);
            }
            else
            {
                qr = String.Format("{0} , {1} = {2}", qr, dt.Rows[i][0], nv);
            }
        }
        SqlDataSource1.UpdateCommand = String.Format("Exec childBranch @branchId='{0}',@query='{1}'", e.Keys["BranchIds"], qr);
    }
}