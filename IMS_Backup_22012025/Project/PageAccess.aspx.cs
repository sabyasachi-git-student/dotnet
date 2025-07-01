using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Utils;

public partial class Project_PageAccess : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        GridViewDataTextColumn col1 = new GridViewDataTextColumn { FieldName = "ContentName", VisibleIndex = 0, ReadOnly = true };
        col1.EditFormSettings.Visible = DefaultBoolean.False;
        ASPxGridView1.Columns.Add(col1);

        GridViewDataTextColumn col2 = new GridViewDataTextColumn { FieldName = "ContentType", VisibleIndex = 1, ReadOnly = true };
        col2.EditFormSettings.Visible = DefaultBoolean.False;
        ASPxGridView1.Columns.Add(col2);

        GridViewDataTextColumn col3 = new GridViewDataTextColumn { FieldName = "ParentContentName", VisibleIndex = 2, ReadOnly = true };
        col3.EditFormSettings.Visible = DefaultBoolean.False;
        ASPxGridView1.Columns.Add(col3);

        GridViewDataTextColumn col4 = new GridViewDataTextColumn { FieldName = "ContentsId", VisibleIndex = 3, ReadOnly = true, Visible = false };
        col4.EditFormSettings.Visible = DefaultBoolean.False;
        ASPxGridView1.Columns.Add(col4);

        int i = 0;
        DataTable dt = DBAccess.FetchData("Select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='tbl_Access' and COLUMN_NAME!='rowid' and COLUMN_NAME!='ContentsId'").Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            GridViewDataCheckColumn colx = new GridViewDataCheckColumn { FieldName = dr[i].ToString(), VisibleIndex = i + 4 };
            ASPxGridView1.Columns.Add(colx);
            i = i++;
        }

        GridViewCommandColumn colc = new GridViewCommandColumn { VisibleIndex = i + 4, Visible = true };
        //colc.EditButton.Visible = true;
        colc.ShowEditButton = true;
        ASPxGridView1.Columns.Add(colc);

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        SqlDataSource1.SelectCommand = "Select ContentName,ContentType,(Select ContentName from tbl_Contents where ContentID=a.ParentContentID) as ParentContentName,b.* from tbl_Contents a join tbl_Access b on a.ContentID=b.ContentsId";
    }

    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dt = DBAccess.FetchData("Select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='tbl_Access' and COLUMN_NAME!='rowid' and COLUMN_NAME!='ContentsId'").Tables[0];
        int i;
        string qr = "";
        for(i=0;i<dt.Rows.Count;i++)
        {
            int nv =Convert.ToInt32(e.NewValues[dt.Rows[i][0].ToString()]==null?"0":e.NewValues[(dt.Rows[i][0].ToString())]);
            if (i == 0)
            {
                qr = String.Format("{0} = {1}", dt.Rows[i][0], nv);
            }
            else
            {
                qr = String.Format("{0} , {1} = {2}", qr, dt.Rows[i][0], nv);
            }
        }
        SqlDataSource1.UpdateCommand = String.Format("Exec childFolder @contentId='{0}',@query='{1}'", e.Keys["ContentsId"], qr);
    }
}